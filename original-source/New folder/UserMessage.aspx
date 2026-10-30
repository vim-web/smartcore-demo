<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="UserMessage.aspx.vb" Inherits="SMARTCORE_UI.UserMessage" 
    title="User Message" %>
             <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
<script type="text/javascript">
    function ValidateSave() {
        var Ctl = document.getElementById('<%=Txt_DateFrom.ClientID%>');
        if (Ctl.value == "") {
            alertify.error("Enter Date...");
            return false;
        }
        var Ctl = document.getElementById('<%=TxtSub.ClientID%>');
        if (Ctl.value == "") {
            alertify.error("Enter Subject...");
            return false;
        }
        var Ctl = document.getElementById('<%=TxtMsg.ClientID%>');
        if (Ctl.value == "") {
            alertify.error("Enter Message...");
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;" >                
    <legend>User Messages</legend> 
    <table width="100%">
        <tr>
        <td width="10%">&nbsp;</td>
         <td width="10%" colspan="2" style="width: 20%">
             <table width="100%">
                 <tr>
                     <td width="30%">
                         <label>
                         Date</label></td>
                     <td width="70%">
                         <asp:TextBox ID="Txt_DateFrom" runat="server" BackColor="White" 
                             CssClass="twitterStyleTextbox" OnChange="validatedate(this);" TabIndex="4" 
                             Width="50%"></asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                             TargetControlID="Txt_DateFrom">
                         </asp:CalendarExtender>
                     </td>
                 </tr>
                 <tr>
                     <td width="30%">
                         &nbsp;</td>
                     <td width="70%">
                         &nbsp;</td>
                 </tr>
             </table>
 </td>
           <td width="10%">&nbsp;</td>
</tr>
        <tr>
            <td width="10%">
                &nbsp;</td>
            <td width="10%">
                &nbsp;</td>
            <td width="10%">
                &nbsp;</td>
            <td width="10%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="10%">
            </td>
            <td width="10%">
                <div style="width:300px; float:left; height:200px; margin-left:50px; overflow:auto">
                    <asp:GridView ID="grd_emp_list" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField >
                            <HeaderTemplate>

                <asp:CheckBox ID="cbSelectAll" runat="server" Text="Select All"  ForeColor="White" AutoPostBack="true" />
 
            </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" 
                                        oncheckedchanged="chkSelect_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                    BorderWidth="1px" ForeColor="White" Width="30%" />
                                <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                    Font-Size="Small" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Pkc_Description" HeaderText="Designation">
                                <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                    BorderWidth="1px" ForeColor="White" Width="50%" />
                                <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                    Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pkc_Code" HeaderText="Code">
                                <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                    BorderWidth="1px" ForeColor="White" Width="20%" />
                                <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                    Font-Size="Small" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
            <td width="10%" align="left">
               <div style="width:250px; float:right; height:200px; margin-left:50px; overflow:auto">
                    <asp:GridView ID="grd_user_list" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Emp_Name" HeaderText="Employe">
                                <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                    BorderWidth="1px" ForeColor="White" Width="50%" />
                                <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                    Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Emp_ID" HeaderText="Code">
                                <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                    BorderWidth="1px" ForeColor="White" Width="50%" />
                                <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                    Font-Size="Small" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
            <td width="10%">
            </td>
        </tr>
 <tr>
            <td width="10%">
                </td>
            <td width="10%" colspan="2" style="width: 20%">
            <table width="100%">
            <tr>
            <td width="30%"><label>Subject</label></td>
            <td width="70%">
                <asp:TextBox ID="TxtSub" runat="server" CssClass="twitterStyleTextbox" 
                             Width="50%"></asp:TextBox></td>
            </tr>
                <tr>
                    <td width="30%">
                        <label>
                        Message</label></td>
                    <td width="70%">
                        <asp:TextBox ID="TxtMsg" runat="server" CssClass="twitterStyleTextbox" 
                            TextMode="MultiLine" Width="50%"></asp:TextBox>
                    </td>
                </tr>
            </table>
                </td>
            <td width="10%">
                </td>
        </tr>
        <tr>
            <td width="10%">
                </td>
            <td width="10%">
                </td>
            <td align="center" width="10%">
                </td>
            <td width="10%">
                </td>
        </tr>
        <tr>
            <td width="10%">
                </td>
            <td width="10%" colspan="2" style="width: 20%" align="center">
             <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
            onclick="CmdSave_Click1" OnClientClick="return ValidateSave();" />
        <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
        <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" 
            PostBackUrl="~/Main.aspx"/>
                </td>
            <td width="10%">
                </td>
        </tr>
        <tr>
            <td width="10%">
                </td>
            <td width="10%">
                </td>
            <td align="center" width="10%">
                </td>
            <td width="10%">
                </td>
        </tr>
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
