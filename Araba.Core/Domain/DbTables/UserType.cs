using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class UserType : BaseEntity
    {
         public UserType()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
