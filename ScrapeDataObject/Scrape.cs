using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Runtime.CompilerServices;
//using OpenQA.Selenium.Support.ui.Select;

namespace ScrapeDataObject
{
    public class Scrape
    {
        IWebDriver driver = null;
        public CommonClass.Result loadWeb()
        {
            var result = new CommonClass.Result();
            try
            {
                var DeviceDriver = ChromeDriverService.CreateDefaultService();
                DeviceDriver.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-infobars");
                driver = new ChromeDriver(DeviceDriver, options);
                driver.Manage().Window.Minimize();
                OpenUrl("https://workbench.developerforce.com/login.php");
                WebElement terms = (WebElement)driver.FindElement(By.Id("termsAccepted"));
                terms.Click();
                var loginWithSF = (WebElement)driver.FindElement(By.Id("loginBtn"));
                loginWithSF.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                result.Status = true;
                result.Message = "Web Loaded";
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return result;
        }
        public CommonClass.Result login(string data)
        {
            var result = new CommonClass.Result();
            try
            {
                var credentials = data.StringToSingleCls<CommonClass.CustomConfig>();

                WebElement email = (WebElement)driver.FindElement(By.Id("username"));
                WebElement password = (WebElement)driver.FindElement(By.Id("password"));
                email.SendKeys(credentials.userName);
                password.SendKeys(credentials.password);
                
                WebElement sFbtn = (WebElement)driver.FindElement(By.Id("Login"));
                sFbtn.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                result.Status = true;
                result.Message = "Logged In Successfully";
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return result;
        }
        public CommonClass.Result ScrapeData()
        {
            var result = new CommonClass.Result();
            try
            {
                var jsonObj = new salesforceJsonConfig();
                jsonObj.sfObjectList = new List<salesforceJsonConfig.sfObjects>();
                jsonObj.action = "extract_all";

                OpenUrl("https://workbench.developerforce.com/query.php");
                SelectElement se = new SelectElement(driver.FindElement(By.Id("QB_object_sel")));

                var ObjectOptions = (WebElement)driver.FindElement(By.Id("QB_object_sel"));
                var allObjects = ObjectOptions.FindElements(By.TagName("option"));
                var allObjects2 = allObjects.ToList();
                allObjects2.RemoveAt(0);
                for (int i = 0; i <= allObjects2.Count; i++)
                {
                    //var se2 = new SelectElement(driver.FindElement(By.Id("QB_object_sel")));
                    se = new SelectElement(driver.FindElement(By.Id("QB_object_sel")));
                    ObjectOptions = (WebElement)driver.FindElement(By.Id("QB_object_sel"));
                    allObjects = ObjectOptions.FindElements(By.TagName("option"));
                    allObjects2 = allObjects.ToList();
                    string currentObject = allObjects2[i].Text.ToString();

                    se.SelectByText(currentObject);
                    allObjects2.RemoveAt(0);
                    var fieldsOptions = (WebElement)driver.FindElement(By.Id("QB_field_sel"));
                    var allFields = fieldsOptions.FindElements(By.TagName("option")).ToList();
                    var countColumn = allFields.FirstOrDefault(x => x.Text == "count()");
                    if (countColumn != null)
                    {
                        allFields.Remove(countColumn);
                    }
                    if (allFields != null && allFields.Count > 1)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine($"Fields For Object : {currentObject}");
                        jsonObj.sfObjectList.Add(new salesforceJsonConfig.sfObjects() { objectName = currentObject, query = $"Select {string.Join(",", allFields.Select(x => x.Text).ToList())} from {currentObject}" });
                        //foreach (var field in allFields)
                        //{
                        //    Console.WriteLine(field.Text);
                        //}
                        Console.WriteLine("-----------------------------");
                    }
                }
                result.Status = true;
                result.Message = "Data Scrapped Successfully";
                result = CreateJsonFile(jsonObj);
//                JsonFileUtils.SimpleWrite(jsonObj, "sfJsonConfig.json");

            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return result;

        }
        public CommonClass.Result CreateJsonFile(salesforceJsonConfig data)
        {
            var result = new CommonClass.Result();
            try
            {
                JsonFileUtils.SimpleWrite(data, "sfJsonConfig.json");
                result.Status = true;
                result.Message = "Json Created";
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return result;
        }
        public CommonClass.Result OpenUrl(string Path)
        {
            var result = new CommonClass.Result();
            try
            {
                driver.Navigate().GoToUrl(Path);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                result.Status = true;
                result.Message = "Navigation Success";
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return result;
        }

    }
}
