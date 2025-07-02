using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace EnterpriseScraping;

public class Tests
{
    private static Lazy<IWebDriver> lazyDriver = new(() => new ChromeDriver());
    protected IWebDriver driver => lazyDriver.Value;
    private IConfiguration _configuration { get; set; }

    [SetUp]
    public void Setup()
    {
        string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        string configPath = Path.Combine(projectPath, "appsettings.json");
        
        var builder = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _configuration = builder.Build();

        driver.Manage().Window.Maximize();
    }
    
    [Test]
    public void Login()
    {
        string loginUrl = _configuration["CompanyInfo:CompanyDomain"];
        
        driver.Navigate().GoToUrl(loginUrl);
        
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        IWebElement email = wait.Until(d => d.FindElement(By.Name("email")));
        email.SendKeys("#sa@rate360.com.tr");
        email.SendKeys(Keys.Return);

        IWebElement password = wait.Until(d => d.FindElement(By.Name("password")));
        password.SendKeys("Taseron360");
        password.SendKeys(Keys.Return);

        wait.Until(d => d.Url.Contains("/dashboard"));
    }
}