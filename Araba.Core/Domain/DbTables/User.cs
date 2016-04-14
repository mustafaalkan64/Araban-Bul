using System;
using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class User : BaseEntity
    {
        public User()
        {
            Cars = new HashSet<Car>();
            Roles = new HashSet<Role>();
        }

        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsConfirmed { get; set; }
        public Guid ConfirmationId { get; set; }
        public int UserTypeId { get; set; }
        public string ImgUrl { get; set; }
        public string ImgUrlSmall { get; set; }
        public string ImgUrlMiddle { get; set; }
        public string ImgUrlBig { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }
        public string Adress { get; set; }
        public int? DistrictId { get; set; }
        public string Street { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LastLoginIP { get; set; }
        public string Website { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual District District { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Car> FavoriteCars { get; set; }
    }
}
