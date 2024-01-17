namespace FirstTest;

using System;
using OpenQA.Selenium.Interactions;
using FirstTest.Handler;
using OpenQA.Selenium;

public interface IResources: IBuildable
{
    void BuildBestSuitableEnergie(int missing_energie);
    void BuildCentraleFusion();
    void BuildCentraleSolaire();
    void BuildForeuse(int number);
    void BuildHangarCristal();
    void BuildHangarDeut();
    void BuildHangarMetal();
    void BuildSatellite(int number);
    bool CanBuildResource(string cssSelector);
    void DevelodDeuterium();
    void DevelopCristal();
    void DevelopEnergie(int missing_energie);
    void DevelopMetal();
    void BuildResource(string resource);
    int EnergieRequired();
    bool HaveEnoughEnergie(string cssSelector, ref int missing_energie);
    string NextResourceToBuild();
}

public class Resources: Buildable, IResources
{
    #region  "constructor"
    public Resources(Actions act, IPlanet planet): base(act, Menu.Ressources, planet){}

    public Resources(Actions act, SharedProperties sharedProperties, IPlanet planet): base(act, Menu.Ressources, sharedProperties, planet){}
    #endregion "constructor"

    #region "method"
    #region "public method"
    #region "Develop"
    public void DevelopMetal()
    {
        Develop(Program.settings.Supplies.DevelopMetal);
    }
    
    public void DevelopCristal()
    {
        Develop(Program.settings.Supplies.DevelopCristal);
    }
    
    public void DevelodDeuterium()
    {
        Develop(Program.settings.Supplies.DevelopDeuterium);
    }
    public void BuildCentraleSolaire()
    {
        Develop(Program.settings.Supplies.DevelopCentralSolaire);
    }

    public void BuildSatellite(int number)
    {
        Develop(Program.settings.Supplies.SatelitteSolaire, number);
    }

    public void BuildCentraleFusion()
    {
        Develop(Program.settings.Supplies.DevelopCentralFusion);
    }

    public void BuildBestSuitableEnergie(int missing_energie){
        OpenDetails(Program.settings.Supplies.CentraleSolaire, menu, act);
        IWebElement central_solaire = MyDriver.FindElement(Program.settings.Supplies.TechnologyDetails.EnergieGainPerSatellite);
        int energieGainWithCentralSolaire = int.Parse(central_solaire.GetAttribute("data-value"));
        int central_solaire_cost = MetalRequired() + CristalRequired();

        OpenDetails(Program.settings.Supplies.SatelitteSolaire, menu, act);
        IWebElement satellite = MyDriver.FindElement(Program.settings.Supplies.TechnologyDetails.EnergieGainPerSatellite);
        int energieGainPerSatellite = int.Parse(satellite.GetAttribute("data-value"));
        int number_satellite_to_reach_central_solaire_energy = (int)Math.Round(energieGainWithCentralSolaire / (float)energieGainPerSatellite);
        int satellite_cost = number_satellite_to_reach_central_solaire_energy * (CristalRequired() + DeuteriumRequired()) * 4; // let consider we might rebuild the satellite 3 times (due to three attack)

        if(satellite_cost < central_solaire_cost)
        {
            int numberSatelitteToDevelop = (int)Math.Ceiling(missing_energie / (float)energieGainPerSatellite);
            BuildSatellite(numberSatelitteToDevelop);
        }
        else
        {
            BuildCentraleSolaire();
        }
    }

    public void DevelopEnergie(int missing_energie){
        if(GetCurrentLevel(Program.settings.Supplies.LevelCentralSolaire) < int.Parse(Program.settings.Supplies.LvlMaxCentralSolaire)){
            if(CanBuildResource(Program.settings.Supplies.CentraleSolaire)){
                BuildCentraleSolaire();
            }else{
                WaitForResourcesAvailable(Program.settings.Supplies.CentraleSolaire);
            }
        }else{
            BuildBestSuitableEnergie(missing_energie);
        }
    }

    public void BuildHangarMetal()
    {
        Develop(Program.settings.Supplies.DevelopHangarMetal);
    }

    public void BuildHangarCristal()
    {
        Develop(Program.settings.Supplies.DevelopHangarCristal);
    }

    public void BuildHangarDeut()
    {
        Develop(Program.settings.Supplies.DevelopHangarDeuterium);
    }

    /// <summary>
    /// (M + C + D) * 8 = max number foreuse
    /// </summary>
    public void BuildForeuse(int number)
    {
        int maximum = (LevelMetal() + LevelCristal() + LevelDeuterium()) * 8 - GetCurrentLevel(Program.settings.Supplies.AmountForeuse);
        Develop(Program.settings.Supplies.Foreuse, number > maximum ? maximum : number);
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
        OpenDetails(cssSelector, menu, act);
        
        missing_energie = GetCurrentEnergie() - EnergieRequired();
        if(missing_energie > - 3)
        {
            return true;
        }

        return false;
    }

    public void BuildResource(string resource) {
        if(resource == Program.settings.Supplies.MineMetal)
        {
            resource = Program.settings.Supplies.DevelopMetal;
        }
        else if(resource == Program.settings.Supplies.MineCristal)
        {
            resource = Program.settings.Supplies.DevelopCristal;
        }
        else if(resource == Program.settings.Supplies.MineDeuterium)
        {
            resource = Program.settings.Supplies.DevelopDeuterium;
        }
        else if(resource == Program.settings.Supplies.CentraleSolaire)
        {
            resource = Program.settings.Supplies.DevelopCentralSolaire;
        }
        else if(resource == Program.settings.Supplies.CentraleFusion)
        {
            resource = Program.settings.Supplies.DevelopCentralFusion;
        }
        else if(resource == Program.settings.Supplies.HangarMetal)
        {
            resource = Program.settings.Supplies.DevelopHangarMetal;
        }
        else if(resource == Program.settings.Supplies.HangarCristal)
        {
            resource = Program.settings.Supplies.DevelopHangarCristal;
        }
        else if(resource == Program.settings.Supplies.HangarDeuterium)
        {
            resource = Program.settings.Supplies.DevelopHangarDeuterium;
        }
        Develop(resource);
    }

    #endregion
    #endregion
    #endregion
}