namespace FirstTest.Configuration;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class AppSettings
{
    [JsonProperty("chrome")]
    public string Chrome { get; set; }

    [JsonProperty("chromeDriver")]
    public string ChromeDriver { get; set; }

    [JsonProperty("Gameclass")]
    private string mGameclass { get; set; }

    [JsonProperty("Cookies")]
    public string Cookies { get; set; }

    public enum GameclassEnum{
        collector,
        general,
        explorateur
    }

    public GameclassEnum Gameclass => mGameclass switch {
        "collector" => GameclassEnum.collector,
        "general" => GameclassEnum.general,
        "explorateur" => GameclassEnum.explorateur,
        _ => GameclassEnum.collector
    };


    [JsonProperty("login")]
    public Login Login { get; set; }

    [JsonProperty("joinGame")]
    public JoinGame JoinGame { get; set; }

    [JsonProperty("intro")]
    public Intro Intro { get; set; }

    [JsonProperty("cookie")]
    public Cookie Cookie { get; set; }

    [JsonProperty("tooltip")]
    public string Tooltip { get; set; }

    [JsonProperty("productionTooltip")]
    public string ProductionTooltip { get; set; }

    [JsonProperty("Nav")]
    public Nav Nav { get; set; }

    [JsonProperty("top")]
    public Top Top { get; set; }

    [JsonProperty("resources")]
    public Resources Resources { get; set; }
    
    [JsonProperty("details")]
    public Details Details { get; set; }

    [JsonProperty("supplies")]
    public Supplies Supplies { get; set; }

    [JsonProperty("formeDeVie")]
    public FormeDeVie FormeDeVie { get; set; }

    [JsonProperty("facilities")]
    public Facilities Facilities { get; set; }

    [JsonProperty("recherche")]
    public Recherche Recherche { get; set; }

    [JsonProperty("chantierSpatial")]
    public ChantierSpatial ChantierSpatial { get; set; }

    [JsonProperty("defense")]
    public Defense Defense { get; set; }
    
    [JsonProperty("productionboxBottom")]
    public ProductionboxBottom ProductionboxBottom { get; set; }

    [JsonProperty("empire")]
    public Empire Empire { get; set; }
}

public partial class Nav
{
[JsonProperty("ApercuDirectives")]
public string ApercuDirectives {get; set;}
[JsonProperty("VueEnsemble")]
public string VueEnsemble {get; set;}
[JsonProperty("Ressources")]
public string Ressources {get; set;}
[JsonProperty("FormeDeVie")]
public string FormeDeVie {get; set;}
[JsonProperty("Installations")]
public string Installations {get; set;}
[JsonProperty("Marchand")]
public string Marchand {get; set;}
[JsonProperty("Recherche")]
public string Recherche {get; set;}
[JsonProperty("ChantierSpatial")]
public string ChantierSpatial {get; set;}
[JsonProperty("Defense")]
public string Defense {get; set;}
[JsonProperty("Flotte")]
public string Flotte {get; set;}
[JsonProperty("Galaxie")]
public string Galaxie {get; set;}
[JsonProperty("Empire")]
public string Empire {get; set;}
[JsonProperty("Alliance")]
public string Alliance {get; set;}
[JsonProperty("MessageOfficier")]
public string MessageOfficier {get; set;}
[JsonProperty("Boutique")]
public string Boutique {get; set;}
[JsonProperty("Recompenses")]
public string Recompenses {get; set;}
}

public partial class Login
{
    [JsonProperty("email")]
    public string Email { get; set;}
    [JsonProperty("password")]
    public string Password { get; set;}
    [JsonProperty("register")]
    public string Register { get; set;}
    [JsonProperty("registerEmail")]
    public string RegisterEmail { get; set;}
    [JsonProperty("registerPassword")]
    public string RegisterPassword { get; set;}
    [JsonProperty("subscribeButton")]
    public string SubscribeButton { get; set;}
    [JsonProperty("loginTab")]
    public string LoginTab { get; set;}
    [JsonProperty("loginEmail")]
    public string LoginEmail { get; set;}
    [JsonProperty("loginPassword")]
    public string LoginPassword { get; set;}
    [JsonProperty("connectButton")]
    public string ConnectButton { get; set;}
    [JsonProperty("lastGame")]
    public string LastGame { get; set;}
    [JsonProperty("play")]
    public string Play { get; set;}
    [JsonProperty("IsFirstConnection")]
    public bool IsFirstConnection { get; set;}
    [JsonProperty("goToLastGame")]
    public bool GoToLastGame { get; set;}
    [JsonProperty("url")]
    public string Url { get; set;}

    [JsonProperty("urlConnected")]
    public string UrlConnected { get; set;}
}

public partial class ChantierSpatial
{
    [JsonProperty("chasseurLeger")]
    public string ChasseurLeger { get; set; }

    [JsonProperty("chasseurLourd")]
    public string ChasseurLourd { get; set; }

    [JsonProperty("croiseur")]
    public string Croiseur { get; set; }

    [JsonProperty("vaisseauBataille")]
    public string VaisseauBataille { get; set; }

    [JsonProperty("traqueur")]
    public string Traqueur { get; set; }

    [JsonProperty("bombardier")]
    public string Bombardier { get; set; }

    [JsonProperty("destructeur")]
    public string Destructeur { get; set; }

    [JsonProperty("edm")]
    public string Edm { get; set; }

    [JsonProperty("faucheur")]
    public string Faucheur { get; set; }

    [JsonProperty("eclaireur")]
    public string Eclaireur { get; set; }

    [JsonProperty("petitTransporteur")]
    public string PetitTransporteur { get; set; }

    [JsonProperty("grandTransporteur")]
    public string GrandTransporteur { get; set; }

    [JsonProperty("vaisseauColonisation")]
    public string VaisseauColonisation { get; set; }

    [JsonProperty("recycleur")]
    public string Recycleur { get; set; }

    [JsonProperty("sondeEspionnage")]
    public string SondeEspionnage { get; set; }

    [JsonProperty("details")]
    public ChantierSpatialDetails Details { get; set; }

    [JsonProperty("amountChasseurLeger")]
    public string AmountChasseurLeger {get; set;}
    
    [JsonProperty("amountChasseurLourd")]
    public string AmountChasseurLourd {get; set;}
    
    [JsonProperty("amountCroiseur")]
    public string AmountCroiseur {get; set;}
    
    [JsonProperty("amountVaisseauBataille")]
    public string AmountVaisseauBataille {get; set;}
    
    [JsonProperty("amountTraqueur")]
    public string AmountTraqueur {get; set;}
    
    [JsonProperty("amountBombardier")]
    public string AmountBombardier {get; set;}
    
    [JsonProperty("amountDestructeur")]
    public string AmountDestructeur {get; set;}
    
    [JsonProperty("amountEdm")]
    public string AmountEdm {get; set;}
    
    [JsonProperty("amountFaucheur")]
    public string AmountFaucheur {get; set;}
    
    [JsonProperty("amountEclaireur")]
    public string AmountEclaireur {get; set;}
    
    [JsonProperty("amountPetitTransporteur")]
    public string AmountPetitTransporteur {get; set;}
    
    [JsonProperty("amountGrandTransporteur")]
    public string AmountGrandTransporteur {get; set;}
    
    [JsonProperty("amountVaisseauColonisation")]
    public string AmountVaisseauColonisation {get; set;}
    
    [JsonProperty("amountRecycleur")]
    public string AmountRecycleur {get; set;}
    
    [JsonProperty("amountSondeEspionnage")]
    public string AmountSondeEspionnage {get; set;}
    
}

public partial class ChantierSpatialDetails
{
    [JsonProperty("timeToSearch")]
    public string TimeToSearch { get; set; }

    [JsonProperty("metalRequired")]
    public string MetalRequired { get; set; }

    [JsonProperty("cristalRequired")]
    public string CristalRequired { get; set; }

    [JsonProperty("deuteriumRequired")]
    public string DeuteriumRequired { get; set; }

    [JsonProperty("develop")]
    public string Develop { get; set; }

    [JsonProperty("levelOrNumber")]
    public string LevelOrNumber { get; set; }

    [JsonProperty("buildAmountAllowed")]
    public string BuildAmountAllowed { get; set; }

    [JsonProperty("buildAmount")]
    public string BuildAmount { get; set; }

    [JsonProperty("technoTree")]
    public DetailsTechnoTree TechnoTree { get; set; }
}

public partial class DetailsTechnoTree
{
}

public partial class Cookie
{
    [JsonProperty("cookieManageButtonSelector")]
    public string CookieManageButtonSelector { get; set; }

    [JsonProperty("cookieRefuseButtonSelector")]
    public string CookieRefuseButtonSelector { get; set; }
}

public partial class DefenseDetails
{
    [JsonProperty("timeToSearch")]
    public string TimeToSearch { get; set; }

    [JsonProperty("metalRequired")]
    public string MetalRequired { get; set; }

    [JsonProperty("cristalRequired")]
    public string CristalRequired { get; set; }

    [JsonProperty("deuteriumRequired")]
    public string DeuteriumRequired { get; set; }

    [JsonProperty("develop")]
    public string Develop { get; set; }

    [JsonProperty("levelOrNumber")]
    public string LevelOrNumber { get; set; }

    [JsonProperty("buildAmountAllowed")]
    public string BuildAmountAllowed { get; set; }

    [JsonProperty("buildAmount")]
    public string BuildAmount { get; set; }

    [JsonProperty("technoTree")]
    public Defense TechnoTree { get; set; }
}

public partial class Defense
{
    [JsonProperty("lanceurMissile")]
    public string LanceurMissile { get; set; }

    [JsonProperty("laserLeger")]
    public string LaserLeger { get; set; }

    [JsonProperty("laserLourd")]
    public string LaserLourd { get; set; }

    [JsonProperty("canonDeGausse")]
    public string CanonDeGausse { get; set; }

    [JsonProperty("artillerieIon")]
    public string ArtillerieIon { get; set; }

    [JsonProperty("lanceurPlasma")]
    public string LanceurPlasma { get; set; }

    [JsonProperty("petitBouclier")]
    public string PetitBouclier { get; set; }

    [JsonProperty("grandBouclier")]
    public string GrandBouclier { get; set; }

    [JsonProperty("misileInterception")]
    public string MisileInterception { get; set; }

    [JsonProperty("missileInterplanetaire")]
    public string MissileInterplanetaire { get; set; }

    [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
    public DefenseDetails Details { get; set; }

    [JsonProperty("amountLanceurMissile")]
    public string AmountLanceurMissile { get; set;}
    [JsonProperty("amountLaserLeger")]
    public string AmountLaserLeger { get; set;}
    [JsonProperty("amountLaserLourd")]
    public string AmountLaserLourd { get; set;}
    [JsonProperty("amountCanonDeGausse")]
    public string AmountCanonDeGausse { get; set;}
    [JsonProperty("amountArtillerieIon")]
    public string AmountArtillerieIon { get; set;}
    [JsonProperty("amountLanceurPlasma")]
    public string AmountLanceurPlasma { get; set;}
    [JsonProperty("amountPetitBouclier")]
    public string AmountPetitBouclier { get; set;}
    [JsonProperty("amountGrandBouclier")]
    public string AmountGrandBouclier { get; set;}
    [JsonProperty("amountMisileInterception")]
    public string AmountMisileInterception { get; set;}
    [JsonProperty("amountMissileInterplanetaire")]
    public string AmountMissileInterplanetaire { get; set;}

    [JsonProperty("nameLanceurMissile")]
    public string NameLanceurMissile { get; set;}

    [JsonProperty("nameLaserLeger")]
    public string NameLaserLeger { get; set;}

    [JsonProperty("nameLaserLourd")]
    public string NameLaserLourd { get; set;}

    [JsonProperty("nameCanonDeGausse")]
    public string NameCanonDeGausse { get; set;}
    
    [JsonProperty("nameArtillerieIon")]
    public string NameArtillerieIon { get; set;}

    [JsonProperty("nameLanceurPlasma")]
    public string NameLanceurPlasma { get; set;}

    [JsonProperty("namePetitBouclier")]
    public string NamePetitBouclier { get; set;}

    [JsonProperty("nameGrandBouclier")]
    public string NameGrandBouclier { get; set;}

    [JsonProperty("nameMisileInterception")]
    public string NameMisileInterception { get; set;}

    [JsonProperty("nameMissileInterplanetaire")]
    public string NameMissileInterplanetaire { get; set;}

}

public partial class ProductionboxBottom
{
    [JsonProperty("activeConstructionInChantierSpatial")]
    public string ActiveConstructionInChantierSpatial { get; set; }
    
    [JsonProperty("activeCountConstructionInChantierSpatial")]
    public string ActiveCountConstructionInChantierSpatial { get; set; }
    
    [JsonProperty("queueTableChantierSpatial")]
    public string QueueTableChantierSpatial { get; set; }
}

public partial class Empire {
    [JsonProperty("planetList")]
    public string PlanetList { get; set; }
    
}

public partial class FacilitiesDetails
{
    [JsonProperty("timeToSearch")]
    public string TimeToSearch { get; set; }

    [JsonProperty("metalRequired")]
    public string MetalRequired { get; set; }

    [JsonProperty("cristalRequired")]
    public string CristalRequired { get; set; }

    [JsonProperty("deuteriumRequired")]
    public string DeuteriumRequired { get; set; }

    [JsonProperty("develop")]
    public string Develop { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("technoTree")]
    public Facilities TechnoTree { get; set; }
}

public partial class Facilities
{
    [JsonProperty("decompteTempsDeConstruction", NullValueHandling = NullValueHandling.Ignore)]
    public string DecompteTempsDeConstruction { get; set; }

    [JsonProperty("usineRobot")]
    public string UsineRobot { get; set; }

    [JsonProperty("developUsineRobot")]
    public string DevelopUsineRobot {get; set;}

    [JsonProperty("levelUsineRobot")]
    public string LevelUsineRobot {get; set;}

    [JsonProperty("chantierSpatial")]
    public string ChantierSpatial { get; set; }

    [JsonProperty("developChantierSpatial")]
    public string DevelopChantierSpatial {get; set;}

    [JsonProperty("levelChantierSpatial")]
    public string LevelChantierSpatial {get; set;}

    [JsonProperty("laboRecherche")]
    public string LaboRecherche { get; set; }

    [JsonProperty("developLaboRecherche")]
    public string DevelopLaboRecherche {get; set;}

    [JsonProperty("levelLaboRecherche")]
    public string LevelLaboRecherche {get; set;}

    [JsonProperty("depotRavitaillement")]
    public string DepotRavitaillement { get; set; }

    [JsonProperty("developDepotRavitaillement")]
    public string DevelopDepotRavitaillement {get; set;}

    [JsonProperty("levelDepotRavitaillement")]
    public string LevelDepotRavitaillement {get; set;}

    [JsonProperty("siloMissible")]
    public string SiloMissible { get; set; }

    [JsonProperty("developSiloMissible")]
    public string DevelopSiloMissible {get; set;}

    [JsonProperty("levelSiloMissible")]
    public string LevelSiloMissible {get; set;}

    [JsonProperty("nanites")]
    public string Nanites { get; set; }

    [JsonProperty("developNanites")]
    public string DevelopNanites {get; set;}

    [JsonProperty("levelNanites")]
    public string LevelNanites {get; set;}

    [JsonProperty("terraformeur")]
    public string Terraformeur { get; set; }

    [JsonProperty("developTerraformeur")]
    public string DevelopTerraformeur {get; set;}

    [JsonProperty("levelTerraformeur")]
    public string LevelTerraformeur {get; set;}

    [JsonProperty("docker")]
    public string Docker { get; set; }

    [JsonProperty("developDocker")]
    public string DevelopDocker {get; set;}

    [JsonProperty("levelDocker")]
    public string LevelDocker {get; set;}

    [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
    public FacilitiesDetails Details { get; set; }
}

public partial class FormeDeVieDetails
{
    [JsonProperty("timeToSearch")]
    public string TimeToSearch { get; set; }

    [JsonProperty("metalRequired")]
    public string MetalRequired { get; set; }

    [JsonProperty("cristalRequired")]
    public string CristalRequired { get; set; }

    [JsonProperty("deuteriumRequired")]
    public string DeuteriumRequired { get; set; }

    [JsonProperty("develop")]
    public string Develop { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("technoTree")]
    public FormeDeVie TechnoTree { get; set; }
}

public partial class FormeDeVie
{
    [JsonProperty("decompteTempsDeConstruction", NullValueHandling = NullValueHandling.Ignore)]
    public string DecompteTempsDeConstruction { get; set; }

    [JsonProperty("SecteurResidentiel")]
    public string SecteurResidentiel { get; set; }

    [JsonProperty("fermebiospherique")]
    public string Fermebiospherique { get; set; }

    [JsonProperty("centreRecherche")]
    public string CentreRecherche { get; set; }

    [JsonProperty("AcademieSciences")]
    public string AcademieSciences { get; set; }

    [JsonProperty("CentreNeuroCalibrage")]
    public string CentreNeuroCalibrage { get; set; }

    [JsonProperty("fusionHauteEnergie")]
    public string FusionHauteEnergie { get; set; }

    [JsonProperty("extractionParFusion")]
    public string ExtractionParFusion { get; set; }

    [JsonProperty("reserveAlimentaire")]
    public string ReserveAlimentaire { get; set; }

    [JsonProperty("tourHabitation")]
    public string TourHabitation { get; set; }

    [JsonProperty("laboBiotech")]
    public string LaboBiotech { get; set; }

    [JsonProperty("metropolis")]
    public string Metropolis { get; set; }

    [JsonProperty("bouclierPlanetaire")]
    public string BouclierPlanetaire { get; set; }

    [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
    public FormeDeVieDetails Details { get; set; }
}

public partial class Intro
{
    [JsonProperty("veteranPlayerSelector")]
    public string VeteranPlayerSelector { get; set; }

    [JsonProperty("continueButtonSelector")]
    public string ContinueButtonSelector { get; set; }

    [JsonProperty("collectorClassSelector")]
    public string CollectorClassSelector { get; set; }

    [JsonProperty("generaltClassSelector")]
    public string GeneraltClassSelector { get; set; }

    [JsonProperty("explorateurClassSelector")]
    public string ExplorateurClassSelector { get; set; }
}

public partial class JoinGame
{
    [JsonProperty("startOnNewServerSelector")]
    public string StartOnNewServerSelector { get; set; }

    [JsonProperty("serverlist")]
    public Serverlist Serverlist { get; set; }
}

public partial class Serverlist
{
    [JsonProperty("serverlistSelector")]
    public string ServerlistSelector { get; set; }

    [JsonProperty("serverStartSelector")]
    public string ServerStartSelector { get; set; }
}

public partial class RechercheDetails
{
    [JsonProperty("timeToSearch")]
    public string TimeToSearch { get; set; }

    [JsonProperty("metalRequired")]
    public string MetalRequired { get; set; }

    [JsonProperty("cristalRequired")]
    public string CristalRequired { get; set; }

    [JsonProperty("deuteriumRequired")]
    public string DeuteriumRequired { get; set; }

    [JsonProperty("develop")]
    public string Develop { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("technoTree")]
    public Recherche TechnoTree { get; set; }
}

public partial class Recherche
{
    [JsonProperty("technoEnergie")]
    public string TechnoEnergie { get; set; }

    [JsonProperty("technoLaser")]
    public string TechnoLaser { get; set; }

    [JsonProperty("technoIons")]
    public string TechnoIons { get; set; }

    [JsonProperty("technoHyperespace")]
    public string TechnoHyperespace { get; set; }

    [JsonProperty("technoPlasma")]
    public string TechnoPlasma { get; set; }

    [JsonProperty("reacteurCombustion")]
    public string ReacteurCombustion { get; set; }

    [JsonProperty("reacteurImpulsion")]
    public string ReacteurImpulsion { get; set; }

    [JsonProperty("propulsionHyperespace")]
    public string PropulsionHyperespace { get; set; }

    [JsonProperty("technoEspionnage")]
    public string TechnoEspionnage { get; set; }

    [JsonProperty("technoOrdinateur")]
    public string TechnoOrdinateur { get; set; }

    [JsonProperty("technoAstro")]
    public string TechnoAstro { get; set; }

    [JsonProperty("ReseauRecherche")]
    public string ReseauRecherche { get; set; }

    [JsonProperty("technoGraviton")]
    public string TechnoGraviton { get; set; }

    [JsonProperty("technoArme")]
    public string TechnoArme { get; set; }

    [JsonProperty("technoBouclier")]
    public string TechnoBouclier { get; set; }

    [JsonProperty("technoProtectionVaisseaux")]
    public string TechnoProtectionVaisseaux { get; set; }

    [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
    public RechercheDetails Details { get; set; }

    [JsonProperty("developTechnoEnergie")]
    public string DevelopTechnoEnergie {get; set;}
    
    [JsonProperty("developTechnoLaser")]
    public string DevelopTechnoLaser {get; set;}
    
    [JsonProperty("developTechnoIons")]
    public string DevelopTechnoIons {get; set;}
    
    [JsonProperty("developTechnoHyperespace")]
    public string DevelopTechnoHyperespace {get; set;}
    
    [JsonProperty("developTechnoPlasma")]
    public string DevelopTechnoPlasma {get; set;}
    
    [JsonProperty("developReacteurCombustion")]
    public string DevelopReacteurCombustion {get; set;}
    
    [JsonProperty("developReacteurImpulsion")]
    public string DevelopReacteurImpulsion {get; set;}
    
    [JsonProperty("developPropulsionHyperespace")]
    public string DevelopPropulsionHyperespace {get; set;}
    
    [JsonProperty("developTechnoEspionnage")]
    public string DevelopTechnoEspionnage {get; set;}
    
    [JsonProperty("developTechnoOrdinateur")]
    public string DevelopTechnoOrdinateur {get; set;}
    
    [JsonProperty("developTechnoAstro")]
    public string DevelopTechnoAstro {get; set;}
    
    [JsonProperty("developReseauRecherche")]
    public string DevelopReseauRecherche {get; set;}
    
    [JsonProperty("developTechnoGraviton")]
    public string DevelopTechnoGraviton {get; set;}
    
    [JsonProperty("developTechnoArme")]
    public string DevelopTechnoArme {get; set;}
    
    [JsonProperty("developTechnoBouclier")]
    public string DevelopTechnoBouclier {get; set;}
    
    [JsonProperty("developTechnoProtectionVaisseaux")]
    public string DevelopTechnoProtectionVaisseaux {get; set;}

    [JsonProperty("levelTechnoEnergie")]
    public string LevelTechnoEnergie {get; set;}

    [JsonProperty("levelTechnoLaser")]
    public string LevelTechnoLaser {get; set;}

    [JsonProperty("levelTechnoIons")]
    public string LevelTechnoIons {get; set;}

    [JsonProperty("levelTechnoHyperespace")]
    public string LevelTechnoHyperespace {get; set;}

    [JsonProperty("levelTechnoPlasma")]
    public string LevelTechnoPlasma {get; set;}

    [JsonProperty("levelReacteurCombustion")]
    public string LevelReacteurCombustion {get; set;}

    [JsonProperty("levelReacteurImpulsion")]
    public string LevelReacteurImpulsion {get; set;}

    [JsonProperty("levelPropulsionHyperespace")]
    public string LevelPropulsionHyperespace {get; set;}

    [JsonProperty("levelTechnoEspionnage")]
    public string LevelTechnoEspionnage {get; set;}

    [JsonProperty("levelTechnoOrdinateur")]
    public string LevelTechnoOrdinateur {get; set;}

    [JsonProperty("levelTechnoAstro")]
    public string LevelTechnoAstro {get; set;}

    [JsonProperty("levelReseauRecherche")]
    public string LevelReseauRecherche {get; set;}

    [JsonProperty("levelTechnoGraviton")]
    public string LevelTechnoGraviton {get; set;}

    [JsonProperty("levelTechnoArme")]
    public string LevelTechnoArme {get; set;}

    [JsonProperty("levelTechnoBouclier")]
    public string LevelTechnoBouclier {get; set;}

    [JsonProperty("levelTechnoProtectionVaisseaux    ")]
    public string LevelTechnoProtectionVaisseaux {get; set;}

    
}

public class Details
{
    [JsonProperty("tempsProduction")]
    public string TempsProduction {get; set;}
    [JsonProperty("energieNecessaire")]
    public string EnergieNecessaire {get; set;}
    [JsonProperty("niveau")]
    public string Niveau {get; set;}
    [JsonProperty("metalRequired")]
    public string MetalRequired {get; set;}
    [JsonProperty("cristalRequired")]
    public string CristalRequired {get; set;}
    [JsonProperty("deuteriumRequired")]
    public string DeuteriumRequired {get; set;}
    [JsonProperty("develop")]
    public string Develop {get; set;}
    [JsonProperty("buildAmountAllowed")]
    public string BuildAmountAllowed {get; set;}
    [JsonProperty("buildAmount")]
    public string BuildAmount {get; set;}
    
    [JsonProperty("closeButton")]
    public string CloseButton {get; set;}
}

public partial class Supplies
{
    [JsonProperty("resourcesettings")]
    public string Resourcesettings { get; set; }
    [JsonProperty("headerTitle")]
    public string HeaderTitle { get; set; }

    [JsonProperty("mineMetal")]
    public string MineMetal { get; set; }

    [JsonProperty("levelMetal")]
    public string LevelMetal { get; set; }

    [JsonProperty("developMetal")]
    public string DevelopMetal { get; set; }
    
    [JsonProperty("mineCristal")]
    public string MineCristal { get; set; }

    [JsonProperty("levelCristal")]
    public string LevelCristal { get; set; }

    [JsonProperty("developCristal")]
    public string DevelopCristal { get; set; }

    [JsonProperty("mineDeuterium")]
    public string MineDeuterium { get; set; }

    [JsonProperty("levelDeuterium")]
    public string LevelDeuterium { get; set; }

    [JsonProperty("developDeuterium")]
    public string DevelopDeuterium { get; set; }

    [JsonProperty("centraleSolaire")]
    public string CentraleSolaire { get; set; }

    [JsonProperty("levelCentralSolaire")]
    public string LevelCentralSolaire { get; set; }

    [JsonProperty("developCentralSolaire")]
    public string DevelopCentralSolaire { get; set; }

    [JsonProperty("lvlMaxCentralSolaire")]
    public string LvlMaxCentralSolaire { get; set; }

    [JsonProperty("centraleFusion")]
    public string CentraleFusion { get; set; }

    [JsonProperty("levelCentralFusion")]
    public string LevelCentralFusion { get; set; }

    [JsonProperty("satelitteSolaire")]
    public string SatelitteSolaire { get; set; }

    [JsonProperty("amountSatelitteSolaire")]
    public string AmountSatelitteSolaire { get; set; }

    [JsonProperty("hangarMetal")]
    public string HangarMetal { get; set; }

    [JsonProperty("levelhangarMetal")]
    public string LevelHangarMetal { get; set; }

    [JsonProperty("hangarCristal")]
    public string HangarCristal { get; set; }

    [JsonProperty("levelhangarCristal")]
    public string LevelHangarCristal { get; set; }

    [JsonProperty("hangarDeuterium")]
    public string HangarDeuterium { get; set; }

    [JsonProperty("levelhangarDeuterium")]
    public string LevelHangarDeuterium { get; set; }

    [JsonProperty("foreuse")]
    public string Foreuse { get; set; }

    [JsonProperty("amountForeuse")]
    public string AmountForeuse { get; set; }

    [JsonProperty("technologyDetails")]
    public TechnologyDetails TechnologyDetails { get; set; }

    [JsonProperty("decompteTempsDeConstruction")]
    public string DecompteTempsDeConstruction { get; set; }
}

public partial class TechnologyDetails
{
    [JsonProperty("tempsProduction")]
    public string TempsProduction { get; set; }

    [JsonProperty("energieNecessaire")]
    public string EnergieNecessaire { get; set; }

    [JsonProperty("energieGainPerSatelitte")]
    public string EnergieGainPerSatellite { get; set; }

    [JsonProperty("energieGainFromNextCentralSolaire")]
    public string EnergieGainFromNextCentralSolaire { get; set; }

    [JsonProperty("niveau")]
    public string Niveau { get; set; }

    [JsonProperty("metalRequired")]
    public string MetalRequired { get; set; }

    [JsonProperty("cristalRequired")]
    public string CristalRequired { get; set; }

    [JsonProperty("deuteriumRequired")]
    public string DeuteriumRequired { get; set; }

    [JsonProperty("develop")]
    public string Develop { get; set; }

    [JsonProperty("buildAmountAllowed")]
    public string BuildAmountAllowed { get; set; }

    [JsonProperty("buildAmount")]
    public string BuildAmount { get; set; }

    [JsonProperty("technoTree")]
    public TechnologyDetailsTechnoTree TechnoTree { get; set; }

    [JsonProperty("closeButton")]
    public string CloseButton { get; set; }

}

public partial class TechnologyDetailsTechnoTree
{
    [JsonProperty("centraleFusion")]
    public string CentraleFusion { get; set; }

    [JsonProperty("foreuse")]
    public string Foreuse { get; set; }
}

public partial class Top
{
    [JsonProperty("pageReloader")]
    public string PageReloader { get; set; }

    [JsonProperty("headerbarcomponent")]
    public Headerbarcomponent Headerbarcomponent { get; set; }
}
public partial class Resources
{
    [JsonProperty("metal")]
    public string Metal { get; set; }

    [JsonProperty("cristal")]
    public string Cristal { get; set; }

    [JsonProperty("deuterium")]
    public string Deuterium { get; set; }
    
    [JsonProperty("energie")]
    public string Energie { get; set; }
}

public partial class Headerbarcomponent
{
    [JsonProperty("logOut")]
    public string LogOut { get; set; }
}

public partial class AppSettings
{
    public static AppSettings FromJson(string json) => JsonConvert.DeserializeObject<AppSettings>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this AppSettings self) => JsonConvert.SerializeObject(self, Converter.Settings);
}

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new()
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters = {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };
}