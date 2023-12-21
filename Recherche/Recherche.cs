using OpenQA.Selenium.Interactions;
using FirstTest.Handler;

namespace FirstTest
{
    public class Recherche: Buildable
    {

        #region "Constructor"
        public Recherche(Actions act): base(act, Menu.Recherche){}
        #endregion

        #region "public"
        #region "Recherche fondamentales"
        public void BuildTechnoEnergie()
        {
            Develop(Program.settings.Recherche.TechnoEnergie);
        }

        public bool CanBuildTechnoEnergie()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoEnergie);
        }

        public void BuildTechnoLaser()
        {
            Develop(Program.settings.Recherche.TechnoLaser);
        }

        public bool CanBuildTechnoLaser()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoLaser);
        }

        public void BuildTechnoIons()
        {
            Develop(Program.settings.Recherche.TechnoIons);
        }

        public bool CanBuildTechnoIons()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoIons);
        }

        public void BuildTechnoHyperespace()
        {
            Develop(Program.settings.Recherche.TechnoHyperespace);
        }

        public bool CanBuildTechnoHyperespace()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoHyperespace);
        }
        public void BuildTechnoPlasma()
        {
            Develop(Program.settings.Recherche.TechnoPlasma);
        }

        public bool CanBuildTechnoPlasma()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoPlasma);
        }
        #endregion

        #region "Recherche en Propulsion"
        public void BuildReacteurCombustion()
        {
            Develop(Program.settings.Recherche.ReacteurCombustion);
        }

        public bool CanBuildReacteurCombustion()
        {
            return CanBuildElement(Program.settings.Recherche.ReacteurCombustion);
        }
        public void BuildReacteurImpulsion()
        {
            Develop(Program.settings.Recherche.ReacteurImpulsion);
        }

        public bool CanBuildReacteurImpulsion()
        {
            return CanBuildElement(Program.settings.Recherche.ReacteurImpulsion);
        }
        public void BuildPropulsionHyperespace()
        {
            Develop(Program.settings.Recherche.PropulsionHyperespace);
        }

        public bool CanBuildPropulsionHyperespace()
        {
            return CanBuildElement(Program.settings.Recherche.PropulsionHyperespace);
        }
        #endregion

        #region "Recherche avancée"
        public void BuildTechnoEspionnage()
        {
            Develop(Program.settings.Recherche.TechnoEspionnage);
        }

        public bool CanBuildTechnoEspionnage()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoEspionnage);
        }
        public void BuildTechnoOrdinateur()
        {
            Develop(Program.settings.Recherche.TechnoOrdinateur);
        }

        public bool CanBuildTechnoOrdinateur()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoOrdinateur);
        }
        public void BuildTechnoAstro()
        {
            Develop(Program.settings.Recherche.TechnoAstro);
        }

        public bool CanBuildTechnoAstro()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoAstro);
        }
        public void BuildReseauRecherche()
        {
            Develop(Program.settings.Recherche.ReseauRecherche);
        }

        public bool CanBuildReseauRecherche()
        {
            return CanBuildElement(Program.settings.Recherche.ReseauRecherche);
        }
        public void BuildTechnoGraviton()
        {
            Develop(Program.settings.Recherche.TechnoGraviton);
        }

        public bool CanBuildTechnoGraviton()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoGraviton);
        }
        #endregion

        #region "Recherche de combat"
        public void BuildTechnoArme()
        {
            Develop(Program.settings.Recherche.TechnoArme);
        }

        public bool CanBuildTechnoArme()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoArme);
        }
        public void BuildTechnoBouclier()
        {
            Develop(Program.settings.Recherche.TechnoBouclier);
        }

        public bool CanBuildTechnoBouclier()
        {
            return CanBuildElement(Program.settings.Recherche.TechnoBouclier);
        }
        public void BuildTechnoProtectionVaisseaux()
        {
            Develop(Program.settings.Recherche.TechnoProtectionVaisseaux);
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