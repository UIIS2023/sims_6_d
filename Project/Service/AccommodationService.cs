using Project.Model;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AccommodationService
    {
        private IAccommodationRepository _accommodationRepository;
        private IAccommodationImageRepository _imageRepository;
        private IUserRepository _userRepository;

        public AccommodationService() 
        {
            _accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
            _imageRepository = Injector.Injector.CreateInstance<IAccommodationImageRepository>();
            _userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            LinkAccommodationsAndImages();
            LinkAccommodationsAndOwners();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return _accommodationRepository.GetAllAccommodations();
        }

        private void LinkAccommodationsAndImages()
        {
            foreach (var image in _imageRepository.GetAllImages())
            {
                Accommodation accommodation = _accommodationRepository.GetAllAccommodations().Find(a => a.Id == image.AccommodationId);
                if (accommodation == null)
                {
                    continue;
                }
                if (accommodation.Images.Exists(i => i.Id == image.Id))
                {
                    continue;
                }

                accommodation.Images.Add(image);
            }
        }

        private void LinkAccommodationsAndOwners()
        {
            foreach (var accommodation in _accommodationRepository.GetAllAccommodations())
            {
                var owner = _userRepository.GetById(accommodation.OwnerId);
                if (owner != null)
                {
                    accommodation.Owner = owner;
                }
            }
        }


        public Accommodation GetAccommodationById(int id)
        {
            return _accommodationRepository.GetAccommodationById(id);
        }

        public List<string> GetAllAccommodationCountries()
        {
            List<string> countries = new();
            foreach (var accommodation in GetAllAccommodations())
            {
                string country = accommodation.Location.Country;
                if (!countries.Contains(country))
                {
                    countries.Add(country);
                }
            }

            return countries;
        }

        public List<string> GetAllAccommodationCitiesByCountry(string country)
        {
            List<string> cities = new();
            foreach (var accommodation in GetAllAccommodations())
            {
                if (accommodation.Location.Country == country)
                {
                    if (!cities.Exists(c => c == accommodation.Location.City))
                    {
                        cities.Add(accommodation.Location.City);
                    }
                }
            }

            return cities;
        }

        public List<Accommodation> GetAllOwnerAccommodations(int ownerId)
        {
            List<Accommodation> accommodations = new List<Accommodation>();

            foreach (Accommodation accommodation in _accommodationRepository.GetAllAccommodations())
            {
                if (accommodation.OwnerId == ownerId)
                {
                    accommodations.Add(accommodation);
                }
            }

            return accommodations;
        }


        public void Add(Accommodation accommodation)
        {
            _accommodationRepository.Add(accommodation);
        }

        public void AddImage(AccommodationImage image)
        {
            _imageRepository.Add(image);
        }


    }
}
