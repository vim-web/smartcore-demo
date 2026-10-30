<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LkrDisplay.aspx.vb" Inherits="SMARTCORE_UI.LkrDisplay" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
     <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:15%; margin-right:15%;">
          <legend> LOCKER DISPLAY</legend>
     
    <br />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table   width="100%">      
        <tr>
            <td width="10%"></td>
            <td width="30%" ><label >BRANCH:</label></td>
            <td width="50%" >        
             <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" Width="100%">
             </asp:DropDownList>
           </td>
           <td width="10%">&nbsp;</td></tr>
        <tr>
           <td width="10%"></td>
           <td width="30%" style="width: 0"><label>LOCKERCODE:</label> </td>
           <td width="50%">
                 <asp:DropDownList ID="DDLCODE" runat="server" AutoPostBack="True" Width="100%">
                 </asp:DropDownList>
          </td>
          <td width="10%">&nbsp;</td>
         </tr>
        <tr>
            <td width="10%">
               </td>
            <td align="center" colspan="2" style="width: 80%">
                   <asp:Label ID="Label1" runat="server" BackColor="Red" Font-Bold="True" 
                    Height="40px" Text="LOCKER OPEN" Width="100px" />
                <asp:Label ID="Label2" runat="server" BackColor="Yellow" Font-Bold="True" 
                    Height="40px" Text="LOCKER CLOSED" Width="100px" />
             
                 
                
             
            </td>
            <td width="10%">
               </td>
        </tr>
        <tr>
            <td width="10%">
               </td>
            <td align="center" colspan="2" style="width: 80%">
               </td>
            <td width="10%">
               </td>
        </tr>
    <tr>
    
     <td width="100%" colspan="4">
        <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign="Justify">
        </asp:Panel>
                    </td>
     </tr>
    <tr>
    <td width="10%">
       </td>
     <td width="80%" colspan="2" align="center">
         <asp:ImageButton ID="CmdClose" runat="server" 
             ImageUrl="~/Images/CloseBtn.png" />
        </td>
      <td width="10%">&nbsp;</td></tr>
    <tr>
    <td width="10%">
       </td>
     <td width="80%" colspan="2">
        </td>
      <td width="10%">&nbsp;</td></tr>
    </table>
              
          </legend>
         
              </div>
    </ContentTemplate>
</asp:UpdatePanel> 
    
   
</asp:Content>
