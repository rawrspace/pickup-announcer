using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PickupAnnouncer.Hubs
{
    public class PickupHub : Hub
    {
        public async Task AnnouncePickup(string carId, string coneId)
        {
            await Clients.All.SendAsync("PickupAnnouncement", carId, coneId);
        }
    }
}
