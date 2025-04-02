using System.ComponentModel.DataAnnotations;

namespace CRUDProject.Models
{
	public class ToDoListViewModel
	{
		public List<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();

		[Required]
		public string Task { get; set; }

		public string Description { get; set; }

		[Required]
		public string Status { get; set; }
	}
}
