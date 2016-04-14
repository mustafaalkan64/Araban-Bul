using Araba.Core.Domain.DbTables;
using Best.Web.Framework.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Araba.Web.Models
{
    public class EditCarModel:BaseViewModel
    {
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Yayında")]
        public bool IsPublished { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Model Yılı")]
        public int ModelYear { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Silindir Hacmi")]
        public int EngineSize { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Model")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Versiyon")]
        public int? VersionId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Yakıt Tipi")]
        public int FuelTypeId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kasa Tipi")]
        public int BodyTypeId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Km")]
        public int Km { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Vites Tipi")]
        public int GearTypeId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Renk")]
        public int ColorId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Beygir Gücü")]
        public int EnginePower { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Durumu")]
        public int UsageStatusId { get; set; }

        [Display(Name = "Takas Edilebilir")]
        public bool AvailableForSwap { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Fiyat")]
        public int Price { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Plaka Tipi")]
        public int PlateTypeId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "İlçe")]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Resim")]
        public HttpPostedFileBase Img { get; set; }

        public string ImgUrl { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Marka")]
        public int Brand { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<BodyType> BodyTypes { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
    }
}