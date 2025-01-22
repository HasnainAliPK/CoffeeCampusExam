using System;
using CoffeeCampus.Data;
using System.Linq;
using Microsoft.AspNetCore.Http; // For context
using System.Security.Claims; // For krav fra bruger

namespace CoffeeCampus.Services
{
    public class ServiceLogService
    {
        private readonly CoffeeCampusDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceLogService(CoffeeCampusDbContext context, IHttpContextAccessor httpContextAccessor) //Konstruktor
        {
            _context = context; //instansfelt i konstruktoren 
            _httpContextAccessor = httpContextAccessor; 
        }


        public DateTime? GetLastServiceDate() //Metode til at se den sidste service, hvornår den er blevet lavet
        {
            var logEntry = _context.ServiceLogs.OrderByDescending(s => s.ServiceDate).FirstOrDefault();
            return logEntry?.ServiceDate;
        }
        public string GetServiceBy() //Metode til at se hvem der har lavet den sidste service
        {
            var logEntry = _context.ServiceLogs.OrderByDescending(s => s.ServiceDate).FirstOrDefault();
            return logEntry?.ServiceBy;
        }

        public void LogService() //Metode til at finde ud af hvilken bruger der er logget ind og hvornår der er lavet service
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return;

            _context.ServiceLogs.Add(new Models.ServiceLog { ServiceDate = DateTime.Now, ServiceBy = userId }); 
            _context.SaveChanges(); //gemmer ændringer i database
        }
    }
}