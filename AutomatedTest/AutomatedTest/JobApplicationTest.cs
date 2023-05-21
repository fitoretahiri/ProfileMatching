using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTest
{
    public class JobApplicationTest:TestBase
    {
        [Test]
        public void ApplyToJobPosition()
        {
            //To apply, you need to be logged in first
            login("dua@gmail.com", "Pa$$w0rd1");

            var elements = driver.FindElements(By.Id("butoni"));
            elements[2].Click();

            string expectedUrl = "http://localhost:3000/jobpositions";
            string actualUrl = driver.Url;
            Console.WriteLine("Validating result");
            Assert.AreEqual(expectedUrl, actualUrl, "The page was not redirected to the expected page.");
        }

        [Test]
        public void CheckApplications()
        {
            //login as a recruiter or administrator
            login("r@example.com", "Pa$$w0rd");

            driver.FindElement(By.ClassName("css-av538e-MuiButtonBase-root-MuiIconButton-root")).Click();
            Console.WriteLine("Clicked Icon");

            driver.FindElement(By.Id("applications")).Click();
            Console.WriteLine("Clicked Applications");

            string expectedURL = "http://localhost:3000/applications";
            string actualURL = driver.Url;

            Console.WriteLine("Validating result. Actual URL: " +actualURL);
            Assert.AreEqual(expectedURL, actualURL, "The page was not redirected to the expected page.");
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
