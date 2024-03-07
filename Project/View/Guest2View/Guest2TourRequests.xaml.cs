using Project.Model;
using Project.ViewModel.Guest2ViewModel;
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

namespace Project.View
{
    /// <summary>
    /// Interaction logic for Guest2TourRequests.xaml
    /// </summary>
    public partial class Guest2TourRequests : Window
    {
        public Guest2TourRequests(User user)
        {
            InitializeComponent();
            var vm = new Guest2TourRequestsViewModel(user);
            this.DataContext = vm;
            //vm.ClosingRequest += (sender, e) => this.Close();
        }

        
    }
}
