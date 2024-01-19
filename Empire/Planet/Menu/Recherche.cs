using OpenQA.Selenium.Interactions;
using FirstTest.Handler;

namespace FirstTest
{
    public interface IRecherche: IBuildable
{
    void BuildPropulsionHyperespace();
    void BuildReacteurCombustion();
    void BuildReacteurImpulsion();
    void BuildReseauRecherche();
    void BuildTechnoArme();
    void BuildTechnoAstro();
    void BuildTechnoBouclier();
    void BuildTechnoEnergie();
    void BuildTechnoEspionnage();
    void BuildTechnoGraviton();
    void BuildTechnoHyperespace();
    void BuildTechnoIons();
    void BuildTechnoLaser();
    void BuildTechnoOrdinateur();
    void BuildTechnoPlasma();
    void BuildTechnoProtectionVaisseaux();
    bool CanBuildPropulsionHyperespace();
    bool CanBuildReacteurCombustion();
    bool CanBuildReacteurImpulsion();
    bool CanBuildReseauRecherche();
    bool CanBuildTechnoArme();
    bool CanBuildTechnoAstro();
    bool CanBuildTechnoBouclier();
    bool CanBuildTechnoEnergie();
    bool CanBuildTechnoEspionnage();
    bool CanBuildTechnoGraviton();
    bool CanBuildTechnoHyperespace();
    bool CanBuildTechnoIons();
    bool CanBuildTechnoLaser();
    bool CanBuildTechnoOrdinateur();
    bool CanBuildTechnoPlasma();
    bool CanBuildTechnoProtectionVaisseaux();
    int GetLevelPropulsionHyperespace();
    int GetLevelReacteurCombustion();
    int GetLevelReacteurImpulsion();
    int GetLevelReseauRecherche();
    int GetLevelTechnoArme();
    int GetLevelTechnoAstro();
    int GetLevelTechnoBouclier();
    int GetLevelTechnoEnergie();
    int GetLevelTechnoEspionnage();
    int GetLevelTechnoGraviton();
    int GetLevelTechnoHyperespace();
    int GetLevelTechnoIons();
    int GetLevelTechnoLaser();
    int GetLevelTechnoOrdinateur();
    int GetLevelTechnoPlasma();
    int GetLevelTechnoProtectionVaisseaux();
    new bool IsBusy();
}
    public class Recherche: Buildable, IRecherche
    {

        #region "Constructor"
        public Recherche(Actions act, IPlanet planet): base(act, Menu.Recherche, planet){}
        #endregion

        #region "public"
        public override bool IsBusy()
        {
            return GetPlanet().Installations().GetInstallationsInConstruction() == Program.settings.Facilities.LaboRecherche ? true : base.IsBusy();
        }
        #region "Recherche fondamentales"
        public void BuildTechnoEnergie()
        {
            Develop(Program.settings.Recherche.DevelopTechnoEnergie);
        }

        public bool CanBuildTechnoEnergie()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoEnergie);
        }

        public void BuildTechnoLaser()
        {
            Develop(Program.settings.Recherche.DevelopTechnoLaser);
        }

        public bool CanBuildTechnoLaser()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoLaser);
        }

        public void BuildTechnoIons()
        {
            Develop(Program.settings.Recherche.DevelopTechnoIons);
        }

        public bool CanBuildTechnoIons()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoIons);
        }

        public void BuildTechnoHyperespace()
        {
            Develop(Program.settings.Recherche.DevelopTechnoHyperespace);
        }

        public bool CanBuildTechnoHyperespace()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoHyperespace);
        }
        public void BuildTechnoPlasma()
        {
            Develop(Program.settings.Recherche.DevelopTechnoPlasma);
        }

        public bool CanBuildTechnoPlasma()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoPlasma);
        }
        #endregion

        #region "Recherche en Propulsion"
        public void BuildReacteurCombustion()
        {
            Develop(Program.settings.Recherche.DevelopReacteurCombustion);
        }

        public bool CanBuildReacteurCombustion()
        {
            return CanBuildElement(Program.settings.Recherche.ReacteurCombustion);
        }
        public void BuildReacteurImpulsion()
        {
            Develop(Program.settings.Recherche.DevelopReacteurImpulsion);
        }

        public bool CanBuildReacteurImpulsion()
        {
            return CanBuildElement(Program.settings.Recherche.ReacteurImpulsion);
        }
        public void BuildPropulsionHyperespace()
        {
            Develop(Program.settings.Recherche.DevelopPropulsionHyperespace);
        }

        public bool CanBuildPropulsionHyperespace()
        {
            return CanBuildElement(Program.settings.Recherche.PropulsionHyperespace);
        }
        #endregion

        #region "Recherche avancée"
        public void BuildTechnoEspionnage()
        {
            Develop(Program.settings.Recherche.DevelopTechnoEspionnage);
        }

        public bool CanBuildTechnoEspionnage()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoEspionnage);
        }
        public void BuildTechnoOrdinateur()
        {
            Develop(Program.settings.Recherche.DevelopTechnoOrdinateur);
        }

        public bool CanBuildTechnoOrdinateur()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoOrdinateur);
        }
        public void BuildTechnoAstro()
        {
            Develop(Program.settings.Recherche.DevelopTechnoAstro);
        }

        public bool CanBuildTechnoAstro()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoAstro);
        }
        public void BuildReseauRecherche()
        {
            Develop(Program.settings.Recherche.DevelopReseauRecherche);
        }

        public bool CanBuildReseauRecherche()
        {
            return CanBuildElement(Program.settings.Recherche.ReseauRecherche);
        }
        public void BuildTechnoGraviton()
        {
            Develop(Program.settings.Recherche.DevelopTechnoGraviton);
        }

        public bool CanBuildTechnoGraviton()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoGraviton);
        }
        #endregion

        #region "Recherche de combat"
        public void BuildTechnoArme()
        {
            Develop(Program.settings.Recherche.DevelopTechnoArme);
        }

        public bool CanBuildTechnoArme()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoArme);
        }
        public void BuildTechnoBouclier()
        {
            Develop(Program.settings.Recherche.DevelopTechnoBouclier);
        }

        public bool CanBuildTechnoBouclier()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoBouclier);
        }
        public void BuildTechnoProtectionVaisseaux()
        {
            Develop(Program.settings.Recherche.DevelopTechnoProtectionVaisseaux);
        }

        public bool CanBuildTechnoProtectionVaisseaux()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoProtectionVaisseaux);
        }

        public int GetLevelTechnoEnergie()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoEnergie);
        }
        public int GetLevelTechnoLaser()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoLaser);
        }
        public int GetLevelTechnoIons()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoIons);
        }
        public int GetLevelTechnoHyperespace()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoHyperespace);
        }
        public int GetLevelTechnoPlasma()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoPlasma);
        }
        public int GetLevelReacteurCombustion()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelReacteurCombustion);
        }
        public int GetLevelReacteurImpulsion()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelReacteurImpulsion);
        }
        public int GetLevelPropulsionHyperespace()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelPropulsionHyperespace);
        }
        public int GetLevelTechnoEspionnage()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoEspionnage);
        }
        public int GetLevelTechnoOrdinateur()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoOrdinateur);
        }
        public int GetLevelTechnoAstro()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoAstro);
        }
        public int GetLevelReseauRecherche()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelReseauRecherche);
        }
        public int GetLevelTechnoGraviton()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoGraviton);
        }
        public int GetLevelTechnoArme()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoArme);
        }
        public int GetLevelTechnoBouclier()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoBouclier);
        }
        public int GetLevelTechnoProtectionVaisseaux()
        {
            return GetCurrentLevel(Program.settings.Recherche.LevelTechnoProtectionVaisseaux);
        }
        #endregion
        #endregion
    }
}