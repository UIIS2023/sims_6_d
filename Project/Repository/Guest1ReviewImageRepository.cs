using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.RepositoryInterfaces;

namespace Project.Repository
{
    public class Guest1ReviewImageRepository : IGuest1ReviewImageRepository
    {
        private const string FilePath = "../../../Resources/Data/guest1ReviewImages.csv";

        private readonly Serializer<Guest1ReviewImage> serializer;

        private List<Guest1ReviewImage> images;

        public Guest1ReviewImageRepository()
        {
            serializer = new Serializer<Guest1ReviewImage>();
            images = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, images);
        }

        private int GenerateId()
        {
            if (images.Count == 0) return 0;
            return images[images.Count - 1].Id + 1;
        }

        public Guest1ReviewImage Add(Guest1ReviewImage image)
        {
            image.Id = GenerateId();
            images.Add(image);
            SaveInFile();
            return image;
        }

        public Guest1ReviewImage Update(Guest1ReviewImage image)
        {
            Guest1ReviewImage oldImage = GetImageById(image.Id);
            if (oldImage == null) return null;

            oldImage.Url = image.Url;
            oldImage.ReviewId = image.ReviewId;


            SaveInFile();
            return oldImage;
        }

        public Guest1ReviewImage Remove(int id)
        {
            Guest1ReviewImage image = GetImageById(id);
            if (image == null) return null;

            images.Remove(image);
            SaveInFile();
            return image;
        }

        public Guest1ReviewImage GetImageById(int id)
        {
            return images.Find(v => v.Id == id);
        }

        public List<Guest1ReviewImage> GetAllImages()
        {
            return images;
        }
    }
}
