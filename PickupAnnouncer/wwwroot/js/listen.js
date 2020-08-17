"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pickup").build();

connection.on("PickupAnnouncement", function (pickupAnnouncementJson) {
    console.log("Got it: " + pickupAnnouncementJson);
    var pickupAnnouncement = JSON.parse(pickupAnnouncementJson);
    if (pickupAnnouncement.students && pickupAnnouncement.students.length > 0) {
        pickupAnnouncement.students.forEach((student) => {
            addAnnouncement(pickupAnnouncement.car, pickupAnnouncement.cone, student);
        });
    }
    else {
        studentDetails = "No Students Found"
    }
});

function addAnnouncement(car, cone, student) {
    var div = document.createElement("div");
    div.className = 'card shadow mt-2';
    var announcementCard = `
        <div class="card-body row">
            <div class="col-4 col-sm-2 p-2">
                <div class="cone-container mx-auto">
                    <img src="images/cone-icon.png"/>
                    <h3><span class="cone-centered badge badge-info">${cone}</span></h3>
                </div>
            </div>
            <div class="col-6 col-sm-9 border border-light rounded grade-${student.gradeLevel}">
                <div class="row">
                    <div class="col-12 text-center">
                        <h2 class="mt-4">${student.name}</h2>
                        <h4 class="align-text-bottom text-right">${student.teacher}</h4>
                    </div>
                </div>
            </div>
            <div class="col-1 d-flex p-2">
                <div class="align-self-center mx-auto">
                    <span>Grade</span>
                    <h3 class="text-center">${student.gradeLevel}</h3>
                </div>
            </div>
        </div>
    `;
    div.innerHTML = announcementCard;
    var messageList = document.getElementById("messagesList");
    messageList.insertBefore(div, messageList.firstChild);
}

connection.start().then(function () {
    //Maybe add connected thing
}).catch(function (err) {
    return console.error(err.toString());
});