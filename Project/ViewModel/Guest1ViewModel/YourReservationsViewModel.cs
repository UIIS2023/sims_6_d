using Project.Command.Guest1Commands.WindowLinkCommands;
using Project.Command.Guest1Commands.YourReservationsCommands;
using Project.Model;
using Project.Observer;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel.Guest1ViewModel
{
    public class YourReservationsViewModel : ViewModelBase, IObserver
    {
        private readonly AccommodationReservationService _reservationService;
        private readonly OwnerNotificationService _ownerNotificationService;

        public ObservableCollection<AccommodationReservation> CurrentReservations { get; set; }
        public ObservableCollection<AccommodationReservation> FormerReservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }

        public ICommand ProfileLinkCommand { get; }
        public ICommand YourReservationsLinkCommand { get; }
        public ICommand MoveReservationLinkCommand { get; }
        public ICommand SearchAccommodationsLinkCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand RateCommand { get; }


        public YourReservationsViewModel(User user, Window window)
        {

            User = user;
            Window = window;

            _reservationService = new AccommodationReservationService();
            _ownerNotificationService = new OwnerNotificationService();
            _reservationService.SubscribeToReservationRepository(this);

            ProfileLinkCommand = new ProfileLinkCommand(this);
            YourReservationsLinkCommand = new YourReservationsLinkCommand(this);
            MoveReservationLinkCommand = new MoveReservationLinkCommand(this);
            SearchAccommodationsLinkCommand = new SearchAccommodationsLinkCommand(this);
            CancelCommand = new CancelReservationCommand(this, _ownerNotificationService, _reservationService);
            RateCommand = new RateOwnerCommand(this);

            CurrentReservations = new ObservableCollection<AccommodationReservation>(_reservationService.GetGuestsCurrentReservations(User.Id));
            FormerReservations = new ObservableCollection<AccommodationReservation>(_reservationService.GetGuestsFormerReservations(User.Id));

        }

        public void Update()
        {
            CurrentReservations.Clear();
            foreach (var reservation in _reservationService.GetGuestsCurrentReservations(User.Id))
            {
                CurrentReservations.Add(reservation);
            }
        }
    }
}
