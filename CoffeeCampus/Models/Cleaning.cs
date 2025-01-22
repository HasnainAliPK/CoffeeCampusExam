namespace CoffeeCampus.Models
{
    public class Cleaning
    {
        public int CleaningID { get; set; }
        public DateTime DateTime { get; set; }
        public string Responsible { get; set; }

        public int CoffeeMachineID { get; set; }

        public CoffeeMachine CoffeeMachine { get; set; }

    }
}
