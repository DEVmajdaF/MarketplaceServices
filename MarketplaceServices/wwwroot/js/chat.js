"user strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatter").build();


connection.on("ReceiveMessage", (user, message, time, img) => {
    //var thisUser = document.getElementById("thisuser");
    //let iscurrentUserName = user === thisUser;
    //let container = document.getElementById("container");
    //container.className = iscurrentUserName ? "chat-message-right" : "chat-message-left";
    var cont = document.createElement("div");
     

    cont.innerHTML=` <div>
                                                <img src="/images/${img}" class="rounded-circle mr-1" alt="Chris Wood" width="40" height="40">
                                                <div class="text-muted small text-nowrap mt-2"> ${ time }</div>
                                            </div>
                                            <div class="flex-shrink-1 bg-light rounded py-2 px-3 mr-3">
                                                <div class="font-weight-bold mb-1">${ user }</div>
                                                ${ message }
                                            </div>`;

    document.getElementById("chatmessage").appendChild(cont);
                
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


