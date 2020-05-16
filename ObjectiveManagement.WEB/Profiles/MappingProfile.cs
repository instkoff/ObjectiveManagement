using System;
using System.Linq;
using AutoMapper;
using ObjectiveManagement.DataAccess.Entities;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Web.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ObjectiveEntity, ObjectiveModel>()
                .ForMember(dest=>dest.CreatedTime, opt=>opt.MapFrom(src=>src.CreatedTime.ToString("yyyy-MM-ddThh:mm")))
                .ForMember(dest=>dest.CompletedTime, opt=>opt.MapFrom(src=>src.CompletedTime.ToString("yyyy-MM-ddThh:mm")));

            CreateMap<ObjectiveModel, ObjectiveEntity>()
                .ForMember(dest => dest.CompletedTime, opt => opt.MapFrom(src => DateTime.Parse(src.CompletedTime)))
                .AfterMap((src, dest) => dest.CreatedTime = DateTime.Now);

            CreateMap<ObjectiveEntity, MenuItemModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.ParentId == null ? "#" : src.ParentId.ToString()))
                .AfterMap((src,dest)=>
                {
                    dest.Icon = src.ObjectiveStatus switch
                    {
                        ObjectiveStatus.Assigned => "/img/task_assigned.png",
                        ObjectiveStatus.InProgress => "/img/task_in_progress.png",
                        ObjectiveStatus.Suspended => "/img/task_suspended.png",
                        ObjectiveStatus.Completed => "/img/task_completed.png",
                        _ => "/img/task_assigned.png",
                    };
                    if (src.SubObjectives.Any()) 
                        dest.Children = true;
                });
        }
    }
}
