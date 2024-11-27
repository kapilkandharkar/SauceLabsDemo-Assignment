using LeanTech.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanTech.Pages
{
    public class ShoppingCartPage
    {
        private readonly IWebDriver driver;
        public ShoppingCartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement btnCheckout => driver.FindElement(By.XPath(".//button[@id='checkout']"));
        IWebElement btnContinueShop => driver.FindElement(By.XPath(".//button[@id='continue-shopping']"));
        IWebElement listCart => driver.FindElement(By.XPath(".//div[@class='cart_list']"));
        IWebElement lblCartItemName => driver.FindElement(By.XPath(".//div[@class='inventory_item_name']"));
        IWebElement lblCartItemPrice => driver.FindElement(By.XPath(".//div[@class='inventory_item_price']"));
        IWebElement lblCartItemDesc => driver.FindElement(By.XPath(".//div[@class='inventory_item_desc']"));

        public void ValidateProductNameInCart(string expProductName)
        {
            IWebElement itemName = driver.FindElement(By.XPath($".//div[@class='cart_item']//div[@class='inventory_item_name' and text()='{expProductName}']"));
            Assert.AreEqual(expProductName, itemName.Text, "Product Name not present in Cart!");

        }

        public void ValidateProductPriceInCart(string productName, string expProductPrice)
        {
            IWebElement itemPrice = driver.FindElement(By.XPath($".//div[text()='{productName}']/ancestor::div[@class='cart_item']//div[@class='inventory_item_price']"));
            Assert.AreEqual(expProductPrice, itemPrice.Text, "Product Price not in Cart is not as expected!");

        }

        public void ClickCheckout()
        {
            btnCheckout.Click();
        }

        




    }
}
