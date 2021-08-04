<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeClient.aspx.cs" Inherits="WebServiceClient.HomeClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>WebServiceClient-Demo</title>
</head>
<body>
    <p><%=this.Message %></p>
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="使用AsmxClient调用" />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="使用WcfClientBase调用" />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="使用SoapHttpClientProtocolIntercept调用" />
    </form>
    
</body>
</html>
