using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class Attribute : BaseEntity
    {
        public Attribute()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }
        public int AttributeTypeId { get; set; }

        public virtual AttributeType AttributeType { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
