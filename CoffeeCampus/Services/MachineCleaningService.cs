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
        public void AddMachineCleaningLog(int machineId, User ResponsiblePerson) // Parameter her
        {
            _ = _context.MachineCleanings.Add(new MachineCleaning
            {
                CleaningDateTime = DateTime.Now,
                ResponsiblePerson = ResponsiblePerson, // Korrekt her, med stort "R".
                CoffeeMachineID = machineId
            });
            _context.SaveChanges();
        }
        public List<MachineCleaning> GetAllMachineCleaningLogs() {
            return _context.MachineCleanings.ToList();
        }

        public List<MachineCleaning> GetMachineCleaningLogs(int machineId) {
            return _context.MachineCleanings.Where(mc => mc.CoffeeMachineID == machineId).ToList();
        }

        public DateTime? GetLastCleaningDate(int machineId) {
            return _context.MachineCleanings.Where(mc => mc.CoffeeMachineID == machineId).OrderByDescending(mc => mc.CleaningDateTime).FirstOrDefault()?.CleaningDateTime;
        }
    }
}