﻿using PickupAnnouncer.Models;
using PickupAnnouncer.Models.DAO;
using PickupAnnouncer.Models.DAO.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PickupAnnouncer.Interfaces
{
    public interface IDbHelper
    {
        Task<bool> AddStudentRegistrations(IEnumerable<RegistrationDetailsDAO> registrationDetails);

        Task<bool> DeleteStudentRegistrations();

        Task<IEnumerable<StudentDAO>> GetStudentsForRegistrationId(int registrationId);
        Task<Stream> GetRegistrationDetailsStream();
        Task<int> GetNumberOfCones();
        Task AddPickupLog(PickupNotice pickupNotice);
        Task<IEnumerable<PickupNotice>> GetPickupNotices(DateTimeOffset startOfDay);
        Task<IDictionary<string, GradeLevel>> GetGradeLevelConfig(IEnumerable<string> gradeLevels);
        Task<bool> AuthenticateUser(string userName, string password);
    }
}
