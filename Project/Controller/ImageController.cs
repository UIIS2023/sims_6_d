using Project.Model;
using Project.Observer;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class ImageController
    {
        //ImageRepository imageRepository { get; set; }
        private IImageRepository imageRepository;

        public ImageController()
        {
            //imageRepository = new ImageRepository();
            imageRepository = Injector.Injector.CreateInstance<IImageRepository>();
        }

        public void Subscribe(IObserver observer)
        {
            imageRepository.Subscribe(observer);
        }
        public void Create(string url, int entityId, PictureType type)
        {
            Image image = new Image(url, entityId, type);
            imageRepository.Add(image);


        }

        public List<string> GetImageUrlByTourId(int id)
        {
            List<Image> imageList = imageRepository.GetImagesByEntityIdandType(id, PictureType.TOUR);
            List<string> urls = new List<string>();

            foreach (Image image in imageList)
            {
                urls.Add(image.Url);
            }

            return urls;
        }
    }
}
