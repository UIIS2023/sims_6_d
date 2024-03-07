using Project.Model;
using Project.Service;
using Project.ViewModel.TourGuideViewModel;
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

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for ComplexTourDetails.xaml
    /// </summary>
    public partial class ComplexTourDetails : Window
    {
        public ComplexTourDetails(ComplexTour complexTour, User guide, ComplexTourService complexTourService)
        {
            InitializeComponent();
            DataContext = new ComplexTourDetailsViewModel(complexTour,guide, complexTourService);
            
        }
    }
}
