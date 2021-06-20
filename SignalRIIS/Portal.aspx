<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Portal.aspx.cs" Inherits="SignalRIIS.Portal" %>

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

            $("input[type=button]").click(function () {

                console.log(222);
                var msg = $("input[type=text]").val();
                var action = $("form").prop("action")
                $.post(action, { op: "msg", msg: msg }, function () {

                });
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
    <form  action="#">
        <div>
            <label><%=this.Session["userInfo"] as string %></label>
        </div>
    <input type="text" name="Msg" />
        <input type="button" value="确定" />
    </form>
    <div>
        会话列表
        <ul></ul>
    </div>
</body>
</html>
