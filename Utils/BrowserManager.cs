using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using AlsavUt11Autotests.Config;

namespace AlsavUt11Autotests.Utils;

public class BrowserManager
{
    private IWebDriver? _driver;
    private WebDriverWait? _wait;
    private readonly TestSettings _settings;

    public BrowserManager(TestSettings settings)
    {
        _settings = settings;
    }

    public IWebDriver Driver => _driver ?? throw new InvalidOperationException("Driver not initialized");
    public WebDriverWait Wait => _wait ?? throw new InvalidOperationException("Wait not initialized");
    public TestSettings Settings => _settings;

    public void Start()
    {
        var options = new ChromeOptions();
        if (_settings.Headless)
        {
            options.AddArgument("--headless");
        }
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--disable-gpu");

        _driver = new ChromeDriver(options);
        _driver.Manage().Window.Maximize();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_settings.TimeoutSeconds);
        
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_settings.TimeoutSeconds));
    }

    public void Quit()
    {
        _driver?.Quit();
        _driver = null;
        _wait = null;
    }

    public void NavigateTo(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }

    public IWebElement FindElement(By locator)
    {
        return Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
    }

    public void ClickElement(By locator)
    {
        var element = FindElement(locator);
        element.Click();
    }

    public void TypeText(By locator, string text)
    {
        var element = FindElement(locator);
        element.Clear();
        element.SendKeys(text);
    }

    public bool IsElementVisible(By locator, int timeoutSeconds = 5)
    {
        try
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)) != null;
        }
        catch
        {
            return false;
        }
    }
}