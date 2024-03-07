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
using System.Windows.Input;
using System.Windows;
using Project.Observer;
using Project.Command.Guest1Commands.MoveReservationCommands;

namespace Project.ViewModel.Guest1ViewModel
{
    public class MoveReservationViewModel : ViewModelBase, IObserver
    {

        private readonly AccommodationReservationService _reservationService;

        private readonly MoveRequestService _requestService;

        public ObservableCollection<AccommodationReservation> CurrentReservations { get; set; }

        public ObservableCollection<MoveRequest> PendingRequests { get; set; }
        public ObservableCollection<MoveRequest> AcceptedRequests { get; set; }
        public ObservableCollection<MoveRequest> DeclinedRequests { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }

        public ICommand ProfileLinkCommand { get; }
        public ICommand YourReservationsLinkCommand { get; }
        public ICommand MoveReservationLinkCommand { get; }
        public ICommand SearchAccommodationsLinkCommand { get; }
        public ICommand MoveReservationCommand { get; }

        public MoveReservationViewModel(User u, Window window)
        {
            User = u;
            Window = window;

            _reservationService = new AccommodationReservationService();
            _requestService = new MoveRequestService();
            _requestService.SubscribeToRepository(this);

            ProfileLinkCommand = new ProfileLinkCommand(this);
            YourReservationsLinkCommand = new YourReservationsLinkCommand(this);
            MoveReservationLinkCommand = new MoveReservationLinkCommand(this);
            SearchAccommodationsLinkCommand = new SearchAccommodationsLinkCommand(this);
            MoveReservationCommand = new MoveCommand(this, _requestService);

            CurrentReservations = new ObservableCollection<AccommodationReservation>(_reservationService.GetGuestsCurrentReservations(User.Id));
            PendingRequests = new ObservableCollection<MoveRequest>(_requestService.GetGuestsPendingRequests(User.Id));
            AcceptedRequests = new ObservableCollection<MoveRequest>(_requestService.GetGuestsAcceptedRequests(User.Id));
            DeclinedRequests = new ObservableCollection<MoveRequest>(_requestService.GetGuestsDeclinedRequests(User.Id));
        }

        private void UpdatePendingRequests()
        {
            PendingRequests.Clear();
            foreach (var request in _requestService.GetGuestsPendingRequests(User.Id))
            {
                PendingRequests.Add(request);
            }
        }

        private void UpdateAcceptedRequests()
        {
            AcceptedRequests.Clear();
            foreach (var request in _requestService.GetGuestsAcceptedRequests(User.Id))
            {
                AcceptedRequests.Add(request);
            }
        }

        private void UpdateDeclinedRequests()
        {
            DeclinedRequests.Clear();
            foreach (var request in _requestService.GetGuestsDeclinedRequests(User.Id))
            {
                DeclinedRequests.Add(request);
            }
        }

        public void Update()
        {
            UpdatePendingRequests();
            UpdateAcceptedRequests();
            UpdateDeclinedRequests();
        }

    }
}
