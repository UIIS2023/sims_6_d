using Project.Model;
using Project.ViewModel.OwnerViewModel;
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

namespace Project.View.OwnerView
{
    /// <summary>
    /// Interaction logic for AddAccommodationForm.xaml
    /// </summary>
    public partial class AddAccommodationForm : Window
    {
        private readonly AddAccommodationViewModel viewModel;
        public AddAccommodationForm(User user)
        {
            InitializeComponent();
            SignInView.PreviousWindow = this;
            viewModel = new AddAccommodationViewModel(user, this);
            DataContext = viewModel;
        }
    }
}
