<%@ Page Language="C#" AutoEventWireup="false" CodeFile="TestHost.aspx.cs" Inherits="TestHost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TestHost</title>
</head>
<body>
    <div>
        TestHost
    </div>
    <% rptAge.DataSource = this.MyProperty; rptAge.DataBind(); %>
    <asp:Repeater runat="server" ID="rptAge">
        <ItemTemplate>
            <p><%# Container.DataItem.ToString() %></p>
        </ItemTemplate>
    </asp:Repeater>
</body>
</html>
