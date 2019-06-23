using AbrasNigEnt.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _category;

        public CategoryMenu(ICategoryRepository category)
        {
            _category = category;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _category.GetAll().OrderBy(c => c.CategoryName);
            return View(categories);
        }

    }
}
