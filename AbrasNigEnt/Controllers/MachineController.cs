using AbrasNigEnt.Data.Interfaces;
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
            var machine = _machineRepository.GetById(id);

            return View(machine);
        }
    }
}
