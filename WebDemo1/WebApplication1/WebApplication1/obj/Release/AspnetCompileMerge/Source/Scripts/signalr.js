
// 引用自动生成的集线器代理
var chat = $.connection.chatHub;
$(function () {
    // 声明一个代理
    // 定义服务器端调用的客户端sendMessage来显示新消息
    chat.client.sendMessage = function (name, message) {
        // 向页面添加消息
        showTip();
        $('#discussion').append('<li><strong>' + ' >: ' + htmlEncode(name)
            + '</strong>: ' + htmlEncode(message) + '</li>');
    };

    // 设置焦点到输入框
    //$('#message').focus();
    // 开始连接服务器
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // 调用服务器端集线器的Send方法
            chat.server.send($('#message').val());
            // 清空输入框信息并获取焦点
            $('#message').val('').focus();
        });
        /*
        $("#saveEdit").click(function () {
            chat.server.send($('#ID').val());
            // 清空输入框信息并获取焦点
        });
        */
    });

    $.connection.hub.disconnected(function () {
        setTimeout(function () {
            $.connection.hub.start();
        }, 5000); // Restart connection after 5 seconds.
    });

});

// 为显示的消息进行Html编码
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

$('.theme-empty .quxiao').click(function () {
    hideTip();
})

$('.theme-empty .submit').click(function () {
    hideTip();
    $('#discussion').html("");
})

function showTip() {
    $('.theme-popover-mask').fadeIn(100);
    $('.theme-empty').slideDown(200);
}

function hideTip() {
    $('.theme-popover-mask').fadeOut(100);
    $('.theme-empty').slideUp(200);
}