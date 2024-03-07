using Project.Model;
using Project.Serializer;
using Project.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IGuest1ReviewRepository
    {
        public Guest1Review Add(Guest1Review review);

        public Guest1Review Update(Guest1Review review);

        public Guest1Review Remove(int id);

        public Guest1Review GetReviewById(int id);

        public List<Guest1Review> GetAllReviews();

    }
}
