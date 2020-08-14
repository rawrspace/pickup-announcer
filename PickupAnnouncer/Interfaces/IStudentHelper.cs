using PickupAnnouncer.Models.DTO;
using System.Collections.Generic;

namespace PickupAnnouncer.Interfaces
{
    public interface IStudentHelper
    {
        public List<StudentDTO> GetStudentsForCar(string carId);
    }
}
