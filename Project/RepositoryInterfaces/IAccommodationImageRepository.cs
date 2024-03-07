using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IAccommodationImageRepository
    {
        public AccommodationImage Add(AccommodationImage image);

        public AccommodationImage Update(AccommodationImage image);

        public AccommodationImage Remove(int id);

        public AccommodationImage GetImageById(int id);

        public List<AccommodationImage> GetAllImages();

    }
}
