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
using System.Windows.Input;
using ToastNotifications.Messages.Core;

namespace Project.ViewModel.TourGuideViewModel
{
    public class RequestDatePickerViewModel: CloseableViewModel
    {
		private DateTime _date;
		public DateTime Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
				OnPropertyChanged(nameof(Date));
			}
		}

		private string _time = string.Empty;
		public string Time
		{
			get
			{
				return _time;
			}
			set
			{
				_time = value;
				OnPropertyChanged(nameof(Time));
			}
		}

		private DateTime _start;
		public DateTime Start
		{
			get
			{
				return _start;
			}
			set
			{
				_start = value;
				OnPropertyChanged(nameof(Start));
			}
		}

		private DateTime _end;
		public DateTime End
		{
			get
			{
				return _end;
			}
			set
			{
				_end = value;
				OnPropertyChanged(nameof(End));
			}
		}

		private CalendarDateRange _blackoutEnd = new CalendarDateRange();
		public CalendarDateRange BlackoutEnd
		{
			get
			{
				return _blackoutEnd;
			}
			set
			{
				_blackoutEnd = value;
				OnPropertyChanged(nameof(BlackoutEnd));
			}
		}

		private CalendarDateRange _blackoutStart = new CalendarDateRange();
		public CalendarDateRange BlackoutStart
		{
			get
			{
				return _blackoutStart;
			}
			set
			{
				_blackoutStart = value;
				OnPropertyChanged(nameof(BlackoutStart));
			}
		}

		private TourRequest tourRequest;
		private User Guide;

		private readonly TourService tourService;
		private readonly AppointmentService appointmentService;

        public RequestDatePickerViewModel(TourRequest request, User guide, TourService tourServicee, AppointmentService appointmentServicee)
        {
			tourService = tourServicee;
			appointmentService = appointmentServicee;

            tourRequest = request;
			Date = tourRequest.StartDate;
			Start = tourRequest.StartDate;
			End = tourRequest.EndDate;

			Guide = guide;

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


        private RelayCommand acceptCommand;
        public ICommand AcceptCommand
        {
            get
            {
                if (acceptCommand == null)
                {
                    acceptCommand = new RelayCommand(param => this.Accept(), param => this.CanAccept());
                }
                return acceptCommand;
            }
        }
        private bool CanAccept()
        {
            string pattern = @"^(?:[01]\d|2[0-3]):[0-5]\d$";
            return Regex.IsMatch(Time, pattern) && (Time != null) && (Date.Date >= DateTime.Today);

        }
        private void Accept()
        {
			DateTime appointment = BuildDate(Date, Time);

			if (IsGuideFree(appointment))
			{
				AddSharedViewModel sharedViewModel = new AddSharedViewModel();
				sharedViewModel.Country = tourRequest.Location.Country;
				sharedViewModel.City = tourRequest.Location.City;
				sharedViewModel.Language = tourRequest.Language;
				sharedViewModel.GuestNumber = tourRequest.GuestNumber;
				sharedViewModel.Appointment = appointment;

                AddNewTour addNewTour = new AddNewTour(sharedViewModel, Guide, tourRequest.Id, tourService, appointmentService);
                addNewTour.Show();
                Close();
            }
			else
			{
				MessageBox.Show("You already have an organized tour in that period. Try another one");
			}

			
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
