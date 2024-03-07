using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;
using ToastNotifications.Messages;

namespace Project.Service
{
    public class OwnerNotificationService
    {
        private readonly OwnerNotificationRepository _repository;


        public OwnerNotificationService()
        {
            _repository = new OwnerNotificationRepository();
        }

        public void NotifyOwner(Notifier notifier, int userId)
        {
            List<OwnerNotification> notifications = new(GetOwnerNotifications(userId));

            if (notifications.Count == 0) { return; }

            foreach (var notification in notifications)
            {
                notifier.ShowInformation(notification.Message);
                _repository.Remove(notification.Id);
            }

        }


        private List<OwnerNotification> GetOwnerNotifications(int userId)
        {
            List<OwnerNotification> notifications = new List<OwnerNotification>();

            foreach (var notification in _repository.GetAllNotifications())
            {
                if (notification.OwnerId == userId)
                {
                    notifications.Add(notification);
                }
            }

            return notifications;
        }

        public void Add(OwnerNotification notification)
        {
            _repository.Add(notification);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }
    }
}
