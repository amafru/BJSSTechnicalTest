using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace BJSSTech2
{
    [TestFixture]
    public class Class1
    {
        //declarring the method for taking a screenshot at the end of the test
        public ITakesScreenshot FirefoxInstance { get; private set; }

        [Test]
        public void tcQuickViewItem()
        {
            IWebDriver driver = new FirefoxDriver();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string username = ("kelly-1993@live.co.uk");
            string password = ("BJSSTest");
            string url = ("http://automationpractice.com/index.php");

            /*To run tests Headless using chrome (remember to change the driver above when doing this
             * (insert this before driver.Navigate()...etc
             */

            //var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless");
            //driver = new ChromeDriver(chromeOptions);


            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Sign into user account 
            driver.FindElement(By.XPath("//a[@class='login'][contains(.,'Sign in')]")).Click();
            driver.FindElement(By.XPath("//input[@id='email']")).SendKeys(username);
            driver.FindElement(By.XPath("//input[contains(@name,'passwd')]")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Sign in')]")).Click();

            //Go to 'Womens' tab
            driver.FindElement(By.XPath("//a[contains(@title,'Women')]")).Click();

            //Scroll down page so the desired element (item) is in full view
            js.ExecuteScript("window.scrollBy(0,800);");

            //select product 'Faded Short Sleeve T-Shirts in Full view
            driver.FindElement(By.XPath("//img[contains(@title,'Faded Short Sleeve T-shirts')]")).Click();

            //Navigate to and select size 'Large' from the dropdown box       
            driver.FindElement(By.Id("group_1")).Click();
            new SelectElement(driver.FindElement(By.Id("group_1"))).SelectByText("L");
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='L'])[1]/following::option[3]")).Click();

            //Add the selected item in its chosen size to cart
            driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Add to cart')]")).Click();

            //Click "Continue Shopping"
            //driver.FindElement(By.XPath("(//span[contains(.,'Continue shopping')])[2]")).Click();
            js.ExecuteScript("window.scrollBy(0,50);");
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath("(//span[contains(.,'Continue shopping')])[2]")).Click();

            //Add Another item to basket
            //Chose 'Printed Dress' Model Demo_4
            driver.FindElement(By.XPath("(//a[@href='http://automationpractice.com/index.php?id_category=8&controller=category'][contains(.,'Dresses')])[2]")).Click();
            js.ExecuteScript("window.scrollBy(0,800);");
            driver.FindElement(By.XPath("//img[@src='http://automationpractice.com/img/p/1/0/10-home_default.jpg']")).Click();

            //Add product to cart in default size
            driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Add to cart')]")).Click();

            //Click 'Proceed to Checkout'
            js.ExecuteScript("window.scrollBy(0,50);");
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath("//span[contains(.,'Proceed to checkout')]")).Click();



            //view items in basket. 
            //confirm prices are correct
            //and total incl shipping + tax is correct
            //Code below obtained using Katalon's "assert text" feature
            //P.S: Variable names are case sensitive! Capital elements must be written as capitals in the code too. Otherwise test will probably fail.

            Assert.AreEqual("Total products", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Total'])[4]/following::td[2]")).Text);
            Assert.AreEqual("Total shipping", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='$0.00'])[1]/following::td[1]")).Text);
            Assert.AreEqual("Tax", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='$69.50'])[3]/following::td[1]")).Text);
            Assert.AreEqual("TOTAL", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='$2.78'])[1]/following::span[1]")).Text);

            //click 'Proceed to CheckOut'
            driver.FindElement(By.XPath("(//span[contains(.,'Proceed to checkout')])[2]")).Click();

            //click 'Proceed to CheckOut' again
            driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Proceed to checkout')]")).Click();

            //Tick 'Agree to terms' and click 'Proceed to CheckOut' again
            driver.FindElement(By.XPath("//input[contains(@name,'cgv')]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Proceed to checkout')]")).Click();

            //Select 'Pay by Bank Wire'
            driver.FindElement(By.XPath("//a[@class='bankwire'][contains(.,'Pay by bank wire (order processing will be longer)')]")).Click();

            //select '...Confirm... Order'
            driver.FindElement(By.XPath("//button[@class='button btn btn-default button-medium'][contains(.,'I confirm my order')]")).Click();


            //first scroll down and centralise the screen a little to capture the full order details (as laptop screen is small and details hide below screen edge
            js.ExecuteScript("window.scrollBy(0,300);");


            //Take a Screenshot of the Order Confirmation Page and save in a specified folder
            //Declare the  the Screenshot datatype, variable name and parameter
            Screenshot TakeScreenShot = ((ITakesScreenshot)driver).GetScreenshot();

            //declare the take screenshot method, specify save location (AND ADD NAME TO CALL THE SCREENSHOT FILE- 'BJSSTestOrderConfirmation.jpeg' used here
            //And specify the screenshot file format at the end of the parameter too. 
            //That's the full formula
            TakeScreenShot.SaveAsFile(@"C:\Users\HP User\Documents\Test Automation\Selenium Self Learning\Automation Test ScreenShots\BJSSTestOrderConfirmation1.jpeg",ScreenshotImageFormat.Jpeg);

            System.Threading.Thread.Sleep(2000);
            driver.Close();
        }

    }
}
