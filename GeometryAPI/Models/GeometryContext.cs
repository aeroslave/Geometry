using Microsoft.EntityFrameworkCore;

namespace GeometryAPI.Models
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public sealed class GeometryContext : DbContext
    {
        /// <summary>
        /// Фигуры.
        /// </summary>
        public DbSet<Figure> Figures { get; set; }

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public GeometryContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}