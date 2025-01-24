using CoffeeCampus.Data;
using CoffeeCampus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeCampus.Services
{
    public class CoffeeMachineStatisticsService
    {
        private readonly CoffeeCampusDbContext _context;

        public CoffeeMachineStatisticsService(CoffeeCampusDbContext context) //Konstruktor 
        {
            _context = context;
        }

        public void AddRefillLog(int machineId, int amount, DateTime dateTime) //Metode til at tilføje en ny refill log
        {
            _ = _context.Refills.Add(new Refill
            {
                RefillDate = DateTime.Now,
                CoffeeMachineID = machineId,
                RefillAmount = amount
            });
            _context.SaveChanges();
        }

        public List<Refill> GetRefillLogs(int machineId) //henter refillLog fra bestemt maskine
        {
            return _context.Refills.Where(r => r.CoffeeMachineID == machineId).ToList(); //retunere en list over refillLogs fra en bestemt maskine
        }

        public List<Refill> GetAllRefillLogs() //henter alle refillLogs fra listen
        {
            return _context.Refills.ToList(); //retunere den opdaterede liste 
        }

        public double GetTotalCoffeeUsage(int machineId, DateTime startDate, DateTime endDate) //Henter oplysning for kaffebruget af en bestem maskine
        {
            return _context.Refills.Where(r => r.CoffeeMachineID == machineId && r.RefillDate >= startDate && r.RefillDate <= endDate).Sum(r => r.RefillAmount); //Returnere hele kaffebruget af en bestem maskine

        }

        public double GetTotalCoffeeUsage(int machineId, TimeSpan timeSpan) //Metode til at se hvor meget kaffebrug for en bestem periode
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.Subtract(timeSpan);
            return GetTotalCoffeeUsage(machineId, startDate, endDate);
        }

        public double GetTotalCoffeeUsage(int machineId, string interval) //Mulige perioder
        {
            switch (interval.ToLower())
            {
                case "daily":
                    return GetTotalCoffeeUsage(machineId, TimeSpan.FromDays(1));
                case "weekly":
                    return GetTotalCoffeeUsage(machineId, TimeSpan.FromDays(7));
                case "monthly":
                    return GetTotalCoffeeUsage(machineId, TimeSpan.FromDays(30));
                case "yearly":
                    return GetTotalCoffeeUsage(machineId, TimeSpan.FromDays(365));
                default:
                    throw new ArgumentException("Invalid Interval type");
            }
        }
    }
}