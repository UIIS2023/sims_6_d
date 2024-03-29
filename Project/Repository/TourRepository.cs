﻿using Project.Serializer;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Observer;
using Project.Controller;
using Project.RepositoryInterfaces;

namespace Project.Repository
{
    public class TourRepository: ISubject, ITourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> serializer;
        private readonly List<IObserver> _observers;

        private List<Tour> tours;

        public TourRepository(){
            serializer = new Serializer<Tour>();
            tours = serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, tours);
        }

        private int GenerateId()
        {
            if (tours.Count == 0)
                return 0;

            return tours[tours.Count - 1].Id + 1;
        }

        public int Add(Tour tour)
        {
            tour.Id = GenerateId();
            tours.Add(tour);
            SaveInFile();
            NotifyObservers();
            
            return tour.Id;
  
        }

        public void Remove(int id)
        {
            Tour tour = GetById(id);

            tours.Remove(tour);
            SaveInFile();

        }

        public Tour GetById(int id)
        {
            return tours.Find(v => v.Id == id);
        }

        public List<Tour> GetAll(int guideId)
        {
            List<Tour> tempTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                if(tour.GuideId == guideId)
                {
                    tempTours.Add(tour);
                }
            }
            return tempTours;
        }

        public List<Tour> GetAll()
        {
            return tours;
        }

        public List<Tour> FindAllAlternatives(Tour tour, int numberOfGuests)
        {
            List<Tour> alternative = new List<Tour>();
            List<Tour> tours = GetAll();
            
            foreach(Tour t in tours)
            {
                if (t.City.Equals(tour.City) && t.MaxGuests >= numberOfGuests)
                {
                    alternative.Add(t);
                }
            }
            return alternative;
        }



        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
