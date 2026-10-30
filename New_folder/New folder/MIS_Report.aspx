<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="MIS_Report.aspx.vb" Inherits="SMARTCORE_UI.MIS_Report" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="Scripts/Common.js"></script> 
<style type="text/css">
   .txt_class{color:#7465C9; font-family:Arial; font-size:90%; font-weight:600;}
   .grid-view tr.rowHover:hover{background-color:#DFD9FD; font-family:Arial; cursor:pointer; }
.tr-css{border-bottom:1px solid #000000;}
 
    </style>
<script type="text/javascript">

    window.onload = function() {
        showhide();
    }
    function GetQueryStringParams(sParam) {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }

    function showhide() {
        var report = GetQueryStringParams('Type');
        var type_1 = document.getElementById('<%=ddl_acc_report_2.ClientID %>');
        var dt = document.getElementById('td_id');
       
        if (report == 'Increase' || report == 'Employee') {
            type_1.style.display = 'none';
            dt.innerHTML = '';
        }
        else { type_1.style.display = 'block'; dt.innerHTML = 'A/C Type'; }
    }


    function Date_AddValid() {
        var input = document.getElementById('<%=txt_date.ClientID %>');
       if (input.value == '') {
           alertify.error('Please Select Transaction Date')
           return false;
       }
       return true;
    }


    function validate(txtdate) {
        var date;
        if (txtdate.value != '') {
            if (validatedate) {
                var now = new Date();
                var dat = txtdate.value;
                var sd = dat.split('/');
                var select_date = new Date(sd[2], sd[1] * 1 - 1, sd[0]);
                if (now.getTime() > select_date.getTime()) {
                   
                    var grid = document.getElementById('<%=Gridreportdata.ClientID%>');
                    for (var i = 1; grid.rows.length > i; i++) {

                        date = grid.rows[i].cells[0].innerHTML;
                       
                        valida(txtdate, date);
                    }
                }
                else {
                    alertify.error("Invalid date format! Please enter a valid date!ss");
                    txtdate.value = '';
                    return false;
                }

            }
            else {
                alertify.error("Invalid date format! Please enter a valid date!dd");
                txtdate.value = '';
                return false;
            }
        }
        else {
            alertify.error("Invalid date format! Please enter a valid date!ff");
            txtdate.value = '';
            return false;
        }

    }
    
    function valida(inpt,grd_date) {
        var dat = inpt.value;
        var sd = dat.split('/');
    var gd=grd_date.split('/');
    if(sd.length==3) {
        var select_date = new Date(sd[2], sd[1] * 1 - 1, sd[0]);
        var gr_dt = new Date(gd[2], gd[1] * 1 - 1, gd[0]);
       
        if (select_date.setHours(0,0,0,0) == gr_dt.setHours(0,0,0,0)) {
            alertify.error('Already selected This  Date');
            inpt.value = '';
            return false;
        }           
    }
    else {
//        alert("Invalid date format! Please enter a valid date!gg");
//        inpt.value = '';
        return false;
    }
}

function SelectAll(id) {
    var frm = document.forms[0];
   
    for (i = 0; i < frm.elements.length; i++) {

        if (frm.elements[i].type == "checkbox") {

            frm.elements[i].checked = document.getElementById(id).checked;

        }

    }

}

function acc_validate() { 
    var ddl_acc = document.getElementById('<%=ddl_acc_report_1.ClientID %>');
    var ddl = document.getElementById('<%=ddl_acc_report_2.ClientID %>');
    if (ddl_acc.value==ddl.value)
{
    alertify.error('Two A/c Type is Same');
   
    return false;
}

return true;
}

function isPastDate(value) {
    var now = new Date;
    var target = new Date(value);

    if (now.getFullYear() > target.getFullYear()) {
        return true;
    }
     if (now.getMonth() > target.getMonth()) {
        return true;
    }
     if (now.getDate() > target.getDate()) {
        return true;
    }

    return false;
}



function Valid_checkBoxes() {
    var gridView = document.getElementById("<%=grd_branchdetails.ClientID %>");
    var checkBoxes = gridView.getElementsByTagName("input");
    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
            return true;
        }
        
    }
    return false;
}


function valid_final() {
    var RptType = GetQueryStringParams('Type');
    if (RptType == 'MisReort' || RptType == 'Balance') {
        if (!acc_validate())
        { return false; }
    }
    if (!Valid_checkBoxes()) {
        alertify.error('Please Select Branch');
    return false; }
    var grid = document.getElementById('<%=Gridreportdata.ClientID%>');
     if (grid.rows[1].cells[0].innerHTML == '&nbsp;') {
         alertify.error('Please Add Date');
         return false;
     }
    return true;
}

function gridrowclick(grid) {
   // var checkbox = row.getElementsByTagName('input');
    // alert(grid.rows.length)

    for (var i = 0; grid.rows.length - 1 > i; i++) {
        
        var tr = grid.rows[i];
        tr.onclick = function() {
           
            GetRows(tr.rowIndex);
        }
    }
   
}

function GetRows(cell, Index) {
 
    var grid = cell.parentNode;
   
    var checkbox = grid.getElementsByTagName('input');
    
    for (var i = 0; checkbox.length > i; i++) {
        if (checkbox[i].type == "checkbox") {
            if (checkbox[i].checked == false) {
                checkbox[i].checked = true;
            }
            else{
                checkbox[i].checked = false;
            }
        }
     }
    
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div style="width:100%; margin:0 auto;">
<div style="width:1000px; margin:0 auto; padding:20px;">


<div class="gradientBoxesWithOuterShadows" style="width: 100%; margin-top:40px;  font-family: Tahoma" >
<legend>MIS REPORTS</legend>                
    
 
 <div style="width:95%; height:30px; padding:20px;  ">
 
 </div>
 
 
 
 <table width = "100%"> 
<tr>
	<td width ="2%"></td>
	<td width ="15%"></td>
	<td width ="33%"></td>
	<td width ="2%"></td>
	<td width ="15%"></td>
	<td width ="33%"></td>
	<td width ="2%"></td>
</tr>
     <tr class="tr-css">
         <td></td>
         <td class="txt_class" >A/c Type</td>
         <td>
             <asp:DropDownList ID="ddl_acc_report_1" runat="server" style="width:100%">
                 <asp:ListItem>Select</asp:ListItem>
             </asp:DropDownList>
         </td>
         <td></td>
         <td class="txt_class" id="td_id" >A/c Type</td>
         <td>
             <asp:DropDownList ID="ddl_acc_report_2" runat="server" 
                 onchange="acc_validate()" style="width:100%">
                 <asp:ListItem>Select</asp:ListItem>
             </asp:DropDownList>
         </td>
         <td></td>
     </tr>
     <tr>
         <td></td>
         <td>&nbsp;&nbsp;</td>
         <td></td>
         <td></td>
         <td></td>
         <td></td>
         <td></td>
     </tr>
          <tr>
         <td height="40px;"></td>
         <td colspan="2" rowspan="2"  valign="top" height="100" style="overflow:auto;" >
             <asp:GridView ID="grd_branchdetails" runat="server" AutoGenerateColumns="False"  
                 CssClass="grid-view" RowStyle-CssClass="rowHover" Width="100%"  >
                 <Columns>
                     <asp:TemplateField>
                         <HeaderTemplate>
                             <asp:CheckBox ID="chkSelectAll" runat="server" 
                                 onClick="SelectAll('chkSelectAll')" Text="ALL" />
                         </HeaderTemplate>
                         <ItemTemplate>
                             <asp:CheckBox ID="chkSelectAll1" runat="server" AutoPostBack="true" />
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="Br_Code" HeaderText="Code "  />
                     <asp:BoundField DataField="Br_Name" HeaderText="Branch Name" />
                 </Columns>
             </asp:GridView>
              </td>
         <td></td>
         <td class="txt_class" >Date</td>
         <td>
         <asp:CalendarExtender runat="server" ID="Calander_ex" TargetControlID="txt_date" Format="dd/MM/yyyy" ></asp:CalendarExtender>
             <asp:TextBox ID="txt_date" runat="server" Onchange="validate(this)" 
                 style="width:70%"></asp:TextBox>
             <asp:ImageButton ID="but_add" runat="server" ImageUrl="~/Images/AddBtn.png"  OnClientClick="return Date_AddValid()"
                 Width="68px" Height="22px"  />
              </td>
         <td></td>
     </tr>
          <tr>
         <td></td>
         <td></td>
         <td colspan="2" style="vertical-align:top; overflow:auto;" ><input type="hidden" value="NEW" id="hid_gird_row" runat="server" />
             <asp:GridView ID="Gridreportdata" runat="server" AutoGenerateColumns="False" 
                 CssClass="grid-view" RowStyle-CssClass="rowHover" 
                 Width="100%">
                 <Columns>
                     <asp:BoundField DataField="Date" HeaderText="Report Date" />
                     <asp:ButtonField ButtonType="Image" CommandName="Delete" 
                         ImageUrl="~/Images/del.jpg" Text="Delete" />
                 </Columns>
             </asp:GridView>
              </td>
         <td></td>
     </tr>
     <tr>
         <td></td>
         <td colspan="5" align="center">
             <asp:ImageButton ID="CmdShow" runat="server" Height="32px" 
                 ImageUrl="~/Images/Show.png" OnClientClick="return valid_final()" />
             &nbsp;
             <asp:ImageButton ID="CmdClose" runat="server" Height="30px" 
                 ImageUrl="~/Images/CloseBtn.png" />
         </td>
         <td></td>
     </tr>
</table>

    
   
       </div>
       
       </div>
</div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
