using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using OpenQA.Selenium;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class SimplePageAnalysisTests
{
    [Test]
    public void FindInputFields()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        
        // Найдем все поля ввода на странице
        var inputs = TestSetup.Browser.Driver.FindElements(By.TagName("input"));
        Console.WriteLine($"Found {inputs.Count} input elements:");
        
        for (int i = 0; i < inputs.Count; i++)
        {
            var input = inputs[i];
            Console.WriteLine($"Input {i + 1}:");
            Console.WriteLine($"  Type: {input.GetAttribute("type")}");
            Console.WriteLine($"  Id: {input.GetAttribute("id")}");
            Console.WriteLine($"  Name: {input.GetAttribute("name")}");
            Console.WriteLine($"  Placeholder: {input.GetAttribute("placeholder")}");
            Console.WriteLine($"  Class: {input.GetAttribute("class")}");
            Console.WriteLine();
        }
        
        // Найдем все кнопки
        var buttons = TestSetup.Browser.Driver.FindElements(By.CssSelector("button, input[type='submit'], input[type='button']"));
        Console.WriteLine($"Found {buttons.Count} button elements:");
        
        for (int i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            Console.WriteLine($"Button {i + 1}:");
            Console.WriteLine($"  Tag: {button.TagName}");
            Console.WriteLine($"  Type: {button.GetAttribute("type")}");
            Console.WriteLine($"  Id: {button.GetAttribute("id")}");
            Console.WriteLine($"  Name: {button.GetAttribute("name")}");
            Console.WriteLine($"  Text: {button.Text}");
            Console.WriteLine($"  Value: {button.GetAttribute("value")}");
            Console.WriteLine();
        }
        
        TestSetup.Browser.Quit();
    }
}