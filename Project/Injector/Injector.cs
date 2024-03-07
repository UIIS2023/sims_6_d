using Project.Repository;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.RepositoryInterfaces;

namespace Project.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
        { typeof(IGuest1ReviewRepository), new Guest1ReviewRepository() },
        { typeof(IGuest1ReviewImageRepository), new Guest1ReviewImageRepository() },
        { typeof(IGuest1RemindNotificationRepository), new Guest1RemindNotificationRepository() },
        { typeof(IGuest1NotificationRepository), new Guest1NotificationRepository() },
        { typeof(IOwnerReviewRepository), new OwnerReviewRepository() },
        { typeof(IAccommodationReservationRepository), new AccommodationReservationRepository() },
        { typeof(IAccommodationRepository), new AccommodationRepository() },
        { typeof(IAccommodationImageRepository), new AccommodationImageRepository() },
        { typeof(IMoveRequestRepository), new MoveRequestRepository() },
        { typeof(IUserRepository), new UserRepository() },
        { typeof(ITourReviewRepository), new TourReviewRepository() },
            { typeof(ITourRepository), new TourRepository() },
            {typeof(ITourPointRepository), new TourPointRepository() },
            {typeof(ITourPointsListRepository), new TourPointsListRepository() },
            {typeof(IPresentGuestsRepository), new PresentGuestsRepository() },
            {typeof(IAppointmentRepository), new AppointmentRepository() },
            {typeof(ILocationRepository), new LocationRepository() },
            {typeof(ICouponRepository), new CouponRepository() },
            {typeof(IImageRepository), new ImageRepository() },
            {typeof(ITourReservationRepository), new TourReservationRepository() },
            {typeof(ITourRequestRepository), new TourRequestRepository() },
            {typeof(INotificationRepository), new NotificationRepository() },
            {typeof(IComplexTourRepository), new ComplexTourRepository() },
            {typeof(ISuperGuideRepository), new SuperGuideRepository() },
            

        // Services 
        //{ typeof(Guest1ReviewService), new Guest1ReviewService() },
        //{ typeof(AccommodationReservationService), new AccommodationReservationService() },
        //{ typeof(AccommodationReservationReviewService), new AccommodationReservationReviewService() },
        
        };

        /*public static void BindComponents()
        {
            Guest1ReviewRepository guest1ReviewRepository = new();
            Guest1ReviewImageRepository guest1ReviewImageRepository = new();
            Guest1RemindNotificationRepository guest1RemindNotificationRepository = new();
            OwnerReviewRepository ownerReviewRepository = new();
            AccommodationReservationRepository accommodationReservationRepository = new();
            _implementations.Add(typeof(IGuest1ReviewRepository), guest1ReviewRepository);
            _implementations.Add(typeof(IGuest1ReviewImageRepository), guest1ReviewImageRepository);
            _implementations.Add(typeof(IGuest1RemindNotificationRepository), guest1RemindNotificationRepository);
            _implementations.Add(typeof(IOwnerReviewRepository), ownerReviewRepository);
            _implementations.Add(typeof(IAccommodationReservationRepository), accommodationReservationRepository);
            Guest1ReviewService guest1ReviewService = new();
            _implementations.Add(typeof(Guest1ReviewService), guest1ReviewService);
            AccommodationReservationService accommodationReservationService = new();
            _implementations.Add(typeof(AccommodationReservationService), accommodationReservationService);
            AccommodationReservationReviewService accommodationReservationReviewService = new();
            _implementations.Add(typeof(AccommodationReservationReviewService), accommodationReservationReviewService);


        }*/

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
