using Application.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Branch)
                .WithMany()
                .HasForeignKey(b => b.BranchId);

            modelBuilder.Entity<Author>()
              .HasKey(r => r.Id);

            modelBuilder.Entity<Genre>()
              .HasKey(r => r.Id);

            modelBuilder.Entity<Branch>()
              .HasKey(r => r.Id);

            modelBuilder.Entity<Loan>()
               .HasKey(r => r.Id);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book) 
                .WithMany()
                .HasForeignKey(l => l.BookId);

            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany()
                .HasForeignKey(r => r.BookId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
