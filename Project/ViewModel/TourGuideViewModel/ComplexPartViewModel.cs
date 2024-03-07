using Project.Command;
using Project.Model;
using Project.Service;
using Project.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Project.ViewModel.TourGuideViewModel
{

    public class ComplexPartViewModel: CloseableViewModel
    {
		private TourRequest _tourPart = new TourRequest();
		public TourRequest TourPart
		{
			get
			{
				return _tourPart;
			}
			set
			{
				_tourPart = value;
				OnPropertyChanged(nameof(TourPart));
			}
		}

		private string _startDate = string.Empty;
		public string StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
				OnPropertyChanged(nameof(StartDate));
			}
		}

		private string _endDate = string.Empty;
		public string EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				_endDate = value;
				OnPropertyChanged(nameof(EndDate));
			}
		}


		private DateTime _defaultDate;
		public DateTime DefaultDate
		{
			get
			{
				return _defaultDate;
			}
			set
			{
				_defaultDate = value;
				OnPropertyChanged(nameof(DefaultDate));
			}
		}

		private DateTime _appointmentDate;
		public DateTime AppointmentDate
		{
			get
			{
				return _appointmentDate;
			}
			set
			{
				_appointmentDate = value;
				OnPropertyChanged(nameof(AppointmentDate));
			}
		}

		private string _appointmentTime = string.Empty;
		public string AppointmentTime
		{
			get
			{
				return _appointmentTime;
			}
			set
			{
				_appointmentTime = value;
				OnPropertyChanged(nameof(AppointmentTime));
			}
		}

		private ComplexTour _selectedComplexTour = new ComplexTour();
		public ComplexTour SelectedComplexTour
		{
			get
			{
				return _selectedComplexTour;
			}
			set
			{
				_selectedComplexTour = value;
				OnPropertyChanged(nameof(ComplexTour));
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

		private readonly TourService tourService;
        private readonly AppointmentService _appointmentService;
		private readonly ComplexTourService _complexTourService;

        public ComplexPartViewModel(TourRequest selectedPart, ComplexTour complexTour, User guide, AppointmentService appointmentService, ComplexTourService complexTourService)
        {
            tourService = new TourService();
			_complexTourService = complexTourService;
            Guide = guide;
            TourPart = selectedPart;
            SelectedComplexTour = complexTour;

            StartDate = TourPart.StartDate.ToString("dd/MM/yyyy");
            EndDate = TourPart.EndDate.ToString("dd/MM/yyyy");
            DefaultDate = GetDefaulDate();
			AppointmentDate = DefaultDate;
            _appointmentService = appointmentService;
        }

        private DateTime GetDefaulDate()
		{
			if (DateTime.Compare(DateTime.Today.Date, TourPart.StartDate.Date) < 0)
			{
				return TourPart.StartDate.Date;
			}
			else
			{
				return DateTime.Today.Date;
			}


		}


        private RelayCommand scheduleCommand;
        public ICommand ScheduleCommand
        {
            get
            {
                if (scheduleCommand == null)
                {
                    scheduleCommand = new RelayCommand(param => this.Schedule(), param => this.CanSchedule());
                }
                return scheduleCommand;
            }
        }

        private bool CanSchedule()
        {
            string pattern = @"^(?:[01]\d|2[0-3]):[0-5]\d$";
            return Regex.IsMatch(AppointmentTime, pattern) && (AppointmentTime != null) && (AppointmentDate.Date >= DateTime.Today);
        }

        private void Schedule()
        {
            DateTime appointment = BuildDate(AppointmentDate, AppointmentTime);


			if(IsAccepted())
			{
                System.Windows.Forms.MessageBox.Show("You cannot schedule your appointment. This part of the tour has already been accepted");
            }
			else if (HasGuideAlreadySchedule())
			{
                System.Windows.Forms.MessageBox.Show("You have already made an appointment for this complex tour. " +
					"One guide can schedule only one appointment, so that other guides have the chance to schedule an appointment as well.");
            }
			else if (!IsGuideFree(appointment))
			{
                System.Windows.Forms.MessageBox.Show("You already have an organized tour in that period. Try another one");
            }
            else if(!CheckOtherParts(appointment))
            {
                System.Windows.Forms.MessageBox.Show("Someone has already scheduled a part of the complex tour in that period. If you are able, choose another appointment, if not, unfortunately you will not be able to accept this part of the tour");
            }
			else
			{
                AddSharedViewModel sharedViewModel = new AddSharedViewModel();
                sharedViewModel.Country = TourPart.Location.Country;
                sharedViewModel.City = TourPart.Location.City;
                sharedViewModel.Language = TourPart.Language;
                sharedViewModel.GuestNumber = TourPart.GuestNumber;
                sharedViewModel.Appointment = appointment;

                AddNewTour addNewTour = new AddNewTour(sharedViewModel, Guide, TourPart.Id, tourService, _appointmentService,_complexTourService);
                addNewTour.Show();
                Close();
            }
        }

        public bool IsGuideFree(DateTime dateTime)
        {
            bool isFree = true;
            List<Tour> tours = tourService.GetAllTourAppointments(Guide.Id);
            foreach (Tour tour in tours)
            {
                DateTime dateTime1 = tour.TourAppointment.DateAndTimeOfAppointment.AddHours(tour.Duration);
                if (dateTime >= tour.TourAppointment.DateAndTimeOfAppointment && dateTime < dateTime1)
                {
                    isFree = false;
                }
            }

            return isFree;
        }

		private bool CheckOtherParts(DateTime dateTime)
		{
			bool NoOverlap = true;
			
			foreach(TourRequest tourRequest in SelectedComplexTour.complexTourParts)
			{
				if(tourRequest.AcceptedAppointment == dateTime)
				{
					NoOverlap = false;
				}
			}

			return NoOverlap;
		}

		private bool HasGuideAlreadySchedule()
		{
			bool alredySchedule = false;

            foreach (TourRequest tourRequest in SelectedComplexTour.complexTourParts)
            {
                if (tourRequest.GuideId == Guide.Id)
                {
                    alredySchedule = true;
                }
            }
			return alredySchedule;
        }

		private bool IsAccepted()
		{
			if(TourPart.Status == TourRequest.STATUS.ACCEPTED)
			{
				return true;
			}
            
            return false;
        }


        private DateTime BuildDate(DateTime date, string time)
        {
            string[] splitedTime = time.Split(':');
            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, int.Parse(splitedTime[0]), int.Parse(splitedTime[1]), 0);
            return newDate;
        }

        private RelayCommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(param => this.Close(), param => this.CanClose());
                }
                return closeCommand;
            }
        }

        private bool CanClose()
        {
            return true;
        }

        private void Close()
        {
            this.OnClosingRequest();
        }
    }
}
