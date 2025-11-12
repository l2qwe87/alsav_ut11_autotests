using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using OpenQA.Selenium;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class PageAnalysisTests
{
    [Test]
    public void AnalyzeLoginPage()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        
        // Выведем HTML страницы для анализа
        Console.WriteLine("Page HTML:");
        Console.WriteLine(TestSetup.Browser.Driver.PageSource);
        
        // Поищем возможные поля для ввода имени пользователя
        var possibleUserSelectors = new[]
        {
            By.Id("username"),
            By.Id("user"),
            By.Id("login"),
            By.Id("user-name"),
            By.Name("username"),
            By.Name("user"),
            By.Name("login"),
            By.CssSelector("input[type='text']"),
            By.CssSelector("input[name*='user']"),
            By.CssSelector("input[id*='user']"),
            By.CssSelector("input[placeholder*='имя']"),
            By.CssSelector("input[placeholder*='пользователь']"),
        };
        
        Console.WriteLine("\nSearching for username fields:");
        foreach (var selector in possibleUserSelectors)
        {
            try
            {
                var elements = TestSetup.Browser.Driver.FindElements(selector);
                if (elements.Count > 0)
                {
                    Console.WriteLine($"Found {elements.Count} elements for selector: {selector}");
                    foreach (var element in elements)
                    {
                        Console.WriteLine($"  - Tag: {element.TagName}, Id: {element.GetAttribute("id")}, Name: {element.GetAttribute("name")}, Placeholder: {element.GetAttribute("placeholder")}, Type: {element.GetAttribute("type")}");
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