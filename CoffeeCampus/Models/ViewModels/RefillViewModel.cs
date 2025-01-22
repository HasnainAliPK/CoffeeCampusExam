namespace CoffeeCampus.Models.ViewModels
{
    public class RefillViewModel
    {
        public int RefillID { get; set; }
        public DateTime DateTime { get; set; }
        public string RefillType { get; set; }
        public string Responsible { get; set; }  // Responsible person's name
        public int CoffeeMachineId { get; set; }
        public string CoffeeMachineName { get; set; } // Added for the coffee machine name
    }
}
