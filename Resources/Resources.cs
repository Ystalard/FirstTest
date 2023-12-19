namespace FirstTest;

using System;
using OpenQA.Selenium.Interactions;

public class Resources: Buildable
{
    #region  "constructor"
    public Resources(Actions act): base(act, Menu.Ressources)
    {
        GoTo(menu, act);
        CheckDecompteTimer(menu);
    }
    #endregion

    #region "method"
    #region "private method"
    
    protected override TimeSpan GetTimeToBuild()
    {  
        return GetTimeToBuild(Program.settings.Supplies.TechnologyDetails.TempsProduction);
    }

    protected override void OpenDetails(string ressource_to_check)
    {
        OpenDetails(ressource_to_check, Program.settings.Supplies.TechnologyDetails.CloseButton);
    }
    #endregion

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

    public void BuildSatteliteSolaire()
    {
        Develop(Program.settings.Supplies.SatelitteSolaire);
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

    public override int MetalRequired()
    {
        return GetResourceRequired(Program.settings.Supplies.TechnologyDetails.MetalRequested);
    }

    public override int CristalRequired()
    {
        return GetResourceRequired(Program.settings.Supplies.TechnologyDetails.CristalRequired);
    }
    public override int DeuteriumRequired()
    {
        return GetResourceRequired(Program.settings.Supplies.TechnologyDetails.DeuteriumRequired);
    }

    public override int EnergieRequired()
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

    public override bool HaveResourceToBuild(string cssSelector)
    {
        OpenDetails(cssSelector);
        
        if(MetalRequired() <= GetCurrentMetal() && CristalRequired() <= GetCurrentCristal())
        {
            return true;
        }

        return false;
    }

    public bool CanBuildResource(string cssSelector)
    {
        return CanBuildElement(cssSelector);      
    }
    
    public bool HaveEnoughEnergie(string cssSelector)
    {
        GoTo(menu, act);
        OpenDetails(cssSelector);
        
        if(GetCurrentEnergie() - EnergieRequired() > - 3)
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