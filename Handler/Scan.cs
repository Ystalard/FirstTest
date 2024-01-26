using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTest
{
    public partial class Fleet
    { 
        public class ScanOptions
        {
            public class RangeGalaxy
            {
                public int min = 1;
                public int max = 4; 
            }
            public class RangeSolarSystem
            {
                public int min = 1;
                public int max = 499; 
            }
            public class RangePosition
            {
                public int min = 1;
                public int max = 16; 
            }
            public RangeGalaxy rangeGalaxy;
            public RangeSolarSystem rangeSolarSystem;
            public RangePosition rangePosition;
        }

        #region definition
        public class Galaxy
        {
            public int galaxy;
            public List<SolarSystem> solarSystems;
            public Galaxy(int galaxy)
            {
                this.galaxy = galaxy;
            }
        }

        public class SolarSystem
        {
            public int galaxy;
            public int solarSystem;
            public List<Planet> planets;
            public SolarSystem(int galaxy, int solarSystem)
            {
                this.galaxy = galaxy;
                this.solarSystem = solarSystem;
            }

        }

        public class Planet
        {
            public string name;
            public Coordinates coordinates;

            public int metal;
            public int crystal;
            public int deuterium;
            public Status status;

            public Planet(int galaxy, int solarSystem, int position)
            {
                coordinates = (galaxy, solarSystem, position);
            }
        }

        #region enum
        public enum Status
        {
            common,
            noob,
            strong,
            inactive
        }
    #endregion
    #endregion

        #region scan
        public T Scan<T>(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            if(typeof(T) == typeof(List<Galaxy>))
            {
                if(Cordinates_of_planetFrom_are_inside_scanRange(scanOptions))
                {
                    return (T)(object)ScanAll_galaxyFrom_Inside(ScanPrerogative, scanOptions);
                }

                return (T)(object)ScanAll_galaxyFrom_Outside(ScanPrerogative, scanOptions);
            }

            if(typeof(T) == typeof(List<SolarSystem>))
            {
                return (T)(object)ScanGalaxy(ScanPrerogative, scanOptions);
            }

            if(typeof(T) == typeof(List<Coordinates>))
            {
                return (T)(object)ScanSolarSystem(ScanPrerogative, scanOptions);
            }

            if(typeof(T) == typeof(Coordinates))
            {
                if(Cordinates_of_planetFrom_are_inside_scanRange(scanOptions))
                {
                    return (T)(object)ScanAll_returnNearest(ScanPrerogative, scanOptions);
                }
                return (T)(object)ScanAll_returnFirst(ScanPrerogative, scanOptions);
            }

            throw new NotImplementedException();
        }

        private bool Cordinates_of_planetFrom_are_inside_scanRange(ScanOptions scanOptions)
        {
            if(scanOptions.rangeGalaxy.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangeGalaxy.max )
            {
                return false;
            }

            if(scanOptions.rangeSolarSystem.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangeSolarSystem.max )
            {
                return false;
            }

            if(scanOptions.rangePosition.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangePosition.max )
            {
                return false;
            }

            return true;
        }
        public List<Coordinates> ScanSolarSystem(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;

            List<Coordinates> solarSystem = new();
            if(scanOptions.rangePosition.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangePosition.max )
            {
                positionFrom = Math.Min(scanOptions.rangePosition.min - positionFrom, positionFrom - scanOptions.rangePosition.max);
            }

            bool firstLoop = true;
            for (int position_offset = 0; position_offset <= 15; position_offset++)
                {
                    bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= 16;
                    if (planets_being_scanned_on_positive_offset)
                    {
                        TargetPlanet(galaxyFrom, solarSystemFrom, positionFrom + position_offset);
                        if(ScanPrerogative.Invoke())
                        {
                            solarSystem.Add((galaxyFrom, solarSystemFrom, positionFrom + position_offset));
                        }
                    }

                    if(!firstLoop)
                    {
                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset > 0;
                        if (planets_being_scanned_on_negative_offset && position_offset != 0)
                        {
                            TargetPlanet(galaxyFrom, solarSystemFrom, positionFrom - position_offset);
                            if(ScanPrerogative.Invoke())
                            {   
                                solarSystem.Add((galaxyFrom, solarSystemFrom, positionFrom - position_offset));
                            }
                        }

                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }

                    firstLoop = false;
                }
            return solarSystem;
        }
        private List<SolarSystem> ScanGalaxy(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            
            List<SolarSystem> result = new();
            
            if(scanOptions.rangeSolarSystem.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangeSolarSystem.max )
            {
                solarSystemFrom = Math.Min(scanOptions.rangeSolarSystem.min - solarSystemFrom, solarSystemFrom - scanOptions.rangeSolarSystem.max);
            }

            if(scanOptions.rangePosition.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangePosition.max )
            {
                positionFrom = Math.Min(scanOptions.rangePosition.min - positionFrom, positionFrom - scanOptions.rangePosition.max);
            }
            bool firstLoop = true;
            for (int solarSystem_offset = 0; solarSystem_offset < int.Parse(Program.settings.Univers.SolarSystemCount); solarSystem_offset++)
            {
                SolarSystem solarSystem = new(galaxyFrom, solarSystemFrom + solarSystem_offset);
                bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= int.Parse(Program.settings.Univers.SolarSystemCount);
                bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset > 0;
                
                if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                {
                    // scan of the current galaxy ends
                    break;
                }

                for (int position_offset = 0; position_offset <= 15; position_offset++)
                {
                    bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= 16;
                    if (solar_system_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                    {
                        TargetPlanet(galaxyFrom, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                        if(ScanPrerogative.Invoke())
                        {
                            Planet planet = new(galaxyFrom, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            solarSystem.planets.Add(planet);
                        }
                    }

                    if(!firstLoop)
                    {
                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset > 0;
                        if (solar_system_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset && !(solarSystem_offset == 0 && position_offset == 0))
                        {
                            TargetPlanet(galaxyFrom, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                            if(ScanPrerogative.Invoke())
                            {   
                                Planet planet = new(galaxyFrom, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                                solarSystem.planets.Add(planet);
                            }
                        }

                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }

                    firstLoop = false;
                }

                if(solarSystem.planets.Count > 0)
                {
                    result.Add(solarSystem);
                }
            }

            return result;
        }
        private List<Galaxy> ScanAll_galaxyFrom_Inside(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            
            List<Galaxy> galaxies = new();
            bool firstLoop = true;
            for (int galaxy_offset = 0; galaxy_offset < Math.Max(scanOptions.rangeGalaxy.max - galaxyFrom, galaxyFrom - scanOptions.rangeGalaxy.min) ; galaxy_offset++)
            {
                Galaxy galaxy = new(galaxyFrom + galaxy_offset);
                bool univers_being_scanned_on_positive_offset = galaxyFrom + galaxy_offset <= Math.Min(scanOptions.rangeGalaxy.max, int.Parse(Program.settings.Univers.GalaxyCount));
                bool univers_being_scanned_on_negative_offset = galaxyFrom - galaxy_offset >= Math.Max(scanOptions.rangeGalaxy.min, 1);

                if (!univers_being_scanned_on_positive_offset && !univers_being_scanned_on_negative_offset)
                {
                    // scan of the univers ends.
                    break;
                }

                for (int solarSystem_offset = 0; solarSystem_offset < Math.Max(scanOptions.rangeSolarSystem.max - solarSystemFrom, solarSystemFrom - scanOptions.rangeSolarSystem.min); solarSystem_offset++)
                {
                    SolarSystem solarSystem = new(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset);
                    bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= Math.Min(scanOptions.rangeSolarSystem.max, int.Parse(Program.settings.Univers.SolarSystemCount));
                    bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset >= Math.Max(scanOptions.rangeSolarSystem.min, 1);
                    
                    if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                    {
                        // scan of the current galaxy ends
                        break;
                    }

                    for (int position_offset = 0; position_offset <= Math.Max(scanOptions.rangePosition.max - positionFrom, positionFrom - scanOptions.rangePosition.min); position_offset++)
                    {
                        bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= Math.Min(scanOptions.rangePosition.max,16);
                        if (univers_being_scanned_on_positive_offset && solar_system_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            if(ScanPrerogative.Invoke())
                            {
                                solarSystem.planets.Add(new(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset));
                            }
                        }

                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset >= Math.Max(scanOptions.rangePosition.min,1);
                        if(!firstLoop)
                        {
                            if (univers_being_scanned_on_negative_offset && solar_system_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset)
                            {
                                TargetPlanet(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                                if(ScanPrerogative.Invoke())
                                {   
                                    solarSystem.planets.Add(new(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset));
                                }
                            }
                        }

                        firstLoop = false;
                        
                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }

                    if(solarSystem.planets.Count > 0 )
                    {
                        galaxy.solarSystems.Add(solarSystem);
                    }
                }

                if(galaxy.solarSystems.Count > 0)
                {
                    galaxies.Add(galaxy);
                }
            }

            return galaxies;
        }
        private List<Galaxy> ScanAll_galaxyFrom_Outside(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            
            List<Galaxy> galaxies = new();
            if(scanOptions.rangeGalaxy.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangeGalaxy.max )
            {
                galaxyFrom = Math.Min(scanOptions.rangeGalaxy.min - galaxyFrom, galaxyFrom - scanOptions.rangeGalaxy.max);
            }

            if(scanOptions.rangeSolarSystem.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangeSolarSystem.max )
            {
                solarSystemFrom = Math.Min(scanOptions.rangeSolarSystem.min - solarSystemFrom, solarSystemFrom - scanOptions.rangeSolarSystem.max);
            }

            if(scanOptions.rangePosition.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangePosition.max )
            {
                positionFrom = Math.Min(scanOptions.rangePosition.min - positionFrom, positionFrom - scanOptions.rangePosition.max);
            }
            
            bool firstLoop = true;
            for (int galaxy_offset = 0; galaxy_offset < Math.Max(scanOptions.rangeGalaxy.max - galaxyFrom, galaxyFrom - scanOptions.rangeGalaxy.min) ; galaxy_offset++)
            {
                Galaxy galaxy = new(galaxyFrom + galaxy_offset);
                bool univers_being_scanned_on_positive_offset = galaxyFrom + galaxy_offset <= Math.Min(scanOptions.rangeGalaxy.max, int.Parse(Program.settings.Univers.GalaxyCount));
                bool univers_being_scanned_on_negative_offset = galaxyFrom - galaxy_offset >= Math.Max(scanOptions.rangeGalaxy.min, 1);

                if (!univers_being_scanned_on_positive_offset && !univers_being_scanned_on_negative_offset)
                {
                    // scan of the univers ends.
                    break;
                }

                for (int solarSystem_offset = 0; solarSystem_offset < Math.Max(scanOptions.rangeSolarSystem.max - solarSystemFrom, solarSystemFrom - scanOptions.rangeSolarSystem.min); solarSystem_offset++)
                {
                    SolarSystem solarSystem = new(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset);
                    bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= Math.Min(scanOptions.rangeSolarSystem.max, int.Parse(Program.settings.Univers.SolarSystemCount));
                    bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset >= Math.Max(scanOptions.rangeSolarSystem.min, 1);
                    
                    if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                    {
                        // scan of the current galaxy ends
                        break;
                    }

                    for (int position_offset = 0; position_offset <= Math.Max(scanOptions.rangePosition.max - positionFrom, positionFrom - scanOptions.rangePosition.min); position_offset++)
                    {
                        bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= Math.Min(scanOptions.rangePosition.max,16);
                        if (univers_being_scanned_on_positive_offset && solar_system_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            if(ScanPrerogative.Invoke())
                            {
                                solarSystem.planets.Add(new(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset));
                            }
                        }

                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset >= Math.Max(scanOptions.rangePosition.min,1);
                        if(!firstLoop)
                        {
                            if (univers_being_scanned_on_negative_offset && solar_system_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset)
                            {
                                TargetPlanet(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                                if(ScanPrerogative.Invoke())
                                {   
                                    solarSystem.planets.Add(new(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset));
                                }
                            }
                        }

                        firstLoop = false;
                        
                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }

                    if(solarSystem.planets.Count > 0 )
                    {
                        galaxy.solarSystems.Add(solarSystem);
                    }
                }

                if(galaxy.solarSystems.Count > 0)
                {
                    galaxies.Add(galaxy);
                }
            }

            return galaxies;
        }
        private Coordinates ScanAll_returnNearest(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            
            List<Galaxy> result = new();
            
            bool firstLoop = true;
            for (int galaxy_offset = 0; galaxy_offset < Math.Max(scanOptions.rangeGalaxy.max - galaxyFrom, galaxyFrom - scanOptions.rangeGalaxy.min) ; galaxy_offset++)
            {
                Galaxy galaxy = new(galaxyFrom + galaxy_offset);
                bool univers_being_scanned_on_positive_offset = galaxyFrom + galaxy_offset <= Math.Min(scanOptions.rangeGalaxy.max, int.Parse(Program.settings.Univers.GalaxyCount));
                bool univers_being_scanned_on_negative_offset = galaxyFrom - galaxy_offset >= Math.Max(scanOptions.rangeGalaxy.min, 1);

                if (!univers_being_scanned_on_positive_offset && !univers_being_scanned_on_negative_offset)
                {
                    // scan of the univers ends.
                    break;
                }

                for (int solarSystem_offset = 0; solarSystem_offset < Math.Max(scanOptions.rangeSolarSystem.max - solarSystemFrom, solarSystemFrom - scanOptions.rangeSolarSystem.min); solarSystem_offset++)
                {
                    SolarSystem solarSystem = new(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset);
                    bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= Math.Min(scanOptions.rangeSolarSystem.max, int.Parse(Program.settings.Univers.SolarSystemCount));
                    bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset >= Math.Max(scanOptions.rangeSolarSystem.min, 1);
                    
                    if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                    {
                        // scan of the current galaxy ends
                        break;
                    }

                    for (int position_offset = 0; position_offset <= Math.Max(scanOptions.rangePosition.max - positionFrom, positionFrom - scanOptions.rangePosition.min); position_offset++)
                    {
                        bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= Math.Min(scanOptions.rangePosition.max,16);
                        if (univers_being_scanned_on_positive_offset && solar_system_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            if(ScanPrerogative.Invoke())
                            {
                                return (galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            }
                        }

                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset >= Math.Max(scanOptions.rangePosition.min,1);
                        if(!firstLoop)
                        {
                            if (univers_being_scanned_on_negative_offset && solar_system_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset)
                            {
                                TargetPlanet(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                                if(ScanPrerogative.Invoke())
                                {   
                                    return (galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                                }
                            }
                        }

                        firstLoop = false;
                        
                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }
                }
            }

            return null;
        }
        private Coordinates ScanAll_returnFirst(Delegate.ScanPrerogative ScanPrerogative, ScanOptions scanOptions)
        {
            Coordinates coordinates_from = planetFrom.Coordinates;
            int galaxyFrom = coordinates_from.galaxy;
            int solarSystemFrom = coordinates_from.solarSystem;
            int positionFrom = coordinates_from.position;
            
            if(scanOptions.rangeGalaxy.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangeGalaxy.max )
            {
                galaxyFrom = Math.Min(scanOptions.rangeGalaxy.min - galaxyFrom, galaxyFrom - scanOptions.rangeGalaxy.max);
            }

            if(scanOptions.rangeSolarSystem.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangeSolarSystem.max )
            {
                solarSystemFrom = Math.Min(scanOptions.rangeSolarSystem.min - solarSystemFrom, solarSystemFrom - scanOptions.rangeSolarSystem.max);
            }

            if(scanOptions.rangePosition.min > planetFrom.Coordinates.galaxy || planetFrom.Coordinates.galaxy > scanOptions.rangePosition.max )
            {
                positionFrom = Math.Min(scanOptions.rangePosition.min - positionFrom, positionFrom - scanOptions.rangePosition.max);
            }
            
            bool firstLoop = true;
            for (int galaxy_offset = 0; galaxy_offset < Math.Max(scanOptions.rangeGalaxy.max - galaxyFrom, galaxyFrom - scanOptions.rangeGalaxy.min) ; galaxy_offset++)
            {
                Galaxy galaxy = new(galaxyFrom + galaxy_offset);
                bool univers_being_scanned_on_positive_offset = galaxyFrom + galaxy_offset <= Math.Min(scanOptions.rangeGalaxy.max, int.Parse(Program.settings.Univers.GalaxyCount));
                bool univers_being_scanned_on_negative_offset = galaxyFrom - galaxy_offset >= Math.Max(scanOptions.rangeGalaxy.min, 1);

                if (!univers_being_scanned_on_positive_offset && !univers_being_scanned_on_negative_offset)
                {
                    // scan of the univers ends.
                    break;
                }

                for (int solarSystem_offset = 0; solarSystem_offset < Math.Max(scanOptions.rangeSolarSystem.max - solarSystemFrom, solarSystemFrom - scanOptions.rangeSolarSystem.min); solarSystem_offset++)
                {
                    SolarSystem solarSystem = new(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset);
                    bool solar_system_being_scanned_on_positive_offset = solarSystemFrom + solarSystem_offset <= Math.Min(scanOptions.rangeSolarSystem.max, int.Parse(Program.settings.Univers.SolarSystemCount));
                    bool solar_system_being_scanned_on_negative_offset = solarSystemFrom - solarSystem_offset >= Math.Max(scanOptions.rangeSolarSystem.min, 1);
                    
                    if (!solar_system_being_scanned_on_positive_offset && !solar_system_being_scanned_on_negative_offset)
                    {
                        // scan of the current galaxy ends
                        break;
                    }

                    for (int position_offset = 0; position_offset <= Math.Max(scanOptions.rangePosition.max - positionFrom, positionFrom - scanOptions.rangePosition.min); position_offset++)
                    {
                        bool planets_being_scanned_on_positive_offset = positionFrom + position_offset <= Math.Min(scanOptions.rangePosition.max,16);
                        if (univers_being_scanned_on_positive_offset && solar_system_being_scanned_on_positive_offset && planets_being_scanned_on_positive_offset)
                        {
                            TargetPlanet(galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            if(ScanPrerogative.Invoke())
                            {
                                return (galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                            }
                        }

                        bool planets_being_scanned_on_negative_offset = positionFrom - position_offset >= Math.Max(scanOptions.rangePosition.min,1);
                        if(!firstLoop)
                        {
                            if (univers_being_scanned_on_negative_offset && solar_system_being_scanned_on_negative_offset && planets_being_scanned_on_negative_offset)
                            {
                                TargetPlanet(galaxyFrom - galaxy_offset, solarSystemFrom - solarSystem_offset, positionFrom - position_offset);
                                if(ScanPrerogative.Invoke())
                                {   
                                    return (galaxyFrom + galaxy_offset, solarSystemFrom + solarSystem_offset, positionFrom + position_offset);
                                }
                            }
                        }

                        firstLoop = false;
                        
                        if(planets_being_scanned_on_positive_offset && planets_being_scanned_on_negative_offset)
                        {
                            //scan of the current solar system ends
                            break;
                        }
                    }
                }
            }

            return null;
        }
        #endregion
    }
}