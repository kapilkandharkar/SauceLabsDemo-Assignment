using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using LeanTech.Common;
using LeanTech.Pages;
using Microsoft.VisualStudio.CodeCoverage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace LeanTech
{
    [TestClass]
    public class TestMethods
    {
        private IWebDriver _driver;
        private ChromeOptions _chromeOptions;

        public static ExtentReports extentReports;
        public static ExtentTest Test;
        public static ExtentTest Step;        

        [TestInitialize]
        public void Setup()
        {
            _chromeOptions = new ChromeOptions();            
            _chromeOptions.AddArgument("--guest"); // To disable pop up warning in chrome for password
            _driver = new ChromeDriver(_chromeOptions);
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            string resultFile = @"C:\Lean\LeanTech\LeanTech\Reports\Report_" + context.TestName +"_"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            extentReports = new ExtentReports();
            var sparkReporter = new ExtentSparkReporter(resultFile);
            extentReports.AttachReporter(sparkReporter);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            extentReports.Flush();
        }

        [TestMethod]
        public void Test_Checkout_With_Product_Names()
        {
            ExtentTest test = extentReports.CreateTest("Test_Checkout_With_Product_Names");
            HomePage homePage = new HomePage(_driver);
            LoginPage loginPage = new LoginPage(_driver);
            ProductsPage productsPage = new ProductsPage(_driver);
            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(_driver);
            CheckoutPage checkoutPage = new CheckoutPage(_driver);

            loginPage.Login("standard_user", "secret_sauce");
            homePage.ValidateHeaderText("Products");
            test.Log(Status.Info,"Login to application performed");

            productsPage.SortProductsBy("Price (low to high)");
            test.Log(Status.Info, "Products Sorted by Price - low to high");

            productsPage.AddToCart("Sauce Labs Onesie");
            test.Log(Status.Info, "Product Added to Cart - Sauce Labs Onesie");
            productsPage.AddToCart("Sauce Labs Bike Light");
            test.Log(Status.Info, "Product Added to Cart - Sauce Labs Bike Light");
            productsPage.AddToCart("Sauce Labs Bolt T-Shirt");
            test.Log(Status.Info, "Product Added to Cart - Sauce Labs Bolt T-Shirt");
            productsPage.AddToCart("Test.allTheThings() T-Shirt (Red)");
            test.Log(Status.Info, "Product Added to Cart - Test.allTheThings() T-Shirt (Red)");

            homePage.GoToCart();
            homePage.ValidateHeaderText("Your Cart");

            shoppingCartPage.ValidateProductNameInCart("Sauce Labs Bike Light");
            shoppingCartPage.ValidateProductPriceInCart("Sauce Labs Bike Light", "$9.99");
            shoppingCartPage.ValidateProductPriceInCart("Sauce Labs Onesie", "$7.99");
            shoppingCartPage.ClickCheckout();

            checkoutPage.SetCheckoutInfo("Tim", "David", "411015");
            checkoutPage.ClickButton("Continue");
            test.Log(Status.Info, "Checkout Info updated");

            string[] cartPrices = { "7.99", "9.99", "15.99", "15.99" };
            checkoutPage.ValidatePriceTotal(cartPrices);
            test.Log(Status.Pass, "Price Total values verfied on order summary");
            checkoutPage.ClickButton("Finish");
            checkoutPage.ValidateOrderCompleteHeader("Thank you for your order!");
            checkoutPage.ValidateOrderCompleteMessage("Your order has been dispatched, and will arrive just as fast as the pony can get there!");            
            checkoutPage.ClickButton("Back Home");
            test.Log(Status.Pass, "Checkout completed with product added using Product Names");
        }

        [TestMethod]
        public void Test_Checkout_With_Random_Products()
        {
            ExtentTest test = extentReports.CreateTest("Test_Checkout_With_Random_Products");
            HomePage homePage = new HomePage(_driver);
            LoginPage loginPage = new LoginPage(_driver);
            ProductsPage productsPage = new ProductsPage(_driver);
            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(_driver);
            CheckoutPage checkoutPage = new CheckoutPage(_driver);

            loginPage.Login("standard_user", "secret_sauce");
            homePage.ValidateHeaderText("Products");
            test.Log(Status.Info, "Login to application performed");

            Random objRandom = new Random();
            productsPage.AddToCartByIndex(objRandom.Next(1, 6));
            test.Log(Status.Info, "First Random Product Added to Cart");
            productsPage.AddToCartByIndex(objRandom.Next(1, 6));
            test.Log(Status.Info, "Second Random Product Added to Cart");
            productsPage.AddToCartByIndex(objRandom.Next(1, 6));
            test.Log(Status.Info, "Third Random Product Added to Cart");

            homePage.GoToCart();
            homePage.ValidateHeaderText("Your Cart");

            shoppingCartPage.ClickCheckout();

            checkoutPage.SetCheckoutInfo("Tim", "David", "411015");
            checkoutPage.ClickButton("Continue");
            test.Log(Status.Info, "Checkout Info updated");

            checkoutPage.ClickButton("Finish");
            checkoutPage.ValidateOrderCompleteHeader("Thank you for your order!");
            checkoutPage.ValidateOrderCompleteMessage("Your order has been dispatched, and will arrive just as fast as the pony can get there!");
            checkoutPage.ClickButton("Back Home");
            test.Log(Status.Pass, "Checkout completed with random product added");
        }
    }
}