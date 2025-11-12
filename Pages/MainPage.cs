using OpenQA.Selenium;
using AlsavUt11Autotests.Utils;

namespace AlsavUt11Autotests.Pages;

public class MainPage : BasePage
{
    private readonly By _mainSurface = By.Id("mainSurface");
    private readonly By _captionbar = By.Id("captionbar");
    private readonly By _captionbarTitle = By.Id("captionbarTitle");
    private readonly By _iframe = By.TagName("iframe");

    public MainPage(BrowserManager browser) : base(browser)
    {
    }

    private void SwitchToIframe()
    {
        var iframe = _browser.FindElement(_iframe);
        _browser.Driver.SwitchTo().Frame(iframe);
    }

    private void SwitchToDefaultContent()
    {
        _browser.Driver.SwitchTo().DefaultContent();
    }

    public bool IsMainInterfaceVisible()
    {
        try
        {
            SwitchToDefaultContent();
            return _browser.IsElementVisible(_mainSurface) && 
                   _browser.IsElementVisible(_captionbar);
        }
        catch
        {
            return false;
        }
    }

    public string GetPageTitle()
    {
        try
        {
            SwitchToDefaultContent();
            var element = _browser.FindElement(_captionbarTitle);
            return element.Text;
        }
        catch
        {
            return "";
        }
    }

    public string GetIframeContent()
    {
        try
        {
            SwitchToIframe();
            var body = _browser.FindElement(By.TagName("body"));
            return body.Text.Substring(0, Math.Min(100, body.Text.Length));
        }
        catch
        {
            return "";
        }
        finally
        {
            SwitchToDefaultContent();
        }
    }

    public override bool IsLoaded()
    {
        return IsMainInterfaceVisible();
    }
}