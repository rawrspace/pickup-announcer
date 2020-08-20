using CsvHelper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PickupAnnouncer.Extensions;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using PickupAnnouncer.Models.DAO;
using PickupAnnouncer.Models.DAO.Data;
using PickupAnnouncer.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PickupAnnouncer.Helpers
{
    public class DbHelper : IDbHelper
    {
        private readonly IDbService _dbService;
        private readonly ILogger<DbHelper> _logger;

        public DbHelper(IDbService dbService, ILogger<DbHelper> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }
        public async Task<bool> AddStudentRegistrations(IEnumerable<RegistrationDetailsDAO> registrationDetails)
        {
            try
            {
                var dataTable = new DataTable("Staging.RegistrationDetails");
                dataTable.Columns.Add(new DataColumn("RegistrationId", typeof(string)));
                dataTable.Columns.Add(new DataColumn("FirstName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("LastName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Teacher", typeof(string)));
                dataTable.Columns.Add(new DataColumn("GradeLevel", typeof(string)));
                foreach (var item in registrationDetails)
                {
                    var row = dataTable.NewRow();
                    row["RegistrationId"] = item.RegistrationId;
                    row["FirstName"] = item.FirstName;
                    row["LastName"] = item.LastName;
                    row["Teacher"] = item.Teacher;
                    row["GradeLevel"] = item.GradeLevel;
                    dataTable.Rows.Add(row);
                }
                //Update Database
                using (var connection = _dbService.GetSqlConnection())
                {
                    var bulkInsert = new SqlBulkCopy(connection);
                    bulkInsert.DestinationTableName = "Staging.RegistrationDetails";
                    bulkInsert.ColumnMappings.Add("RegistrationId", "RegistrationId");
                    bulkInsert.ColumnMappings.Add("FirstName", "FirstName");
                    bulkInsert.ColumnMappings.Add("LastName", "LastName");
                    bulkInsert.ColumnMappings.Add("Teacher", "Teacher");
                    bulkInsert.ColumnMappings.Add("GradeLevel", "GradeLevel");
                    connection.Open();
                    bulkInsert.WriteToServer(dataTable);
                    connection.Close();
                }
                await _dbService.ExecuteStoredProcedure(Sprocs.ProcessStagingRegistrationDetails);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add registrations");
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
                _logger.LogError(e, "Failed to delete registrations.");
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

        public async Task AddPickupLog(PickupNotice pickupNotice)
        {
            var jsonData = JsonConvert.SerializeObject(pickupNotice);
            await _dbService.ExecuteStoredProcedure(Sprocs.AddPickupLog, new Dictionary<string, object>() { { "json", jsonData } });
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

        public async Task<IEnumerable<PickupNotice>> GetPickupNotices(DateTimeOffset startOfDay)
        {
            var pickupLogs = await _dbService.Get<PickupDAO>("WHERE PickupTimeUTC >= @StartOfDay AND PickupTimeUTC < @EndOfDay", new { StartOfDay = startOfDay, EndOfDay = startOfDay.AddDays(1) });
            var pickupNotices = pickupLogs.GroupBy(x => new { x.RegistrationId, x.Cone, x.PickupTimeUTC }).Select(x => new PickupNotice()
            {
                Car = x.Key.RegistrationId.ToString(),
                Cone = x.Key.Cone.ToString(),
                PickupTimeUTC = x.Key.PickupTimeUTC,
                Students = x.Select(y => new StudentDTO()
                {
                    Name = y.Name,
                    Teacher = y.Teacher,
                    GradeLevel = y.GradeLevel
                })
            });
            return pickupNotices;
        }
    }
}
