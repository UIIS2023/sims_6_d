using Project.Model;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class SuperGuestService
    {
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly IUserRepository _userRepository;

        public SuperGuestService()
        {
            _accommodationReservationService = new AccommodationReservationService();
            _userRepository =  Injector.Injector.CreateInstance<IUserRepository>();
        }

        public void UpdateGuestStatus(User guest)
        {
            if (guest.SuperUserActivationDate.Date.AddYears(1) >= DateTime.Now.Date)
            {
                return;
            }

            List<AccommodationReservation> reservations = _accommodationReservationService.GetGuestsLastYearReservations(guest.Id, DateTime.Now.AddYears(-1));

            if (reservations.Count >= 10)
            {
                guest.SuperUserActivationDate = DateTime.Now;
                guest.Points = 5;
                _userRepository.Update(guest);
            }
            else
            {
                if (guest.Points != 0)
                {
                    guest.Points = 0;
                    _userRepository.Update(guest);
                }
            }
            
        }



    }
}
