namespace FreeCourse.Services.Basket.Settings;

public sealed class RedisSettings
{
    public RedisSettings(string host, int port) : this()
    {
        Host = host;
        Port = port;
    }
    public RedisSettings()
    {

    }
    public string Host { get; set; }
    public int Port { get; set; }
}
