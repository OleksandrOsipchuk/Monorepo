using ITSadok.DotNetMentorship.Admin.API.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ITSadok.DotNetMentorship.Admin.API.Controllers;


[ApiController]
[Route("api/webhook")]
public class WebHookController : ControllerBase
{
    private readonly ILogger<WebHookController> _logger;
    private readonly ITelegramBotClient _telegramBot;

    public WebHookController(ILogger<WebHookController> logger, BotService botService)
    {
        _logger = logger;
        _telegramBot = botService.Bot;
    }

    [HttpPost]
    public async Task<IActionResult> WebHook([FromBody] Update update)
    {
        Message? ReceivedMessage = update.Message;
        if (ReceivedMessage.Type == MessageType.Text && ReceivedMessage.Text.StartsWith("/start"))
        {
            await _telegramBot.SendTextMessageAsync(
                chatId: update.Message.Chat,
                text: "Hello World"
            );
        }
        return Ok();

    }
}
