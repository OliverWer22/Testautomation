using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

[TestFixture]
public class VerifyLinksTest
{

    private IWebDriver driver;
    public IDictionary<string, object> vars { get; private set; }
    private IJavaScriptExecutor js;
    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();
        driver.Navigate().GoToUrl("https://norban.se/");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")));
        driver.Manage().Window.Size = new System.Drawing.Size(1552, 832);
    }
    public void Initialize()
    {
        driver.Navigate().GoToUrl("https://norban.se/");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")));
        driver.Manage().Window.Size = new System.Drawing.Size(1552, 832);
    }

    [TearDown]
    protected void TearDown()
    {
        driver.Quit();
    }
    
    public void Cleanup()
    {
        driver.Manage().Cookies.DeleteAllCookies();
    }

    [Test]
    public void verifyHomeButton()
    {
        driver.FindElement(By.CssSelector(".Header-logo")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")));
        Assert.That(driver.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")).Text, Is.EqualTo("Mänskliga mäklare. Digitala möjligheter."));
        driver.FindElement(By.LinkText("Så funkar Norban")).Click();
        WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult2 = wait.Until(e => e.FindElement(By.CssSelector(".SellPage-introTitle")));
        Assert.That(driver.FindElement(By.CssSelector(".SellPage-introTitle")).Text, Is.EqualTo("Varför stressa igenom ditt livs största affär?"));
        driver.FindElement(By.CssSelector(".Header-logo")).Click();
        WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult3 = wait.Until(e => e.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")));
        Assert.That(driver.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")).Text, Is.EqualTo("Mänskliga mäklare. Digitala möjligheter."));

    }
    [Test]
    public void verifyNyproduktion()
    {
        driver.FindElement(By.LinkText("Nyproduktion")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".NewConstruction-title")));
        Assert.That(driver.FindElement(By.CssSelector(".NewConstruction-title")).Text, Is.EqualTo("Nyproduktion"));
    }
    [Test]
    public void verifySåfunkarNorban()
    {
        driver.FindElement(By.LinkText("Så funkar Norban")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".SellPage-introTitle")));
        Assert.That(driver.FindElement(By.CssSelector(".SellPage-introTitle")).Text, Is.EqualTo("Varför stressa igenom ditt livs största affär?"));
    }
    [Test]
    public void logIn()
    {
        driver.FindElement(By.CssSelector("li .AccountIcon div:nth-child(1)")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")));
        driver.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")).Click();
        WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult1 = wait.Until(e => e.FindElement(By.CssSelector(".AuthForms-formInput:nth-child(1)")));
        driver.FindElement(By.CssSelector(".AuthForms-formInput:nth-child(1)")).SendKeys("oliver.werthen@hotmail.com");
        driver.FindElement(By.CssSelector(".AuthForms-formInput:nth-child(2)")).SendKeys("TestTest123");
        driver.FindElement(By.CssSelector(".AuthForms-submitBtn")).Click();
        WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult2 = wait.Until(e => e.FindElement(By.CssSelector(".AccountPage-linkButton")));
        var elements = driver.FindElements(By.CssSelector(".AccountEmptyState-img"));
        Assert.True(elements.Count > 0);
    }
}


