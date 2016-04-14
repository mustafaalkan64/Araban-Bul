using System.ComponentModel.DataAnnotations;

namespace Best.Web.Framework.Models
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
