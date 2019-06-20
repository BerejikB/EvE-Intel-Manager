"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Notifications").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("receiveMessage" , function (CharName, ReportGenerated, ReportSystem, Reportbody) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //Replace with "AAHHH SOMETHING IS HAPPEnINg ahHH"
    var encodedMsg = CharName + " in " + ReportSystem + " Alert: " + Reportbody + " at " + ReportGenerated + " UTC ";
    //Do not create list, instead create popup alert
    alert(encodedMsg);
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    
    var CharName = document.getElementById("CharName").value;
    var ReportGenerated = document.getElementById("ReportGenerated").value;
    var ReportSystem = document.getElementById("ReportSystem").value;
        var ReportBody = document.getElementById("ReportBody").value;
    connection.invoke("SendMessage", CharName, ReportGenerated, ReportSystem, Reportbody).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});