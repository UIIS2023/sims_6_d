using Microsoft.Win32;
using Project.Controller;
using Project.Model;
using Project.Service;
using Project.ViewModel.TourGuideViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AddNewTour.xaml
    /// </summary>
    public partial class AddNewTour : Window
    {
        public AddNewTour(TourService tourService, AppointmentService appointmentService, User user, AddSharedViewModel sharedViewModel)
        {
            InitializeComponent();
            var vm = new AddNewTourViewModel(tourService,appointmentService,user,sharedViewModel);
            this.DataContext = vm;
            vm.ClosingRequest += (sender, e) => this.Close();
        }

        public AddNewTour(AddSharedViewModel sharedViewModel, User user,int requestId, TourService tourService,AppointmentService appointmentService)
        {
            InitializeComponent();
            var vm = new AddNewTourViewModel(sharedViewModel,user,requestId,tourService,appointmentService);
            this.DataContext = vm;
            vm.ClosingRequest += (sender, e) => this.Close();
        }

        public AddNewTour(AddSharedViewModel sharedViewModel, User user, int requestId, TourService tourService, AppointmentService appointmentService, ComplexTourService complexTourService)
        {
            InitializeComponent();
            var vm = new AddNewTourViewModel(sharedViewModel, user, requestId, tourService, appointmentService, complexTourService);
            this.DataContext = vm;
            vm.ClosingRequest += (sender, e) => this.Close();
        }
    }
}
