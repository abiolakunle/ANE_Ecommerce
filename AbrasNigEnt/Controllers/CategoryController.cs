using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbrasNigEnt.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
            var categoryList = _categoryRepository.GetAll();
            return View(categoryList);
        }

        [HttpGet]
        public ViewResult Create()
        {
            var category = new Category();

            return View(category);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _categoryRepository.Create(category);

            return RedirectToAction("List");
        }

    }
}
