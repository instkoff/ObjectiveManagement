using System;

namespace ObjectiveManagement.DataAccess.Entities
{
    /// <summary>
    /// Свойства базовой сущности
    /// </summary>
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
