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
    }
    /*public void Initialize()
    {
        driver.Navigate().GoToUrl("https://norban.se/");
        driver.Manage().Window.Size = new System.Drawing.Size(1552, 832);
    }
    */
        [TearDown]
    protected void TearDown()
    {
        driver.Quit();
    }
    [Test]
    public void verifyHomeButton()
    {
        driver.Navigate().GoToUrl("https://norban.se/");
        driver.Manage().Window.Size = new System.Drawing.Size(1552, 832);
        driver.FindElement(By.CssSelector(".Header-logo")).Click();
        Assert.That(driver.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")).Text, Is.EqualTo("M�nskliga m�klare. Digitala m�jligheter."));
        driver.FindElement(By.LinkText("S� funkar Norban")).Click();
        Assert.That(driver.FindElement(By.XPath("/html/body/div[1]/main/div/div/section[1]/div/div[1]/h1")).Text, Is.EqualTo("Varf�r stressa igenom ditt livs st�rsta aff�r?"));
        driver.FindElement(By.CssSelector(".Header-logoPath")).Click();
        Assert.That(driver.FindElement(By.XPath("//*[@id='react - root']/div/section[1]/div/div[1]/p[1]/text()[1]")).Text, Is.EqualTo("M�nskliga m�klare. Digitala m�jligheter."));
    }
    [Test]
    public void verifyNyproduktion()
    {
        driver.Navigate().GoToUrl("https://norban.se/");
        driver.Manage().Window.Size = new System.Drawing.Size(1536, 816);
        driver.FindElement(By.LinkText("Nyproduktion")).Click();
        Assert.That(driver.FindElement(By.XPath("/html/body/div[1]/main/div/div/div/div[1]/h1")).Text, Is.EqualTo("Nyproduktion"));
    }
    [Test]
    public void verifySfunkarNorban()
    {
        driver.Navigate().GoToUrl("https://norban.se/");
        driver.Manage().Window.Size = new System.Drawing.Size(1536, 816);
        driver.FindElement(By.LinkText("S� funkar Norban")).Click();
        Assert.That(driver.FindElement(By.CssSelector(".SellPage-introTitle")).Text, Is.EqualTo("Varf�r stressa igenom ditt livs st�rsta aff�r?"));
    }
}
