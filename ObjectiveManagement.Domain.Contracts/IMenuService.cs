﻿using System;
using System.Collections.Generic;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Domain.Contracts
{
    public interface IMenuService
    {
        List<MenuItemModel> GetMenuItemsList();
        List<MenuItemModel> GetTree();
        List<MenuItemModel> GetTree(Guid id);
    }
}