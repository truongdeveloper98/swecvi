using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SWECVI.ApplicationCore.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace SWECVI.Infrastructure.Data
{
    public class ManagerHospitalDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ManagerHospitalDbContext(DbContextOptions<ManagerHospitalDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<AppRole> AppRoles { get; set; } = default!;
        public DbSet<AssessmentReportReference> AssessmentReportReferences { get; set; } = default!;
        public DbSet<AssessmentTextReference> AssessmentTextReferences { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Finding> Findings { get; set; } = default!;
        public DbSet<Patient> Patients { get; set; } = default!;
        public DbSet<Study> Studies { get; set; } = default!;
        public DbSet<StudyParameter> StudyParameters { get; set; } = default!;
        public DbSet<ParameterReference> ParameterReferences { get; set; } = default!;
        public DbSet<PatientReport> PatientReports { get; set; } = default!;
        public DbSet<ParameterSetting> ParameterSettings { get; set; } = default!;
        public DbSet<StudyFinding> StudyFinding { get; set; } = default!;
        public DbSet<FindingStructure> FindingStructure { get; set; } = default!;
        public DbSet<CommonDicomTags> ComonDicomTags { get; set; } = default!;
        public DbSet<DicomTags> DicomTags { get; set; } = default!;
        public DbSet<ContextID> MeasurementContexts { get; set; } = default!;
        public DbSet<ManufacturerDicomParameters> ManufacturerDicomParameters { get; set; } = default!;
        public DbSet<Hospital> Hospitals { get; set; } = default!;
        public DbSet<Region> Regions { get; set; } = default!;
        public DbSet<SystemLog> SystemLogs { get; set; } = default!;
        public DbSet<PythonCode> PythonCodes { get; set; } = default!;
        public DbSet<PythonDefault> PythonDefaults { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Patient>()
                   .HasMany(e => e.Studies)
                   .WithOne(e => e.Patient)
                   .HasForeignKey(e => e.PatientId);

            builder.Entity<Hospital>()
                   .HasMany(e => e.Departments)
                   .WithOne(e => e.Hospital)
                   .HasForeignKey(e => e.IndexHospital);

            builder.Entity<Study>()
                   .HasMany(e => e.Parameters)
                   .WithOne(e => e.HospitalStudy)
                   .HasForeignKey(e => e.StudyIndex);

            builder.Entity<Study>()
                   .HasMany(e => e.StudyFindings)
                   .WithOne(e => e.Study)
                   .HasForeignKey(e => e.StudyId);

            builder.Entity<FindingStructure>()
                   .HasMany(e => e.StudyFindings)
                   .WithOne(e => e.FindingStructure)
                   .HasForeignKey(e => e.FindingStructureId);

            builder.Entity<ContextID>()
                  .HasMany(e => e.ParameterCodes)
                  .WithOne(e => e.ContextID)
                  .HasForeignKey(e => e.IndexContextID);

            builder.Entity<DicomTags>()
                   .HasMany(e => e.DicomParameters1)
                   .WithOne(e => e.DicomTags1)
                   .HasForeignKey(e => e.MeasurementMethod)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DicomTags>()
                    .HasMany(e => e.DicomParameters2)
                   .WithOne(e => e.DicomTags2)
                   .HasForeignKey(e => e.ImageMode)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DicomTags>()
                   .HasMany(e => e.DicomParameters3)
                   .WithOne(e => e.DicomTags3)
                   .HasForeignKey(e => e.ImageView)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DicomTags>()
                   .HasMany(e => e.DicomParameters4)
                   .WithOne(e => e.DicomTags4)
                   .HasForeignKey(e => e.CardiacPhase)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DicomTags>()
                   .HasMany(e => e.DicomParameters5)
                   .WithOne(e => e.DicomTags5)
                   .HasForeignKey(e => e.FindingSite)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DicomTags>()
                   .HasMany(e => e.DicomParameters6)
                   .WithOne(e => e.DicomTags6)
                   .HasForeignKey(e => e.FlowDirection)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Region>()
                   .HasMany(e => e.Hospitals)
                   .WithOne(e => e.Region)
                   .HasForeignKey(e => e.IndexRegion);

            AddSoftDeleteFilters(builder);

        }

        protected static void AddSoftDeleteFilters(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //other automated configurations left out
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
        }
        public override int SaveChanges()
        {
            UpdateEntityState();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntityState();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateEntityState()
        {
            var now = DateTime.UtcNow;

            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is BaseEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedAt = now;
                            entity.UpdatedAt = now;
                            break;
                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                            entity.UpdatedAt = now;
                            break;
                        case EntityState.Deleted:
                            entity.IsDeleted = true;
                            entity.DeletedAt = now;
                            changedEntity.State = EntityState.Modified;
                            break;
                    }
                }
            }
        }
    }

    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(
             this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension)
                .GetMethod(nameof(GetSoftDeleteFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall?.Invoke(null, new object[] { });
            if (filter != null)
            {
                entityData.SetQueryFilter((LambdaExpression)filter);
            }
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : BaseEntity
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }
}
