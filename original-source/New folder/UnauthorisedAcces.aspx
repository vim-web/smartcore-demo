<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="UnauthorisedAcces.aspx.vb" Inherits="SMARTCORE_UI.UnauthorisedAcces" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>



<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%; margin-top :10%">
      <br />
      <br />
      
      <asp:Label ID="LblMsg"  runat ="server"   
             Text="    You Have No Permission to Access this Page" Font-Bold="True" 
             Font-Size="20pt" ForeColor="#FF3300"  ></asp:Label> 
      <br />
      <br />
      <table width ="100%">
      <tr>
      <td width="80%">     
    
     </td>
     <td width="20%">
             <asp:ImageButton ID="ImageButton1" runat="server" 
             ImageUrl="~/Images/Home.png" />
         
     </td> 
      </tr>
      </table>
        
         
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>