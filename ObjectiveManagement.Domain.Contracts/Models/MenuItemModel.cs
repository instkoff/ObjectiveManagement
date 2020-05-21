﻿namespace ObjectiveManagement.Domain.Contracts.Models
{
    /// <summary>
    /// Класс элемента меню, необходим для работы jsTree
    /// </summary>
    public class MenuItemModel
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public bool Children { get; set; }
        public string Data { get; set; }
    }
}
