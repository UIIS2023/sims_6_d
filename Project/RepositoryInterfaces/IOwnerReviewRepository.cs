using Project.Model;
using Project.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IOwnerReviewRepository
    {
        public OwnerReview Add(OwnerReview review);

        public OwnerReview AddOrUpdate(OwnerReview review);

        public OwnerReview Update(OwnerReview review);

        public OwnerReview Remove(int id);

        public OwnerReview GetReviewById(int id);       

        public OwnerReview GetReviewByReservationId(int reservationId);

        public List<OwnerReview> GetAllReviews();
    }
}
