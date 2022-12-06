using System;

namespace API;

public class BotSettings
{
    public const string PropertiesSection = "BotSettings";
    public string TelegramApiKey { get; set; } = string.Empty;
}
