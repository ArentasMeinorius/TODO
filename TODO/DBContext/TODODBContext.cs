using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TODO.DBSchemas;
using Constants = TODO.Helpers.Constants;

namespace TODO.DBContext
{
    public class TODODBContext : DbContext, ITODODBContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }
        public DbSet<TodoList> Lists { get; set; }
        private IPasswordHasher m_hasher { get; set; }
        private Random m_random { get; set; }

        public TODODBContext(DbContextOptions<TODODBContext> options) : base(options)
        {
            m_hasher = new PasswordHasher();
            m_random = new Random();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to tables  
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<TodoTask>().ToTable("Tasks");
            modelBuilder.Entity<TodoList>().ToTable("Lists");

            // Configure Primary Keys  
            modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("PK_Users");
            modelBuilder.Entity<TodoTask>().HasKey(t => t.Id).HasName("PK_Tasks");
            modelBuilder.Entity<TodoList>().HasKey(l => l.Id).HasName("PK_Lists");

            // Configure columns  
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UserName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PassWord).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.CreationDateTime).HasColumnType("datetime").IsRequired().HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<User>().Property(u => u.ListId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<TodoTask>().Property(t => t.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<TodoTask>().Property(t => t.Name).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<TodoTask>().Property(t => t.Description).HasColumnType("nvarchar(100)").IsRequired(false);
            modelBuilder.Entity<TodoTask>().Property(t => t.Completed).HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
            modelBuilder.Entity<TodoTask>().Property(t => t.ListId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<TodoList>().Property(l => l.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<TodoList>().Property(l => l.Name).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<TodoList>().Property(l => l.Description).HasColumnType("nvarchar(100)").IsRequired(false);
            modelBuilder.Entity<TodoList>().Property(l => l.UserId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<TodoList>()
                .HasMany(x => x.Tasks);

            modelBuilder.Entity<User>()
                .HasOne(x => x.List);

            CreateData(modelBuilder);
        }

        private User CreateUser(int id, bool isAdmin = false)
        {
            var user = isAdmin ? new Admin() : new User();
            var index = m_random.Next(0, Constants.UserNames.Length);
            var name = Constants.UserNames[index] + id;
            user.UserName = name;
            user.PassWord = m_hasher.HashPassword(name);
            user.Id = id;
            user.ListId = id;
            return user;
        }

        private TodoList CreateList(int id)
        {
            var list = new TodoList();
            var index = m_random.Next(0, Constants.ListNames.Length);
            list.Name = Constants.ListNames[index];
            list.Id = id;
            list.UserId = id;
            list.Tasks = new Collection<TodoTask> { };
            return list;
        }

        private TodoTask CreateTask(int listId, int id)
        {
            var task = new TodoTask();
            task.Id = id;
            task.ListId = listId;
            task.UserId = listId;
            var index = m_random.Next(0, Constants.TaskNames.Length);
            task.Name = Constants.TaskNames[index];
            return task;
        }
        public void CreateData(ModelBuilder modelBuilder)
        {
            int userCounter = 0, taskCounter = 0;
            var userList = new List<User>
            {
                CreateUser(++userCounter),
            };
            var listList = new List<TodoList>
            {
               CreateList(userCounter),
            };
            var taskList = new List<TodoTask>
            {
               CreateTask(userCounter, ++taskCounter),
               CreateTask(userCounter, ++taskCounter),
               CreateTask(userCounter, ++taskCounter),
            };

            userList.Add(CreateUser(++userCounter));
            listList.Add(CreateList(userCounter));
            taskList.AddRange(new Collection<TodoTask>
            {
                CreateTask(userCounter, ++taskCounter),
                CreateTask(userCounter, ++taskCounter),
                CreateTask(userCounter, ++taskCounter),
            });

            var admin = CreateUser(++userCounter, true);
            listList.Add(CreateList(userCounter));
            taskList.AddRange(new Collection<TodoTask>
            {
                CreateTask(userCounter, ++taskCounter),
                CreateTask(userCounter, ++taskCounter),
                CreateTask(userCounter, ++taskCounter),
            });

            modelBuilder.Entity<User>().HasData(userList);
            modelBuilder.Entity<TodoList>().HasData(listList);
            modelBuilder.Entity<TodoTask>().HasData(taskList);
            modelBuilder.Entity<Admin>().HasData(admin);
        }
    }
}
