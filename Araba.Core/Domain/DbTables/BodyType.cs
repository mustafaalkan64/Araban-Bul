using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class BodyType : BaseEntity
    {
        public BodyType()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
