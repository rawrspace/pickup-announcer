using PickupAnnouncer.Interfaces;

namespace PickupAnnouncer.Services
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;

        public DbService(string connectionString)
        {
            _connectionString = connectionString;
        }

    }
}
