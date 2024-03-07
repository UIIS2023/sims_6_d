using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.ViewModel.TourGuideViewModel
{
    public class StatisticViewModel:ViewModelBase
    {
        private string _nameoftour;
        public string NameOfTour
        {
            get => _nameoftour;
            set
            {
                if (value != _nameoftour)
                {
                    _nameoftour = value;
                    OnPropertyChanged(nameof(NameOfTour));

                }
            }
        }

        private int _under18;
        public int Under18
        {
            get => _under18;
            set
            {
                if (value != _under18)
                {
                    _under18 = value;
                    OnPropertyChanged(nameof(Under18));
                }
            }
        }

        private int _over50;
        public int Over50
        {
            get => _over50;
            set
            {
                if (value != _over50)
                {
                    _over50 = value;
                    OnPropertyChanged(nameof(Over50));
                }
            }
        }

        private double _percent18;
        public double Percent18
        {
            get => _percent18;
            set
            {
                if (value != _percent18)
                {
                    _percent18 = value;
                    OnPropertyChanged(nameof(Percent18));
                }
            }
        }

        private double _percent50;
        public double Percent50
        {
            get => _percent50;
            set
            {
                if (value != _percent50)
                {
                    _percent50 = value;
                    OnPropertyChanged(nameof(Percent50));
                }
            }
        }

        private double _percent1850;
        public double Percent1850
        {
            get => _percent1850;
            set
            {
                if (value != _percent1850)
                {
                    _percent1850 = value;
                    OnPropertyChanged(nameof(Percent1850));
                }
            }
        }

        private int _between18and50;
        public int Between18and50
        {
            get => _between18and50;
            set
            {
                if (value != _between18and50)
                {
                    _between18and50 = value;
                    OnPropertyChanged(nameof(Between18and50));
                }
            }
        }

        private DateTime _appointmentdate;
        public DateTime AppointmentDate
        {
            get => _appointmentdate;
            set
            {
                if (value != _appointmentdate)
                {
                    _appointmentdate = value;
                    OnPropertyChanged(nameof(AppointmentDate));
                }
            }
        }

        private double _coupon;
        public double Coupon
        {
            get => _coupon;
            set
            {
                if (value != _coupon)
                {
                    _coupon = value;
                    OnPropertyChanged(nameof(Coupon));
                }
            }
        }

        private string _couponpercent;
        public string CouponPercent
        {
            get => _couponpercent;
            set
            {
                if (value != _couponpercent)
                {
                    _couponpercent = value;
                    OnPropertyChanged(nameof(CouponPercent));
                }
            }
        }

        private string _bestnameoftour;
        public string bestNameOfTour
        {
            get => _bestnameoftour;
            set
            {
                if (value != _bestnameoftour)
                {
                    _bestnameoftour = value;
                    OnPropertyChanged(nameof(bestNameOfTour));
                }
            }
        }

        private DateTime _bestappointmentdate;
        public DateTime bestAppointmentDate
        {
            get => _bestappointmentdate;
            set
            {
                if (value != _bestappointmentdate)
                {
                    _bestappointmentdate = value;
                    OnPropertyChanged(nameof(bestAppointmentDate));
                }
            }
        }

        private int _bestunder18;
        public int bestUnder18
        {
            get => _bestunder18;
            set
            {
                if (value != _bestunder18)
                {
                    _bestunder18 = value;
                    OnPropertyChanged(nameof(bestUnder18));
                }
            }
        }

        private double _bestpercent18;
        public double bestPercent18
        {
            get => _bestpercent18;
            set
            {
                if (value != _bestpercent18)
                {
                    _bestpercent18 = value;
                    OnPropertyChanged(nameof(bestPercent18));
                }
            }
        }

        private int _bestover50;
        public int bestOver50
        {
            get => _bestover50;
            set
            {
                if (value != _bestover50)
                {
                    _bestover50 = value;
                    OnPropertyChanged(nameof(bestOver50));
                }
            }
        }

        private double _bestpercent50;
        public double bestPercent50
        {
            get => _bestpercent50;
            set
            {
                if (value != _bestpercent50)
                {
                    _bestpercent50 = value;
                    OnPropertyChanged(nameof(bestPercent50));
                }
            }
        }

        private double _bestpercent1850;
        public double bestPercent1850
        {
            get => _bestpercent1850;
            set
            {
                if (value != _bestpercent1850)
                {
                    _bestpercent1850 = value;
                    OnPropertyChanged(nameof(bestPercent1850));
                }
            }
        }

        private int _bestbetween18and50;
        public int bestBetween18and50
        {
            get => _bestbetween18and50;
            set
            {
                if (value != _bestbetween18and50)
                {
                    _bestbetween18and50 = value;
                    OnPropertyChanged(nameof(bestBetween18and50));
                }
            }
        }




        private double _bestcoupon;
        public double bestCoupon
        {
            get => _bestcoupon;
            set
            {
                if (value != _bestcoupon)
                {
                    _bestcoupon = value;
                    OnPropertyChanged(nameof(bestCoupon));
                }
            }
        }

        private string _bestcouponpercent;
        public string bestCouponPercent
        {
            get => _bestcouponpercent;
            set
            {
                if (value != _bestcouponpercent)
                {
                    _bestcouponpercent = value;
                    OnPropertyChanged(nameof(bestCouponPercent));
                }
            }
        }

        private string _selectedYear;
        public string SelectedYear
        {

            get
            {
                return _selectedYear;
            }
            set
            {
                if(_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    SelectedYearChanged();
                }
                
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                if(_selectedTour != value)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                    SelectedTourChanged();
                }
                
            }
        }



        private List<string> _years;
        public List<string> Years
        {
            get
            {
                return _years;
            }
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        private Visibility _visible;
        public Visibility Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }

        private List<Tour> _completedTours;
        public List<Tour> CompletedTours
        {
            get
            {
                return _completedTours;
            }
            set
            {
                _completedTours = value;
                OnPropertyChanged(nameof(CompletedTours));
            }
        }

        private User user;

        private readonly TourService tourService;
        private readonly PresentGuestsService presentGuestsService;

        
        public bool DateView;
        public StatisticViewModel(User user)
        {
            User = user;

            tourService = new TourService();
            presentGuestsService = new PresentGuestsService();
            CompletedTours = new List<Tour>(tourService.GetCompletedTours(User.Id));

            Years = new List<string>();

            NameOfTour = "";
            AppointmentDate = DateTime.Now;
            Under18 = 0;
            Over50 = 0;
            Between18and50 = 0;
            Percent18 = 0;
            Percent50 = 0;
            Percent1850 = 0;
            Coupon = 0;
            CouponPercent = "0%";
            DateView = false;

            Years.Add("Overall");
            Years.Add("2023");
            Years.Add("2022");
            Years.Add("2021");

            SelectedYear = Years[0];
            Visible = Visibility.Hidden;

        }

        public void SelectedTourChanged()
        {
            RefreshView(SelectedTour, false);

            if (presentGuestsService.GetNumberOfGuests(SelectedTour.TourAppointment.Id) != 0)
            {
                CalculatePercentages(SelectedTour, false);

            }

            CouponPercent = Coupon.ToString() + "%";

            Visible = Visibility.Visible;
        }

        public void SelectedYearChanged()
        {
            Tour tour = GetBestTour();

            RefreshView(tour,true);

            if (presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id) != 0)
            {
                CalculatePercentages(tour, true);
            }

            bestCouponPercent = bestCoupon.ToString() + "%";

        }

        public void RefreshView(Tour tour, bool indicator)
        {
            if (indicator)
            {
                bestNameOfTour = tour.Name;
                bestAppointmentDate = tour.TourAppointment.DateAndTimeOfAppointment;
                bestUnder18 = presentGuestsService.GetUnder18(tour.TourAppointment.Id);
                bestOver50 = presentGuestsService.GetOver50(tour.TourAppointment.Id);
                bestBetween18and50 = presentGuestsService.GetBetween18and50(tour.TourAppointment.Id);

                bestPercent18 = 0;
                bestPercent50 = 0;
                bestPercent1850 = 0;
                bestCoupon = 0;
            }
            else
            {
                NameOfTour = SelectedTour.Name;
                AppointmentDate = SelectedTour.TourAppointment.DateAndTimeOfAppointment;
                Under18 = presentGuestsService.GetUnder18(SelectedTour.TourAppointment.Id);
                Over50 = presentGuestsService.GetOver50(SelectedTour.TourAppointment.Id);
                Between18and50 = presentGuestsService.GetBetween18and50(SelectedTour.TourAppointment.Id);

                Percent18 = 0;
                Percent50 = 0;
                Percent1850 = 0;
                Coupon = 0;
            }
            

        }

        public void CalculatePercentages(Tour tour, bool indicator)
        {
            if (indicator)
            {
                bestPercent18 = presentGuestsService.GetUnder18(tour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                bestPercent1850 = presentGuestsService.GetBetween18and50(tour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                bestPercent50 = presentGuestsService.GetOver50(tour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                bestCoupon = presentGuestsService.GetNumberOfGuestsWithCoupon(tour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
            }
            else
            {
                Percent18 = presentGuestsService.GetUnder18(SelectedTour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(SelectedTour.TourAppointment.Id);
                Percent1850 = presentGuestsService.GetBetween18and50(SelectedTour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(SelectedTour.TourAppointment.Id);
                Percent50 = presentGuestsService.GetOver50(SelectedTour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(SelectedTour.TourAppointment.Id);
                Coupon = presentGuestsService.GetNumberOfGuestsWithCoupon(SelectedTour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(SelectedTour.TourAppointment.Id);
            }

        }


        public Tour GetBestTour()
        {
            List<Tour> completed = tourService.GetCompletedTours(User.Id);
            int highestNumberOfGuests = 0;
            Tour bestTour = new Tour();

            if (SelectedYear.ToString() == "Overall")
            {
                foreach (Tour tour in completed)
                {
                    if (presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id) > highestNumberOfGuests)
                    {
                        highestNumberOfGuests = presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                        bestTour = tour;
                    }
                }
            }
            else
            {
                foreach (Tour tour in completed)
                {
                    if (presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id) > highestNumberOfGuests &&
                        tour.TourAppointment.DateAndTimeOfAppointment.Year.ToString() == SelectedYear.ToString())
                    {
                        highestNumberOfGuests = presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                        bestTour = tour;
                    }
                }
            }
            return bestTour;
        }

    }
}
