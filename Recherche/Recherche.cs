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
        #endregion
        #endregion
    }
}