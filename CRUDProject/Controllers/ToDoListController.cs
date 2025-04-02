using CRUDProject.Data;
using CRUDProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDProject.Controllers
{

	public class ToDoListController : Controller
	{

		private readonly ApplicationDBContext _db;

		public ToDoListController(ApplicationDBContext db)
		{
			_db = db;
		}

		// GET: ToDoListController
		public ActionResult Index()
		{
			//IEnumerable<ToDoList> obj = _db.ToDoLists;
			var model = new ToDoListViewModel()
			{
				ToDoLists = _db.ToDoLists.OrderByDescending(t => t.CreatedAt).ToList()
			};

			return View(model);
		}
		// POST: ToDoListController/Create

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ToDoList obj)
		{
			if (ModelState.IsValid)
			{
				_db.ToDoLists.Add(obj);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			var viewModel = new ToDoListViewModel
			{
				ToDoLists = _db.ToDoLists.ToList(), // โหลดข้อมูลล่าสุด
				Task = obj.Task,                    // คงค่า Task ที่ผู้ใช้กรอก
				Description = obj.Description       // คงค่า Description ที่ผู้ใช้กรอก
			};
			return View("Index", viewModel); // คืนค่า view เดิมพร้อม error
		}

		// GET: ToDoListController/Edit/5

		public IActionResult Edit(int? id)
		{
			if(id == null || id == 0)
			{
				return NotFound();
			}
			var obj = _db.ToDoLists.Find(id);
			if(obj == null)
			{
				return NotFound();
			}

			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ToDoList obj)
		{
			if (ModelState.IsValid)
			{
				_db.ToDoLists.Update(obj);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		public IActionResult Delete(int? id)
		{
			if(id == 0 || id == null)
			{
				return NotFound();
			}
			var obj = _db.ToDoLists.Find(id);
			if(obj == null)
			{
				return NotFound();
			}
			_db.ToDoLists.Remove(obj);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult ToggleStatus(int? id)
		{
			if(id == 0||id == null)
			{
				return NotFound();
			}
			var obj = _db.ToDoLists.Find(id);
			if(obj != null)
			{
				obj.Status = obj.Status == "Pending" ? "Completed" : "Pending";
				obj.UpdatedAt = DateTime.Now;
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			
			return RedirectToAction("Index");
		}
	}
}
