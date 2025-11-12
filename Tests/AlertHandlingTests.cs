using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using AlsavUt11Autotests.Pages;
using OpenQA.Selenium;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class AlertHandlingTests
{
    [Test]
    public void TestLoginWithAlertHandling()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        
        var loginPage = new LoginPage(TestSetup.Browser);
        loginPage.Login(TestSetup.Settings.Username, TestSetup.Settings.Password);
        
        // Подождем немного и проверим на alert
        System.Threading.Thread.Sleep(2000);
        
        try
        {
            var alert = TestSetup.Browser.Driver.SwitchTo().Alert();
            Console.WriteLine($"Alert detected: {alert.Text}");
            alert.Accept();
            Console.WriteLine("Alert accepted");
        }
        catch (NoAlertPresentException)
        {
            Console.WriteLine("No alert present");
        }
        
        // Проверим страницу после обработки alert
        Console.WriteLine($"Page title: {TestSetup.Browser.Driver.Title}");
        Console.WriteLine($"Current URL: {TestSetup.Browser.Driver.Url}");
        
        TestSetup.Browser.Quit();
    }
}