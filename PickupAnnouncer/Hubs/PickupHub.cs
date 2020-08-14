using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using System.Threading.Tasks;

namespace PickupAnnouncer.Hubs
{
    public class PickupHub : Hub
    {
        private readonly IStudentHelper _studentDetailsHelper;

        public PickupHub(IStudentHelper studentDetailsHelper)
        {
            _studentDetailsHelper = studentDetailsHelper;
        }

        public async Task AnnouncePickup(ArrivalNotice details)
        {
            var students = _studentDetailsHelper.GetStudentsForCar(details.Car);
            var announcement = new PickupNotice()
            {
                Car = details.Car,
                Cone = details.Cone,
                Students = students
            };
            await Clients.All.SendAsync("PickupAnnouncement", JsonConvert.SerializeObject(announcement));
        }
    }
}
