using Project.Controller;
using Project.Model;
using Project.Observer;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Project.Service
{
    public class TourService
    {
        private ITourRepository tourRepository;
        LocationService locationService;
        AppointmentService appointmentService;
        TourReservationService tourReservationService;

        public TourService()
        {
            tourRepository = Injector.Injector.CreateInstance<ITourRepository>();
            appointmentService = new AppointmentService();
            locationService = new LocationService();
            tourReservationService = new TourReservationService();

        }

        public int Create(Location location, string name, string description, string language, int maxGuests, int duration, int guideId)
        {


            Tour tour = new Tour(location, name, description, language, maxGuests, duration,guideId);

            int tourId = tourRepository.Add(tour);

            return tourId;

        }

        public DateTime BuildDate(DateTime date, string time)
        {
            string[] splitedTime = time.Split(':');
            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, int.Parse(splitedTime[0]), int.Parse(splitedTime[1]), 0);
            return newDate;
        }



        public List<Tour> GetAll(int guideId)
        {
            return tourRepository.GetAll(guideId);
        }

        public List<Tour> GetAll()
        {
            return tourRepository.GetAll();
        }

        public Tour GetById(int id)
        {
            return tourRepository.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            tourRepository.Subscribe(observer);
        }

        public List<Tour> GetAllTourAppointments()
        {
            List<Tour> tourAppointments = new List<Tour>();
            List<Tour> tours = GetAll();
            appointmentService.RefreshAppointments();

            foreach(Tour tour in tours)
            {
                List<Appointment> appointments = appointmentService.GetByTourId(tour.Id);
                foreach(Appointment appointment in appointments)
                {
                    Tour newTour = new Tour(tour,appointment);
                    newTour.Location = locationService.GetById(newTour.LocationId);
                    tourAppointments.Add(newTour);
                }
            }


            return tourAppointments;
        }

        public void CancelAllToursOfGude(User guide)
        {
            List<Tour> tours = GetAll(guide.Id);
            appointmentService.RefreshAppointments();

            foreach (Tour tour in tours)
            {
                List<Appointment> appointments = appointmentService.GetByTourId(tour.Id);
                foreach (Appointment appointment in appointments)
                {
                    if(DateTime.Compare(appointment.DateAndTimeOfAppointment, DateTime.Now) > 0)
                    {
                        appointmentService.CancelGuideTour(appointment,tour.Name);
                    } 
                }
            }



        }

        public List<Tour> GetAllTourAppointments(int guideId)
        {
            List<Tour> tourAppointments = new List<Tour>();
            List<Tour> tours = GetAll(guideId);
            appointmentService.RefreshAppointments();

            foreach (Tour tour in tours)
            {
                List<Appointment> appointments = appointmentService.GetByTourId(tour.Id);
                foreach (Appointment appointment in appointments)
                {
                    Tour newTour = new Tour(tour, appointment);
                    newTour.Location = locationService.GetById(newTour.LocationId);
                    tourAppointments.Add(newTour);
                }
            }


            return tourAppointments;
        }

        public List<Tour> GetCompleted(int guideId)
        {
            List<Tour> completedTours = new List<Tour>();
            foreach(Tour tour in GetAllTourAppointments(guideId))
            {
                if(tour.TourAppointment.Status == Appointment.STATUS.COMPLETED)
                {
                    completedTours.Add(tour);
                }
            }
            return completedTours;
        }

        public List<Tour> GetNotStarted(int guideId)
        {
            List<Tour> notStartedTours = new List<Tour>();
            foreach (Tour tour in GetAllTourAppointments(guideId))
            {
                if (tour.TourAppointment.Status == Appointment.STATUS.NOTSTARTED && tour.TourAppointment.IsNotCanceled)
                {
                    notStartedTours.Add(tour);
                }
            }
            return notStartedTours;
        }

        public List<Tour> GetCanceled(int guideId)
        {
            List<Tour> canceledTours = new List<Tour>();
            foreach (Tour tour in GetAllTourAppointments(guideId))
            {
                if (!tour.TourAppointment.IsNotCanceled)
                {
                    canceledTours.Add(tour);
                }
            }
            return canceledTours;
        }

        public List<Tour> GetCompletedTours(int guideId)
        {
            List<Tour> allTours = GetAllTourAppointments(guideId);
            List<Tour> completedTours = new List<Tour>();

            foreach(Tour tour in allTours)
            {
                if(tour.TourAppointment.Status == Appointment.STATUS.COMPLETED)
                {
                    completedTours.Add(tour);
                }
            }

            return completedTours;
        }


    }
}
