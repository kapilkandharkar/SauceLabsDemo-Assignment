using LeanTech.Pages;
using Microsoft.VisualStudio.CodeCoverage;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.Mime.MediaTypeNames;

namespace LeanTech
{
    [TestClass]
    public class TestMethods
    {

        [TestMethod]
        public void TestMethod()
        {
            ChromeOptions options = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(options);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Manage().Window.Maximize();

            HomePage homePage = new HomePage(driver);
            LoginPage loginPage = new LoginPage(driver);
            ProductsPage productsPage = new ProductsPage(driver);
            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(driver);
            CheckoutPage checkoutPage = new CheckoutPage(driver);

            loginPage.Login("standard_user", "secret_sauce");
            homePage.ValidateHeaderText("Products");
            productsPage.SortProductsBy("Price (low to high)");

            productsPage.AddToCart("Sauce Labs Onesie");
            productsPage.AddToCart("Sauce Labs Bike Light");
            productsPage.AddToCart("Sauce Labs Bolt T-Shirt");
            productsPage.AddToCart("Test.allTheThings() T-Shirt (Red)");            
                
            homePage.GoToCart();
            homePage.ValidateHeaderText("Your Cart");

            shoppingCartPage.ValidateProductNameInCart("Sauce Labs Bike Light");
            shoppingCartPage.ValidateProductPriceInCart("Sauce Labs Bike Light", "$9.99");
            shoppingCartPage.ValidateProductPriceInCart("Sauce Labs Onesie", "$7.99");
            shoppingCartPage.ClickCheckout();

            checkoutPage.SetCheckoutInfo("Tim", "David", "411015");
            checkoutPage.ClickContinue();

            string[] cartPrices = { "7.99", "9.99", "15.99", "15.99" };
            checkoutPage.ValidatePriceTotal(cartPrices);

            checkoutPage.ClickFinish();

            checkoutPage.ValidateOrderCompleteHeader("Thank you for your order!");

            checkoutPage.ValidateOrderCompleteMessage("Your order has been dispatched, and will arrive just as fast as the pony can get there!");

            checkoutPage.ClickBackToHome();
        }
    }
}