////"use strict";


////var connection = new signalR.HubConnectionBuilder().withUrl("/Chatter").build();

////var message = document.getElementById("txtmessage");
////connection.on("receiveMessage", function (txtuser, message) {


////    var li = document.createElement("li");
////    li.textContent = txtuser + ":" + message;
////    document.getElementById("list").prepend(li);

////  /*  message.tntent = "";*/
   
////});
////connection.start();

////var submitbutton = document.getElementById("submitbutton");
////submitbutton.addEventListener("click", function(event) {
////    var txtuser = document.getElementById("txtuser").value;
////    var txtmsg= message.value;
////    connection.invoke("SendMessage", txtuser, txtmsg).catch(function(err) {
       
////        return console.error(err.toString());
////    });
////    event.preventDefault();

////});




"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/Chatter").build();

var message = document.getElementById("txtmessage");

connection.on("ReceiveMessage", function (message, txtuser, time) {
    var li = document.createElement("li");
    li.textContent = txtuser + " Says:" + message + " At " + time ;
    document.getElementById("list").prepend(li);

});






connection.start().then(function () {
connection.invoke("getConnectionId").then(function (id) {
document.getElementById("connectionId").value = id;
 })
});



//var joinRoom = function () {

//    var url = '/Chat/joinRoom/connectionId/@Model.Id'
//    axios.post(url, null)
//        .then(res => {
//            console.log("roomJoined!", res);
//        })
//        .catch(err => {
//            console.log("faild to join room", res);
//        })

//}
//.then(function () {
//    connection.invoke('joinRoom', '@Model.Id');
//})
//    .catch(function (err) {
//        console.log(err)
//    })
//window.addEventListener('onunload', function () {
//    connection.invoke('leaveRoom', '@Model.Id');
//})


//    .then(function () {
//    connection.invoke("GetConnectionId").then(function (id) {
//        document.getElementById("connectionId").innerText = id;
//    })
//});


//var submitbutton2 = document.getElementById("submitbutton2");
//submitbutton2.addEventListener("click", function (event) {
//    var txtuser = document.getElementById("txtuser").value;
//    var txtmsg = message.value;
//    var receiverId = document.getElementById("receiverId").value;
 
    
//    connection.invoke("SendMessageToUser", txtuser, receiverId, txtmsg).catch(function (err) {
//        return console.error(err.toString());
//    });
 
//    event.preventDefault();
   

//});

//var submitbutton = document.getElementById("submitbutton");
//submitbutton.addEventListener("click", function (event) {
//    var txtuser = document.getElementById("txtuser").value;
//    var txtmsg = message.value;
//    connection.invoke("SendMessage", txtuser, txtmsg).catch(function (err) {

//        return console.error(err.toString());
//    });
//    event.preventDefault();

//});



var SendMessage = function (event) {
   event.preventDefault();


    //'objet FormData sera rempli avec les clés/valeurs du formulaire en utilisant les noms de propriétés 
    //de chaque élément pour clé et les valeurs soumises.Cela encodera aussi le contenu des fichiers.
    //event.target 
    var data = new FormData(event.target);

    document.getElementById('txtmessage').value = '';
    axios.post('/Chat/SendMessage', data)
        .then(res => {
            console.log("Message Sent!")
        })
        .catch(err => {
            console.log("Failed to send message!")
        })
}








