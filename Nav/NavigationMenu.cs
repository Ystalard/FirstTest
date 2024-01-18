namespace FirstTest;

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Handler;


public abstract class NavigationMenu
{
    #region private properties
    private static Menu IsOpened = Menu.Undefined;
    private static Planet current_planet = null;
    #endregion

    #region enum
    public enum Menu{
        ApercuDirectives,
        VueEnsemble,
        Ressources,
        FormeDeVie,
        Installations,
        Marchand,
        Recherche,
        ChantierSpatial,
        Defense,
        Flotte,
        Galaxie,
        Empire,
        Alliance,
        MessageOfficier,
        Boutique,
        Recompenses,
        Undefined
    }
    #endregion

    #region public
    public static void GoTo(Menu menu, Actions act, bool force = false)
    {
        //force: when we want to refresh the resource wrapper.
        if(!force && IsOpened == menu)
        {
            return;
        }
        
        string target_menu = menu switch
        {
            Menu.ApercuDirectives => Program.settings.Nav.ApercuDirectives,
            Menu.VueEnsemble => Program.settings.Nav.VueEnsemble,
            Menu.Ressources => Program.settings.Nav.Ressources,
            Menu.FormeDeVie => Program.settings.Nav.FormeDeVie,
            Menu.Installations => Program.settings.Nav.Installations,
            Menu.Marchand => Program.settings.Nav.Marchand,
            Menu.Recherche => Program.settings.Nav.Recherche,
            Menu.ChantierSpatial => Program.settings.Nav.ChantierSpatial,
            Menu.Defense => Program.settings.Nav.Defense,
            Menu.Flotte => Program.settings.Nav.Flotte,
            Menu.Galaxie => Program.settings.Nav.Galaxie,
            Menu.Empire => Program.settings.Nav.Empire,
            Menu.Alliance => Program.settings.Nav.Alliance,
            Menu.MessageOfficier => Program.settings.Nav.MessageOfficier,
            Menu.Boutique => Program.settings.Nav.Boutique,
            Menu.Recompenses => Program.settings.Nav.Recompenses,
            _ => Program.settings.Nav.VueEnsemble,
        };

        if(!force)
        {
            if (Opened_on(target_menu))
            {
                IsOpened = menu;
                return;
            }  
        }else{
            IsOpened = Menu.Undefined;
        }

        MyDriver.MoveToElement(target_menu, act).Click().Build().Perform();
        act.Pause(TimeSpan.FromSeconds( 2 + Program.random.NextDouble())).Build().Perform();

        IsOpened = menu;
        return;
    }
    
    public IPlanet GoToPlanet(string planetName, List<(string id, Planet planet)> planets, Actions act)
    {
        if(current_planet != null && planetName == current_planet.name)
        {
            return current_planet;
        }

        IWebElement planet = MyDriver.FindElement(planets.Find(planet => planet.planet.name == planetName).id);
        MyDriver.MoveToElement(planet, act).Click().Build().Perform();
        
        bool planetSelected = false;
        while(!planetSelected)
        {
            planet = MyDriver.FindElement(planets.Find(planet => planet.planet.name == planetName).id);
            planetSelected = planet.GetAttribute("class").Contains("hightlightPlanet");
        };

        current_planet = planets.Find(planet => planet.planet.name == planetName).planet;
        return planets.Find(planet => planet.planet.name == planetName).planet;
    }

    public IPlanet GoToPlanet(int position, List<(string id, Planet planet)> planets, Actions act)
    {
        if(current_planet != null && planets[position].planet.name == current_planet.name)
        {
            return current_planet;
        }

        IWebElement planet = MyDriver.FindElement(planets[position].id);
        MyDriver.MoveToElement(planet, act).Click().Build().Perform();
        bool planetSelected = false;
        while(!planetSelected)
        {
            planet = MyDriver.FindElement(planets[position].id);
            if(planets.Count > 1)
            {
                planetSelected = planet.GetAttribute("class").Contains("hightlightPlanet");
            }
            else
            {
                planetSelected = true;
            }
        };

        current_planet = planets[position].planet;
        return planets[position].planet;
    }
    #endregion

    #region protected
    /// <summary>
    /// Open the details tab of the element
    /// </summary>
    /// <param name="cssSelector">cssSelector of the element to check</param>
    protected void OpenDetails(string cssSelector, Menu menu, Actions act)
    { 
        GoTo(menu, act);
        //when the details tab is opened there is a closebutton visible.
        if(MyDriver.ElementExists(Program.settings.Details.CloseButton))
        {
            IWebElement element = MyDriver.FindElement(cssSelector);

            // check either details tab is the correct one
            if(!element.GetAttribute("class").Contains("showsDetails") || !MyDriver.ElementExists(Program.settings.Details.Develop))
            {
                //close the details as the wrong details is opened or must be refreshed
                MyDriver.MoveToElement(Program.settings.Details.CloseButton, act).Click().Build().Perform();
                MyDriver.AssertElementDisappear(Program.settings.Details.CloseButton);
                act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();

                // open the details by clicking on the element.
                MyDriver.MoveToElement(cssSelector, act).Click().Build().Perform();
                act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();
                act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();
            }
        }
        else
        {
            // open the details by clicking on the element.
            MyDriver.MoveToElement(cssSelector, act).Click().Build().Perform();
            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();
            act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();
        }        

        if(!Details_opened(Program.settings.Details.CloseButton))
        {
            GoTo(menu, act);//re-load the resources page by clicking on the resources nav button.
            MyDriver.MoveToElement(cssSelector, act).Click().Build().Perform();// open the details by clicking on the resource.

            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();

            if(!Details_opened(Program.settings.Details.CloseButton))
            {
                throw new MustRestartException();
            }
        }

        if(!Details_opened_on(cssSelector))
        {
            throw new MustRestartException();
        }
    }

    protected void OpenDetails(IWebElement element, Menu menu, Actions act)
    { 
        GoTo(menu, act);
        //when the details tab is opened there is a closebutton visible.
        if(MyDriver.ElementExists(Program.settings.Details.CloseButton))
        {
            // check either details tab is the correct one
            if(!element.GetAttribute("class").Contains("showsDetails") || !MyDriver.ElementExists(Program.settings.Details.Develop))
            {
                //close the details as the wrong details is opened or must be refreshed
                MyDriver.MoveToElement(element, act).Click().Build().Perform();
                MyDriver.AssertElementDisappear(Program.settings.Details.CloseButton);
                act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();

                // open the details by clicking on the resource.
            MyDriver.MoveToElement(element, act).Click().Build().Perform();
            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();
            act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();
            }
        }
        else
        {
            // open the details by clicking on the resource.
            MyDriver.MoveToElement(element, act).Click().Build().Perform();
            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();
            act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();
        }

        if(!Details_opened(Program.settings.Details.CloseButton))
        {
            GoTo(menu, act);//re-load the resources page by clicking on the resources nav button.
            MyDriver.MoveToElement(element, act).Click().Build().Perform();// open the details by clicking on the resource.

            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();

            if(!Details_opened(Program.settings.Details.CloseButton))
            {
                throw new MustRestartException();
            }
        }

        if(!Details_opened_on(element))
        {
            throw new MustRestartException();
        }
    }
    #endregion

    #region  private
    private static bool Opened_on(string cssSelector)
    {
        try{
            IWebElement element = MyDriver.FindElement(cssSelector);
            return element.GetAttribute("class").Contains("selected");
        }
        catch(Exception)
        {
            throw new MustRestartException();
        }
        
    }

    private static bool Details_opened(string cssSelector)
    {
        return MyDriver.FindElement(cssSelector) != null;
    }        

    private static bool Details_opened_on(string element_to_check)
    {
        return MyDriver.CheckElementContains(cssSelector: element_to_check, attribute: "class", content: "showsDetails");
    }

    private static bool Details_opened_on(IWebElement element_to_check)
    {
        return MyDriver.CheckElementContains(element: element_to_check, attribute: "class", content: "showsDetails");
    }
    #endregion
}