using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        public Accommodation Add(Accommodation accommodation);

        public Accommodation Update(Accommodation accommodation);

        public Accommodation Remove(int id);

        public Accommodation GetAccommodationById(int id);

        public List<Accommodation> GetAllAccommodations();
    }
}
