using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CoffeeCampus.Pages //ikke i brug
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger; //ILogger kan give besked tilbage f.eks. fejlfinding

        public PrivacyModel(ILogger<PrivacyModel> logger) { //Konstruktor
            _logger = logger;
        }

        public void OnGet() //Top onGet metode
        {
        }
    }

}
