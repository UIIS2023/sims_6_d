using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface INotificationRepository: ISubject
    {
        public int Add(Notification notification);

        public void Remove(int id);

        public Notification GetById(int id);

        public List<Notification> GetAll();

    }
}
