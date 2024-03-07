using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IGuest1ReviewImageRepository
    {
        public Guest1ReviewImage Add(Guest1ReviewImage image);

        public Guest1ReviewImage Update(Guest1ReviewImage image);

        public Guest1ReviewImage Remove(int id);

        public Guest1ReviewImage GetImageById(int id);

        public List<Guest1ReviewImage> GetAllImages();
    }
}
