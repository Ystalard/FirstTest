using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace FirstTest.Handler
{
    public static class Workflow
    {
        
        public class BuildStart
        {            
            private delegate void Builder();
            public static void Start(Resources resources, Installations installations, Recherche recherche){
                Builder builder = resources.BuildCentraleSolaire;
                builder += resources.DevelopMetal;
                builder += resources.DevelopMetal;
                builder += resources.BuildCentraleSolaire;
                builder += resources.DevelopMetal;
                builder += resources.DevelopMetal;
                builder += resources.BuildCentraleSolaire;
                builder += resources.DevelopCristal;
                builder += resources.DevelopMetal;
                builder += resources.BuildCentraleSolaire;
                builder += resources.DevelopCristal;
                builder += resources.DevelopCristal;
                builder += resources.BuildCentraleSolaire;
                builder += resources.DevelodDeuterium;
                builder += resources.DevelopMetal;
                builder += resources.BuildCentraleSolaire;
                builder += resources.DevelopCristal;
                builder += resources.DevelopMetal;
                builder += resources.BuildCentraleSolaire;
                builder += resources.DevelopCristal;
                builder += resources.DevelodDeuterium;
                builder += resources.BuildCentraleSolaire;
                builder += resources.DevelodDeuterium;
                builder += resources.DevelodDeuterium;
                builder += resources.DevelodDeuterium; // Remember to lower the deuterium production to 80% in the resource menu!!
                builder += resources.BuildCentraleSolaire; // Remember to reset the deuterium production to 100%
                builder += resources.DevelopCristal;
                builder += resources.DevelodDeuterium; // Remember to lower the deuterium production to 90% in the resource menu and set the metal mine to 90% too!!!
                builder += installations.BuildUsineRobot;
                builder += installations.BuildUsineRobot;
                builder += installations.BuildChantierSpatiale;
                builder += installations.BuildChantierSpatiale;
                builder += installations.BuildLaboRecherche;
                // builder += recherche.DevelopEnergyTech;
                // builder += recherche.BuildCombustionReactor;
                // builder += recherche.BuildCombustionReactor;

            }
        }
    }
}