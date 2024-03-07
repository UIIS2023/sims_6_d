using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        public AccommodationReservation Add(AccommodationReservation accReservation);

        public AccommodationReservation Update(AccommodationReservation accReservation);

        public AccommodationReservation Remove(int id);

        public AccommodationReservation GetReservationById(int id);

        public List<AccommodationReservation> GetReservationsByAccommodationId(int accId);

        public List<AccommodationReservation> GetAllReservations();


        public void Subscribe(IObserver observer);       

        public void Unsubscribe(IObserver observer);

        public void NotifyObservers();
    }
}
