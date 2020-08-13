"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pickup").build();

connection.on("PickupAnnouncement", function (pickupAnnouncementJson) {
    console.log("Got it: " + pickupAnnouncementJson);
    var pickupAnnouncement = JSON.parse(pickupAnnouncementJson);
    var li = document.createElement("li");
    var studentDetails = "";
    if (pickupAnnouncement.studentDetails && pickupAnnouncement.studentDetails.length > 0) {
        pickupAnnouncement.studentDetails.forEach((details) => {
            studentDetails += `[${details.name},${details.teacher},${details.gradeLevel}]`;
        });
    }
    else {
        studentDetails = "No Students Found"
    }
    li.textContent = `${pickupAnnouncement.car} is here @ cone ${pickupAnnouncement.cone} : ${studentDetails}`;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    //Maybe add connected thing
}).catch(function (err) {
    return console.error(err.toString());
});