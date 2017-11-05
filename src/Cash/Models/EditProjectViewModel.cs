using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Cash.Web.Models
{
    public class EditProjectViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Наименование")]
        public string Name { get; set; }

        [Display(Name = "Доп. информация")]
        [AllowHtml]
        public string Description { get; set; }

    }
}