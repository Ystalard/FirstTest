using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;

namespace FirstTest
{
    public class Recherche: NavigationMenu
    {

        private static void BuildRecherche(string cssSelector, ref Actions act){
            GoTo(Menu.Recherche, ref act);
         
            MyDriver.MoveToElement(cssSelector, ref act).Click().Build().Perform();

            MyDriver.MoveToElement(Program.settings.Recherche.Details.Develop, ref act).Click().Build().Perform();
        }

        #region "public"
        #region "Recherche fondamentales"
        public static void BuildTechnoEnergie(ref Actions act){
            BuildRecherche(Program.settings.Recherche.TechnoEnergie, ref act);
        }

        public static void BuildTechnoLaser(ref Actions act){
            BuildRecherche(Program.settings.Recherche.TechnoLaser, ref act);
        }

        public static void BuildTechnoIons(ref Actions act){
            BuildRecherche(Program.settings.Recherche.TechnoIons, ref act);
        }

        public static void BuildTechnoHyperespace(ref Actions act){
            BuildRecherche(Program.settings.Recherche.TechnoHyperespace, ref act);
        }
        public static void BuildTechnoPlasma(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.TechnoPlasma, ref act);
                }
        #endregion

        #region "Recherche en Propulsion"
        public static void BuildReacteurCombustion(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.ReacteurCombustion, ref act);
                }
        public static void BuildReacteurImpulsion(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.ReacteurImpulsion, ref act);
                }
        public static void BuildPropulsionHyperespace(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.PropulsionHyperespace, ref act);
                }
        #endregion

        #region "Recherche avancée"
        public static void BuildTechnoEspionnage(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.TechnoEspionnage, ref act);
                }
        public static void BuildTechnoOrdinateur(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.TechnoOrdinateur, ref act);
                }
        public static void BuildTechnoAstro(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.TechnoAstro, ref act);
                }
        public static void BuildReseauRecherche(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.ReseauRecherche, ref act);
                }
        public static void BuildTechnoGraviton(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.TechnoGraviton, ref act);
                }
        #endregion

        #region "Recherche de combat"
        public static void BuildTechnoArme(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.TechnoArme, ref act);
        }
        public static void BuildTechnoBouclier(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.TechnoBouclier, ref act);
        }
        public static void BuildTechnoProtectionVaisseaux(ref Actions act){
                    BuildRecherche(Program.settings.Recherche.TechnoProtectionVaisseaux, ref act);
        }
        #endregion
        #endregion
    }
}