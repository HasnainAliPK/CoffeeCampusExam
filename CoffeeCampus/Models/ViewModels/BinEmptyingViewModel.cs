using System;

namespace CoffeeCampus.Models.ViewModels
{
    public class BinEmptyingViewModel
    {
        public int EmptyingID { get; set; } //Added
        public DateTime DateTime { get; set; }
        public string Responsible { get; set; }
        public int CoffeeMachineID { get; set; }
    }
}