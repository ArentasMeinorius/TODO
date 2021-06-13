using Microsoft.EntityFrameworkCore;
using TODO.DBSchemas;

namespace TODO.DBContext
{
    public interface ITODODBContext
    {
        DbSet<Admin> Admins { get; set; }
        DbSet<TodoList> Lists { get; set; }
        DbSet<TodoTask> Tasks { get; set; }
        DbSet<User> Users { get; set; }

        void CreateData(ModelBuilder modelBuilder);
        int SaveChanges();
    }
}