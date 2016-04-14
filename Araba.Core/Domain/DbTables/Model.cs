using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class Model : BaseEntity
    {
        public Model()
        {
            Versions = new HashSet<Version>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<Version> Versions { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
