using System.Collections.Generic;
using AutoMapper;
using ObjectiveManagement.DataAccess.Entities;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Web.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ObjectiveEntity, ObjectiveModel>();
            CreateMap<ObjectiveModel, ObjectiveEntity>();
            CreateMap<List<ObjectiveEntity>, List<ObjectiveModel>>();
            CreateMap<List<ObjectiveModel>,List<ObjectiveEntity>>();
        }
    }
}
