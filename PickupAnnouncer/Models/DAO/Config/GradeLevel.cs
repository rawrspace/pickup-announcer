using Dapper;

namespace PickupAnnouncer.Models.DAO.Config
{
    [Table("GradeLevel", Schema = "Config")]
    public class GradeLevel : BaseDAO
    {
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
    }
}
