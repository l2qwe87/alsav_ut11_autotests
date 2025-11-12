using NUnit.Framework;
using AlsavUt11Autotests.Tests;
using AlsavUt11Autotests.Pages;

namespace AlsavUt11Autotests.Tests;

[TestFixture]
public class LoginTests
{
    private LoginPage? _loginPage;
    private MainPage? _mainPage;

    [SetUp]
    public void SetUp()
    {
        TestSetup.Browser!.Start();
        TestSetup.Browser.NavigateTo(TestSetup.Settings!.BaseUrl);
        _loginPage = new LoginPage(TestSetup.Browser);
        _mainPage = new MainPage(TestSetup.Browser);
    }

    [TearDown]
    public void TearDown()
    {
        TestSetup.Browser!.Quit();
    }

    [Test]
    public void Login_ValidCredentials_ShouldNavigateToMainPage()
    {
        var loginResult = _loginPage!.Login(TestSetup.Settings!.Username, TestSetup.Settings.Password);
        
        Assert.That(loginResult, Is.True, "Login should succeed with informational alerts");
        
        // Проверим, что основная страница 1С загрузилась
        System.Threading.Thread.Sleep(TestSetup.Settings!.PageLoadWaitMilliseconds);
        Assert.That(_mainPage!.IsLoaded(), Is.True, "Main 1C interface should be loaded after login");
        Assert.That(_mainPage.IsMainInterfaceVisible(), Is.True, "Main interface elements should be visible");
        
        Console.WriteLine($"Page title: {_mainPage.GetPageTitle()}");
        Console.WriteLine($"Iframe content: {_mainPage.GetIframeContent()}");
    }

    [Test]
    public void Login_InvalidCredentials_ShouldShowErrorMessage()
    {
        var loginResult = _loginPage!.Login("invalid_user", "invalid_password");
        
        Assert.That(loginResult, Is.False, "Login should fail with invalid credentials");
    }

    [Test]
    public void Logout_ShouldReturnToLoginPage()
    {
        _loginPage!.Login(TestSetup.Settings!.Username, TestSetup.Settings.Password);
        
        // TODO: Реализовать метод выхода после определения селекторов
        // _mainPage!.Logout();
        
        Assert.That(_mainPage!.IsLoaded(), Is.True, "Main page should be loaded after login");
    }
}