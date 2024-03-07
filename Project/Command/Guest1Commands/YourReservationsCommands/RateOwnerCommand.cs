using Project.Model;
using Project.View.Guest1View;
using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Command.Guest1Commands.YourReservationsCommands
{
    public class RateOwnerCommand : CommandBase
    {
        private readonly YourReservationsViewModel _yourReservationsViewModel;

        public RateOwnerCommand(YourReservationsViewModel yourReservationsViewModel)
        {
            _yourReservationsViewModel = yourReservationsViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_yourReservationsViewModel.SelectedReservation == null)
            {
                MessageBox.Show("Select a reservation first.", "Reservation not selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_yourReservationsViewModel.SelectedReservation.EndDate >= DateTime.Now.Date)
            {
                MessageBox.Show("You will be able to rate owner and accommodation when reservation finishes.", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (_yourReservationsViewModel.SelectedReservation.EndDate.AddDays(5).Date < DateTime.Now.Date)
            {
                MessageBox.Show($"Rate period has passed - {_yourReservationsViewModel.SelectedReservation.EndDate.AddDays(5).Date} was final date for rating!");
                return;
            }

            if (_yourReservationsViewModel.SelectedReservation.GuestReview != null)
            {
                MessageBox.Show("You have already rated this reservation!", "Already rated", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            RateOwnerForm rateOwnerForm = new RateOwnerForm(_yourReservationsViewModel.User, _yourReservationsViewModel.SelectedReservation);
            rateOwnerForm.ShowDialog();

        }
    }
}
