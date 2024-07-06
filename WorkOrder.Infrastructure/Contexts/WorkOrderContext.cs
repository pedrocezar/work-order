using WorkOrder.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkOrder.Infrastructure.Contexts;

public class WorkOrderContext : DbContext
{
    public WorkOrderContext(DbContextOptions<WorkOrderContext> options) : base(options)
    { 
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<CompanyModel> Companies { get; set; }
    public DbSet<FinalizationModel> Finalizations { get; set; }
    public DbSet<WorkModel> Works { get; set; }
    public DbSet<WorkOrderModel> WorkOrders { get; set; }
    public DbSet<RelationalModel> Relationals { get; set; }
    public DbSet<ProfileModel> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().HasOne(s => s.Company).WithMany(s => s.Users).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WorkOrderModel>().HasOne(s => s.User).WithMany(s => s.WorkOrders).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<WorkOrderModel>().HasOne(s => s.Finalization).WithMany(s => s.WorkOrders).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RelationalModel>().HasOne(s => s.WorkOrder).WithMany(s => s.Relationals).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<RelationalModel>().HasOne(s => s.Work).WithMany(s => s.Relationals).OnDelete(DeleteBehavior.Restrict);
    }
}
