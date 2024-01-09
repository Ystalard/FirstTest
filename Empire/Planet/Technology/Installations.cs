using OpenQA.Selenium.Interactions;
using FirstTest.Handler;

namespace FirstTest
{
    public interface IInstallations: IBuildable
{
    void BuildChantierSpatiale();
    void BuildDepotRavitaillement();
    void BuildDocker();
    void BuildLaboRecherche();
    void BuildNanites();
    void BuildSiloMissible();
    void BuildTerraformeur();
    void BuildUsineRobot();
    bool CanBuildChantierSpatial();
    bool CanBuildDepotRavitaillement();
    bool CanBuildDocker();
    bool CanBuildLaboRecherche();
    bool CanBuildNanites();
    bool CanBuildSiloMissible();
    bool CanBuildTerraformeur();
    bool CanBuildUsineRobot();
    int GetLevelChantierSpatial();
    int GetLevelDepotRavitaillement();
    int GetLevelDocker();
    int GetLevelLaboRecherche();
    int GetLevelNanites();
    int GetLevelSiloMissible();
    int GetLevelTerraformeur();
    int GetLevelUsineRobot();
}
    public class Installations: Buildable, IInstallations
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

        public bool CanBuildUsineRobot()
        {
            return CanBuildElement(Program.settings.Facilities.UsineRobot);
        }

        public void BuildChantierSpatiale()
        {
            Develop(Program.settings.Facilities.ChantierSpatial);
        }

        public bool CanBuildChantierSpatial()
        {
            return CanBuildElement(Program.settings.Facilities.ChantierSpatial);
        }

        public void BuildLaboRecherche()
        {
            Develop(Program.settings.Facilities.LaboRecherche);
        }

        public bool CanBuildLaboRecherche()
        {
            return CanBuildElement(Program.settings.Facilities.LaboRecherche);
        }

        public  void BuildDepotRavitaillement()
        {
            Develop(Program.settings.Facilities.DepotRavitaillement);
        }

        public bool CanBuildDepotRavitaillement()
        {
            return CanBuildElement(Program.settings.Facilities.DepotRavitaillement);
        }

        public void BuildSiloMissible()
        {
            Develop(Program.settings.Facilities.SiloMissible);
        }

        public bool CanBuildSiloMissible()
        {
            return CanBuildElement(Program.settings.Facilities.SiloMissible);
        }

        public void BuildNanites()
        {
            Develop(Program.settings.Facilities.Nanites);
        }

        public bool CanBuildNanites()
        {
            return CanBuildElement(Program.settings.Facilities.Nanites);
        }

        public void BuildTerraformeur()
        {
            Develop(Program.settings.Facilities.Terraformeur);
        }

        public bool CanBuildTerraformeur()
        {
            return CanBuildElement(Program.settings.Facilities.Terraformeur);
        }

        public void BuildDocker()
        {
            Develop(Program.settings.Facilities.Docker);
        }

        public bool CanBuildDocker()
        {
            return CanBuildElement(Program.settings.Facilities.Docker);
        }

        public int GetLevelUsineRobot()
        {
            return GetCurrentLevel(Program.settings.Facilities.LevelUsineRobot);
        }
        
        public int GetLevelChantierSpatial()
        {
            return GetCurrentLevel(Program.settings.Facilities.LevelChantierSpatial);
        }
        
        public int GetLevelLaboRecherche()
        {
            return GetCurrentLevel(Program.settings.Facilities.LevelLaboRecherche);
        }
        
        public int GetLevelDepotRavitaillement()
        {
            return GetCurrentLevel(Program.settings.Facilities.LevelDepotRavitaillement);
        }
        
        public int GetLevelSiloMissible()
        {
            return GetCurrentLevel(Program.settings.Facilities.LevelSiloMissible);
        }
        
        public int GetLevelNanites()
        {
            return GetCurrentLevel(Program.settings.Facilities.LevelNanites);
        }
        
        public int GetLevelTerraformeur()
        {
            return GetCurrentLevel(Program.settings.Facilities.LevelTerraformeur);
        }
        
        public int GetLevelDocker()
        {
            return GetCurrentLevel(Program.settings.Facilities.LevelDocker);
        }
        
        #endregion
    }
}