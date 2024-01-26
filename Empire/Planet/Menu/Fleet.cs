using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FirstTest.Handler;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace FirstTest
{
    public interface IFleet
    {
        public int GetChasseurLegerAvailable { get;}
        public int GetChasseurLourdAvailable { get;}
        public int GetCroiseurAvailable { get;}
        public int GetVaisseauBatailleAvailable { get;}
        public int GetTraqueurAvailable { get;}
        public int GetBombardierAvailable { get;}
        public int GetDestructeurAvailable { get;}
        public int GetEdmAvailable { get;}
        public int GetFaucheurAvailable { get;}
        public int GetEclaireurAvailable { get;}
        public int GetPetitTransporteurAvailable { get;}
        public int GetGrandTransporteurAvailable { get;}
        public int GetVaisseauColonisationAvailable { get;}
        public int GetRecycleurAvailable { get;}
        public int GetSondeEspionnageAvailable { get;}
        public int GetMetalLoad { get;}
        public int GetCrystalLoad { get;}
        public int GetDeuteriumLoad { get;}
        public int GetFoodLoad { get;}
        public enum TargetStatus;
        public void AddChasseurLeger(int count);
        public void AddChasseurLourd(int count);
        public void AddCroiseur(int count);
        public void AddVaisseauBataille(int count);
        public void AddTraqueur(int count);
        public void AddBombardier(int count);
        public void AddDestructeur(int count);
        public void AddEdm(int count);
        public void AddFaucheur(int count);
        public void AddEclaireur(int count);
        public void AddPetitTransporteur(int count);
        public void AddGrandTransporteur(int count);
        public void AddVaisseauColonisation(int count);
        public void AddRecycleur(int count);
        public void AddSondeEspionnage(int count);
        public void Continue();
        public void SelectExpedition();
        public bool IsMissionExpeditionAvailable();
        public void SelectColonize();
        public bool IsMissionColonizeAvailable();
        public void SelectRecycle();
        public bool IsMissionRecycleAvailable();
        public void SelectTransport();
        public bool IsMissionTransportAvailable();
        public void SelectPark();
        public bool IsMissionParkAvailable();
        public void SelectSpy();
        public void SelectParkOutSideEmpire();
        public bool IsMissionParkOutSideEmpireAvailable();
        public void SelectAttack();
        public bool IsMissionAttackAvailable();
        public void SelectGroupAttack();
        public bool IsMissionGroupAttackAvailable();
        public void SelectDestroyMoon();
        public bool IsMissionDestroyMoonAvailable();
        public Fleet.TargetStatus GetStatus();
        public void TargetPlanet(int galaxy, int system, int position);
        public DateTime GetArrivalTime();
        public DateTime GetReturnTime();
        public TimeSpan GetExpectedDuration();
        public void LoadMetal(int count);
        public void LoadCrystal(int count);
        public void LoadDeuterium(int count);
        public void LoadFood(int count);
        public void LoadAllResources();
        public int GetRemainingResources();
        public void selectStep(int step);
        public void SendFleet();
    }
    public class Fleet : NavigationMenu, IFleet
    {
        #region constructor
        public Fleet(Actions act, IPlanet planetFrom, Delegate.MovingFleetEventHandler addMovingFleet)
        {
            this.act = act;
            this.planetFrom = planetFrom;
            AddMovingFleet = addMovingFleet;
            GoTo(menu, act);
        }
        #endregion constructor

        #region private properties
        private IPlanet planetFrom;
        private Menu menu = Menu.Flotte;
        private readonly Actions act; 
        private int ChasseurLeger;
        private int ChasseurLourd;
        private int Croiseur;
        private int VaisseauBataille;
        private int Traqueur;
        private int Bombardier;
        private int Destructeur;
        private int Edm;
        private int Faucheur;
        private int Eclaireur;
        private int PetitTransporteur;
        private int GrandTransporteur;
        private int VaisseauColonisation;
        private int Recycleur;
        private int SondeEspionnage  ;

        private int MetalLoad;
        private int CrystalLoad;
        private int DeuteriumLoad;
        private int FoodLoad;
        #endregion private properties

        #region enum
        public class ScanOptions
        {
            public class Range
            {
                public int min;
                public int max; 
            }

            [Flags]
            public enum InRange
            {
                galaxy,
                solarSystem_of_the_planet_which_scans,
                position,
                nearest,
                all
            }

            public InRange inRange;
            public List<Range> range;
        }

        #endregion enum
        #region public properties
        public event Delegate.MovingFleetEventHandler AddMovingFleet;
        public int GetChasseurLegerAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.ChasseurLeger);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.CssSelector("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetChasseurLourdAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.ChasseurLourd);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetCroiseurAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.Croiseur);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetVaisseauBatailleAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.VaisseauBataille);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetTraqueurAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.Traqueur);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetBombardierAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.Bombardier);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetDestructeurAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.Destructeur);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetEdmAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.Edm);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetFaucheurAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.Faucheur);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetEclaireurAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.Eclaireur);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetPetitTransporteurAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.PetitTransporteur);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetGrandTransporteurAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.GrandTransporteur);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetVaisseauColonisationAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.VaisseauColonisation);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetRecycleurAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.Recycleur);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetSondeEspionnageAvailable
        {
            get
            {
                IWebElement inputElement = MyDriver.FindElement(Program.settings.Fleet.SondeEspionnage);
                IWebElement parent = inputElement.FindElement(By.XPath("./.."));
                return int.Parse(parent.FindElement(By.TagName("span > span")).GetAttribute("data-value"));
            }
        }
        
        public int GetMetalLoad { get => MetalLoad;}
        public int GetCrystalLoad { get => CrystalLoad;}
        public int GetDeuteriumLoad { get => DeuteriumLoad;}
        public int GetFoodLoad { get => FoodLoad;}
        #endregion public properties

        #region enum
        public enum TargetStatus
        {
            common,
            noob,
            strong,
            inactive
        }
        #endregion

        #region public method
        #region Add ship to the fleet
        public void AddChasseurLeger(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.ChasseurLeger, act).Click().SendKeys(count.ToString());
        }
        public void AddChasseurLourd(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.ChasseurLourd, act).Click().SendKeys(count.ToString());
        }
        public void AddCroiseur(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Croiseur, act).Click().SendKeys(count.ToString());
        }
        public void AddVaisseauBataille(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.VaisseauBataille, act).Click().SendKeys(count.ToString());
        }
        public void AddTraqueur(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Traqueur, act).Click().SendKeys(count.ToString());
        }
        public void AddBombardier(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Bombardier, act).Click().SendKeys(count.ToString());
        }
        public void AddDestructeur(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Destructeur, act).Click().SendKeys(count.ToString());
        }
        public void AddEdm(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Edm, act).Click().SendKeys(count.ToString());
        }
        public void AddFaucheur(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Faucheur, act).Click().SendKeys(count.ToString());
        }
        public void AddEclaireur(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Eclaireur, act).Click().SendKeys(count.ToString());
        }
        public void AddPetitTransporteur(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.PetitTransporteur, act).Click().SendKeys(count.ToString());
        }
        public void AddGrandTransporteur(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.GrandTransporteur, act).Click().SendKeys(count.ToString());
        }
        public void AddVaisseauColonisation(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.VaisseauColonisation, act).Click().SendKeys(count.ToString());
        }
        public void AddRecycleur(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Recycleur, act).Click().SendKeys(count.ToString());
        }
        public void AddSondeEspionnage(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.SondeEspionnage, act).Click().SendKeys(count.ToString());
        }
        #endregion Add ship to the fleet

        public void Continue()
        {
            act.Build().Perform();
            
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.ChasseurLeger).GetAttribute("value"), out ChasseurLeger);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.ChasseurLourd).GetAttribute("value"), out ChasseurLourd);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.Croiseur).GetAttribute("value"), out Croiseur);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.VaisseauBataille).GetAttribute("value"), out VaisseauBataille);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.Traqueur).GetAttribute("value"), out Traqueur);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.Bombardier).GetAttribute("value"), out Bombardier);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.Destructeur).GetAttribute("value"), out Destructeur);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.Edm).GetAttribute("value"), out Edm);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.Faucheur).GetAttribute("value"), out Faucheur);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.Eclaireur).GetAttribute("value"), out Eclaireur);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.PetitTransporteur).GetAttribute("value"), out PetitTransporteur);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.GrandTransporteur).GetAttribute("value"), out GrandTransporteur);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.VaisseauColonisation).GetAttribute("value"), out VaisseauColonisation);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.Recycleur).GetAttribute("value"), out Recycleur);
            int.TryParse(MyDriver.FindElement(Program.settings.Fleet.SondeEspionnage).GetAttribute("value"), out SondeEspionnage);
            MyDriver.MoveToElement(Program.settings.Fleet.NextButton, act).Click().Build().Perform();
        }

        private void WaitLoadingEnd()
        {
            bool isLoading = true;
            act.Pause(TimeSpan.FromMilliseconds(Program.random.Next(500,550))).Build().Perform(); 
            while(isLoading)
            {
                if(MyDriver.ElementExists(Program.settings.Fleet.Loading))
                {
                    IWebElement loadElement = MyDriver.FindElement(Program.settings.Fleet.Loading);
                    if(!loadElement.GetCssValue("display").Contains("none"))
                    {
                        isLoading = false;
                    }
                }
                else
                {
                    isLoading = false;
                }
            }
        }

        #region select type of mission
        public void SelectExpedition()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.Expedition, act).Click().Build().Perform();
        }

        public bool IsMissionExpeditionAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.Expedition);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectColonize()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.Colonize, act).Click().Build().Perform();
        }

        public bool IsMissionColonizeAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.Colonize);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectRecycle()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.Recycle, act).Click().Build().Perform();
        }

        public bool IsMissionRecycleAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.Recycle);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectTransport()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.Transport, act).Click().Build().Perform();
        }

        public bool IsMissionTransportAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.Transport);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectPark()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.Park, act).Click().Build().Perform();
        }

        public bool IsMissionParkAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.Park);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectSpy()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.Spy, act).Click().Build().Perform();
        }

        public bool IsMissionSpyAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.Spy);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectParkOutSideEmpire()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.ParkOutSideEmpire, act).Click().Build().Perform();
        }

        public bool IsMissionParkOutSideEmpireAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.ParkOutSideEmpire);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectAttack()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.Attack, act).Click().Build().Perform();
        }

        public bool IsMissionAttackAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.Attack);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectGroupAttack()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.GroupAttack, act).Click().Build().Perform();
        }

        public bool IsMissionGroupAttackAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.GroupAttack);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void SelectDestroyMoon()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.Mission.DestroyMoon, act).Click().Build().Perform();
        }

        public bool IsMissionDestroyMoonAvailable()
        {
            IWebElement mission = MyDriver.FindElement(Program.settings.Fleet.Mission.DestroyMoon);
            IWebElement parent = mission.FindElement(By.XPath("./.."));
            if (parent.GetAttribute("class").Contains("on"))
            {
                return true;
            }
            else{
                return false;
            }
        }
        #endregion select type of mission

        public TargetStatus GetStatus()
        {
            IWebElement player = MyDriver.FindElement(Program.settings.Fleet.To.PlayerName);
            IWebElement spanChild = player.FindElement(By.TagName("span"));
            if (spanChild == null)
            {
                return TargetStatus.common;
            }
            else
            {
                if(spanChild.GetAttribute("class").Contains("status_abbr_strong"))
                {
                    return TargetStatus.strong;
                }
                else if(spanChild.GetAttribute("class").Contains("status_abbr_inactive") || spanChild.GetAttribute("class").Contains("status_abbr_longinactive"))
                {
                    return TargetStatus.inactive;
                }
                else if(spanChild.GetAttribute("class").Contains("status_abbr_noob"))
                {
                    return TargetStatus.noob;
                }
                else
                {
                    return TargetStatus.common;
                }
            }
        }

        public void TargetPlanet(int galaxy, int system, int position)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.To.Galaxy, act).Click().SendKeys(galaxy.ToString());
            MyDriver.MoveToElement(Program.settings.Fleet.To.System, act).Click().SendKeys(system.ToString());
            MyDriver.MoveToElement(Program.settings.Fleet.To.Position, act).Click().SendKeys(position.ToString());
            act.Build().Perform();
            WaitLoadingEnd();
        }

        public DateTime GetArrivalTime()
        {
            return DateTime.ParseExact(MyDriver.FindElement(Program.settings.Fleet.Time.ArrivalTime).Text,"dd.MM.yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }

        public DateTime GetReturnTime()
        {
            return DateTime.ParseExact(MyDriver.FindElement(Program.settings.Fleet.Time.ReturnTime).Text,"dd.MM.yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }

        public TimeSpan GetExpectedDuration()
        {
            DateTime returnTime = DateTime.ParseExact(MyDriver.FindElement(Program.settings.Fleet.Time.ReturnTime).Text,"dd.MM.yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            return returnTime - DateTime.Now;
        }

        #region resources to load
        public void LoadMetal(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.ResourcesToLoad.Metal, act).Click().SendKeys(count.ToString()).Build().Perform();
        }
        public void LoadCrystal(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.ResourcesToLoad.Crystal, act).Click().SendKeys(count.ToString()).Build().Perform();
        }
        public void LoadDeuterium(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.ResourcesToLoad.Deuterium, act).Click().SendKeys(count.ToString()).Build().Perform();
        }
        public void LoadFood(int count)
        {
            MyDriver.MoveToElement(Program.settings.Fleet.ResourcesToLoad.Food, act).Click().SendKeys(count.ToString()).Build().Perform();
        }
        
        public void LoadAllResources()
        {
            MyDriver.MoveToElement(Program.settings.Fleet.ResourcesToLoad.AllResources, act).Click().Build().Perform();
        }

        public int GetRemainingResources()
        {
            return int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.RemainingResources).Text);
        }
        public int GetMaxResources(string cssSelector)
        {
            return int.Parse(MyDriver.FindElement(cssSelector).Text);
        }
        #endregion resources to load

        /// <summary>
        /// between 1 and 10 steps which represents the speed of the fleet. 
        /// </summary>
        /// <param name="step"></param>
        public void selectStep(int step)
        {
            ReadOnlyCollection<IWebElement> steps = Program._driver.FindElement(By.CssSelector(Program.settings.Fleet.Time.Steps)).FindElements(By.ClassName("step2"));
            
            switch (step)
            {
                case 1:
                    MyDriver.MoveToElement(steps[0], act).Click().Build().Perform();
                break;
                case 2:
                    MyDriver.MoveToElement(steps[1], act).Click().Build().Perform();
                break;
                case 3:
                    MyDriver.MoveToElement(steps[2], act).Click().Build().Perform();
                break;
                case 4:
                    MyDriver.MoveToElement(steps[3], act).Click().Build().Perform();
                break;
                case 5:
                    MyDriver.MoveToElement(steps[4], act).Click().Build().Perform();
                break;
                case 6:
                    MyDriver.MoveToElement(steps[5], act).Click().Build().Perform();
                break;
                case 7:
                    MyDriver.MoveToElement(steps[6], act).Click().Build().Perform();
                break;
                case 8:
                    MyDriver.MoveToElement(steps[7], act).Click().Build().Perform();
                break;
                case 9:
                    MyDriver.MoveToElement(steps[8], act).Click().Build().Perform();
                break;
                case 10:
                    MyDriver.MoveToElement(steps[9], act).Click().Build().Perform();
                break;
            }
        }

#region scan
        public List<Coordinates> ScanGalaxyInRange(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            List<Coordinates> validCoordinates = new();
            bool returnNearest = (scanOptions.inRange & ScanOptions.InRange.nearest) == ScanOptions.InRange.nearest;

            for (int galaxy = scanOptions.range.First().min; galaxy <= scanOptions.range.First().max; galaxy++)
            {
                for (int solarSystem_offset = 0; solarSystem_offset < int.Parse(Program.settings.Univers.SolarSystemCount); solarSystem_offset++)
                {
                    bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= int.Parse(Program.settings.Univers.SolarSystemCount);
                    bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset > 0;
                    
                    if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                    {
                        // scan of the current galaxy ends
                        break;
                    }

                    for (int position_offset = 0; position_offset <= 15; position_offset++)
                    {
                        bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= 16;
                        if (solar_system_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            if(ScanPrerogative.Invoke())
                            {
                                if(returnNearest && validCoordinates.Count > 0)
                                {
                                    return validCoordinates;    
                                }

                                validCoordinates.Add(new Coordinates(galaxyFrom + galaxy, solarSystemFrom + solarSystem_offset, positionFrom + position_offset));
                            }
                        }

                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset > 0;
                        if (solar_system_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset)
                        {
                            TargetPlanet(galaxyFrom - galaxy, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                            if(ScanPrerogative.Invoke())
                            {   
                                if(returnNearest)
                                {
                                    validCoordinates.Add(new Coordinates(galaxyFrom - galaxy, solarSystemFrom - solarSystem_offset, positionFrom - position_offset));
                                    return validCoordinates;
                                }
                                else
                                {
                                    validCoordinates.Add(new Coordinates(galaxyFrom - galaxy, solarSystemFrom - solarSystem_offset, positionFrom - position_offset));
                                }
                            }
                        }

                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }
                }
            }

            return validCoordinates;
        }
        public List<Coordinates> ScanSolarSyst                                                                                                                   emInRange(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            List<Coordinates> result = new();
            bool returnNearest = (scanOptions.inRange & ScanOptions.InRange.nearest) == ScanOptions.InRange.nearest;
            
            int galaxy_offset_limit = (scanOptions.inRange & ScanOptions.InRange.solarSystem_of_the_planet_which_scans)
                                      == ScanOptions.InRange.solarSystem_of_the_planet_which_scans ? 
                                      1 : int.Parse(Program.settings.Univers.GalaxyCount);
            
            for (int galaxy_offset = 0; galaxy_offset < galaxy_offset_limit; galaxy_offset++)
            {
                bool galaxies_being_scanned_on_positive_offset = galaxyFrom + galaxy_offset <= int.Parse(Program.settings.Univers.GalaxyCount);
                bool galaxies_being_scanned_on_negative_offset = galaxyFrom - galaxy_offset > 0;

                if (!galaxies_being_scanned_on_positive_offset && !galaxies_being_scanned_on_negative_offset)
                {
                    // scan of the univers ends.
                    break;
                }

                for (int solarSytem = scanOptions.range.First().min; solarSytem <= scanOptions.range.First().max; solarSytem++)
                {
                    for (int position_offset = 0; position_offset <= 15; position_offset++)
                    {
                        bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= 16;
                        if (galaxies_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy_offset, solarSytem, positionFrom + position_offset);
                            if(ScanPrerogative.Invoke())
                            {
                                if(returnNearest && result.Count > 0)
                                {
                                    return result;    
                                }

                                result.Add(new Coordinates(galaxyFrom + galaxy_offset, solarSystemFrom + solarSytem, positionFrom + position_offset));
                            }
                        }

                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset > 0;
                        if (galaxies_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset)
                        {
                            TargetPlanet(galaxyFrom - galaxy_offset, solarSytem, positionFrom - position_offset);
                            if(ScanPrerogative.Invoke())
                            {   
                                result.Add(new Coordinates(galaxyFrom - galaxy_offset, solarSystemFrom - solarSytem, positionFrom - position_offset));
                                if(returnNearest)
                                {
                                    return result;
                                }
                            }
                        }

                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private List<Coordinates> ScanPositionInRange(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            List<Coordinates> result = new();
            bool returnNearest = (scanOptions.inRange & ScanOptions.InRange.nearest) == ScanOptions.InRange.nearest;

            for (int galaxy_offset = 0; galaxy_offset < int.Parse(Program.settings.Univers.GalaxyCount); galaxy_offset++)
            {
                bool univers_being_scanned_on_positive_offset = galaxyFrom + galaxy_offset <= int.Parse(Program.settings.Univers.GalaxyCount);
                bool univers_being_scanned_on_negative_offset = galaxyFrom - galaxy_offset > 0;

                if (!univers_being_scanned_on_positive_offset && !univers_being_scanned_on_negative_offset)
                {
                    // scan of the univers ends.
                    break;
                }

                for (int solarSystem_offset = 0; solarSystem_offset < int.Parse(Program.settings.Univers.SolarSystemCount); solarSystem_offset++)
                {
                    bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= int.Parse(Program.settings.Univers.SolarSystemCount);
                    bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset > 0;
                    
                    if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                    {
                        // scan of the current galaxy ends
                        break;
                    }

                    for (int position = scanOptions.range.First().min; position <= scanOptions.range.First().max; position++)
                    {
                        if (univers_being_scanned_on_positive_offset && solar_system_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, position);
                            if(ScanPrerogative.Invoke())
                            {
                                if(returnNearest && result.Count > 0)
                                {
                                    return result;
                                }

                                result.Add(new Coordinates(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position));                               
                            }
                        }

                        if (univers_being_scanned_on_negative_offset && solar_system_being_scanned_on_negative_offset)
                        {
                            TargetPlanet(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, position);
                            if(ScanPrerogative.Invoke())
                            {   
                                if(returnNearest)
                                {
                                    result.Add(new Coordinates(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position));
                                    return result;
                                }
                                    
                                result.Add(new Coordinates(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position));
                            }
                        }
                    }
                }
            }

            return null;
        }

        private List<Coordinates> ScanNearest(Delegate.ScanPrerogative ScanPrerogative)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            List<Coordinates> result = new();

            for (int galaxy_offset = 0; galaxy_offset < int.Parse(Program.settings.Univers.GalaxyCount); galaxy_offset++)
            {
                bool univers_being_scanned_on_positive_offset = galaxyFrom + galaxy_offset <= int.Parse(Program.settings.Univers.GalaxyCount);
                bool univers_being_scanned_on_negative_offset = galaxyFrom - galaxy_offset > 0;

                if (!univers_being_scanned_on_positive_offset && !univers_being_scanned_on_negative_offset)
                {
                    // scan of the univers ends.
                    break;
                }

                for (int solarSystem_offset = 0; solarSystem_offset < int.Parse(Program.settings.Univers.SolarSystemCount); solarSystem_offset++)
                {
                    bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= int.Parse(Program.settings.Univers.SolarSystemCount);
                    bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset > 0;
                    
                    if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                    {
                        // scan of the current galaxy ends
                        break;
                    }

                    for (int position_offset = 0; position_offset <= 15; position_offset++)
                    {
                        bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= 16;
                        if (univers_being_scanned_on_positive_offset && solar_system_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            if(ScanPrerogative.Invoke())
                            {
                                if(result.Count > 0)
                                {
                                    return result;
                                }

                                result.Add(new Coordinates(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset));
                            }
                        }

                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset > 0;
                        if (univers_being_scanned_on_negative_offset && solar_system_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset)
                        {
                            TargetPlanet(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                            if(ScanPrerogative.Invoke())
                            {   
                                result.Add(new Coordinates(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset));
                                return result;                                
                            }
                        }

                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }
                }
            }

            return null;
        }

        public List<Coordinates> ScanAll(Delegate.ScanPrerogative ScanPrerogative)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            List<Coordinates> validCoordinates = new();

            for (int galaxy_offset = 0; galaxy_offset < int.Parse(Program.settings.Univers.GalaxyCount); galaxy_offset++)
            {
                bool univers_being_scanned_on_positive_offset = galaxyFrom + galaxy_offset <= int.Parse(Program.settings.Univers.GalaxyCount);
                bool univers_being_scanned_on_negative_offset = galaxyFrom - galaxy_offset > 0;

                if (!univers_being_scanned_on_positive_offset && !univers_being_scanned_on_negative_offset)
                {
                    // scan of the univers ends.
                    break;
                }

                for (int solarSystem_offset = 0; solarSystem_offset < int.Parse(Program.settings.Univers.SolarSystemCount); solarSystem_offset++)
                {
                    bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= int.Parse(Program.settings.Univers.SolarSystemCount);
                    bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset > 0;
                    
                    if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                    {
                        // scan of the current galaxy ends
                        break;
                    }

                    for (int position_offset = 0; position_offset <= 15; position_offset++)
                    {
                        bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= 16;
                        if (univers_being_scanned_on_positive_offset && solar_system_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            if(ScanPrerogative.Invoke())
                            {
                                validCoordinates.Add(new Coordinates(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset));
                            }
                        }

                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset > 0;
                        if (univers_being_scanned_on_negative_offset && solar_system_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset)
                        {
                            TargetPlanet(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                            if(ScanPrerogative.Invoke())
                            {   
                                validCoordinates.Add(new Coordinates(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset));
                            }
                        }

                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }
                }
            }

            return validCoordinates;
        }
#endregion
        public Coordinates FindNearestTargetType(TargetStatus status)
        {
            List<Coordinates> list_result = ScanNearest(() => {return GetStatus() == status;});
            if(list_result.Count > 0)
            {
                return list_result[0];
            }

            return null;
        }

        public Coordinates FindNearestInSolarSystem_targetType(TargetStatus status)
        {
            ScanOptions scanOptions = new();
            scanOptions.inRange = ScanOptions.InRange.solarSystem_of_the_planet_which_scans & ScanOptions.InRange.nearest;
            scanOptions.range = new();
            scanOptions.range.First().min = 1;
            scanOptions.range.First().max = 16;

            List<Coordinates> list_result = ScanSolarSystemInRange(() => {return GetStatus() == status;}, scanOptions);
            
            if(list_result.Count > 0)
            {
                return list_result[0];
            }

            return null;
        }

        public Coordinates FindNearestInTheGalaxy_targetType(TargetStatus status)
        {
            ScanOptions scanOptions = new();
            scanOptions.inRange = ScanOptions.InRange.solarSystem_of_the_planet_which_scans & ScanOptions.InRange.nearest;
            scanOptions.range.First().min = 1;
            scanOptions.range.First().max = 16;
            List<Coordinates> list_result = ScanPositionInRange(() => {return GetStatus() == status;}, scanOptions);
            
            if(list_result.Count > 0)
            {
                return list_result[0];
            }

            return null;
        }

        public void SendFleet()
        {
            MetalLoad = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.Metal).Text);
            CrystalLoad = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.Crystal).Text);
            DeuteriumLoad = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.Deuterium).Text);
            FoodLoad = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.Food).Text);
            TimeSpan arrivalTime = GetArrivalTime() - DateTime.Now;
            TimeSpan returnTime = GetReturnTime() - DateTime.Now;
            MyDriver.MoveToElement(Program.settings.Fleet.SendFleetButton, act).Click().Build().Perform();

            MovingFleet movingFleet = new(arrivalTime, returnTime, ChasseurLeger, ChasseurLourd, Croiseur, VaisseauBataille, Traqueur, Bombardier, Destructeur, Edm, Faucheur, Eclaireur,
        PetitTransporteur, GrandTransporteur, VaisseauColonisation, Recycleur, SondeEspionnage, MetalLoad, CrystalLoad, DeuteriumLoad, FoodLoad, planetFrom.Coordinates);
            
            #region reset Fleet menu
            ChasseurLeger = 0;
            ChasseurLourd = 0;
            Croiseur = 0;
            VaisseauBataille = 0;
            Traqueur = 0;
            Bombardier = 0;
            Destructeur = 0;
            Edm = 0;
            Faucheur = 0;
            Eclaireur= 0;
            PetitTransporteur = 0;
            GrandTransporteur = 0;
            VaisseauColonisation = 0;
            Recycleur = 0;
            SondeEspionnage = 0;
            MetalLoad = 0;
            CrystalLoad = 0;
            DeuteriumLoad = 0;
            FoodLoad = 0;
            #endregion reset Fleet menu

            AddMovingFleet?.Invoke(movingFleet);
        }

        #endregion
    }
}