using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Odh.BooksDemo.Entities;

namespace Odh.BooksDemo.Data
{
    public class ElmahErrorConfiguration : EntityTypeConfiguration<ElmahError>
    {
        public ElmahErrorConfiguration()
        {
            this.ToTable("ELMAH_Error");
            // Primary Key
            this.HasKey(t => t.ErrorId);

            // Properties
            this.Property(t => t.Application)
                 .IsRequired()
                 .HasMaxLength(60);

            this.Property(t => t.Host)
                 .IsRequired()
                 .HasMaxLength(50);

            this.Property(t => t.Type)
                 .IsRequired()
                 .HasMaxLength(100);

            this.Property(t => t.Source)
                 .IsRequired()
                 .HasMaxLength(60);

            this.Property(t => t.Message)
                 .IsRequired()
                 .HasMaxLength(500);

            this.Property(t => t.User)
                 .IsRequired()
                 .HasMaxLength(50);

            this.Property(t => t.Sequence)
                 .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AllXml)
                 .IsRequired();
        }
    }
}
