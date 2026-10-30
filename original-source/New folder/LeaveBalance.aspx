<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LeaveBalance.aspx.vb" Inherits="SMARTCORE_UI.LeaveBalance" 
    title="Untitled Page" %>
        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script runat="server">

   
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width:60%; margin-left:20%; margin-right:20%;" id=">">                
    <legend>LeaveBalance</legend> 
    <table width="100%">
        <tr>
            <td width="50%" ><label>EmpId</label></td>
        
            <td width="50%" >
                <asp:TextBox ID="Txt_EmpId" runat="server" Width="80%" AutoPostBack="True"></asp:TextBox>
                <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                    ImageUrl="~/images/Search.png" Width="23px" />
            </td>
                

        </tr>
        <tr>
            <td width="50%">
                <label>
                EmpName</label></td>
            <td  width="50%">
                <asp:TextBox ID="Txt_EmpName" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr> 
            <td width="50%">
            <label>OpeningDate</label></td>

            <td width="50%">
                <asp:TextBox ID="Txt_Date" runat="server" Width="80%"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td width="50%" align="center" colspan="2" style="width: 100%">&nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="width: 100%" width="50%">
                <asp:GridView ID="DgvLeavebalance" runat="server" AutoGenerateColumns="False" 
                   TabIndex="32" Width="100%" onrowdatabound="DgvLeavebalance_RowDataBound">
                    <Columns>                       
                      
                        <asp:BoundField DataField="pkc_code" HeaderText="LeaveId" >
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" />
                </asp:BoundField>   
                       <asp:BoundField DataField="Pkc_Description" HeaderText="LeaveName" >
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" />
                </asp:BoundField>   
                      
                        <asp:TemplateField runat="server" HeaderText="LeaveBalance" 
                            SortExpression="Leave Balance" >
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" />
                            <ItemTemplate>
                                <asp:TextBox ID="TxtBalance" runat="server" AutoPostBack="true" 
                                    BorderStyle="none" ForeColor="Blue" style="text-align:right" 
                                    Text='<%# Bind("Balance") %>' Width="50%">
                        </asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="width: 100%" width="50%">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="56" />
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="57" />
            </td>
        </tr>
        <tr>
            <td width="50%" align="right">
                &nbsp;</td>
            <td width="50%">
                &nbsp;</td>
        </tr>
    </table>
    
    
    
    
                  
    </ContentTemplate>
    </asp:UpdatePanel>

    

</asp:Content>
