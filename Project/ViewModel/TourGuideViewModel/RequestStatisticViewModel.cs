using LiveCharts;
using LiveCharts.Wpf;
using Project.Command;
using Project.Model;
using Project.Service;
using Project.View.TourGuideView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class RequestStatisticViewModel: ViewModelBase
    {

        private TourRequestFilter filter = new TourRequestFilter();

        private List<TourRequest> _tourRequests = new List<TourRequest>();
        public List<TourRequest> TourRequests
        {
            get
            {
                return _tourRequests;
            }
            set
            {
                _tourRequests = value;
                OnPropertyChanged(nameof(TourRequests));
            }
        }

        private List<TourRequest> _appropriateRequests = new List<TourRequest>();
        public List<TourRequest> AppropriateRequests
        {
            get
            {
                return _appropriateRequests;
            }
            set
            {
                _appropriateRequests = value;
                OnPropertyChanged(nameof(AppropriateRequests));
            }
        }

        private int _numberOfRequests;
        public int NumberOfRequests
        {
            get
            {
                return _numberOfRequests;
            }
            set
            {
                _numberOfRequests = value;
                OnPropertyChanged(nameof(NumberOfRequests));
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
                AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language, Year);
                UpdateChartData();
                
            }
        }

        private string[] _countries;
        public string[] Countries
        {
            get
            {
                return _countries;
            }
            set
            {
                _countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }

        private string[] _cities;
        public string[] Cities
        {
            get
            {
                return _cities;
            }
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

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
                AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language, Year);
                if(City != string.Empty)
                {
                    City = string.Empty;
                }
                Cities = LoadCities();
                UpdateChartData();
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
                AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language, Year);
                UpdateChartData();
            }
        }

        private List<string> _languages;
        public List<string> Languages
        {
            get
            {
                return _languages;
            }
            set
            {
                _languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }

        private string _year = string.Empty;
        public string Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
                ChangeHeader();
                AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language, Year);
                UpdateChartData();
            }
        }

        private List<string> _years = new List<string>();
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


        private string _dataGridHeader = string.Empty;
        public string DataGridHeader
        {
            get
            {
                return _dataGridHeader;
            }
            set
            {
                _dataGridHeader = value;
                OnPropertyChanged(nameof(DataGridHeader));
            }
        }


        private ObservableCollection<RequestChartData> _chartData = new ObservableCollection<RequestChartData>();
        public ObservableCollection<RequestChartData> ChartData
        {
            get
            {
                return _chartData;
            }
            set
            {
                _chartData = value;
                OnPropertyChanged(nameof(ChartData));
            }
        }

        private List<RequestChartData> allChartData;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        private readonly TourRequestService _tourRequestService;
        private readonly LocationService locationService;

        public RequestStatisticViewModel()
        {
            _tourRequestService = new TourRequestService();
            locationService = new LocationService();
            TourRequests = _tourRequestService.GetAllRegular();
            AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language,Year);

            Years = GetYears();

            allChartData = GetData();
            ChartData = new ObservableCollection<RequestChartData>(GetData());

            DataGridHeader = "Year";
            Languages = LoadLanguages();
            Countries = locationService.GetAllCountries();
            Cities = LoadCities();
            

        }


        public List<RequestChartData> GetData()
        {
            List<RequestChartData> data = new List<RequestChartData>();
            List<TourRequest> requests = AppropriateRequests;
            Dictionary<int, int> dict = new Dictionary<int, int>();

            int value;

            foreach(TourRequest request in requests)
            {
                int id;
                if(Year == string.Empty)
                {
                    id = request.CreatingDate.Year;
                }
                else
                {
                    id = request.CreatingDate.Month;
                }

                if(dict.TryGetValue(id, out value))
                {
                    dict[id] = value + 1;
                }
                else
                {
                    dict[id] = 1;
                }
            }

            SortedDictionary<int, int> sortedDictionary = new SortedDictionary<int, int>(dict);

            foreach (KeyValuePair<int, int> entry in sortedDictionary)
            {
                string monthText = string.Empty;
                monthText = entry.Key.ToString();
                if(Year != string.Empty)
                {
                    string monthNumber = entry.Key.ToString();
                    monthText = ConvertToText(monthNumber);
                }
                RequestChartData chartData = new RequestChartData(monthText,entry.Value);
                data.Add(chartData);
            }

            return data;
        }

        private string ConvertToText(string num)
        {
            string txt = string.Empty;
            string[] months = {"January", "February", "March", "April" ,"May", "June", "July", "August", "September", "October", "November", "December"};

            string month = months[int.Parse(num) - 1];

            return month;
        }

        private void ChangeHeader()
        {
            if(Year == string.Empty)
            {
                DataGridHeader = "Year";
            }
            else
            {
                DataGridHeader = "Month";
            }
        }

        private List<string> LoadLanguages()
        {
            List<string> languages = new List<string>();
            StreamReader languageSource = new StreamReader(@"../../../Resources/Data/languages.csv");
            string content = languageSource.ReadToEnd();
            string[] language = content.Split('|');
            foreach (string element in language)
            {
                languages.Add(element);
            }

            return languages;

        }

        private string[] LoadCities()
        {
            if (Country == string.Empty)
            {
                return locationService.GetAllCities();
            }
            else
            {
                return locationService.GetAppropriateCities(Country);
            }
        }

        private RelayCommand clearFilterCommand;
        public ICommand ClearFilterCommand
        {
            get
            {
                if (clearFilterCommand == null)
                {
                    clearFilterCommand = new RelayCommand(param => this.ClearFilter(), param => this.CanClearFilter());
                }
                return clearFilterCommand;
            }
        }

        private bool CanClearFilter()
        {
            return true;
        }

        private void ClearFilter()
        {
            Country = string.Empty;
            City = string.Empty;
            Language = string.Empty;
            Year = String.Empty;
            ChartData.Clear();
            foreach (var el in allChartData)
            {
                ChartData.Add(el);
            }

        }

        private void UpdateChartData()
        {
            ChartData.Clear();
            foreach(var el in GetData())
            {
                ChartData.Add(el);
            }
        }

        private List<string> GetYears()
        {
            List<string> years = new List<string>();

            for(int i=2024; i>2000; i--)
            {
                years.Add(i.ToString());
            }
            return years;
        }




    }
}
