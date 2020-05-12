using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ObjectiveManagement.DataAccess.Entities;

namespace ObjectiveManagement.DataAccess
{
    public class DataContext : DbContext
    {
        //todo Не забыть отключить логгер
        private readonly ILoggerFactory _loggerFactory;
        public DbSet<ObjectiveEntity> Objectives { get; set; }
        public DataContext(DbContextOptions<DataContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ObjectiveEntity>()
                .HasOne(p => p.ParentObjective)
                .WithMany(p => p.SubObjectives)
                .HasForeignKey(p => p.ParentId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public new IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }

    }

}
