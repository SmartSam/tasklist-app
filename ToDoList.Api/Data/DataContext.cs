using Microsoft.EntityFrameworkCore;
using ToDoList.Shared.Models;

namespace ToDoList.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ToDoItem>()
            //.Property(p => p.Id)
            //.ValueGeneratedOnAdd();
        }
    }
}
