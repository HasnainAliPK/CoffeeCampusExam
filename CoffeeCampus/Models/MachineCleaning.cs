namespace CoffeeCampus.Models
{
    public class MachineCleaning
    {
        public int Id { get; set; }
        public int CoffeeMachineID { get; set; }
        public CoffeeMachine CoffeeMachine { get; set; }

        public DateTime CleaningDateTime { get; set; }

        // Relation til User
        public string ResponsiblePersonId { get; set; }
        public User ResponsiblePerson { get; set; }

        public DateTime? ReminderSentDateTime { get; set; }
    }
}
