using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using System.Collections.Generic;

namespace PickupAnnouncer.Helpers
{
    public class StudentDetailsHelper : IStudentDetailsHelper
    {
        //TODO: Implement SQLite
        public List<StudentDetails> GetDetailsForCar(string carId)
        {
            switch (carId)
            {
                case "1234":
                    return new List<StudentDetails>()
                    {
                        new StudentDetails()
                        {
                            Name = "Bobby",
                            Teacher = "Smith",
                            GradeLevel = 1
                        },
                        new StudentDetails()
                        {
                            Name = "Doug",
                            Teacher = "Taylor",
                            GradeLevel = 3
                        }
                    };
                case "4321":
                    return null;
                default:
                    return new List<StudentDetails>()
                    {
                        new StudentDetails()
                        {
                            Name = "Leslie",
                            Teacher = "Potan",
                            GradeLevel = 2
                        }
                    };
            }

        }
    }
}
