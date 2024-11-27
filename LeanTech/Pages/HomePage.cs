using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanTech.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement linkShopCart => driver.FindElement(By.XPath(".//div[@id='shopping_cart_container']/a[@class='shopping_cart_link']"));
        IWebElement lblHeader => driver.FindElement(By.XPath(".//span[@class='title']"));

        public void ValidateHeaderText(string expLabelTitleText)
        {
            string actLabelTitleText = lblHeader.Text;
            Assert.AreEqual(actLabelTitleText, expLabelTitleText, "Header Text NOT OK");
        }

        public void GoToCart()
        {
            linkShopCart.Click();
        }
    }
    
}
