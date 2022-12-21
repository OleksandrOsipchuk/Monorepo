﻿using System.Text.Json.Nodes;
using CliWrap;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Telegram.Bot;

namespace ITSadok.DotNetMentorship.Admin.API.Services;

public class TunnelService : BackgroundService
{
    private readonly IServer _server;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IConfiguration _config;
    private readonly ILogger<TunnelService> _logger;
    private readonly ITelegramBotClient _telegramBot;


    public TunnelService(IServer server, IHostApplicationLifetime hostApplicationLifetime,
        IConfiguration config, ILogger<TunnelService> logger, BotService botService)
    {
        this._server = server;
        this._hostApplicationLifetime = hostApplicationLifetime;
        this._config = config;
        this._logger = logger;
        this._telegramBot = botService.Bot;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await WaitForApplicationStarted();

        var urls = _server.Features.Get<IServerAddressesFeature>()!.Addresses;
        // Use https:// if you authenticated ngrok, otherwise, you can only use http://
        var localUrl = urls.Single(u => u.StartsWith("http://"));

        _logger.LogInformation("Starting ngrok tunnel for {LocalUrl}", localUrl);
        var ngrokTask = StartNgrokTunnel(localUrl, stoppingToken);

        var publicUrl = await GetNgrokPublicUrl();
        _logger.LogInformation("Public ngrok URL: {NgrokPublicUrl}", publicUrl);

        var urlForWebhook = publicUrl + "/api/webhook";
        await _telegramBot.SetWebhookAsync(urlForWebhook);
        _logger.LogInformation("Webhook for telegram bot was set: {NgrokPublicUrl}", urlForWebhook);

        await ngrokTask;

        _logger.LogInformation("Ngrok tunnel stopped");
    }

    private Task WaitForApplicationStarted()
    {
        var completionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        _hostApplicationLifetime.ApplicationStarted.Register(() => completionSource.TrySetResult());
        return completionSource.Task;
    }

    private CommandTask<CommandResult> StartNgrokTunnel(string localUrl, CancellationToken stoppingToken)
    {
        var ngrokTask = Cli.Wrap("ngrok")
            .WithArguments(args => args
                .Add("http")
                .Add(localUrl)
                .Add("--log")
                .Add("stdout"))
            .WithStandardOutputPipe(PipeTarget.ToDelegate(s => _logger.LogDebug(s)))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(s => _logger.LogError(s)))
            .ExecuteAsync(stoppingToken);

        return ngrokTask;
    }

    private async Task<string> GetNgrokPublicUrl()
    {
        using var httpClient = new HttpClient();
        for (var ngrokRetryCount = 0; ngrokRetryCount < 10; ngrokRetryCount++)
        {
            _logger.LogDebug("Get ngrok tunnels attempt: {RetryCount}", ngrokRetryCount + 1);

            try
            {
                var json = await httpClient.GetFromJsonAsync<JsonNode>("http://127.0.0.1:4040/api/tunnels");
                var publicUrl = json["tunnels"].AsArray()
                    .Select(e => e["public_url"].GetValue<string>())
                    .SingleOrDefault(u => u.StartsWith("https://"));
                if (!string.IsNullOrEmpty(publicUrl)) return publicUrl;
            }
            catch
            {
                // ignored
            }

            await Task.Delay(200);
        }

        throw new Exception("Ngrok dashboard did not start in 10 tries");
    }
}