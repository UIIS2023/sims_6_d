using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ISuperGuideRepository: ISubject
    {
        public int Add(SuperGuide superGuide);
        public void RemoveByGuideAndLanguage(int guideId, string lnaguage);
        public SuperGuide GetById(int id);
        public SuperGuide GetByGuideAndLanguage(int guideId, string language);
        public List<SuperGuide> GetAll();
        public void ClearAll();
    }
}
