﻿using System;
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
        public async Task<Guid> Create(ObjectiveModel objectiveModel)
        {
            var entity = _mapper.Map<ObjectiveEntity>(objectiveModel);
            if (objectiveModel.ParentId == Guid.Empty)
            {
                entity.ParentId = null;
                await _dbRepository.AddAsync(entity);
                await _dbRepository.SaveChangesAsync();
            }
            else
            {
                var parentEntity = _dbRepository
                    .Get<ObjectiveEntity>(x => x.Id == objectiveModel.ParentId)
                    .Include(x => x.SubObjectives).FirstOrDefault();
                parentEntity?.SubObjectives.Add(entity);
                await _dbRepository.SaveChangesAsync();

            }
            return entity.Id;
        }

        public async Task<Guid> Update(ObjectiveModel objectiveModel)
        {
            var entity = _mapper.Map<ObjectiveEntity>(objectiveModel);

            await _dbRepository.UpdateAsync(entity);
            await _dbRepository.SaveChangesAsync();

            return entity.Id;
        }

        public ObjectiveModel Get(Guid id)
        {
            var entity = _dbRepository.Get<ObjectiveEntity>().FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<ObjectiveModel>(entity);
            return model;
        }

        public List<ObjectiveModel> GetAllActive()
        {
            var entitiesCollection = _dbRepository.Get<ObjectiveEntity>();
            var result = _mapper.Map<List<ObjectiveModel>>(entitiesCollection);
            if (result == null || !result.Any())
            {
                return null;
            }

            return result;
        }

        public async Task Delete(Guid id)
        {
            await _dbRepository.DeleteAsync<ObjectiveEntity>(id);
            await _dbRepository.SaveChangesAsync();
        }
    }
}
