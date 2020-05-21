namespace ObjectiveManagement.Domain.Contracts.Models
{
    /// <summary>
    /// Класс элемента меню, необходим для работы jsTree
    /// </summary>
    public class MenuItemModel
    {
        /// <summary>
        /// Id ноды
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Родительская нода
        /// </summary>
        public string Parent { get; set; }
        /// <summary>
        /// Текст ноды
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Иконка в дереве
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Признак наличия потомка
        /// </summary>
        public bool Children { get; set; }
        /// <summary>
        /// Поле для любых дополнительных данных
        /// </summary>
        public string Data { get; set; }
    }
}
