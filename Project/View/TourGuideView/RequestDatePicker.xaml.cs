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
    /// Interaction logic for RequestDatePicker.xaml
    /// </summary>
    public partial class RequestDatePicker : Window
    {
        public RequestDatePicker(TourRequest request, User guide, TourService tourService, AppointmentService appointmentService)
        {
            InitializeComponent();
            var vm = new RequestDatePickerViewModel(request, guide, tourService,appointmentService);
            this.DataContext = vm;
            vm.ClosingRequest += (sender, e) => this.Close();

        }
    }
}
