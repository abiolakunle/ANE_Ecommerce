using AbrasNigEnt.Data;
using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.Data.Models;
using AbrasNigEnt.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AbrasNigEnt.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public ViewResult List()
        {
            var products = _productRepository.LoadAllWithCategoryAndBrand();

            return View(products);
        }


        public ViewResult Create()
        {
            var viewModel = new ManageProductViewModel
            {
                Product = new Product(),
                Brands = _brandRepository.GetAll(),
                Categories = _categoryRepository.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(ManageProductViewModel manageProductViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(manageProductViewModel);
            }

            _productRepository.Create(manageProductViewModel.Product);

            return RedirectToAction("List");
        }


    }
}
