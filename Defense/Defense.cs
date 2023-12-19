using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;

namespace FirstTest
{
    public class Defense: NavigationMenu
    {
        private static void BuildDefense(string cssSelector, int number, Actions act)
        {
            GoTo(Menu.Defense, act);

            MyDriver.MoveToElement(cssSelector, act).Click().Build().Perform();
              
            SendKeysAmount(number, act);

            MyDriver.MoveToElement(Program.settings.Defense.Details.Develop, act).Click();
            act.Build().Perform();
        }

        private static Actions SendKeysAmount(int number, Actions act)
        {
            IWebElement amount = MyDriver.FindElement(Program.settings.Defense.Details.BuildAmount);
            int maximum = int.Parse(amount.GetAttribute("max"));
            number = maximum < number ? maximum : number;

            MyDriver.MoveToElement(Program.settings.Defense.Details.BuildAmount, act).Click().SendKeys(number.ToString());
            return act;
        }

        private static void BuildDefense(string cssSelector, Actions act)
        {
            GoTo(Menu.Defense, act);

            MyDriver.MoveToElement(cssSelector, act).Click().Build().Perform();
            MyDriver.MoveToElement(Program.settings.Defense.Details.Develop, act).Click();
            act.Build().Perform();
        }

        #region "public"

        public static void BuildLanceurMissile(int number, Actions act)
        {
            BuildDefense(Program.settings.Defense.LanceurMissile,number, act);
        }

        public static void BuildLaserLeger(int number, Actions act)
        {
            BuildDefense(Program.settings.Defense.LaserLeger,number, act);
        }
        public static void BuildLaserLourd(int number, Actions act)
        {
            BuildDefense(Program.settings.Defense.LaserLourd,number, act);
        }
        public static void BuildCanonDeGausse(int number, Actions act)
        {
            BuildDefense(Program.settings.Defense.CanonDeGausse,number, act);
        }
        public static void BuildArtillerieIon(int number, Actions act)
        {
            BuildDefense(Program.settings.Defense.ArtillerieIon,number, act);
        }
        public static void BuildLanceurPlasma(int number, Actions act)
        {
            BuildDefense(Program.settings.Defense.LanceurPlasma,number, act);
        }
        public static void BuildPetitBouclier(Actions act)
        {
            BuildDefense(Program.settings.Defense.PetitBouclier, act);
        }
        public static void BuildGrandBouclier(Actions act)
        {
            BuildDefense(Program.settings.Defense.GrandBouclier, act);
        }
        public static void BuildMisileInterception(int number, Actions act)
        {
            BuildDefense(Program.settings.Defense.MisileInterception,number, act);
        }
        public static void BuildMissileInterplanetaire(int number, Actions act)
        {
            BuildDefense(Program.settings.Defense.MissileInterplanetaire,number, act);
        }
        #endregion  
    }
}