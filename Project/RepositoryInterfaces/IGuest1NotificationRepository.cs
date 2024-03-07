using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications.Core;

namespace Project.RepositoryInterfaces
{
    public interface IGuest1NotificationRepository
    {
        public Guest1Notification Add(Guest1Notification notification);

        public Guest1Notification Remove(int id);

        public Guest1Notification GetNotificationById(int id);

        public List<Guest1Notification> GetAllNotifications();
    }
}
