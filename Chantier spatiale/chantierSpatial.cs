using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;

namespace FirstTest
{
    public class ChantierSpatial: NavigationMenu
    {
        private Actions act;

        public ChantierSpatial(Actions act)
        {
            this.act = act;
        }
        private void BuildVaisseau(string cssSelector, int number)
        {
            GoTo(Menu.ChantierSpatial, act);
           
            MyDriver.MoveToElement(cssSelector, act).Click().Build().Perform();
            
          
            SendKeysAmount(number);

            MyDriver.MoveToElement(Program.settings.ChantierSpatial.Details.Develop, act).Click();
            act.Build().Perform();
        }

        private Actions SendKeysAmount(int number)
        {
            IWebElement amount = Handler.MyDriver.FindElement(Program.settings.ChantierSpatial.Details.BuildAmount);
            int maximum = int.Parse(amount.GetAttribute("max"));
            number = maximum < number ? maximum : number;

            MyDriver.MoveToElement(Program.settings.ChantierSpatial.Details.BuildAmount, act).Click().SendKeys(number.ToString());
            return act;
        }

        #region "public"
        public void BuildVaisseauColonisation(int number)
        {
            BuildVaisseau(Program.settings.ChantierSpatial.VaisseauColonisation,number);
        }

        public void BuildPetitTransporteur(int number)
        {
            BuildVaisseau(Program.settings.ChantierSpatial.PetitTransporteur,number);
        }

        public void BuildGrandTransporteur(int number)
        {
            BuildVaisseau(Program.settings.ChantierSpatial.GrandTransporteur, number);
        }

        public void BuildChasseurLeger(int number)
        {
            BuildVaisseau(Program.settings.ChantierSpatial.ChasseurLeger, number);
        }
        #endregion
    }
}