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
    /// Interaction logic for YourAccommodationsView.xaml
    /// </summary>
    public partial class YourAccommodationsView : Window
    {
        private readonly YourAccommodationsViewModel _yourAccommodationsViewModel;
        public YourAccommodationsView(User user)
        {
            InitializeComponent();
            SignInView.PreviousWindow = this;
            _yourAccommodationsViewModel = new(user, this);
            DataContext = _yourAccommodationsViewModel;
        }

        
    }
}
