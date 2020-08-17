using Microsoft.Extensions.Logging;
using PickupAnnouncer.Extensions;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using PickupAnnouncer.Models.DAO;
using PickupAnnouncer.Models.DAO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickupAnnouncer.Helpers
{
    public class DbHelper : IDbHelper
    {
        private readonly IDbService _dbService;

        public DbHelper(IDbService dbService)
        {
            _dbService = dbService;
        }
        public async Task<bool> AddStudentRegistrations(IEnumerable<RegistrationDetailsDAO> registrationDetails)
        {
            try
            {
                //Update Database
                await _dbService.Insert(registrationDetails);
                await _dbService.ExecuteStoredProcedure(Sprocs.ProcessStagingRegistrationDetails);
                return true;
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "Failed to Add Registrations");
                return false;
            }
        }

        public async Task<bool> DeleteStudentRegistrations()
        {
            try
            {
                await _dbService.ExecuteStoredProcedure(Sprocs.ClearDatabase);
                return true;
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "Failed to Delete Registrations");
                return false;
            }
        }

        public async Task<IEnumerable<StudentDAO>> GetStudentsForRegistrationId(int registrationId)
        {
            var results = await _dbService.ExecuteStoredProcedure(Sprocs.GetStudentsForRegistrationId, new Dictionary<string, object>() { { "RegistrationId", registrationId } });
            return ParseResults<StudentDAO>(results);
        }

        private static List<T> ParseResults<T>(IList<IDictionary<string, object>> results)
        {

            var resultsList = new List<T>();
            if (results != null)
            {
                foreach (var result in results)
                {
                    var dbResponse = result.ToType<object, T>();
                    resultsList.Add(dbResponse);
                }
            }

            return resultsList;
        }
    }
}
