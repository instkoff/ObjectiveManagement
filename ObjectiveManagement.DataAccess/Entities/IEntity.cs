using System;

namespace ObjectiveManagement.DataAccess.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedTime { get; set; }
        bool IsActive { get; set; }
    }
}
