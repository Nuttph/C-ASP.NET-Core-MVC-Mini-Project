using CRUDProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDProject.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) { }

		public DbSet<ToDoList> ToDoLists { get; set; }

		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries<ToDoList>())
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreatedAt = DateTime.UtcNow; // ตั้งค่า CreatedAt ตอนเพิ่มใหม่
				}
				else if (entry.State == EntityState.Modified)
				{
					entry.Entity.UpdatedAt = DateTime.UtcNow; // ตั้งค่า UpdatedAt ตอนแก้ไข
				}
			}
			return base.SaveChanges();
		}
	}
}
