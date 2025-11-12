using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using OpenQA.Selenium;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class ConnectionTests
{
    [Test]
    public void TestAuthenticationMethods()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        
        Console.WriteLine($"Initial page title: {TestSetup.Browser.Driver.Title}");
        Console.WriteLine($"Initial URL: {TestSetup.Browser.Driver.Url}");
        
        // Проверим, есть ли варианты аутентификации
        var authOptions = TestSetup.Browser.Driver.FindElements(By.CssSelector("[class*='auth'], [class*='login'], [id*='auth'], [id*='login']"));
        Console.WriteLine($"Found {authOptions.Count} potential auth elements");
        
        // Проверим, может есть кнопки для разных способов входа
        var buttons = TestSetup.Browser.Driver.FindElements(By.CssSelector("button, input[type='button'], a"));
        Console.WriteLine($"Found {buttons.Count} clickable elements:");
        
        foreach (var button in buttons.Take(10))
        {
            try
            {
                var text = button.Text.Trim();
                var id = button.GetAttribute("id");
                var className = button.GetAttribute("class");
                
                if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(id))
                {
                    Console.WriteLine($"  - Text: '{text}', Id: '{id}', Class: '{className}'");
                }
            }
            catch
            {
                // Пропускаем элементы, которые вызывают ошибки
            }
        }
        
        // Попробуем войти с пустыми полями (возможно используется аутентификация ОС)
        Console.WriteLine("\nTrying login with empty fields (OS authentication)...");
        
        var userNameField = TestSetup.Browser.Driver.FindElement(By.Id("userName"));
        var userPasswordField = TestSetup.Browser.Driver.FindElement(By.Id("userPassword"));
        var loginButton = TestSetup.Browser.Driver.FindElement(By.Id("okButton"));
        
        userNameField.Clear();
        userPasswordField.Clear();
        loginButton.Click();
        
        System.Threading.Thread.Sleep(3000);
        
        try
        {
            var alert = TestSetup.Browser.Driver.SwitchTo().Alert();
            Console.WriteLine($"Alert: {alert.Text}");
            alert.Accept();
        }
        catch (NoAlertPresentException)
        {
            Console.WriteLine("No alert - checking if login successful");
            Console.WriteLine($"Page title after login: {TestSetup.Browser.Driver.Title}");
            Console.WriteLine($"URL after login: {TestSetup.Browser.Driver.Url}");
        }
        
        TestSetup.Browser.Quit();
    }
}