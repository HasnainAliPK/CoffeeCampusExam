using System.ComponentModel.DataAnnotations;

namespace CoffeeCampus.Models
{
    public class CoffeeMachine
    {
        public int CoffeeMachineID { get; set; }
        public string Building { get; set; }

        public DateTime HoseChangeDate { get; set; }
        public DateTime ServiceDate { get; set; }

        public DateTime CleaningDate { get; set; }
        public DateTime RefillDate { get; set; }

        public List<Refill> Refills { get; set; }

        public List<Service> Services { get; set; }

        public List<HoseChange> HoseChanges { get; set; }
    }

}

