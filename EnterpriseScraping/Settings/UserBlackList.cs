using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping.Settings;

[TestFixture]
public class UserBlackList:Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void UserBlackListTest()
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

        IWebElement userBlackListButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("(//div[contains(@class, 'css-1tfmnlc')])[8]")));
        ScrollAndClick(userBlackListButton);
        Thread.Sleep(500);


        IWebElement addUser =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-nli90d")));
        ScrollAndClick(addUser);
        Thread.Sleep(1000);

        var userInfo = new
        {
            Name = "Test Users Name",
            Surname = "Test Users Surname",
            TCKN = "91003669790"
        };

        var inputs = wait.Until(d => d.FindElements(By.CssSelector(".css-yzm7vx")));
        inputs[0].SendKeys(userInfo.Name);
        inputs[1].SendKeys(userInfo.Surname);
        inputs[2].SendKeys(userInfo.TCKN);
        
        
        IWebElement addToUserBlackList =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1ahihjf")));
        ScrollAndClick(addToUserBlackList);   
        
        IWebElement searchUser = wait.Until(d => d.FindElement(By.CssSelector(".css-1nuss9t")));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display = 'block';", searchUser);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", searchUser);
        Thread.Sleep(500);

        searchUser.SendKeys(userInfo.TCKN);

        Thread.Sleep(1000);

        IWebElement deleteButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[name()='svg' and @data-testid='DeleteIcon']")));
        deleteButton.Click();
        Thread.Sleep(1000);
    }
}