﻿namespace FirstTest;
using FirstTest.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using Microsoft.Extensions.Configuration;
using FirstTest.Handler;
using System.Diagnostics;

class Program
{
    public static IWebDriver _driver;
    public static string originalWindow;
    public static string originalTitleWindow;
    public static string originalUrl;

    public static AppSettings settings;

    public static readonly Random random = new(); //aim to make the bot more human
    static void Main(string[] args)
    {
        Init:
        bool refresh = false;
        
              
            IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Accéder aux valeurs de configuration
                settings = config.Get<AppSettings>() ?? throw new Exception("configuration is not provided.");

            //Instantiate driver
            ChromeOptions options = new()
            {
                BinaryLocation = settings.Chrome,
            }; // Optional
            _driver = new ChromeDriver(settings.ChromeDriver, options);
            
            
            
            _driver.Navigate().GoToUrl(settings.Login.Url);
            // Load cookies
            // CookieManager cookieManager = new CookieManager(_driver, settings.Cookies);
            // cookieManager.LoadCookies();
            // _driver.Navigate().Refresh();

            
            
            originalWindow = _driver.CurrentWindowHandle;
            originalTitleWindow = _driver.Title;
            originalUrl = _driver.Url;

            Actions act = new(_driver);
            
            try{
                //refuse cookie                
                act.Pause(TimeSpan.FromSeconds(1)).Build().Perform();
                act.MoveToElement(_driver.FindElement(By.CssSelector(settings.Cookie.CookieManageButtonSelector))).Click().Build().Perform();
            
                act.MoveToElement(_driver.FindElement(By.CssSelector(settings.Cookie.CookieRefuseButtonSelector))).Click().Build().Perform();
            }
            finally
            {

            }
            
            Connector.Connect(act);

            Empire empire = new(act);
            empire.GoToPlanet(0);
        Start:
        try{ 
            if(refresh)
            {
                bool initializeResources = true;
                if(MyDriver.ElementExists(settings.Login.LastGame))
                {
                    Connector.GoToLastGame(act);
                }
                else
                {
                    IWebElement validate_on_correct_page = MyDriver.FindElement(settings.Nav.Ressources); //if it does not exist it will throw an issue.
                    initializeResources = false;
                }

                if(initializeResources)
                {
                    act = new(_driver);
                    empire = new(act);
                    empire.GoToPlanet(0);
                }
                // NavigationMenu.GoTo(NavigationMenu.Menu.Ressources, act, force: true);
                refresh = false; //the refresh solve the issue so we reset the value;
            }
            
            while(true)
            {   
                IFleet fleet = empire.GoToPlanet(0).Flotte(act);
                int count_chasseur_leger_available = fleet.GetChasseurLegerAvailable;
                if(count_chasseur_leger_available > 0){
                    fleet.AddChasseurLeger(count_chasseur_leger_available);
                    fleet.Continue();

                    Coordinates coord = empire.GoToPlanet(0).Coordinates;

                    fleet.TargetPlanet(coord.galaxy, coord.solarSystem, coord.position + 2);

                    DateTime arrival = fleet.GetArrivalTime();
                    DateTime returnTime = fleet.GetArrivalTime();
            
                    if(fleet.GetStatus() == Fleet.TargetStatus.noob)
                    {
                        fleet.SelectAttack();
                    }
                    else
                    {
                        fleet.SelectTransport();
                    }

                    fleet.LoadAllResources();

                    fleet.selectStep(2);
                    DateTime arrival2 = fleet.GetArrivalTime();
                    DateTime returnTime2 = fleet.GetArrivalTime();

                    fleet.SendFleet();

                }
            }
        }
        catch(Exception ex)
        {            
            act = new(_driver);
            if(!refresh)
            {
                _driver?.Navigate().Refresh();
                refresh = true;
                goto Start;
            }
            else
            {
                // refresh did not succeed

                Debug.WriteLine($"message: {ex.Message}");
                Debug.WriteLine($"stack trace: {ex.StackTrace}");

                int InnerExceptionCount = 0;
                while(ex.InnerException != null)
                {
                    Debug.WriteLine($"InnerException {InnerExceptionCount}: {ex.InnerException?.ToString()}");
                    InnerExceptionCount++;
                    if(ex.InnerException != null)
                        {ex = ex.InnerException;}
                }
                _driver?.Quit();
                goto Init;
            }
        }
    }
}