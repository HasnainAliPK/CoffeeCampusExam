using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace CoffeeCampus.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] //Bruges til at kontrollere hvor længe og hvordan svaret gemmes i cache 
    [IgnoreAntiforgeryToken] //Attribut til at deaktivere validering
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger; //ILogger kan give besked tilbage f.eks. fejlfinding

        public ErrorModel(ILogger<ErrorModel> logger) //Konstruktor
        {
            _logger = logger;
        }

        public void OnGet() //Metode til at finde aktivitet og hvis ingen aktivitet er fundet laver den HttpContext.TraceIdentifier
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }

}
