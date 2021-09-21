<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewExportPDF.aspx.cs" Inherits="Jawan.Portal.GridViewExportPDF" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="GridViewExportPDF1" runat="server">
    <div>
    
    
    
    <asp:GridView ID="gvProducts" AutoGenerateColumns="False"  CssClass="GridViewStyle"
    runat="server" >  
    <RowStyle CssClass="RowStyle" />             
    <FooterStyle CssClass="RowStyle" />                       
    <SelectedRowStyle CssClass="SelectedRowStyle" />   
    <HeaderStyle CssClass="HeaderStyle" />             
    <AlternatingRowStyle CssClass="AltRowStyle" />
    <Columns>
        <asp:BoundField DataField="unitid" HeaderText="unitId"/> 
     <asp:BoundField DataField="designation" HeaderText="designation"/> 
         <asp:BoundField DataField="noofemps" HeaderText="No Of Employees"/> 
        <asp:BoundField DataField="payrate" HeaderText="Payate"/> 
      
          
          
          <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="test" runat="server" Text="test" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns> 
</asp:GridView>
<p><asp:LinkButton ID="lnkExport" runat="server" onclick="lnkExport_Click" Text="Export"/></p> 
<p><asp:LinkButton ID="lnkExportWFormat" runat="server" onclick="lnkExportFormat_Click" Text="Export With Format"/></p>

    
    </div>
    </form>
</body>
</html>
