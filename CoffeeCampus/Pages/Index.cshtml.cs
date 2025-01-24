using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCampus.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; //ILogger kan give besked tilbage f.eks. fejlfinding

        public IndexModel(ILogger<IndexModel> logger) //Konstruktur
        {
            _logger = logger;
        }

        public void OnGet() //Tom OnGet metode
        {

        }
    }
}
