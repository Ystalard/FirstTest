namespace FirstTest;

using System;
using OpenQA.Selenium.Interactions;
using FirstTest.Handler;
using OpenQA.Selenium;

public class Resources: Buildable
{
    #region  "constructor"
    public Resources(Actions act): base(act, Menu.Ressources){}

    public Resources(Actions act, SharedProperties sharedProperties): base(act, Menu.Ressources, sharedProperties){}
    #endregion "constructor"

    #region "method"

    #region "public method"
    #region "Develop"
    public void DevelopMetal()
    {
        Develop(Program.settings.Supplies.MineMetal);
    }
    
    public void DevelopCristal()
    {
        Develop(Program.settings.Supplies.MineCristal);
    }
    
    public void DevelodDeuterium()
    {
        Develop(Program.settings.Supplies.MineDeuterium);
    }
    public void BuildCentraleSolaire()
    {
        Develop(Program.settings.Supplies.CentraleSolaire);
    }

    public void BuildCentraleFusion()
    {
        Develop(Program.settings.Supplies.CentraleFusion);
    }

    public void BuildSatteliteSolaire(int missing_energie){
        OpenDetails(Program.settings.Supplies.SatelitteSolaire);

        IWebElement satelitte = MyDriver.FindElement(Program.settings.Supplies.TechnologyDetails.EnergieGainPerSatelitte);
        int energieGainPerHour = int.Parse(satelitte.GetAttribute("data-value"));
        int numberSatelitteToDevelop = (int)Math.Ceiling(missing_energie / (float)energieGainPerHour);
        
        DevelopResource(Program.settings.Supplies.SatelitteSolaire, numberSatelitteToDevelop);
    }

    public void DevelopEnergie(int missing_energie){
        if(GetCurrentLevel(Program.settings.Supplies.CentraleSolaire) < 16){
            if(CanBuildResource(Program.settings.Supplies.CentraleSolaire)){
                BuildCentraleSolaire();
            }else{
                WaitForResourcesAvailable(Program.settings.Supplies.CentraleSolaire);
            }
        }else{
            BuildSatteliteSolaire(missing_energie);
        }
    }

    public void BuildHangarMetal()
    {
        Develop(Program.settings.Supplies.HangarMetal);
    }

    public void BuildHangarCristal()
    {
        Develop(Program.settings.Supplies.HangarCristal);
    }

    public void BuildHangarDeut()
    {
        Develop(Program.settings.Supplies.HangarDeuterium);
    }

    public void BuildForeuse()
    {
        Develop(Program.settings.Supplies.Foreuse);
    }

    public int EnergieRequired()
    {
        return GetResourceRequired(Program.settings.Supplies.TechnologyDetails.EnergieNecessaire);
    }
    #endregion

    #region "Level resources"
    public static int LevelMetal()
    {
        return GetCurrentLevel(Program.settings.Supplies.LevelMetal);
    }

    public static int LevelCristal()
    {
        return GetCurrentLevel(Program.settings.Supplies.LevelCristal);
    }

    public static int LevelDeuterium()
    {
        return GetCurrentLevel(Program.settings.Supplies.LevelDeuterium);
    }

    public static int LevelCentralSolaire()
    {
        return GetCurrentLevel(Program.settings.Supplies.LevelCentralSolaire);
    }

    public static int LevelCentralFusion()
    {
        return GetCurrentLevel(Program.settings.Supplies.LevelCentralFusion);
    }

    public static int AmountSatelitteSolaire()
    {
        return GetCurrentLevel(Program.settings.Supplies.AmountSatelitteSolaire);
    }

    public static int LevelhangarMetal()
    {
        return GetCurrentLevel(Program.settings.Supplies.LevelHangarMetal);
    }

    public static int LevelhangarCristal()
    {
        return GetCurrentLevel(Program.settings.Supplies.LevelHangarCristal);
    }

    public static int LevelhangarDeuterium()
    {
        return GetCurrentLevel(Program.settings.Supplies.LevelHangarDeuterium);
    }

    public static int AmountForeuse()
    {
        return GetCurrentLevel(Program.settings.Supplies.AmountForeuse);
    }
    #endregion

    #region "builder"
    public string NextResourceToBuild()
    {
        GoTo(Menu.Ressources, act);
        int levelMetal = LevelMetal();
        if (levelMetal < 3) return Program.settings.Supplies.MineMetal;

        int expectedLevelCristal = levelMetal - 2;
        int expectedLevelDeuterium = expectedLevelCristal - 2;
        int levelCristal = LevelCristal();
        int levelDeuterium = LevelDeuterium();

        if(expectedLevelCristal == levelCristal) {
            if(levelDeuterium < expectedLevelDeuterium) return Program.settings.Supplies.MineDeuterium;
            
            return Program.settings.Supplies.MineMetal;
        }  

        if (levelDeuterium == expectedLevelDeuterium)
        {
            if(levelCristal < expectedLevelCristal) return Program.settings.Supplies.MineCristal;
            
            return Program.settings.Supplies.MineMetal;
        }

        
        if(levelCristal > expectedLevelCristal)
        {
            if (levelDeuterium < expectedLevelDeuterium) return Program.settings.Supplies.MineDeuterium;
          
            return Program.settings.Supplies.MineMetal; 
        }

        if(levelDeuterium < expectedLevelDeuterium)
        {
            if(expectedLevelCristal - levelCristal > expectedLevelDeuterium - levelDeuterium) return Program.settings.Supplies.MineCristal;

            return Program.settings.Supplies.MineDeuterium;
        }

        return  Program.settings.Supplies.MineCristal;
    }

    public bool CanBuildResource(string cssSelector)
    {
        return CanBuildElement(cssSelector);      
    }
    
    public bool HaveEnoughEnergie(string cssSelector, ref int missing_energie)
    {
        GoTo(menu, act);
        OpenDetails(cssSelector);
        
        missing_energie = GetCurrentEnergie() - EnergieRequired();
        if(missing_energie > - 3)
        {
            return true;
        }

        return false;
    }

    public void DevelopResource(string resource) {
        Develop(resource);
    }

    #endregion
    #endregion
    #endregion
}