using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetCore_demo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepo;

        public CategoryController(ICategoryRepository db)
        {
            _CategoryRepo = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _CategoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _CategoryRepo.Add(obj);
                _CategoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /* return View();*/
            var categoryFromDb = _CategoryRepo.Get(i => i.Id == id);
            /*var categoryFromDbFirst = _db.Categories.FirstOrDefault(i => i.Id == id);
            var categoryFromDbSingle = _db.Categories.Single(i => i.Id == id);*/
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST                                           
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _CategoryRepo.Update(obj);
                _CategoryRepo.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /* return View();*/
            var categoryFromDb = _CategoryRepo.Get(i => i.Id == id);
            /*var categoryFromDbFirst = _db.Categories.FirstOrDefault(i => i.Id == id);
            var categoryFromDbSingle = _db.Categories.Single(i => i.Id == id);*/
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST                                           
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _CategoryRepo.Get(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _CategoryRepo.Remove(obj);
            _CategoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");


        }
    }
}
