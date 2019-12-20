using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPICore.Model;

namespace WebAPICore.DBContext
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
        {
        }
        public DbSet<Demo> Demos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //EntityTypeBuilder<BookChapter> chapter = modelBuilder.Entity<BookChapter>();
            //chapter.ToTable("Chapters").HasKey(p => p.Id);
            //chapter.Property<Guid>(p => p.Id).HasColumnType("UniqueIdentifier").HasDefaultValueSql("newid()");
            //chapter.Property<string>(p => p.Title).HasMaxLength(120);
        }
    }
}
