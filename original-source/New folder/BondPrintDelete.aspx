<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BondPrintDelete.aspx.vb" Inherits="SMARTCORE_UI.BondPrintDelete" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 17%;
        }
        .twitterStyleTextbox {
            margin-left: 12px;
        }
        .auto-style2 {
            width: 11%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                <table width="75%">
                    <tr>
                        <td class="auto-style1">
                             <label>
                            Transaction Date</label></td>
                       <td width="15%">
                           <asp:TextBox ID="TxtTranDt"  placeholder="DD/MM/YYYY"  runat="server" CssClass="twitterStyleTextbox" 
                               meta:resourcekey="TxtTranDtResource1" TabIndex="1" Width="84%"></asp:TextBox>
                           <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="TxtTranDt">
                    </asp:CalendarExtender>
                       </td>               
                        
                    <%--<tr>--%>
                                <td class="auto-style2">
                                    <label>
                                    Print Type</label></td>                               
                                <td width = "35%">
                                    <asp:DropDownList ID="CmbPrnType" runat="server" Width="87%" AutoPostBack="True">
                                        <asp:ListItem Text="Gold Bond KFS" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Gold Bond Tamil" Value="2"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            <%--</tr>--%>
                    <td width="10%">
                            <asp:Button ID="BtnRefresh" runat="server" Height="27px" Text="Refresh" 
                                Width="84%" />                         
                            </td>
  <td width="15%">
                             <asp:Button ID="Close" runat="server" Height="27px" Text="Close" Width="78%" />                         
                            </td>
                    </tr> 
                </table>
                    </div>
                <div></div>
                <div id="NonTranDtls" width: 100%>
        
             <asp:GridView ID="Dgv" runat="server" AllowPaging="True" 
                 AutoGenerateColumns="False" DataKeyNames="TranNo,Tr_date" TabIndex="8" Width="100%" 
                 PageSize="5">
                 <RowStyle Height="20px" />
                 <Columns>
                     <asp:CommandField SelectText="Delete" ShowSelectButton="True">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" Width="5%" />
                     </asp:CommandField>                   
                     <asp:BoundField DataField="Br_Name" HeaderText="Branch">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" ForeColor="White" Width="20%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="AccNo" HeaderText="Account No.">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" ForeColor="White" Width="30%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                          <asp:BoundField DataField="AccId" HeaderText="AccId" Visible="False" />
                     <asp:BoundField DataField="Tr_Date" HeaderText="Transaction Date">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" ForeColor="White" HorizontalAlign="Left" Width="10%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="TranNo" HeaderText="Tran_No" Visible="False" />
                     <asp:BoundField DataField="PrintTime" HeaderText="PrintTime">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                             Width="10%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Left" />
                     </asp:BoundField>
  <asp:BoundField DataField="BondType" HeaderText="BondType">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                             Width="10%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Left" />
                     </asp:BoundField>
    <asp:BoundField DataField="UserId" HeaderText="UserId">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                             Width="10%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Left" />
                     </asp:BoundField>
                 </Columns>
                 <AlternatingRowStyle BackColor="#F5F4FF" />
             </asp:GridView>
        
         </div>
               <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">

</asp:ToolkitScriptManager>
              </div>  
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
