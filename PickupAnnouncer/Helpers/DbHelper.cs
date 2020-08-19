using CsvHelper;
using PickupAnnouncer.Extensions;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models.DAO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                return false;
            }
        }

        public async Task<IEnumerable<StudentDAO>> GetStudentsForRegistrationId(int registrationId)
        {
            var results = await _dbService.ExecuteStoredProcedure(Sprocs.GetStudentsForRegistrationId, new Dictionary<string, object>() { { "RegistrationId", registrationId } });
            return ParseResults<StudentDAO>(results);
        }

        public async Task<Stream> GetRegistrationDetailsStream()
        {
            var results = await _dbService.ExecuteStoredProcedure(Sprocs.ExportRegistrationDetails);
            var registrationDetails = ParseResults<RegistrationDetailsDAO>(results).OrderBy(x => x.RegistrationId);
            var outputStream = new MemoryStream();
            var writer = new StreamWriter(outputStream);
            var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(registrationDetails);
            writer.Flush();
            outputStream.Position = 0;
            return outputStream;
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
