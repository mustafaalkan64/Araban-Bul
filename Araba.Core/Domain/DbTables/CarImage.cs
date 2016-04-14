namespace Araba.Core.Domain.DbTables
{
    public class CarImage : BaseEntity
    {
        public string ImgUrl { get; set; }
        public string ImgUrlSmall { get; set; }
        public string ImgUrlMiddle { get; set; }
        public string ImgUrlBig { get; set; }
        public string ImgName { get; set; }
        public int ImgSize { get; set; }
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
