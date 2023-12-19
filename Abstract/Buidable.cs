using FirstTest.Handler;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace FirstTest
{
    public abstract class Buildable(Actions act, NavigationMenu.Menu menu) : NavigationMenu
    {
        #region "property"
        #region "public property"
            
        #endregion "public property"

        #region "private property"
        private readonly Handler.Timer timer = new();
        #endregion "private property"

        #region "protected property"
        protected TimeSpan TimeToBuild {get; set;}
        protected readonly Actions act = act;   
        
        protected Menu menu = menu;
            
        protected TimeSpan? RemainingTime {
            get{
                if(timer == null)
                {
                    return null;
                }
                if(timer.CheckIsRunning)
                {
                    TimeSpan time = timer.GetTimeSpan();
                
                    if(TimeToBuild <= time)
                    {
                        timer.StopTimer(); // the construction of the resource ends
                        TimeToBuild = TimeSpan.Zero;
                        return TimeSpan.Zero; // no remaining time to wait.
                    }

                    return TimeToBuild - time; // return the remaining time to wait.
                }
                
                // timer is not running so there is no construction in coming.
                return TimeSpan.Zero; // no remaining time to wait.          
                
            }
        }
        #endregion "protected property"
        #endregion "property"

        #region "method"
        #region "public method"
        #region "resources available"
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the current metal on the planet</returns>
        public static int GetCurrentMetal()
        {
            return GetCurrentResource(Program.settings.Resources.Metal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>the current cristal on the planet</returns>
        public static int GetCurrentCristal()
        {
            return GetCurrentResource(Program.settings.Resources.Cristal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>the current deuterium on the planet</returns>
        public static int GetCurrentDeut()
        {
            return GetCurrentResource(Program.settings.Resources.Deuterium);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>the current energie on the planet</returns>
        public static int GetCurrentEnergie()
        {
            return GetCurrentResource(Program.settings.Resources.Energie);
        }
        #endregion "resources available"

        #region "required resources"
        /// <summary>
        /// This method requires the details tab of the element to be opened.
        /// </summary>
        /// <returns>the metal required</returns>
        public abstract int MetalRequired();
        
        /// <summary>
        /// This method requires the details tab of the element to be opened.
        /// </summary>
        /// <returns>the cristal required</returns>
        public abstract int CristalRequired();
        
        /// <summary>
        /// This method requires the details tab of the element to be opened.
        /// </summary>
        /// <returns>the deuterium required</returns>
        public abstract int DeuteriumRequired();
        
        /// <summary>
        /// This method requires the details tab of the element to be opened.
        /// </summary>
        /// <returns>the energie required</returns>
        public abstract int EnergieRequired();
        #endregion "required resources"

        /// <summary>
        /// Initiate the remaining time to wait before developing an element.
        /// </summary>
        /// <param name="cssSelector">css selector of the element you aim to develop.</param>
        public virtual void WaitForResourcesAvailable(string cssSelector)
        {
            GoTo(menu, act);
            OpenDetails(cssSelector);

            int metal = GetCurrentMetal();
            int cristal = GetCurrentCristal();
            int deuterium = GetCurrentDeut();

            int metalRequired = MetalRequired();
            int cristalRequired = CristalRequired();
            int deuteriumRequired = DeuteriumRequired();

            int missing_metal = metalRequired < metal ? 0 : metalRequired - metal;
            int missing_cristal = cristalRequired < cristal ? 0 : cristalRequired - cristal;
            int missing_deuterium = deuteriumRequired < deuterium ? 0 : deuteriumRequired - deuterium;

            if(missing_metal + missing_cristal + missing_deuterium == 0)
            {
                // resources are available.
                return;
            }

            // collect production per hours
            int metalProduction = GetProductionPerHour(Program.settings.Resources.Metal);
            int cristalProduction = GetProductionPerHour(Program.settings.Resources.Cristal);
            int deuteriumProduction = GetProductionPerHour(Program.settings.Resources.Deuterium);

            TimeSpan timeToGetMetal = TimeSpan.FromHours((double)missing_metal / metalProduction);
            TimeSpan timeToGetCristal = TimeSpan.FromHours((double)missing_cristal / cristalProduction);
            TimeSpan timeToGetDeuterium = TimeSpan.FromHours((double)missing_deuterium / deuteriumProduction);

            TimeSpan timeToWait = timeToGetMetal < timeToGetCristal ? (timeToGetCristal < timeToGetDeuterium ? timeToGetDeuterium : timeToGetCristal) : timeToGetMetal < timeToGetDeuterium ? timeToGetDeuterium : timeToGetMetal; 

            if(timeToWait != TimeSpan.Zero)
            {
                TimeToBuild = timeToWait;
                timer.StartTimer();
            }
        }

        /// <summary>
        /// Open the details tab on the element for which you want to check either you have the resources to build it or not.
        /// </summary>
        /// <param name="cssSelector">the css selector of the element to check</param>
        /// <returns>true if you have the resources, false otherwise</returns>
        public abstract bool HaveResourceToBuild(string cssSelector);

        /// <summary>
        /// Inform about the availability of the builder.
        /// </summary>
        /// <returns>true: the builder can construct an element.
        /// false: an element is being built or the builder has not enough resources to build anything.</returns>
        public bool IsBusy()
        {
            TimeSpan? remainingTime = RemainingTime;
            if (remainingTime != null && timer.CheckIsRunning)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check the "develop" button exists. If it exist then the element can be built. This method can't work for enumerable element (a ship or a defense for instance)
        /// </summary>
        /// <param name="cssSelector">the css selector of the element to build</param>
        /// <returns></returns>
        /// <exception cref="Handler.NotImplementedException">the element is not implemented in the Buidable abstract class</exception>
        public bool CanBuildElement(string cssSelector)
        {
            if(IsBusy())
            {
                return false;
            } 

            GoTo(menu, act);

            try{
                IWebElement buildElement;
                if (cssSelector == Program.settings.Supplies.MineMetal)
                {
                    if(!MyDriver.ElementExists(Program.settings.Supplies.DevelopMetal))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopMetal);
                }
                else if (cssSelector == Program.settings.Supplies.MineCristal)
                {
                    if(!MyDriver.ElementExists(Program.settings.Supplies.DevelopCristal))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopCristal);
                }
                else if (cssSelector == Program.settings.Supplies.MineDeuterium)
                {
                    if(!MyDriver.ElementExists(Program.settings.Supplies.DevelopDeuterium))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopDeuterium);
                }
                else if (cssSelector == Program.settings.Supplies.CentraleSolaire)
                {
                    if(!MyDriver.ElementExists(Program.settings.Supplies.DevelopCentralSolaire))
                {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopCentralSolaire);
                }
                else
                {
                    throw new Handler.NotImplementedException();
                }

            }catch(WebDriverTimeoutException)
            {
                return false;
            }

            return true;   
        }
        
        #endregion "public method"

        #region "protected method"
        protected void CheckDecompteTimer(Menu menu)
        {
            GoTo(menu, act);

            // we want to be sure everything is loaded before checking the DecompteTempsDeConstruction element. 
            // It is important not to get wrong here as it would impact the timer process. Which is a tricky thing (timer is running on a different tread).
            act.Pause(TimeSpan.FromSeconds(Program.random.Next(1,2))).Build().Perform(); 

            if(MyDriver.ElementExists(Program.settings.Supplies.DecompteTempsDeConstruction))
            {
                IWebElement resourceInConstruction = MyDriver.FindElement(Program.settings.Supplies.DecompteTempsDeConstruction);
                TimeToBuild = Iso8601Duration.Parse(resourceInConstruction.GetAttribute("datetime"));
                timer.StartTimer();
            }
        }

        protected static int GetCurrentResource(string cssSelector)
        {
            return int.Parse(MyDriver.FindElement(cssSelector).GetAttribute("data-raw"));
        }
            
        protected static int GetCurrentLevel(string cssSelector)
        {
            IWebElement level = MyDriver.FindElement(cssSelector);
            return int.Parse(level.GetAttribute("data-value"));
        }
        
        protected static int GetResourceRequired(string cssSelector)
        {
            try{
                return int.Parse(MyDriver.FindElement(cssSelector).GetAttribute("data-value"));
            }catch(Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// This method must call GetTimeToBuild(string cssSelector).
        /// </summary>
        /// <param name="cssSelector"></param>
        /// <returns>The time a construction must take.</returns>
        protected abstract TimeSpan GetTimeToBuild();

        /// <summary>
        ///  
        /// </summary>
        /// <param name="cssSelector">the css selector of the time of production of an element.</param>
        /// <returns>The time a construction must take.</returns>
        protected TimeSpan GetTimeToBuild(string cssSelector)
        {
            return Iso8601Duration.Parse(MyDriver.FindElement(cssSelector).GetAttribute("datetime"));
        }

        /// <summary>
        /// Get the production per hour of the resource
        /// </summary>
        /// <param name="cssSelector">css selector of the resource</param>
        /// <returns></returns>
        protected int GetProductionPerHour(string cssSelector)
        {
            MyDriver.MoveToElement(cssSelector, act).Build().Perform(); // hover the ressource
            act.Pause(TimeSpan.FromSeconds(2)).Build().Perform(); // wait for js to display the tooltip
            IWebElement tooltip = MyDriver.FindElement(Program.settings.ProductionTooltip); // access tooltip
            return int.Parse(tooltip.Text.TrimStart('+'));
        }

        /// <summary>
        /// In the subclass, this method must call OpenDetails(string element, string closeButton) and define the relevant element and closebutton css selectors.
        /// </summary>
        /// <param name="element"></param>
        protected abstract void OpenDetails(string element);

        /// <summary>
        /// Open the details tab of the element
        /// </summary>
        /// <param name="element">css selector of the element</param>
        /// <param name="closeButton">css selector of the close button which closes the details tab</param>
        protected void OpenDetails(string element, string closeButton)
        {
            //when the details tab is opened there is a closebutton visible.
            if(MyDriver.ElementExists(Program.settings.Supplies.TechnologyDetails.CloseButton))
            {
                //close the details as it needs to be refreshed, the wrong details might be opened.
                MyDriver.MoveToElement(Program.settings.Supplies.TechnologyDetails.CloseButton, act).Click().Build().Perform();
                MyDriver.AssertElementDisappear(Program.settings.Supplies.TechnologyDetails.CloseButton);
            }

            // open the details by clicking on the resource.
            MyDriver.MoveToElement(element, act).Click().Build().Perform();
            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();
            act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();

            if(!Details_opened(closeButton))
            {
                GoTo(Menu.Ressources, act);//re-load the resources page by clicking on the resources nav button.
                MyDriver.MoveToElement(element, act).Click().Build().Perform();// open the details by clicking on the resource.

                act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();

                if(!Details_opened(closeButton))
                {
                    throw new MustRestartException();
                }
            }

            if(!Details_opened_on(element))
            {
                throw new MustRestartException();
            }
        }

        /// <summary>
        /// Must click on the button which would develop the element chosen.
        /// </summary>
        /// <param name="cssSelectorToDevelop">css selector of the element to develop.</param>
        protected void Develop(string cssSelectorToDevelop)
        {
            GoTo(menu, act);
            OpenDetails(cssSelectorToDevelop);
            TimeToBuild = GetTimeToBuild();
            
            IWebElement buildElement;
            if (cssSelectorToDevelop == Program.settings.Supplies.MineMetal)
            {
                buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopMetal);
            }
            else if (cssSelectorToDevelop == Program.settings.Supplies.MineCristal)
            {
                buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopCristal);
            }
            else if (cssSelectorToDevelop == Program.settings.Supplies.MineDeuterium)
            {
                buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopDeuterium);
            }
            else if (cssSelectorToDevelop == Program.settings.Supplies.CentraleSolaire)
            {
                buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopCentralSolaire);
            }
            else
            {
                throw new Handler.NotImplementedException();
            }

            MyDriver.MoveToElement(buildElement, act).Click().Build().Perform();
            
            timer.StartTimer();
        }            
        #endregion "protected method"
        
        #region "private method"
        private static bool Details_opened(string cssSelector)
        {
            return MyDriver.FindElement(cssSelector) != null;
        }        

        private static bool Details_opened_on(string element_to_check)
        {
            return MyDriver.CheckElementContains(cssSelector: element_to_check, attribute: "class", content: "showsDetails");
        }
        #endregion
        #endregion
    }
}