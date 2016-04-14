using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class UsageStatus : BaseEntity
    {
        public UsageStatus()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
