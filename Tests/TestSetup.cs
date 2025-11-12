using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using AlsavUt11Autotests.Config;
using AlsavUt11Autotests.Utils;
using AlsavUt11Autotests.Pages;

namespace AlsavUt11Autotests.Tests;

[SetUpFixture]
public class TestSetup
{
    public static BrowserManager? Browser { get; private set; }
    public static TestSettings? Settings { get; private set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(TestContext.CurrentContext.TestDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        Settings = config.Get<TestSettings>() ?? new TestSettings();
        Browser = new BrowserManager(Settings);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Browser?.Quit();
    }
}