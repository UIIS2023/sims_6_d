using Project.Model;
using Project.Repository;
using Project.RepositoryInterfaces;
using Project.View.Guest1View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Project.Service
{
    public class Guest1NotificationService
    {
        private readonly IGuest1NotificationRepository _repository;


        public Guest1NotificationService()
        {
            _repository = Injector.Injector.CreateInstance<IGuest1NotificationRepository>();
        }

        public void NotifyGuest(Notifier notifier, int userId)
        {
            List<Guest1Notification> notifications = new(GetGuestsNotifications(userId));

            if(notifications.Count == 0) { return; }

            foreach (var notification in notifications)
            {
                notifier.ShowInformation(notification.Message);
                _repository.Remove(notification.Id);
            }

        }


        private List<Guest1Notification> GetGuestsNotifications(int userId) 
        {
            List<Guest1Notification> notifications = new List<Guest1Notification>();

            foreach (var notification in _repository.GetAllNotifications())
            {
                if (notification.GuestId == userId)
                {
                    notifications.Add(notification);
                }
            }

            return notifications;
        }

        public void Add(Guest1Notification notification)
        {
            _repository.Add(notification);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

    }
}



/*if (notification.Type == NotificationType.INFO)
{
    notifier.ShowInformation(notification.Message);
    _repository.Remove(notification.Id);
    continue;
}
else if (notification.Type == NotificationType.ERROR)
{
    notifier.ShowError(notification.Message);
    _repository.Remove(notification.Id);
    continue;
}
else if (notification.Type == NotificationType.WARNING)
{
    notifier.ShowWarning(notification.Message);
    _repository.Remove(notification.Id);
    continue;
}
else
{
    notifier.ShowSuccess(notification.Message);
    _repository.Remove(notification.Id);
}*/