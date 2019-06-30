using AbrasNigEnt.Data;
using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AbrasNigEnt.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineRepository _machineRepository;
        private readonly ISectionGroupRepository _sectionGroupRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly AppDbContext _dbContext;

        public MachineController(IMachineRepository machineRepository, AppDbContext dbContext, 
            ISectionGroupRepository sectionGroupRepository, ISectionRepository sectionRepository
            )
        {
            _machineRepository = machineRepository;
            _dbContext = dbContext;
            _sectionGroupRepository = sectionGroupRepository;
            _sectionRepository = sectionRepository;
        }

        public ViewResult List()
        {
            var machines = _machineRepository.LoadAllWithBrand();
            
            return View(machines);
        }

        public ViewResult Machine(int id)
        {
            var machine = _machineRepository.LoadWithBrandSection(id);
            MachineViewModel machineViewModel = new MachineViewModel
            {
                Machine = machine,
                SectionGroups = machine.SectionGroups,
                Sections = machine.Sections
            };

            return View(machineViewModel);
        }
    }
}
