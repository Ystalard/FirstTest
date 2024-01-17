using FirstTest.Handler;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace FirstTest
{
    public class SharedProperties
    {
        public Handler.Timer Timer { get; set; }
    }

    public interface IBuildable
{
    bool CanBuildElement(string cssSelector, bool force = false);
    int CristalRequired();
    int DeuteriumRequired();
    bool HaveResourceToBuild(string cssSelector);
    bool IsBusy();
    int MetalRequired();
    void WaitForResourcesAvailable(string cssSelector);
}

    public abstract class Buildable: NavigationMenu
    {
        #region "constructor"
        public Buildable(Actions act, NavigationMenu.Menu menu, SharedProperties sharedProperties, IPlanet planet)
        {
            this.SharedProperties = sharedProperties;
            this.act = act;
            this.menu = menu;
            this.planet = planet;
            GoTo(menu, act);
            CheckDecompteTimer(menu);
        }

        public Buildable(Actions act, NavigationMenu.Menu menu, IPlanet planet)
        {
            Timer = new();
            this.act = act;
            this.menu = menu;
            this.planet = planet;
            GoTo(menu, act);
            CheckDecompteTimer(menu);
        }
        #endregion "constructor"

        #region "property"
        #region "private property"
        private Handler.Timer _timer;
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
        protected readonly Actions act;   
        
        protected Menu menu;
        protected IPlanet planet;    
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
            OpenDetails(cssSelector, menu, act);

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
                Timer.StartTimer(timeToWait);
            }
        }

        /// <summary>
        /// Open the details tab on the element for which you want to check either you have the resources to build it or not.
        /// </summary>
        /// <param name="cssSelector">the css selector of the element to check</param>
        /// <returns>true if you have the resources, false otherwise</returns>
        public bool HaveResourceToBuild(string cssSelector)
        {
            OpenDetails(cssSelector, menu, act);
        
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
        public virtual bool IsBusy()
        {
            if (Timer.IsRunning())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check the element status is valid. When force is true then the timer is not checked as the element can be built even if something is already in construction.
        /// </summary>
        /// <param name="cssSelector">the css selector of the element to build</param>
        /// <param name="force">force the validation according to 'on' and 'active' status</param>
        /// <returns></returns>
        /// <exception cref="Handler.NotImplementedException">the element is not implemented in the Buidable abstract class</exception>
        public bool CanBuildElement(string cssSelector, bool force = false)
        {
            if(!force && IsBusy())
            {
                return false;
            } 

            GoTo(menu, act);

            try{
                string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                return  status == "on" || force && status == "active";
            }catch(WebDriverTimeoutException)
            {
                return false;
            }  
        }
        
        #endregion "public method"

        #region "protected method"
        protected IPlanet GetPlanet()
        {
            return planet;
        }

        protected void CheckDecompteTimer(Menu menu)
        {
            if(Timer.IsRunning())
            {
                // the timer is already running. No need to check if it needs to be started.
                return;
            }
            GoTo(menu, act);

            // we want to be sure everything is loaded before checking the DecompteTempsDeConstruction element. 
            // It is important not to get wrong here as it would impact the timer process. Which is a tricky thing (timer is running on a different tread).
            act.Pause(TimeSpan.FromSeconds(Program.random.Next(1,2))).Build().Perform(); 
            
            switch (menu)
            {
                case Menu.Ressources:
                    if(MyDriver.ElementExists(Program.settings.Supplies.DecompteTempsDeConstruction))
                    {
                        IWebElement resourceInConstruction = MyDriver.FindElement(Program.settings.Supplies.DecompteTempsDeConstruction);
                        Timer.StartTimer(Iso8601Duration.Parse(resourceInConstruction.GetAttribute("datetime")));
                    }
                break;

                case Menu.Installations:
                    if(MyDriver.ElementExists(Program.settings.Facilities.DecompteTempsDeConstruction))
                    {
                        IWebElement resourceInConstruction = MyDriver.FindElement(Program.settings.Facilities.DecompteTempsDeConstruction);
                        Timer.StartTimer(Iso8601Duration.Parse(resourceInConstruction.GetAttribute("datetime")));
                    }
                break;

                case Menu.Recherche:
                    if(MyDriver.ElementExists(Program.settings.Recherche.DecompteTempsDeConstruction))
                    {
                        IWebElement resourceInConstruction = MyDriver.FindElement(Program.settings.Recherche.DecompteTempsDeConstruction);
                        Timer.StartTimer(Iso8601Duration.Parse(resourceInConstruction.GetAttribute("datetime")));
                    }
                break;

                case Menu.ChantierSpatial:
                    if(MyDriver.ElementExists(Program.settings.ChantierSpatial.DecompteTempsDeConstruction))
                    {
                        IWebElement resourceInConstruction = MyDriver.FindElement(Program.settings.ChantierSpatial.DecompteTempsDeConstruction);
                        Timer.StartTimer(LiteralDuration.Parse(resourceInConstruction.Text));
                    }
                break;
                case Menu.Defense:
                    if(MyDriver.ElementExists(Program.settings.Defense.DecompteTempsDeConstruction))
                    {
                        IWebElement resourceInConstruction = MyDriver.FindElement(Program.settings.Defense.DecompteTempsDeConstruction);
                        Timer.StartTimer(LiteralDuration.Parse(resourceInConstruction.Text));
                    }
                break;

                default:
                break;
            }
        }

        protected static int GetCurrentResource(string cssSelector)
        {
            return int.Parse(MyDriver.FindElement(cssSelector).GetAttribute("data-raw"));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cssSelector">selector of the level element</param>
        /// <returns></returns>
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
        /// The details tab on the element to check the time of production must be opened
        /// </summary>
        /// <returns>The time a construction must take.</returns>
        protected TimeSpan GetTimeToBuild()
        {  
            return Iso8601Duration.Parse(MyDriver.FindElement(Program.settings.Details.TempsProduction).GetAttribute("datetime"));
        }

        protected int GetMetalProductionPerHour()
        {
            return GetProductionPerHour(Program.settings.Resources.Metal);
        }

        protected int GetCristalProductionPerHour()
        {
            return GetProductionPerHour(Program.settings.Resources.Cristal);
        }
        protected int GetDeuteriumProductionPerHour()
        {
            return GetProductionPerHour(Program.settings.Resources.Deuterium);
        }

        /// <summary>
        /// Must click on the button which would develop the element chosen.
        /// </summary>
        /// <param name="cssSelectorToDevelop">css selector of the develop button of the element.</param>
        protected void Develop(string cssSelectorToDevelop)
        {            
            IWebElement buildElement = MyDriver.FindElement(cssSelectorToDevelop);

            MyDriver.MoveToElement(buildElement, act).Click().Build().Perform();
            
            CheckDecompteTimer(menu);
        }     

        protected void Develop(string element, int number) {
            OpenDetails(element, menu, act);
            
            int maximum = int.Parse(MyDriver.FindElement(Program.settings.Details.BuildAmount).GetAttribute("max"));
            int numberToBuild = maximum > number ? number : maximum;
            
            if(maximum == 0)
            {
                WaitForResourcesAvailable(element);
            }else{
                TimeSpan timeToBuild = GetTimeToBuild() * numberToBuild;
                MyDriver.MoveToElement(Program.settings.Supplies.TechnologyDetails.BuildAmount, act).Click().SendKeys(numberToBuild.ToString());
                MyDriver.MoveToElement(Program.settings.Supplies.TechnologyDetails.Develop, act).Click().Build().Perform();

                Timer.StartTimer(timeToBuild);
            }
        }       
        #endregion "protected method"
        
        #region "private method"
        /// <summary>
        /// Get the production per hour of the resource
        /// </summary>
        /// <param name="cssSelector">css selector of the resource</param>
        /// <returns></returns>
        private int GetProductionPerHour(string cssSelector)
        {
            MyDriver.MoveToElement(cssSelector, act).Build().Perform(); // hover the ressource
            act.Pause(TimeSpan.FromSeconds(2)).Build().Perform(); // wait for js to display the tooltip
            IWebElement tooltip = MyDriver.FindElement(Program.settings.ProductionTooltip); // access tooltip
            return int.Parse(tooltip.Text.TrimStart('+'));
        }
        #endregion
        #endregion
    }
}