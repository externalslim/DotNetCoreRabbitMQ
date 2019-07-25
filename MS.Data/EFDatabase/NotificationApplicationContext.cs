using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MS.Data.EFDatabase
{
    public partial class NotificationApplicationContext : DbContext
    {
        public NotificationApplicationContext()
        {
        }

        public NotificationApplicationContext(DbContextOptions<NotificationApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<Mail> Mail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=xx.xx.xx.xx;Database=NotificationApplication;User ID=sa;Password=xx;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.MailParameter).IsRequired();

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.StackTrace).IsRequired();
            });

            modelBuilder.Entity<Mail>(entity =>
            {
                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.IsSend).HasDefaultValueSql("((0))");

                entity.Property(e => e.MailBcc)
                    .HasColumnName("MailBCC")
                    .HasMaxLength(500);

                entity.Property(e => e.MailCc)
                    .HasColumnName("MailCC")
                    .HasMaxLength(500);

                entity.Property(e => e.MailFrom)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.MailTo)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
