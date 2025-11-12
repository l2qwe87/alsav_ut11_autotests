namespace AlsavUt11Autotests.Config;

public class TestSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Browser { get; set; } = "Chrome";
    public int TimeoutSeconds { get; set; } = 30;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Headless { get; set; } = false;
}