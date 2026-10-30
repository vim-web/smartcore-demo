<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="RemainderRevoke.aspx.vb" Inherits="SMARTCORE_UI.RemainderRevoke" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:25%; margin-right:20%;">

     <legend>Remainder Revoke</legend>
     <table width="100%">
           <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                &nbsp;</td>
            <td width="48%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
            </tr>
           <tr>
               <td width="2%">
               </td>
               <td width="48%">
                   <label>
                   Remainder Type</label></td>
               <td width="48%">
                   <asp:DropDownList ID="CmbRemndrTo" runat="server" AutoPostBack="True" 
                       Width="100%" Enabled="true">
                       <asp:ListItem>Account</asp:ListItem>
                       <asp:ListItem>User</asp:ListItem>
                       <asp:ListItem>Customer</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td width="2%">
               </td>
           </tr>
     </table>
   <div id="DivAccountNo" runat ="server"   >    
          <table width="100%">
           <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Branch Name</label></td>
            <td width="48%">
            
            <table width ="100%">
            <tr>
            
            <td width ="70%">
                <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True">
             </asp:DropDownList>
            </td>
            </tr>
            </table>
            
            
            
                </td>
            <td width="2%">
            </td>
              </tr>
              
                         <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Scheme Name</label></td>
            <td width="48%">
     <table width ="100%">
            <tr>
            <td width ="30%">
            <asp:TextBox ID="TxtScheme" Width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
            
            
            </td>
            <td width ="70%">
                <asp:DropDownList ID="CmbScheme" Width="100%" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            </tr>
            </table>
                
             </td>
            <td width="2%">
            </td>
              </tr>
                                       <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Account Number</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAccNo" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
                                       <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Name</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAcHoldrName" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
                                       <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Address</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAcHoldrAddr" runat="server" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
              <tr>
                  <td width="2%">
                      </td>
                  <td width="48%" colspan="2" style="width: 96%">
                      <asp:GridView ID="DgvReminder" runat="server" AutoGenerateColumns="False" 
                          Width="100%" CssClass="grid-view" DataKeyNames="Reminder_ID">
                          <Columns>
                              <asp:BoundField DataField="Reminder_ID" HeaderText="ID"  />
                              <asp:BoundField DataField="RemainderDate" HeaderText="Date" />
                              <asp:BoundField DataField="Remainder_Content" HeaderText="Reminder" />
                              <asp:ButtonField Text="Revoke" />
                          </Columns>
                      </asp:GridView>
                      </td>
                  <td width="2%">
                      </td>
              </tr>
              <tr>
              <td width="2%"></td>
              <td width="48%"></td>
              <td width="48%"></td>
              <td width="2%"></td>                                
              </tr>
              <tr>
                  <td width="2%">
                      &nbsp;</td>
                  <td width="48%">
                      <asp:HiddenField ID="HdnAccID" runat="server" />
                  </td>
                  <td width="48%">
                      &nbsp;</td>
                  <td width="2%">
                      &nbsp;</td>
              </tr>
     </table>
   </div>
     </div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

