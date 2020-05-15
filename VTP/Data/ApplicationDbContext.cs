using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VTP.Models;

namespace VTP.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocumentTemplateData>()
                .HasKey(t => new { t.DocumentId, t.TemplateDataId});

            modelBuilder.Entity<DocumentTemplateData>()
                .HasOne(sc => sc.Document)
                .WithMany(s => s.DocumentTemplateDatas)
                .HasForeignKey(sc => sc.DocumentId);

            modelBuilder.Entity<DocumentTemplateData>()
                .HasOne(sc => sc.TemplateData)
                .WithMany(c => c.DocumentTemplateDatas)
                .HasForeignKey(sc => sc.TemplateDataId);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentTemplateData> DocumentTemplateDatas { get; set; }

        public DbSet<TemplateData> TemplateDatas { get; set; }
    }
}
