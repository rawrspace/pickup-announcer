using Microsoft.AspNetCore.Mvc;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Models;
using PickupAnnouncer.Models.Requests;
using PickupAnnouncer.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickupAnnouncer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupLogController : ControllerBase
    {
        private readonly IDbHelper _dbHelper;

        public PickupLogController(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpPost]
        public async Task<PickupLogResponse> Post([FromBody] PickupLogRequest request)
        {
            var pickupNotices = await _dbHelper.GetPickupNotices(request.StartOfDayUTC);
            return new PickupLogResponse()
            {
                Announcements = pickupNotices.OrderBy(x => x.PickupTimeUTC)
            };
        }
    }
}
