var connection = new signalR.HubConnectionBuilder().withUrl("/chatter").build();


connection.on("receiveMessage", function (txtuser, message) {

    var msg = txtuser + ":" + message;
    var li = document.createElement("li");
    li.textContent = msg;
    $("#list").prepend(li);



});
connection.start();
$("#submitButton").on("click", function () {

    var txtuser = $("#txtuser").val();
    var message = $("#txtmessage").val();
    connection.invoke("SendMessage", txtuser, message);
});


