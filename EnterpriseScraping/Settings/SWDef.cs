using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping.Settings;

[TestFixture]
public class SWDef:Tests
{
     [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SWDefTest()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }

        IWebElement settingsButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("(//div[contains(@class, 'css-u6kvue')])[4]")));
        ScrollAndClick(settingsButton);
        Thread.Sleep(1000);

        IWebElement sWDefButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("(//div[contains(@class, 'css-1tfmnlc')])[7]")));
        ScrollAndClick(sWDefButton);
        Thread.Sleep(1000);

        IWebElement selectBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@role='combobox']")));
        selectBox.Click();

        Thread.Sleep(1000);

        IWebElement optionToSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[contains(text(),'3')]")));
        optionToSelect.Click();

        Thread.Sleep(500);
        
        var activityVolumeObject = new
        {
            val3_1 = "90",
            val3_2 = "5",
            val3_3 = "5",
            val2_1 = "70",
            val2_2 = "30",
            val1_1="100"
        };
        
        var assetVolumeObject = new
        {
            val3_1 = "90",
            val3_2 = "5",
            val3_3 = "5",
            val2_1 = "70",
            val2_2 = "30",
            val1_1="100"
        };
        
        var assetVolumeElements = wait.Until(d => d.FindElements(By.CssSelector(".css-yzm7vx")));
      
        string[] activityValues = { activityVolumeObject.val3_1, activityVolumeObject.val3_2, activityVolumeObject.val3_3, 
            activityVolumeObject.val2_1, activityVolumeObject.val2_2, activityVolumeObject.val1_1 };

        string[] assetValues = { assetVolumeObject.val3_1, assetVolumeObject.val3_2, assetVolumeObject.val3_3, 
            assetVolumeObject.val2_1, assetVolumeObject.val2_2, assetVolumeObject.val1_1 };

        int index = 0;
        for (int i = 0; i < assetValues.Length; i++)
        {
            assetVolumeElements[index].SendKeys(activityValues[i]);
            assetVolumeElements[index + 1].SendKeys(assetValues[i]);
            index += 2;
        }
        Thread.Sleep(500);
        IWebElement saveButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1o014pr")));
        ScrollAndClick(saveButton);   
    }
}