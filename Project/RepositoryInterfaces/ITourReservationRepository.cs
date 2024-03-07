using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ITourReservationRepository:ISubject
    {
        public TourReservation Add(TourReservation tourReservation);

        public TourReservation Update(TourReservation tourReservation);

        public TourReservation Remove(int id);

        public TourReservation GetTourReservationById(int id);

        public List<TourReservation> GetReservationByTourId(int id);

        public List<TourReservation> GetAllTourReservations();
    }
}
