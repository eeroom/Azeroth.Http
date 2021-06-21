<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SignalRIIS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
     <form method="post" >
         <input type="hidden" name="op"  value="submit" />
    用户名:<input type="text" name="UserName" />
        <input type="submit" value="登陆" />
    </form>
</body>
</html>
