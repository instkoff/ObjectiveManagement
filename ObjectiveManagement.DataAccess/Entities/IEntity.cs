using System;

namespace ObjectiveManagement.DataAccess.Entities
{
    /// <summary>
    /// Абстракция для базовой сущности
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedTime { get; set; }
    }
}
