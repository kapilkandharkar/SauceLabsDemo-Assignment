using LeanTech.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LeanTech.Pages
{
    public class CheckoutPage
    {
        private readonly IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement btnContinue => driver.FindElement(By.XPath(".//input[@id='continue']"));
        IWebElement btnCancel => driver.FindElement(By.XPath(".//button[@id='cancel']"));
        IWebElement txtFirstName => driver.FindElement(By.XPath(".//input[@id='first-name']"));
        IWebElement txtLastName => driver.FindElement(By.XPath(".//input[@id='last-name']"));
        IWebElement txtPostalCode => driver.FindElement(By.XPath(".//input[@id='postal-code']"));
        IWebElement btnFinish => driver.FindElement(By.XPath(".//button[@id='finish']"));

        IWebElement lblPaymentInfo => driver.FindElement(By.XPath(".//div[@data-test='payment-info-value']"));
        IWebElement lblShippingInfo => driver.FindElement(By.XPath(".//div[@data-test='shipping-info-value']"));
        IWebElement lblSubTotal => driver.FindElement(By.XPath(".//div[@data-test='subtotal-label']"));
        IWebElement lblTax => driver.FindElement(By.XPath(".//div[@data-test='tax-label']"));
        IWebElement lblTotal => driver.FindElement(By.XPath(".//div[@data-test='total-label']"));
        IWebElement lblCompleteHeader => driver.FindElement(By.XPath(".//h2[@data-test='complete-header']"));
        IWebElement lblCompleteText => driver.FindElement(By.XPath(".//div[@data-test='complete-text']"));

        IWebElement btnBackToHome => driver.FindElement(By.XPath(".//button[@id='back-to-products']"));

        public void SetCheckoutInfo(string firstName, string lastName, string postalCode)
        {
            txtFirstName.SetText(firstName);
            txtLastName.SetText(lastName);
            txtPostalCode.SetText(postalCode);
        }

        public void ClickButton(string buttonText)
        {
            switch (buttonText.ToLower())
            {
                case "continue":
                    btnContinue.Click();
                    break;
                case "finish":
                    btnFinish.Click();
                    break;
                case "back home":
                    btnBackToHome.Click();
                    break;
                case "cancel":
                    btnCancel.Click();
                    break;
            }
            
        }
        
        public void ValidatePaymentInfo(string expValue)
        {
            Assert.AreEqual(expValue, lblPaymentInfo.Text);
        }

        public void ValidateShippingInfo(string expValue)
        {
            Assert.AreEqual(expValue, lblShippingInfo.Text);
        }

        public void ValidatePriceTotal(string[] cartItemPrices, string taxPercent = "8", string currencyChar = "$")
        {
            double totalPrice, itemTotal, tax;
            string expTotalPrice, expItemTotal, expTax;
            itemTotal = 0;
            foreach (var cartItemPrice in cartItemPrices)
            {
                itemTotal = itemTotal + Double.Parse(cartItemPrice); 
            }
            itemTotal = Math.Round(itemTotal, 2);
            tax = Math.Round((double)(itemTotal * Int32.Parse(taxPercent) / 100),2);
            totalPrice = Math.Round(itemTotal+tax, 2);
            
            expItemTotal = currencyChar + itemTotal.ToString();
            expTax = currencyChar + tax.ToString();
            expTotalPrice = currencyChar + totalPrice.ToString(); 

            Assert.IsTrue(lblSubTotal.Text.Contains(expItemTotal), "Item total value NOT OK");
            Assert.IsTrue(lblTax.Text.Contains(expTax), "Tax value NOT OK");
            Assert.IsTrue(lblTotal.Text.Contains(expTotalPrice), "Total Price value NOT OK");

        }



        public void ValidateOrderCompleteHeader(string expHeaderText)
        {
            string actHeaderText = lblCompleteHeader.Text;
            Assert.AreEqual(expHeaderText, actHeaderText, "Order Complete Header Text NOT OK");
        }

        public void ValidateOrderCompleteMessage(string expMsg)
        {
            string actMsg = lblCompleteText.Text;
            Assert.AreEqual(expMsg, actMsg, "Order Complete Message Text NOT OK");
        }


    }
}
