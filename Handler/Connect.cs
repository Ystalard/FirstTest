using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Configuration;
using OpenQA.Selenium.Support.UI;

namespace FirstTest.Handler
{
    public static class Connector
    {
        public static void Connect(Actions act)
        {
            if(Program.settings.Login.IsFirstConnection)
            {
                Subscribe();
            }else{
                IWebElement loginTab = MyDriver.FindElement(Program.settings.Login.LoginTab);
                
                if(!loginTab.GetAttribute("class").Contains("active"))
                {
                    MyDriver.MoveToElement(Program.settings.Login.LoginTab, act).Click().Build().Perform();
                }

              
                MyDriver.MoveToElement(Program.settings.Login.LoginEmail, act).Click().SendKeys(Program.settings.Login.Email);
          
                MyDriver.MoveToElement(Program.settings.Login.LoginPassword, act).Click().SendKeys(Program.settings.Login.Password);
           
                MyDriver.MoveToElement(Program.settings.Login.ConnectButton, act).Click().Build().Perform();
                
                act.Click().Build().Perform();
                
                GoToLastGame(act);
            }

            Random random = new Random();
            long startTicks = TimeSpan.FromSeconds(1).Ticks;
            long endTicks = TimeSpan.FromSeconds(2).Ticks;
            long randomTicks = startTicks + (long)(random.NextDouble() * (endTicks - startTicks));
            TimeSpan randomDuration = TimeSpan.FromTicks(randomTicks); 
            act.Pause(randomDuration);
            act.Build().Perform(); // wait for the page to be fully loaded
        }

        public static void GoToLastGame(Actions act)
        {
            Random random = new Random();
            long startTicks = TimeSpan.FromSeconds(4).Ticks;
            long endTicks = TimeSpan.FromSeconds(5).Ticks;
            long randomTicks = startTicks + (long)(random.NextDouble() * (endTicks - startTicks));
            TimeSpan randomDuration = TimeSpan.FromTicks(randomTicks);
            act.Pause(randomDuration);
            act.Build().Perform();
            
            if(Program.settings.Login.GoToLastGame)
            {
                MyDriver.MoveToElement(Program.settings.Login.LastGame, act).Click().Build().Perform();
            }else{
                MyDriver.MoveToElement(Program.settings.Login.Play, act).Click().Build().Perform();
            }
            
            SwitchWindow();
        }

        public static void SwitchWindow()
        {
            //Wait for the new window or tab
            WebDriverWait webDriverWait = new(Program._driver, new TimeSpan(0,0,0, Program.random.Next(20,30), Program.random.Next(0,100)));
            webDriverWait.Until(wd => wd.WindowHandles.Count == 2 || Program._driver.Url != Program.originalUrl);

            //Loop through the windows to switch on the correct driver
            foreach(string window in Program._driver.WindowHandles)
            {
                if(Program.originalWindow != window)
                {
                    Program._driver.SwitchTo().Window(window);
                    break;
                }
            }

            webDriverWait.Until(wd => wd.Title != Program.originalTitleWindow);
        }

        private static void Subscribe()
        {
            Actions act = new(Program._driver);
            IWebElement registerTab = MyDriver.FindElement(Program.settings.Login.Register);
            
            
            if(!registerTab.GetAttribute("class").Contains("active"))
            {
                MyDriver.MoveToElement(Program.settings.Login.Register, act).Click().Build().Perform();
            }

            
            MyDriver.MoveToElement(Program.settings.Login.RegisterEmail, act).Click().SendKeys(Program.settings.Login.Email);
            MyDriver.MoveToElement(Program.settings.Login.RegisterPassword, act).Click().SendKeys(Program.settings.Login.Password);
            
            act.Build().Perform();
            MyDriver.MoveToElement(Program.settings.Login.SubscribeButton, act).Build().Perform();
            act.Click().Build().Perform();
            act.Pause(TimeSpan.FromSeconds(4));
            act.Build().Perform();

            // NOT TESTED FROM HERE
            //intro
            MyDriver.MoveToElement(Program.settings.Intro.VeteranPlayerSelector, act).Click();
            MyDriver.MoveToElement(Program.settings.Intro.ContinueButtonSelector, act).Click().Build().Perform();
            switch (Program.settings.Gameclass)
            {
                case AppSettings.GameclassEnum.explorateur:
                    MyDriver.MoveToElement(Program.settings.Intro.ExplorateurClassSelector, act).Click().Build().Perform();
                    break;
                case AppSettings.GameclassEnum.general:
                    MyDriver.MoveToElement(Program.settings.Intro.GeneraltClassSelector, act).Click().Build().Perform();
                    break;
                case AppSettings.GameclassEnum.collector:
                    MyDriver.MoveToElement(Program.settings.Intro.CollectorClassSelector, act).Click().Build().Perform();
                    break;
                default:
                    MyDriver.MoveToElement(Program.settings.Intro.CollectorClassSelector, act).Click().Build().Perform();
                    break;
            }
            // END NOT TESTED FROM HERE
        }
    }
}