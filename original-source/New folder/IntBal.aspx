<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="IntBal.aspx.vb" Inherits="SMARTCORE_UI.IntBal" 
    title="Untitled Page" %>
       <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; " >                
    
    <legend>CUSTOMER INTEREST PAID REPORT</legend>
     <table width="100%">
          <tr>
          <td width="2%"></td>
        <td width="23%"></td>
        <td width="23%"></td>
        <td width="2%"></td>
        <td width="20%"></td>
        <td width="23%"></td>
        <td width="5%"></td>
      </tr>
      
      <tr>
      <td>
          </td>
      <td><label>Branch</label></td>
      <td>
          <asp:DropDownList ID="Ddl_Branch" runat="server" Width="100%" 
              TabIndex="1" >
          </asp:DropDownList>
          </td>
      <td></td>
      <td><label>Scheme</label>
          </td>
      <td>
          <asp:DropDownList ID="Ddl_Scheme" runat="server" TabIndex="2" Width="100%">
                  </asp:DropDownList>
          </td>
      <td>
          </td>
      </tr>
      
          <tr>
 <td>
     </td>
 <td><label>From Date</label></td>
 <td>
  <asp:TextBox ID="Txt_FromDate" runat="server" Width="100%" TabIndex="3"></asp:TextBox>
   <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
              TargetControlID="Txt_FromDate">
          </asp:CalendarExtender>
          
 </td>
 
 <td></td>
 <td>
     <label>
     To Date</label></td>
 <td>
     <asp:TextBox ID="Txt_ToDate" runat="server" TabIndex="4" Width="100%"></asp:TextBox>
     <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
         TargetControlID="Txt_ToDate">
     </asp:CalendarExtender>
    
             </td>
 <td>
     </td>
 </tr>
          <tr>
              <td>
                  </td>
              <td>
                  <label>Account No</label></td>
              <td>
                  <asp:TextBox ID="Txt_AccNo" runat="server" AutoPostBack="True" TabIndex="5" 
                      Width="100%"></asp:TextBox>
              </td>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
          </tr>
          <tr>
              <td>
                  </td>
              <td>
                  <label>
                  Customer Name</label></td>
              <td colspan="3">
                  <asp:TextBox ID="Txt_CustName" runat="server" TabIndex="7" Width="100%"></asp:TextBox>
              </td>
              <td>
                  </td>
              <td>
                  </td>
          </tr>
          <tr>
              <td>
                  </td>
              <td>
                  <label>
                  Customer Address</label></td>
              <td colspan="3">
                  <asp:TextBox ID="Txt_CustAddress" runat="server" TabIndex="8" 
                      TextMode="MultiLine" Width="100%"></asp:TextBox>
              </td>
              <td>
                  </td>
              <td>
                  </td>
          </tr>
      
         
      
      
       <tr>
      <td >
          </td>
      <td ></td>
      <td>
          
           </td>
      <td ></td>
      <td >
          
          </td>
      <td ></td>
      <td >
          </td>
      </tr>
     
          <tr>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
          </tr>
     
          <tr>
              <td>
                  </td>
              <td>
                  </td>
              <td align="center" colspan="3">
                  <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  <asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
              </td>
              <td>
                  </td>
              <td>
                  </td>
          </tr>
          <tr>
              <td>
                  </td>
              <td>
                  </td>
              <td>
              
              <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    
                    <div ID="progress">
                        <img alt="progress" src="Images/progress.gif" /> Please Wait...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
              <td>
                  </td>
          </tr>
     
     </table> 
 
   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
