using LeanTech.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanTech.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;

        }

        IWebElement txtUsername => driver.FindElement(By.XPath(".//input[@id='user-name']"));
        IWebElement txtPassword => driver.FindElement(By.XPath(".//input[@id='password']"));
        IWebElement btnLogin => driver.FindElement(By.XPath(".//input[@id='login-button']"));
        

        public void Login(string username, string password)
        {
            txtUsername.SetText(username);
            txtPassword.SetText(password);
            btnLogin.Click();
        }

        

    }
}
