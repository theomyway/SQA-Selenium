using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace ConsoleApplication49                         //TC#01   ----> Login with valid credentials
{                                                      //TC#02   ----> Login with Invalid credentials
                                                       //TC#03   ----> Verify login field validation errors
                                                       // Muhammad Umar Khan 10619
    public class LoginTestCases
    {
        public static bool TC_1_LoginWithValidCredentials()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://adactinhotelapp.com/");
            IWebElement usernameField = driver.FindElement(By.Id("username"));
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            usernameField.SendKeys("Arsalan52288");
            passwordField.SendKeys("P@ssw0rd");
            Assert.AreEqual("Arsalan52288", usernameField.GetAttribute("value"), "Wrong Username");
            Assert.AreEqual("P@ssw0rd", passwordField.GetAttribute("value"), "Wrong Password");
            driver.FindElement(By.Id("login")).Click();
            string expectedPageTitle = "Adactin.com - Search Hotel";
            string actualPageTitle = driver.Title;
            Assert.AreEqual(expectedPageTitle, actualPageTitle, "Incorrect page title");
            bool loginSuccess = expectedPageTitle.Equals(actualPageTitle);
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.PartialLinkText("Click")).Click();

            driver.Quit();

            return loginSuccess;
        }

        public static string TC_2_LoginWithInvalidCredentials()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://adactinhotelapp.com/");
            IWebElement usernameField = driver.FindElement(By.Id("username"));
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            usernameField.SendKeys("theomyway");   //---username
            passwordField.SendKeys("Omar Khan - 10619");   //---paswword
            Assert.AreEqual("theomyway", usernameField.GetAttribute("value"), "Wrong Username");   //--getting value from textbox and comparing with actual username
            Assert.AreEqual("Omar Khan - 10619", passwordField.GetAttribute("value"), "Wrong Password");  //--getting value from textbox and comparing with actual password
            driver.FindElement(By.Id("login")).Click();   //----logging in

            string errorMessage = driver.FindElement(By.ClassName("auth_error")).Text; //--Getting auth_error into variable

            driver.Quit();

            return errorMessage;    //----returning variable which has error msg
        }
        public static void TC_3_VerifyMandatoryLoginFields()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://adactinhotelapp.com/");
            driver.FindElement(By.Id("login")).Click();


            string usernameError = driver.FindElement(By.Id("username_span")).Text;  //--Using username_span to get info or error msg alongside textbox field
            Assert.AreEqual("Enter Username", usernameError);  //---Comparing the username field errors
            driver.FindElement(By.Id("username")).SendKeys("theomyway"); //----Entering with wrong username
            driver.FindElement(By.Id("login")).Click();   //----Pressing login button
            string passwordError = driver.FindElement(By.Id("password_span")).Text;   //-----Getting pwd validation error msg into variable
            Assert.AreEqual("Enter Password", passwordError);   //---Checking if both error msgs are same on pwd field

            driver.Quit();  //--Quitting browser action
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Muhammad Umar Khan - 10619 - SQA FINAL PROJECT");
            //Test Case#01(Running) : Logging in to Adactin with valid credentials
            bool loginResult = LoginTestCases.TC_1_LoginWithValidCredentials();
            Console.WriteLine("Login result: " + loginResult);

            //Test Case#02(Running) : Logging in to Adactin with invalid credentials
            string errorMessage = LoginTestCases.TC_2_LoginWithInvalidCredentials();
            Console.WriteLine("Error message: " + errorMessage);

            //Test Case#03(Running) : Verifying the Validation After Leaving Username empty then Password empty after pressing login
            LoginTestCases.TC_3_VerifyMandatoryLoginFields();

            Console.ReadKey();
        }
    }
}