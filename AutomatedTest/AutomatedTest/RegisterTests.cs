using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTest
{
    public class RegisterTests:TestBase
    {
        [Test]
        public void RegisterWithValidData()
        {
            //Open Login Page
            driver.Url = "http://localhost:3000/login";
            Console.WriteLine("Opening " + driver.Url);

            //navigate to register page
            driver.FindElement(By.Id("register")).Click();
            Console.WriteLine("Navigate to ");

            //Fill form
            driver.FindElement(By.Id("firstName")).SendKeys("Dylan");
            driver.FindElement(By.Id("lastName")).SendKeys("O'Brien");
            driver.FindElement(By.Id("username")).SendKeys("dylan1");
            driver.FindElement(By.Id("email")).SendKeys("d@gmail.com");
            driver.FindElement(By.Id("password")).SendKeys("Pa$$w0rd1");
            driver.FindElement(By.Id("skills")).SendKeys(".net, java,c#,javascript");
            Console.WriteLine("Filled form");

            driver.FindElement(By.TagName("button")).Click();
            Console.WriteLine("Click button");

            string expectedUrl = "http://localhost:3000/login#";
            string actualUrl = driver.Url;
            Console.WriteLine("Validating result");
            Assert.AreEqual(expectedUrl, actualUrl, "The page was not redirected to the expected page.");
        }

        [Test]
        public void RegisterWithInvalidData() 
        {
            //Open Login Page
            driver.Url = "http://localhost:3000/login";
            Console.WriteLine("Opening " + driver.Url);

            //navigate to register page
            driver.FindElement(By.Id("register")).Click();
            Console.WriteLine("Navigate to register form");

            //Fill form
            driver.FindElement(By.Id("firstName")).SendKeys("test");
            driver.FindElement(By.Id("lastName")).SendKeys("test");
            driver.FindElement(By.Id("username")).SendKeys("dylan1");
            driver.FindElement(By.Id("email")).SendKeys("d@gmail.com");
            driver.FindElement(By.Id("password")).SendKeys("Pa$$w0rd1");
            driver.FindElement(By.Id("skills")).SendKeys(".net, java,c#,javascript");
            Console.WriteLine("Filled form");

            driver.FindElement(By.TagName("button")).Click();
            Console.WriteLine("Click button");

            var responseStatusCode = driver.FindElement(By.TagName("h1")).Text; // Assuming the error message is displayed within an 'h1' element
            Assert.AreEqual("400", responseStatusCode, "The registration request did not return the expected 400 Bad Request response.");
        }
    }
}
