namespace FirstTest;
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

            
            Planet planet = new(act);
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
                    planet = new(act);
                }
                // NavigationMenu.GoTo(NavigationMenu.Menu.Ressources, act, force: true);
                refresh = false; //the refresh solve the issue so we reset the value;
            }
            
            while(true)
            {   
                if(planet.defense.CanBuildLanceurMissile() && planet.defense.AmountLanceurMissile() < 1)
                {
                    planet.defense.DevelopLanceurMissile(1);
                }

                if(planet.defense.CanBuildLanceurMissile() && planet.defense.AmountLanceurMissile() < 1)
                {
                    planet.defense.DevelopLanceurMissile(4);
                }

                if(planet.defense.CanBuildLanceurMissile() && planet.defense.AmountLanceurMissile() > 4)
                {
                    planet.defense.DevelopLanceurMissile(4);
                }

                if(planet.defense.CanBuildLanceurMissile() && planet.defense.AmountLanceurMissile() > 8)
                {
                    planet.defense.DevelopLanceurMissile(4);
                }


                if(!planet.chantierSpatial.IsBusy())
                {
                    if(planet.chantierSpatial.CanBuildChasseurLeger() && planet.chantierSpatial.AmountChasseurLeger() < 1)
                    {
                        planet.chantierSpatial.DevelopChasseurLeger(1);
                    }
                }

                if(!planet.recherche.IsBusy())
                {
                    if(planet.recherche.CanBuildTechnoEnergie() && planet.recherche.GetLevelTechnoEnergie() < 1)
                    {
                        planet.recherche.BuildTechnoEnergie();
                    }
                    else if(planet.recherche.CanBuildReacteurCombustion() && planet.recherche.GetLevelReacteurCombustion() < 1)
                    {
                        planet.recherche.BuildReacteurCombustion();
                    }
                    else if(planet.recherche.GetLevelTechnoEnergie() < 1)
                    {
                        planet.recherche.WaitForResourcesAvailable(settings.Recherche.TechnoEnergie);
                    }
                    else if(planet.recherche.GetLevelReacteurCombustion() < 1)
                    {
                        planet.recherche.WaitForResourcesAvailable(settings.Recherche.ReacteurCombustion);
                    }
                }

                if(!planet.installations.IsBusy())
                {
                    if(planet.installations.CanBuildUsineRobot() && planet.installations.GetLevelUsineRobot() < 2)
                    {
                        planet.installations.BuildUsineRobot();
                    }
                    else if(planet.installations.CanBuildChantierSpatial() && planet.installations.GetLevelChantierSpatial() < 1)
                    {
                        planet.installations.BuildChantierSpatiale();
                    }
                    else if(planet.installations.CanBuildLaboRecherche() && planet.installations.GetLevelLaboRecherche() < 1)
                    {
                        planet.installations.BuildLaboRecherche();
                    }
                    else if(planet.installations.GetLevelUsineRobot() < 2)
                    {
                        planet.installations.WaitForResourcesAvailable(settings.Facilities.UsineRobot);
                    }
                    else if(planet.installations.GetLevelChantierSpatial() < 1)
                    {
                        planet.installations.WaitForResourcesAvailable(settings.Facilities.ChantierSpatial);
                    }
                    else if(planet.installations.GetLevelLaboRecherche() < 1)
                    {
                        planet.installations.WaitForResourcesAvailable(settings.Facilities.LaboRecherche);
                    }
                }   

                if(!planet.resources.IsBusy())
                {
                    string cssSelectorNextResourceToBuild = planet.resources.NextResourceToBuild();  

                    if(planet.resources.CanBuildResource(cssSelectorNextResourceToBuild))
                    {
                        int missing_energie = 0;
                        if(planet.resources.HaveEnoughEnergie(cssSelectorNextResourceToBuild, ref missing_energie))
                        {
                            planet.resources.DevelopResource(cssSelectorNextResourceToBuild);
                        }else{
                            planet.resources.DevelopEnergie(missing_energie);
                        }
                    }else{
                        planet.resources.WaitForResourcesAvailable(cssSelectorNextResourceToBuild);
                    }
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