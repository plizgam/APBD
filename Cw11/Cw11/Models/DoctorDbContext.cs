using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class DoctorDbContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }



        public DoctorDbContext(DbContextOptions options) : base(options){ }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("Patient_PK");
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.BirthDate).IsRequired();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_PK");
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_PK");
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();

            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_PK");
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();


                entity.HasOne(e => e.Patient)
                      .WithMany(d => d.Prescriptions)
                      .HasForeignKey(x => x.IdPatient)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("Prescription_Patient");

                entity.HasOne(e => e.Doctor)
                      .WithMany(d => d.Prescriptions)
                      .HasForeignKey(x => x.IdDoctor)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("Prescription_Doctor");

            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {

                entity.ToTable("Prescription_Medicament");


                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription }).HasName("Prescription_Medicament_PK");

                entity.Property(e => e.Details).HasMaxLength(100).IsRequired();

                entity.HasOne(e => e.Medicament)
                      .WithMany(d => d.PrescriptionMedicaments)
                      .HasForeignKey(x => x.IdMedicament)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("PrMed_Medicament");

                entity.HasOne(e => e.Prescription)
                      .WithMany(d => d.PrescriptionMedicaments)
                      .HasForeignKey(x => x.IdPrescription)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("PrMed_Prescrption");

            });



            Seed(modelBuilder);

        }


        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    IdPatient = 1,
                    FirstName = "Miłosz",
                    LastName = "Pliżga",
                    BirthDate = Convert.ToDateTime("15.07.1999")
                },
                new Patient
                {
                    IdPatient = 2,
                    FirstName = "Oliwia",
                    LastName = "Żak",
                    BirthDate = Convert.ToDateTime("05.01.1998")
                }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    IdDoctor = 1,
                    FirstName = "Anna",
                    LastName = "Kowalska",
                    Email = "anna@kowalska.pl"
                },
                new Doctor
                {
                    IdDoctor = 2,
                    FirstName = "Magdalena",
                    LastName = "Nowak",
                    Email = "magdalena@nowak.pl"
                }
            );


            modelBuilder.Entity<Medicament>().HasData(
                new Medicament
                {
                    IdMedicament = 1,
                    Name = "Ibuprom Sprint Max Mega Plus",
                    Description = "The best medicament",
                    Type = "PainKiller" 
                }
            );

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    IdPrescription = 1,
                    Date = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(7),
                    IdDoctor = 1,
                    IdPatient = 1
                }
            );


            modelBuilder.Entity<PrescriptionMedicament>().HasData(
                new PrescriptionMedicament
                {
                   IdMedicament = 1,
                   IdPrescription = 1,
                   Details = "Take one pill after breakfast and one pill before sleep."
                }
            );
           

        }

    }
}
