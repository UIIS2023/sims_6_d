using Project.Command.Guest1Commands.WindowLinkCommands;
using Project.Model;
using Project.Service;
using Project.ViewModel;
using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for SearchAccommodationsView.xaml
    /// </summary>
    public partial class SearchAccommodationsView : Window
    {

        private readonly SearchAccommodationsViewModel _searchAccommodationsViewModel;

        public SearchAccommodationsView(User user)
        {
            InitializeComponent();
            _searchAccommodationsViewModel = new SearchAccommodationsViewModel(user, this);
            DataContext = _searchAccommodationsViewModel;
        }

    }
}
