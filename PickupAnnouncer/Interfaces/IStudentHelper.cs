using PickupAnnouncer.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickupAnnouncer.Interfaces
{
    public interface IStudentHelper
    {
        Task<IEnumerable<StudentDTO>> GetStudentsForCar(int carId);
    }
}
