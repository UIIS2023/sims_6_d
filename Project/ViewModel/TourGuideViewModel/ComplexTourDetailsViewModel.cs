using Project.Command;
using Project.Model;
using Project.Observer;
using Project.Service;
using Project.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class ComplexTourDetailsViewModel: ViewModelBase, IObserver
    {

		private TourRequest _selectedPart;
		public TourRequest SelectedPart
		{
			get
			{
				return _selectedPart;
			}
			set
			{
				_selectedPart = value;
				OnPropertyChanged(nameof(SelectedPart));
			}
		}

		private ObservableCollection<TourRequest> _parts = new ObservableCollection<TourRequest>();
		public ObservableCollection<TourRequest> Parts
		{
			get
			{
				return _parts;
			}
			set
			{
				_parts = value;
				OnPropertyChanged(nameof(Parts));
			}
		}

		private ComplexTour _selectedComplexTour =  new ComplexTour();
		public ComplexTour SelectedComplexTour
		{
			get
			{
				return _selectedComplexTour;
			}
			set
			{
				_selectedComplexTour = value;
				OnPropertyChanged(nameof(SelectedComplexTour));
			}
		}

		private User _guide = new User();
		public User Guide
		{
			get
			{
				return _guide;
			}
			set
			{
				_guide = value;
				OnPropertyChanged(nameof(Guide));
			}
		}

		private readonly AppointmentService _appointmentService;
		private readonly ComplexTourService _complexTourService;

		public ComplexTourDetailsViewModel(ComplexTour sendedComplexTour, User guide,ComplexTourService complexTourService)
        {
			Guide = guide;
			_appointmentService = new AppointmentService();
			_complexTourService = complexTourService;
			_appointmentService.Subscribe(this);
			SelectedComplexTour = sendedComplexTour;
			Parts = new ObservableCollection<TourRequest>(sendedComplexTour.complexTourParts);
            
        }

        private RelayCommand openPartCommand;
        public ICommand OpenPartCommand
        {
            get
            {
                if (openPartCommand == null)
                {
                    openPartCommand = new RelayCommand(param => this.OpenPart(), param => this.CanOpenPart());
                }
                return openPartCommand;
            }
        }

        private bool CanOpenPart()
        {
			return SelectedPart != null ;
        }

        private void OpenPart()
        {
            ComplexPart complexPart = new ComplexPart(SelectedPart, SelectedComplexTour, Guide,_appointmentService, _complexTourService);
			complexPart.Show();
        }

        public void Update()
        {
			UpdateParts();
        }

		private void UpdateParts()
		{
            Parts.Clear();

            foreach (TourRequest request in SelectedComplexTour.complexTourParts)
            {
                Parts.Add(request);
            }
        }
    }
}
