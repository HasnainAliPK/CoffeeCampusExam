using System;

namespace CoffeeCampus.Models
{
    public class HoseChange
    {
        public int HoseChangeID { get; set; }
        public DateTime ChangeDate { get; set; }

        public int CoffeeMachineId { get; set; } // Foreign Key
        public CoffeeMachine CoffeeMachine { get; set; }

    }
}