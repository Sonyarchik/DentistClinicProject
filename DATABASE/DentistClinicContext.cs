using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DentistClinicProject;

public partial class DentistClinicContext : DbContext
{
    public DentistClinicContext()
    {
    }

    public DentistClinicContext(DbContextOptions<DentistClinicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryOfService> CategoryOfServices { get; set; }

    public virtual DbSet<DiagnosisList> DiagnosisLists { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<ListOfService> ListOfServices { get; set; }

    public virtual DbSet<MedicalBook> MedicalBooks { get; set; }

    public virtual DbSet<MedicineRegistrator> MedicineRegistrators { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MEGABOOK_K16\\SQLEXPRESS;Database=DentistClinic;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryOfService>(entity =>
        {
            entity.HasKey(e => e.IdCategory);

            entity.ToTable("Category_of_services");

            entity.Property(e => e.IdCategory).HasColumnName("ID_Category");
            entity.Property(e => e.CategoryDescription)
                .IsUnicode(false)
                .HasColumnName("Category_Description");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Category_name");
        });

        modelBuilder.Entity<DiagnosisList>(entity =>
        {
            entity.HasKey(e => e.IdDiagnosis);

            entity.ToTable("Diagnosis_list");

            entity.Property(e => e.IdDiagnosis).HasColumnName("ID_Diagnosis");
            entity.Property(e => e.DiagnosisDescription)
                .IsUnicode(false)
                .HasColumnName("Diagnosis_Description");
            entity.Property(e => e.DiagnosisName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Diagnosis_Name");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor);

            entity.Property(e => e.IdDoctor).HasColumnName("ID_Doctor");
            entity.Property(e => e.BdayDate)
                .HasColumnType("date")
                .HasColumnName("Bday_Date");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IdCategory).HasColumnName("ID_Category");
            entity.Property(e => e.IdJobTitle).HasColumnName("ID_JobTitle");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Phone).HasColumnType("decimal(11, 0)");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctors_Category_of_services");

            entity.HasOne(d => d.IdJobTitleNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdJobTitle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctors_Job_Title");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctors_Users");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee);

            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.IdJobTitle).HasColumnName("ID_JobTitle");
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("decimal(11, 0)")
                .HasColumnName("Phone_Number");

            entity.HasOne(d => d.IdJobTitleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdJobTitle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Job_Title");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Roles");
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.IdJobTitle);

            entity.ToTable("Job_Title");

            entity.Property(e => e.IdJobTitle).HasColumnName("ID_JobTitle");
            entity.Property(e => e.JobTitleDescription)
                .IsUnicode(false)
                .HasColumnName("JobTitle_Description");
            entity.Property(e => e.JobTitleName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("JobTitle_Name");
        });

        modelBuilder.Entity<ListOfService>(entity =>
        {
            entity.HasKey(e => e.IdService);

            entity.ToTable("List_of_services");

            entity.Property(e => e.IdService).HasColumnName("ID_Service");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.IdCategory).HasColumnName("ID_Category");
            entity.Property(e => e.NameOfService)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Name_of_service");
            entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.ListOfServices)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_List_of_services_Category_of_services");
        });

        modelBuilder.Entity<MedicalBook>(entity =>
        {
            entity.HasKey(e => e.IdMedicalBook);

            entity.ToTable("Medical_Book");

            entity.Property(e => e.IdMedicalBook).HasColumnName("ID_MedicalBook");
            entity.Property(e => e.DataAppointment)
                .HasColumnType("date")
                .HasColumnName("Data_Appointment");
            entity.Property(e => e.IdDiagnosis).HasColumnName("ID_Diagnosis");
            entity.Property(e => e.IdDoctor).HasColumnName("ID_Doctor");
            entity.Property(e => e.IdPatient).HasColumnName("ID_Patient");
            entity.Property(e => e.IdService).HasColumnName("ID_Service");
            entity.Property(e => e.IdStatus).HasColumnName("ID_Status");
            entity.Property(e => e.TimeAppointment)
                .HasPrecision(2)
                .HasColumnName("Time_Appointment");
            entity.Property(e => e.TotalPayment).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.Treatment).IsUnicode(false);

            entity.HasOne(d => d.IdDiagnosisNavigation).WithMany(p => p.MedicalBooks)
                .HasForeignKey(d => d.IdDiagnosis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medical_Book_Diagnosis_list");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.MedicalBooks)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medical_Book_Doctors");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.MedicalBooks)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medical_Book_Patients");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.MedicalBooks)
                .HasForeignKey(d => d.IdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medical_Book_List_of_services");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.MedicalBooks)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medical_Book_Statuses");
        });

        modelBuilder.Entity<MedicineRegistrator>(entity =>
        {
            entity.HasKey(e => e.IdMedicineReg);

            entity.ToTable("Medicine_Registrator");

            entity.Property(e => e.IdMedicineReg).HasColumnName("ID_MedicineReg");
            entity.Property(e => e.BdayDate)
                .HasColumnType("date")
                .HasColumnName("Bday_Date");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Phone).HasColumnType("decimal(11, 0)");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MedicineRegistrators)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medicine_Registrator_Users");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.IdPatient);

            entity.Property(e => e.IdPatient).HasColumnName("ID_Patient");
            entity.Property(e => e.BdayDate)
                .HasColumnType("date")
                .HasColumnName("Bday_Date");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PassportData)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("Passport_Data");
            entity.Property(e => e.Phone).HasColumnType("decimal(11, 0)");
            entity.Property(e => e.Snils)
                .HasColumnType("decimal(11, 0)")
                .HasColumnName("SNILS");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.RoleDescription)
                .IsUnicode(false)
                .HasColumnName("Role_Description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus);

            entity.Property(e => e.IdStatus).HasColumnName("ID_Status");
            entity.Property(e => e.StatusDescription)
                .IsUnicode(false)
                .HasColumnName("Status_Description");
            entity.Property(e => e.StatusName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Status_Name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasColumnType("decimal(11, 0)");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
