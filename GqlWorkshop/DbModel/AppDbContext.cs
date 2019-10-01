using Microsoft.EntityFrameworkCore;

namespace GqlWorkshop.DbModel
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quote>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Quote);
        }
    }
}