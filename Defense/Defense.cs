using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;

namespace FirstTest
{
    public class Defense: NavigationMenu
    {

        private static void BuildDefense(string cssSelector, int number, ref Actions act){
            GoTo(Menu.Defense, ref act);

            MyDriver.MoveToElement(cssSelector, ref act).Click().Build().Perform();
              
            SendKeysAmount(number, ref act);

            MyDriver.MoveToElement(Program.settings.Defense.Details.Develop, ref act).Click();
            act.Build().Perform();
        }

        private static Actions SendKeysAmount(int number, ref Actions act){
            IWebElement amount = MyDriver.FindElement(Program.settings.Defense.Details.BuildAmount);
            int maximum = int.Parse(amount.GetAttribute("max"));
            number = maximum < number ? maximum : number;

            MyDriver.MoveToElement(Program.settings.Defense.Details.BuildAmount, ref act).Click().SendKeys(number.ToString());
            return act;
        }

        private static void BuildDefense(string cssSelector, ref Actions act){
            GoTo(Menu.Defense, ref act);

            MyDriver.MoveToElement(cssSelector, ref act).Click().Build().Perform();
            MyDriver.MoveToElement(Program.settings.Defense.Details.Develop, ref act).Click();
            act.Build().Perform();
        }

        #region "public"

        public static void BuildLanceurMissile(int number, ref Actions act){
            BuildDefense(Program.settings.Defense.LanceurMissile,number, ref act);
        }

        public static void BuildLaserLeger(int number, ref Actions act){
            BuildDefense(Program.settings.Defense.LaserLeger,number, ref act);
        }
        public static void BuildLaserLourd(int number, ref Actions act){
            BuildDefense(Program.settings.Defense.LaserLourd,number, ref act);
        }
        public static void BuildCanonDeGausse(int number, ref Actions act){
            BuildDefense(Program.settings.Defense.CanonDeGausse,number, ref act);
        }
        public static void BuildArtillerieIon(int number, ref Actions act){
            BuildDefense(Program.settings.Defense.ArtillerieIon,number, ref act);
        }
        public static void BuildLanceurPlasma(int number, ref Actions act){
            BuildDefense(Program.settings.Defense.LanceurPlasma,number, ref act);
        }
        public static void BuildPetitBouclier(ref Actions act){
            BuildDefense(Program.settings.Defense.PetitBouclier, ref act);
        }
        public static void BuildGrandBouclier(ref Actions act){
            BuildDefense(Program.settings.Defense.GrandBouclier, ref act);
        }
        public static void BuildMisileInterception(int number, ref Actions act){
            BuildDefense(Program.settings.Defense.MisileInterception,number, ref act);
        }
        public static void BuildMissileInterplanetaire(int number, ref Actions act){
            BuildDefense(Program.settings.Defense.MissileInterplanetaire,number, ref act);
        }
        #endregion

        
    }
}