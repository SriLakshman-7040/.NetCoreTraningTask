using Microsoft.EntityFrameworkCore;
using WFM.Domain_DbFrstApprh.Models;

namespace WFM_Api.API_Context
{
    public class WFM_API_Context : DbContext
    {
        public WFM_API_Context(DbContextOptions<WFM_API_Context> options) : base(options)
        {
                
        }

        public DbSet<Employee_DBF> Employees { get; set; }

        public DbSet<SoftLock_DBF> Softlock { get; set; }

        public DbSet<Users_DBF> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee_DBF>()
        //    .HasMany(p => p.PurnchaseOrderItems)
        //    .WithOne(g => g.PurnchaseOrder).HasForeignKey(s => s.PurnchaseOrderId)
        //    .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
