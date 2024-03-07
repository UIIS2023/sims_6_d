using Project.Model;
using Project.Service;
using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Command.Guest1Commands.YourReservationsCommands
{
    public class CancelReservationCommand : CommandBase
    {
        private readonly YourReservationsViewModel _yourReservationsViewModel;
        private readonly OwnerNotificationService _ownerNotificationService;
        private readonly AccommodationReservationService _accommodationReservationService;

        public CancelReservationCommand(YourReservationsViewModel yourReservationsViewModel, OwnerNotificationService ownerNotificationService, AccommodationReservationService accommodationReservationService)
        {
            _yourReservationsViewModel = yourReservationsViewModel;
            _ownerNotificationService = ownerNotificationService;
            _accommodationReservationService = accommodationReservationService;
        }

        public override void Execute(object? parameter)
        {
            if (_yourReservationsViewModel.SelectedReservation == null)
            {
                MessageBox.Show("Select a reservation first.", "Reservation not selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (HasCancellationPeriodPassed())
            {
                MessageBox.Show("You cannot cancel this reservation, cancellation period has passed!", "Cancellation period passed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to cancel this reservation?\n\nAccommodation Name: {_yourReservationsViewModel.SelectedReservation.Accommodation.Name}\nStart date: {_yourReservationsViewModel.SelectedReservation.StartDate}\nEnd date: {_yourReservationsViewModel.SelectedReservation.EndDate}", "Confirm cancellation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            string msg = $"Guest {_yourReservationsViewModel.User.Username} has cancelled reservation: Accommodation Name: {_yourReservationsViewModel.SelectedReservation.Accommodation.Name}, Start date: {_yourReservationsViewModel.SelectedReservation.StartDate}, End date: {_yourReservationsViewModel.SelectedReservation.EndDate} ";
            NotifyOwner(msg);

            _accommodationReservationService.Remove(_yourReservationsViewModel.SelectedReservation, _yourReservationsViewModel.User);
            MessageBox.Show("Reservation has been successfully cancelled.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private bool HasCancellationPeriodPassed()
        {
            return DateTime.Now.AddDays((double)_yourReservationsViewModel.SelectedReservation.Accommodation.CancellationPeriod) > _yourReservationsViewModel.SelectedReservation.StartDate;
        }

        private void NotifyOwner(string msg)
        {
            OwnerNotification notification = new OwnerNotification(_yourReservationsViewModel.User.Id, _yourReservationsViewModel.SelectedReservation.Accommodation.OwnerId, msg);
            _ownerNotificationService.Add(notification);
        }



    }
}
