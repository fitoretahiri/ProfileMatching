using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTest
{
    public class LoginTest:TestBase
    {
        [Test]
        public void VerifyValidLogin()
        {
            //Open Login Page
            login("ft53961@ubt-uni.net", "Pa$$w0rd");

            string expectedUrl = "http://localhost:3000/jobpositions";
            string actualUrl = driver.Url;
            Console.WriteLine("Validating result");
            Assert.AreEqual(expectedUrl, actualUrl, "The page was not redirected to the expected page.");
        }

        [Test]
        public void VerifyInvalidLogin()
        {
            driver.Url = "http://localhost:3000/login";
            Console.WriteLine("Opening " + driver.Url);

            driver.FindElement(By.Id("email")).SendKeys("ft53961@ubt-uni.net");
            driver.FindElement(By.Id("password")).SendKeys("Paw0rd");
            Console.WriteLine("Enter username and password");

            driver.FindElement(By.TagName("button")).Click();
            Console.WriteLine("Click button");

            login("ft53961@ubt-uni.net", "Paword");

            string expectedMessage = "Invalid password or email!";
            string messageReturned = driver.FindElement(By.ClassName("css-1pxa9xg-MuiAlert-message")).Text;
            Console.WriteLine("Validating result");

            Assert.AreEqual(expectedMessage, messageReturned, "The result was not correct");
        }

        public void login(string email, string password)
        {

            driver.Url = "http://localhost:3000/login";
            Console.WriteLine("Opening " + driver.Url);
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("password")).SendKeys(password);
            Console.WriteLine("Enter username and password");

            driver.FindElement(By.TagName("button")).Click();
            Console.WriteLine("Click button");
        }
    }
}
