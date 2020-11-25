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
        public void SetupTests()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.razer.com/");
        }


        [Test]
        public void GetNumberOfItemsAddedToCart()
        {
            driver.Navigate().GoToUrl("https://www.razer.com/gaming-mice");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement allMiceButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/app-root/cx-storefront/cx-page-layout/cx-page-slot[1]/app-razer-dream[2]/app-v3-comp/section[3]/app-razer-dream/app-multipanels/section/div/div/div/div[2]/h3/a")));
            allMiceButton.Click();

            IWebElement addToCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mm-add-to-cart_RZ01-03350100-R3U1")));
            addToCartButton.Click();

            IWebElement viewCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/app-root/cx-storefront/cx-page-layout/cx-page-slot[1]/app-razer-main-sku/div/div/div[3]/button")));
            viewCartButton.Click();

            IWebElement addedItems = wait.Until(ExpectedConditions.ElementExists(By.Id("cart-item-counter")));

            string numberItemText = addedItems.Text;
            numberItemText = numberItemText.Split(' ')[1];
            numberItemText = numberItemText.Substring(1);
            numberItemText = numberItemText.Split(' ')[0];
            Assert.IsTrue(numberItemText == "1");
        }

        [TearDown]
        public void TearDownTests()
        {
            if (driver != null)
                driver.Quit();
        }

    }
}
