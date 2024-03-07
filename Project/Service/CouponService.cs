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
    public class CouponService
    {
        private ICouponRepository couponRepository;
        //CouponRepository couponRepository;
        public Guest2 Guest { get; set; }

        public CouponService()
        {
            //couponRepository = new CouponRepository();
            couponRepository = Injector.Injector.CreateInstance<ICouponRepository>();
            Guest = new Guest2();
            LinkGuest2Coupons();
        }

        public CouponService(User u)
        {
            //couponRepository = new CouponRepository();
            couponRepository = Injector.Injector.CreateInstance<ICouponRepository>();
            Guest = new Guest2(u);
            LinkGuest2Coupons();
        }

        private void LinkGuest2Coupons()
        {
            foreach(var coupon in couponRepository.GetAll())
            {
                if((coupon.GuestId == Guest.User.Id) && (coupon.Status == Coupon.STATUS.NOTUSED))
                {
                    Guest.Coupons.Add(coupon);
                }
            }
        }

        public List<Coupon> GetGuest2Coupons()
        {
            return Guest.Coupons;
        }


        public int Create(int guestId, DateTime dateOfExpire,int guideId)
        {
            Coupon coupon = new Coupon(guestId,dateOfExpire,guideId);

            int couponId = couponRepository.Add(coupon);

            return couponId;

        }

        public Coupon GetById(int id)
        {
            return couponRepository.GetById(id);
        }

        public List<Coupon> GetAll()
        {
            return couponRepository.GetAll();
        }

        public void Remove(int id)
        {
            couponRepository.Remove(id);
        }

        public void ChangeCouponToUsed(int id)
        {
            couponRepository.ChangeToUsed(id);
        }

        public void ChangeCouponUsabilityToAnywhere(int id)
        {
            couponRepository.ChangeCouponUsabilityToAnywhere(id);
        }

        public void GuideQuitJobCorection(int guideId)
        {
            List<Coupon> allCoupones = couponRepository.GetAll();
            foreach(Coupon coupon in allCoupones)
            {
                if(coupon.GuideId == guideId)
                {
                    ChangeCouponUsabilityToAnywhere(coupon.Id);
                }
            }
        }

    }
}
