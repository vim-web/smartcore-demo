<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RptView.aspx.vb" Inherits="SMARTCORE_UI.RptView"  MasterPageFile="~/SMARTCORE.Master" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



       
<script language="javascript" type="text/javascript">
            var gAutoPrint = true; // Flag for whether or not to automatically call the print function 
            function printSpecial() { 
            if (document.getElementById != null) { 
            var html = '<HTML>\n<HEAD>\n'; 
            if (document.getElementsByTagName != null) { 
            var headTags = document.getElementsByTagName("head"); 
            if (headTags.length > 0) html += headTags[0].innerHTML; 
            } 
            if (gAutoPrint) { 
            if (navigator.appName == "Microsoft Internet Explorer") { 
            html += '\n</HEAD>\n<'
            html += 'BODY onLoad="PrintCommandObject.ExecWB(6, -1);">\n'; 
            } 
            else { 
            html += '\n</HEAD>\n<BODY >\n'; 
            } } 
            else { 
            html += '\n</HEAD>\n<BODY >\n'; 
            } 

            var printReadyElem = document.getElementById("printReady"); 
            if (printReadyElem != null) { 
            html += printReadyElem.innerHTML; 
            } 
            else { 
            alert("Could not find the printReady section in the HTML"); 
            return; 
            }
            if (gAutoPrint) { 
            if (navigator.appName == "Microsoft Internet Explorer") { 
            html += '< O B J E C T ID="PrintCommandObject" WIDTH=0 HEIGHT=0 '
            html += 'CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></ O B J E C T >\n</ B O D Y >\n</ H T M L >'; } 
            else { 
            html += '\n</BODY>\n</HTML>'; 
            } } 
            else { 
            html += '\n</BODY>\n</HTML>'; 
            } 
            var printWin = window.open("","printSpecial"); 
            printWin.document.open(); 
            printWin.document.write(html); 
            printWin.document.close();
            if (gAutoPrint) { 
            if (navigator.appName != "Microsoft Internet Explorer") { 
            printWin.print(); 
            }}} 
            else { 
            alert("Sorry, the print ready feature is only available in modern browsers.");
            }} 
            

        </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <br />
      <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <input ID="Btn_Print" type="button" value="Print" 
        onclick = "printSpecial()" /><asp:DropDownList 
        ID="Ddl_Export" runat="server" Height="22px" Width="188px">
    </asp:DropDownList>
    <asp:Button ID="Btn_Export" runat="server" Text="Export" 
    CausesValidation="False" />
    
     <asp:DropDownList ID="Ddl_Printer" runat="server" Height="16px" Width="234px">
    </asp:DropDownList>
    
    <br />

 <div id ="printReady" > 
    <CR:CrystalReportViewer ID="CRV" runat="server"  AutoDataBind="false"
            Width="100%" Height="100%" ReportSourceID="CrystalReportSource1"  DisplayGroupTree="False"
            HasCrystalLogo="False" DisplayToolbar="False" 
        HasExportButton="False" PrintMode="ActiveX" />
            
            <asp:HiddenField ID="hdnOutput" runat="server" />
            
        <br />
            
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
            
             <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

</div>
            
            </asp:Content>


