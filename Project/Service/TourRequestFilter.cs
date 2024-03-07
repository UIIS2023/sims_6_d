using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourRequestFilter
    {
        private string Country = string.Empty;
        private string City = string.Empty;
        private string Language = string.Empty;
        private int GuestNumber = 0;
        private DateTime StartDate = DateTime.MinValue;
        private DateTime EndDate = DateTime.MinValue;
        private string Year = string.Empty;

        private List<TourRequest> tourRequests = new List<TourRequest>();


        public TourRequestFilter()
        {

        }

        public List<TourRequest> Filtering(List<TourRequest> sendedList, string country, string city, string language, int guestNumber,
            DateTime startDate, DateTime endDate)
        {
            tourRequests = new List<TourRequest>(sendedList);
            Country = country;
            City = city;
            Language = language;
            GuestNumber = guestNumber;
            StartDate = startDate;
            EndDate = endDate;
            List<TourRequest> requests = new List<TourRequest>(tourRequests);

            if(Country != string.Empty)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if(element.Location.Country != Country)
                    {
                        requests.Remove(element);
                    }
                }
            }

            if(City != string.Empty)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.Location.City != City)
                    {
                        requests.Remove(element);
                    }
                }
            }

            if(Language != string.Empty)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.Language != Language)
                    {
                        requests.Remove(element);
                    }
                }
            }

            if(GuestNumber != 0)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.GuestNumber != GuestNumber)
                    {
                        requests.Remove(element);
                    }
                }
            }

            if(StartDate != DateTime.MinValue)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.StartDate.Date < StartDate.Date)
                    {
                        requests.Remove(element);
                    }
                }
            }

            if (EndDate != DateTime.MinValue)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.EndDate.Date > EndDate.Date)
                    {
                        requests.Remove(element);
                    }
                }
            }

            return requests;
        }

        public List<TourRequest> StatisticFiltering(List<TourRequest> sendedList, string country, string city, string language,string year)
        {
            tourRequests = new List<TourRequest>(sendedList);
            Country = country;
            City = city;
            Language = language;
            Year = year;
            List<TourRequest> requests = new List<TourRequest>(tourRequests);

            if (Country != string.Empty)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.Location.Country != Country)
                    {
                        requests.Remove(element);
                    }
                }
            }

            if (City != string.Empty)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.Location.City != City)
                    {
                        requests.Remove(element);
                    }
                }
            }

            if (Language != string.Empty)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.Language != Language)
                    {
                        requests.Remove(element);
                    }
                }
            }

            if (Year != string.Empty)
            {
                List<TourRequest> tempList = new List<TourRequest>(requests);
                foreach (TourRequest element in tempList)
                {
                    if (element.CreatingDate.Year.ToString() != Year)
                    {
                        requests.Remove(element);
                    }
                }
            }

            return requests;
        }

    }
}
