﻿using Project.Observer;
using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class Guest2Controller
    {
        public Guest2 Guest { get; set; }
        public TourRepository TourRepository { get; set; } 
        public TourReservationRepository TourReservationRepository { get; set; }
        public AppointmentRepository AppointmentRepository { get; set; }
        public List<Location> TourLocations { get; set; }
        public ImageRepository ImageRepository { get; set; }
        public List<string> Languages { get; set; }

        public Guest2Controller()
        {
            Guest = new Guest2();
            TourRepository = new TourRepository();
            TourReservationRepository = new TourReservationRepository();
            AppointmentRepository = new AppointmentRepository();
            ImageRepository = new ImageRepository();
            TourLocations = new List<Location>();
            Languages = new List<string>();
            LinkGuest2TourReservations();
            //LinkGuest2Appointments();
            FillTourLocationsList();
            FillTourLanguagesList();
            
        }

        public Guest2Controller(User u)
        {
            Guest = new Guest2(u);
            TourRepository = new TourRepository();
            TourReservationRepository = new TourReservationRepository();
            AppointmentRepository = new AppointmentRepository();
            ImageRepository = new ImageRepository();
            TourLocations = new List<Location>();
            Languages = new List<string>();
            LinkGuest2TourReservations();
            //LinkGuest2Appointments();
            FillTourLocationsList();
            FillTourLanguagesList();
        }

        private void LinkGuest2TourReservations()
        {
            foreach(var tourReservation in TourReservationRepository.GetAllTourReservations())
            {
                if(tourReservation.GuestId == Guest.User.Id)
                {
                    Guest.Reservations.Add(tourReservation);
                }
            }
        }


        

        public List<Appointment> GetAppointmentsForReview()
        {
            List<TourReservation> reservations = GetTourReservations();
            foreach(var appointmentReview in AppointmentRepository.GetAll())
            {
                foreach(var reservation in reservations)
                {
                    if ((appointmentReview.Status == Appointment.STATUS.COMPLETED) && (appointmentReview.TourId == reservation.Id))
                    {
                        Guest.AppointmentsForReview.Add(appointmentReview);
                    }
                }
                
            }
            return Guest.AppointmentsForReview;
        }


        public List<TourReservation> GetTourReservations()
        {
            return Guest.Reservations;
        }





        public List<Tour> GetAllTourAppointments()
        {
            return TourRepository.GetAll();
        }

        public List<Tour> FindAllAlternatives(Tour tour, int numberOfGuests)
        {
            return TourRepository.FindAllAlternatives(tour, numberOfGuests);
        }

        public List<Coupon> GetGuestsCoupons()
        {
            return Guest.Coupons;
        }

        public List<Tour> GetTours()
        {
            return TourRepository.GetAll();
        }

        public List<Location> GetTourLocations()
        {
            return TourLocations;
        }

        public List<string> GetTourLanguages()
        {
            return Languages;
        }

        

        private void FillTourLocationsList()
        {
            foreach(var tour in TourRepository.GetAll())
            {
                Location location = TourLocations.Find(l => (l.City == tour.Location.City) && (l.Country == tour.Location.Country));

                if(location == null)
                {
                    TourLocations.Add(tour.Location);
                }
            }
        }

        private void FillTourLanguagesList()
        {
            foreach(var tour in TourRepository.GetAll())
            {
                string language = Languages.Find(l => (l.ToString() == tour.Language.ToString()) && (l.ToString() == tour.Language.ToString()));

                if(language == null)
                {
                    Languages.Add(tour.Language);
                }
            }
        }

        public void SubscribeToReservationRepo(IObserver observer)
        {
            TourReservationRepository.Subscribe(observer);
        }

        public void AddReservation(TourReservation reservation)
        {
            Guest.Reservations.Add(reservation);
            TourReservationRepository.Add(reservation);
        }
    }
}
