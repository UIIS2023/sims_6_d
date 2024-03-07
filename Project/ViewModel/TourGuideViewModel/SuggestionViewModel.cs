using Project.Command;
using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class SuggestionViewModel: CloseableViewModel
    {
        public AddSharedViewModel SharedViewModel { get; set; }

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


        private readonly TourRequestService _tourRequestService;
        public SuggestionViewModel(AddSharedViewModel sharedViewModel)
        {
            SharedViewModel = sharedViewModel;
            _tourRequestService = new TourRequestService();

            Country = GetMostPopularLocation().Country;
            City = GetMostPopularLocation().City;
            Language = GetMostPopularLanguage();
        }

        public Location GetMostPopularLocation()
        {
            Location location = new Location();

            Dictionary<Location, int> counter = new Dictionary<Location, int>();
            List<TourRequest> allRequests = _tourRequestService.GetAllRegular();
            List<TourRequest> requests = new List<TourRequest>();

            foreach (TourRequest req in allRequests)
            {
                if (req.CreatingDate > DateTime.Today.AddYears(-1))
                {
                    requests.Add(req);
                }
            }

            int value;

            foreach (TourRequest request in requests)
            {
                Location id = request.Location;
                
                if (counter.TryGetValue(id, out value))
                {
                    counter[id] = value + 1;
                }
                else
                {
                    counter[id] = 1;
                }
            }

            location = counter.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;


            return location;
        }

        public string GetMostPopularLanguage()
        {
            string language = string.Empty;

            Dictionary<string, int> counter = new Dictionary<string, int>();
            List<TourRequest> allRequests = _tourRequestService.GetAllRegular();
            List<TourRequest> requests = new List<TourRequest>();

            foreach(TourRequest req in allRequests)
            {
                if(req.CreatingDate > DateTime.Today.AddYears(-1))
                {
                    requests.Add(req);
                }
            }

            int value;

            foreach (TourRequest request in requests)
            {
                string id = request.Language;

                if (counter.TryGetValue(id, out value))
                {
                    counter[id] = value + 1;
                }
                else
                {
                    counter[id] = 1;
                }
            }

            language = counter.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            return language;
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

        private RelayCommand suggestLocationCommand;
        public ICommand SuggestLocationCommand
        {
            get
            {
                if (suggestLocationCommand == null)
                {
                    suggestLocationCommand = new RelayCommand(param => this.SuggestLocation(), param => this.CanSuggestLocation());
                }
                return suggestLocationCommand;
            }
        }

        private bool CanSuggestLocation()
        {
            return true;
        }

        private void SuggestLocation()
        {
            SharedViewModel.City = GetMostPopularLocation().City;
            SharedViewModel.Country = GetMostPopularLocation().Country;
            Close();
        }

        private RelayCommand suggestLanguageCommand;
        public ICommand SuggestLanguageCommand
        {
            get
            {
                if (suggestLanguageCommand == null)
                {
                    suggestLanguageCommand = new RelayCommand(param => this.SuggestLanguage(), param => this.CanSuggestLanguage());
                }
                return suggestLanguageCommand;
            }
        }

        private bool CanSuggestLanguage()
        {
            return true;
        }

        private void SuggestLanguage()
        {
            SharedViewModel.Language = GetMostPopularLanguage();
            Close();
        }
    }
}
