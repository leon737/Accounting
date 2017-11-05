using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Cash.Web.Models
{
    public class EditResourceViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Наименование")]
        public string Name { get; set; }

        [Display(Name = "Единицы измерения")]
        public int MeasureUnitId { get; set; }

        [Display(Name = "Доп. информация")]
        [AllowHtml]
        public string AdditionalInfo { get; set; }

        public IReadOnlyList<SelectListItem> MeasureUnits { get; set; }

        [Display(Name = "Цена")]
        public decimal? Price { get; set; }
    }
}