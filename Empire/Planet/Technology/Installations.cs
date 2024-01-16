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
    string GetInstallationsInConstruction();
}
    public class Installations: Buildable, IInstallations
    {
        #region properties
        #region private properties
        private string installations_in_construction;
        #endregion private properties
        #endregion properties

        #region "constructor"
        public Installations(Actions act, IPlanet planet): base(act, Menu.Installations, planet){}

        public Installations(Actions act, SharedProperties sharedProperties, IPlanet planet): base(act, Menu.Installations, sharedProperties, planet){}
        #endregion "constructor"

        #region "public"
        public string GetInstallationsInConstruction()
        {
            if(!base.IsBusy())
            {
                installations_in_construction = string.Empty;
            }
            return installations_in_construction;
        }

        public void BuildUsineRobot()
        {
            Develop(Program.settings.Facilities.UsineRobot);
            if(base.IsBusy())
            {
                installations_in_construction = Program.settings.Facilities.UsineRobot;
            }
        }

        public bool CanBuildUsineRobot()
        {
            return CanBuildElement(Program.settings.Facilities.UsineRobot);
        }

        public void BuildChantierSpatiale()
        {
            Develop(Program.settings.Facilities.ChantierSpatial);
            if(base.IsBusy())
            {
                installations_in_construction = Program.settings.Facilities.ChantierSpatial;
            }
        }

        public bool CanBuildChantierSpatial()
        {
            if(GetPlanet().Defense().IsBusy() || GetPlanet().ChantierSpatial().IsBusy())
            {
                return false;
            }
            return CanBuildElement(Program.settings.Facilities.ChantierSpatial);
        }

        public void BuildLaboRecherche()
        {
            Develop(Program.settings.Facilities.LaboRecherche);
            if(base.IsBusy())
            {
                installations_in_construction = Program.settings.Facilities.LaboRecherche;
            }
        }

        public bool CanBuildLaboRecherche()
        {
            if(GetPlanet().Recherche().IsBusy())
            {
                return false;
            }
            
            return CanBuildElement(Program.settings.Facilities.LaboRecherche);
        }

        public  void BuildDepotRavitaillement()
        {
            Develop(Program.settings.Facilities.DepotRavitaillement);
            if(base.IsBusy())
            {
                installations_in_construction = Program.settings.Facilities.DepotRavitaillement;
            }
        }

        public bool CanBuildDepotRavitaillement()
        {
            return CanBuildElement(Program.settings.Facilities.DepotRavitaillement);
        }

        public void BuildSiloMissible()
        {
            Develop(Program.settings.Facilities.SiloMissible);
            if(base.IsBusy())
            {
                installations_in_construction = Program.settings.Facilities.SiloMissible;
            }
        }

        public bool CanBuildSiloMissible()
        {
            return CanBuildElement(Program.settings.Facilities.SiloMissible);
        }

        public void BuildNanites()
        {
            Develop(Program.settings.Facilities.Nanites);
            if(base.IsBusy())
            {
                installations_in_construction = Program.settings.Facilities.Nanites;
            }
        }

        public bool CanBuildNanites()
        {
            return CanBuildElement(Program.settings.Facilities.Nanites);
        }

        public void BuildTerraformeur()
        {
            Develop(Program.settings.Facilities.Terraformeur);
            if(base.IsBusy())
            {
                installations_in_construction = Program.settings.Facilities.Terraformeur;
            }
        }

        public bool CanBuildTerraformeur()
        {
            return CanBuildElement(Program.settings.Facilities.Terraformeur);
        }

        public void BuildDocker()
        {
            Develop(Program.settings.Facilities.Docker);
            if(base.IsBusy())
            {
                installations_in_construction = Program.settings.Facilities.Docker;
            }
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