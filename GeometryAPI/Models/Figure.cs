using System;
using System.ComponentModel.DataAnnotations;

namespace GeometryAPI.Models
{
    /// <summary>
    /// Модель фигуры.
    /// </summary>
    public class Figure
    {
        /// <summary>
        /// Площадь
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public Types Type { get; set; }

        /// <summary>
        /// Дата и время.
        /// </summary>
        public DateTime DateAndTime { get; set; }
    }
}