<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Emp_PayScale.aspx.vb" Inherits="SMARTCORE_UI.Emp_PayScale" 
    title="Untitled Page" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript" src="Scripts/Common.js"></script>
<style type="text/css">
   .txt_class{color:#7465C9; font-family:Arial;  font-size:90%; font-weight:600; }
   .grid-view tr.rowHover:hover{background-color:#DFD9FD; font-family:Arial; color:#FFF; }
   .grid-view tr.rowHover{color:#4C3CA4;}
</style>
<script type="text/javascript">
function validate()
{
    var salary = document.getElementById('<%=txt_Salary.ClientID %>');
    var Increment = document.getElementById('<%=txt_Increment.ClientID %>');
if (salary.value == '') {
    alertify.error('Please Specify Salary'); return false;
}
if (Increment.value == '') {
    alertify.error('Please Specify Increment'); return false;
}
if (!gidValidate())
{  return false;}
return true;
}
function SaveValidate() {
    var status = document.getElementById('<%=hid_Grd_status.ClientID %>');
    var date = document.getElementById('<%=txt_Date.ClientID %>');
    if (date.value == '')
    { alertify.error('Please Specify Date'); return false; }

    if (status.value == 'NEW')
    { alertify.error('Please Add Salary and Increment'); return false; }
    return true;
}

function gidValidate() {
    var salary = document.getElementById('<%=txt_Salary.ClientID%>');
    var grid = document.getElementById('<%=grd_emp_payScale.ClientID%>');
    
    for (var i = 1; i < grid.rows.length; i++) {
        var GrdSalary = grid.rows[i].cells[0].innerHTML;
        
        if (GrdSalary == salary.value) {
            alertify.error('This Salary Already Exists')
            salary.value = '';
            return false;
        }
    }
    return true;
}

function validatenumerics(key) {
    //getting key code of pressed key
    var keycode = (key.which) ? key.which : key.keyCode;
    //comparing pressed keycodes

    if (keycode > 31 && (keycode < 48 || keycode > 57)) {
        alertify.error(" You can enter only characters 0 to 9 ");
        return false;
    }
    else return true;


}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div style="width:100%; margin:0 auto; margin-top:50px;">
<div class="gradientBoxesWithOuterShadows" style="width:1000px; margin:0 auto; padding:20px;">
<legend>Employe Pay Scale</legend>
 <%--<div style="width:100%;height:40px; padding-top:20px; text-align:center; font-weight:bold; color:#7B67EB; " >
     Employe Pay Scale</div>--%>
    <table width="100%" style="border-bottom:1px solid #C2C1C1;"  >
 <tr>
 <td width="15%" ></td>
 <td width="15%" class="txt_class" >Designation</td>
 <td width="40%">
     <asp:DropDownList ID="ddl_Designation" runat="server" style="width:80%" 
         AutoPostBack="True">
          <asp:ListItem>Select</asp:ListItem>
     </asp:DropDownList>
     </td>
 <td width="15%" ></td>
 
 </tr> 
 
 <tr>
 <td width="15%" ></td>
 <td width="15%" class="txt_class" >Date</td>
 <td width="40%">
 <asp:CalendarExtender ID="calander_ex" runat="server" TargetControlID="txt_Date" Format="dd/MM/yyyy"></asp:CalendarExtender>
     <asp:TextBox ID="txt_Date" Width="78%" runat="server" AutoPostBack="True"></asp:TextBox>
     </td>
 <td width="15%" ></td>
 
 </tr> 
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td class="txt_class" width="15%">
                &nbsp;</td>
            <td width="40%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
 </table>
  <table width="80%" style="margin:0 auto;" >
 <tr>
 <td width="20%" ></td>
 <td width="20%" ></td>
 <td width="20%" class="txt_class" >
     &nbsp;</td>
     <td width="20%" ></td>
 <td width="20%" ></td>
 </tr> 
 <tr>
 <td  class="txt_class" >Salary</td>
 <td>
     <asp:TextBox ID="txt_Salary" Width="80%" runat="server" onchange="gidValidate()" onkeypress="return validatenumerics(event);" ></asp:TextBox>
     </td>
     <td  class="txt_class" >
     Increment</td>
<td>
    <asp:TextBox ID="txt_Increment" Width="80%" runat="server" onkeypress="return validatenumerics(event);" ></asp:TextBox>
     </td>
<td>
    <asp:ImageButton ID="but_add"  runat="server" ImageUrl="~/Images/AddBtn.png" OnClientClick="return validate()" />
     </td>
 </tr>
      <tr>
          <td colspan="5" style="padding-top:30px;">
              <input id="hid_Grd_status" runat="server" type="hidden" value="NEW" />
              <asp:GridView ID="grd_emp_payScale" runat="server" AutoGenerateColumns="False" 
                  CssClass="grid-view" RowStyle-CssClass="rowHover" Width="90%"  >
                  <RowStyle CssClass="rowHover" />
                  <Columns>
                      <asp:BoundField DataField="Emp_Salary" HeaderText="Salary" />
                      <asp:BoundField DataField="Emp_Increment" HeaderText="Increment" />
                      <asp:ButtonField ButtonType="Image" CommandName="Delete" 
                          ImageUrl="~/Images/del.jpg" Text="Delete" />
                  </Columns>
              </asp:GridView>
          </td>
      </tr>
      <tr>
          <td>
              &nbsp;</td>
          <td colspan="2" style="padding-top:20px;">
              <%--<asp:Button ID="but_Save" runat="server" Text="Save" CssClass="btn" Width="100" 
                  Height="32" BorderStyle="None" />
              <asp:Button ID="but_Close" runat="server" Text="Close" CssClass="btn" 
                  Width="100" Height="32"  style="margin-left:30px;" BorderStyle="None"/>--%>
<asp:ImageButton ID="but_Save"  runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return SaveValidate()"  style="margin-left:90px;" />
             <asp:ImageButton ID="but_Close"  runat="server" ImageUrl="~/Images/Close.png"  style="margin-left:30px;" /></td>
              &nbsp;</td>
      </tr>
 </table>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
