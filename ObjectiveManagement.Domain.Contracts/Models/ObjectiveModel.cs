using System;

namespace ObjectiveManagement.Domain.Contracts.Models
{
    public class ObjectiveModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Performers { get; set; }
        public ObjectiveStatus ObjectiveStatus { get; set; }
        public int EstimateTime { get; set; }
        public int FactTime { get; set; }
        public DateTime CompletedTime { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}