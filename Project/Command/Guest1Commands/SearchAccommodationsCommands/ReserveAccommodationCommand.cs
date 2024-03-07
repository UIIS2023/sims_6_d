using Project.Model;
using Project.Service;
using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Command.Guest1Commands.SearchAccommodationsCommands
{
    public class ReserveAccommodationCommand : CommandBase
    {
        private readonly ReserveAccommodationViewModel viewModel;
        private readonly AccommodationReservationService reservationService;

        public ReserveAccommodationCommand(ReserveAccommodationViewModel viewModel, AccommodationReservationService reservationService)
        {
            this.viewModel = viewModel;
            this.reservationService = reservationService;
        }

        public override void Execute(object? parameter)
        {
            if (viewModel.SelectedReservation == null)
            {
                MessageBox.Show("Choose a reservation first!", "Reservation not chosen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to reserve this accommodation at chosen date?\n\nAccommodation Name: {viewModel.Accommodation.Name}\nNumber of guests: {viewModel.Guests}\nStart date: {viewModel.SelectedReservation.StartDate}\nEnd date: {viewModel.SelectedReservation.EndDate}", "Confirm reservation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (viewModel.User.Points > 0)
                {
                    viewModel.SelectedReservation.UsedPoints = true;
                }
                viewModel.SelectedReservation.Accommodation = viewModel.Accommodation;
                viewModel.SelectedReservation.Guest = viewModel.User;                
                reservationService.Add(viewModel.SelectedReservation, viewModel.User);
                MessageBox.Show("Accommodation has been successfully reserved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                viewModel.Window.Close();

            }

        }
    }
}
