using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel.TourGuideViewModel
{
    public class AddSharedViewModel:ViewModelBase
    {

		private string _country = string.Empty;

		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				_country = value;
				OnPropertyChanged(nameof(Country));
			}
		}

		private string _city = string.Empty;
		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
				OnPropertyChanged(nameof(City));
			}
		}

		private string _language = string.Empty;
		public string Language
		{
			get
			{
				return _language;
			}
			set
			{
				_language = value;
				OnPropertyChanged(nameof(Language));
			}
		}

		private int _guestNumber;
		public int GuestNumber
		{
			get
			{
				return _guestNumber;
			}
			set
			{
				_guestNumber = value;
				OnPropertyChanged(nameof(GuestNumber));
			}
		}

		private DateTime _appointment;
		public DateTime Appointment
		{
			get
			{
				return _appointment;
			}
			set
			{
				_appointment = value;
				OnPropertyChanged(nameof(Appointment));
			}
		}
	}
}
