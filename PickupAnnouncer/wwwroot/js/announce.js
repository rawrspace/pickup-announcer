"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pickup").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

function addToCarInput(number) {
    var carInput = $('#carInput');
    carInput.val(carInput.val() + number);
}

function clearCarInput() {
    $('#carInput').val('');
}

function createConeButtons(numberOfCones, conesPerGroup) {
    var totalCones = 0;
    var numberOfGroups = Math.ceil(numberOfCones / conesPerGroup);
    for (var group = 0; group < numberOfGroups; group++) {
        var div = document.createElement("div");
        div.className = 'btn-group btn-group-lg d-flex w-100 mb-2';
        for (var i = 0; i < conesPerGroup; i++) {
            if (totalCones >= numberOfCones) {
                break; //Exit the loop if we have all the buttons
            }
            var button = document.createElement("button");
            button.className = "btn btn-outline-primary p-4 w-100 border-2";
            const coneNumber = totalCones + 1;
            button.innerHTML = coneNumber;
            div.appendChild(button);
            totalCones++;
        }
        $('#cone-container').append(div);
    }
}

createConeButtons(numberOfCones, 4);

$('#cone-container button').click(function () {
    $('#cone-container button').removeClass('active');
    $(this).addClass('active');
});

function getActiveCone() {
    const coneValue = $('#cone-container .active:first');
    if (coneValue.length > 0) {
        return coneValue[0].innerHTML;
    }
    return undefined;
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    var car = document.getElementById("carInput").value;
    var cone = getActiveCone();
    if (!cone) {
        toastr.error("A value must be selected for the pickup cone");
    }
    else {
        connection.invoke("AnnouncePickup", { 'car': car, 'cone': cone })
            .then(() => {
                toastr.info('Notification Sent');
                $('#cone-container button').removeClass('active');
                $("#carInput").val('');
            }).catch (function (err) {
                toastr.error(err.toString());
             });
        event.preventDefault();
    }
});