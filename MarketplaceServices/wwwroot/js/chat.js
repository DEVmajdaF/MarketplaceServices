"user strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatter").build();


connection.on("ReceiveMessage", (user, message, time) => {

    console.log(user, message);
    var li = document.createElement("li");
    document.getElementById("list").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message} At ${time}`;
});

var groupname = document.getElementById("roomid").value;
connection.start().then(res => {
    connection.invoke("JoinGroup", groupname)  //JoinGroup is C# method name
        .catch(err => {
            console.log(err);
        });
}).catch(err => {
    console.log(err);
});


var SendMessage = function (event) {
    event.preventDefault();
    //form
    var data = new FormData(event.target);
    console.log(data);
    console.log(event.target);
    document.getElementById('txtmessage').value = '';

    $.ajax({
        url: '/Chat/SendMessage',
        type: "post",
        processData: false,
        contentType: false,
        /*  contentType: 'application/x-www-form-urlencoded',*/
        data: data,
        success: function (result) {
            //console.log(result);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response);
        },


    });
};


