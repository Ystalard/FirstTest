using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FirstTest.Handler;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace FirstTest.Empire.Planet.Menu
{
    public class Fleet
    {
        #region private properties
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

        #region public properties
        public int GetChasseurLeger { get => ChasseurLeger;}
        public int GetChasseurLourd { get => ChasseurLourd;}
        public int GetCroiseur { get => Croiseur;}
        public int GetVaisseauBataille { get => VaisseauBataille;}
        public int GetTraqueur { get => Traqueur;}
        public int GetBombardier { get => Bombardier;}
        public int GetDestructeur { get => Destructeur;}
        public int GetEdm { get => Edm;}
        public int GetFaucheur { get => Faucheur;}
        public int GetEclaireur { get => Eclaireur;}
        public int GetPetitTransporteur { get => PetitTransporteur;}
        public int GetGrandTransporteur { get => GrandTransporteur;}
        public int GetVaisseauColonisation { get => VaisseauColonisation;}
        public int GetRecycleur { get => Recycleur;}
        public int GetSondeEspionnage { get => SondeEspionnage;}
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
            strong
        }
        #endregion

        #region public method
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

        public void Continue()
        {
            ChasseurLeger = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ChasseurLeger).Text);
            ChasseurLourd = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ChasseurLourd).Text);
            Croiseur = int.Parse(MyDriver.FindElement(Program.settings.Fleet.Croiseur).Text);
            VaisseauBataille = int.Parse(MyDriver.FindElement(Program.settings.Fleet.VaisseauBataille).Text);
            Traqueur = int.Parse(MyDriver.FindElement(Program.settings.Fleet.Traqueur).Text);
            Bombardier = int.Parse(MyDriver.FindElement(Program.settings.Fleet.Bombardier).Text);
            Destructeur = int.Parse(MyDriver.FindElement(Program.settings.Fleet.Destructeur).Text);
            Edm = int.Parse(MyDriver.FindElement(Program.settings.Fleet.Edm).Text);
            Faucheur = int.Parse(MyDriver.FindElement(Program.settings.Fleet.Faucheur).Text);
            Eclaireur = int.Parse(MyDriver.FindElement(Program.settings.Fleet.Eclaireur).Text);
            PetitTransporteur = int.Parse(MyDriver.FindElement(Program.settings.Fleet.PetitTransporteur).Text);
            GrandTransporteur = int.Parse(MyDriver.FindElement(Program.settings.Fleet.GrandTransporteur).Text);
            VaisseauColonisation = int.Parse(MyDriver.FindElement(Program.settings.Fleet.VaisseauColonisation).Text);
            Recycleur = int.Parse(MyDriver.FindElement(Program.settings.Fleet.Recycleur).Text);
            SondeEspionnage = int.Parse(MyDriver.FindElement(Program.settings.Fleet.SondeEspionnage).Text);
            MyDriver.MoveToElement(Program.settings.Fleet.NextButton, act).Click().Build().Perform();
        }

        private void waitLoadingEnd()
        {
            bool isLoading = true;
            while(isLoading)
            {
                IWebElement loadElement = MyDriver.FindElement(Program.settings.Fleet.Loading);
                if(!loadElement.GetCssValue("display").Contains("none"))
                {
                    isLoading = false;
                }
            }
        }

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

       public static TargetStatus GetStatus()
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
                else
                {
                    return TargetStatus.noob;
                }
            }
       }

       public void TargetPlanet(int galaxy, int system, int position)
       {
            MyDriver.MoveToElement(Program.settings.Fleet.To.Galaxy, act).Click().SendKeys(galaxy.ToString());
            MyDriver.MoveToElement(Program.settings.Fleet.To.System, act).Click().SendKeys(system.ToString());
            MyDriver.MoveToElement(Program.settings.Fleet.To.Position, act).Click().SendKeys(position.ToString());
            act.Build().Perform();
            waitLoadingEnd();
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

        public void SendFleet()
        {
            MetalLoad = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.Metal).Text);
            CrystalLoad = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.Crystal).Text);
            DeuteriumLoad = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.Deuterium).Text);
            FoodLoad = int.Parse(MyDriver.FindElement(Program.settings.Fleet.ResourcesToLoad.Food).Text);

            MyDriver.MoveToElement(Program.settings.Fleet.SendFleetButton, act).Click().Build().Perform();
        }

        #endregion
    }
}