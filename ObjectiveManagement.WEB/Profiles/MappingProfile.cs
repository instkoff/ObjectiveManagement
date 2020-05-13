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
                .ForMember(dest=>dest.TotalEstimateTime, opt=>opt.MapFrom(src=>src.EstimateTime))
                .ForMember(dest=>dest.CreatedTime, opt=>opt.MapFrom(src=>src.CreatedTime.ToString("yyyy-MM-ddThh:mm")))
                .ForMember(dest=>dest.CompletedTime, opt=>opt.MapFrom(src=>src.CompletedTime.ToString("yyyy-MM-ddThh:mm")));
            CreateMap<ObjectiveModel, ObjectiveEntity>()
                .ForMember(dest => dest.EstimateTime, opt => opt.MapFrom(src => src.TotalEstimateTime))
                .ForMember(dest => dest.CompletedTime, opt => opt.MapFrom(src => DateTime.Parse(src.CompletedTime)))
                .AfterMap((src, dest) => dest.CreatedTime = DateTime.Now);
            CreateMap<ObjectiveEntity, MenuItemModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.parent, opt => opt.MapFrom(src => src.ParentId == null ? "#" : src.ParentId.ToString()))
                .AfterMap((src,dest)=>
                {
                    switch (src.ObjectiveStatus)
                    {
                        case ObjectiveStatus.Assigned:
                            dest.Icon = "/img/task_assigned.png";
                            break;
                        case ObjectiveStatus.InProgress:
                            dest.Icon = "/img/task_in_progress.png";
                            break;
                        case ObjectiveStatus.Suspended:
                            dest.Icon = "/img/task_suspended.png";
                            break;
                        case ObjectiveStatus.Completed:
                            dest.Icon = "/img/task_completed.png";
                            break;
                        default:
                            dest.Icon = "/img/task_assigned.png";
                            break;
                    }
                    //dest.a_attr.Href = "/api/Home/id?id=" + dest.Id;
                    if (src.SubObjectives.Any()) 
                        dest.Children = true;
                });
        }
        //private class CustomResolver : IValueResolver<ObjectiveEntity, ObjectiveModel, int>
        //{
        //    public int Resolve(ObjectiveEntity source, ObjectiveModel destination, int totalTime, ResolutionContext context)
        //    {
        //        return CalculateEstimateTime(source);
        //    }

        //    private int CalculateEstimateTime(ObjectiveEntity data)
        //    {
        //        return data.EstimateTime + data.SubObjectives.Sum(CalculateEstimateTime);
        //    }
        //}
    }
}
