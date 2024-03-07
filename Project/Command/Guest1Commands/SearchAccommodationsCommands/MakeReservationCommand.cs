using Project.View.Guest1View;
using Project.ViewModel;
using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Command.Guest1Commands.SearchAccommodationsCommands
{
    public class MakeReservationCommand : CommandBase
    {
        SearchAccommodationsViewModel searchAccommodationsViewModel;

        public MakeReservationCommand(SearchAccommodationsViewModel viewModel)
        {
            searchAccommodationsViewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (searchAccommodationsViewModel.SelectedAccommodation == null)
            {
                MessageBox.Show("Choose an accommodation first!", "Accommodation not selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ReserveAccommodationWindow reserveAccommodationWindow = new ReserveAccommodationWindow(searchAccommodationsViewModel.SelectedAccommodation, searchAccommodationsViewModel.User);
            reserveAccommodationWindow.ShowDialog();
        }
    }
}
