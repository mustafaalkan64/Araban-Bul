using System;
using System.Collections.Generic;

namespace Araba.Core.Domain.DbTables
{
    public class Car : BaseEntity
    {
        public Car()
        {
            Attributes = new HashSet<Attribute>();
            Tags = new HashSet<Tag>();
            Images = new HashSet<CarImage>();
        }

        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public int ModelYear { get; set; }
        public int EngineSize { get; set; }
        public int ModelId { get; set; }
        public int? VersionId { get; set; }
        public int FuelTypeId { get; set; }
        public int BodyTypeId { get; set; }
        public int Km { get; set; }
        public int GearTypeId { get; set; }
        public int ColorId { get; set; }
        public int EnginePower { get; set; }
        public int PublishUserId { get; set; }
        public int UsageStatusId { get; set; }
        public bool AvailableForSwap { get; set; }
        public int Price { get; set; }
        public int PlateTypeId { get; set; }
        public int DistrictId { get; set; }
        public string ImgUrl { get; set; }
        public string ImgUrlSmall { get; set; }
        public string ImgUrlMiddle { get; set; }
        public string ImgUrlBig{ get; set; }

        public virtual User PublishUser { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual BodyType BodyType { get; set; }
        public virtual GearType GearType { get; set; }
        public virtual Color Color { get; set; }
        public virtual PlateType PlateType { get; set; }
        public virtual District District { get; set; }
        public virtual UsageStatus UsageStatus { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<CarImage> Images { get; set; }
        public virtual ICollection<User> FavoriteMarkedUsers { get; set; }
    }
}
