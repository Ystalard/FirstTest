using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FirstTest.Handler;

namespace FirstTest
{
    public interface IDefense: IBuildable
{
    int AmountArtillerieIon();
    int AmountCanonDeGausse();
    int AmountGrandBouclier();
    int AmountLanceurMissile();
    int AmountLanceurPlasma();
    int AmountLaserLeger();
    int AmountLaserLourd();
    int AmountMisileInterception();
    int AmountMissileInterplanetaire();
    int AmountPetitBouclier();
    bool CanBuildArtillerieIon();
    bool CanBuildCanonDeGausse();
    bool CanBuildGrandBouclier();
    bool CanBuildLanceurMissile();
    bool CanBuildLanceurPlasma();
    bool CanBuildLaserLeger();
    bool CanBuildLaserLourd();
    bool CanBuildMisileInterception();
    bool CanBuildMissileInterplanetaire();
    bool CanBuildPetitBouclier();
    void DevelopArtillerieIon(int number);
    void DevelopCanonDeGausse(int number);
    void DevelopGrandBouclier(int number);
    void DevelopLanceurMissile(int number);
    void DevelopLanceurPlasma(int number);
    void DevelopLaserLeger(int number);
    void DevelopLaserLourd(int number);
    void DevelopMisileInterception(int number);
    void DevelopMissileInterplanetaire(int number);
    void DevelopPetitBouclier(int number);
    List<(string name, int value)> ExtractDataFromTable(IWebElement table);
    new bool IsBusy();
}
    public class Defense : Buildable, IDefense
    {
        #region  "constructor"
        public Defense(Actions act): base(act, Menu.Defense){}

        public Defense(Actions act, SharedProperties sharedProperties): base(act, Menu.Defense, sharedProperties){}
        #endregion "constructor"
        
        #region "public method"
        public override bool IsBusy()
        {
            return false; //never busy, construction gets into the pile of defense construction.
        }

        public void DevelopLanceurMissile(int number)
        {
            Develop(Program.settings.Defense.LanceurMissile, number);
        }

        public bool CanBuildLanceurMissile()
        {
            return CanBuildElement(Program.settings.Defense.LanceurMissile);
        }
        public void DevelopLaserLeger(int number)
        {
            Develop(Program.settings.Defense.LaserLeger, number);
        }

        public bool CanBuildLaserLeger()
        {
            return CanBuildElement(Program.settings.Defense.LaserLeger);
        }
        public void DevelopLaserLourd(int number)
        {
            Develop(Program.settings.Defense.LaserLourd, number);
        }

        public bool CanBuildLaserLourd()
        {
            return CanBuildElement(Program.settings.Defense.LaserLourd);
        }
        public void DevelopCanonDeGausse(int number)
        {
            Develop(Program.settings.Defense.CanonDeGausse, number);
        }

        public bool CanBuildCanonDeGausse()
        {
            return CanBuildElement(Program.settings.Defense.CanonDeGausse);
        }
        public void DevelopArtillerieIon(int number)
        {
            Develop(Program.settings.Defense.ArtillerieIon, number);
        }

        public bool CanBuildArtillerieIon()
        {
            return CanBuildElement(Program.settings.Defense.ArtillerieIon);
        }
        public void DevelopLanceurPlasma(int number)
        {
            Develop(Program.settings.Defense.LanceurPlasma, number);
        }

        public bool CanBuildLanceurPlasma()
        {
            return CanBuildElement(Program.settings.Defense.LanceurPlasma);
        }
        public void DevelopPetitBouclier(int number)
        {
            Develop(Program.settings.Defense.PetitBouclier, number);
        }

        public bool CanBuildPetitBouclier()
        {
            return CanBuildElement(Program.settings.Defense.PetitBouclier);
        }
        public void DevelopGrandBouclier(int number)
        {
            Develop(Program.settings.Defense.GrandBouclier, number);
        }

        public bool CanBuildGrandBouclier()
        {
            return CanBuildElement(Program.settings.Defense.GrandBouclier);
        }
        public void DevelopMisileInterception(int number)
        {
            Develop(Program.settings.Defense.MisileInterception, number);
        }

        public bool CanBuildMisileInterception()
        {
            return CanBuildElement(Program.settings.Defense.MisileInterception);
        }
        public void DevelopMissileInterplanetaire(int number)
        {
            Develop(Program.settings.Defense.MissileInterplanetaire, number);
        }

        public bool CanBuildMissileInterplanetaire()
        {
            return CanBuildElement(Program.settings.Defense.MissileInterplanetaire);
        }

        #region "Amount of defense"
        public int AmountLanceurMissile()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameLanceurMissile);
            
            return GetCurrentLevel(Program.settings.Defense.AmountLanceurMissile) + number_in_construction;
        }
        public int AmountLaserLeger()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameLaserLeger);
            
            return GetCurrentLevel(Program.settings.Defense.AmountLaserLeger) + number_in_construction;
        }
        public int AmountLaserLourd()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameLaserLourd);
            
            return GetCurrentLevel(Program.settings.Defense.AmountLaserLourd) + number_in_construction;
        }
        public int AmountCanonDeGausse()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameCanonDeGausse);
            
            return GetCurrentLevel(Program.settings.Defense.AmountCanonDeGausse) + number_in_construction;
        }
        public int AmountArtillerieIon()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameArtillerieIon);
            
            return GetCurrentLevel(Program.settings.Defense.AmountArtillerieIon) + number_in_construction;
        }
        public int AmountLanceurPlasma()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameLanceurPlasma);
            
            return GetCurrentLevel(Program.settings.Defense.AmountLanceurPlasma) + number_in_construction;
        }
        public int AmountPetitBouclier()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NamePetitBouclier);
            
            return GetCurrentLevel(Program.settings.Defense.AmountPetitBouclier) + number_in_construction;
        }
        public int AmountGrandBouclier()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameGrandBouclier);
            
            return GetCurrentLevel(Program.settings.Defense.AmountGrandBouclier) + number_in_construction;
        }
        public int AmountMisileInterception()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameMisileInterception);
            
            return GetCurrentLevel(Program.settings.Defense.AmountMisileInterception) + number_in_construction;
        }
        public int AmountMissileInterplanetaire()
        {
            int number_in_construction = GetNumberInConstruction(Program.settings.Defense.NameMissileInterplanetaire);
            
            return GetCurrentLevel(Program.settings.Defense.AmountMissileInterplanetaire) + number_in_construction;
        }

        public List<(string name,int value)> ExtractDataFromTable(IWebElement table)
        {
            // Find all td elements within the table
            IList<IWebElement> tds = table.FindElements(By.TagName("td"));

            var result = new List<(string name,int value)>();

            // Loop through each td and extract the title of the image and the number
            foreach (IWebElement td in tds)
            {
                IWebElement image = td.FindElement(By.TagName("img"));
                string title = image.GetAttribute("title");
                int number = int.Parse(td.Text);

                result.Add((title, number));
            }

            return result;
        }
        #endregion "Amount of defense"
        #endregion "public method"

        #region "private"
        private int GetNumberInConstruction(string element_name)
        {
            int number_in_construction = 0;

            if(MyDriver.ElementExists(Program.settings.ProductionboxBottom.ActiveConstructionInChantierSpatial))
            {
                if(MyDriver.FindElement(Program.settings.ProductionboxBottom.ActiveConstructionInChantierSpatial).Text == element_name)
                {
                    IWebElement element = MyDriver.FindElement(Program.settings.ProductionboxBottom.ActiveCountConstructionInChantierSpatial);
                    number_in_construction = int.Parse(element.Text);
                }                
            }

            if(MyDriver.ElementExists(Program.settings.ProductionboxBottom.QueueTableChantierSpatial))
            {
                IWebElement table = MyDriver.FindElement(Program.settings.ProductionboxBottom.QueueTableChantierSpatial);
                List<(string name,int value)> inConstructionElements = ExtractDataFromTable(table);
                inConstructionElements.FindAll(element => element.name == element_name).ForEach(element => number_in_construction += element.value);
            }

            return number_in_construction;
        }
        #endregion "private"
    }
}