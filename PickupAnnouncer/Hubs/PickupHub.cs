using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using System.Threading.Tasks;

namespace PickupAnnouncer.Hubs
{
    public class PickupHub : Hub
    {
        private readonly IStudentDetailsHelper _studentDetailsHelper;

        public PickupHub(IStudentDetailsHelper studentDetailsHelper)
        {
            _studentDetailsHelper = studentDetailsHelper;
        }

        public async Task AnnouncePickup(PickupDetails details)
        {
            var students = _studentDetailsHelper.GetDetailsForCar(details.Car);
            var announcement = new PickupAnnouncement()
            {
                Car = details.Car,
                Cone = details.Cone,
                StudentDetails = students
            };
            await Clients.All.SendAsync("PickupAnnouncement", JsonConvert.SerializeObject(announcement));
        }
    }
}
