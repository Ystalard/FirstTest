using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;

namespace FirstTest
{
    public class ChantierSpatial: NavigationMenu
    {
        private static void BuildVaisseau(string cssSelector, int number, ref Actions act){
            GoTo(Menu.ChantierSpatial, ref act);
           
            MyDriver.MoveToElement(cssSelector, ref act).Click().Build().Perform();
            
          
            SendKeysAmount(number, ref act);

            MyDriver.MoveToElement(Program.settings.ChantierSpatial.Details.Develop, ref act).Click();
            act.Build().Perform();
        }

        private static Actions SendKeysAmount(int number, ref Actions act){
            IWebElement amount = Handler.MyDriver.FindElement(Program.settings.ChantierSpatial.Details.BuildAmount);
            int maximum = int.Parse(amount.GetAttribute("max"));
            number = maximum < number ? maximum : number;

            MyDriver.MoveToElement(Program.settings.ChantierSpatial.Details.BuildAmount, ref act).Click().SendKeys(number.ToString());
            return act;
        }

        #region "public"
        public static void BuildVaisseauColonisation(int number, ref Actions act){
            BuildVaisseau(Program.settings.ChantierSpatial.VaisseauColonisation,number, ref act);
        }

        public static void BuildPetitTransporteur(int number, ref Actions act){
            BuildVaisseau(Program.settings.ChantierSpatial.PetitTransporteur,number, ref act);
        }

        public static void BuildGrandTransporteur(int number, ref Actions act){
            BuildVaisseau(Program.settings.ChantierSpatial.GrandTransporteur, number, ref act);
        }

        public static void BuildChasseurLeger(int number, ref Actions act){
            BuildVaisseau(Program.settings.ChantierSpatial.ChasseurLeger, number, ref act);
        }
        #endregion
    }
}