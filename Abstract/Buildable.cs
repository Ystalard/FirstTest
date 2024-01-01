using FirstTest.Handler;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace FirstTest
{
    public class SharedProperties
    {
        public Handler.Timer Timer { get; set; }
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
        public virtual bool IsBusy()
        {
            if (Timer.IsRunning())
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
                else if (cssSelector == Program.settings.Recherche.TechnoEnergie)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoEnergie))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoEnergie);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoLaser)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoLaser))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoLaser);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoIons)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoIons))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoIons);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoHyperespace)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoHyperespace))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoHyperespace);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoPlasma)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoPlasma))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoPlasma);
                }
                else if (cssSelector == Program.settings.Recherche.ReacteurCombustion)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopReacteurCombustion))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopReacteurCombustion);
                }
                else if (cssSelector == Program.settings.Recherche.ReacteurImpulsion)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopReacteurImpulsion))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopReacteurImpulsion);
                }
                else if (cssSelector == Program.settings.Recherche.PropulsionHyperespace)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopPropulsionHyperespace))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopPropulsionHyperespace);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoEspionnage)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoEspionnage))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoEspionnage);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoOrdinateur)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoOrdinateur))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoOrdinateur);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoAstro)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoAstro))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoAstro);
                }
                else if (cssSelector == Program.settings.Recherche.ReseauRecherche)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopReseauRecherche))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopReseauRecherche);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoGraviton)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoGraviton))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoGraviton);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoArme)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoArme))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoArme);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoBouclier)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoBouclier))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoBouclier);
                }
                else if (cssSelector == Program.settings.Recherche.TechnoProtectionVaisseaux)
                {
                    if(!MyDriver.ElementExists(Program.settings.Recherche.DevelopTechnoProtectionVaisseaux))
                    {
                        return false;
                    }
                    buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoProtectionVaisseaux);
                }
                else if (cssSelector == Program.settings.ChantierSpatial.ChasseurLeger)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.ChasseurLourd)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.Croiseur)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.VaisseauBataille)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.Traqueur)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.Bombardier)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.Destructeur)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.Edm)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.Faucheur)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.Eclaireur)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.PetitTransporteur)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.GrandTransporteur)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.VaisseauColonisation)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.Recycleur)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if (cssSelector == Program.settings.ChantierSpatial.SondeEspionnage)
                {
                    return MyDriver.FindElement(cssSelector).GetAttribute("data-status") == "on";
                }
                else if(cssSelector == Program.settings.Defense.LanceurMissile)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.LaserLeger)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.LaserLourd)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.CanonDeGausse)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.ArtillerieIon)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.LanceurPlasma)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.PetitBouclier)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.GrandBouclier)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.MisileInterception)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
                }
                else if(cssSelector == Program.settings.Defense.MissileInterplanetaire)
                {
                    string status = MyDriver.FindElement(cssSelector).GetAttribute("data-status");
                    return  status == "on" || status == "active";
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
                act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();
            }

            // open the details by clicking on the resource.
            MyDriver.MoveToElement(element, act).Click().Build().Perform();
            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();
            act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();

            if(!Details_opened(Program.settings.Details.CloseButton))
            {
                GoTo(menu, act);//re-load the resources page by clicking on the resources nav button.
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
            TimeSpan timeToBuild = GetTimeToBuild();
            
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
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoEnergie)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoEnergie);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoLaser)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoLaser);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoIons)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoIons);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoHyperespace)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoHyperespace);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoPlasma)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoPlasma);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.ReacteurCombustion)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopReacteurCombustion);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.ReacteurImpulsion)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopReacteurImpulsion);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.PropulsionHyperespace)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopPropulsionHyperespace);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoEspionnage)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoEspionnage);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoOrdinateur)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoOrdinateur);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoAstro)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoAstro);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.ReseauRecherche)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopReseauRecherche);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoGraviton)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoGraviton);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoArme)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoArme);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoBouclier)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoBouclier);
            }
            else if ( cssSelectorToDevelop == Program.settings.Recherche.TechnoProtectionVaisseaux)
            {
                buildElement = MyDriver.FindElement(Program.settings.Recherche.DevelopTechnoProtectionVaisseaux);
            }
            else
            {
                throw new Handler.NotImplementedException();
            }

            MyDriver.MoveToElement(buildElement, act).Click().Build().Perform();
            
            Timer.StartTimer(timeToBuild);
        }     

        public void Develop(string element, int number) {
            GoTo(menu, act);
            OpenDetails(element);
            
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