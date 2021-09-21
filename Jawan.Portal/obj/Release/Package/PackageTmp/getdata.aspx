<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getdata.aspx.cs" Inherits="Jawan.Portal.getdata" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="getdata1" runat="server">
    <div>
    
  Enter Table Name : <asp:TextBox ID="txttname" runat="server"> </asp:TextBox>
    <asp:Button  ID="btnget" runat="server"  Text="get" class="btn save" 
            onclick="btnget_Click"/>
    
    
    
    </div>
    </form>
</body>
</html>
