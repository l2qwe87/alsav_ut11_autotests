using OpenQA.Selenium;
using AlsavUt11Autotests.Utils;

namespace AlsavUt11Autotests.Pages;

public class BasePage
{
    protected readonly BrowserManager _browser;

    public BasePage(BrowserManager browser)
    {
        _browser = browser;
    }

    public virtual bool IsLoaded()
    {
        return true;
    }

    protected void Click(By locator)
    {
        _browser.ClickElement(locator);
    }

    protected void Type(By locator, string text)
    {
        _browser.TypeText(locator, text);
    }

    protected bool IsVisible(By locator)
    {
        return _browser.IsElementVisible(locator);
    }

    protected string GetText(By locator)
    {
        return _browser.FindElement(locator).Text;
    }
}