using System;

namespace ObjectiveManagement.DataAccess.Entities
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
