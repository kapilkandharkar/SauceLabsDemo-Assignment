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


    }
}
