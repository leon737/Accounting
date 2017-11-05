using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cash.Domain.Models;

namespace Cash.Web.Models
{
    public class DisplayTaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Трудозатраты")]
        public decimal Workload { get; set; }

        [Display(Name = "Дата начала")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Доп. информация")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Важность")]
        public int Importance { get; set; }

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

        [Display(Name = "Активность")]
        public bool Active { get; set; }

        [Display(Name = "Расчет трудозатрат по вложенным задачам")]
        public bool WorkloadAutoCalc { get; set; }

        [Display(Name = "Тип")]
        public string TaskType { get; set; }

        [Display(Name = "Статус")]
        public string TaskStatus { get; set; }

        public int? ParentId { get; set; }

        public int ChildrenCount { get; set; }

        public ICollection<TaskResource> Resources { get; set; }

        public ICollection<TaskStatusTransition> Transitions { get; set; }

    }
}