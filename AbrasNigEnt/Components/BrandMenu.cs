using AbrasNigEnt.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AbrasNigEnt.Components
{
    public class BrandMenu : ViewComponent
    {
        private readonly IBrandRepository _brandrepository;

        public BrandMenu(IBrandRepository brandRepository)
        {
            _brandrepository = brandRepository;
        }

        public IViewComponentResult Invoke()
        {
            var brands = _brandrepository.GetAll();
            return View(brands);
        }
    }
}
