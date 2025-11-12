using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using AlsavUt11Autotests.Pages;
using OpenQA.Selenium;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class PostLoginAnalysisTests
{
    [Test]
    public void Analyze1CInterfaceAfterLogin()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        
        var loginPage = new LoginPage(TestSetup.Browser);
        loginPage.Login(TestSetup.Settings.Username, TestSetup.Settings.Password);
        
        // Подождем полной загрузки интерфейса 1С
        System.Threading.Thread.Sleep(5000);
        
        Console.WriteLine($"Page title after login: {TestSetup.Browser.Driver.Title}");
        Console.WriteLine($"Current URL after login: {TestSetup.Browser.Driver.Url}");
        
        // Проверим наличие iframe - 1С часто использует фреймы
        var iframes = TestSetup.Browser.Driver.FindElements(By.TagName("iframe"));
        var frames = TestSetup.Browser.Driver.FindElements(By.TagName("frame"));
        
        Console.WriteLine($"Found {iframes.Count} iframes and {frames.Count} frames");
        
        if (iframes.Count > 0)
        {
            Console.WriteLine("Analyzing iframes:");
            for (int i = 0; i < iframes.Count; i++)
            {
                var iframe = iframes[i];
                var id = iframe.GetAttribute("id");
                var name = iframe.GetAttribute("name");
                var src = iframe.GetAttribute("src");
                Console.WriteLine($"  IFrame {i + 1}: Id='{id}', Name='{name}', Src='{src}'");
                
                try
                {
                    TestSetup.Browser.Driver.SwitchTo().Frame(i);
                    var frameTitle = TestSetup.Browser.Driver.Title;
                    var frameElements = TestSetup.Browser.Driver.FindElements(By.CssSelector("*")).Count;
                    Console.WriteLine($"    Frame title: {frameTitle}, Elements count: {frameElements}");
                    TestSetup.Browser.Driver.SwitchTo().DefaultContent();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"    Error analyzing frame: {ex.Message}");
                    TestSetup.Browser.Driver.SwitchTo().DefaultContent();
                }
            }
        }
        
        // Поищем основные элементы интерфейса 1С
        var interfaceSelectors = new[]
        {
            By.CssSelector("[class*='panel']"),
            By.CssSelector("[class*='menu']"),
            By.CssSelector("[class*='toolbar']"),
            By.CssSelector("[class*='ribbon']"),
            By.CssSelector("[class*='nav']"),
            By.CssSelector("[class*='sidebar']"),
            By.CssSelector("[class*='workspace']"),
            By.CssSelector("[class*='desktop']"),
            By.CssSelector("[class*='main']"),
            By.CssSelector("[id*='panel']"),
            By.CssSelector("[id*='menu']"),
            By.CssSelector("[id*='toolbar']"),
        };
        
        Console.WriteLine("\nSearching for 1C interface elements:");
        foreach (var selector in interfaceSelectors)
        {
            try
            {
                var elements = TestSetup.Browser.Driver.FindElements(selector);
                if (elements.Count > 0)
                {
                    Console.WriteLine($"Found {elements.Count} elements for selector: {selector}");
                    foreach (var element in elements.Take(2))
                    {
                        Console.WriteLine($"  - Tag: {element.TagName}, Id: {element.GetAttribute("id")}, Class: {element.GetAttribute("class")}, Text: {element.Text.Substring(0, Math.Min(30, element.Text.Length))}");
                    }
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