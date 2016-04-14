using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class District : BaseEntity
    {
        public District()
        {
            Cars = new HashSet<Car>();
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
