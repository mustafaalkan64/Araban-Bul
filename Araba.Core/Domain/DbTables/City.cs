using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class City : BaseEntity
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        public string Name { get; set; }
        public string PlateCode { get; set; }
        public string PhoneCode { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
