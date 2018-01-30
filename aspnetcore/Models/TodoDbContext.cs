
using Microsoft.EntityFrameworkCore;

namespace myAppApi.Models{
    public class TodoDbContext : DbContext{
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
            
        }

        public DbSet<TodoItem> TodoItem { get; set; }

    }

    public class TodoItem{
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}