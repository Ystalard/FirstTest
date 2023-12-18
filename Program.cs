namespace FirstTest;
using FirstTest.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using FirstTest.Handler;
using System.Diagnostics;
using System.Runtime.Versioning;



#pragma warning disable CS8618
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
            }finally{

            }
            
            
            // if(cookieManager.cookiedLoaded){
            //     Connector.GoToLastGame(ref act);
            // }else{
                Connector.Connect(ref act); //connect and start the game
            // }
            Resources resources = new(ref act);
        Start:
        try{ 
            if(refresh){
                bool initializeResources = true;
                if(MyDriver.ElementExists(settings.Login.LastGame)){
                    Connector.GoToLastGame(ref act);
                }
                else
                {
                    IWebElement validate_on_correct_page = MyDriver.FindElement(settings.Nav.Ressources); //if it does not exist it will throw an issue.
                    initializeResources = false;
                }

                if(initializeResources){
                    act = new(_driver);
                    resources = new(ref act);
                }
                NavigationMenu.GoTo(NavigationMenu.Menu.Ressources, ref act, force: true);
                refresh = false; //the refresh solve the issue so we reset the value;
            }
            
            while(true){   
                if(resources.IsBusy(ref act)){
                    continue;
                }

                string cssSelectorNextResourceToBuild = Resources.NextResourceToBuild(ref act);  

                if(resources.CanBuildResource(cssSelectorNextResourceToBuild, ref act)){
                    if(resources.HaveEnoughEnergie(cssSelectorNextResourceToBuild, ref act)){
                        resources.DevelopResource(cssSelectorNextResourceToBuild, ref act);
                    }else{
                        if(resources.CanBuildResource(settings.Supplies.CentraleSolaire, ref act)){
                            resources.DevelopResource(settings.Supplies.CentraleSolaire, ref act);
                        }else{
                            resources.WaitForResourcesAvailable(cssSelectorNextResourceToBuild, ref act);
                        }
                    }
                }else{
                    resources.WaitForResourcesAvailable(cssSelectorNextResourceToBuild, ref act);
                }
            }
        }
        catch(Exception ex){            
            act = new(_driver);
            if(!refresh){
                _driver?.Navigate().Refresh();
                refresh = true;
                goto Start;
            }else{
                // refresh did not succeed

                Debug.WriteLine($"message: {ex.Message}");
                Debug.WriteLine($"stack trace: {ex.StackTrace}");

                int InnerExceptionCount = 0;
                while(ex.InnerException != null){
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
#pragma warning restore CS8618