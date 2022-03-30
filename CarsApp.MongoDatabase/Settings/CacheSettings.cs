namespace CarsApp.MongoDatabase.Settings;

public class CacheSettings
{
    public CacheSettings()
    {
        Host = string.Empty;
        Port = string.Empty;
        StreamName = string.Empty;
    }

    public string Host { get; set; }

    public string Port { get; set; }

    public string StreamName { get; set; }

    public TimeSpan ExpirationTime() => TimeSpan.FromSeconds(30);
}
