using Project.Model;
using Project.Service;
using Project.View.OwnerView;
using Project.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Command.OwnerCommands.AddAccommodationCommands
{
    public class AddAccommodationCommand : CommandBase
    {
        private readonly AddAccommodationViewModel _viewModel;
        private readonly AccommodationService _accommodationService;

        public AddAccommodationCommand(AddAccommodationViewModel viewModel, AccommodationService accommodationService)
        {
            this._viewModel = viewModel;
            _accommodationService = accommodationService;
        }

        public override void Execute(object? parameter)
        {
            if (AreFieldsEmpty()) return;

            int bookingLeadTime = Convert.ToInt32(_viewModel.BookingLeadTime);
            int capacity = Convert.ToInt32(_viewModel.Capacity);
            int cancelation;
            if (string.IsNullOrWhiteSpace(_viewModel.CancellationPeriod))
            {
                cancelation = 1;
            }
            else
            {
                cancelation = Convert.ToInt32(_viewModel.CancellationPeriod);
            }
            AccommodationType type = GetAccommodationType();
            Location location = new(_viewModel.City, _viewModel.Country);

            Accommodation accommodation = new(_viewModel.Name, _viewModel.User.Id, type, location, capacity, bookingLeadTime, cancelation);
            _accommodationService.Add(accommodation);

            AddImages(accommodation);
            accommodation.Owner = _viewModel.User;

            MessageBox.Show("Accommodation successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            MenuView menuView = new MenuView(_viewModel.User);
            menuView.Show();
            _viewModel.Window.Close();

        }

        private bool AreFieldsEmpty()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Name))
            {
                MessageBox.Show("Please enter accommodation name.", "Name missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.Type))
            {
                MessageBox.Show("Please choose accommodation type.", "Type missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.Country))
            {
                MessageBox.Show("Please enter accommodation country.", "Country missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.City))
            {
                MessageBox.Show("Please enter accommodation city.", "City missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.Capacity))
            {
                MessageBox.Show("Please enter accommodation capacity.", "Capacity missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.BookingLeadTime))
            {
                MessageBox.Show("Please enter accommodation booking lead time.", "Booking lead time missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            if (_viewModel.ImageUrls.Count == 0)
            {
                MessageBox.Show("Please enter at least one image", "Accommodation images missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            return false;

        }

        private AccommodationType GetAccommodationType()
        {
            if (_viewModel.Type == "House")
            {
                return AccommodationType.HOUSE;
            }
            else if (_viewModel.Type == "Appartment")
            {
                return AccommodationType.APPARTMENT;
            }
            else
                return AccommodationType.COTTAGE;
        }

        private void AddImages(Accommodation accommodation)
        {
            foreach(var imageUrl in _viewModel.ImageUrls)
            {
                AccommodationImage image = new(0, imageUrl, accommodation.Id);
                _accommodationService.AddImage(image);
                accommodation.Images.Add(image);

            }
        }


    }
}
