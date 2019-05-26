using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbrasNigEnt.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public ViewResult List()
        {
            var brandList = _brandRepository.GetAll();

            return View(brandList);
        }

        [HttpGet]
        public ViewResult Create()
        {
            var brand = new Brand();

            return View(brand);
        }

        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }

            _brandRepository.Create(brand);

            return RedirectToAction("List");
        }
    }
}
