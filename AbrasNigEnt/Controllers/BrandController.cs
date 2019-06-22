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

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public ViewResult Update(int id)
        {
            var brand = _brandRepository.GetById(id);

            return View(brand);
        }

        [HttpPost]
        public IActionResult Update(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }

            _brandRepository.Update(brand);

            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(int id)
        {
            var brand = _brandRepository.GetById(id);

            _brandRepository.Delete(brand);

            return RedirectToAction(nameof(List));
        }
    }
}
