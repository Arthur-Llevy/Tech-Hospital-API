using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Database;

public class DatabaseContext : DbContext 
{
    public DatabaseContext(DbContextOptions options) : base (options)
    {

    }

    protected void OModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoctorsDaysAvailableEntity>()
        .HasOne(x => x.Doctor)
        .WithMany(x => x.Available_Days)
        .HasForeignKey(x => x.Doctor_Id);
    }


    DbSet<AdministratorsEntity> Administrators { get; set; } = default!;
    DbSet<DoctorsEntity> Doctors { get; set; } = default!;
    DbSet<DoctorsDaysAvailableEntity> Doctors_Avaiable_Days { get; set; } = default!;
}