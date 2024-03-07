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
    /// Interaction logic for ComplexPart.xaml
    /// </summary>
    public partial class ComplexPart : Window
    {
        public ComplexPart(TourRequest selectedPart,ComplexTour complexTour, User guide, AppointmentService appService, ComplexTourService complexTourService)
        {
            InitializeComponent();
            var vm = new ComplexPartViewModel(selectedPart, complexTour, guide,appService,complexTourService);
            this.DataContext = vm;
            vm.ClosingRequest += (sender, e) => this.Close();
        }
    }
}
