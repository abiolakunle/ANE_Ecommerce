using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using OfficeOpenXml;
using AbrasNigEnt.Data.Models;
using System.Collections.Generic;
using AbrasNigEnt.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace AbrasNigEnt.Controllers
{
    public class ExcelToDbController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly AppDbContext _dbContext;

        public ExcelToDbController(IHostingEnvironment environment, AppDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;

        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Machine([FromForm] IFormFile machinesFile)
        {
            if (machinesFile == null || machinesFile.Length == 0)
            {
                return Content("File not selected");
            }

            //check if file extension is excel
            string fileExtension = Path.GetExtension(machinesFile.FileName);

            //Validate uploaded file and return error
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ViewBag.Message = "Please select the excel file with .xls or .xlsx extension";
                return View();
            }

            //Generate full filpath and save file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _environment.WebRootPath, machinesFile.FileName);

            using (var bits = new FileStream(filePath, FileMode.Create))
            {
                await machinesFile.CopyToAsync(bits);
            }

            FileInfo file = new FileInfo(filePath);

            //dotnet add package EPPlus.Core--version 1.5.4
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                int totalRows = workSheet.Dimension.Rows;

                List<Machine> machineList = new List<Machine>();

                for (int i = 2; i <= totalRows; i++)
                {
                    machineList.Add(new Machine
                    {
                        //BrandId = Convert.ToInt32(workSheet.Cells[i, 1].Value.ToString()),
                        ModelName = workSheet.Cells[i, 2].Value.ToString(),
                        Description = workSheet.Cells[i, 3].Value.ToString(),
                        BrandId = Convert.ToInt32(workSheet.Cells[i, 4].Value.ToString()) 
                    });
                }

                foreach (var machine in machineList)
                {
                    //dotnet add package FlexLabs.EntityFrameworkCore.Upsert--version 2.1.2
                     _dbContext.Upsert(machine).On(b => new { b.ModelName}).Run();
                }

                return View(nameof(Index));

            }
        }

        //*******************************

        [HttpPost]
        public async Task<IActionResult> Brand([FromForm] IFormFile brandsFile)
        {
            if (brandsFile == null || brandsFile.Length == 0)
            {
                return Content("File not selected");
            }

            //check if file extension is excel
            string fileExtension = Path.GetExtension(brandsFile.FileName);

            //Validate uploaded file and return error
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ViewBag.Message = "Please select the excel file with .xls or .xlsx extension";
                return View();
            }

            //Generate full filpath and save file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _environment.WebRootPath, brandsFile.FileName);

            using (var bits = new FileStream(filePath, FileMode.Create))
            {
                await brandsFile.CopyToAsync(bits);
            }

            FileInfo file = new FileInfo(filePath);

            //dotnet add package EPPlus.Core--version 1.5.4
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                int totalRows = workSheet.Dimension.Rows;

                List<Brand> brandList = new List<Brand>();

                for (int i = 2; i <= totalRows; i++)
                {
                    brandList.Add(new Brand
                    { 
                        Name = workSheet.Cells[i, 2].Value.ToString(),
                        Description = workSheet.Cells[i, 3].Value.ToString(),
                    });
                }

                foreach (var brand in brandList)
                {
                    //dotnet add package FlexLabs.EntityFrameworkCore.Upsert--version 2.1.2
                    _dbContext.Upsert(brand).On(e => new { e.Name }).Run();
                }

                return View(nameof(Index));

            }
        }

        private async Task<string> NameAndSave(IFormFile formFile)
        {

            return null;

            
        }
    }
}
