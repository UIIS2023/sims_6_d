using Project.Model;
using Project.Serializer;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class OwnerReviewRepository : IOwnerReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/ownerReview.csv";

        private readonly Serializer<OwnerReview> _serializer;

        private List<OwnerReview> _reviews;

        public OwnerReviewRepository()
        {
            _serializer = new Serializer<OwnerReview>();
            _reviews = _serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            _serializer.ToCSV(FilePath, _reviews);
        }

        private int GenerateId()
        {
            if (_reviews.Count == 0) return 0;
            return _reviews[_reviews.Count - 1].Id + 1;
        }

        public OwnerReview Add(OwnerReview review)
        {
            review.Id = GenerateId();
            _reviews.Add(review);
            SaveInFile();
            return review;
        }
        

        public OwnerReview AddOrUpdate(OwnerReview review)
        {
            OwnerReview oldReview = GetReviewByReservationId(review.ReservationId);
            if (oldReview == null)
            {
                review.Id = GenerateId();
                _reviews.Add(review);
                SaveInFile();
                return review;
            }
            oldReview.ReservationId = review.ReservationId;
            oldReview.Cleanliness = review.Cleanliness;
            oldReview.HousePolicies = review.HousePolicies;
            oldReview.Comment = review.Comment;
            SaveInFile();
            return oldReview;
        }
        

        public OwnerReview Update(OwnerReview review)
        {
            OwnerReview oldReview = GetReviewById(review.Id);
            if (oldReview == null) return null;

            oldReview.ReservationId = review.ReservationId;
            oldReview.Cleanliness = review.Cleanliness;
            oldReview.HousePolicies = review.HousePolicies;
            oldReview.Comment = review.Comment;
            SaveInFile();
            return oldReview;
        }

        public OwnerReview Remove(int id)
        {
            OwnerReview review = GetReviewById(id);
            if (review == null) return null;

            _reviews.Remove(review);
            SaveInFile();
            return review;
        }

        public OwnerReview GetReviewById(int id)
        {
            return _reviews.Find(v => v.Id == id);
        }

        public OwnerReview GetReviewByReservationId(int reservationId)
        {
            return _reviews.Find(r => r.ReservationId == reservationId);
        }

        public List<OwnerReview> GetAllReviews()
        {
            return _reviews;
        }

    }
}
