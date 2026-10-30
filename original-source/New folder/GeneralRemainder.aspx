<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GeneralRemainder.aspx.vb" Inherits="SMARTCORE_UI.GeneralRemainder" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  

<script type='text/javascript'>

    function ValidateSave() {
        var Ctl = document.getElementById('<%=TxtRemPostedBy.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Posyted By');
            Ctl.focus();
            return false;
        }




        var Ctl = document.getElementById('<%=TxtRemDate.ClientID%>');
        if (Ctl.value == "") {
            alert('Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtRemainder.ClientID%>');
        if (Ctl.value == "") {
            alert('Remainder Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        return true;
    }

</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;">
     <legend>Scroll Message</legend><br />
    <table width="100%">
<tr>
<td width="2%" colspan="3" style="width: 50%"></td>
<td width="2%"> </td>
</tr>
        <tr>
            <td width="2%">
            </td>
            <td colspan="2" style="width: 96%" width="48%">
                <asp:GridView ID="DgvRemainder" runat="server" Width="100%" AllowPaging="True" 
                    AutoGenerateColumns="False" DataKeyNames="Reminder_Id" PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="Reminder_Id" HeaderText="Remainder_ID" 
                            Visible="False" >
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Small" ForeColor="White"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="RegisterDate" DataFormatString="{0:dd/MM/yyyy}"  
                            HeaderText="Remainder Date" >
                        <HeaderStyle BackColor="#00246F" Font-Size="Small" ForeColor="White" 
                            Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AssignedBy" HeaderText="User" >
                        <HeaderStyle BackColor="#00246F" Font-Size="Small" ForeColor="White" 
                            Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Remainder_Content" HeaderText="Message">
                        <HeaderStyle BackColor="#00246F" Font-Size="Small" ForeColor="White" 
                            Width="67%" />
                        </asp:BoundField>
                        <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                            ShowDeleteButton="True" >
                        <HeaderStyle Width="3%" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </td>
            <td width="2%">
            </td>
        </tr>
<tr>
<td> </td>
<td> </td>
<td width="48%">
     </td>
<td> </td>
</tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="48%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                Message Date</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtRemDate" runat="server" Width="100%"></asp:TextBox>
                <asp:CalendarExtender ID="TxtRemDate_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="TxtRemDate">
                </asp:CalendarExtender>
            </td>
            <td>
            </td>
        </tr>
<tr>
<td> </td>
<td><label>Message Posted By</label></td>
<td>
    <asp:TextBox ID="TxtRemPostedBy" Width="100%" runat="server"></asp:TextBox>
    </td>
<td> </td>
</tr>
<tr >
<td height="60px"> </td>
<td><label>Message</label></td>
<td>
    <asp:TextBox ID="TxtRemainder" Width="100%" Height="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
    </td>
<td> </td>
</tr>
<tr>
<td> </td>
<td align="center" colspan="2">
     </td>
<td> </td>
</tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center" colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                 </td>
            <td align="center" colspan="2">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();" />
                 <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                 <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
            </td>
            <td>
                 </td>
        </tr>
</table>
    
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
    