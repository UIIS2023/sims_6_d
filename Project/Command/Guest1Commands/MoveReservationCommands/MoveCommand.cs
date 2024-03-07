using Project.Service;
using Project.View.Guest1View;
using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.Command.Guest1Commands.MoveReservationCommands
{
    public class MoveCommand : CommandBase
    {
        private readonly MoveReservationViewModel _viewModel;
        private readonly MoveRequestService _moveRequestService;


        public MoveCommand(MoveReservationViewModel viewModel, MoveRequestService moveRequestService)
        {
            _viewModel = viewModel;
            _moveRequestService = moveRequestService;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.SelectedReservation == null)
            {
                MessageBox.Show("Choose a reservation first!", "Reservation not chosen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_viewModel.SelectedReservation.StartDate <= DateTime.Now.Date)
            {
                MessageBox.Show("You can not move reservation that has already started", "Reservation already started", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_moveRequestService.DoesRequestAlreadyExist(_viewModel.SelectedReservation))
            {
                MessageBox.Show("There is already pending move request for this reservation!", "Request already exists", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MakeMoveRequestView makeMoveRequestView = new MakeMoveRequestView(_viewModel.SelectedReservation, _viewModel.User);
            makeMoveRequestView.ShowDialog();
        }
    }
}
