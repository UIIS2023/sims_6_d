using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ICouponRepository : ISubject
    {
        public int Add(Coupon coupon);
        public void ChangeToUsed(int id);
        public List<Coupon> GetAll();
        public void Remove(int id);
        public Coupon GetById(int id);

        public void ChangeCouponUsabilityToAnywhere(int id);
    }
}
