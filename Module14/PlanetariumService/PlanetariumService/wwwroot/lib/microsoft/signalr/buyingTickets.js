"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/buyingTickets").build();

document.getElementById("buyButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    
    if (message != "") {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        li.textContent = `${message} bought`;
    }
});

connection.start().then(function () {
    document.getElementById("buyButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("buyButton").addEventListener("click", function (event) {
    var checkboxes = document.getElementsByName("tickets");
    var seats = "";
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
    if (seats == "") {
        var counter = 2;
        for (var checkbox of checkboxes) {
            if (counter == 0) {
                var message = "Performance: " + checkboxes[0].getAttribute("performance") + ", date of event: " + checkboxes[0].getAttribute("date") + ". Place/s " + seats;            
                connection.invoke("SendMessage", message).catch(function (err) {
                    return console.error(err.toString());
                });
            }
            if (checkbox.getAttribute("status") == "available") {
                if (seats != "") {
                    seats = seats + ", " + checkbox.getAttribute("place");

                }
                else {
                    seats = checkbox.getAttribute("place");
                }
                counter--;
            }
        }
        if (seats == "") {
            var message = "";
        } else {
            var message = "Performance: " + checkboxes[0].getAttribute("performance") + ", date of event: " + checkboxes[0].getAttribute("date") + ". Place/s " + seats;
        }
    }
    else {
        var message = "Performance: " + checkboxes[0].getAttribute("performance") + ", date of event: " + checkboxes[0].getAttribute("date") + ". Place/s " + seats;
        connection.invoke("SendMessage", message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    
});