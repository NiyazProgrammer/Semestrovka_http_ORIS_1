namespace Server;

public static class ConfigManager
{
    private static AppSettings _config;

    public static AppSettings GetConfig()
    {
        if (_config == null)
            _config = ConfigLoader.LoadConfig(@"appSetting.json");
        return _config;
    }
}