using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping.ReportsWithObstacles;

[TestFixture]
public class NonFinancialWithoutWebSite:Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NonFinancialReportsTestWithoutWebSiteInfo()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }

        var company = new
        {
            name = "GALATASARAY SPORTİF SINAİ VE TİCARİ YATIRIMLAR ANONİM ŞİRKETİ",
            website = "https://www.galatasaray.org/anasayfa"
        };

        IWebElement nonFinancialButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-18ldgz6")));
        ScrollAndClick(nonFinancialButton);
        Thread.Sleep(500);

        Thread.Sleep(1500);
        IWebElement companyName = wait.Until(d => d.FindElement(By.CssSelector(".css-1nuss9t")));
        companyName.SendKeys(company.name);
        Thread.Sleep(1000);

        IWebElement companyLabel = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//td[normalize-space(text())='{company.name}']")));
        ScrollAndClick(companyLabel);

        Thread.Sleep(1500);

        IWebElement updateWebSite = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-gld5zg")));
        ScrollAndClick(updateWebSite);
        Thread.Sleep(500);

        var vknNumberElements = wait.Until(d => d.FindElements(By.CssSelector(".css-yzm7vx")));
        IWebElement website = vknNumberElements[1];
        website.SendKeys(company.website);

        IWebElement updateCompany = wait.Until(d => d.FindElement(By.CssSelector(".css-yxydmy")));
        updateCompany.Click();

        Thread.Sleep(1000);
        IWebElement nextButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1euz8a8")));
        ScrollAndClick(nextButton);
        Thread.Sleep(500);

        Assert.Pass();
    }
}