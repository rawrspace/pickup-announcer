using CsvHelper.Configuration.Attributes;
using Dapper;

namespace PickupAnnouncer.Models.DAO
{
    public abstract class BaseDAO
    {
        public int Id { get; set; }
    }
}
