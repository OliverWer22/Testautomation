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
public class Tests
{

    private IWebDriver driver;
    public IDictionary<string, object> vars { get; private set; }
    private IJavaScriptExecutor js;

    [SetUp]
    // At startup, opens driver and norban and waits for it to load in correctly and sets window to full size.
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

    //Opens Norban between tests and waits for it to load in correctly and sets window to full size. 
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
    // Verifys that homebutton works from "Så funkar Norban" and from homepage.
    public void verifyHomeButton()
    {
        driver.FindElement(By.CssSelector(".Header-logo")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")));
        Assert.That(driver.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")).Text, Is.EqualTo("Mänskliga mäklare. Digitala möjligheter."));
        driver.FindElement(By.LinkText("Så funkar Norban")).Click();
        IWebElement firstResult2 = wait.Until(e => e.FindElement(By.CssSelector(".SellPage-introTitle")));
        Assert.That(driver.FindElement(By.CssSelector(".SellPage-introTitle")).Text, Is.EqualTo("Varför stressa igenom ditt livs största affär?"));
        driver.FindElement(By.CssSelector(".Header-logo")).Click();
        IWebElement firstResult3 = wait.Until(e => e.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")));
        Assert.That(driver.FindElement(By.CssSelector(".FrontPage-topAboveFoldTagline")).Text, Is.EqualTo("Mänskliga mäklare. Digitala möjligheter."));

    }
    [Test]
    // Verify button "Nyproduktion" links to right page. 
    public void verifyNyproduktion()
    {
        driver.FindElement(By.LinkText("Nyproduktion")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".NewConstruction-title")));
        Assert.That(driver.FindElement(By.CssSelector(".NewConstruction-title")).Text, Is.EqualTo("Nyproduktion"));
    }
    [Test]
    // Verify button "Så funkar Norban" links to right page.
    public void verifySåfunkarNorban()
    {
        driver.FindElement(By.LinkText("Så funkar Norban")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".SellPage-introTitle")));
        Assert.That(driver.FindElement(By.CssSelector(".SellPage-introTitle")).Text, Is.EqualTo("Varför stressa igenom ditt livs största affär?"));
    }
    [Test]
    // Login with active user works. 
    public void logIn()
    {
        driver.FindElement(By.CssSelector("li .AccountIcon div:nth-child(1)")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")));
        driver.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")).Click();
        IWebElement firstResult1 = wait.Until(e => e.FindElement(By.CssSelector(".AuthForms-formInput:nth-child(1)")));
        driver.FindElement(By.CssSelector(".AuthForms-formInput:nth-child(1)")).SendKeys("oliver.werthen@hotmail.com");
        driver.FindElement(By.CssSelector(".AuthForms-formInput:nth-child(2)")).SendKeys("TestTest123");
        driver.FindElement(By.CssSelector(".AuthForms-submitBtn")).Click();
        IWebElement firstResult2 = wait.Until(e => e.FindElement(By.CssSelector(".AccountEmptyState-img")));
        var elements = driver.FindElements(By.CssSelector(".AccountEmptyState-img"));
        Assert.True(elements.Count > 0);
    }
    [Test]
    //Login without passoword verify label "lösenord saknas" shows. 
    public void logInNopassword()
    {
        driver.FindElement(By.CssSelector("li .AccountIcon")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")));
        driver.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")).Click();
        IWebElement firstResul2 = wait.Until(e => e.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")));
        driver.FindElement(By.CssSelector(".AuthForms-formInput:nth-child(1)")).SendKeys("oliver.werthen@hotmail.com");
        driver.FindElement(By.CssSelector(".AuthForms-submitBtn")).Click();
        IWebElement firstResul3 = wait.Until(e => e.FindElement(By.CssSelector(".AuthForms-errorText")));
        Assert.That(driver.FindElement(By.CssSelector(".AuthForms-errorText")).Text, Is.EqualTo("Lösenord saknas"));
    }
    [Test]
    //Login without username verify label with "e-post saknas" shows. 
    public void loginNousername()
    {
        driver.FindElement(By.CssSelector("li .AccountIcon")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")));
        driver.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")).Click();
        IWebElement firstResul2 = wait.Until(e => e.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")));
        driver.FindElement(By.CssSelector(".AuthForms-formInput:nth-child(2)")).SendKeys("TestTest");
        driver.FindElement(By.CssSelector(".AuthForms-submitBtn")).Click();
        IWebElement firstResul3 = wait.Until(e => e.FindElement(By.CssSelector(".AuthForms-errorText")));
        Assert.That(driver.FindElement(By.CssSelector(".AuthForms-errorText")).Text, Is.EqualTo("E-post saknas"));
    }
    [Test]
    //Verify missing e-post and password label when logging in without text in both fields. 
    public void loginNoUserandPass()
    {
        driver.FindElement(By.CssSelector("li .AccountIcon")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")));
        driver.FindElement(By.CssSelector(".AuthPage-segmentedControlBtn:nth-child(3)")).Click();
        driver.FindElement(By.CssSelector(".AuthForms-submitBtn")).Click();
        IWebElement firstResul2 = wait.Until(e => e.FindElement(By.CssSelector(".AuthForms-errorText")));
        Assert.That(driver.FindElement(By.XPath("//div[@id=\'react-root\']/div[2]/div/div[2]/form/p")).Text, Is.EqualTo("E-post saknas"));
        Assert.That(driver.FindElement(By.XPath("//div[@id=\'react-root\']/div[2]/div/div[2]/form/p[2]")).Text, Is.EqualTo("Lösenord saknas"));
    }
     [Test]
     //Test to grab adress from latest property and search for it.
    public void getAdressAndSearch()
    {
        driver.FindElement(By.LinkText("Våra bostäder")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.XPath("//div[@id=\'react-root\']/div/div[3]/div/div/div[2]/a/div/div[3]/div[2]/div")));
        driver.FindElement(By.XPath("//div[@id=\'react-root\']/div/div[3]/div/div/div[2]/a/div/div[3]")).Click();
        string Adress = driver.FindElement(By.XPath("/html/body/div[1]/main/div/div/div[3]/div[1]/div/div[2]/a[1]/div/div[3]/div[2]/div[1]")).Text;
        driver.FindElement(By.XPath("//div[@id=\'react-root\']/div/div[2]/div/div/div[3]/div[2]")).Click();
        IWebElement firstResult1 = wait.Until(e => e.FindElement(By.CssSelector(".Prototype-searchInput")));
        driver.FindElement(By.CssSelector(".Prototype-searchInputCtrl")).SendKeys(Adress);
        driver.FindElement(By.CssSelector(".Prototype-searchInputCtrl")).Click();
        IWebElement firstResult2 = wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/main/div/div/div[2]/div/div[1]/div[3]/div[2]/div[2]/div[2]")));
        driver.FindElement(By.XPath("/html/body/div[1]/main/div/div/div[2]/div/div[1]/div[3]/div[2]/div[2]/div[2]")).Click();
        string bostad = "Vi har hittat 1 bostad.";
        IWebElement bostadtext = driver.FindElement(By.CssSelector(".Prototype-resultCount"));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(bostadtext, bostad));
        Assert.That(driver.FindElement(By.CssSelector(".Prototype-resultCount")).Text, Is.EqualTo("Vi har hittat 1 bostad."));
        Thread.Sleep(1000);
    }

    [Test]
    //Test to go through phone side meny. 
    public void phonemeny()
    {
        driver.Manage().Window.Size = new System.Drawing.Size(782, 823);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector(".Header-hamburger")));
        driver.FindElement(By.CssSelector(".Header-hamburger")).Click();
        IWebElement firstResult1 = wait.Until(e => e.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(1) > .Header-mobileMenuLinkTitle")));
        driver.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(1) > .Header-mobileMenuLinkTitle")).Click();
        IWebElement firstResult2 = wait.Until(e => e.FindElement(By.XPath("//div[@id=\'react-root\']/div/section/div/div/h1")));
        Assert.That(driver.FindElement(By.XPath("//div[@id=\'react-root\']/div/section/div/div/h1")).Text, Is.EqualTo("Varför stressa igenom ditt livs största affär?"));
        driver.FindElement(By.CssSelector(".Header-hamburger")).Click();
        IWebElement firstResult3 = wait.Until(e => e.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(1) > .Header-mobileMenuLinkTitle")));
        driver.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(2) > .Header-mobileMenuLinkTitle")).Click();
        IWebElement firstResult4 = wait.Until(e => e.FindElement(By.XPath("//div[@id=\'react-root\']/div/div/div/h1")));
        Assert.That(driver.FindElement(By.XPath("//div[@id=\'react-root\']/div/div/div/h1")).Text, Is.EqualTo("Nyproduktion"));
        driver.FindElement(By.CssSelector(".Header-hamburger")).Click();
        IWebElement firstResult5 = wait.Until(e => e.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(1) > .Header-mobileMenuLinkTitle")));
        driver.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(3) > .Header-mobileMenuLinkTitle")).Click();
        IWebElement firstResult6 = wait.Until(e => e.FindElement(By.XPath("(//button[@type=\'button\'])[2]")));
        var elements = driver.FindElements(By.XPath("(//button[@type=\'button\'])[2]"));
        Assert.True(elements.Count > 0);
        driver.FindElement(By.CssSelector(".Header-hamburger")).Click();
        IWebElement firstResult7 = wait.Until(e => e.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(1) > .Header-mobileMenuLinkTitle")));
        driver.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(4) > .Header-mobileMenuLinkTitle")).Click();
        IWebElement firstResult8 = wait.Until(e => e.FindElement(By.XPath("//div[@id=\'react-root\']/div/section/div/div/h1")));
        Assert.That(driver.FindElement(By.XPath("//div[@id=\'react-root\']/div/section/div/div/h1")).Text, Is.EqualTo("Vi vill skapa världens bästa mäklartjänst."));
        driver.FindElement(By.CssSelector(".Header-hamburger")).Click();
        IWebElement firstResult9 = wait.Until(e => e.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(1) > .Header-mobileMenuLinkTitle")));
        driver.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(5) > .Header-mobileMenuLinkTitle")).Click();
        IWebElement firstResult10 = wait.Until(e => e.FindElement(By.CssSelector(".BlogOverviewPage-body")));
        var elements1 = driver.FindElements(By.CssSelector(".BlogOverviewPage-body"));
        Assert.True(elements1.Count > 0);
        driver.FindElement(By.CssSelector(".Header-hamburger")).Click();
        IWebElement firstResult11 = wait.Until(e => e.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(1) > .Header-mobileMenuLinkTitle")));
        driver.FindElement(By.CssSelector(".Header-mobileMenuLink:nth-child(6) > .Header-mobileMenuLinkTitle")).Click();
        IWebElement firstResult12 = wait.Until(e => e.FindElement(By.XPath("//div[@id=\'react-root\']/div/section/div/div/h1")));
        var elements2 = driver.FindElement(By.CssSelector(".FAQRow-title"));
        Assert.True(elements1.Count > 0);

    }
    
}


        


