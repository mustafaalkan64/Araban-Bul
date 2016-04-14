using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class Brand : BaseEntity
    {
        public Brand()
        {
            Models = new HashSet<Model>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }
        public string ImgUrl { get; set; }
        public string ImgUrlSmall { get; set; }
        public string ImgUrlMiddle{ get; set; }
        public string ImgUrlBig { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
