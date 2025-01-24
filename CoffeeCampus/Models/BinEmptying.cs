using System;
using CoffeeCampus.Models;
using System.ComponentModel.DataAnnotations; 


namespace CoffeeCampus.Models
{
    public class BinEmptying
    {
        [Key] 
        public int EmptyingID { get; set; }
        public DateTime DateTime { get; set; }
        public string Responsible { get; set; }

        public int CoffeeMachineID { get; set; } 
        public CoffeeMachine CoffeeMachine { get; set; } 
    }
}