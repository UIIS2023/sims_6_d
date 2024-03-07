using Project.Model;
using Project.Observer;
using Project.RepositoryInterfaces;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Repository
{
    public class CouponRepository : ISubject, ICouponRepository
    {
        private const string FilePath = "../../../Resources/Data/coupons.csv";

        private readonly Serializer<Coupon> serializer;

        private readonly List<IObserver> _observers;

        private List<Coupon> coupons;


        public CouponRepository()
        {
            serializer = new Serializer<Coupon>();
            coupons = serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, coupons);
        }

        private int GenerateId()
        {
            if (coupons.Count == 0)
                return 0;

            return coupons[coupons.Count - 1].Id + 1;
        }

        public int Add(Coupon coupon)
        {
            coupon.Id = GenerateId();
            coupons.Add(coupon);
            SaveInFile();
            NotifyObservers();

            return coupon.Id;

        }

        public void ChangeToUsed(int id) {
            coupons.Find(c => c.Id == id).Status = Coupon.STATUS.USED;
            SaveInFile();
            NotifyObservers();
        }

        public List<Coupon> GetAll()
        {
            return coupons;
        }

        public void Remove(int id)
        {
            Coupon coupon = GetById(id);
            coupons.Remove(coupon);
            SaveInFile();
            NotifyObservers();
        }

        public Coupon GetById(int id)
        {
            return coupons.Find(v => v.Id == id);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void ChangeCouponUsabilityToAnywhere(int id)
        {
            coupons.Find(c => c.Id == id).GuideId = -2;
            SaveInFile();
            NotifyObservers();
        }
    }
}
