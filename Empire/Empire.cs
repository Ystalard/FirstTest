using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstTest.Handler;
using OpenQA.Selenium;

namespace FirstTest
{
    public class Empire
    {
        List<Planet> planets;

        public void GoToPlanet(string planetName)
        {
            var parentElement = MyDriver.FindElement(Program.settings.Empire.PlanetList);
            var planetElements = parentElement.FindElements(By.ClassName("planet-name"));

            foreach (var planet in planetElements)
            {
                if (planet.Text.Equals(planetName, StringComparison.OrdinalIgnoreCase))
                {
                    planet.Click();
                    break;
                }
            }
        }
    }
}