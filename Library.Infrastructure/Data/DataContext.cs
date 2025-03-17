using Library.Domain.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<BorrowEnttity> Borrows { get; set; }
        public DbSet<PatronEntity> Patrons { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdminEntity>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<BookEntity>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<BorrowEnttity>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<PatronEntity>().HasQueryFilter(x => !x.IsDeleted);

            builder.Entity<PatronEntity>()
                   .HasMany(x => x.BorrowedBooks)
                   .WithOne(x => x.Patron)
                   .HasForeignKey(x => x.PatronId);
            builder.Entity<PatronEntity>()
                    .OwnsOne(x => x.Address);

            builder.Entity<BookEntity>()
                   .HasMany(x => x.BorrowedBooks)
                   .WithOne(x => x.Book)
                   .HasForeignKey(x => x.BookId);

            base.OnModelCreating(builder);
        }
    }
}
