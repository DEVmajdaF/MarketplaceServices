
"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/Chatter").build();

var message = document.getElementById("txtmessage");
connection.on("receivemsgfromuser", function (txtuser, message) {


    var li = document.createElement("li");
    li.textContent = txtuser + " Says:" + message;
    document.getElementById("list").prepend(li);

    /*  message.tntent = "";*/

});
connection.start().then(function () {
    connection.invoke("GetConnectionId").then(function (id) {
        document.getElementById("connectionid").innerText = id;
    })
});


var submitbutton = document.getElementById("submitbutton2");
submitbutton.addEventListener("click", function (event) {
    var txtuser = document.getElementById("txtuser").value;
    var txtmsg = message.value;
    var receiverconid = document.getElementById("receiverId").value;
    connection.invoke("SendMessage", txtuser, receiverconid, txtmsg).catch(function (err) {

        return console.error(err.toString());
    });
   event.preventDefault();

});