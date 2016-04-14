using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Araba.Core.Domain.DbTables
{
    public class PlateType : BaseEntity
    {
        public PlateType()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
