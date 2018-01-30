using System.Collections.Generic;
using System.Linq;
using myAppApi.Models;

namespace myAppApi.Data{
    public static class DbInitializer{
        public static void initialize(TodoDbContext db){
            db.Database.EnsureCreated();

            if(db.TodoItem.Any()){
                return;
            }

            var todoItem = new List<TodoItem>(){
                new TodoItem{  Title="todo 1", Description="description 1"  }
            };

            db.TodoItem.AddRange(todoItem);
            db.SaveChanges();
        }
    }
}