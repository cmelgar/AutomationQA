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
            driver.Url = "http://www.ebay.com";
            ControlsBE controls = new ControlsBE();
            ShoeBE shoe = new ShoeBE();
            InitializeControls(ref controls, ref shoe);

            var elementShoes = FindObject(controls.InputSearch, Constantes.ID, driver);
            elementShoes.SendKeys("shoes");

            var elementButton = FindObject(controls.ButtonSearch, Constantes.ID, driver);
            elementButton.Click();

            var elementBrand = FindObject(controls.InputBrand, Constantes.XPATH, driver);
            elementBrand.Click();

            var elementDetail = FindObject(controls.InputDetail, Constantes.XPATH, driver);
            elementDetail.Click();

            var result = FindObject(controls.InputResult, Constantes.XPATH, driver);
            Console.WriteLine(result.Text);

            var elementButtonOrder = FindObject(controls.ButtonOrder, Constantes.XPATH, driver);

            Actions mouse = new Actions(driver);
            mouse.MoveToElement(elementButtonOrder).Perform();

            var elementOrder = FindObject(controls.ProductOptions, Constantes.XPATH, driver);
            List<IWebElement> elementOption = elementOrder.FindElements(By.TagName("li")).ToList();
            int i;
            for (i = 0; i < elementOption.Count; i++)
            {
                if (elementOption[i].Text.Contains("lower") || elementOption[i].Text.Contains("bajo"))
                {
                    elementOption[i].Click();
                    break;
                }
            }
            
            IWebElement elementProduct = FindObject(controls.ProductElement, Constantes.XPATH, driver);

            i = 0;
            List<IWebElement> list = new List<IWebElement>();

            ShoeBE objShoe;
            
            List<ShoeBE> listShoe = new List<ShoeBE>();
            while (i < 5)
            {
                list.Add(elementProduct.FindElements(By.XPath(controls.ProductElementInfo))[i]);
                i++;
            }

            for (i = 0; i < list.Count; i++)
            {
                objShoe = new ShoeBE();
                objShoe.name = list.ElementAt(i).FindElement(By.XPath(shoe.name)).Text;
                objShoe.price = list.ElementAt(i).FindElement(By.XPath(shoe.price)).Text;
                listShoe.Add(objShoe);
            }

            List<ShoeBE> nameOrder = listShoe.OrderBy(o=>o.name).ToList();
            Console.WriteLine("Lista de productos por nombre ascendente: ");
            Console.WriteLine("------------------------------------------");
            for (i = 0; i < nameOrder.Count; i++)
            {
                Console.WriteLine("Producto {0} : ",i);
                Console.WriteLine("Nombre : " + nameOrder.ElementAt(i).name);
                Console.WriteLine("Precio : " + nameOrder.ElementAt(i).price);
            }

            List<ShoeBE> priceOrder = listShoe.OrderByDescending(o => o.price).ToList();
            Console.WriteLine("Lista de productos por precio descendente: ");
            Console.WriteLine("-------------------------------------------");
            for (i = 0; i < priceOrder.Count; i++)
            {
                Console.WriteLine("Producto {0} : ", i);
                Console.WriteLine("Nombre : " + priceOrder.ElementAt(i).name);
                Console.WriteLine("Precio : " + priceOrder.ElementAt(i).price);
            }
        }

        private static void InitializeControls(ref ControlsBE controls,ref ShoeBE shoe)
        {
            controls.InputSearch = "gh-ac";
            controls.ButtonSearch = "gh-btn";
            controls.InputBrand = "//input[@aria-label='PUMA']";
            controls.InputDetail = "//input[@aria-label='Nuevo']";
            controls.InputResult = "//h1[@class='srp-controls__count-heading']";
            controls.ButtonOrder = "//div[@class='srp-controls__control--legacy']";
            controls.ProductOptions = "//ul[@class='srp-sort__menu']";
            controls.ProductElement = "//ul[@class='srp-results srp-grid clearfix']";
            controls.ProductElementInfo = "//div[@class='s-item__info clearfix']";

            shoe.name = "a[@class='s-item__link']/h3[@class='s-item__title']";
            shoe.price = "div[@class='s-item__details clearfix']/div[@class='s-item__detail s-item__detail--primary']/span[@class='s-item__price']/span[@class='ITALIC']";
            
        }

        private static IWebElement FindObject(string control,string by, IWebDriver driver)
        {
            IWebElement element;
            By locator = null;
            switch(by)
            {
                case "ID":
                    locator = By.Id(control);
                    break;
                case "XPATH":
                    locator = By.XPath(control);
                    break;
                default:
                    break;
            }
            
            element = driver.FindElement(locator);
            return element;
        } 
    }
}
