﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Serializer;

namespace Project.Model
{
    public class AccommodationReservation : ISerializable
    {

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestId { get; set; }
        public User Guest { get; set; }

        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }

        public int Guests { get; set; }
        public Guest1Review GuestReview { get; set; }
        public OwnerReview OwnerReview { get; set; }
        public bool UsedPoints { get; set; }

        public AccommodationReservation() { }

        public AccommodationReservation(int id, DateTime start, DateTime end, int guestId, int accId, int guests, bool usedPoints = false)
        {
            Id = id;
            StartDate = start;
            EndDate = end;
            GuestId = guestId;
            AccommodationId = accId;
            Guests = guests;
            UsedPoints = usedPoints;
        }

        public AccommodationReservation(AccommodationReservation accommodationReservation)
        {
            Id = accommodationReservation.Id;
            StartDate = accommodationReservation.StartDate;
            EndDate = accommodationReservation.EndDate;
            GuestId = accommodationReservation.GuestId;
            AccommodationId = accommodationReservation.AccommodationId;
            Guest = accommodationReservation.Guest;
            Accommodation = accommodationReservation.Accommodation;
            Guests = accommodationReservation.Guests;
            UsedPoints = accommodationReservation.UsedPoints;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), StartDate.ToString(), EndDate.ToString(),
                                GuestId.ToString(), AccommodationId.ToString(), Guests.ToString(), UsedPoints.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = DateTime.Parse(values[1]);
            EndDate = DateTime.Parse(values[2]);
            GuestId = int.Parse(values[3]);
            AccommodationId = int.Parse(values[4]);
            Guests = int.Parse(values[5]);
            UsedPoints = bool.Parse(values[6]);
        }




    }
}
