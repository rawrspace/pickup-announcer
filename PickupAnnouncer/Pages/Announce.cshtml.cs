using Microsoft.AspNetCore.Mvc.RazorPages;
using PickupAnnouncer.Interfaces;
using System.Threading.Tasks;

namespace PickupAnnouncer.Pages
{
    public class AnnounceModel : PageModel
    {
        private readonly IDbHelper _dbHelper;

        public AnnounceModel(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public int NumberOfCones { get; set; }
        public async Task OnGet()
        {
            NumberOfCones = await _dbHelper.GetNumberOfCones();
        }
    }
}