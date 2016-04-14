using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class AttributeType : BaseEntity
    {
        public AttributeType()
        {
            Attributes = new HashSet<Attribute>();
        }

        public string Name { get; set; }


        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
