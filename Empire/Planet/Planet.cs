using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace FirstTest
{
    public interface IPlanet
{
    (int galaxy, int solarSystem, int position) Coordinate { get; set; }

    IChantierSpatial ChantierSpatial();
    IDefense Defense();
    IInstallations Installations();
    IRecherche Recherche();
    IResources Resources();
}

    public class Planet: IPlanet
    {
        public string name;
        public int galaxy;
        public int solarSystem;
        public int position;

        public (int min, int max) temperatureRange;
        public (int used, int max) cases;

        public (int galaxy, int solarSystem, int position) Coordinate {
            get{
                return (galaxy, solarSystem, position);
            }
            set{
                galaxy = value.galaxy;
                solarSystem = value.solarSystem;
                position = value.position;
            }
        }

        private Resources resources;
        private Installations installations;
        private Recherche recherche;
        private ChantierSpatial chantierSpatial;
        private Defense defense;

        SharedProperties resources_and_installations_shared_timer;
        SharedProperties chantierSpatial_and_defense_shared_timer;

        public Planet(Actions act, string name, (int min, int max) temperatureRange, (int used, int max) cases, (int galaxy, int solarSystem, int position) coordinates)
        {
            this.name = name;
            this.temperatureRange = temperatureRange;
            this.cases = cases;
            this.Coordinate = coordinates;
            resources_and_installations_shared_timer = new SharedProperties{ Timer = new Handler.Timer()};
            resources = new Resources(act,resources_and_installations_shared_timer);
            installations = new Installations(act, resources_and_installations_shared_timer);
            recherche = new Recherche(act);
            chantierSpatial_and_defense_shared_timer = new SharedProperties{ Timer = new Handler.Timer()};
            chantierSpatial = new ChantierSpatial(act, chantierSpatial_and_defense_shared_timer);
            defense = new Defense(act, chantierSpatial_and_defense_shared_timer);
        }

        public IResources Resources()
        {
            return resources;
        }
        public IInstallations Installations()
        {
            return installations;
        }
        public IDefense Defense()
        {
            return defense;
        }
        public IChantierSpatial ChantierSpatial()
        {
            return chantierSpatial;
        }
        public IRecherche Recherche()
        {
            return recherche;
        }
    }
}