using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using PickupAnnouncer.Models.DTO;
using System.Collections.Generic;

namespace PickupAnnouncer.Helpers
{
    public class StudentHelper : IStudentHelper
    {
        private readonly IDbService _dbService;

        public StudentHelper(IDbService dbService)
        {
            _dbService = dbService;
        }

        //TODO: Implement SQLite
        public List<StudentDTO> GetStudentsForCar(string carId)
        {
            switch (carId)
            {
                case "1234":
                    return new List<StudentDTO>()
                    {
                        new StudentDTO()
                        {
                            Name = "Bobby",
                            Teacher = "Smith",
                            GradeLevel = 1
                        },
                        new StudentDTO()
                        {
                            Name = "Doug",
                            Teacher = "Taylor",
                            GradeLevel = 3
                        }
                    };
                case "4321":
                    return null;
                default:
                    return new List<StudentDTO>()
                    {
                        new StudentDTO()
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
