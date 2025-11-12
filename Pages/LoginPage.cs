using OpenQA.Selenium;
using AlsavUt11Autotests.Utils;

namespace AlsavUt11Autotests.Pages;

public class LoginPage : BasePage
{
    private readonly By _usernameField = By.Id("userName");
    private readonly By _passwordField = By.Id("userPassword");
    private readonly By _loginButton = By.Id("okButton");
    private readonly By _errorMessage = By.CssSelector(".error-message");

    public LoginPage(BrowserManager browser) : base(browser)
    {
    }

    public void EnterUsername(string username)
    {
        Type(_usernameField, username);
    }

    public void EnterPassword(string password)
    {
        Type(_passwordField, password);
    }

    public void ClickLogin()
    {
        Click(_loginButton);
    }

    public bool Login(string username, string password)
    {
        EnterUsername(username);
        EnterPassword(password);
        Click(_loginButton);
        
        // Подождем и обработаем информационные алерты (про шрифты и т.д.)
        System.Threading.Thread.Sleep(_browser.Settings.AlertWaitMilliseconds);
        
        try
        {
            var alert = _browser.Driver.SwitchTo().Alert();
            var alertText = alert.Text;
            Console.WriteLine($"Informational alert: {alertText}");
            alert.Accept();
            
            // После информационного алерта может появиться еще один
            System.Threading.Thread.Sleep(1000);
            try
            {
                var secondAlert = _browser.Driver.SwitchTo().Alert();
                Console.WriteLine($"Second informational alert: {secondAlert.Text}");
                secondAlert.Accept();
            }
            catch (OpenQA.Selenium.NoAlertPresentException)
            {
                // Второго алерта нет
            }
            
            return true; // Вход успешный, были только информационные алерты
        }
        catch (OpenQA.Selenium.NoAlertPresentException)
        {
            return true; // Нет алертов - вход успешный
        }
    }

    public bool IsErrorMessageVisible()
    {
        return IsVisible(_errorMessage);
    }

    public string GetErrorMessage()
    {
        return GetText(_errorMessage);
    }

    public override bool IsLoaded()
    {
        return IsVisible(_usernameField) && IsVisible(_passwordField);
    }
}