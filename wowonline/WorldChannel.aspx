<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorldChannel.aspx.cs" Inherits="wowonline.WorldChannel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>艾泽拉斯世界频道</title>
    <script src="/Scripts/jquery-1.6.4.js"></script>
    <script src="/Scripts/jquery.signalR-1.1.4.js"></script>
    <script src="/signalr/hubs"></script>
    <script type="text/javascript">
        $(function () {
            var worldChannelHub = $.connection.worldChannelHub;
            worldChannelHub.client.reciveMsg = function (username, msg) {
                $("ul").append("<li>" + username + ":" + msg + "</li>");
            }
            worldChannelHub.client.reciveError = function (error) {
                $("ul").append("<li>" + error + "</li>");
            }
            $.connection.hub.start();


            $("#form4all").submit(function () {
                //直接推送消息，调用signalr的hub上定义的SendMessageToAll方法
                var msgItem = $(this).serializeArray().find(function (formitem) {
                    return formitem.name == "msg"
                });
                worldChannelHub.server.sendMessageToAll(msgItem.value);
                //必须，否则浏览器会再发同步的表单提交
                return false;
            });

            $("#form4target").submit(function () {
                //间接推送消息，首先调用业务上的控制器的SendMessage方法，控制器方法后续会调用signalr推送消息
                $.post("", $(this).serialize(), function () {
                    //todo 业务上相关的一些处理逻辑
                })
                //必须，否则浏览器会再发同步的表单提交
                return false;
            });
        });
    </script>
</head>
<body>
    <h3><%=this.User.Identity.Name %></h3>
    <form id="form4all">
        <label>发送给所有人</label>
        <input type="text" name="msg" placeholder="请输入消息内容" />
        <input type="submit" value="发送" />
    </form>
    <form id="form4target">
        <input type="hidden" name="cmd" value="SendMessage" />
        <input type="text" name="target" placeholder="请输入接收人" />
        <input type="text" name="msg" placeholder="请输入消息内容" />
        <input type="submit" value="发送" />
    </form>
    <h3>会话列表</h3>
    <ul></ul>
</body>
</html>
