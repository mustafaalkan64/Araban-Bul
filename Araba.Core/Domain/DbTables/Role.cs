using Araba.Core.Domain;
using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class Role : BaseEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
