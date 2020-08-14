using Microsoft.AspNetCore.Http;
using PickupAnnouncer.Models;
using System.Collections.Generic;

namespace PickupAnnouncer.Interfaces
{
    public interface IRegistrationFileHelper
    {
        public IEnumerable<RegistrationRecord> ProcessFile(IFormFile formFile);
    }
}
