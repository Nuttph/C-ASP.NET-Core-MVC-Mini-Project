using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRUDProject.Models
{
	public class ToDoList
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please fill in the Task.")]
		public string Task { get; set; }

		[Required(ErrorMessage = "Please fill in the Description.")]
		public string Description { get; set; }

		[Required]
		public string Status { get; set; } = "Pending";

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public DateTime? UpdatedAt { get; set; }
	}
}
