using System;
using System.ComponentModel.DataAnnotations;
using Cash.Domain.Models;

namespace Cash.Web.Models
{
    public class DisplayResourceViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Наименование")]
        public string Name { get; set; }

        [Display(Name = "Единицы измерения")]
        public ResourceMeasureUnit MeasureUnit { get; set; }

        public string MeasureUnitName { get; set; }

        [Display(Name = "Доп. информация")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Дата создания")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}")]
        public DateTime Created { get; set; }

        [Display(Name = "Кем создано")]
        public User CreatedBy { get; set; }

        [Display(Name = "Дата изменения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}")]
        public DateTime? Modified { get; set; }

        [Display(Name = "Кем изменено")]
        public User ModifiedBy { get; set; }

        public bool CanBeDeleted { get; set; }

        [Display(Name = "Цена")]
        public decimal? Price { get; set; }
    }
}