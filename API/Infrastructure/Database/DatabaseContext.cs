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

        modelBuilder.Entity<AppointmentsEntity>()
        .HasOne(x => x.Patient)
        .WithMany(x => x.Appointments)
        .HasForeignKey(x => x.Patient_Id);

        modelBuilder.Entity<AppointmentsEntity>()
        .HasOne(x => x.Doctor)
        .WithMany(x => x.Appointments)
        .HasForeignKey(x => x.Doctor_Id);

        modelBuilder.Entity<AppointmentsEntity>()
        .HasOne(x => x.Date)
        .WithOne(x => x.Appointment)
        .HasForeignKey<AppointmentsEntity>(x => x.Doctors_Days_Available_Id);
    }


    DbSet<AdministratorsEntity> Administrators { get; set; } = default!;
    DbSet<DoctorsEntity> Doctors { get; set; } = default!;
    DbSet<DoctorsDaysAvailableEntity> Doctors_Avaiable_Days { get; set; } = default!;
    DbSet<PatientsEntity> Patients { get; set; } = default!;
    DbSet<AppointmentsEntity> Appointments { get; set; } = default!;
}