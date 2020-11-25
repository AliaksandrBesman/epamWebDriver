using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverLab
{
    class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void BrowserSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.razer.com/");
        }


        [Test]
        public void GetItemAddedToCartAndCompareWithSelectedOne()
        {
            driver.Navigate().GoToUrl("https://www.razer.com/shop/mice/gaming-mice");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement productList = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("cx-product-container")));
            IWebElement firstProductFromList = productList.FindElement(By.XPath("//app-razer-product-grid-item"));

            IWebElement nameProductWebElement = firstProductFromList.FindElement(By.ClassName("product-item-title"));
            string nameProduct = nameProductWebElement.Text;

            IWebElement addToCartButton = firstProductFromList.FindElement(By.ClassName("add-to-cart-btn"));
            addToCartButton.Click();

            IWebElement mainItemDevBlock = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//app-razer-main-sku")));
            IWebElement viewCartButton = mainItemDevBlock.FindElement(By.ClassName("button-primary"));
            viewCartButton.Click();

            IWebElement cartItemList = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//app-razer-cart-item-list")));
            IWebElement addedItemInfoContainer = cartItemList.FindElement(By.ClassName("cx-info-container"));
            IWebElement nameItemAddedToCart = addedItemInfoContainer.FindElement(By.ClassName("cx-name"));
            string nameProductAddedToCart = nameItemAddedToCart.Text;

            IWebElement numberAddedItems = driver.FindElement(By.Id("cart-item-counter"));

            string numberItemText = numberAddedItems.Text;
            numberItemText = numberItemText.Split(' ')[1];
            numberItemText = numberItemText.Substring(1);
            numberItemText = numberItemText.Split(' ')[0];

            Assert.IsTrue(numberItemText == "1" && nameProduct == nameProductAddedToCart);

        }

        [TearDown]
        public void BrowserTearDown()
        {
            if (driver != null)
                driver.Quit();
        }

    }
}
