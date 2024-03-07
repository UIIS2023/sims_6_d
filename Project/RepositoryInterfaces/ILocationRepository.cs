using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ILocationRepository: ISubject
    {
        public Location Add(Location location);

        public Location Update(Location location);

        public Location Remove(int id);

        public Location GetLocationById(int id);

        public string GetCountryById(int id);

        public string GetCityById(int id);

        public string[] GetAllCountries();

        public string[] GetAppropriateCities(string country);

        public string[] GetAllCities();

        public List<Location> GetAllLocations();

        public string GetAppropriateCountry(string city);
    }
}
