using PickupAnnouncer.Models;
using System.Collections.Generic;

namespace PickupAnnouncer.Interfaces
{
    public interface IStudentDetailsHelper
    {
        public List<StudentDetails> GetDetailsForCar(string carId);
    }
}
