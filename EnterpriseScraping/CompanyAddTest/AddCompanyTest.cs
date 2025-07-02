using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EnterpriseScraping;

[TestFixture]
public class AddCompanyTest:Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CompanyAdd()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        
        IWebElement companyButton = wait.Until(d => d.FindElement(By.CssSelector(".css-u6kvue")));
        companyButton.Click();
        
        Thread.Sleep(300); 

        IWebElement addNewCompany = wait.Until(d => d.FindElement(By.CssSelector(".css-1tfmnlc")));
        addNewCompany.Click();
            
        
        IWebElement companyName = wait.Until(d => d.FindElement(By.CssSelector(".css-yzm7vx")));    
        companyName.SendKeys("CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ");
       
        Thread.Sleep(1000); 
        IWebElement findCompany = wait.Until(d => d.FindElement(By.CssSelector(".css-1ctcywv")));
        findCompany.Click();
        
        
        IWebElement companyLabel = wait.Until(d => d.FindElement(By.XPath("//td[div[normalize-space(text())='CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ']]")));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", companyLabel);


        Thread.Sleep(1000); 
        IWebElement saveButton = wait.Until(d => d.FindElement(By.CssSelector(".css-1euz8a8")));
        saveButton.Click();
        
        var companyTypeDropdownElements = wait.Until(d => d.FindElements(By.CssSelector(".css-1d3ohrd")));
        

        var options = wait.Until(d => d.FindElements(By.CssSelector(".MuiAutocomplete-option")));
        
        
        var vknNumberElements = wait.Until(d => d.FindElements(By.CssSelector(".css-yzm7vx")));
        IWebElement website = vknNumberElements[1];
        website.SendKeys("https://canhafriyat.com.tr/");
        
        IWebElement vknNumber = vknNumberElements[4];
        vknNumber.SendKeys("1980013406");
        
        companyTypeDropdownElements[1].Click();
        options = wait.Until(d => d.FindElements(By.CssSelector(".MuiAutocomplete-option")));
        options[1].Click();
        
        IWebElement saveCompany = wait.Until(d => d.FindElement(By.CssSelector(".css-yxydmy")));
        saveCompany.Click();
        //wait.Until(d => d.FindElement(By.XPath("//td[normalize-space(text())='CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ']")));
    }
    
}