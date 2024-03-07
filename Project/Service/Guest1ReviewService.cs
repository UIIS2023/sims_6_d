using Project.Injector;
using Project.Model;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class Guest1ReviewService
    {
        private readonly IGuest1ReviewRepository _reviewRepository;
        private readonly IGuest1ReviewImageRepository _imageRepository;


        public Guest1ReviewService()
        {
            _reviewRepository = Injector.Injector.CreateInstance<IGuest1ReviewRepository>();
            _imageRepository = Injector.Injector.CreateInstance<IGuest1ReviewImageRepository>();
            LinkReviewsAndImages();
        }

        public void Add(Guest1Review review)
        {
            _reviewRepository.Add(review);
        }

        public void AddImage(Guest1ReviewImage image)
        {
            _imageRepository.Add(image);
        }

        public List<Guest1Review> GetAllReviews()
        {
            return _reviewRepository.GetAllReviews();
        }

        public Guest1Review GetReviewByReservationId(int reservationId)
        {
            return GetAllReviews().Find(r => r.ReservationId == reservationId);

        }

        private void LinkReviewsAndImages()
        {
            foreach (var image in _imageRepository.GetAllImages())
            {
                var review = _reviewRepository.GetReviewById(image.ReviewId);

                if (review == null)
                {
                    continue;
                }
                if (review.Images.Exists(i => i.Id == image.Id))
                {
                    continue;
                }

                review.Images.Add(image);
            }
        }


    }
}
