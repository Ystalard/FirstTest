namespace FirstTest;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Handler;


public class NavigationMenu
{
    private static Menu IsOpened = Menu.Undefined;
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

    public static void GoTo(Menu menu, ref Actions act, bool force = false){
        //force: when we want to refresh the resource wrapper.
        if(!force && IsOpened == menu){
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

        if(!force){
            if (Opened_on(target_menu)){
                IsOpened = menu;
                return;
            }  
        }else{
            IsOpened = Menu.Undefined;
        }

        MyDriver.MoveToElement(target_menu, ref act).Click().Build().Perform();
        int count=0;
        while(!Opened_on(target_menu) || count > 200){ //in order to wait the lag
            count++;
        };

        if(count > 200) {
            throw new MustRestartException($"Can't go to menu {menu} with css selector = {target_menu.ToString()}");
        }

        IsOpened = menu;
        return;
    }

    private static bool Opened_on(string cssSelector){
        try{
            IWebElement element = MyDriver.FindElement(cssSelector);
            return element.GetAttribute("class").Contains("selected");
        }catch(Exception){
            throw new MustRestartException();
        }
        
    }
}