using DataBase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Data
{
    public class HospitalDbContext : IdentityDbContext<User>
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasOne<Doctor>(p => p.Doctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.DoctorId);

            modelBuilder.Entity<Consultation>()
                .HasMany<Drug>(c => c.Drugs)
                .WithOne(d => d.Consultation)
                .HasForeignKey(d => d.ConsultationId);

            modelBuilder.Entity<Consultation>()
                .HasOne<Doctor>(c => c.Doctor)
                .WithMany(d => d.Consultations)
                .HasForeignKey(c => c.DoctorId);

            modelBuilder.Entity<Consultation>()
                .HasOne<Patient>(c => c.Patient)
                .WithMany(p => p.Consultations)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
