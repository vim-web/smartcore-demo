<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanActionTakenCancel.aspx.vb" Inherits="SMARTCORE_UI.LoanActionTakenCancel" 
    title="LOAN ACTION TAKEN CANCELLATION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;">

     <legend>LOAN ACTION TAKEN CANCELLATION</legend>
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
                      <asp:GridView ID="DgvLoanActionTable" runat="server" AutoGenerateColumns="False" 
                          Width="100%" CssClass="grid-view" DataKeyNames="Sl_No">
                          <Columns>
                           <asp:BoundField DataField="Sl_No" HeaderText="Sl_No"   Visible ="true" />
                              <asp:BoundField DataField="Reg_Date" HeaderText="Date"   />
                              <asp:BoundField DataField="SendTo" HeaderText="Send To" />
                              <asp:BoundField DataField="Acc_Name" HeaderText="Posting Head" />
                             <asp:BoundField DataField="Pkc_Description" HeaderText="Send Type" />
                             <asp:BoundField DataField="Amount" HeaderText="Amount" />
                              <asp:ButtonField Text="Cancel" />
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
