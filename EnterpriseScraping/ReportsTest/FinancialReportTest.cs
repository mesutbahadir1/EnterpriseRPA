using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;

namespace EnterpriseScraping;

[TestFixture]
public class FinancialReportTest : AddCompanyTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FinancialWithoutAddingFileTest()
    {
        Login();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        Thread.Sleep(2000);
        string companyTitle = "CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ";
    
        try
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".MuiBackdrop-root")));
        }
        catch (WebDriverTimeoutException)
        {
        }

        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        void ScrollAndClick(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'}); arguments[0].click();", element);
        }

        IWebElement financialButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-zvognn")));
        ScrollAndClick(financialButton);
        Thread.Sleep(1500);
        
        IWebElement companyName = wait.Until(d => d.FindElement(By.CssSelector(".css-1nuss9t")));    
        companyName.SendKeys(companyTitle);
        Thread.Sleep(1000);
        
        IWebElement companyLabel = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//td[normalize-space(text())='{companyTitle}']")));
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
    }



    [Test]
    public void FinancialWithAddingFileTest()
    {
        CompanyAdd();
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        
        Thread.Sleep(3000);
        IWebElement companyNameInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".css-1nuss9t")));
        companyNameInput.SendKeys("CAN HAFRİYAT NAKLİYAT İNŞAAT TURİZM TİCARET LİMİTED ŞİRKETİ");
        Thread.Sleep(2000);
        IWebElement chosenCompany = wait.Until(d=>d.FindElement(By.CssSelector(".css-10ygcul")));
        chosenCompany.Click();
        Thread.Sleep(1000);
        
        wait.Until(d => d.Url.Contains("/company-details/"));
        string companyId = new Uri(driver.Url).Segments.Last().TrimEnd('/');

        IWebElement tabButton = wait.Until(d => d.FindElement(By.CssSelector("[data-cypress='companyTab-4']")));
        tabButton.Click();
        Thread.Sleep(1000);
        string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

        string[] filePaths = new string[]
        {
            Path.Combine(projectPath, "reports", "KVB_202411141020597593_1980013406.pdf"),
            Path.Combine(projectPath, "reports", "KVB_202411141020528339_1980013406.pdf"),
            Path.Combine(projectPath, "reports", "KVB_202411141020464876_1980013406.pdf"),
            Path.Combine(projectPath, "reports", "CAN HAFRIYAT NAKLIYAT INSAAT T_Risk-Raporu_NaN_2c36bd7e-b9ca-4051-a5d7-08dd047c8961.pdf"),
            Path.Combine(projectPath, "reports", "CAN HAFRIYAT NAKLIYAT INSAAT T_Cek-Raporu_NaN_52746fef-4157-4cb0-de12-08dd047c8efe.pdf")
        };

        string[] documentTypes = new string[]
        {
            "Kurumlar Vergi Beyannamesi",
            "Kurumlar Vergi Beyannamesi",
            "Kurumlar Vergi Beyannamesi",
            "Findeks Risk Raporu",
            "Findeks Çek Raporu"
        };

        for (int i = 0; i < filePaths.Length; i++)
        {
            try
            {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".MuiDialog-container")));
            }
            catch (WebDriverTimeoutException)
            {
                try
                {
                    IWebElement closeButton = driver.FindElement(By.CssSelector("[data-cypress='close-dialog']"));
                    closeButton.Click();
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".MuiDialog-container")));
                }
                catch (Exception)
                {
                    IJavaScriptExecutor jsO = (IJavaScriptExecutor)driver;
                    jsO.ExecuteScript("document.querySelector('.MuiBackdrop-root').click();");
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".MuiDialog-container")));
                }
            }

            IWebElement addReport =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-cypress='add-document']")));
            js.ExecuteScript("arguments[0].click();", addReport);

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".MuiDialog-container")));
            Thread.Sleep(1000);

            IWebElement fileInput = wait.Until(d => d.FindElement(By.CssSelector("input[type='file']")));
            js.ExecuteScript("arguments[0].style.display = 'block';", fileInput);
            Thread.Sleep(1000);
            fileInput.SendKeys(filePaths[i]);
            js.ExecuteScript("arguments[0].style.display = 'none';", fileInput);

            IWebElement dropdown =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div[role='combobox']")));
            dropdown.Click();
            Thread.Sleep(1000);

            IWebElement option = wait.Until(d =>
                d.FindElement(
                    By.XPath($"//li[contains(@class, 'MuiMenuItem') and contains(text(), '{documentTypes[i]}')]")));
            option.Click();
            Thread.Sleep(1000);

            IWebElement saveReport =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-cypress='save-document']")));
            saveReport.Click();

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".MuiDialog-container")));
            Thread.Sleep(2000);
        }

        IWebElement goDashboard =
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div[contains(@class, 'css-u6kvue')])[1]")));
        goDashboard.Click();

        FinancialWithoutAddingFileTest();
        Thread.Sleep(20000);
        DeleteCompany(companyId);
    }

    private void DeleteCompany(string companyId)
    {
        try
        {
            using (HttpClient client=new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoibWVzdXRiYWhhZGlyIiwianRpIjoiYzBkZWY2MzUtOWNlZS00YWU1LTliZTktM2YyNWEwYzg1OTM4IiwiZW1haWwiOiJtZXN1dC5iYWhhZGlyQGVjb250ZWNoLmNvbS50ciIsInBob25lX251bWJlciI6IiIsInN1YiI6IjllMTUyMmI5LWEwNTMtNDBlOS1hYzYwLTMyNDgyMzhhZjBjMCIsInJvbGUiOiJTdXBlckFkbWluIiwiZXhwIjoxNzQzODM1NzU3LCJpc3MiOiJodHRwczovL2VudGVycHJpc2VhcGkucmF0ZTM2MC5jb20udHIiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDozMDAwIn0.4NorhVB4b3PvpjhFlzrMx9cm1aihOnQST1Ou0FzHdao");
                string url = $"https://enterpriseapi.rate360.com.tr/api/Company/DeleteCompany/{companyId}";
                HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("Company successfully deleted");
                }
                else
                {
                    string errorDetails = responseMessage.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Error deleting company: " + errorDetails);
                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}