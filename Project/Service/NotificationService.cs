using Project.Model;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;


        public NotificationService()
        {
            _notificationRepository = Injector.Injector.CreateInstance<INotificationRepository>();
        }

        public int Create(int recipientId, string message)
        {

            Notification notification = new Notification(recipientId,message);

            int notificationId = _notificationRepository.Add(notification);

            return notificationId;

        }

        public List<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public void Remove(int id)
        {
            _notificationRepository.Remove(id);
        }

        public Notification GetById(int id)
        {
            return _notificationRepository.GetById(id);
        }

        public List<Notification> GetByRecipientId(int id)
        {
            List <Notification> filteredNotifications = new List<Notification>();
            List<Notification> allNotifications = _notificationRepository.GetAll();

            foreach (Notification notification in allNotifications)
            {
                if(notification.Id == id && !notification.IsSended)
                {
                    filteredNotifications.Add(notification);
                }
            }

            return filteredNotifications;
        }
    }
}
