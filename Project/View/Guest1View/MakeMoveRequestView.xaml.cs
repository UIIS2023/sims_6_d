using Project.Model;
using Project.Repository;
using Project.Service;
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

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for MakeMoveRequestView.xaml
    /// </summary>
    public partial class MakeMoveRequestView : Window
    {
        private MoveRequestService _requestService;

        private User user;
        public int Days { get; set; }

        private OwnerNotificationService _ownerNotificationService;
        public AccommodationReservation SelectedReservation { get; set; }

        public DateTime NewStartDate { get; set; } = default;
        public DateTime NewEndDate { get; set; } = default;
        public string Comment { get; set; } = string.Empty;

        public MakeMoveRequestView(AccommodationReservation reservation, User user)
        {
            InitializeComponent();
            DataContext = this;

            _requestService = new MoveRequestService();
            _ownerNotificationService = new OwnerNotificationService();
            SelectedReservation = reservation;
            Days = (int)(SelectedReservation.EndDate - SelectedReservation.StartDate).TotalDays;
            
            this.user = user;
        }

        private void btnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckConditions())
                return;

            if(!_requestService.IsAccommodationFree(SelectedReservation, NewStartDate, NewEndDate))
            {
                AlreadyReservedMessageBox();
                return;
            }

            if (_requestService.DoesRequestAlreadyExist(SelectedReservation))
            {
                MessageBox.Show("There is already move request on pending for this reservation!", "Request already exists", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MoveRequest request = new(SelectedReservation.Id, MoveRequestStatus.PENDING, "", Comment, NewStartDate, NewEndDate);
            request.Reservation = SelectedReservation;
            _requestService.Add(request);

            RequestSentMessageBox();
            NotifyOwner();
            Close();
            

        }

        private void MissingInputMessageBox(string item)
        {
            string sMessageBoxText = $"Please enter {item}!";
            string sCaption = $"Missing input: {item}";

            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;


            MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        private void AlreadyReservedMessageBox()
        {
            string sMessageBoxText = "The accommodation is already reserved at chosen date range!";
            string sCaption = "Accommodtion already reserved";
            MessageBoxButton btn = MessageBoxButton.OK;
            MessageBoxImage icn = MessageBoxImage.Error;

            MessageBox.Show(sMessageBoxText, sCaption, btn, icn);
        }

        private void RequestSentMessageBox()
        {
            string messageBoxText = "The request has been sent!";
            string caption = "Success";
            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, btnMessageBox, icnMessageBox);
        }


        private bool AreInputsEmpty()
        {
            if (NewStartDate == default)
            {
                MissingInputMessageBox("new start date");
                return true;
            }
            if (NewEndDate == default)
            {
                MissingInputMessageBox("new end date");
                return true;
            }
            if (Comment == string.Empty)
            {
                MissingInputMessageBox("comment");
                return true;
            }

            return false;
        }

        private void NotifyOwner()
        {
            string msg = $"Guest {user.Username} has made move request for your accommodation {SelectedReservation.Accommodation.Name}!";
            OwnerNotification notification = new(SelectedReservation.GuestId, SelectedReservation.Accommodation.OwnerId, msg);
            _ownerNotificationService.Add(notification);
        }

        private bool IsEndBeforeStart()
        {
            if (NewStartDate.Date > NewEndDate.Date)
            {
                string sMessageBoxText = $"Start date cannot be before end date!";
                string sCaption = "Date not valid";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                return true;
            }

            return false;
        }

        private bool CheckConditions()
        {

            if(AreInputsEmpty()) return false;
            // Date check
            if (IsEndBeforeStart()) return false;

            return true;
        }
    }
}
