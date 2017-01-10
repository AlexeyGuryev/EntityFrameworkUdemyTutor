using System.Data.Entity.ModelConfiguration;

namespace FluentAPI.EntityConfigurations
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            ToTable("Courses");
            HasKey(c => c.Id);

            Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(255);

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(2000);

            HasRequired(c => c.Author)
                .WithMany(a => a.Courses)
                .HasForeignKey(c => c.AuthorId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.Cover)
                .WithRequiredPrincipal(c => c.Course);

            HasMany(c => c.Tags)
                .WithMany(t => t.Courses)
                .Map(m =>
                {
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("TagId");
                    m.ToTable("CourseTags");
                });
        }
    }
}
