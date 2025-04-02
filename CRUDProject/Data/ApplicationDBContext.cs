using CRUDProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDProject.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) { }

		public DbSet<ToDoList> ToDoLists { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ToDoList>()
				.Property(t => t.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			modelBuilder.Entity<ToDoList>()
				.Property(t => t.UpdatedAt)
				.IsRequired(false); // ✅ อนุญาตให้เป็น NULL
		}


		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries<ToDoList>())
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreatedAt = DateTime.UtcNow;
					entry.Entity.UpdatedAt = DateTime.UtcNow;
				}
				else if (entry.State == EntityState.Modified)
				{
					entry.Entity.UpdatedAt = DateTime.UtcNow;
				}
			}
			return base.SaveChanges();
		}
	}
}
