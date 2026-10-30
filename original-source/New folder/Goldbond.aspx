<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Goldbond.aspx.vb" Inherits="SMARTCORE_UI.Goldbond" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type = "text/javascript">
         function CallPrint(strid) {
             var prtContent = document.getElementById(strid);
             var WinPrint = window.open('', '', 'letf=0,top=0,width=400,height=400,toolbar=0,scrollbars=0,status=0');
             WinPrint.document.write(prtContent.innerHTML);
             WinPrint.document.close();
             WinPrint.focus();
             WinPrint.print();
             WinPrint.close();
         }


         //06022013
         //            function CallPrint(strid) {

         //                var sOption = "toolbar=no,location=no,directories=no,menubar=no,scrollbars=0,width=120,height=120";
         //                // Get the HTML content of the div
         //                var sDivText = window.document.getElementById(strid).innerHTML;
         //                // Print the window            
         //                var objWindow = window.open("", "", sOption);
         //                // Write the div element to the window objWindow.document.write(sDivText); objWindow.document.close(); // Print the window                  
         //                print();
         //                // Close the window
         //                objWindow.close();
         //            }
         //            function print() {
         //                if (document.all) {
         //                    WebBrowser1.ExecWB(6, 1) //use 6, 1 to prompt the print dialog or 6, 6 to omit it; 
         //                    WebBrowser1.outerHTML = "";
         //                }
         //                else {
         //                    window.print();
         //                }
         //            }







         function exportToExcel() {
             var oExcel = new ActiveXObject("Excel.Application");
             var oBook = oExcel.Workbooks.Add;
             var oSheet = oBook.Worksheets(1);
             for (var y = 0; y < GridView1.rows.length; y++)
             // GridView3 is the table where the content to be exported is
             {
                 for (var x = 0; x < GridView1.rows(y).cells.length; x++) {
                     oSheet.Cells(y + 1, x + 1) =
            GridView1.rows(y).cells(x).innerText;
                 }
             }
             oExcel.Visible = true;
             oExcel.UserControl = true;
         }

            
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div id="divPrint">
    <table width="100%" align="center">
    <tr>
    <td width="2%" class="style1"></td>
    <td width="23%" height="50%" align="center">
        &nbsp;</td>
    <td width="23%" class="style1" align="center">
        &nbsp;</td>
    <td width="2%" class="style1" align="center">
        &nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td class="style2" align="center" colspan="2">
        &nbsp;</td>
    </tr>
    <tr>
    <td width="2%" class="style1">&nbsp;</td>
    <td width="23%" height="50%" align="left">
        &nbsp;<asp:Label ID="LblDuedate" runat="server" Text="Label"></asp:Label></td>
    <td width="23%" class="style1" align="center">
        <asp:Label ID="LblAmount" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%" class="style1" align="center">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
    <td width="23%" align="left">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LblGlNo" runat="server" Text="Label"></asp:Label>
        </td>
    <td class="style2" align="center" colspan="2">
        &nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;&nbsp;
        </td>
    <td width="23%" align="left">
        <asp:Label ID="LblMemberNo" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td class="style3" align="center" colspan="2">
        &nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%" align="left">
        <asp:Label ID="LblName" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td class="style3" align="center" colspan="2">
        &nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
    <td width="23%" align="left">
        <asp:Label ID="LblAddress" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="right">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LblDebtAmt" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="left">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%" colspan="2" style="width: 25%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%" colspan="2" style="width: 25%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%" colspan="2" style="width: 25%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%" class="style1"></td>
    <td width="23%" class="style1">
    <table width="100%">
    <tr>
    <td width="50%" align="left" style="width: 100%">
        <asp:Label ID="LblDescription" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
    </table>
        </td>
    <td width="23%" class="style1">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LblCount" runat="server" Text="Label"></asp:Label>
                    </td>
    <td width="2%" class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LblTotWeigt" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LblGoldWeight" runat="server" Text="Label"></asp:Label>
                    </td>
    <td width="23%" align="center" class="style1">
        <asp:Label ID="LblCost" runat="server" Text="Label"></asp:Label>
                    </td>
    <td class="style1"></td>
    <td width="2%" class="style1"></td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="2%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    <td width="23%">
        &nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="right">&nbsp;</td>
    <td width="23%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
    <td width="2%">
        <asp:Label ID="Lblweight" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;<asp:Label ID="LbllCost" runat="server" Text="Label"></asp:Label></td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Lblamt2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td width="2%">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    <td width="23%">
        &nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblAmt4" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;</td>
    <td width="2%" align="center">
        <asp:Label ID="LblAmt3" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblTotAmt" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="left">
        &nbsp;</td>
    <td width="2%" align="center">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="2%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
    <td width="23%">&nbsp;</td>
    <td class="style3" align="center">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    <td width="23%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="2%" align="left">
        &nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="LblDate" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="right">
        <asp:Label ID="LblVoucherNo" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%" align="center">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
    <table width="100%">
    </table>
        </td>
    <td width="23%" style="width: 25%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td class="style3" align="right">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%" style="width: 25%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3" align="center">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%" align="left">
        <asp:Label ID="LblGLNo2" runat="server" Text="Label"></asp:Label>
        </td>
    <td class="style3" align="center">&nbsp;</td>
    <td width="2%">
        <asp:Label ID="LblGlAmt" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%" align="left">
        <asp:Label ID="LblAmtinWords" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="left">
        <asp:Label ID="LblMno" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td width="2%">
        <asp:Label ID="LblName2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="right">
        <asp:Label ID="LblWeight2" runat="server" Text="Label"></asp:Label>
        </td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="left">
        <asp:Label ID="LblGlNo4" runat="server" Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    <td width="23%">
        &nbsp;
        <asp:Label ID="lbl_weight" runat="server" Text="Weight:"></asp:Label>
        <asp:Label ID="LblWeight3" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%" align="center">
        <asp:Label ID="LblGlNo3" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="left">
        <asp:Label ID="LblName4" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%" align="center">
        <asp:Label ID="LblName3" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="left">
        <asp:Label ID="LblRs" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="left">
        <asp:Label ID="LblOrnament" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="left">
        <asp:Label ID="LblDate2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%" align="center">
        <asp:Label ID="LblRs2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="center">
        <asp:Label ID="LblDate4" runat="server" Text="Label"></asp:Label>
        </td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%" align="center">
        <asp:Label ID="LblOrnament2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="center">
        &nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    </table>
    </div>
     <input type="button" value="PRINT" id="btnPrint"  Class ="ButtonStyle"  runat="Server" onclick="javascript:CallPrint('divPrint')" />
    </form>
</body>
</html>
