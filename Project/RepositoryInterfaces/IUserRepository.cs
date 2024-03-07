using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public User GetByUsername(string username);
        public User GetById(int id);
        public User Update(User user);
        public User ChangePassword(User user, string newPassword);
        public void Remove(int id);
    }
}
