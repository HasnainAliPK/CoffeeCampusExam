using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;


namespace CoffeeCampus.Services
{
    public class NotificationService
    {
        private readonly CoffeeCampusDbContext _context;


        public NotificationService(CoffeeCampusDbContext context) //Konstruktor
        {
            _context = context; //instansvariablen
        }


        public async Task<string> GetHoseChangeNotificationAsync(int coffeeMachineId) //Async gør den køre asynkront
        {
            var coffeeMachine = await _context.CoffeeMachines //Await, køre først efter den har fået coffemachine context
                .Include(m => m.HoseChanges) //Eager Loading = automatisk henter relaterede data fra database i en forespørgsel
                .FirstOrDefaultAsync(m => m.CoffeeMachineID == coffeeMachineId); //Returner CoffemachineID eller Null


            if (coffeeMachine == null) { return "Coffee machine not found"; }


            var lastChange = coffeeMachine.HoseChanges 
                 .OrderByDescending(hc => hc.ChangeDate) //Filtrere seneste til ældste dato 
                  .FirstOrDefault();


            if (lastChange == null)
            {
                return "Slangen skal skiftes! Denne er første gang."; 
            }


            DateTime nextChangeDate = lastChange.ChangeDate.AddMonths(3);//Notification bliver sat 3 måneder frem


            if (DateTime.Now >= nextChangeDate) //Hvis der er gået over 3 måneder = skal den skiftes
            {
                return "Slangen skal skiftes!";
            }
            else
            {
                TimeSpan timeRemaining = nextChangeDate - DateTime.Now;
                return $"Slangen skal skiftes om {timeRemaining.Days} dage"; //dage countdown 
            }
        }



        public async Task MarkHoseChangeAsync(int coffeeMachineId) /*Denne metode kontrollere om en kaffemaskine findes i systemet,
                                                                   registrere en ny post for et slangeskift og gemmer opdateringen i databasen*/
        {
            var coffeeMachine = await _context.CoffeeMachines.FirstOrDefaultAsync(m => m.CoffeeMachineID == coffeeMachineId);
            if (coffeeMachine == null) return;  

            var hoseChange = new HoseChange { ChangeDate = DateTime.Now, CoffeeMachineId = coffeeMachineId }; //
            _context.HoseChanges.Add(hoseChange); 

            await _context.SaveChangesAsync(); //Gemmer ændringer i database

        }
    }
}