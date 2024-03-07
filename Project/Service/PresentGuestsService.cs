using Project.Model;
using Project.Observer;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{

    public class PresentGuestsService
    {
        private IPresentGuestsRepository presentGuestsRepository;
        //PresentGuestsRepository presentGuestsRepository;
        UserRepository userRepository;


        private readonly TourPointService tourPointService;
        private readonly TourReservationService tourReservationService;

        

        public PresentGuestsService()
        {
            //presentGuestsRepository = new PresentGuestsRepository();
            presentGuestsRepository = Injector.Injector.CreateInstance<IPresentGuestsRepository>();
            userRepository = new UserRepository();
            tourPointService = new TourPointService();
            tourReservationService = new TourReservationService();

            
        }

        public void Create(PresentGuests presentGuest)
        {
            presentGuestsRepository.Add(presentGuest);
        }

        public List<User> GetPresentGuestsOfTheAppointment(int id)
        {
            List<User> guestList = new List<User>();
            List<PresentGuests> presentGuests = presentGuestsRepository.GetAll();

            foreach(PresentGuests present in presentGuests)
            {
                if(present.AppointmentId == id)
                {
                    guestList.Add(userRepository.GetById(present.GuestId));
                }
            }

            return guestList;
        }

        public List<User> GetNotPresentGuests(int appointmentId)
        {
            List<User> notPresent = new List<User>();

            foreach (User guest in  tourReservationService.GetApproprietReservations(appointmentId))
            {
                if (!presentGuestsRepository.GetAllGuestIds(appointmentId).Contains(guest.Id))
                {
                    notPresent.Add(guest);
                }
            }

            return notPresent;
        }

        public List<User> GetUserByAppointmentId(int id)
        {
            return presentGuestsRepository.GetUserByAppointmentId(id);
        }

        public List<PresentGuests> GetByAppointmentId(int id)
        {
            return presentGuestsRepository.GetByAppointmentId(id);
        }

        public int GetUnder18(int id)
        {
            List<User> guests = GetPresentGuestsOfTheAppointment(id);
            int count = 0;
            foreach(User user in guests)
            {
                if (user.Age < 18) { count++; }
            }

            return count;
        }

        public int GetBetween18and50(int id)
        {
            List<User> guests = GetPresentGuestsOfTheAppointment(id);
            int count = 0;
            foreach (User user in guests)
            {
                if (user.Age > 18 && user.Age < 50) { count++; }
            }

            return count;
        }

        public int GetOver50(int id)
        {
            List<User> guests = GetPresentGuestsOfTheAppointment(id);
            int count = 0;
            foreach (User user in guests)
            {
                if (user.Age > 50) { count++; }
            }

            return count;
        }

        public int GetNumberOfGuests(int id)
        {
            List<User> guests = GetPresentGuestsOfTheAppointment(id);
            return guests.Count;
        }

        public int GetNumberOfGuestsWithCoupon(int id)
        {
            int guestsWithCoupon = 0;
            List<PresentGuests> guests = presentGuestsRepository.GetAll();
            foreach(PresentGuests guest in guests)
            {
                if(guest.AppointmentId == id && guest.Coupon)
                {
                    guestsWithCoupon++;
                }
            }
            return guestsWithCoupon;
        }

        public string GetBoardingPoint(int guestId, int appointmentId)
        {
            int boardingPointId = presentGuestsRepository.GetBoardingPointByGuestIdAndAppointmentId(guestId, appointmentId);
            if(boardingPointId == -1)
            {
                return "N/A";
            }
            else
            {
                return tourPointService.GetPointNameById(boardingPointId);
            }
            
        }

        public void Subscribe(IObserver observer)
        {
            presentGuestsRepository.Subscribe(observer);
        }



    }
}
