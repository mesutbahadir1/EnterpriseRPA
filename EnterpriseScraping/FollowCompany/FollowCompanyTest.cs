using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping.FollowCompany;

[TestFixture]
public class FollowCompanyTest:Tests
{
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FollowCompaniesTest()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }
        
        IWebElement followCompanyButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div[contains(@class, 'css-u6kvue')])[3]")));
        ScrollAndClick(followCompanyButton);        
        Thread.Sleep(1000);
      
        IWebElement startFollow =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div[contains(@class, 'css-1tfmnlc')])[1]")));
        ScrollAndClick(startFollow);
        
        IWebElement companyName = wait.Until(d => d.FindElement(By.CssSelector(".css-1nuss9t")));    
        companyName.SendKeys("CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ");
        
       
        IWebElement chosenCompany =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-10ygcul")));
        ScrollAndClick(chosenCompany);   
            
            
        IWebElement startFollowCompany =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1judmc9")));
        ScrollAndClick(startFollowCompany);   
        Thread.Sleep(500);
        
        IWebElement confirmButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1gt42h3")));
        ScrollAndClick(confirmButton);       
        Assert.Pass();
    }
    
    [Test]
    public void UnfollowCompaniesTest()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }
        IWebElement followCompanyButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div[contains(@class, 'css-u6kvue')])[3]")));
        ScrollAndClick(followCompanyButton);   
        
        Thread.Sleep(1000);
      
        IWebElement startFollow =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div[contains(@class, 'css-1tfmnlc')])[1]")));
        ScrollAndClick(startFollow);   
        
        
        var elements = driver.FindElements(By.CssSelector(".css-118eqn5"));
        IWebElement secondElement = elements[1]; 
        ScrollAndClick(secondElement);   
        
        
        IWebElement companyName = wait.Until(d => d.FindElement(By.CssSelector(".css-1nuss9t")));    
        companyName.SendKeys("CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ");
        
       
        IWebElement chosenCompany =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-10ygcul")));
        ScrollAndClick(chosenCompany);       
            
            
        IWebElement unfollowCompany =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1judmc9")));
        ScrollAndClick(unfollowCompany);        
        
        Thread.Sleep(500);
        IWebElement confirmButton =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-1gt42h3")));
        ScrollAndClick(confirmButton);         
        Assert.Pass();
    }
    
}