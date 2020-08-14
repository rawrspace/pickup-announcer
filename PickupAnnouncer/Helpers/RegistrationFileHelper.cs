using CsvHelper;
using Microsoft.AspNetCore.Http;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PickupAnnouncer.Helpers
{
    public class RegistrationFileHelper : IRegistrationFileHelper
    {
        public IEnumerable<RegistrationRecord> ProcessFile(IFormFile formFile)
        {
            IEnumerable<RegistrationRecord> records = null;
            using(var reader = new StreamReader(formFile.OpenReadStream()))
            {
                using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<RegistrationRecord>().ToList();
                }
            }
            return records;
        }
    }
}
