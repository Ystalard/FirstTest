using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace FirstTest
{
    public interface IPlanet
{
    Coordinates Coordinates { get; set; }

    IChantierSpatial ChantierSpatial();
    IDefense Defense();
    IInstallations Installations();
    IRecherche Recherche();
    IResources Resources();
    IFleet Flotte(Actions act);
}

    public class Coordinates
    {
        public int galaxy;
        public int solarSystem;
        public int position;

        public Coordinates(int galaxy, int solarSystem, int position)
        {
            this.galaxy = galaxy;
            this.solarSystem = solarSystem;
            this.position = position;
        }

        public static implicit operator Coordinates((int galaxy, int solarSystem, int position) tuple)
        {
            return new Coordinates(tuple.galaxy, tuple.solarSystem, tuple.position);
        }
    }
    
    public class Planet: IPlanet
    {
        #region "properties"
        #region "public properties"
        public string name;
        public int galaxy;
        public int solarSystem;
        public int position;

        public (int min, int max) temperatureRange;
        public (int used, int max) cases;

        public Coordinates Coordinates {
            get{
                return (galaxy, solarSystem, position);
            }
            set{
                galaxy = value.galaxy;
                solarSystem = value.solarSystem;
                position = value.position;
            }
        }
        #endregion "public property"

        #region "private properties"
        private Resources resources;
        private Installations installations;
        private Recherche recherche;
        private ChantierSpatial chantierSpatial;
        private Defense defense;
        private Fleet flotte;

        SharedProperties resources_and_installations_shared_timer;
        SharedProperties chantierSpatial_and_defense_shared_timer;
        #endregion "private properties"
        #endregion "properties"
        
        
        #region "constructor"
        public Planet(Actions act, string name, (int min, int max) temperatureRange, (int used, int max) cases, (int galaxy, int solarSystem, int position) coordinates, Delegate.MovingFleetEventHandler addMovingFleet)
        {
            this.name = name;
            this.temperatureRange = temperatureRange;
            this.cases = cases;
            this.Coordinates = coordinates;
            resources_and_installations_shared_timer = new SharedProperties{ Timer = new Handler.Timer()};
            resources = new Resources(act,resources_and_installations_shared_timer, this);
            installations = new Installations(act, resources_and_installations_shared_timer, this);
            recherche = new Recherche(act, this);
            chantierSpatial_and_defense_shared_timer = new SharedProperties{ Timer = new Handler.Timer()};
            chantierSpatial = new ChantierSpatial(act, chantierSpatial_and_defense_shared_timer, this);
            defense = new Defense(act, chantierSpatial_and_defense_shared_timer, this);
            flotte = new Fleet(act, this, addMovingFleet);
        }
        #endregion "constructor"

        #region "methods"
        #region "public methods"
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

        public IFleet Flotte(Actions act)
        {
            NavigationMenu.GoTo(NavigationMenu.Menu.Flotte, act);
            return flotte;
        }
        #endregion "public methods"
        #endregion "methods"
    }
}