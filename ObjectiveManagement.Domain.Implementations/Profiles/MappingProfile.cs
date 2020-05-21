using System;
using System.Linq;
using AutoMapper;
using ObjectiveManagement.DataAccess.Entities;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Domain.Implementations.Profiles
{
    /// <summary>
    /// Профиль для маппинга сущностей
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ObjectiveEntity, ObjectiveModel>()
                .ForMember(dest=>dest.CreatedTime, opt=>opt.MapFrom(src=>src.CreatedTime.ToString("f")))
                .ForMember(dest=>dest.CompletedTime, opt=>opt.MapFrom(src=>src.CompletedTime.ToString("f")))
                .AfterMap((src, dest) =>
                {
                    if (dest.SubObjectives.Any())
                    {
                        foreach (var objective in dest.SubObjectives)
                        {
                            if (objective.ObjectiveStatus != ObjectiveStatusType.Completed)
                            {
                                dest.CanComplete = false;
                                return;
                            }
                            dest.CanComplete = true;
                        }
                    }
                    else
                    {
                        dest.CanComplete = true;
                    }
                });

            CreateMap<ObjectiveModel, ObjectiveEntity>()
                .ForMember(dest => dest.CompletedTime, opt => opt.MapFrom(src => DateTime.Parse(src.CompletedTime)));

            CreateMap<ObjectiveEntity, MenuItemModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.ParentId == null ? "#" : src.ParentId.ToString()))
                .ForMember(dest=>dest.Data, opt=>opt.MapFrom(src=>src.ObjectiveStatus.ToString()))
                .AfterMap((src,dest)=>
                {
                    dest.Icon = src.ObjectiveStatus switch
                    {
                        ObjectiveStatusType.Assigned => "/img/task_assigned.png",
                        ObjectiveStatusType.InProgress => "/img/task_in_progress.png",
                        ObjectiveStatusType.Suspended => "/img/task_suspended.png",
                        ObjectiveStatusType.Completed => "/img/task_completed.png",
                        _ => "/img/task_assigned.png",
                    };
                    if (src.SubObjectives.Any()) 
                        dest.Children = true;
                });
        }
    }
}
