using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications.Core;

namespace Project.RepositoryInterfaces
{
    public interface IGuest1RemindNotificationRepository
    {
        public Guest1RemindNotification Add(Guest1RemindNotification notification);

        public Guest1RemindNotification Remove(int id);

        public Guest1RemindNotification GetNotificationById(int id);

        public List<Guest1RemindNotification> GetAllNotifications();
    }
}
