using Microsoft.Win32.SafeHandles;
using Project.Command.Guest1Commands.WindowLinkCommands;
using Project.Model;
using Project.Service;
using Project.View.Guest1View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace Project.ViewModel.Guest1ViewModel
{
    public class ProfileWindowViewModel : ViewModelBase
    {

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }



        private readonly AccommodationReservationReviewService _reservationReviewService;
        private readonly Guest1NotificationService _guest1NotificationService;

        private readonly Notifier notifier;

        public ObservableCollection<Guest1Review> GivenReviews { get; set; }
        public ObservableCollection<OwnerReview> RecievedReviews { get; set; }

        public ProfileWindowViewModel(User u, Window window)
        {
            _reservationReviewService = new AccommodationReservationReviewService();
            _guest1NotificationService = new Guest1NotificationService();

            GivenReviews = new ObservableCollection<Guest1Review>();
            RecievedReviews = new ObservableCollection<OwnerReview>();
            User = u;
            Window = window;
            ProfileLinkCommand = new ProfileLinkCommand(this);
            YourReservationsLinkCommand = new YourReservationsLinkCommand(this);
            MoveReservationLinkCommand = new MoveReservationLinkCommand(this);
            SearchAccommodationsLinkCommand = new SearchAccommodationsLinkCommand(this);
            _reservationReviewService.FillGuestsGivenAndRecievedReviewsLists(GivenReviews, RecievedReviews, User.Id);

            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Window,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(10),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(8));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });

            if (User.SuperUserActivationDate.AddYears(1).Date >= DateTime.Now.Date)
            {
                Status = "Yes";
            }
            else
                Status = "No";


        }

        public ICommand ProfileLinkCommand { get; }
        public ICommand YourReservationsLinkCommand { get; }
        public ICommand MoveReservationLinkCommand { get; }
        public ICommand SearchAccommodationsLinkCommand { get; }


        public void ShowNotifications()
        {
            _guest1NotificationService.NotifyGuest(notifier, User.Id);
        }

        public void RemindGuestToRate()
        {
            _reservationReviewService.RemindGuestToRate(User.Id);
        }


    }
}
