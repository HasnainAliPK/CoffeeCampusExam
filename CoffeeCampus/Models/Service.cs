namespace CoffeeCampus.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public int CoffeeMachineID { get; set; }
        public CoffeeMachine CoffeeMachine { get; set; }
    };
}