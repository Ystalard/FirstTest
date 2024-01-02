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

        SharedProperties resources_and_installations_shared_timer;
        SharedProperties chantierSpatial_and_defense_shared_timer;

        public Planet(Actions act)
        {
            resources_and_installations_shared_timer = new SharedProperties{ Timer = new Handler.Timer()};
            resources = new(act,resources_and_installations_shared_timer);
            installations = new(act, resources_and_installations_shared_timer);
            recherche = new(act);
            chantierSpatial_and_defense_shared_timer = new SharedProperties{ Timer = new Handler.Timer()};
            chantierSpatial = new(act, chantierSpatial_and_defense_shared_timer);
            defense = new(act, chantierSpatial_and_defense_shared_timer);
        }
    }
}