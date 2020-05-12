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
                .ForMember(dest=>dest.TotalEstimateTime, opt=>opt.MapFrom<CustomResolver>());
            CreateMap<ObjectiveModel, ObjectiveEntity>()
                .ForMember(dest=>dest.EstimateTime, opt=>opt.MapFrom(src=>src.TotalEstimateTime));
            CreateMap<ObjectiveEntity, MenuItemModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.parent, opt => opt.MapFrom(src => src.ParentId == null ? "#" : src.ParentId.ToString()))
                .AfterMap((src,dst)=>
                {
                    dst.Icon = "/img/task.png";
                    dst.a_attr.Href = "id?id=" + dst.Id;
                });
        }
        private class CustomResolver : IValueResolver<ObjectiveEntity, ObjectiveModel, int>
        {
            public int Resolve(ObjectiveEntity source, ObjectiveModel destination, int totalTime, ResolutionContext context)
            {
                return CalculateEstimateTime(source);
            }

            private int CalculateEstimateTime(ObjectiveEntity data)
            {
                return data.EstimateTime + data.SubObjectives.Sum(CalculateEstimateTime);
            }
        }
    }
}
