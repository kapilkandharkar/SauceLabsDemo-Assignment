using LeanTech.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanTech.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver driver;
        public ProductsPage(IWebDriver driver) 
        {
            this.driver = driver;
        }

        IWebElement selProductSort => driver.FindElement(By.XPath(".//select[@class='product_sort_container']"));
        IWebElement lblNumOfItemsShoppingCart => driver.FindElement(By.XPath(".//span[@class='shopping_cart_badge']"));
        IWebElement lnkShoppingCart => driver.FindElement(By.XPath(".//a[@class='shopping_cart_link']"));


        public void SortProductsBy(string sortByValue)
        {
            WebCommon.SelectDropDownByText(selProductSort, sortByValue);
        }

        public void AddToCart(string productName)
        {
            IWebElement lblProductDesc = driver.FindElement(By.XPath($".//div[text()='{productName}']/ancestor::div[@class='inventory_item_description']"));
            IWebElement btnAddToCart = lblProductDesc.FindElement(By.XPath("//button[contains(@id,'add-to-cart')]"));
            btnAddToCart.Click();
        }

        public void AddToCartByIndex(int index)
        {
            
            if (lnkShoppingCart.Text != "")
            {
                index = Math.Abs(index - Int32.Parse(lnkShoppingCart.Text));
                if(index == 0)
                {
                    index = 1;
                }
            }
            
            IWebElement btnAddToCart = driver.FindElement(By.XPath($"(.//button[text()='Add to cart'])[{index}]"));
            btnAddToCart.Click();
        }

    }
}
