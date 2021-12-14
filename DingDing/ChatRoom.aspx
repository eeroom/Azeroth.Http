<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatRoom.aspx.cs" Inherits="DingDing.ChatRoom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/Scripts/jquery-1.6.4.js"></script>
    <script src="/Scripts/jquery.signalR-1.1.4.js"></script>
    <script src="/signalr/hubs"></script>
    <script type="text/javascript">
        $(function () {

            $("form").submit(function () {
                var formdata = $(this).serialize();
                console.log("formdata", formdata);
                $.post("", formdata, function (data) {
                    console.log("发送成功：",data)
                });
                return false;//这里特别重要，否则浏览器会再发同步的表单提交
            });

            var lolHub = $.connection.lolHub;
            lolHub.client.refresh = function (username, msg) {

                $("ul").append("<li>"+username+":"+msg+"</li>");
            }

            $.connection.hub.start();
        });
    </script>
</head>
<body>
    <form>
        <input type="hidden"  name="cmd" value="SendMessage" />
        <div>
            <label><%=this.User.Identity.Name %></label>
        </div>
        <input type="text" name="Msg" />
        <input type="submit" value="确定" />
    </form>
    <div>
        会话列表
        <ul></ul>
    </div>
</body>
</html>
