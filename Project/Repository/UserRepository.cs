using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Project.Model;
using Project.Serializer;
using Project.RepositoryInterfaces;

namespace Project.Repository
{
    public class UserRepository : IUserRepository
    {

        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> serializer;

        private List<User> users;

        public UserRepository()
        {
            serializer = new Serializer<User>();
            users = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, users);
        }

        public List<User> GetAllGuide()
        {
            List<User> Guides = new List<User>();
            foreach (var user in users)
            {
                if(user.Role == Role.GUIDE)
                {
                    Guides.Add(user);
                }
            }
            return Guides;
        }

        public User Update(User user)
        {
            Model.User oldUser = GetById(user.Id);
            if (oldUser == null) return null;

            oldUser.Username = user.Username;
            oldUser.Role = user.Role;
            oldUser.Age = user.Age;
            oldUser.Points = user.Points;
            oldUser.Password = user.Password;
            oldUser.SuperUserActivationDate = user.SuperUserActivationDate;

            SaveInFile();
            return oldUser;
        }

        public User ChangePassword(User user, string newPassword)
        {
            User oldUser = GetById(user.Id);
            if (oldUser == null) return null;
            oldUser.Password = newPassword;

            SaveInFile();
            return oldUser;
        }

        public User GetByUsername(string username)
        {
            users = serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }

        public User GetById(int id)
        {
            users = serializer.FromCSV(FilePath);
            return users.Find(v => v.Id == id);
        }

        public void Remove(int id)
        {
            users = serializer.FromCSV(FilePath);
            users.Remove(users.Find(v => v.Id == id));

            SaveInFile();
        }

    }
}
