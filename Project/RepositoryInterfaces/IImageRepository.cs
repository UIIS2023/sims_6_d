using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IImageRepository:ISubject
    {
        public Image Add(Image image);

        public Image Update(Image image);

        public Image Remove(int id);

        public Image GetImageById(int id);

        public List<Image> GetImagesByEntityIdandType(int id, PictureType type);

        public List<Image> GetAllImages();
    }
}
