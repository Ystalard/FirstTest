using OpenQA.Selenium.Interactions;
using FirstTest.Handler;


namespace FirstTest
{
    public class Installations: NavigationMenu
    {

        private static void BuildInstallations(string cssSelector, Actions act)
        {
            GoTo(Menu.Installations, act);

            MyDriver.MoveToElement(cssSelector, act).Click().Build().Perform();

            MyDriver.MoveToElement(Program.settings.Facilities.Details.Develop, act).Click().Build().Perform();
        }

        #region "public"
        public static void BuildUsineRobot(Actions act)
        {
            BuildInstallations(Program.settings.Facilities.UsineRobot, act);
        }

        public static void BuildLaboRecherche(Actions act)
        {
            BuildInstallations(Program.settings.Facilities.LaboRecherche, act);
        }

        public static void BuildDepotRavitaillement(Actions act)
        {
            BuildInstallations(Program.settings.Facilities.DepotRavitaillement, act);
        }

        public static void BuildSiloMissible(Actions act)
        {
            BuildInstallations(Program.settings.Facilities.SiloMissible, act);
        }

        public static void BuildNanites(Actions act)
        {
            BuildInstallations(Program.settings.Facilities.Nanites, act);
        }

        public static void BuildTerraformeur(Actions act)
        {
            BuildInstallations(Program.settings.Facilities.Terraformeur, act);
        }

        public static void BuildDocker(Actions act)
        {
            BuildInstallations(Program.settings.Facilities.Docker, act);
        }
        #endregion
    }
}