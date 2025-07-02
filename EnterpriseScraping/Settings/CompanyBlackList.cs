using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping.Settings;

[TestFixture]
public class CompanyBlackList:Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CompanyBlackListTest()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }

        IWebElement settingsButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div[contains(@class, 'css-u6kvue')])[4]")));
        ScrollAndClick(settingsButton);
        Thread.Sleep(1000);

        IWebElement companyBlackListButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("(//div[contains(@class, 'css-1tfmnlc')])[9]")));
        ScrollAndClick(companyBlackListButton);
        Thread.Sleep(500);

        
        IWebElement addCompany =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-nli90d")));
        ScrollAndClick(addCompany);
        Thread.Sleep(1000);
        
        var companyInfo = new
        {
            Name = "Test Company Name",
            VKN = "3332545282",
            TaxOffice = "Test Company Tax Office",
            Address = "Address"

        };


        var inputs = wait.Until(d => d.FindElements(By.CssSelector(".css-yzm7vx")));
        inputs[0].SendKeys(companyInfo.Name);
        inputs[1].SendKeys(companyInfo.VKN);
        inputs[2].SendKeys(companyInfo.TaxOffice);
        inputs[3].SendKeys(companyInfo.Address);
        
        
        IWebElement addToCompanyBlackList =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1ahihjf")));
        ScrollAndClick(addToCompanyBlackList);   
        
        IWebElement searchCompany = wait.Until(d => d.FindElement(By.CssSelector(".css-1nuss9t")));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display = 'block';", searchCompany);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", searchCompany);
        Thread.Sleep(500);

        searchCompany.SendKeys(companyInfo.VKN);

        Thread.Sleep(1000);

        IWebElement deleteButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[name()='svg' and @data-testid='DeleteIcon']")));
        deleteButton.Click();
        Thread.Sleep(1000);
        
    }
}