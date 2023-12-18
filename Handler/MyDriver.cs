using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Security.Cryptography;
using System.Diagnostics;

namespace FirstTest.Handler
{
    public class MyDriver
    {

        private static int refres_count = 0;
        public MyDriver(){

        }

        public static IWebElement FindElement(string cssSelector){
            //use this method if you expect to find the element
            AssertStillWorking();
            WebDriverWait webDriverWait = new(Program._driver, new TimeSpan(0,0,Program.random.Next(20,30))); //after 20 ~ 30 seconds. Let consider there is an issue.
            try{
                webDriverWait.Until(condition =>
                            {
                                try
                                {
                                    IWebElement element = Program._driver.FindElement(By.CssSelector(cssSelector));
                                    return element.Displayed && element.Size.Height > 0 && element.Size.Width > 0; // must find the element but also it gets a certain size.
                                }
                                catch (StaleElementReferenceException)
                                {
                                    return false;
                                }
                                catch (NoSuchElementException)
                                {
                                    return false;
                                }
                                catch (OpenQA.Selenium.InvalidSelectorException)
                                {
                                    return false;
                                }
                            });
            
                return Program._driver.FindElement(By.CssSelector(cssSelector));
            }
            catch (WebDriverTimeoutException)
            {
                Debug.WriteLine($"Time out. element not found. cssSelector: {cssSelector}.");
                throw;
            }
            
        }

        public static bool ElementExists(string cssSelector){
            //use this method if you expect not to found the element
            AssertStillWorking();
            WebDriverWait webDriverWait = new(Program._driver, new TimeSpan(0,0,Program.random.Next(2,4))); //after 2 ~ 4 seconds we will consider the element does not exist. 
           
           try
           {
                return webDriverWait.Until(condition =>
                            {
                                try
                                {
                                    return Program._driver.FindElement(By.CssSelector(cssSelector)).Displayed;
                                }
                                catch (StaleElementReferenceException)
                                {
                                    return false;
                                }
                                catch (NoSuchElementException)
                                {
                                    return false;
                                }
                                catch (OpenQA.Selenium.InvalidSelectorException)
                                {
                                    return false;
                                }
                            });
           }
           catch (OpenQA.Selenium.WebDriverTimeoutException)
           {
                return false;
           }
            
        }

        public static ref Actions MoveToElement(string cssSelector, ref Actions act){
            AssertStillWorking();
            IWebElement element = FindElement(cssSelector);
            System.Drawing.Size size = element.Size;
            int xOffsetMax = size.Width / 2;
            int yOffsetMax = size.Height / 2;

            act.MoveToElement(element,Program.random.Next(- xOffsetMax, xOffsetMax),Program.random.Next(- yOffsetMax, yOffsetMax));
            act.Pause(new TimeSpan(0,0,0,Program.random.Next(0,2),Program.random.Next(145,999)));
            return ref act;
        }

        public static ref Actions MoveToElement(IWebElement element, ref Actions act){
            AssertStillWorking();

            System.Drawing.Size size = element.Size;
            int xOffsetMax = size.Width / 2;
            int yOffsetMax = size.Height / 2;

            act.MoveToElement(element,Program.random.Next(- xOffsetMax, xOffsetMax),Program.random.Next(- yOffsetMax, yOffsetMax));
            act.Pause(new TimeSpan(0,0,0,Program.random.Next(0,2),Program.random.Next(145,999)));
            return ref act;
        }

        private static void AssertStillWorking()
        {
            var logs = Program._driver.Manage().Logs.GetLog(LogType.Browser);
            foreach (var log in logs)
            {
                if(log.Message.Contains("Unexpected end of JSON input")){
                    Debug.WriteLine(log.Message);
                    if(refres_count == 0){
                        Program._driver.Navigate().Refresh();
                        refres_count++;
                    }else{
                        throw new MustRestartException();
                    }
                }
            }
           
        }

        public static void AssertElementDisappear(string cssSelector){
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (ElementExists(cssSelector + ":visible"))
            {
                if (stopwatch.Elapsed.TotalSeconds > 10)
                {
                    stopwatch.Stop();
                    Debug.WriteLine($"cssSelector = {cssSelector}");
                    throw new MustRestartException();
                }
            }
            stopwatch.Stop();
        }

        public static bool CheckElementContains(string cssSelector, string attribute, string content){
            IWebElement element = FindElement(cssSelector);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!element.GetAttribute(attribute).Contains(content))
            {
                if (stopwatch.Elapsed.TotalSeconds > 2)
                {
                    stopwatch.Stop();

                    return false;
                }
            }

            stopwatch.Stop();
            return true;
        }
    }
}