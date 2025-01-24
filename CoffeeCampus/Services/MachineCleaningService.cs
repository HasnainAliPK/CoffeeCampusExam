using CoffeeCampus.Data;
using CoffeeCampus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeCampus.Services
{
    public class MachineCleaningService
    {
        private readonly CoffeeCampusDbContext _context;

        public MachineCleaningService(CoffeeCampusDbContext context) {
            _context = context;
        }
        public void AddMachineCleaningLog(int machineId, User ResponsiblePerson) //Metode til at tilføje ny machineCleaningLog
        {
            _ = _context.MachineCleanings.Add(new MachineCleaning
            {
                CleaningDateTime = DateTime.Now,
                ResponsiblePerson = ResponsiblePerson, 
                CoffeeMachineID = machineId
            });
            _context.SaveChanges();
        }
        public List<MachineCleaning> GetAllMachineCleaningLogs() { //Henter alle MachineCleaningLog 
            return _context.MachineCleanings.ToList(); //Returner Liste af MC Log
        }

        public List<MachineCleaning> GetMachineCleaningLogs(int machineId) { //Henter alle MachineCleaninglog fra en bestem maskine
            return _context.MachineCleanings.Where(mc => mc.CoffeeMachineID == machineId).ToList(); //Returnere liste af MC Log fra den bestemt maskine
        }

        public DateTime? GetLastCleaningDate(int machineId) { //Henter den seneste dato for den seneste rengøring
            return _context.MachineCleanings.Where(mc => mc.CoffeeMachineID == machineId).OrderByDescending(mc => mc.CleaningDateTime).FirstOrDefault()?.CleaningDateTime; //Returnere den seneste rengøringsdato eller null hvis der ikke er nogen
        }
    }
}