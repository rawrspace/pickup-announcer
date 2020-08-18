using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PickupAnnouncer.Models;

namespace PickupAnnouncer.Pages
{
    public class AnnounceModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public AnnounceModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int NumberOfCones { get; set; }
        public void OnGet()
        {
            var siteConfig = _configuration.GetSection("SiteConfig").Get<SiteConfig>();
            NumberOfCones = siteConfig.NumberOfCones;
        }
    }
}