using Project.Model;
using Project.Observer;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AppointmentService
    {
        private IAppointmentRepository appointmentRepository;
        //AppointmentRepository appointmentRepository { get; set; }
        
        private readonly CouponService couponService;
        private readonly TourReservationService tourReservationService;
        private readonly NotificationService notificationService;
        public AppointmentService()
        {
            //appointmentRepository = new AppointmentRepository();
            appointmentRepository = Injector.Injector.CreateInstance<IAppointmentRepository>();

            couponService = new CouponService();
            tourReservationService = new TourReservationService();
            notificationService = new NotificationService();
        }


        public void Subscribe(IObserver observer)
        {
            appointmentRepository.Subscribe(observer);
        }

        public void Create(int tourId, DateTime date)
        {

            Appointment appointment = new Appointment(tourId, date);
            appointmentRepository.Add(appointment);

        }

        public void RefreshAppointments()
        {
            appointmentRepository.RefreshAppointments();
        }

        public List<Appointment> GetByTourId(int id)
        {
            List<Appointment> allAppointments = appointmentRepository.GetAll();
            List<Appointment> appointments = new List<Appointment>();

            foreach (Appointment appoint in allAppointments)
            {
                if (appoint.TourId == id)
                {
                    appointments.Add(appoint);
                }
            }

            return appointments;

        }

        public int GetTourId(int id)
        {
            return appointmentRepository.GetById(id).TourId;
        }

        public Appointment GetById(int id)
        {
            return appointmentRepository.GetById(id);
        }

        public void Cancel(Appointment appointment, User guide, string tourName)
        {
            appointmentRepository.Cancel(appointment.Id);
            List<User> guests =  tourReservationService.GetGuestsWithReservation(appointment.Id);

            foreach(User guest in guests)
            {
                couponService.Create(guest.Id, appointment.DateAndTimeOfAppointment.AddYears(1),guide.Id);
                string message = $"Tour {tourName} ({appointment.DateAndTimeOfAppointment}) for which you have a reservation has been cancelled, as an apology, you received a coupon that you can use on any tour in the next year.";
                notificationService.Create(guest.Id, message);
            }

        }

        public void CancelGuideTour(Appointment appointment,string tourName)
        {
            appointmentRepository.Cancel(appointment.Id);

            List<User> guests = tourReservationService.GetGuestsWithReservation(appointment.Id);

            foreach (User guest in guests)
            {
                couponService.Create(guest.Id, appointment.DateAndTimeOfAppointment.AddYears(2), -2);
                string message = $"Tour {tourName} ({appointment.DateAndTimeOfAppointment}) for which you have a reservation has been cancelled, as an apology, you received a coupon that you can use on any tour in the next 2 years";
                notificationService.Create(guest.Id, message);
            }

            tourReservationService.RemoveReservationsForAppointment(appointment.Id);

        }

        public void CompleteTour(int id)
        {
            appointmentRepository.CompleteTour(id);
        }

    }
}
