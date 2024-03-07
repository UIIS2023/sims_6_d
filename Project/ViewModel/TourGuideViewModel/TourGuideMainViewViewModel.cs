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
using System.Windows;
using System.Windows.Input;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Diagnostics.Metrics;
using ScottPlot.Drawing.Colormaps;

namespace Project.ViewModel.TourGuideViewModel
{
    public class TourGuideMainViewViewModel: ViewModelBase, IObserver
    {

        private readonly TourService _tourService;
        private readonly AppointmentService _appointmentService;
        private readonly SuperGuideService superGuideService;
        private TourRequestFilter filter;
        private readonly TourRequestService _tourRequestService;

        //public Tour SelectedTour { get; set; }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                if(SelectedTour != null)
                {
                    IsCancelEnabled = true;
                }
            }
        }

        private User _currentUser = new User();
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private string _username = string.Empty;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


        private string _imagesource = string.Empty;
        public string ImageSource
        {
            get => _imagesource;
            set
            {
                if (value != _imagesource)
                {
                    _imagesource = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _status = string.Empty;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private bool _isCancelEnabled = false;
        public bool IsCancelEnabled
        {
            get
            {
                return _isCancelEnabled;
            }
            set
            {
                _isCancelEnabled = value;
                OnPropertyChanged(nameof(IsCancelEnabled));
            }
        }


        public AddSharedViewModel SharedViewModel { get; set; } = new AddSharedViewModel();

        //public ObservableCollection<Tour> Tours { get; set; }

        private ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
        public ObservableCollection<Tour> Tours
        {
            get
            {
                return _tours;
            }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

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

        private string _comboBoxTour;
        public string ComboBoxTour
        {
            get
            {
                return _comboBoxTour;
            }
            set
            {
                _comboBoxTour = value;
                OnPropertyChanged(nameof(ComboBoxTour));
                UpdateTours();
            }
        }

        private List<string> _comboBoxValues;
        public List<string> ComboBoxValues
        {
            get
            {
                return _comboBoxValues;
            }
            set
            {
                _comboBoxValues = value;
                OnPropertyChanged(nameof(ComboBoxValues));
            }
        }


        public TourGuideMainViewViewModel(User user)
        {

            superGuideService = new SuperGuideService();
            _appointmentService = new AppointmentService();
            _appointmentService.Subscribe(this);
            _tourRequestService = new TourRequestService();

            _tourService = new TourService();
            _tourService.Subscribe(this);

            filter = new TourRequestFilter();
            

            CurrentUser = user;
            Username = CurrentUser.Username;
            IsCancelEnabled = false;

            
            ComboBoxValues = new List<string>
            {
                "Not started",
                "Completed",
                "Canceled",
            };

            ComboBoxTour = ComboBoxValues[0];




            ImageSource = "../../Resources/Data/images.csv";
            SetStatus();


            //Tours = new ObservableCollection<Tour>(_tourService.GetAllTourAppointments(CurrentUser.Id));

            LoadTours(CurrentUser.Id);

        }

        public void SetStatus()
        {
            if (superGuideService.IsSuper(CurrentUser.Id))
            {
                Status = "Super";
            }
            else
            {
                Status = "Regular";
            }
        }

        public void LoadTours(int id)
        {
            if (ComboBoxTour == "Canceled")
            {
                Tours = new ObservableCollection<Tour>(_tourService.GetCanceled(id));
            }
            else if (ComboBoxTour == "Completed")
            {
                Tours = new ObservableCollection<Tour>(_tourService.GetCompleted(id));
            }
            else
            {
                Tours = new ObservableCollection<Tour>(_tourService.GetNotStarted(id));
            }
            

        }

        public void Update()
        {
            UpdateTours();
        }

        public void UpdateTours()
        {
            Tours.Clear();

            if (ComboBoxTour == "Not started")
            {
                foreach (var tour in _tourService.GetNotStarted(CurrentUser.Id))
                {
                    Tours.Add(tour);
                }
            }
            else if (ComboBoxTour == "Completed")
            {
                foreach (var tour in _tourService.GetCompleted(CurrentUser.Id))
                {
                    Tours.Add(tour);
                }
            }
            else
            {
                foreach (var tour in _tourService.GetCanceled(CurrentUser.Id))
                {
                    Tours.Add(tour);
                }
            }

        }

        /*   -- Commands --     */

        private RelayCommand addTourCommand;
        public ICommand AddTourCommand
        {
            get
            {
                if (addTourCommand == null)
                {
                    addTourCommand = new RelayCommand(param => this.AddTour(), param => this.CanAddTour());
                }
                return addTourCommand;
            }
        }

        private RelayCommand dataGridDoubleClickCommand;
        public ICommand DataGridDoubleClickCommand
        {
            get
            {
                if (dataGridDoubleClickCommand == null)
                {
                    dataGridDoubleClickCommand = new RelayCommand(param => this.DoubleClick(), param => this.CanDoubleClick());
                }
                return dataGridDoubleClickCommand;
            }
        }

        private RelayCommand cancelTourCommand;
        public ICommand CancelTourCommand
        {
            get
            {
                if (cancelTourCommand == null)
                {
                    cancelTourCommand = new RelayCommand(param => this.CancelTour(), param => this.CanCancelTour());
                }
                return cancelTourCommand;
            }
        }

        private RelayCommand statisticClickCommand;
        public ICommand StatisticClickCommand
        {
            get
            {
                if (statisticClickCommand == null)
                {
                    statisticClickCommand = new RelayCommand(param => this.StatisticClick(), param => this.CanStatisticClick());
                }
                return statisticClickCommand;
            }
        }

        private RelayCommand reviewsClickCommand;
        public ICommand ReviewsClickCommand
        {
            get
            {
                if (reviewsClickCommand == null)
                {
                    reviewsClickCommand = new RelayCommand(param => this.ReviewClick(), param => this.CanReviewClick());
                }
                return reviewsClickCommand;
            }
        }

        private RelayCommand requestsClickCommand;
        public ICommand RequestsClickCommand
        {
            get
            {
                if (requestsClickCommand == null)
                {
                    requestsClickCommand = new RelayCommand(param => this.RequestsClick(), param => this.CanRequestsClick());
                }
                return requestsClickCommand;
            }
        }

        private RelayCommand settingsClickCommand;
        public ICommand SettingsClickCommand
        {
            get
            {
                if (settingsClickCommand == null)
                {
                    settingsClickCommand = new RelayCommand(param => this.SettingsClick(), param => this.CanSettingsClick());
                }
                return settingsClickCommand;
            }
        }

        private RelayCommand generatePdfCommand;
        public ICommand GeneratePdfCommand
        {
            get
            {
                if (generatePdfCommand == null)
                {
                    generatePdfCommand = new RelayCommand(param => this.GeneratePDF(), param => this.CanGeneratePDF());
                }
                return generatePdfCommand;
            }
        }

        private RelayCommand startTutorialCommand;
        public ICommand StartTutorialCommand
        {
            get
            {
                if (startTutorialCommand == null)
                {
                    startTutorialCommand = new RelayCommand(param => this.StartTutorial(), param => this.CanStartTutorial());
                }
                return startTutorialCommand;
            }
        }

        private bool CanGeneratePDF()
        {
            return true;
        }
        private bool CanAddTour()
        {
            return true;
        }
        private bool CanDoubleClick()
        {
            return SelectedTour != null;
        }
        private bool CanCancelTour()
        {
            return SelectedTour != null;
        }

        private bool CanStatisticClick()
        {
            return true;
        }
        private bool CanReviewClick()
        {
            return true;
        }
        private bool CanRequestsClick()
        {
            return true;
        }
        private bool CanSettingsClick()
        {
            return true;
        }

        private bool CanStartTutorial()
        {
            return true;
        }




        private void AddTour()
        {
            SharedViewModel = new AddSharedViewModel();
            AddNewTour addNewTour = new AddNewTour(_tourService, _appointmentService, CurrentUser, SharedViewModel);
            addNewTour.Show();
        }

        private void DoubleClick()
        {
                SingleTourOverview singleTour = new SingleTourOverview(SelectedTour);
                singleTour.Show();
        }

        private void CancelTour()
        {
            var timespan = SelectedTour.TourAppointment.DateAndTimeOfAppointment - DateTime.Now;

            if (timespan.TotalHours < 48)
            {
                MessageBox.Show("You cannot cancel this tour.\nThe tour can be canceled no later than 48 hours before the scheduled start.");
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to cancel the tour?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //no
                }
                else
                {
                    //yes
                    _appointmentService.Cancel(SelectedTour.TourAppointment, CurrentUser, SelectedTour.Name);
                }

            }
        }


        private void StatisticClick()
        {
            Statistic statistic = new Statistic(CurrentUser);
            statistic.Show();
        }

        private void ReviewClick()
        {
            Reviews reviews = new Reviews(CurrentUser.Id);
            reviews.Show();
        }

        private void RequestsClick()
        {
            TourRequests tourRequests = new TourRequests(CurrentUser);
            tourRequests.Show();
        }

        private void SettingsClick()
        {
            Settings settings = new Settings(CurrentUser);
            settings.Show();
        }

        private void StartTutorial()
        {
            Tutorial tutorial = new Tutorial();
            tutorial.Show();
        }

        private void GeneratePDF()
        {
            string content = "report_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
            string path = $"C:\\Users\\lukar\\Documents\\git\\Sims\\SIMS_GroupD\\Project\\Project\\Resources\\PDFReports\\{content}.pdf";

            Document document = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);
            Label label = new Label("Request statistic report", 0, 0, 504, 100, Font.TimesRoman, 23, TextAlign.Center);
            page.Elements.Add(label);

            Line line = new Line(0,30,504,30);
            page.Elements.Add(line);


            Label dateLabel = new Label($"Date: {DateTime.Now.ToString("dd/MM/yy H:mm:ss")} ", 0, 0, 504, 40, Font.TimesRoman, 13);
            Label usernameLabel = new Label($"Guide username: {CurrentUser.Username} ", 0, 0, 504, 40, Font.TimesRoman, 13);
            Label statusLabel = new Label($"Guide status: {Status} ", 0, 0, 504, 40, Font.TimesRoman, 13);
            dateLabel.Y = 60;
            usernameLabel.Y = 80;
            statusLabel.Y = 100;

            page.Elements.Add(dateLabel);
            page.Elements.Add(usernameLabel);
            page.Elements.Add(statusLabel);




            Table2 table = new Table2(0, 0, 600, 600);

            table.BackgroundColor = RgbColor.White;

            table.Columns.Add(90);
            table.Columns.Add(150);
            Row2 row = table.Rows.Add(20);
            row.Cells.Add("Year");
            row.Cells.Add("Number of requests");

            row.Cells[0].BackgroundColor = RgbColor.LightSlateGray;
            row.Cells[1].BackgroundColor = RgbColor.LightSlateGray;
            row.Cells[0].Font = Font.TimesBold;
            row.Cells[1].Font = Font.TimesBold;

            int i = 0;

            foreach (RequestChartData rqcd in GetData(_tourRequestService.GetAllRegular(),""))
            {
                Row2 tempRow = table.Rows.Add(20);
                tempRow.Cells.Add(rqcd.Date);

                tempRow.Cells.Add(rqcd.NumberOfRequests.ToString());

                i++;
            }

            table.Y = 170;

            Label tableLabel = new Label("All requests ever", 0, 0, 300, 50, Font.Helvetica, 13);
            tableLabel.Y = 150;

            page.Elements.Add(table);
            page.Elements.Add(tableLabel);

            Table2 tableMonth = new Table2(0, 0, 600, 600);

            tableMonth.BackgroundColor = RgbColor.White;

            tableMonth.Columns.Add(90);
            tableMonth.Columns.Add(150);
            Row2 row1 = tableMonth.Rows.Add(20);
            row1.Cells.Add("Month");
            row1.Cells.Add("Number of requests");

            row1.Cells[0].BackgroundColor = RgbColor.LightSlateGray;
            row1.Cells[1].BackgroundColor = RgbColor.LightSlateGray;
            row1.Cells[0].Font = Font.TimesBold;
            row1.Cells[1].Font = Font.TimesBold;



            foreach (RequestChartData rqcd in GetData(_tourRequestService.GetAllRegular(), DateTime.Today.Year.ToString()))
            {
                Row2 tempRow = tableMonth.Rows.Add(20);
                tempRow.Cells.Add(rqcd.Date);

                tempRow.Cells.Add(rqcd.NumberOfRequests.ToString());

            }

            tableMonth.Y = 170;
            tableMonth.X = 290;

            Label tableMonthLabel = new Label("This year requests", 0, 0, 300, 50, Font.Helvetica, 13);
            tableMonthLabel.Y = 150;
            tableMonthLabel.X = 290;

            page.Elements.Add(tableMonth);
            page.Elements.Add(tableMonthLabel);





            Page page2 = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page2);

            Table2 accepted = new Table2(0, 0, 600, 600);

            accepted.BackgroundColor = RgbColor.White;

            accepted.Columns.Add(90);
            accepted.Columns.Add(90);
            accepted.Columns.Add(90);
            accepted.Columns.Add(90);
            accepted.Columns.Add(120);
            Row2 acceptedRow = accepted.Rows.Add(20);
            acceptedRow.Cells.Add("Country");
            acceptedRow.Cells.Add("City");
            acceptedRow.Cells.Add("Language");
            acceptedRow.Cells.Add("Guest number");
            acceptedRow.Cells.Add("Date of appointment");


            acceptedRow.Cells[0].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRow.Cells[1].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRow.Cells[2].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRow.Cells[3].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRow.Cells[4].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRow.Cells[0].Font = Font.TimesBold;
            acceptedRow.Cells[1].Font = Font.TimesBold;
            acceptedRow.Cells[2].Font = Font.TimesBold;
            acceptedRow.Cells[3].Font = Font.TimesBold;
            acceptedRow.Cells[4].Font = Font.TimesBold;



            foreach (TourRequest tourRequest in _tourRequestService.GetAllGuideAccepted(CurrentUser.Id))
            {
                Row2 tempRow = accepted.Rows.Add(20);
                tempRow.Cells.Add(tourRequest.Location.Country);
                tempRow.Cells.Add(tourRequest.Location.City);
                tempRow.Cells.Add(tourRequest.Language);
                tempRow.Cells.Add(tourRequest.GuestNumber.ToString());
                tempRow.Cells.Add(tourRequest.AcceptedAppointment.ToString());
            }

            accepted.Y = 20;
            accepted.X = 0;

            Label acceptedLabel = new Label("All accepted requests ever", 0, 0, 300, 50, Font.Helvetica, 13);
            acceptedLabel.Y = 0;
            acceptedLabel.X = 0;

            page2.Elements.Add(accepted);
            page2.Elements.Add(acceptedLabel);


            Table2 acceptedRecent = new Table2(0, 0, 600, 600);

            acceptedRecent.BackgroundColor = RgbColor.White;

            acceptedRecent.Columns.Add(90);
            acceptedRecent.Columns.Add(90);
            acceptedRecent.Columns.Add(90);
            acceptedRecent.Columns.Add(90);
            acceptedRecent.Columns.Add(120);
            Row2 acceptedRecentRow = acceptedRecent.Rows.Add(20);
            acceptedRecentRow.Cells.Add("Country");
            acceptedRecentRow.Cells.Add("City");
            acceptedRecentRow.Cells.Add("Language");
            acceptedRecentRow.Cells.Add("Guest number");
            acceptedRecentRow.Cells.Add("Date of appointment");


            acceptedRecentRow.Cells[0].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRecentRow.Cells[1].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRecentRow.Cells[2].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRecentRow.Cells[3].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRecentRow.Cells[4].BackgroundColor = RgbColor.LightSlateGray;
            acceptedRecentRow.Cells[0].Font = Font.TimesBold;
            acceptedRecentRow.Cells[1].Font = Font.TimesBold;
            acceptedRecentRow.Cells[2].Font = Font.TimesBold;
            acceptedRecentRow.Cells[3].Font = Font.TimesBold;
            acceptedRecentRow.Cells[4].Font = Font.TimesBold;



            foreach (TourRequest tourRequest in _tourRequestService.GetAllGuideAcceptedThisYear(CurrentUser.Id))
            {
                Row2 tempRow = acceptedRecent.Rows.Add(20);
                tempRow.Cells.Add(tourRequest.Location.Country);
                tempRow.Cells.Add(tourRequest.Location.City);
                tempRow.Cells.Add(tourRequest.Language);
                tempRow.Cells.Add(tourRequest.GuestNumber.ToString());
                tempRow.Cells.Add(tourRequest.AcceptedAppointment.ToString());
            }

            acceptedRecent.Y = 360;
            acceptedRecent.X = 0;

            Label acceptedRecentLabel = new Label("Requests accepted this year", 0, 0, 300, 50, Font.Helvetica, 13);
            acceptedRecentLabel.Y = 340;
            acceptedRecentLabel.X = 0;

            page2.Elements.Add(acceptedRecent);
            page2.Elements.Add(acceptedRecentLabel);



            document.Draw(path);


            MessageBox.Show("PDF report successfully created.");
        }

        public List<RequestChartData> GetData(List<TourRequest> tourRequests, string year)
        {
            string Year = year;
            List<RequestChartData> data = new List<RequestChartData>();
            List<TourRequest> requests = filter.StatisticFiltering(tourRequests, "", "", "", Year);
            Dictionary<int, int> dict = new Dictionary<int, int>();
            


            int value;

            foreach (TourRequest request in requests)
            {
                int id;
                if (Year == string.Empty)
                {
                    id = request.CreatingDate.Year;
                }
                else
                {
                    id = request.CreatingDate.Month;
                }

                if (dict.TryGetValue(id, out value))
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
                if (Year != string.Empty)
                {
                    string monthNumber = entry.Key.ToString();
                    monthText = ConvertToText(monthNumber);
                }
                RequestChartData chartData = new RequestChartData(monthText, entry.Value);
                data.Add(chartData);
            }

            return data;
        }

        private string ConvertToText(string num)
        {
            string txt = string.Empty;
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            string month = months[int.Parse(num) - 1];

            return month;
        }





    }
}
