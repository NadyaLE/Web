using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;

namespace Tests
{
    public class Tests
    {
        private IWebDriver driver;
        private readonly By aa = By.XPath("//input[@id='CRLRate']");
        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:49609/index.aspx");
            //File.WriteAllText(@"C:\Users\nlitk\OneDrive\Рабочий стол\1.txt", "тесты открыты");
        }

        [Test]
        public void Test1()
        {
            var gg = driver.FindElement(aa);

            gg.SendKeys("blablabla");
            //Assert.Pass();
        }
        [TearDown]
        public void TearDown()
        {
            
        }
    }
}