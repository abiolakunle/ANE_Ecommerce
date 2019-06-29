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
        private readonly AppDbContext _dbContext;

        public MachineController(IMachineRepository machineRepository, AppDbContext dbContext)
        {
            _machineRepository = machineRepository;
            _dbContext = dbContext;
        }

        public ViewResult List()
        {
            var machines = _machineRepository.LoadAllWithBrand();
            return View(machines);
        }

        public ViewResult Machine(int id)
        {
            var machine = _machineRepository.LoadWithBrandSection(id);

            return View(machine);
        }
    }
}
