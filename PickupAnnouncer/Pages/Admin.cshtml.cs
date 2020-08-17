using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using PickupAnnouncer.Interfaces;
using System;
using System.Threading.Tasks;

namespace PickupAnnouncer.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IRegistrationFileHelper _registrationFileHelper;
        private readonly IToastNotification _toastNotification;

        public AdminModel(IRegistrationFileHelper registrationFileHelper, IToastNotification toastNotification)
        {
            _registrationFileHelper = registrationFileHelper;
            _toastNotification = toastNotification;
        }
        public async Task OnPostInsert(IFormFile registrationFile)
        {
            try
            {
                await _registrationFileHelper.ProcessFile(registrationFile);
                _toastNotification.AddSuccessToastMessage("Records updated");
            }
            catch(Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message);
            }
        }

        public async Task OnPostDelete()
        {
            try
            {
                await _registrationFileHelper.DeleteAll();
                _toastNotification.AddSuccessToastMessage("Records deleted");
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message);
            }
        }
    }
}