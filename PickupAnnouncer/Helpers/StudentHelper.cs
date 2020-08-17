using AutoMapper;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickupAnnouncer.Helpers
{
    public class StudentHelper : IStudentHelper
    {
        private readonly IDbHelper _dbHelper;
        private readonly IMapper _mapper;

        public StudentHelper(IDbHelper dbHelper, IMapper mapper)
        {
            _dbHelper = dbHelper;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDTO>> GetStudentsForCar(int carId)
        {
            var studentRecords = await _dbHelper.GetStudentsForRegistrationId(carId);
            return studentRecords.Select(x => _mapper.Map<StudentDTO>(x));
        }
    }
}
