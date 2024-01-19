using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;

namespace FirstTest
{
    public interface IChantierSpatial: IBuildable
{
    int CountBombardier();
    int CountChasseurLeger();
    int CountChasseurLourd();
    int CountCroiseur();
    int CountDestructeur();
    int CountEclaireur();
    int CountEdm();
    int CountFaucheur();
    int CountGrandTransporteur();
    int CountPetitTransporteur();
    int CountRecycleur();
    int CountSondeEspionnage();
    int CountTraqueur();
    int CountVaisseauBataille();
    int CountVaisseauColonisation();
    bool CanBuildBombardier();
    bool CanBuildChasseurLeger();
    bool CanBuildChasseurLourd();
    bool CanBuildCroiseur();
    bool CanBuildDestructeur();
    bool CanBuildEclaireur();
    bool CanBuildEdm();
    bool CanBuildFaucheur();
    bool CanBuildGrandTransporteur();
    bool CanBuildPetitTransporteur();
    bool CanBuildRecycleur();
    bool CanBuildSondeEspionnage();
    bool CanBuildTraqueur();
    bool CanBuildVaisseauBataille();
    bool CanBuildVaisseauColonisation();
    void DevelopBombardier(int number);
    void DevelopChasseurLeger(int number);
    void DevelopChasseurLourd(int number);
    void DevelopCroiseur(int number);
    void DevelopDestructeur(int number);
    void DevelopEclaireur(int number);
    void DevelopEdm(int number);
    void DevelopFaucheur(int number);
    void DevelopGrandTransporteur(int number);
    void DevelopPetitTransporteur(int number);
    void DevelopRecycleur(int number);
    void DevelopSondeEspionnage(int number);
    void DevelopTraqueur(int number);
    void DevelopVaisseauBataille(int number);
    void DevelopVaisseauColonisation(int number);
    new bool IsBusy();
}
    public class ChantierSpatial: Buildable, IChantierSpatial
    {

    #region  "constructor"
    public ChantierSpatial(Actions act, IPlanet planet): base(act, Menu.ChantierSpatial, planet){}

    public ChantierSpatial(Actions act, SharedProperties sharedProperties, IPlanet planet): base(act, Menu.ChantierSpatial, sharedProperties, planet){}
    #endregion "constructor"

    #region "public method"
    public override bool IsBusy()
    {
        return GetPlanet().Installations().GetInstallationsInConstruction() == Program.settings.Facilities.ChantierSpatial ? true : base.IsBusy();
    }

    public void DevelopChasseurLeger(int number)
    {
        Develop(Program.settings.ChantierSpatial.ChasseurLeger, number);
    }

    public bool CanBuildChasseurLeger()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.ChasseurLeger);
    }
    public void DevelopChasseurLourd(int number)
    {
        Develop(Program.settings.ChantierSpatial.ChasseurLourd, number);
    }

    public bool CanBuildChasseurLourd()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.ChasseurLourd);
    }
    public void DevelopCroiseur(int number)
    {
        Develop(Program.settings.ChantierSpatial.Croiseur, number);
    }

    public bool CanBuildCroiseur()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.Croiseur);
    }
    public void DevelopVaisseauBataille(int number)
    {
        Develop(Program.settings.ChantierSpatial.VaisseauBataille, number);
    }

    public bool CanBuildVaisseauBataille()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.VaisseauBataille);
    }
    public void DevelopTraqueur(int number)
    {
        Develop(Program.settings.ChantierSpatial.Traqueur, number);
    }

    public bool CanBuildTraqueur()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.Traqueur);
    }
    public void DevelopBombardier(int number)
    {
        Develop(Program.settings.ChantierSpatial.Bombardier, number);
    }

    public bool CanBuildBombardier()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.Bombardier);
    }
    public void DevelopDestructeur(int number)
    {
        Develop(Program.settings.ChantierSpatial.Destructeur, number);
    }

    public bool CanBuildDestructeur()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.Destructeur);
    }
    public void DevelopEdm(int number)
    {
        Develop(Program.settings.ChantierSpatial.Edm, number);
    }

    public bool CanBuildEdm()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.Edm);
    }
    public void DevelopFaucheur(int number)
    {
        Develop(Program.settings.ChantierSpatial.Faucheur, number);
    }

    public bool CanBuildFaucheur()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.Faucheur);
    }
    public void DevelopEclaireur(int number)
    {
        Develop(Program.settings.ChantierSpatial.Eclaireur, number);
    }

    public bool CanBuildEclaireur()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.Eclaireur);
    }
    public void DevelopPetitTransporteur(int number)
    {
        Develop(Program.settings.ChantierSpatial.PetitTransporteur, number);
    }

    public bool CanBuildPetitTransporteur()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.PetitTransporteur);
    }
    public void DevelopGrandTransporteur(int number)
    {
        Develop(Program.settings.ChantierSpatial.GrandTransporteur, number);
    }

    public bool CanBuildGrandTransporteur()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.GrandTransporteur);
    }
    public void DevelopVaisseauColonisation(int number)
    {
        Develop(Program.settings.ChantierSpatial.VaisseauColonisation, number);
    }

    public bool CanBuildVaisseauColonisation()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.VaisseauColonisation);
    }
    public void DevelopRecycleur(int number)
    {
        Develop(Program.settings.ChantierSpatial.Recycleur, number);
    }

    public bool CanBuildRecycleur()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.Recycleur);
    }
    public void DevelopSondeEspionnage(int number)
    {
        Develop(Program.settings.ChantierSpatial.SondeEspionnage, number);
    }

    public bool CanBuildSondeEspionnage()
    {
        return CanBuildElement(Program.settings.ChantierSpatial.SondeEspionnage);
    }

    #region "Amount of spaceship"
    public int CountChasseurLeger()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountChasseurLeger);
    }
    public int CountChasseurLourd()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountChasseurLourd);
    }
    public int CountCroiseur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountCroiseur);
    }
    public int CountVaisseauBataille()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountVaisseauBataille);
    }
    public int CountTraqueur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountTraqueur);
    }
    public int CountBombardier()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountBombardier);
    }
    public int CountDestructeur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountDestructeur);
    }
    public int CountEdm()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountEdm);
    }
    public int CountFaucheur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountFaucheur);
    }
    public int CountEclaireur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountEclaireur);
    }
    public int CountPetitTransporteur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountPetitTransporteur);
    }
    public int CountGrandTransporteur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountGrandTransporteur);
    }
    public int CountVaisseauColonisation()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountVaisseauColonisation);
    }
    public int CountRecycleur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountRecycleur);
    }
    public int CountSondeEspionnage()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountSondeEspionnage);
    }
    #endregion "Amount of spaceship"
    #endregion "public method"
    }
}