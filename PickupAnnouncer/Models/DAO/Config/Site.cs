using Dapper;

namespace PickupAnnouncer.Models.DAO.Config
{
    [Table("Site", Schema = "Config")]
    public class Site : BaseDAO
    {
        public int NumberOfCones { get; set; }
        public string AdminUser { get; set; }
        public string AdminPass { get; set; }
    }
}
