using Project.Injector;
using Project.Model;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;
using ToastNotifications.Messages;

namespace Project.Service
{
    public class Guest1RemindNotificationService
    {
        private readonly IGuest1RemindNotificationRepository _repository;
        public Guest1RemindNotificationService()
        {
            _repository = Injector.Injector.CreateInstance<IGuest1RemindNotificationRepository>();
        }

        public void NotifyGuest(Notifier notifier, int guestId)
        {
            List<Guest1RemindNotification> notifications = new(GetGuestsNotifications(guestId));

            foreach (var notification in notifications)
            {
                if (notification.Date.Date.AddDays(5) >= DateTime.Now.Date)
                {
                    notifier.ShowInformation(notification.Message);
                }else
                    Remove(notification.Id);
            }

        }


        private List<Guest1RemindNotification> GetGuestsNotifications(int userId)
        {
            List<Guest1RemindNotification> notifications = new List<Guest1RemindNotification>();

            foreach (var notification in _repository.GetAllNotifications())
            {
                if (notification.Reservation.GuestId == userId)
                {
                    notifications.Add(notification);
                }
            }

            return notifications;
        }

        public void Add(Guest1RemindNotification notification)
        {
            _repository.Add(notification);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void CreateNewNotifications(List<AccommodationReservation> formerReservations)
        {
            foreach (var reservation in formerReservations)
            {
                if (reservation.EndDate.Date.AddDays(5) < DateTime.Now.Date)
                {
                    continue;
                }

                if (reservation.GuestReview != null)
                {
                    continue;
                }

                if (_repository.GetAllNotifications().Exists(n => n.ReservationId == reservation.Id))
                {
                    continue;
                }

                string msg = $"You can now rate reservation: Accommodation Name: {reservation.Accommodation.Name}, Start date: {reservation.StartDate}, End date: {reservation.EndDate}. You will be able to make a review until {reservation.EndDate.AddDays(5)}.";
                Guest1RemindNotification notification = new Guest1RemindNotification(reservation.Id, DateTime.Now, msg);
                Add(notification);
            }
        }

        public void RemoveByReservation(int reservationId)
        {
            var notification = _repository.GetAllNotifications().Find(n => n.ReservationId == reservationId);

            if (notification != null)
            {
                _repository.Remove(notification.Id);
            }
        }

    }
}
