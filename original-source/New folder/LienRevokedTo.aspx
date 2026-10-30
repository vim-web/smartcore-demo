<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LienRevokedTo.aspx.vb" Inherits="SMARTCORE_UI.LienRevokedTo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  
   
    
     <style type="text/css">
         .auto-style1
         {
             height: 23px;
         }
     </style>
   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; margin-top:10%;" >                
    
    <legend>LIEN RELEASED </legend>

    <table width="100%">
        <tr>
            <td width="2%" > </td>
            <td width="48%" > <label>Branch</label></td>
            <td width="48%" > 
                <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                    TabIndex="4" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%" > </td>
           
        </tr>
         
        
         <tr>
            <td width="2%" > &nbsp;</td>
            <td width="48%" > 
                <label>Loan/Gsd</label></td>
            <td width="48%" > 
                <asp:DropDownList ID="CmbOption" runat="server" AutoPostBack="True" 
                    TabIndex="4" Width="100%">
                    <asp:ListItem Value="37">GROUP SAVINGS DEPOSIT </asp:ListItem>
                    <asp:ListItem Value="0">LOAN APPLICATION</asp:ListItem>
                </asp:DropDownList>
             </td>
            <td width="2%" > 
                &nbsp;</td>
            
        </tr>
         
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
               <label>
                   <asp:Label ID="LoanType" runat="server" Text="Loan Type"></asp:Label>  </label></td>
             <td width="48%">
                 <asp:DropDownList ID="CmbLoantype" runat="server" AutoPostBack="True" 
                     TabIndex="4" Width="100%">
                 </asp:DropDownList>
             </td>
             <td width="2%">
                 &nbsp;</td>
        </tr>
         
         <tr>
             <td width="2%">
             </td>
             <td width="48%">
                 <label>Account No</label>
             </td>
             <td width="48%">
                 <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" 
                     CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
             </td>
             <td width="2%">
                 &nbsp;</td>
        </tr>
         
         <tr>
            <td width="2%"> </td>
            <td width="48%" align="right"> 
                <asp:ImageButton ID="CmdShow" runat="server" Height="25px" ImageUrl="~/Images/Show.png" />
             </td>
            <td width="48%" align="left"> 
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/CloseBtn.png" />
                <br />
             </td>
            <td width="2%"> </td>
           
        </tr>        
       <tr>
            <td width="2%" > </td>
            <td width="48%" colspan="2" style="width: 96%" > 
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                       <%-- <div id="blur">
                        </div>--%>
                        <div id="progress">
                            <img alt="progress" src="Images/progress.gif" />
                            Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
            <td width="2%" > </td>
           
        </tr>
        <tr>
            <td width="2%" class="auto-style1" > </td>
            <td width="48%" class="auto-style1" > </td>
            <td width="48%" class="auto-style1" > </td>
            <td width="2%" class="auto-style1" > </td>
           
        </tr>
        <tr>
            <td width="2%" > </td>
            <td width="48%" colspan="2" style="width: 96%" > 
                <asp:GridView ID="DgvRevoke" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Id,Type" PageSize="5" TabIndex="8" Width="100%">
                    <RowStyle Height="20px" />
                    <Columns>
                        <asp:CommandField SelectText="Revoke" ShowSelectButton="True">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" Width="5%" />
                        </asp:CommandField>
                       <%-- <asp:CheckBoxField DataField="Revoked">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Width="2%" />
                        </asp:CheckBoxField>  --%>                     
                       
                        <asp:BoundField DataField="Id" HeaderText="Id" >
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Width="42%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" />
                        </asp:BoundField>
                       <asp:BoundField DataField="Type" HeaderText="Type">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Width="42%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" />
                        </asp:BoundField>
                       <asp:BoundField DataField="Details" HeaderText="Detail">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Width="42%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" />
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F4FF" />
                </asp:GridView>
            </td>
            <td width="2%" > </td>
           
        </tr>
        <tr>
            <td width="2%" > </td>
            <td width="48%" > &nbsp;</td>
            <td width="48%" > </td>
            <td width="2%" > </td>
           
        </tr>
        </table>
       
            </div>

   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
