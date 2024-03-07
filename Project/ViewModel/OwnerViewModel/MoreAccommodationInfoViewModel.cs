using Project.Command.OwnerCommands;
using Project.Command.OwnerCommands.YourAccommodationsCommands;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Project.ViewModel.OwnerViewModel
{
    public class MoreAccommodationInfoViewModel : ViewModelBase
    {
        public Accommodation SelectedAccommodation { get; set; }
        public int Index { get; set; } = 0;

        private string _imageUrl;
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public List<AccommodationImage> Images { get; set; }

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand ReturnCommand { get; }
        public MoreAccommodationInfoViewModel(User user, Window window, Accommodation accommodation)
        {
            User = user;
            Window = window;
            SelectedAccommodation = accommodation;
            Images = SelectedAccommodation.GetAccommodationImages();
            ImageUrl = Images[Index].Url;

            NextCommand = new NextImageCommand(this);
            PreviousCommand = new PreviousImageCommand(this);
            ReturnCommand = new ReturnCommand(this);
        }
    }
}
