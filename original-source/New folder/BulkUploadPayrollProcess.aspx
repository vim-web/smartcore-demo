<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BulkUploadPayrollProcess.aspx.vb" Inherits="SMARTCORE_UI.BulkUploadPayrollProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h4> Bulk Upload Payroll </h4>
    <table>
        <tr>
           <%-- <td>

            </td>--%>
              <td>
                   <asp:FileUpload ID="FleBank" runat="server" CssClass="ui-button-text-icon-primary" Width="227px" />

                   <asp:ImageButton ID="CmdUpload" runat="server" height="25px" ImageUrl="~/Images/Uploadbtn.png" Width="90px" />
             
           
            </td>

              </tr>
        <tr>
            <td>
               <asp:ImageButton ID="Btn_Show" runat="server" ImageUrl="~/Images/Show.png" Width="98px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="DgvCsvFiledata" runat="server" AutoGenerateColumns="False" 
                      CssClass="grid-viewNew" Font-Names="Tahoma" Font-Size="Medium" Width="100%">
                      <Columns>
                          <asp:BoundField DataField="Br_Name" HeaderText="Branch" />
                          <asp:BoundField DataField="Acc_Name" HeaderText="Account Name" />
                          <asp:BoundField DataField="Amount" HeaderText="Amount" />
                          <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                      </Columns>
                  </asp:GridView>
                </td>
              </tr>
        <tr>
            <%--<td> </td>--%>
            <td>
                 <asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png"/> 
                 <asp:ImageButton ID="BtnClose" runat="server" ImageUrl="~/Images/Close.png" />
            </td>
            <td>
                <%--<asp:ImageButton ID="BtnClose" runat="server" ImageUrl="~/Images/Close.png" />--%>
            </td>
        </tr>
    </table>
    <div>
         
    </div>
</asp:Content>
