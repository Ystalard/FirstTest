namespace FirstTest;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;
using System.Diagnostics;

public class Resources: NavigationMenu
{
    #region "property"
    #region "public property"
    private TimeSpan? RemainingTime {
        get{
            if(timer == null){
                return null;
            }
            if(timer.CheckIsRunning){
                TimeSpan time = timer.GetTimeSpan();
            
                if(TimeToBuild <= time){
                    timer.StopTimer(); // the construction of the resource ends
                    TimeToBuild = TimeSpan.Zero;
                    return TimeSpan.Zero; // no remaining time to wait.
                }

                return TimeToBuild - time; // return the remaining time to wait.
            }
            
            // timer is not running so there is no construction in coming.
            return TimeSpan.Zero; // no remaining time to wait.          
            
        }
    }
    #endregion

    #region "private property"
    private TimeSpan TimeToBuild {get; set;}
    
    private readonly Timer timer = new();

    private string Set_details_opened_on_resource = "";
    private Type Get_details_opened_on_resource => DictionnarySupply[Set_details_opened_on_resource];
    static readonly Dictionary<string, Type> DictionnarySupply = new()
    {
        {Program.settings.Supplies.MineMetal, Type.Metal},
        {Program.settings.Supplies.MineCristal, Type.Cristal},
        {Program.settings.Supplies.MineDeuterium, Type.Deuterium},
        {Program.settings.Supplies.CentraleSolaire, Type.CentraleSolaire},
        {"", Type.Undefined}
    };
    #endregion
    #endregion

    #region "enum"
    enum Type{
        Metal,
        Cristal,
        Deuterium,
        CentraleSolaire,
        Undefined
    }
    #endregion

    #region  "constructor"
    public Resources(ref Actions act){
        GoTo(Menu.Ressources, ref act);
        CheckDecompteTimer(ref act);
    }
    #endregion

    #region "method"
    #region "private method"
    
    private void CheckDecompteTimer(ref Actions act){
        GoTo(Menu.Ressources, ref act);

        // we want to be sure everything is loaded before checking the DecompteTempsDeConstruction element. 
        // It is important not to get wrong here as it would impact the timer process. Which is a tricky thing (timer is running on a different tread).
        act.Pause(TimeSpan.FromSeconds(Program.random.Next(1,2))).Build().Perform(); 

        if(MyDriver.ElementExists(Program.settings.Supplies.DecompteTempsDeConstruction)){
            IWebElement resourceInConstruction = MyDriver.FindElement(Program.settings.Supplies.DecompteTempsDeConstruction);
            TimeToBuild = Iso8601Duration.Parse(resourceInConstruction.GetAttribute("datetime"));
            timer.StartTimer();
        }
    }
    private static TimeSpan GetTimeToBuild(string cssSelector){  
        return Iso8601Duration.Parse(MyDriver.FindElement(Program.settings.Supplies.TechnologyDetails.TempsProduction).GetAttribute("datetime"));
    }

    private void OpenDetails(String ressource_to_check, ref Actions act){
        if(Get_details_opened_on_resource == DictionnarySupply[ressource_to_check]){
            if(MyDriver.ElementExists(Program.settings.Supplies.TechnologyDetails.CloseButton)){
                //close the details as it needs to be refreshed.
                MyDriver.MoveToElement(Program.settings.Supplies.TechnologyDetails.CloseButton, ref act).Click().Build().Perform();
            }
            Set_details_opened_on_resource = "";
            MyDriver.AssertElementDisappear(Program.settings.Supplies.TechnologyDetails.CloseButton);
        }

        // open the details by clicking on the resource.
        MyDriver.MoveToElement(ressource_to_check, ref act).Click().Build().Perform();
        act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();
        Set_details_opened_on_resource = ressource_to_check;
        act.Pause(TimeSpan.FromSeconds(1 + Program.random.NextDouble())).Build().Perform();

        if(!Details_opened()){
            Set_details_opened_on_resource = "";
            GoTo(Menu.Ressources, ref act);//re-load the resources page by clicking on the resources nav button.
            MyDriver.MoveToElement(ressource_to_check, ref act).Click().Build().Perform();// open the details by clicking on the resource.

            act.Pause(TimeSpan.FromSeconds(2 + Program.random.NextDouble())).Build().Perform();

            if(!Details_opened()){
                throw new MustRestartException();
            }
        }

        if(!Details_opened_on(ressource_to_check)){
            throw new MustRestartException();
        }

    }

    private static bool Details_opened(){
        return MyDriver.FindElement(Program.settings.Supplies.TechnologyDetails.CloseButton) != null;
    }

    private static bool Details_opened_on(String ressource_to_check){
            return MyDriver.CheckElementContains(cssSelector: ressource_to_check, attribute: "class", content: "showsDetails");
    }

    private static int GetCurrentResource(string cssSelector){
        return int.Parse(MyDriver.FindElement(cssSelector).GetAttribute("data-raw"));
    }

    private static int GetCurrentLevel(string cssSelector, ref Actions act){
        IWebElement level = MyDriver.FindElement(cssSelector);
        return int.Parse(level.GetAttribute("data-value"));
    }

    private static int GetResourceRequired(string cssSelector){
        try{
            return int.Parse(MyDriver.FindElement(cssSelector).GetAttribute("data-value"));
        }catch(Exception){
            return 0;
        }
        
    }

    private static int GetProductionPerHour(string cssSelector, ref Actions act){
        MyDriver.MoveToElement(cssSelector, ref act).Build().Perform(); // hover the ressource
        act.Pause(TimeSpan.FromSeconds(2)).Build().Perform(); // wait for js to display the tooltip
        IWebElement tooltip = MyDriver.FindElement(Program.settings.ProductionTooltip); // access tooltip
        return int.Parse(tooltip.Text.TrimStart('+'));
    }
        
        
        
        
    #endregion

    #region "public method"
    #region "Develop"
    public void DevelopMetal(ref Actions act){
        DevelopResource(Program.settings.Supplies.MineMetal, ref act);
    }
    
    public void DevelopCristal(ref Actions act){
        DevelopResource(Program.settings.Supplies.MineCristal, ref act);
    }
    public void DevelodDeuterium(ref Actions act){
        DevelopResource(Program.settings.Supplies.MineDeuterium, ref act);
    }
    public void BuildCentraleSolaire(ref Actions act){
        DevelopResource(Program.settings.Supplies.CentraleSolaire, ref act);
    }

    public void BuildCentraleFusion(ref Actions act){
        DevelopResource(Program.settings.Supplies.CentraleFusion, ref act);
    }

    public void BuildSatteliteSolaire(int missing_energie, ref Actions act){
        OpenDetails(Program.settings.Supplies.SatelitteSolaire, ref act);

        IWebElement satelitte = MyDriver.FindElement(Program.settings.Supplies.TechnologyDetails.EnergieGainPerSatelitte);
        int energieGainPerHour = int.Parse(satelitte.GetAttribute("data-value"));
        int numberSatelitteToDevelop = (int)Math.Ceiling(missing_energie / (float)energieGainPerHour);
        
        DevelopResource(Program.settings.Supplies.SatelitteSolaire, numberSatelitteToDevelop, ref act);
    }

    public void BuildHangarMetal(ref Actions act){
        DevelopResource(Program.settings.Supplies.HangarMetal, ref act);
    }

    public void BuildHangarCristal(ref Actions act){
        DevelopResource(Program.settings.Supplies.HangarCristal, ref act);
    }

    public void BuildHangarDeut(ref Actions act){
        DevelopResource(Program.settings.Supplies.HangarDeuterium, ref act);
    }

    public void BuildForeuse(ref Actions act){
        DevelopResource(Program.settings.Supplies.Foreuse, ref act);
    }

    public static int GetCurrentMetal(){
        return GetCurrentResource(Program.settings.Resources.Metal);
    }

    public static int GetCurrentDeut(){
        return GetCurrentResource(Program.settings.Resources.Deuterium);
    }
    public static int GetCurrentCristal(){
        return GetCurrentResource(Program.settings.Resources.Cristal);
    }
    public static int GetCurrentEnergie(){
        return GetCurrentResource(Program.settings.Resources.Energie);
    }

    public static int MetalRequired(){
        return GetResourceRequired(Program.settings.Supplies.TechnologyDetails.MetalRequested);
    }

    public static int CristalRequired(){
        return GetResourceRequired(Program.settings.Supplies.TechnologyDetails.CristalRequired);
    }
    public static int DeuteriumRequired(){
        return GetResourceRequired(Program.settings.Supplies.TechnologyDetails.DeuteriumRequired);
    }

    public static int EnergieRequired(){
        return GetResourceRequired(Program.settings.Supplies.TechnologyDetails.EnergieNecessaire);
    }
    #endregion

    #region "Level resources"
    public static int LevelMetal(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.LevelMetal, ref act);
    }

    public static int LevelCristal(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.LevelCristal, ref act);
    }

    public static int LevelDeuterium(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.LevelDeuterium, ref act);
    }

    public static int LevelCentralSolaire(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.LevelCentralSolaire, ref act);
    }

    public static int LevelCentralFusion(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.LevelCentralFusion, ref act);
    }

    public static int AmountSatelitteSolaire(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.AmountSatelitteSolaire, ref act);
    }

    public static int LevelhangarMetal(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.LevelHangarMetal, ref act);
    }

    public static int LevelhangarCristal(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.LevelHangarCristal, ref act);
    }

    public static int LevelhangarDeuterium(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.LevelHangarDeuterium, ref act);
    }

    public static int AmountForeuse(ref Actions act){
        return GetCurrentLevel(Program.settings.Supplies.AmountForeuse, ref act);
    }
    #endregion

    #region "builder"
    public static string NextResourceToBuild(ref Actions act){
        GoTo(Menu.Ressources, ref act);
        int levelMetal = LevelMetal(ref act);
        if (levelMetal < 3) return Program.settings.Supplies.MineMetal;

        int expectedLevelCristal = levelMetal - 2;
        int expectedLevelDeuterium = expectedLevelCristal - 2;
        int levelCristal = LevelCristal(ref act);
        int levelDeuterium = LevelDeuterium(ref act);

        if(expectedLevelCristal == levelCristal) {
            if(levelDeuterium < expectedLevelDeuterium) return Program.settings.Supplies.MineDeuterium;
            
            return Program.settings.Supplies.MineMetal;
        }  

        if (levelDeuterium == expectedLevelDeuterium){
            if(levelCristal < expectedLevelCristal) return Program.settings.Supplies.MineCristal;
            
            return Program.settings.Supplies.MineMetal;
        }

        
        if(levelCristal > expectedLevelCristal){
            if (levelDeuterium < expectedLevelDeuterium) return Program.settings.Supplies.MineDeuterium;
          
            return Program.settings.Supplies.MineMetal; 
        }

        if(levelDeuterium < expectedLevelDeuterium){
            if(expectedLevelCristal - levelCristal > expectedLevelDeuterium - levelDeuterium) return Program.settings.Supplies.MineCristal;

            return Program.settings.Supplies.MineDeuterium;
        }

        return  Program.settings.Supplies.MineCristal;
    }

    public bool HaveResourceToBuild(string cssSelector, ref Actions act){
        OpenDetails(cssSelector, ref act);
        
        if(MetalRequired() <= GetCurrentMetal() && CristalRequired() <= GetCurrentCristal()){
            return true;
        }

        return false;
    }

    public bool IsBusy(ref Actions act){
        TimeSpan? remainingTime = RemainingTime;
        if (remainingTime != null && timer.CheckIsRunning){
            return true;
        }

        // CheckDecompteTimer(ref act);
        return false;
    }

    public bool CanBuildResource(string cssSelector, ref Actions act){
        if(IsBusy(ref act)){
            return false;
        } 

        GoTo(Menu.Ressources, ref act);

        try{
            IWebElement buildElement;
            if (cssSelector == Program.settings.Supplies.MineMetal)
            {
                if(!MyDriver.ElementExists(Program.settings.Supplies.DevelopMetal)){
                    return false;
                }
                buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopMetal);
            }
            else if (cssSelector == Program.settings.Supplies.MineCristal)
            {
                if(!MyDriver.ElementExists(Program.settings.Supplies.DevelopCristal)){
                    return false;
                }
                buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopCristal);
            }
            else if (cssSelector == Program.settings.Supplies.MineDeuterium)
            {
                if(!MyDriver.ElementExists(Program.settings.Supplies.DevelopDeuterium)){
                    return false;
                }
                buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopDeuterium);
            }
            else if (cssSelector == Program.settings.Supplies.CentraleSolaire)
            {
                if(!MyDriver.ElementExists(Program.settings.Supplies.DevelopCentralSolaire)){
                    return false;
                }
                buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopCentralSolaire);
            }
            else
            {
                throw new Handler.NotImplementedException();
            }

        }catch(WebDriverTimeoutException){
            return false;
        }

        return true;         
    }
    
    public bool HaveEnoughEnergie(string cssSelector, ref Actions act, ref int missing_energie){
        GoTo(Menu.Ressources, ref act);
        OpenDetails(cssSelector, ref act);
        
        missing_energie = GetCurrentEnergie() - EnergieRequired();
        if(missing_energie > - 3){
            return true;
        }

        return false;
    }

    public void DevelopEnergie(int missing_energie, ref Actions act){
        if(GetCurrentLevel(Program.settings.Supplies.CentraleSolaire, ref act) < 16){
            if(CanBuildResource(Program.settings.Supplies.CentraleSolaire, ref act)){
                BuildCentraleSolaire(ref act);
            }else{
                WaitForResourcesAvailable(Program.settings.Supplies.CentraleSolaire, ref act);
            }
        }else{
            BuildSatteliteSolaire(missing_energie, ref act);
        }
        
    }

    public void DevelopResource(string resource, ref Actions act) {
        GoTo(Menu.Ressources, ref act);
        OpenDetails(resource, ref act);
        TimeToBuild = GetTimeToBuild(resource);
        
        IWebElement buildElement;
        if (resource == Program.settings.Supplies.MineMetal)
        {
            buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopMetal);
        }
        else if (resource == Program.settings.Supplies.MineCristal)
        {
            buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopCristal);
        }
        else if (resource == Program.settings.Supplies.MineDeuterium)
        {
            buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopDeuterium);
        }
        else if (resource == Program.settings.Supplies.CentraleSolaire)
        {
            buildElement = MyDriver.FindElement(Program.settings.Supplies.DevelopCentralSolaire);
        }
        else
        {
            throw new Handler.NotImplementedException();
        }

        MyDriver.MoveToElement(buildElement, ref act).Click().Build().Perform();
        
        Set_details_opened_on_resource = "";
        timer.StartTimer();
    }

    public void DevelopResource(string resource, int number, ref Actions act) {
        GoTo(Menu.Ressources, ref act);
        OpenDetails(resource, ref act);
        

        int numberToBuild = 0;
        int maximum = 0;
        if(resource == Program.settings.Supplies.SatelitteSolaire){
            maximum = int.Parse(MyDriver.FindElement(Program.settings.Supplies.TechnologyDetails.BuildAmount).GetAttribute("max"));
            numberToBuild = maximum > number ? number : maximum;
        }

        if(maximum == 0){
            WaitForResourcesAvailable(resource, ref act);
        }else{
            TimeToBuild = GetTimeToBuild(resource) * numberToBuild;
            MyDriver.MoveToElement(Program.settings.Supplies.TechnologyDetails.BuildAmount, ref act).Click().SendKeys(numberToBuild.ToString());
            MyDriver.MoveToElement(Program.settings.Supplies.TechnologyDetails.Develop, ref act).Click().Build().Perform();

            Set_details_opened_on_resource = "";
            timer.StartTimer();
        }
    }

    public void WaitForResourcesAvailable(string resource, ref Actions act){
        GoTo(Menu.Ressources, ref act);
        OpenDetails(resource, ref act);

        int metal = GetCurrentMetal();
        int cristal = GetCurrentCristal();
        int deuterium = GetCurrentDeut();

        int metalRequired = MetalRequired();
        int cristalRequired = CristalRequired();
        int deuteriumRequired = DeuteriumRequired();

        int missing_metal = metalRequired < metal ? 0 : metalRequired - metal;
        int missing_cristal = cristalRequired < cristal ? 0 : cristalRequired - cristal;
        int missing_deuterium = deuteriumRequired < deuterium ? 0 : deuteriumRequired - deuterium;

        if(missing_metal + missing_cristal + missing_deuterium == 0){
            // resources are available.
            return;
        }

        // collect production per hours
        int metalProduction = GetProductionPerHour(Program.settings.Resources.Metal, ref act);
        int cristalProduction = GetProductionPerHour(Program.settings.Resources.Cristal, ref act);
        int deuteriumProduction = GetProductionPerHour(Program.settings.Resources.Deuterium, ref act);

        TimeSpan timeToGetMetal = TimeSpan.FromHours((double)missing_metal / metalProduction);
        TimeSpan timeToGetCristal = TimeSpan.FromHours((double)missing_cristal / cristalProduction);
        TimeSpan timeToGetDeuterium = TimeSpan.FromHours((double)missing_deuterium / deuteriumProduction);

        TimeSpan timeToWait = timeToGetMetal < timeToGetCristal ? (timeToGetCristal < timeToGetDeuterium ? timeToGetDeuterium : timeToGetCristal) : timeToGetMetal < timeToGetDeuterium ? timeToGetDeuterium : timeToGetMetal; 

        if(timeToWait != TimeSpan.Zero){
            TimeToBuild = timeToWait;
            timer.StartTimer();
        }
    }

    #endregion
    #endregion
    #endregion
}