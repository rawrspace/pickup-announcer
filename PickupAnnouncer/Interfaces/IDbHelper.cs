using PickupAnnouncer.Models.DAO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickupAnnouncer.Interfaces
{
    public interface IDbHelper
    {
        Task<bool> AddStudentRegistrations(IEnumerable<RegistrationDetailsDAO> registrationDetails);

        Task<bool> DeleteStudentRegistrations();

        Task<IEnumerable<StudentDAO>> GetStudentsForRegistrationId(int registrationId);
    }
}
