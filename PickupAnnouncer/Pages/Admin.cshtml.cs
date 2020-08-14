using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;

namespace PickupAnnouncer.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IRegistrationFileHelper _registrationFileHelper;

        public AdminModel(IRegistrationFileHelper registrationFileHelper)
        {
            _registrationFileHelper = registrationFileHelper;
            Records = new List<RegistrationRecord>();
        }
        public IEnumerable<RegistrationRecord> Records { get; set; }
        public void OnPost(IFormFile registrationFile)
        {
            Records = _registrationFileHelper.ProcessFile(registrationFile);
        }
        public void OnGet()
        {

        }
    }
}