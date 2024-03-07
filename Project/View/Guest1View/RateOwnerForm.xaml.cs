using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project.Injector;
using Project.Model;
using Project.Service;
using Project.RepositoryInterfaces;
using System.Collections.ObjectModel;
using Project.View.TourGuideView;

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for RateOwnerForm.xaml
    /// </summary>
    public partial class RateOwnerForm : Window
    {

        private Guest1ReviewService _reviewService { get; set; }
        private OwnerNotificationService _ownerNotificationService { get; set; }

        private Guest1RemindNotificationService _remindNotificationService { get; set; }

        private User user;
        public int Cleanliness { get; set; } = 1;
        public int OwnerBehaviour { get; set; } = 1;
        public string Comment { get; set; } = string.Empty;

        public int RenovationEmergencyLevel { get; set; } = 0;
        public ObservableCollection<string> ImageUrls { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }
        public RateOwnerForm(User u, AccommodationReservation reservation)
        {
            InitializeComponent();
            DataContext = this;

            _reviewService = new Guest1ReviewService();
            _ownerNotificationService = new OwnerNotificationService();
            _remindNotificationService = new Guest1RemindNotificationService();
            SelectedReservation = reservation;
            ImageUrls = new ObservableCollection<string>();
            user = u;
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (tbImageUrl.Text == string.Empty)
            {
                return;
            }
            if(tbImageUrl.Text == "Paste image urls here")
            {
                return;
            }

            ImageUrls.Add(tbImageUrl.Text);
            tbImageUrl.Text = string.Empty;
        }

        private void chbRenovation_Checked(object sender, RoutedEventArgs e)
        {
            tbRenovation.Foreground = new SolidColorBrush(Colors.Black);
            wpRenovation.IsEnabled = true;
            lblRenovation.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        private void chbRenovation_Unchecked(object sender, RoutedEventArgs e)
        {
            tbRenovation.Foreground = new SolidColorBrush(Colors.Gray);
            wpRenovation.IsEnabled = false;
            lblRenovation.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void btnSendRecension_Click(object sender, RoutedEventArgs e)
        {
            if (Comment == string.Empty)
            {
                MessageBox.Show("Please write a comment.");
                return;
            }

            MessageBoxResult result = ConfirmRatingMessageBox();
            if(result == MessageBoxResult.No) { return; }

            Guest1Review review = new Guest1Review(Cleanliness, OwnerBehaviour, Comment, SelectedReservation.Id);

            if ((bool)chbRenovation.IsChecked)
            {
                review.RenovationEmergencyLevel = RenovationEmergencyLevel;
            }

            AddImages(review);

            SelectedReservation.GuestReview = review;
            review.Reservation = SelectedReservation;
            _reviewService.Add(review);

            NotifyOwner();
            _remindNotificationService.RemoveByReservation(SelectedReservation.Id);

            Close();
        }

        private MessageBoxResult ConfirmRatingMessageBox()
        {
            //Owner username: {SelectedReservation.Accommodation.Owner.Username}
            MessageBoxResult result = MessageBox.Show($"Accommodation Name: {SelectedReservation.Accommodation.Name}\nStart date: {SelectedReservation.StartDate.Date}\nEnd date: {SelectedReservation.EndDate.Date}\n\nAre you sure you want to rate this accommodation?", "Confirm review",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

            return result;
        }

        private void NotifyOwner()
        {
            if (SelectedReservation.OwnerReview != null)
            {
                string msg = $"Guest {user.Username} has rated you for reservation: Accommodation _name: {SelectedReservation.Accommodation.Name}, Start date: {SelectedReservation.StartDate.Date}, End date: {SelectedReservation.EndDate.Date}";
                OwnerNotification notifiation = new OwnerNotification(user.Id, SelectedReservation.Accommodation.OwnerId, msg);
                _ownerNotificationService.Add(notifiation);
            }
            else
            {
                string msg = $"Guest {user.Username} has rated you for reservation: Accommodation _name: {SelectedReservation.Accommodation.Name}, Start date: {SelectedReservation.StartDate.Date}, End date: {SelectedReservation.EndDate.Date}. You will be able to see his review once you rate him or when rate period ends.";
                OwnerNotification notifiation = new OwnerNotification(user.Id, SelectedReservation.Accommodation.OwnerId, msg);
                _ownerNotificationService.Add(notifiation);
            }
        }

        private void AddImages(Guest1Review review)
        {
            foreach (var imageUrl in ImageUrls)
            {
                Guest1ReviewImage image = new(imageUrl, review.Id);
                _reviewService.AddImage(image);
                review.Images.Add(image);

            }
        }

        private void tbImageUrl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbImageUrl.Text == "Paste image urls here")
            {
                tbImageUrl.Text = "";
                tbImageUrl.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void tbImageUrl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbImageUrl.Text))
            {
                tbImageUrl.Text = "Paste image urls here";
                tbImageUrl.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
