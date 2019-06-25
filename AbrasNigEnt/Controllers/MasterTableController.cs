using System.IO;
using System.Threading.Tasks;
using OfficeOpenXml;
using AbrasNigEnt.Data.Models;
using System.Collections.Generic;
using AbrasNigEnt.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AbrasNigEnt.Controllers
{
    public class MasterTableController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly AppDbContext _dbContext;

        public MasterTableController(IHostingEnvironment environment, AppDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadMaster()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadMaster([FromForm] IFormFile masterFile)
        {
            if (masterFile == null || masterFile.Length == 0)
            {
                return Content("File not selected");
            }

            //check if file extension is excel
            string fileExtension = Path.GetExtension(masterFile.FileName);

            //Validate uploaded file and return error
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ViewBag.Message = "Please select the excel file with .xls or .xlsx extension";
                return View();
            }

            //Generate full filpath and save file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _environment.WebRootPath, masterFile.FileName);

            using (var bits = new FileStream(filePath, FileMode.Create))
            {
                await masterFile.CopyToAsync(bits);
            }

            FileInfo file = new FileInfo(filePath);

            //dotnet add package EPPlus.Core--version 1.5.4
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                int totalRows = workSheet.Dimension.Rows;

                List<MasterTableLine> masterLines = new List<MasterTableLine>();

                string section = null;
                string sectionGroup = null;
                string modelName = null;

                for (int i = 1; i <= totalRows; i++)
                {
                    int next = i + 1;

                    
                    
                    
                    bool sectionBool = workSheet.Cells[i, 3].Style.Font.Bold;
                    bool sectionGroupBool = workSheet.Cells[next, 3].Style.Font.Bold;
                    if (sectionBool && sectionGroupBool)
                    {
                        modelName = workSheet.Cells[i-1, 1].Value != null ? workSheet.Cells[i-1, 1].Value.ToString() : modelName;
                        section = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString() : "";
                        sectionGroup = workSheet.Cells[next, 3].Value != null ? workSheet.Cells[next, 3].Value.ToString() : "";
                    }

                    if (sectionBool && !sectionGroupBool)
                    {
                        sectionGroup = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString() : "";
                    }

                    bool containsPart = workSheet.Cells[i, 4].Value != null;

                    if(!sectionBool && containsPart)
                    {
                        masterLines.Add(new MasterTableLine
                        {
                            ModelName = modelName,
                            SerialNumber = workSheet.Cells[i, 6].Value != null ? workSheet.Cells[i, 6].Value.ToString() : "",
                            Section = section,
                            SectionGroup = sectionGroup,
                            PartName = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString() : "",
                            PartNumber = workSheet.Cells[i, 4].Value != null ? workSheet.Cells[i, 4].Value.ToString() : "",
                            Quantity = workSheet.Cells[i, 5].Value != null ? Convert.ToInt32(workSheet.Cells[i, 5].Value.ToString()) : 0,
                            Remarks = workSheet.Cells[i, 7].Value != null ? workSheet.Cells[i, 7].Value.ToString() : "",
                            //Brand = workSheet.Cells[i, 10].Value != null ? workSheet.Cells[i, 10].Value.ToString() : "",
                        });
                    }
                    

                }

                


                //get data to db
                //foreach (var line in masterLines)
                //{
                //    if (false
                //        //line.Brand != ""
                //        )
                //    {
                //        Brand brand = new Brand
                //        {
                //            Name = line.Brand
                //        };
                //        _dbContext.Upsert(brand).On(b => new { b.Name }).Run();
                //        brand = _dbContext.Brands.Where(m => m.Name == line.Brand).FirstOrDefault();

                //        if (line.ModelName != "")
                //        {
                //            Machine machine = new Machine
                //            {
                //                ModelName = line.ModelName,
                //                SerialNumber = line.SerialNumber,
                //                BrandId = brand.BrandId
                //            };

                //            _dbContext.Upsert(machine).On(m => new { m.ModelName }).Run();
                //            machine = _dbContext.Machines.Where(m => m.ModelName == line.ModelName).FirstOrDefault();


                //            if (line.Section != "")
                //            {
                //                Section section = new Section
                //                {
                //                    SectionName = line.Section
                //                };
                //                _dbContext.Sections.Upsert(section).On(s => new { s.SectionName }).Run();
                //                section = _dbContext.Sections.Where(s => s.SectionName == line.Section).FirstOrDefault();

                //                section.Machines.Add(machine);
                //                machine.Sections.Add(section);
                //                _dbContext.SaveChanges();


                //                if (line.SectionGroup != "")
                //                {
                //                    SectionGroup sectionGroup = new SectionGroup
                //                    {
                //                        SectionGroupName = line.SectionGroup,
                //                        SectionId = section.SectionId
                //                    };

                //                    _dbContext.SectionGroups.Upsert(sectionGroup).On(s => new { s.SectionGroupName }).Run();
                //                    sectionGroup = _dbContext.SectionGroups.Where(s => s.SectionGroupName == line.SectionGroup).FirstOrDefault();

                //                    sectionGroup.Machines.Add(machine);
                //                    machine.SectionGroups.Add(sectionGroup);
                //                    _dbContext.SaveChanges();

                //                    if(line.PartName != "")
                //                    {
                //                        Category category = new Category
                //                        {
                //                            CategoryName = line.PartName,
                //                        };

                //                        _dbContext.Categories.Upsert(category).On(s => new { s.CategoryName }).Run();
                //                        category = _dbContext.Categories.Where(s => s.CategoryName == line.PartName).FirstOrDefault();

                //                        brand.Categories.Add(category);
                //                        _dbContext.SaveChanges();

                //                        if(line.PartNumber != null)
                //                        {
                //                            Product product = new Product
                //                            {
                //                                PartNumber = line.PartNumber,
                //                                BrandId = brand.BrandId,
                //                                CategoryId = category.CategoryId,
                //                                Quantity = line.Quantity,
                //                                Remarks = line.Remarks
                //                            };

                //                            _dbContext.Products.Upsert(product).On(s => new { s.PartNumber }).Run();
                //                            product = _dbContext.Products.Where(s => s.PartNumber == line.PartNumber).FirstOrDefault();

                //                            brand.Products.Add(product);
                //                            section.Products.Add(product);
                //                            sectionGroup.Products.Add(product);
                //                            category.Products.Add(product);
                //                            _dbContext.SaveChanges();

                //                        }

                //                    }
                //                }
                //            }

                            
                //        }
                //    }

                //}


                return View(nameof(UploadMaster), masterLines);

                //List<Machine> machineList = new List<Machine>();

                //for (int i = 2; i <= totalRows; i++)
                //{
                //    machineList.Add(new Machine
                //    {
                //        //BrandId = Convert.ToInt32(workSheet.Cells[i, 1].Value.ToString()),
                //        ModelName = workSheet.Cells[i, 2].Value.ToString(),
                //        Description = workSheet.Cells[i, 3].Value.ToString(),
                //        BrandId = Convert.ToInt32(workSheet.Cells[i, 4].Value.ToString())
                //    });
                //}

                //foreach (var machine in machineList)
                //{
                //    //dotnet add package FlexLabs.EntityFrameworkCore.Upsert--version 2.1.2
                //    _dbContext.Upsert(machine).On(b => new { b.ModelName }).Run();
                //}



            }
        }
    }
}
