using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Cash.Web.Models
{
    public class EditTaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Трудозатраты")]
        public decimal Workload { get; set; }

        [Display(Name = "Дата начала")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Доп. информация")]
        [AllowHtml]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Важность")]
        public int Importance { get; set; }

        [Display(Name = "Активность")]
        public bool Active { get; set; }

        [Display(Name = "Расчет трудозатрат по вложенным задачам")]
        public bool WorkloadAutoCalc { get; set; }

        [Display(Name = "Подзадача в")]
        public int? ParentId { get; set; }

        public string ResourcesJson { get; set; }

        public string ParentTitle { get; set; }

        [Display(Name = "Тип задачи")]
        public int TaskTypeId { get; set; }

        public IReadOnlyList<SelectListItem> TaskTypes { get; set; }

    }
}