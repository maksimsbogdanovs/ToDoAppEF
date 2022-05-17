using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Context
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoListModel>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<ToDoApp.Models.ToDoListModel>? ToDoListModel { get; set; }
    }
}
