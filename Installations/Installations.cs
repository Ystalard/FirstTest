using OpenQA.Selenium.Interactions;
using FirstTest.Handler;

namespace FirstTest
{
    public class Installations: Buildable
    {
        #region "constructor"
        public Installations(Actions act): base(act, Menu.Installations){}

        public Installations(Actions act, SharedProperties sharedProperties): base(act, Menu.Installations, sharedProperties){}
        #endregion "constructor"


        #region "public"
        public void BuildUsineRobot()
        {
            Develop(Program.settings.Facilities.UsineRobot);
        }

        public void BuildLaboRecherche()
        {
            Develop(Program.settings.Facilities.LaboRecherche);
        }

        public  void BuildDepotRavitaillement()
        {
            Develop(Program.settings.Facilities.DepotRavitaillement);
        }

        public void BuildSiloMissible()
        {
            Develop(Program.settings.Facilities.SiloMissible);
        }

        public void BuildNanites()
        {
            Develop(Program.settings.Facilities.Nanites);
        }

        public void BuildTerraformeur()
        {
            Develop(Program.settings.Facilities.Terraformeur);
        }

        public void BuildDocker()
        {
            Develop(Program.settings.Facilities.Docker);
        }
        #endregion
    }
}