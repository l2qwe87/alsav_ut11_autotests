using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using AlsavUt11Autotests.Pages;
using OpenQA.Selenium;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class SuccessfulLoginAnalysisTests
{
    [Test]
    public void AnalyzeSuccessfulLogin()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        
        var loginPage = new LoginPage(TestSetup.Browser);
        loginPage.Login("admin", "admin");
        
        // Подождем загрузки
        System.Threading.Thread.Sleep(3000);
        
        Console.WriteLine($"Page title: {TestSetup.Browser.Driver.Title}");
        Console.WriteLine($"Current URL: {TestSetup.Browser.Driver.Url}");
        
        // Проверим, изменилась ли страница
        if (TestSetup.Browser.Driver.Url != TestSetup.Settings.BaseUrl)
        {
            Console.WriteLine("URL changed - login might be successful");
        }
        
        // Поищем признаки успешного входа
        var bodyText = TestSetup.Browser.Driver.FindElement(By.TagName("body")).Text;
        Console.WriteLine($"Body text (first 200 chars): {bodyText.Substring(0, Math.Min(200, bodyText.Length))}");
        
        // Проверим наличие основных элементов интерфейса 1С
        var possibleIndicators = new[]
        {
            By.CssSelector("[class*='panel']"),
            By.CssSelector("[class*='menu']"),
            By.CssSelector("[class*='toolbar']"),
            By.CssSelector("[class*='workspace']"),
            By.CssSelector("[class*='section']"),
            By.CssSelector("iframe"),
            By.TagName("frame"),
        };
        
        Console.WriteLine("\nSearching for 1C interface elements:");
        foreach (var selector in possibleIndicators)
        {
            try
            {
                var elements = TestSetup.Browser.Driver.FindElements(selector);
                if (elements.Count > 0)
                {
                    Console.WriteLine($"Found {elements.Count} elements for selector: {selector}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with selector {selector}: {ex.Message}");
            }
        }
        
        TestSetup.Browser.Quit();
    }
}