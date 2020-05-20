using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ObjectiveManagement.DataAccess;
using ObjectiveManagement.DataAccess.Entities;
using ObjectiveManagement.Domain.Contracts;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Domain.Implementations
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly IDbRepository _dbRepository;
        private readonly IMapper _mapper;

        public ObjectiveService(IDbRepository dbRepository, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
        }
        public async Task<MenuItemModel> Create(ObjectiveModel objectiveModel)
        {
            var entity = _mapper.Map<ObjectiveEntity>(objectiveModel);
            if (objectiveModel.ParentId == null || objectiveModel.ParentId == Guid.Empty)
            {
                await _dbRepository.AddAsync(entity);
            }
            else
            {
                var parentEntity = _dbRepository
                    .Get<ObjectiveEntity>(x => x.Id == objectiveModel.ParentId)
                    .Include(x => x.SubObjectives).FirstOrDefault();
                if (parentEntity == null) 
                    return null;
                parentEntity.SubObjectives.Add(entity);
            }
            await _dbRepository.SaveChangesAsync();
            var menuItem = _mapper.Map<MenuItemModel>(entity);
            return menuItem;
        }

        public async Task<Guid> Update(ObjectiveModel objectiveModel)
        {
            var entityForUpdate = _dbRepository
                .Get<ObjectiveEntity>()
                .FirstOrDefault(x => x.Id == objectiveModel.Id);
            if (entityForUpdate == null) 
                return Guid.Empty;
            if (objectiveModel.ObjectiveStatus == ObjectiveStatus.Completed)
            {
                var now = DateTime.Now;
                objectiveModel.CompletedTime = now.ToString("f");
                var createdTime = entityForUpdate.CreatedTime;
                objectiveModel.FactTime = (int)now.Subtract(createdTime).TotalHours;
            }
            _mapper.Map(objectiveModel, entityForUpdate);
            await _dbRepository.SaveChangesAsync();
            return entityForUpdate.Id;
        }

        public ObjectiveModel Get(Guid id)
        {
            var entity = _dbRepository
                .Get<ObjectiveEntity>()
                .Include(s=>s.SubObjectives)
                .ToList()
                .FirstOrDefault(x => x.Id == id);
            if (entity == null)
                return null;
            var model = _mapper.Map<ObjectiveModel>(entity);
            model.TotalSubObjectivesEstimateTime += CalculateEstimateTime(model);
            model.TotalSubObjectivesEstimateTime -= model.EstimateTime;
            model.TotalSubObjectivesFactTime += CalculateFactTime(model);
            model.TotalSubObjectivesFactTime -= model.FactTime;
            return model;
        }

        private int CalculateEstimateTime(ObjectiveModel data)
        {
            return data.EstimateTime + data.SubObjectives.Sum(CalculateEstimateTime);
        }
        private int CalculateFactTime(ObjectiveModel data)
        {
            return data.FactTime + data.SubObjectives.Sum(CalculateFactTime);
        }

        public List<ObjectiveModel> GetAllActive()
        {
            var entitiesCollection = _dbRepository
                .Get<ObjectiveEntity>()
                .Include(s=>s.SubObjectives)
                .AsEnumerable()
                .Where(p=>p.ParentId==null)
                .ToList();
            var result = _mapper.Map<List<ObjectiveModel>>(entitiesCollection);
            if (result == null || !result.Any())
            {
                return null;
            }
            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = _dbRepository
                .Get<ObjectiveEntity>()
                .Include(s=>s.SubObjectives)
                .FirstOrDefault(x => x.Id == id);
            if (entity != null && entity.SubObjectives.Any() || entity == null) 
                return false;
            await _dbRepository.RemoveAsync<ObjectiveEntity>(id);
            await _dbRepository.SaveChangesAsync();
            return true;
        }
    }
}
