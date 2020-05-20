﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ObjectiveManagement.DataAccess;
using ObjectiveManagement.DataAccess.Entities;
using ObjectiveManagement.Domain.Contracts;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Domain.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly IDbRepository _dbRepository;
        private List<MenuItemModel> _menuItems;

        public MenuService(IMapper mapper, IDbRepository dbRepository)
        {
            _mapper = mapper;
            _dbRepository = dbRepository;
            _menuItems = new List<MenuItemModel>();
        }
        
        public List<MenuItemModel> GetTree()
        {
            var entitiesCollection = _dbRepository
                .Get<ObjectiveEntity>()
                .Where(o => o.ParentId == null)
                .Include(o=>o.SubObjectives)
                .ToList();
            var menuItems = _mapper.Map<List<MenuItemModel>>(entitiesCollection);
            return menuItems;
        }

        public List<MenuItemModel> GetTree(Guid id)
        {
            var entitiesCollection = _dbRepository
                .Get<ObjectiveEntity>()
                .Include(o=>o.SubObjectives)
                .Where(o => o.ParentId == id)
                .ToList();
            var menuItems = _mapper.Map<List<MenuItemModel>>(entitiesCollection);
            return menuItems;
        }

    }
}
