using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using AlsavUt11Autotests.Pages;
using OpenQA.Selenium;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class QuickInterfaceAnalysisTests
{
    [Test]
    public void QuickAnalyze1CInterface()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        
        var loginPage = new LoginPage(TestSetup.Browser);
        loginPage.Login(TestSetup.Settings.Username, TestSetup.Settings.Password);
        
        // Подождем загрузки
        System.Threading.Thread.Sleep(3000);
        
        Console.WriteLine($"Page title: {TestSetup.Browser.Driver.Title}");
        Console.WriteLine($"URL: {TestSetup.Browser.Driver.Url}");
        
        // Быстрая проверка на фреймы
        var iframes = TestSetup.Browser.Driver.FindElements(By.TagName("iframe"));
        Console.WriteLine($"Found {iframes.Count} iframes");
        
        if (iframes.Count > 0)
        {
            var firstIframe = iframes[0];
            var id = firstIframe.GetAttribute("id");
            var name = firstIframe.GetAttribute("name");
            Console.WriteLine($"First iframe: Id='{id}', Name='{name}'");
            
            try
            {
                TestSetup.Browser.Driver.SwitchTo().Frame(firstIframe);
                var bodyText = TestSetup.Browser.Driver.FindElement(By.TagName("body")).Text;
                Console.WriteLine($"Frame body text (first 100 chars): {bodyText.Substring(0, Math.Min(100, bodyText.Length))}");
                TestSetup.Browser.Driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error switching to frame: {ex.Message}");
            }
        }
        
        // Проверим основные div элементы
        var divs = TestSetup.Browser.Driver.FindElements(By.CssSelector("div[id], div[class*='panel'], div[class*='menu'], div[class*='toolbar']"));
        Console.WriteLine($"Found {divs.Count} significant div elements");
        
        foreach (var div in divs.Take(5))
        {
            var id = div.GetAttribute("id");
            var className = div.GetAttribute("class");
            Console.WriteLine($"Div: Id='{id}', Class='{className}'");
        }
        
        TestSetup.Browser.Quit();
    }
}