using Project.Model;
using Project.Observer;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IPresentGuestsRepository : ISubject
    {
        public void Add(PresentGuests presentGuest);
        public void ClearPresents();
        public List<PresentGuests> GetAll();
        public List<int> GetAllGuestIds(int appointmentid);
        public List<PresentGuests> GetByAppointmentId(int id);
        public List<User> GetUserByAppointmentId(int id);
        public int GetBoardingPointByGuestIdAndAppointmentId(int guestId, int appointmentId);
    }
}
