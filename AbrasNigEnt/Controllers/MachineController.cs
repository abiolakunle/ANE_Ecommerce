using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace AbrasNigEnt.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineRepository _machineRepository;

        public MachineController(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public ViewResult List()
        {
            var machines = _machineRepository.LoadAllWithBrand();
            return View(machines);
        }

        public ViewResult Machine(int id)
        {
            return View();
        }
    }
}
