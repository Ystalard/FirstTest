using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;

namespace FirstTest
{
    public interface IChantierSpatial: IBuildable
{
    int AmountBombardier();
    int AmountChasseurLeger();
    int AmountChasseurLourd();
    int AmountCroiseur();
    int AmountDestructeur();
    int AmountEclaireur();
    int AmountEdm();
    int AmountFaucheur();
    int AmountGrandTransporteur();
    int AmountPetitTransporteur();
    int AmountRecycleur();
    int AmountSondeEspionnage();
    int AmountTraqueur();
    int AmountVaisseauBataille();
    int AmountVaisseauColonisation();
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
}
    public class ChantierSpatial: Buildable, IChantierSpatial
    {

    #region  "constructor"
    public ChantierSpatial(Actions act): base(act, Menu.ChantierSpatial){}

    public ChantierSpatial(Actions act, SharedProperties sharedProperties): base(act, Menu.ChantierSpatial, sharedProperties){}
    #endregion "constructor"

    #region "public method"
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
    public int AmountChasseurLeger()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountChasseurLeger);
    }
    public int AmountChasseurLourd()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountChasseurLourd);
    }
    public int AmountCroiseur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountCroiseur);
    }
    public int AmountVaisseauBataille()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountVaisseauBataille);
    }
    public int AmountTraqueur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountTraqueur);
    }
    public int AmountBombardier()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountBombardier);
    }
    public int AmountDestructeur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountDestructeur);
    }
    public int AmountEdm()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountEdm);
    }
    public int AmountFaucheur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountFaucheur);
    }
    public int AmountEclaireur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountEclaireur);
    }
    public int AmountPetitTransporteur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountPetitTransporteur);
    }
    public int AmountGrandTransporteur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountGrandTransporteur);
    }
    public int AmountVaisseauColonisation()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountVaisseauColonisation);
    }
    public int AmountRecycleur()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountRecycleur);
    }
    public int AmountSondeEspionnage()
    {
        return GetCurrentLevel(Program.settings.ChantierSpatial.AmountSondeEspionnage);
    }
    #endregion "Amount of spaceship"
    #endregion "public method"
    }
}