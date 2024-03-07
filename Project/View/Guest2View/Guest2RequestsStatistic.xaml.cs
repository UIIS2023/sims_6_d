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
    /// Interaction logic for Guest2RequestsStatistic.xaml
    /// </summary>
    public partial class Guest2RequestsStatistic : Window
    {
        public Guest2RequestsStatistic(User user)
        {
            InitializeComponent();
            var viewModel = new Guest2RequestsStatisticViewModel();
            this.DataContext = viewModel;
        }
    }
}
