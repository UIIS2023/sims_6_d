using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class OwnerNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/ownerNotifications.csv";

        private readonly Serializer<OwnerNotification> _serializer;

        private List<OwnerNotification> notifications;

        public OwnerNotificationRepository()
        {
            _serializer = new Serializer<OwnerNotification>();
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

        public int GetLastId()
        {
            if (notifications.Count == 0) return 0;
            return notifications[notifications.Count - 1].Id + 1;
        }

        public OwnerNotification Add(OwnerNotification notification)
        {
            notification.Id = GenerateId();
            notifications.Add(notification);
            SaveInFile();
            return notification;
        }

        public OwnerNotification Remove(int id)
        {
            OwnerNotification notification = GetNotificationById(id);
            if (notification == null) return null;

            notifications.Remove(notification);
            SaveInFile();
            return notification;
        }

        public OwnerNotification GetNotificationById(int id)
        {
            return notifications.Find(v => v.Id == id);
        }

        public List<OwnerNotification> GetAllNotifications()
        {
            return notifications;
        }
    }
}
