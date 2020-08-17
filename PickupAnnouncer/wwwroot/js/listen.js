"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pickup").build();

connection.on("PickupAnnouncement", function (pickupAnnouncementJson) {
    console.log("Got it: " + pickupAnnouncementJson);
    var pickupAnnouncement = JSON.parse(pickupAnnouncementJson);
    if (pickupAnnouncement.students && pickupAnnouncement.students.length > 0) {
        addAnnouncement(pickupAnnouncement);
    }
    else {
        studentDetails = "No Students Found"
    }
});

function addAnnouncement(pickupAnnouncement) {
    var div = document.createElement("div");
    div.className = 'card shadow mt-2';
    var announcementCard = `
        <div class="card-body row">
            <div class="col-4 col-sm-2 p-2">
                <div class="row d-flex flex-wrap align-items-center h-100">
                    <div class="col-12">
                        <div class="card border-0">
                            <img class="card-img mx-auto" src="images/cone-icon.png" alt="cone ${pickupAnnouncement.cone}">
                            <div class="card-img-overlay text-white text-center d-flex justify-content-center align-items-center">
                                <div class="rounded p-1 pb-2 bg-dark text-white">
                                  <h2>${pickupAnnouncement.cone}</h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-8 col-sm-10 ">`;
    pickupAnnouncement.students.forEach(student => {
        announcementCard += `
            <div class="row">
                <div class="col-9">
                    <div class="row border border-light rounded grade-${student.gradeLevel}">
                        <div class="col-12 text-center">
                            <h2 class="mt-4">${student.name}</h2>
                            <h4 class="align-text-bottom text-right">${student.teacher}</h4>
                        </div>
                    </div>  
                </div>
                <div class="col-3 d-flex p-2">
                    <div class="align-self-center mx-auto">
                        <span>Grade</span>
                        <h3 class="text-center">${student.gradeLevel}</h3>
                    </div>
                </div>
            </div>`;
    });
    pickupAnnouncement += "</div></div>";
    div.innerHTML = announcementCard;
    var messageList = document.getElementById("messagesList");
    messageList.insertBefore(div, messageList.firstChild);
}

connection.start().then(function () {
    //Maybe add connected thing
}).catch(function (err) {
    return console.error(err.toString());
});