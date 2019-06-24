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

                //loop through excel lines and copy items to list
                for(int i = 2; i <= totalRows; i++)
                {
                    masterLines.Add(new MasterTableLine
                    {
                        ModelName = workSheet.Cells[i, 1].Value != null ? workSheet.Cells[i, 1].Value.ToString() : ""  ,
                        SerialNumber = workSheet.Cells[i, 2].Value != null ? workSheet.Cells[i, 2].Value.ToString() : "",
                        Section = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString() : "",
                        SectionGroup = workSheet.Cells[i, 4].Value != null ? workSheet.Cells[i, 4].Value.ToString() : "",
                        PartName = workSheet.Cells[i, 5].Value != null ? workSheet.Cells[i, 5].Value.ToString() : "",
                        PartNumber = workSheet.Cells[i, 6].Value != null ? workSheet.Cells[i, 6].Value.ToString() : "",
                        Quantity = workSheet.Cells[i, 7].Value != null ? Convert.ToInt32(workSheet.Cells[i, 7].Value.ToString()) : 0,
                        Remarks = workSheet.Cells[i, 9].Value != null ? workSheet.Cells[i, 9].Value.ToString() : "",
                        Brand = workSheet.Cells[i, 10].Value != null ? workSheet.Cells[i, 10].Value.ToString() : "",
                    });
                }


                //get data to db
                foreach(var line in masterLines)
                {
                    if (line.Brand != "")
                    {
                        Brand brand = new Brand
                        {
                            Name = line.Brand
                        };
                        _dbContext.Upsert(brand).On(b => new { b.Name }).Run();
                        brand = _dbContext.Brands.Where(m => m.Name == line.Brand).FirstOrDefault();

                        if (line.ModelName != "")
                        {
                            Machine machine = new Machine
                            {
                                ModelName = line.ModelName,
                                SerialNumber = line.SerialNumber,
                                BrandId = brand.BrandId
                            };

                            _dbContext.Upsert(machine).On(m => new { m.ModelName }).Run();
                            machine = _dbContext.Machines.Where(m => m.ModelName == line.ModelName).FirstOrDefault();


                            if (line.Section != "")
                            {
                                Section section = new Section
                                {
                                    SectionName = line.Section
                                };
                                _dbContext.Sections.Upsert(section).On(s => new { s.SectionName }).Run();
                                section = _dbContext.Sections.Where(s => s.SectionName == line.Section).FirstOrDefault();

                                section.Machines.Add(machine);
                                machine.Sections.Add(section);
                                _dbContext.SaveChanges();


                                if (line.SectionGroup != "")
                                {
                                    SectionGroup sectionGroup = new SectionGroup
                                    {
                                        SectionGroupName = line.SectionGroup,
                                        SectionId = section.SectionId
                                    };

                                    _dbContext.SectionGroups.Upsert(sectionGroup).On(s => new { s.SectionGroupName }).Run();
                                    sectionGroup = _dbContext.SectionGroups.Where(s => s.SectionGroupName == line.SectionGroup).FirstOrDefault();

                                    sectionGroup.Machines.Add(machine);
                                    machine.SectionGroups.Add(sectionGroup);
                                    _dbContext.SaveChanges();
                                }
                            }

                            
                        }
                    }

                }


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
