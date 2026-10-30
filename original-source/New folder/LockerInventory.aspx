<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LockerInventory.aspx.vb" Inherits="SMARTCORE_UI.LockerInventory" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
 <script type='text/javascript'>

    function ValidateSave() {

        var Ctl = document.getElementById('<%=TxtLockerNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('LockerNo Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtkeyNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('LockerKeyNo Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtRowNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Row Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtColNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Column No Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:15%; margin-right:15%;margin-top:10%;">
        <legend>Locker Settings</legend>
        <table width="100%">
        <tr>
        <td width="2%"></td>
        <td width="23%"></td>
         <td width="23%"></td>
          <td width="2%"></td>
          <td width="23%"></td>
         <td width="23%"></td>
          <td width="2%"></td>
        </tr>
          <tr>
        <td width="2%"></td>
        <td width="23%"><label>Branch</label></td>
         <td width="23%">
             <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                 AutoPostBack="True">
             </asp:DropDownList>
              </td>
          <td width="2%"></td>
          <td width="23%"><label>Locker</label></td>
         <td width="23%">
             <asp:DropDownList ID="CmbLricode" runat="server" Width="100%">
             </asp:DropDownList>
              </td>
          <td width="2%"></td>
        </tr>
          <tr>
        <td width="2%"></td>
        <td width="23%"><label>LockerNo</label></td>
         <td width="23%">
             <asp:TextBox ID="TxtLockerNo" runat="server"></asp:TextBox>
              </td>
          <td width="2%"></td>
          <td width="23%"><label>LockerKeyNo</label></td>
         <td width="23%">
             <asp:TextBox ID="TxtkeyNo" runat="server"></asp:TextBox>
              </td>
          <td width="2%"></td>
        </tr>
          <tr>
        <td width="2%"></td>
        <td width="23%"><label>LockerRowNo</label></td>
         <td width="23%">
             <asp:TextBox ID="TxtRowNo" runat="server"></asp:TextBox>
              </td>
          <td width="2%"></td>
          <td width="23%"><label>LockerColNo</label></td>
         <td width="23%">
             <asp:TextBox ID="TxtColNo" runat="server"></asp:TextBox>
              </td>
          <td width="2%"></td>
        </tr>
        <tr>
        <td width="2%"></td>
        <td width="23%"></td>
         <td width="23%"></td>
          <td width="2%"></td>
          <td width="23%"></td>
         <td width="23%"></td>
          <td width="2%"></td>
        </tr>
         <tr>
        <td width="3%"></td>
        <td width="23%">&nbsp;</td>
         <td align="right" colspan="3" style="width: 46%"> <asp:ImageButton ID="BtnSave" 
                 runat="server" ImageUrl="~/Images/Save.png" 
                 onclientclick="return ValidateSave();"/> 
     <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
         <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" 
        PostBackUrl="~/Main.aspx" />
                 
          
     </td>    
                 
          <td width="3%">&nbsp;</td>
        </tr> 
        
        </table>
        </div>
</asp:Content>
