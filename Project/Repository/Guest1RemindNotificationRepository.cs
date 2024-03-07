using Project.Model;
using Project.Serializer;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Guest1RemindNotificationRepository : IGuest1RemindNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/guest1RemindNotifications.csv";

        private readonly Serializer<Guest1RemindNotification> _serializer;

        private List<Guest1RemindNotification> notifications;

        public Guest1RemindNotificationRepository()
        {
            _serializer = new Serializer<Guest1RemindNotification>();
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

        public Guest1RemindNotification Add(Guest1RemindNotification notification)
        {
            notification.Id = GenerateId();
            notifications.Add(notification);
            SaveInFile();
            return notification;
        }

        public Guest1RemindNotification Remove(int id)
        {
            Guest1RemindNotification notification = GetNotificationById(id);
            if (notification == null) return null;

            notifications.Remove(notification);
            SaveInFile();
            return notification;
        }

        public Guest1RemindNotification GetNotificationById(int id)
        {
            return notifications.Find(v => v.Id == id);
        }

        public List<Guest1RemindNotification> GetAllNotifications()
        {
            return notifications;
        }
    }
}
