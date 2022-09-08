using Microsoft.EntityFrameworkCore;
using WFM.Domain.Models;

namespace WFM.Data
{
    public class WorkForceManagementDbContext : DbContext
    {
        public WorkForceManagementDbContext(DbContextOptions<WorkForceManagementDbContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<SkillMap> SkillMap { get; set; }
        public DbSet<SoftLock> SoftLock { get; set; }
    }
}
