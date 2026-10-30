<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DebRpt_Series.aspx.vb" Inherits="SMARTCORE_UI.DebRpt_Series" %>

  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="Scripts/Common.js"></script> 
    <style type="text/css">
        .style3
        {
        }




select 
{
	border: 1px solid #c4c4c4;
    font-size: 13px;
    padding: 4px 4px 4px 4px;
    border-radius: 4px;
    -moz-border-radius: 4px;
    -webkit-border-radius: 4px;
    box-shadow: 0px 0px 8px #d9d9d9;
    -moz-box-shadow: 0px 0px 8px #d9d9d9;
    -webkit-box-shadow: 0px 0px 8px #d9d9d9;
}
        .style6
        {
            width: 159px;
        }
        

.btn {
  background: #3498db;
  background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
  background-image: -moz-linear-gradient(top, #3498db, #2980b9);
  background-image: -ms-linear-gradient(top, #3498db, #2980b9);
  background-image: -o-linear-gradient(top, #3498db, #2980b9);
  background-image: linear-gradient(to bottom, #3498db, #2980b9);
  -webkit-border-radius: 2px;
  -moz-border-radius: 2px;
  border-radius: 2px;
  
  font-family: Arial;
  color: #ffffff;
  font-size: 16px;
  padding: 5px ;
  text-decoration: none;
}

        .style10
        {
            width: 274px;
        }
        .style11
        {
            width: 396px;
        }
        .style12
        {
            width: 42px;
        }
        .style13
        {
            width: 142px;
        }
    </style>

 <script type="text/javascript">
        function validateAdd() {         
            var objDDl = document.getElementById('<%=ddl_Scheme.ClientID%>');
            var SchAccH = objDDl.options[objDDl.selectedIndex].text;

            if (SchAccH == "Select") {
                alertify.error('Please select the scheme'); return false;
            }

            var objDdlSeries = document.getElementById('<%=ddl_Series.ClientID%>');
            var Series = objDdlSeries.options[objDdlSeries.selectedIndex].text;
          
           var FrmMonth = document.getElementById('<%=TxtFrmMonth.ClientID %>');
           var ToMonth = document.getElementById('<%=TxtToMonth.ClientID %>');
           var Redemption = document.getElementById('<%=TxtRedemption.ClientID %>');
           var SubSeries = document.getElementById('<%=TxtSubSeries.ClientID %>');
                     
          
            if (Series == "Select") {
                alertify.error('Please select the Series'); return false;
            }

          if (SubSeries == '') {
              alertify.error('Please Enter the subseries'); return false;
          }        
            if (Redemption.value == '')
            { alertify.error('Please Enter The Redemption Value'); Redemption.focus(); return false; }
            if (FrmMonth.value == '')
            { alertify.error('Please Enter The From Month'); FrmMonth.focus(); return false; }
            if (ToMonth.value == '')
            { alertify.error('Please Enter The To Month'); ToMonth.focus(); return false; }
                                
        }

        function validateSave() {

//            var objDDl = document.getElementById('<%=ddl_Scheme.ClientID%>');
//            var SchAccH = objDDl.options[objDDl.selectedIndex].text;
//            if (SchAccH == "Select") {
//                alertify.error('Please select the scheme');
//                return false;
//            }

//            var FrmMonth = document.getElementById('<%=TxtFrmMonth.ClientID %>');
//            var ToMonth = document.getElementById('<%=TxtToMonth.ClientID %>');
//            var Redemption = document.getElementById('<%=TxtRedemption.ClientID %>');
//            var SubSeries = document.getElementById('<%=TxtSubSeries.ClientID %>');

//            var objDdlSeries = document.getElementById('<%=ddl_Series.ClientID%>');
//            var Series = objDdlSeries.options[objDdlSeries.selectedIndex].text;

//            if (Series == "Select") {
//                alertify.error('Please select the Series'); return false;
//            }

//            if (SubSeries == '') {
//                alertify.error('Please Enter the subseries'); return false;
//            } 
//            if (FrmMonth.value == '')
//            { alertify.error('Please Enter The From Month'); FrmMonth.focus(); return false; }
//            if (ToMonth.value == '')
//            { alertify.error('Please Enter The To Month'); ToMonth.focus(); return false; }

//            if (Redemption.value == '')
//            { alertify.error('Please Enter The Redemption Value'); Redemption.focus(); return false; }
           
            var HidValue = document.getElementById('<%=hid_status.ClientID %>');
            if (HidValue.value == "NEW") {
                { alertify.error('Please Add the details');  return false; }
            }
           
        }
     
     

</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;" > 
<legend> Debenture Sub Series</legend> 
    <table style="width: 100%;">
        <tr>
            <td class="style12">
                &nbsp;
            </td>
            <td class="style13">
                &nbsp;
            </td>
            <td class="style10">
                &nbsp;</td>
            <td class="style6">
                &nbsp;
            </td>
            <td class="style11">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;
                </td>
            <td class="style13">                
                <asp:Label ID="LblScheme" runat="server" Text="Scheme"></asp:Label>
            </td>
            <td class="style10">
                <asp:DropDownList ID="ddl_Scheme" runat="server" Width="100%" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td class="style13">              
                <asp:Label ID="LblSeries" runat="server" Text="Series"></asp:Label>
                </td>
            <td class="style10">
                <asp:DropDownList ID="ddl_Series" runat="server" Width="70%" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;
            </td>
            <td class="style13">
                <asp:Label ID="LblSubSeries" runat="server" Text="Sub Series"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="TxtSubSeries" runat="server" Width="49%"></asp:TextBox>
            </td>
            <td class="style6">
                <asp:Label ID="LblRedemption" runat="server" Text="Redemption Value"></asp:Label>
            </td>
            <td class="style11">
                <asp:TextBox ID="TxtRedemption" runat="server" Width="49%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style13">
                <asp:Label ID="LblFrmMonth" runat="server" Text="From Month"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="TxtFrmMonth" runat="server" Width="50%"></asp:TextBox>
            </td>
            <td class="style6">
                <asp:Label ID="LblToMonth" runat="server" Text="To Month"></asp:Label>
            </td>
            <td class="style11">
                <asp:TextBox ID="TxtToMonth" runat="server" Width="50%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style13">
                <asp:Label ID="LblFrmDays" runat="server" Text="From Days"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="TxtFrmDays" runat="server" Width="50%"></asp:TextBox>
            </td>
            <td class="style6">
                <asp:Label ID="LblToDays" runat="server" Text="To Days"></asp:Label>
            </td>
            <td class="style11">
                <asp:TextBox ID="TxtToDays" runat="server" Width="50%"></asp:TextBox>
                <asp:Button ID="but_add" runat="server" BorderStyle="None" CssClass="btn" 
                    Height="30" OnClientClick="return validateAdd();" Text="Add" Width="70" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style11">
                &nbsp;</td>
            <td>
               <input type="hidden" runat="server" value="NEW" id="hid_status" /></td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style3" colspan="4" rowspan="3">
                <asp:GridView ID="GV_RptDetails" runat="server" 
                    AutoGenerateColumns="False" CssClass="grid-view" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SchCode" HeaderText="Scheme" />
                        <asp:BoundField DataField="SeriesCode" HeaderText="Series Code" />
                        <asp:BoundField DataField="Series" HeaderText="Series"/>  
                        <asp:BoundField DataField="SubSeries" HeaderText="Sub Series" />                     
                        <asp:BoundField DataField="FromMonth" HeaderText="FromMonth" />
                         <asp:BoundField DataField="ToMonth" HeaderText="ToMonth" />
                          <asp:BoundField DataField="FromDays" HeaderText="FromDays" />
                          <asp:BoundField DataField="ToDays" HeaderText="ToDays" />
                           <asp:BoundField DataField="Redemption" HeaderText="Redemption Value" />                         
                           <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/del.jpg" 
                            ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style11">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style13">
                <asp:Label ID="LblRemarks" runat="server" Text="Remarks"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="TxtRemarks" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td class="style6">
                &nbsp;</td>
            <td class="style11">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style11">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" OnClientClick="return validateSave();" 
                    TabIndex="35" />
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" TabIndex="37" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>