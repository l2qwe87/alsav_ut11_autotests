using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using AlsavUt11Autotests.Pages;
using OpenQA.Selenium;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class AuthenticationTests
{
    [Test]
    public void TestDifferentLoginMethods()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        
        var loginPage = new LoginPage(TestSetup.Browser);
        
        // Проверим, какие поля видны на странице
        var userNameField = TestSetup.Browser.Driver.FindElement(By.Id("userName"));
        var userPasswordField = TestSetup.Browser.Driver.FindElement(By.Id("userPassword"));
        var emailField = TestSetup.Browser.Driver.FindElement(By.Id("email"));
        
        Console.WriteLine($"Username field visible: {userNameField.Displayed}");
        Console.WriteLine($"Password field visible: {userPasswordField.Displayed}");
        Console.WriteLine($"Email field visible: {emailField.Displayed}");
        
        // Попробуем войти с разными данными
        Console.WriteLine("\nTrying login with autotest/empty password...");
        loginPage.Login("autotest", "");
        
        try
        {
            var alert = TestSetup.Browser.Driver.SwitchTo().Alert();
            Console.WriteLine($"Alert: {alert.Text}");
            alert.Accept();
        }
        catch (NoAlertPresentException)
        {
            Console.WriteLine("No alert");
        }
        
        // Попробуем с другими возможными вариантами
        Console.WriteLine("\nTrying login with admin/admin...");
        TestSetup.Browser.NavigateTo(TestSetup.Settings.BaseUrl);
        loginPage.Login("admin", "admin");
        
        try
        {
            var alert = TestSetup.Browser.Driver.SwitchTo().Alert();
            Console.WriteLine($"Alert: {alert.Text}");
            alert.Accept();
        }
        catch (NoAlertPresentException)
        {
            Console.WriteLine("No alert - login might be successful");
            Console.WriteLine($"Page title: {TestSetup.Browser.Driver.Title}");
        }
        
        TestSetup.Browser.Quit();
    }
}