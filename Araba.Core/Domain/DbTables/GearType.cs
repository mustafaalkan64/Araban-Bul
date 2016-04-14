using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Araba.Core.Domain.DbTables
{
    public class GearType : BaseEntity
    {
        public GearType()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
