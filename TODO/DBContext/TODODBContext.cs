using Microsoft.EntityFrameworkCore;
using TODO.DBSchemas;

namespace TODO.DBContext
{
    public class TODODBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public TODODBContext(DbContextOptions<TODODBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to tables  
            modelBuilder.Entity<User>().ToTable("Users");

            // Configure Primary Keys  
            modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("PK_Users");

            // Configure columns  
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UserName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.CreationDateTime).HasColumnType("datetime").IsRequired();
        }
    }
}
