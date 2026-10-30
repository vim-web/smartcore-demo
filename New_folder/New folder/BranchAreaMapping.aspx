<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BranchAreaMapping.aspx.vb" Inherits="SMARTCORE_UI.BranchAreaMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   
     <script type="text/javascript">
         function ValidateSave() {

             var valid = document.getElementById('<%=DrpRegion.ClientID%>');
             if (valid.value == "SELECT") {

                 alertify.error('Please Select Region');
                 return false;
             }
             var valid = document.getElementById('<%=DrpArea.ClientID%>');
             if (valid.value.length == 0) {
                 alertify.error('Please Select Area');
                 return false;
             }
             return true;
         }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
        .auto-style2 {
            width: 470px;
        }
        .auto-style3 {
            height: 21px;
        }
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
    <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:25%; margin-right:25%;" > 
<legend>Branch Area Mapping</legend>               
<br />
<table width="100%">
<tr>
    <td width="35%"></td>
<td width="40%"></td>
<td width="25%"></td>
</tr>
    <tr>
        <td >
            <label>
            Region</label></td>
<td >    
    <asp:DropDownList ID="DrpRegion" runat="server" AutoPostBack="True" Width="99%">
    </asp:DropDownList></td>

    </tr>
     <tr>
         <td >
             <label>
             Area</label></td>
<td >

    <asp:DropDownList ID="DrpArea" runat="server" Width="100%">
    </asp:DropDownList></td>
    <td></td>
    </tr>
       
<tr>
<td></td>
    <td align="center" >
        &nbsp;</td>
<td width="25%">
    <asp:HiddenField ID="hid_RegCode" runat="server" />
    </td>
</tr>
</table>
   </div>   


    <table  width="100%">
        <tr >
            <td width="2%">
                &nbsp;</td>
            <td width="96%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="96%">
                <div style="min-height:300px;max-height:400px;overflow:scroll;">
                    <asp:GridView ID="GvBranchDetails" runat="server" AutoGenerateColumns="False" 
                        CssClass="grid-viewNew" EnableModelValidation="True" ShowFooter="True" 
                        Width="100%">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkView" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Br_Code" HeaderText="Branch Code" />
                            <asp:BoundField DataField="Br_Name" HeaderText="Branch Name" />
                            <asp:BoundField DataField="Br_Address" HeaderText="Branch Address" />
                            <asp:BoundField DataField="Br_Phone1" HeaderText="Branch Phone" />
                            <asp:BoundField DataField="Br_Area" HeaderText="Branch Area" />
                            <asp:BoundField DataField="Br_Region" HeaderText="Branch Region" />
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
                    style="height: 33px" TabIndex="56" />
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" 
                    style="margin-top: 0px" TabIndex="57" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>




    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
