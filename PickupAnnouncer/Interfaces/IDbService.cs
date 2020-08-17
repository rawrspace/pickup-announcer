using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickupAnnouncer.Interfaces
{
    public interface IDbService
    {
        Task<IEnumerable<T>> Get<T>(string whereConditions = null, object parameters = null);
        Task Insert<T>(IEnumerable<T> itemsToInsert);
        Task<IList<IDictionary<string, object>>> ExecuteQuery(string queryText, IDictionary<string, object> parameters);
        Task<IList<IDictionary<string, object>>> ExecuteStoredProcedure(string sprocName, IDictionary<string, object> parameters = null);
    }
}
