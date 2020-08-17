using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using System;
using System.Linq;
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
            var registrationId = Int32.Parse(details.Car);
            var students = await _studentDetailsHelper.GetStudentsForCar(registrationId);
            var announcement = new PickupNotice()
            {
                Car = details.Car,
                Cone = details.Cone,
                Students = students.ToList()
            };
            await Clients.All.SendAsync("PickupAnnouncement", JsonConvert.SerializeObject(announcement));
        }
    }
}
