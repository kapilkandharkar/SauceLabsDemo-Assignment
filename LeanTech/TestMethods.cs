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
        private IWebDriver _driver;
        private ChromeOptions _chromeOptions;
        [TestInitialize]
        public void Setup()
        {
            _chromeOptions = new ChromeOptions();            
            _chromeOptions.AddArgument("--guest"); // To diable pop up warning in chrome for password
            _driver = new ChromeDriver(_chromeOptions);
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void Test_Checkout_With_Product_Names()
        {
            HomePage homePage = new HomePage(_driver);
            LoginPage loginPage = new LoginPage(_driver);
            ProductsPage productsPage = new ProductsPage(_driver);
            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(_driver);
            CheckoutPage checkoutPage = new CheckoutPage(_driver);

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
            checkoutPage.ClickButton("Continue");

            string[] cartPrices = { "7.99", "9.99", "15.99", "15.99" };
            checkoutPage.ValidatePriceTotal(cartPrices);
            checkoutPage.ClickButton("Finish");
            checkoutPage.ValidateOrderCompleteHeader("Thank you for your order!");
            checkoutPage.ValidateOrderCompleteMessage("Your order has been dispatched, and will arrive just as fast as the pony can get there!");
            checkoutPage.ClickButton("Back Home");
        }

        [TestMethod]
        public void Test_Checkout_With_Random_Products()
        {
            HomePage homePage = new HomePage(_driver);
            LoginPage loginPage = new LoginPage(_driver);
            ProductsPage productsPage = new ProductsPage(_driver);
            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(_driver);
            CheckoutPage checkoutPage = new CheckoutPage(_driver);

            loginPage.Login("standard_user", "secret_sauce");
            homePage.ValidateHeaderText("Products");

            productsPage.SortProductsBy("Price (low to high)");
            Random objRandom = new Random();
            productsPage.AddToCartByIndex(objRandom.Next(1, 6));
            productsPage.AddToCartByIndex(objRandom.Next(1, 6));
            productsPage.AddToCartByIndex(objRandom.Next(1, 6));

            homePage.GoToCart();
            homePage.ValidateHeaderText("Your Cart");

            shoppingCartPage.ClickCheckout();

            checkoutPage.SetCheckoutInfo("Tim", "David", "411015");
            checkoutPage.ClickButton("Continue");
            checkoutPage.ClickButton("Finish");
            checkoutPage.ValidateOrderCompleteHeader("Thank you for your order!");
            checkoutPage.ValidateOrderCompleteMessage("Your order has been dispatched, and will arrive just as fast as the pony can get there!");
            checkoutPage.ClickButton("Back Home");
        }
    }
}