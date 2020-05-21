using System;
using System.Collections.Generic;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.DataAccess.Entities
{
    /// <summary>
    /// Класс описывающий задачу
    /// </summary>
    public class ObjectiveEntity : BaseEntity
    {
        /// <summary>
        /// Название задания
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Список исполнителей
        /// </summary>
        public string Performers { get; set; }
        /// <summary>
        /// Статус задачи
        /// </summary>
        public ObjectiveStatusType ObjectiveStatus { get; set; }
        /// <summary>
        /// Примерное время выполнения задачи
        /// </summary>
        public int EstimateTime { get; set; }
        /// <summary>
        /// Фактическое время выполнения
        /// </summary>
        public int FactTime { get; set; }
        /// <summary>
        /// Время выполнения
        /// </summary>
        public DateTime CompletedTime { get; set; }
        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Объект родителя
        /// </summary>
        public ObjectiveEntity ParentObjective { get; set; }
        /// <summary>
        /// Список подзадач
        /// </summary>
        public ICollection<ObjectiveEntity> SubObjectives { get; set; }

        public ObjectiveEntity()
        {
            SubObjectives = new List<ObjectiveEntity>();
        }

    }
}
