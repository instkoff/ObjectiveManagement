using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ObjectiveManagement.Domain.Contracts.Models
{
    /// <summary>
    /// Модель для работы с задачами
    /// </summary>
    public class ObjectiveModel
    {
        [BindNever]
        public Guid Id { get; set; }
        /// <summary>
        /// Название задачи
        /// </summary>
        [Required(ErrorMessage = "Укажите название задачи")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина названия должна быть от 3 до 100 символов")]
        public string Name { get; set; }
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Список исполнителей
        /// </summary>
        [Required(ErrorMessage = "Заполните исполнителей")]
        public string Performers { get; set; }
        /// <summary>
        /// Общее ожидаемое время выполнения вместе с подзадачами
        /// </summary>
        public int TotalSubObjectivesEstimateTime { get; set; }
        /// <summary>
        /// Общее фактическое время выполнения вместе с подзадачами
        /// </summary>
        public int TotalSubObjectivesFactTime { get; set; }
        /// <summary>
        /// Плановое время выполнения
        /// </summary>
        [Required(ErrorMessage = "Заполните примерное время выполнения")]
        public int EstimateTime { get; set; }
        /// <summary>
        /// Фактическое время выполнения
        /// </summary>
        public int FactTime { get; set; }
        /// <summary>
        /// Статус задачи
        /// </summary>
        [Required(ErrorMessage = "Статус должен быть проставлен")]
        public ObjectiveStatusType ObjectiveStatus { get; set; }
        /// <summary>
        /// Признако возможности завершить задачу
        /// </summary>
        public bool CanComplete { get; set; }
        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public string CreatedTime { get; set; }
        /// <summary>
        /// Дата завершения задачи
        /// </summary>
        public string CompletedTime { get; set; }
        /// <summary>
        /// Id родителя
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Список подзадач
        /// </summary>
        [BindNever]
        public List<ObjectiveModel> SubObjectives { get; set; }
        public ObjectiveModel()
        {
            ParentId = null;
        }
    }
}