using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class Color : BaseEntity
    {
        public Color()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
