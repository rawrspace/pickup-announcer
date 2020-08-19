using CsvHelper;
using PickupAnnouncer.Extensions;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models.DAO;
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

        public DbHelper(IDbService dbService)
        {
            _dbService = dbService;
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
