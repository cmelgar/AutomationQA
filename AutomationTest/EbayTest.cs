using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using EntidadQA;

namespace AutomationTest
{
    class EbayTest
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            IWebElement elementShoes;
            driver.Url = "http://www.ebay.com";
            By locator = By.Id("gh-ac");
            elementShoes = driver.FindElement(locator);

            elementShoes.SendKeys("shoes");

            IWebElement elementButton;
            By locatorButton = By.Id("gh-btn");
            elementButton = driver.FindElement(locatorButton);

            elementButton.Click();
            IWebElement elementinputcheck;
            //div[@class=x-searcheable-list]
            By xpath = By.XPath("//input[@aria-label='PUMA']");
            //By xpath = By.XPath("//*[@id='w_1551557179374_cbx']");
            elementinputcheck = driver.FindElement(xpath);
            elementinputcheck.Click();

            IWebElement elementinputcheck2;
            By xpath2 = By.XPath("//input[@aria-label='Nuevo']");
            elementinputcheck2 = driver.FindElement(xpath2);
            elementinputcheck2.Click();

            //IWebElement result;
            By xpath3 = By.XPath("//h1[@class='srp-controls__count-heading']");
            String result = driver.FindElement(xpath3).Text;
            Console.Out.WriteLine(result);

            IWebElement buttonOrder;
            By xpathOrder = By.XPath("//div[@class='srp-controls__control--legacy']");
            buttonOrder = driver.FindElement(xpathOrder);
            //buttonOrder.Click();

            //IWebDriver wait = new IWebDriver(driver,TimeSpan.FromSeconds(10));
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //buttonOrder = wait.Until(ExpectedConditions);

            Actions mouse = new Actions(driver);
            mouse.MoveToElement(buttonOrder).Perform();

            IWebElement elementselect;
            //div[@class=x-searcheable-list]
            By xpath4 = By.XPath("//ul[@class='srp-sort__menu']");
            elementselect = driver.FindElement(xpath4);

            //elementselect.Click();
            int i;
            for (i = 0; i < elementselect.FindElements(By.TagName("li")).Count; i++)
            {
                if (elementselect.FindElements(By.TagName("li"))[i].Text.Contains("lower")
                    || elementselect.FindElements(By.TagName("li"))[i].Text.Contains("bajo"))
                {
                    elementselect.FindElements(By.TagName("li"))[i].Click();
                    break;
                }
            }

            IWebElement products;
            By xpath5 = By.XPath("//ul[@class='srp-results srp-grid clearfix']");
            products = driver.FindElement(xpath5);

            i = 0;
            List<IWebElement> list = new List<IWebElement>();
            Shoe objShoe;
            List<Shoe> listShoe = new List<Shoe>(); 
            while (i < 5)
            {
                //list.Add(products.FindElements(By.TagName("li"))[i]);
                list.Add(products.FindElements(By.XPath("//div[@class='s-item__info clearfix']"))[i]);
                
                //objShoe.name = products.FindElements(By.XPath("//div[@class='s-item__info clearfix']"))[i].FindElement(By.XPath("//h3[@class='s-item__title']")).Text));
                //listShoe.Add(objShoe);
                i++;
            }

            for (i = 0; i < list.Count; i++)
            {
                objShoe = new Shoe();
                objShoe.name = list.ElementAt(i).FindElement(By.XPath("a[@class='s-item__link']/h3[@class='s-item__title']")).Text;
                //objShoe.price = list.ElementAt(i).FindElement(By.XPath("//span[@class='ITALIC']")).Text;
                objShoe.price = list.ElementAt(i).FindElement(By.XPath("div[@class='s-item__details clearfix']/div[@class='s-item__detail s-item__detail--primary']/span[@class='s-item__price']/span[@class='ITALIC']")).Text;
                //
                listShoe.Add(objShoe);
            }
            //Console.WriteLine(list.ElementAt(0).FindElement(By.XPath("//h3[@class='s-item__title']")).Text);
            //Console.WriteLine(listShoe.ElementAt(0).name);
            //Console.WriteLine(listShoe.ElementAt(0).price);
            List<Shoe> nameOrder = listShoe.OrderBy(o=>o.name).ToList();
            for (i = 0; i < nameOrder.Count; i++)
            {
                Console.WriteLine(nameOrder.ElementAt(i).name);
                Console.WriteLine(nameOrder.ElementAt(i).price);
            }

            List<Shoe> priceOrder = listShoe.OrderByDescending(o => o.price).ToList();
            for (i = 0; i < priceOrder.Count; i++)
            {
                Console.WriteLine(priceOrder.ElementAt(i).name);
                Console.WriteLine(priceOrder.ElementAt(i).price);
            }
        }
    }
}
