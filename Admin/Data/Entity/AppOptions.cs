namespace Admin.Data.Entity
{
    public class AppProperties
    {
        public const string PropertiesSection = "AppProperties";
        public string TelegramApiKey { get; set; } = string.Empty;
    }
}
