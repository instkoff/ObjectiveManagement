using System;
using System.Collections.Generic;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.DataAccess.Entities
{
    public class ObjectiveEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Performers { get; set; }
        public ObjectiveStatus ObjectiveStatus { get; set; }
        public int EstimateTime { get; set; }
        public int FactTime { get; set; }
        public DateTime CompletedTime { get; set; }
        public Guid? ParentId { get; set; }
        public ObjectiveEntity ParentObjective { get; set; }
        public ICollection<ObjectiveEntity> SubObjectives { get; set; }

        public ObjectiveEntity()
        {
            SubObjectives = new List<ObjectiveEntity>();
        }

    }
}
