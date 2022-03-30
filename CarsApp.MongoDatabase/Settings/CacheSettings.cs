namespace CarsApp.MongoDatabase.Settings;

public class CacheSettings
{
    public CacheSettings()
    {
        Host = String.Empty;
        Port = String.Empty;
    }

    public string Host { get; set; }

    public string Port { get; set; }

    public TimeSpan ExpirationTime() => TimeSpan.FromSeconds(30);
}
