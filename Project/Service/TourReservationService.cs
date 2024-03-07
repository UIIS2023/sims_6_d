using Project.Model;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourReservationService
    {
        //TourReservationRepository tourReservationRepository;
        private ITourReservationRepository tourReservationRepository;
        UserRepository userRepository;

        public TourReservationService()
        {
            //tourReservationRepository = new TourReservationRepository();
            tourReservationRepository = Injector.Injector.CreateInstance<ITourReservationRepository>();
            userRepository = new UserRepository();
        }

        public List<User> GetGuestsWithReservation(int id)
        {
            List<User> guestList = new List<User>();
            foreach(TourReservation reservation in tourReservationRepository.GetReservationByTourId(id))
            {
                guestList.Add(userRepository.GetById(reservation.GuestId));
            }

            return guestList;
        }


        public List<User> GetApproprietReservations(int appointmentId)
        {
            List<TourReservation> tourReservations = tourReservationRepository.GetAllTourReservations();
            List<User> approprietReservations = new List<User>();

            foreach (TourReservation reservation in tourReservations)
            {
                if (reservation.TourId == appointmentId)
                {
                    approprietReservations.Add(userRepository.GetById(reservation.GuestId));
                }
            }

            return approprietReservations;
        }

        public List<TourReservation> GetReservationForAppointment(int appointmentId)
        {
            List<TourReservation> tourReservations = tourReservationRepository.GetAllTourReservations();
            List<TourReservation> approprietReservations = new List<TourReservation>();

            foreach (TourReservation reservation in tourReservations)
            {
                if (reservation.TourId == appointmentId)
                {
                    approprietReservations.Add(reservation);
                }
            }

            return approprietReservations;

        }

        public void RemoveReservationsForAppointment(int appointmentId)
        {
            List<TourReservation> reservations = GetReservationForAppointment(appointmentId);
            foreach(TourReservation reservation in reservations)
            {
                tourReservationRepository.Remove(reservation.Id);
            }
        }
    }
}
