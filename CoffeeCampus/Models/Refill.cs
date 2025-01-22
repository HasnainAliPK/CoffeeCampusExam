using CoffeeCampus.Models;
using System.Reflection.PortableExecutable;
namespace CoffeeCampus.Models
{
    public class Refill
    {
        public int RefillID { get; set; }
        public DateTime RefillDate { get; set; }
        public string RefillType { get; set; } // Kaffe, Mælkepulver, Sukker
        public int CoffeeMachineID { get; set; }
        public string UserId { get; set; } // Admin eller Bruger
        public virtual CoffeeMachine CoffeeMachine { get; set; }
        public virtual User User { get; set; }

        public string Responsible { get; set; } // Hvem der har gjort det//slime
        public int RefillAmount { get; set; }
    }
}
