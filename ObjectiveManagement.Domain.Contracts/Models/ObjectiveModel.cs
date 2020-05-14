using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ObjectiveManagement.Domain.Contracts.Models
{
    public class ObjectiveModel
    {
        [BindNever]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Укажите название задачи")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина названия должна быть от 3 до 100 символов")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Заполните исполнителей")]
        public string Performers { get; set; }
        public int TotalSubObjectivesEstimateTime { get; set; }
        public int TotalSubObjectivesFactTime { get; set; }
        [Required(ErrorMessage = "Заполните примерное время выполнения")]
        public int EstimateTime { get; set; }
        public int FactTime { get; set; }
        [Required(ErrorMessage = "Статус должен быть проставлен")]
        public ObjectiveStatus ObjectiveStatus { get; set; }
        [Required(ErrorMessage = "Время создания должно быть установлено")]
        public string CreatedTime { get; set; }
        public string CompletedTime { get; set; }
        public Guid? ParentId { get; set; }
        [BindNever]
        public List<ObjectiveModel> SubObjectives { get; set; }
        public ObjectiveModel()
        {
            ParentId = null;
        }
    }
}