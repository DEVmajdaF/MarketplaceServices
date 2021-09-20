"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/Chatter").build();

var message = document.getElementById("txtmessage")
connection.on("receiveMessage", function (txtuser, message) {


    var li = document.createElement("li");
    li.textContent = txtuser + ":" + message;
    document.getElementById("list").prepend(li);
  /*  message.tntent = "";*/
   
});
connection.start();
var submit = document.getElementById("submitButton");
submit.addEventListener("click", function (e) {

    var txtuser = document.getElementById("txtuser").value;
    var txtmsg = message.value;
    connection.invoke("SendMessage", txtuser, txtmsg).catch(function(err) {
       
        return console.error(err.toString());
    });
e.preventDefault();

    });



