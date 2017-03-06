using JIF.Scheduler.Core.Domain;
using System.Data.Entity;

namespace JIF.Scheduler.Data.EntityFramework
{
    public class JIFDbContext : DbContext
    {
        public JIFDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobInfo>().ToTable("tbl_jobinfo").HasKey(d => d.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
