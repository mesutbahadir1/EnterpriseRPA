using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping;

[TestFixture]
public class Report360Test : Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Report360WithoutAddingFileTest()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        Thread.Sleep(2000);

        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }

        IWebElement report360Button = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-vc887x")));
        ScrollAndClick(report360Button);
        Thread.Sleep(1500);
        
        IWebElement companyName = wait.Until(d => d.FindElement(By.CssSelector(".css-1nuss9t")));    
        companyName.SendKeys("CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ");
        Thread.Sleep(1000);
        IWebElement companyLabel = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//td[normalize-space(text())='CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ']")));
        ScrollAndClick(companyLabel);
        Thread.Sleep(1000);

        IWebElement nextButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1euz8a8")));
        ScrollAndClick(nextButton);
        Thread.Sleep(1500);

        IWebElement getReport = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-zvognn")));
        ScrollAndClick(getReport);
        Thread.Sleep(1500);

        IWebElement nextReport = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1btjc2a")));
        ScrollAndClick(nextReport);
        Thread.Sleep(500);

        Assert.Pass();
    }
}