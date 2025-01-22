using System;
using CoffeeCampus.Models;
using System.ComponentModel.DataAnnotations; //Add this using


namespace CoffeeCampus.Models
{
    public class BinEmptying
    {
        [Key] //This specifies the primary key
        public int EmptyingID { get; set; }
        public DateTime DateTime { get; set; }
        public string Responsible { get; set; }

        public int CoffeeMachineID { get; set; } //Foreign Key
        public CoffeeMachine CoffeeMachine { get; set; } //Navigation Property
    }
}