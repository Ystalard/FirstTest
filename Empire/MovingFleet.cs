using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace FirstTest
{
    public class MovingFleet: IDisposable
    {
        #region public properties
        public event Delegate.MovingFleetEventHandler DisposeMe;
        public readonly Coordinates Coordinates;
        public readonly Handler.Timer arrivalTime;
        public readonly Handler.Timer returnTime;
        public readonly int ChasseurLeger;
        public readonly int ChasseurLourd;
        public readonly int Croiseur;
        public readonly int VaisseauBataille;
        public readonly int Traqueur;
        public readonly int Bombardier;
        public readonly int Destructeur;
        public readonly int Edm;
        public readonly int Faucheur;
        public readonly int Eclaireur;
        public readonly int PetitTransporteur;
        public readonly int GrandTransporteur;
        public readonly int VaisseauColonisation;
        public readonly int Recycleur;
        public readonly int SondeEspionnage;

        public readonly int MetalLoad;
        public readonly int CrystalLoad;
        public readonly int DeuteriumLoad;
        public readonly int FoodLoad;
        #endregion public properties

        #region constructor
        public MovingFleet( TimeSpan arrivalTime, TimeSpan returnTime, int chasseurLeger, int chasseurLourd, int croiseur, int vaisseauBataille, int traqueur, 
        int bombardier, int destructeur, int edm, int faucheur, int eclaireur, int petitTransporteur, int grandTransporteur, int vaisseauColonisation, int recycleur, 
        int sondeEspionnage, int metalLoad, int crystalLoad, int deuteriumLoad, int foodLoad, Coordinates coordinates )
        {
            this.arrivalTime = new ();
            this.arrivalTime.StartTimer(arrivalTime);

            this.returnTime = new(true);
            this.returnTime.StartTimer(returnTime);
            this.returnTime.timerStop += CatchTimerStop;
            
            ChasseurLeger = chasseurLeger;
            ChasseurLourd = chasseurLourd;
            Croiseur = croiseur;
            VaisseauBataille = vaisseauBataille;
            Traqueur = traqueur;
            Bombardier = bombardier;
            Destructeur = destructeur;
            Edm = edm;
            Faucheur = faucheur;
            Eclaireur = eclaireur;
            PetitTransporteur = petitTransporteur;
            GrandTransporteur = grandTransporteur;
            VaisseauColonisation = vaisseauColonisation;
            Recycleur = recycleur;
            SondeEspionnage = sondeEspionnage;
            MetalLoad = metalLoad;
            CrystalLoad = crystalLoad;
            DeuteriumLoad = deuteriumLoad;
            FoodLoad = foodLoad;

            Coordinates = coordinates;
        }
        #endregion constructor

        #region private method
        public void Dispose()
        {
           arrivalTime.Dispose();
           returnTime.Dispose();
        }

        private void CatchTimerStop(object sender, EventArgs e)
        {
            Dispose();
            DisposeMe?.Invoke(this);
        }
        #endregion private method
    }
}