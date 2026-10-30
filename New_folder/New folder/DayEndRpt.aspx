<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DayEndRpt.aspx.vb" Inherits="SMARTCORE_UI.RptPreview" 
    title="Report" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
            var gAutoPrint = true; // Flag for whether or not to automatically call the print function 
            function printSpecial() {
                window.print();
            }

        </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
      <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <input ID="Btn_Print" type="button" value="Print" 
        onclick = "printSpecial()" onclick="return Btn_Print_onclick()" onclick="return Btn_Print_onclick()" /><asp:DropDownList 
        ID="Ddl_Export" runat="server" Height="22px" Width="188px">
    </asp:DropDownList>
    <asp:Button ID="Btn_Export" runat="server" Text="Export" />
    <asp:ImageButton ID="Img_Redirect" runat="server" Width="22px" 
    ImageUrl="~/Images/Home.png" />
    <br />

    <CR:CrystalReportViewer ID="CRV" runat="server"
            Width="100%" Height="100%"  DisplayGroupTree="False"
            HasCrystalLogo="False" 
        HasExportButton="False" BestFitPage="False" PrintMode="ActiveX" />
            
            <asp:HiddenField ID="hdnOutput" runat="server" />
            
        <br />
            
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
            
             <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            </asp:Content>

