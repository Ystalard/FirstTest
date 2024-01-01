using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace FirstTest
{
    public class Planet
    {
        public Resources resources;
        public Installations installations;
        public Recherche recherche;
        public ChantierSpatial chantierSpatial;
        public Defense defense;

        SharedProperties sharedProperties;

        public Planet(Actions act)
        {
            sharedProperties = new SharedProperties{ Timer = new Handler.Timer()};
            resources = new(act,sharedProperties);
            installations = new(act, sharedProperties);
            recherche = new(act);
            chantierSpatial = new(act);
            defense = new(act);
        }
    }
}