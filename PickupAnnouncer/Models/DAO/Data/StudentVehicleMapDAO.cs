using Dapper;

namespace PickupAnnouncer.Models.DAO.Data
{
    [Table("StudentVehicleMap", Schema = "Data")]
    public class StudentVehicleMapDAO : BaseDAO
    {
        public int StudentId { get; set; }
        public int VehicleId { get; set; }
    }
}
