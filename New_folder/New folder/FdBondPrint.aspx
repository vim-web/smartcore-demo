<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FdBondPrint.aspx.vb" Inherits="SMARTCORE_UI.FdBondPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
<%--<script language='VBScript'>
Sub Print()
       OLECMDID_PRINT = 6
       OLECMDEXECOPT_DONTPROMPTUSER = 2
       OLECMDEXECOPT_PROMPTUSER = 1
       call WB.ExecWB(OLECMDID_PRINT, OLECMDEXECOPT_DONTPROMPTUSER,1)
End Sub
document.write "<object ID='WB' WIDTH=0 HEIGHT=0 CLASSID='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'></object>"
</script>
--%>
    
        <script type = "text/javascript">
             function CallPrint(strid)
      {
          var prtContent = document.getElementById(strid);
          var WinPrint = window.open('','','letf=0,top=0,width=400,height=400,toolbar=0,scrollbars=0,status=0');
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






       
       function exportToExcel()
        {
            var oExcel = new ActiveXObject("Excel.Application");
            var oBook = oExcel.Workbooks.Add;
            var oSheet = oBook.Worksheets(1);
            for (var y=0;y<GridView1.rows.length;y++)
            // GridView3 is the table where the content to be exported is
            {
            for (var x=0;x<GridView1.rows(y).cells.length;x++)
            {
            oSheet.Cells(y+1,x+1) =
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
    <div id="divPrint"  runat="server"  style=" height:1344px; width:960px;" >
    <table width="100%" align="center">
    <tr>
    <td width="2%" class="style1"></td>
    <td width="23%" height="50%">
        <asp:Label ID="LblBranch" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" class="style1"></td>
    <td width="2%" class="style1"></td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td class="style2" align="center" colspan="2">
        <asp:Label ID="LblDuedate" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;&nbsp;
        <asp:Label ID="LblDepNo" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td class="style3" align="center" colspan="2">
        <asp:Label ID="LblDmDate" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td width="23%" align="right">
        &nbsp;</td>
    <td class="style3" align="center" colspan="2">
        <asp:Label ID="LblEffDate" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblName" runat="server" Text="Label"></asp:Label>
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
    <td width="23%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblAddress" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblPostofc" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%" colspan="2" style="width: 25%">
        <asp:Label ID="LblNoToWords" runat="server" Text="Label" Font-Italic="True"></asp:Label>
                    </td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
    <table width="100%">
    <tr>
    <td width="50%" align="right" style="width: 100%">&nbsp;</td>
    </tr>
    </table>
        </td>
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
        <asp:Label ID="LblNoOfmonth" runat="server" Text="Label"></asp:Label>
                    </td>
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
    <td width="23%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblIntrRate" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblDepAmt" runat="server" Text="Label" Font-Bold="True" 
            Font-Size="Large" Font-Italic="True"></asp:Label>
        </td>
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
    <td width="23%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblBranch2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td width="2%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LblDepAmt2" runat="server" Text="Label"></asp:Label>
                    </td>
    <td width="23%">&nbsp;</td>
    <td class="style3" align="center">
        <asp:Label ID="LblAccNo" runat="server" Text="Label"></asp:Label>
                    </td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblNoOfmonth2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%">
        &nbsp;</td>
    <td width="2%">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LblIntRate2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td width="23%" align="center">
        <asp:Label ID="LblDepDate" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td width="2%" align="left">
        &nbsp;&nbsp;&nbsp;&nbsp; </td>
    <td width="23%">&nbsp;</td>
    <td width="2%" align="center">
        &nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
    <table width="100%">
    <tr>
    </tr>
    </table>
        </td>
    <td width="23%" colspan="2" style="width: 25%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3" align="center">
        <asp:Label ID="LblDueDate2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%" colspan="2" style="width: 25%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3" align="center">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%" colspan="2" style="width: 25%">
        <asp:Label ID="LblNoToWords2" runat="server" Text="Label" Font-Italic="True"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td class="style3" align="center">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblCustName2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%" align="right">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%">
        &nbsp;</td>
    <td width="23%">
        <asp:Label ID="LblAddress2" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="right">
        <asp:Label ID="LblNomName" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    <td width="23%">&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td width="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="23%" align="right">
        <asp:Label ID="LblPrevilage" runat="server" Text="Label"></asp:Label>
        </td>
    <td width="23%" align="center">
        <asp:Label ID="LblOpMode" runat="server" Text="Label"></asp:Label>
        </td>
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
    </table>
    </div>
                &nbsp;</form>
    <p>
                <input type="button" value="PRINT" id="btnPrint"  Class ="ButtonStyle"  runat="Server" onclick="javascript:CallPrint('divPrint')" /></p>
</body>
</html>
