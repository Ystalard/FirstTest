using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;


namespace FirstTest
{
    public class Installations: NavigationMenu
    {

        private static void BuildInstallations(string cssSelector, ref Actions act){
            GoTo(Menu.Installations, ref act);

            MyDriver.MoveToElement(cssSelector, ref act).Click().Build().Perform();

            MyDriver.MoveToElement(Program.settings.Facilities.Details.Develop, ref act).Click().Build().Perform();
        }

        #region "public"
        public static void BuildUsineRobot(ref Actions act){
            BuildInstallations(Program.settings.Facilities.UsineRobot, ref act);
        }

        public static void BuildLaboRecherche(ref Actions act){
            BuildInstallations(Program.settings.Facilities.LaboRecherche, ref act);
        }

        public static void BuildDepotRavitaillement(ref Actions act){
            BuildInstallations(Program.settings.Facilities.DepotRavitaillement, ref act);
        }

        public static void BuildSiloMissible(ref Actions act){
            BuildInstallations(Program.settings.Facilities.SiloMissible, ref act);
        }

        public static void BuildNanites(ref Actions act){
            BuildInstallations(Program.settings.Facilities.Nanites, ref act);
        }

        public static void BuildTerraformeur(ref Actions act){
            BuildInstallations(Program.settings.Facilities.Terraformeur, ref act);
        }

        public static void BuildDocker(ref Actions act){
            BuildInstallations(Program.settings.Facilities.Docker, ref act);
        }
        #endregion
    }
}