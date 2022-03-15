"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/buyingTickets").build();

document.getElementById("buyButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${message} bought`;
});

connection.start().then(function () {
    document.getElementById("buyButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("buyButton").addEventListener("click", function (event) {
    var checkboxes = document.getElementsByName("tickets");
    var seats = "";
    //var message = "1";
    //var message = "Performance: " + checkboxes[0].getAttribute("performance") + ", date of event: " + checkboxes[0].getAttribute("date") + ". ";
    for (var checkbox of checkboxes) {
        if (checkbox.checked) {
            if (seats != "") {
                seats = seats + ", " + checkbox.getAttribute("place");

            }
            else {
                seats = checkbox.getAttribute("place");
            }
        }
    }
    var tickets = seats.split(', ').map(function (item) {
        return parseInt(item, 10);
    });
    var message = "Performance: " + checkboxes[0].getAttribute("performance") + ", date of event: " + checkboxes[0].getAttribute("date") + ". Place/s " + seats;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    //event.preventDefault();
});