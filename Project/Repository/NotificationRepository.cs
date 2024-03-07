using Project.Model;
using Project.Observer;
using Project.RepositoryInterfaces;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/notifications.csv";

        private readonly Serializer<Notification> serializer;
        private readonly List<IObserver> _observers;

        private List<Notification> notifications;

        public NotificationRepository()
        {
            serializer = new Serializer<Notification>();
            _observers = new List<IObserver>();
            notifications = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, notifications);

        }

        private int GenerateId()
        {
            if (notifications.Count == 0) return 0;
            return notifications[notifications.Count - 1].Id + 1;
        }
        public int Add(Notification notification)
        {
            notification.Id = GenerateId();
            notifications.Add(notification);
            SaveInFile();
            NotifyObservers();

            return notification.Id;
        }

        public List<Notification> GetAll()
        {
            throw new NotImplementedException();
        }

        public Notification GetById(int id)
        {
            return notifications.Find(v => v.Id == id);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Remove(int id)
        {
            Notification notification = GetById(id);

            notifications.Remove(notification);
            SaveInFile();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer); 
        }

        public void Unsubscribe(IObserver observer)
        {
            throw new NotImplementedException();
        }
    }
}
