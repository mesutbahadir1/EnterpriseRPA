using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping.Settings;

[TestFixture]
public class SystemSettings:Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SystemSettingsTest()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }

        void clearInput(ReadOnlyCollection<IWebElement>? inputs)
        {
            foreach (var input in inputs)
            {
                string existingValue = input.GetAttribute("value");
                foreach (char _ in existingValue)
                {
                    input.SendKeys(Keys.Backspace); 
                }
            
            }
        }

        IWebElement settingsButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div[contains(@class, 'css-u6kvue')])[4]")));
        ScrollAndClick(settingsButton);
        Thread.Sleep(1000);

        IWebElement generalSystemSettingsButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("(//div[contains(@class, 'css-1tfmnlc')])[2]")));
        ScrollAndClick(generalSystemSettingsButton);
        Thread.Sleep(500);
        
        var inputs = wait.Until(d => d.FindElements(By.CssSelector(".css-yzm7vx")));
        clearInput(inputs);

        var documentObject = new
        {
            DocumentUploadDay = "1",
            FindeksUploadDay = "360",
            MizanUploadYear = "1",
            TaxDebtUploadDay = "1",
        };
        string[] inputValues = [documentObject.DocumentUploadDay,documentObject.FindeksUploadDay,
            documentObject.MizanUploadYear,documentObject.TaxDebtUploadDay];
        for (int i = 0; i < inputValues.Length; i++)
        {
            inputs[i].SendKeys(inputValues[i]);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].dispatchEvent(new Event('input'));", inputs[i]);
        }
        Thread.Sleep(500);
        var dateInputs = wait.Until(d => d.FindElements(By.CssSelector(".css-1nuss9t")));
        clearInput(dateInputs);
        Thread.Sleep(1000);

        var dateObject = new
        {
            KVBUploadDay = "30.05",
            GVBPeriodFirst = "05.05",
            GVBPeriodSecond = "01.08",
            GVBPeriodThird = "01.11"
        };
        string[] dateInputValues = [dateObject.KVBUploadDay,dateObject.GVBPeriodFirst,
            dateObject.GVBPeriodSecond,dateObject.GVBPeriodThird];
        for (int i = 0; i < dateInputValues.Length; i++)
        {
            dateInputs[i].SendKeys(dateInputValues[i]);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].dispatchEvent(new Event('input'));", dateInputs[i]);
        }
        
        IWebElement saveChangesButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1mxe2co")));
        ScrollAndClick(saveChangesButton);   
        
        Thread.Sleep(1000);
        
       IWebElement generalSettingsTabButton = 
           wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[contains(@class, 'MuiTab-root')])[2]"))); 
       
       ScrollAndClick(generalSettingsTabButton);
       Thread.Sleep(1000);
       
       var emailInputs = wait.Until(d => d.FindElements(By.CssSelector(".css-yzm7vx")));
       clearInput(emailInputs);
       var passwordInputs = wait.Until(d => d.FindElements(By.CssSelector(".css-1nuss9t")));
       clearInput(passwordInputs);

       var emailObject = new
       {
           ClientSecret = "M5u8Q~dQUhUtj4S-DnJZ8UVivYZaGbPn.Rvl7bIO",
           ClientID = "b22349d8-8147-4c28-95bd-5c02222744b1",
           TenentID = "cd7d5b21-276f-40db-a4ce-ec8055fd0820",
           Inbox = "Inbox",
           Archive = "Archive",
           MailUser = "belge.test@risk360.com.tr",
           MailTime = "120"
       };
       var stmpObject = new
       {
           UserName = "smtp_user@example.com",
           Password = "smtpPassword123",
           Host = "smtp.example.com",
           Port = "587"
       };
       string[] emailInputValues = [emailObject.ClientSecret,emailObject.ClientID,
           emailObject.TenentID,emailObject.Inbox,emailObject.Archive,emailObject.MailUser,emailObject.MailTime,
       stmpObject.UserName,stmpObject.Host,stmpObject.Port];
       
       for (int i = 0; i < emailInputValues.Length; i++)
       {
           emailInputs[i].SendKeys(emailInputValues[i]);
           ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].dispatchEvent(new Event('input'));", emailInputs[i]);
       }
       passwordInputs[0].SendKeys(stmpObject.Password);
       ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].dispatchEvent(new Event('input'));",  passwordInputs[0]);
       
       
       Thread.Sleep(4000);

       IWebElement saveGeneralSystemChangesButton =
           wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1mxe2co")));
       ScrollAndClick(saveGeneralSystemChangesButton);       
    }
}