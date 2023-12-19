using OpenQA.Selenium.Interactions;
using FirstTest.Handler;

namespace FirstTest
{
    public class Recherche: NavigationMenu
    {

        private static void BuildRecherche(string cssSelector, Actions act)
        {
            GoTo(Menu.Recherche, act);
         
            MyDriver.MoveToElement(cssSelector, act).Click().Build().Perform();

            MyDriver.MoveToElement(Program.settings.Recherche.Details.Develop, act).Click().Build().Perform();
        }

        #region "public"
        #region "Recherche fondamentales"
        public static void BuildTechnoEnergie(Actions act)
        {
            BuildRecherche(Program.settings.Recherche.TechnoEnergie, act);
        }

        public static void BuildTechnoLaser(Actions act)
        {
            BuildRecherche(Program.settings.Recherche.TechnoLaser, act);
        }

        public static void BuildTechnoIons(Actions act)
        {
            BuildRecherche(Program.settings.Recherche.TechnoIons, act);
        }

        public static void BuildTechnoHyperespace(Actions act)
        {
            BuildRecherche(Program.settings.Recherche.TechnoHyperespace, act);
        }
        public static void BuildTechnoPlasma(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.TechnoPlasma, act);
                }
        #endregion

        #region "Recherche en Propulsion"
        public static void BuildReacteurCombustion(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.ReacteurCombustion, act);
                }
        public static void BuildReacteurImpulsion(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.ReacteurImpulsion, act);
                }
        public static void BuildPropulsionHyperespace(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.PropulsionHyperespace, act);
                }
        #endregion

        #region "Recherche avancée"
        public static void BuildTechnoEspionnage(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.TechnoEspionnage, act);
                }
        public static void BuildTechnoOrdinateur(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.TechnoOrdinateur, act);
                }
        public static void BuildTechnoAstro(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.TechnoAstro, act);
                }
        public static void BuildReseauRecherche(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.ReseauRecherche, act);
                }
        public static void BuildTechnoGraviton(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.TechnoGraviton, act);
                }
        #endregion

        #region "Recherche de combat"
        public static void BuildTechnoArme(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.TechnoArme, act);
        }
        public static void BuildTechnoBouclier(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.TechnoBouclier, act);
        }
        public static void BuildTechnoProtectionVaisseaux(Actions act)
        {
                    BuildRecherche(Program.settings.Recherche.TechnoProtectionVaisseaux, act);
        }
        #endregion
        #endregion
    }
}