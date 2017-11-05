using System.Linq;
using AutoMapper;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using Cash.Domain.Services;
using Cash.Web.Models;
using Newtonsoft.Json;

namespace Cash.Web.Mapper
{
    public class WebApiAutomapperProfile : Profile
    {
        public WebApiAutomapperProfile(ITaskResourceRepository taskResourceRepository, ITransliterationService transliterationService)
        {
            CreateMap<Resource, EditResourceViewModel>().ReverseMap();

            CreateMap<Task, EditTaskViewModel>()
                .ForMember(v => v.ResourcesJson, m => m.MapFrom(v => JsonConvert.SerializeObject(v.Resources.Select(r => new
                {
                    id = r.Resource.Id,
                    name = r.Resource.Name,
                    unit_name  = r.Resource.MeasureUnit.Name,
                    quantity = r.Quantity
                }))))
                .ForMember(v => v.ParentTitle, m => m.MapFrom(v => v.Parent != null ? $"#{v.Parent.Id} {v.Parent.Name}" : string.Empty))
                .ReverseMap();

            CreateMap<Project, EditProjectViewModel>().ReverseMap()
                .ForMember(v => v.Code, m => m.MapFrom(v => transliterationService.Transliterate(v.Name.Replace(' ', '_').Replace('-', '_').ToLowerInvariant())));

            CreateMap<Resource, DisplayResourceViewModel>()
                .ForMember(v => v.CreatedBy, m => m.MapFrom(v => v.CreatedByUser))
                .ForMember(v => v.ModifiedBy, m => m.MapFrom(v => v.ModifiedByUser))
                .ForMember(v => v.CanBeDeleted, m => m.MapFrom(v => !taskResourceRepository.UsedInTasks(v.Id)));

            CreateMap<Task, DisplayTaskViewModel>()
                .ForMember(v => v.CreatedBy, m => m.MapFrom(v => v.CreatedByUser))
                .ForMember(v => v.ModifiedBy, m => m.MapFrom(v => v.ModifiedByUser))
                .ForMember(v => v.Workload, m => m.MapFrom(v => v.WorkloadAutoCalc ? v.GetCalculatedWorkload() : v.Workload))
                .ForMember(v => v.TaskType, m => m.MapFrom(v => v.TaskType.Name))
                .ForMember(v => v.TaskStatus, m => m.MapFrom(v => v.TaskStatus.Name))
                .ForMember(v => v.Transitions, m => m.MapFrom(v => v.TaskStatus.Transitions));

            CreateMap<Project, DisplayProjectViewModel>()
                .ForMember(v => v.CreatedBy, m => m.MapFrom(v => v.CreatedByUser))
                .ForMember(v => v.ModifiedBy, m => m.MapFrom(v => v.ModifiedByUser))
                .ForMember(v => v.CanBeDeleted, m => m.MapFrom(v => !(v.Tasks.Any() || v.Resources.Any())));
        }
    }
}