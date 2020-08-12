"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pickup").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var car = document.getElementById("carInput").value;
    var cone = document.getElementById("coneInput").value;
    connection.invoke("AnnouncePickup", car, cone).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});