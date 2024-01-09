using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstTest.Handler;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;

namespace FirstTest
{
    public class Empire
    {
        private List<(string id,Planet planet)> planets = [];
        Actions act;

        #region "constructor"
        public Empire(Actions act)
        {
            this.act = act;
            var parentElement = MyDriver.FindElement(Program.settings.Empire.PlanetList);

            var planetElements = parentElement.FindElements(By.TagName("div"));
            for (int i = 0; i < planetElements.Count; i++)
            {
                IWebElement planet = MyDriver.FindElement(Program.settings.Empire.PlanetList).FindElements(By.TagName("div"))[i];
                act.MoveToElement(planet).Build().Perform();
                string name = planet.FindElement(By.ClassName("planet-name")).Text;
                string id = "#" + planet.GetAttribute("id");
                IWebElement tooltip = MyDriver.FindElement(Program.settings.Tooltip);
                
                string innerText = tooltip.GetAttribute("innerText");

                // Extract cases used and maximum cases available
                Match casesMatch = Regex.Match(innerText, @"\((\d+)/(\d+)\)");
                (int used, int max) cases = (0,0);
                (int galaxy, int solarSystem, int position) coordinates = (0,0,0);
                (int min, int max) temperatureRange = (0,0);
                if (casesMatch.Success)
                {
                    cases.used = int.Parse(casesMatch.Groups[1].Value);
                    cases.max = int.Parse(casesMatch.Groups[2].Value);
                }
                else
                {
                    throw new MustRestartException($"cases value not found.");
                }

                // Extract min and max temperature
                Match tempMatch = Regex.Match(innerText, @"(\d+) °C à (\d+)°C");
                if (tempMatch.Success)
                {
                    temperatureRange.min = int.Parse(tempMatch.Groups[1].Value);
                    temperatureRange.max = int.Parse(tempMatch.Groups[2].Value);
                }
                else
                {
                    throw new MustRestartException($"temperatures value not found.");
                }

                // Extract coordinates
                Match coordMatch = Regex.Match(innerText, @"\[(\d+):(\d+):(\d+)\]");
                if (coordMatch.Success)
                {
                    coordinates.galaxy = int.Parse(coordMatch.Groups[1].Value);
                    coordinates.solarSystem = int.Parse(coordMatch.Groups[2].Value);
                    coordinates.position = int.Parse(coordMatch.Groups[3].Value);
                }
                else
                {
                    throw new MustRestartException($"coordinates value not found.");
                }
                
                act.Click().Build().Perform();
                Planet planet1 = new Planet(act, name, temperatureRange, cases, coordinates);
                planets.Add((id, planet1));
            }
        }
        #endregion "constructor"

        public IPlanet GoToPlanet(string planetName)
        {
            IWebElement planet = MyDriver.FindElement(planets.Find(planet => planet.planet.name == planetName).id);
            MyDriver.MoveToElement(planet, act).Click().Build().Perform();
            
            bool planetSelected = false;
            while(!planetSelected)
            {
                planet = MyDriver.FindElement(planets.Find(planet => planet.planet.name == planetName).id);
                planetSelected = planet.GetAttribute("class").Contains("hightlightPlanet");
            };

            return planets.Find(planet => planet.planet.name == planetName).planet;
        }

        public IPlanet GoToPlanet(int position)
        {
            IWebElement planet = MyDriver.FindElement(planets[position].id);
            MyDriver.MoveToElement(planet, act).Click().Build().Perform();
            bool planetSelected = false;
            while(!planetSelected)
            {
                planet = MyDriver.FindElement(planets[position].id);
                planetSelected = planet.GetAttribute("class").Contains("hightlightPlanet");
            };

            return planets[position].planet;
        }
    
        #region "private"
        
        #endregion "private"
    }
}