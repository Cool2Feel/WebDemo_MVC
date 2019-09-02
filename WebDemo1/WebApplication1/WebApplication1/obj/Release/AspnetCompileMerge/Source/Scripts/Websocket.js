jQuery(document).ready(function ($) {

var WebSocketsExist = true;
try {
    var dummy = new WebSocket("ws://localhost:8989/test");
} catch (ex) {
    try {
        webSocket = new MozWebSocket("ws://localhost:8989/test");
    }
    catch (ex) {
        WebSocketsExist = false;
    }
}

if (WebSocketsExist) {
    // alert("欢迎使用WebSocket + c#实现的WebChat", "OK");
    //document.getElementById("Connection").value = "192.168.3.69:4141/chat";
} 
else {
    alert("您的浏览器不支持WebSocket。请选择其他的浏览器再尝试连接服务器。", "ERROR");
    //document.getElementById("ToggleConnection").disabled = true;
    return;
}

})

$(
    function () {
        $("#btnConnect").click(function () {
            var ws = new WebSocket("ws://" + window.location.hostname + ":" + window.location.port + "/api/WSChat");//new WebSocket("ws://192.168.3.69:4141/chat");

            ws.onopen = function () {
                console.log("open");
                $("#messageSpan").text("Connected!");
                //document.write(returnCitySN["cip"]+','+returnCitySN["cname"]) ;
                //ws.send("login:" + "xxxx");
            };

            ws.onmessage = function (evt) {
                console.log(evt.data)
                alert(evt.data);
                var s = evt.data.toString();
                var s1 = s.split(':');
                var s2 = s1[1];
                s1 = s2.split(';');
                var t1 = s1[0];
                var t2 = s1[1];
                $("#messageSpan").text(result.data);
                //$('#room_' + t1).html(t2);
                //document.getElementById('room_' + t1).innerHTML = t2;
            };

            ws.onclose = function (evt) {
                console.log("WebSocketClosed!");
            };

            ws.onerror = function (evt) {
                console.log("WebSocketError!");
                $("#messageSpan").text(error.data);
            };
        });


        $("#btnSend").click(function () {
            if (ws.readyState == WebSocket.OPEN) {
                ws.send($("#txtInput").val());
            }
            else {
                $("messageSpan").text("Connection is Closed!");
            }
        });
        $("#btnDisconnect").click(function () {
            ws.close();
        });

    }
    );