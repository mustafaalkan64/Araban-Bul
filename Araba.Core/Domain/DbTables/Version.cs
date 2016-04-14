namespace Araba.Core.Domain.DbTables
{
    public class Version : BaseEntity
    {
        public string Name { get; set; }
        public int ModelId { get; set; }

        public virtual Model Model { get; set; }
    }
}
