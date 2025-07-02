using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping;

[TestFixture]
public class NonFinancialReportTest:Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NonFinancialReportsTest()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }

        IWebElement nonFinancialButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-18ldgz6")));
        ScrollAndClick(nonFinancialButton);
        Thread.Sleep(500);

        Thread.Sleep(1500);
        IWebElement companyName = wait.Until(d => d.FindElement(By.CssSelector(".css-1nuss9t")));    
        companyName.SendKeys("CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ");
        Thread.Sleep(1000);
        
        IWebElement companyLabel = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//td[normalize-space(text())='CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ']")));
        ScrollAndClick(companyLabel);
        Thread.Sleep(500);

        Thread.Sleep(1000);

        IWebElement nextButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1euz8a8")));
        ScrollAndClick(nextButton);
        Thread.Sleep(500);
        
        Assert.Pass();
    }
}