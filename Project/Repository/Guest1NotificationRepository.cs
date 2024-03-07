using Project.Model;
using Project.RepositoryInterfaces;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Guest1NotificationRepository : IGuest1NotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/guest1Notifications.csv";

        private readonly Serializer<Guest1Notification> _serializer;

        private List<Guest1Notification> notifications;

        public Guest1NotificationRepository()
        {
            _serializer = new Serializer<Guest1Notification>();
            notifications = _serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            _serializer.ToCSV(FilePath, notifications);
        }

        private int GenerateId()
        {
            if (notifications.Count == 0) return 0;
            return notifications[notifications.Count - 1].Id + 1;
        }

        public Guest1Notification Add(Guest1Notification notification)
        {
            notification.Id = GenerateId();
            notifications.Add(notification);
            SaveInFile();
            return notification;
        }

        public Guest1Notification Remove(int id)
        {
            Guest1Notification notification = GetNotificationById(id);
            if (notification == null) return null;

            notifications.Remove(notification);
            SaveInFile();
            return notification;
        }

        public Guest1Notification GetNotificationById(int id)
        {
            return notifications.Find(v => v.Id == id);
        }

        public List<Guest1Notification> GetAllNotifications()
        {
            return notifications;
        }
    }
}
