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

        public CoffeeMachineStatisticsService(CoffeeCampusDbContext context)
        {
            _context = context;
        }

        public void AddRefillLog(int machineId, int amount, DateTime dateTime)
        {
            _ = _context.Refills.Add(new Refill
            {
                RefillDate = DateTime.Now,
                CoffeeMachineID = machineId,
                RefillAmount = amount
            });
            _context.SaveChanges();
        }

        public List<Refill> GetRefillLogs(int machineId)
        {
            return _context.Refills.Where(r => r.CoffeeMachineID == machineId).ToList();
        }

        public List<Refill> GetAllRefillLogs()
        {
            return _context.Refills.ToList();
        }

        public double GetTotalCoffeeUsage(int machineId, DateTime startDate, DateTime endDate)
        {
            return _context.Refills.Where(r => r.CoffeeMachineID == machineId && r.RefillDate >= startDate && r.RefillDate <= endDate).Sum(r => r.RefillAmount);

        }

        public double GetTotalCoffeeUsage(int machineId, TimeSpan timeSpan)
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.Subtract(timeSpan);
            return GetTotalCoffeeUsage(machineId, startDate, endDate);
        }

        public double GetTotalCoffeeUsage(int machineId, string interval)
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