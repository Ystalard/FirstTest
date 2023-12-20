using FirstTest.Handler;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace FirstTest
{
    public class SharedProperties
    {
        public Handler.Timer Timer { get; set; }
        public TimeSpan TimeToBuild { get; set; }
    }

    public abstract class Buildable: NavigationMenu
    {
        #region "constructor"
        public Buildable(Actions act, NavigationMenu.Menu menu, SharedProperties sharedProperties)
        {
            this.SharedProperties = sharedProperties;
            this.act = act;
            this.menu = menu;
            GoTo(menu, act);
            CheckDecompteTimer(menu);
        }

        public Buildable(Actions act, NavigationMenu.Menu menu)
        {
            Timer = new();
            this.act = act;
            this.menu = menu;
            GoTo(menu, act);
            CheckDecompteTimer(menu);
        }
        #endregion "constructor"

        #region "property"
        #region "private property"
        private Handler.Timer _timer;
        private TimeSpan _TimeToBuild;
        private Handler.Timer Timer 
        {
            get
            {
                if(SharedProperties != null){
                    return SharedProperties.Timer;
                }else{
                    return _timer;
                }
            }
            set
            {
                _timer = value;
                if(SharedProperties != null){
                    SharedProperties.Timer = value;
                }
            }
        }

        private SharedProperties SharedProperties = null;
        #endregion "private property"

        #region "protected property"
        protected TimeSpan TimeToBuild 
        {
            get
            {
                if(SharedProperties != null)
                {
                    return SharedProperties.TimeToBuild;
                }
                else
                {
                    return _TimeToBuild;
                }
            }
            set
            {
                _TimeToBuild = value;
                if(SharedProperties != null)
                {
                    
                    SharedProperties.TimeToBuild = value;
                }
            }
        }
        protected readonly Actions act;   
        
        protected Menu menu;
            
        protected TimeSpan? RemainingTime {
            get{
                if(Timer == null)
                {
                    return null;
                }
                if(Timer.CheckIsRunning)
                {
                    TimeSpan time = Timer.GetTimeSpan();
                
                    if(TimeToBuild <= time)
                    {
                        Timer.StopTimer(); // the construction of the resource ends
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
        public int MetalRequired()
        {
            return GetResourceRequired(Program.settings.Details.MetalRequired);
        }
        
        /// <summary>
        /// This method requires the details tab of the element to be opened.
        /// </summary>
        /// <returns>the cristal required</returns>
        public int CristalRequired()
        {
            return GetResourceRequired(Program.settings.Details.CristalRequired);
        }
        
        /// <summary>
        /// This method requires the details tab of the element to be opened.
        /// </summary>
        /// <returns>the deuterium required</returns>
        public int DeuteriumRequired()
        {
            return GetResourceRequired(Program.settings.Details.DeuteriumRequired);
        }
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
                Timer.StartTimer();
            }
        }

        /// <summary>
        /// Open the details tab on the element for which you want to check either you have the resources to build it or not.
        /// </summary>
        /// <param name="cssSelector">the css selector of the element to check</param>
        /// <returns>true if you have the resources, false otherwise</returns>
        public bool HaveResourceToBuild(string cssSelector)
        {
            OpenDetails(cssSelector);
        
            if(MetalRequired() <= GetCurrentMetal() && CristalRequired() <= GetCurrentCristal())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Inform about the availability of the builder.
        /// </summary>
        /// <returns>true: the builder can construct an element.
        /// false: an element is being built or the builder has not enough resources to build anything.</returns>
        public bool IsBusy()
        {
            TimeSpan? remainingTime = RemainingTime;
            if (remainingTime != null && Timer.CheckIsRunning)
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
                else if( cssSelector == Program.settings.Facilities.UsineRobot)
                {
                    if(!MyDriver.ElementExists(Program.settings.Facilities.DevelopUsineRobot))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopUsineRobot);
                }
                else if( cssSelector == Program.settings.Facilities.ChantierSpatial)
                {
                    if(!MyDriver.ElementExists(Program.settings.Facilities.DevelopChantierSpatial))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopChantierSpatial);
                }
                else if( cssSelector == Program.settings.Facilities.LaboRecherche)
                {
                    if(!MyDriver.ElementExists(Program.settings.Facilities.DevelopLaboRecherche))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopLaboRecherche);
                }
                else if( cssSelector == Program.settings.Facilities.DepotRavitaillement)
                {
                    if(!MyDriver.ElementExists(Program.settings.Facilities.DevelopDepotRavitaillement))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopDepotRavitaillement);
                }
                else if( cssSelector == Program.settings.Facilities.SiloMissible)
                {
                    if(!MyDriver.ElementExists(Program.settings.Facilities.DevelopSiloMissible))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopSiloMissible);
                }
                else if( cssSelector == Program.settings.Facilities.Nanites)
                {
                    if(!MyDriver.ElementExists(Program.settings.Facilities.DevelopNanites))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopNanites);
                }
                else if( cssSelector == Program.settings.Facilities.Terraformeur)
                {
                    if(!MyDriver.ElementExists(Program.settings.Facilities.DevelopTerraformeur))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopTerraformeur);
                }
                else if( cssSelector == Program.settings.Facilities.Docker)
                {
                    if(!MyDriver.ElementExists(Program.settings.Facilities.DevelopDocker))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopDocker);
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
            if(Timer.CheckIsRunning)
            {
                // the timer is already running. No need to check if it needs to be started.
                return;
            }
            GoTo(menu, act);

            // we want to be sure everything is loaded before checking the DecompteTempsDeConstruction element. 
            // It is important not to get wrong here as it would impact the timer process. Which is a tricky thing (timer is running on a different tread).
            act.Pause(TimeSpan.FromSeconds(Program.random.Next(1,2))).Build().Perform(); 

            if(MyDriver.ElementExists(Program.settings.Supplies.DecompteTempsDeConstruction))
            {
                IWebElement resourceInConstruction = MyDriver.FindElement(Program.settings.Supplies.DecompteTempsDeConstruction);
                TimeToBuild = Iso8601Duration.Parse(resourceInConstruction.GetAttribute("datetime"));
                Timer.StartTimer();
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
        protected TimeSpan GetTimeToBuild()
        {  
            return GetTimeToBuild(Program.settings.Details.TempsProduction);
        }

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
        /// Open the details tab of the element
        /// </summary>
        /// <param name="element">cssSelector of the element to check</param>
        protected void OpenDetails(string element)
        { 
            //when the details tab is opened there is a closebutton visible.
            if(MyDriver.ElementExists(Program.settings.Details.CloseButton))
            {
                //close the details as it needs to be refreshed, the wrong details might be opened.
                MyDriver.MoveToElement(Program.settings.Details.CloseButton, act).Click().Build().Perform();
                MyDriver.AssertElementDisappear(Program.settings.Details.CloseButton);
            }

            // open the details by clicking on the resource.
            MyDriver.MoveToElement(element, act).Click().Build().Perform();
            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();
            act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();

            if(!Details_opened(Program.settings.Details.CloseButton))
            {
                GoTo(Menu.Ressources, act);//re-load the resources page by clicking on the resources nav button.
                MyDriver.MoveToElement(element, act).Click().Build().Perform();// open the details by clicking on the resource.

                act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();

                if(!Details_opened(Program.settings.Details.CloseButton))
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
            else if ( cssSelectorToDevelop == Program.settings.Facilities.UsineRobot)
            {
                buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopUsineRobot);
            }
            else if ( cssSelectorToDevelop == Program.settings.Facilities.ChantierSpatial)
            {
                buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopChantierSpatial);
            }
            else if ( cssSelectorToDevelop == Program.settings.Facilities.LaboRecherche)
            {
                buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopLaboRecherche);
            }
            else if ( cssSelectorToDevelop == Program.settings.Facilities.DepotRavitaillement)
            {
                buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopDepotRavitaillement);
            }
            else if ( cssSelectorToDevelop == Program.settings.Facilities.SiloMissible)
            {
                buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopSiloMissible);
            }
            else if ( cssSelectorToDevelop == Program.settings.Facilities.Nanites)
            {
                buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopNanites);
            }
            else if ( cssSelectorToDevelop == Program.settings.Facilities.Terraformeur)
            {
                buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopTerraformeur);
            }
            else if ( cssSelectorToDevelop == Program.settings.Facilities.Docker)
            {
                buildElement = MyDriver.FindElement(Program.settings.Facilities.DevelopDocker);
            }
            else
            {
                throw new Handler.NotImplementedException();
            }

            MyDriver.MoveToElement(buildElement, act).Click().Build().Perform();
            
            Timer.StartTimer();
        }     

        public void DevelopResource(string resource, int number) {
            GoTo(menu, act);
            OpenDetails(resource);
            

            int numberToBuild = 0;
            int maximum = 0;
            if(resource == Program.settings.Supplies.SatelitteSolaire){
                maximum = int.Parse(MyDriver.FindElement(Program.settings.Supplies.TechnologyDetails.BuildAmount).GetAttribute("max"));
                numberToBuild = maximum > number ? number : maximum;
            }

            if(maximum == 0){
                WaitForResourcesAvailable(resource);
            }else{
                TimeToBuild = GetTimeToBuild(resource) * numberToBuild;
                MyDriver.MoveToElement(Program.settings.Supplies.TechnologyDetails.BuildAmount, act).Click().SendKeys(numberToBuild.ToString());
                MyDriver.MoveToElement(Program.settings.Supplies.TechnologyDetails.Develop, act).Click().Build().Perform();

               
                Timer.StartTimer();
            }
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