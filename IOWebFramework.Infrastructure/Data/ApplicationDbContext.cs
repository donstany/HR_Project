using System;
using CDN.Core3.Data.Contracts;
using CDN.Core3.Data.Data;
using IO.LogOperation.Models;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Infrastructure.Data.Models.Identity;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IOWebFramework.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, ICdnContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity configuration

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserClaimConfiguration());
            builder.ApplyConfiguration(new ApplicationUserLoginConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleClaimConfiguration());
            builder.ApplyConfiguration(new ApplicationUserTokenConfiguration());

            #endregion

            #region Seed for Development Environment 
            builder.Entity<EducationInstitution>().Property(b => b.Id)
                                  .HasIdentityOptions(startValue: 6);

            builder.Entity<Degree>().Property(b => b.Id)
                                  .HasIdentityOptions(startValue: 4);

            builder.Entity<Employee>().Property(b => b.Id)
                               .HasIdentityOptions(startValue: 7);

            builder.Entity<Position>().Property(b => b.Id)
                              .HasIdentityOptions(startValue: 6);

            builder.Entity<Person>().Property(b => b.Id)
                            .HasIdentityOptions(startValue: 7);

            builder.Entity<TrainingCenter>().Property(b => b.Id)
                            .HasIdentityOptions(startValue: 4);

            builder.Entity<TrainingName>().Property(b => b.Id)
                            .HasIdentityOptions(startValue: 5);

            builder.Entity<CertificateNameIssuer>().Property(b => b.Id)
                            .HasIdentityOptions(startValue: 8);

            builder.Entity<CertificateType>().Property(b => b.Id)
                            .HasIdentityOptions(startValue: 5);
            #endregion

            builder.Entity<Classifier>().Property(b => b.Id)
                                    .HasIdentityOptions(startValue: 1819);

            builder.Entity<Department>().Property(b => b.Id)
                                 .HasIdentityOptions(startValue: 52);

            builder.Entity<Branch>().Property(b => b.Id)
                               .HasIdentityOptions(startValue: 28);

            builder.Entity<Employee>()
                        .HasIndex(e => new { e.Td })
                        .IsUnique(true);

            builder.Entity<Team>()
                       .HasIndex(c => new { c.ProjectId, c.PersonId, c.ProjectRoleId })
                       .IsUnique(true);

            builder.Entity<DiplomaAttachment>()
                .HasKey(ck => new { ck.DiplomaId, ck.AttachedDocumentId });

            builder.Entity<CertificateAttachment>()
                .HasKey(ck => new { ck.CertificateId, ck.AttachedDocumentId });

            builder.Entity<TrainingAttachment>()
               .HasKey(ck => new { ck.TrainingId, ck.AttachedDocumentId });

            builder.Entity<TechnologyProject>()
                        .HasIndex(t => new { t.ProjectId, t.TechnologyId })
                        .IsUnique(true);
            // builder.Entity<ProjectRole>().HasKey(pr => new { pr.ProjectId, pr.RoleId });
            builder.Entity<Person>()
                         .Property(b => b.SyncedAt)
                          .HasDefaultValue(new DateTime(2020, 1, 1, 0, 0, 0, 500, DateTimeKind.Local).AddTicks(1000));
            builder.Entity<Employee>()
                        .Property(b => b.SyncedAt)
                        .HasDefaultValue(new DateTime(2020, 1, 1, 0, 0, 0, 500, DateTimeKind.Local).AddTicks(1000));

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }
        #region Nomenclatures

        //public DbSet<AttachmentType> AttachmentTypes { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<CertificateAttachment> CertificateAttachments { get; set; }
        public DbSet<CertificateNameIssuer> CertificateNameIssuers { get; set; }
        public DbSet<CertificateType> CertificateTypes { get; set; }
        public DbSet<Classifier> Classifiers { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DiplomaAttachment> DiplomaAttachments { get; set; }

        //public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<EducationInstitution> EducationInstitutions { get; set; }

        //public DbSet<Position> Positions { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TrainingCenter> TrainingCenters { get; set; }
        public DbSet<TrainingAttachment> TrainingAtttachments { get; set; }
        public DbSet<TrainingName> TrainingNames { get; set; }
        public DbSet<SchoolProfile> SchoolProfiles { get; set; }
        public DbSet<Client> Clients { get; set; }
        #endregion

        #region CDN

        public DbSet<CdnFile> CdnFiles { get; set; }

        public DbSet<CdnFileContent> CdnFileContents { get; set; }

        #endregion

        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Diploma> Diplomas { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LogOperation> LogOperation { get; set; }
        public DbSet<Person> Persons { get; set; }

        //public DbSet<ProjectDetail> ProjectDetails { get; set; }
        public DbSet<TechnologyProject> TechnologyProjects { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
