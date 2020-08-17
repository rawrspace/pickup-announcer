using Microsoft.AspNetCore.Http;
using PickupAnnouncer.Models.DAO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickupAnnouncer.Interfaces
{
    public interface IRegistrationFileHelper
    {
        public Task<IEnumerable<RegistrationDetailsDAO>> ProcessFile(IFormFile formFile);
        Task DeleteAll();
    }
}
