using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Cars = new HashSet<Car>();
            //BodyTypes = new HashSet<BodyType>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        //public virtual ICollection<BodyType> BodyTypes { get; set; }
    }
}
