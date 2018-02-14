
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace aspnetidentityserver.Models{
    public class TodoDbContext : DbContext{
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
            
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<MyUser> MyUsers { get; set; }
        public DbSet<MyRole> MyRoles { get; set; }
        public DbSet<MyUserRole> MyUserRoles { get; set; }

    }

    public class TodoItem{
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }

    public class MyUser
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
    public class MyRole
    {
        [Key]
        public int ID { get; set; }
        public string Role { get; set; }

    }
    public class MyUserRole
    {
        [Key]
        public int ID { get; set; }
        public MyUser MyUser { get; set; }
        public MyRole MyRole { get; set; }
    }
}