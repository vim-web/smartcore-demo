<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PrnView.aspx.vb" Inherits="SMARTCORE_UI.PrnView" EnableEventValidation="false"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

  

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script src="Scripts/Print.js" type="text/javascript"></script> 
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
 
    <%--    <script type = "text/javascript">

        $(document).ready(function() {
            $("#<%=GvRpt.ClientID%> tr").hover(function() {
                $(this).css("background-color", "LightBlue");
            }, function() {
                $(this).css("background-color", "#ffffff");
            });
        });
    
//    var GridId = "<%=GvRpt.ClientID %>";
//    var ScrollHeight = 400;
//    window.onload = function () {
//        var grid = document.getElementById(GridId);
//        var gridWidth = grid.offsetWidth;
//        var gridHeight = grid.offsetHeight;
//        var headerCellWidths = new Array();
//        for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
//            headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
//        }
//        grid.parentNode.appendChild(document.createElement("div"));
//        var parentDiv = grid.parentNode;
// 
//        var table = document.createElement("table");
//        for (i = 0; i < grid.attributes.length; i++) {
//            if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
//                table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
//            }
//        }
//        table.style.cssText = grid.style.cssText;
//        table.style.width = gridWidth + "px";
//        table.appendChild(document.createElement("tbody"));
//        table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
//        var cells = table.getElementsByTagName("TH");
// 
//        var gridRow = grid.getElementsByTagName("TR")[0];
//        for (var i = 0; i < cells.length; i++) {
//            var width;
//            if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
//                width = headerCellWidths[i];
//            }
//            else {
//                width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
//            }
//            cells[i].style.width = parseInt(width - 3) + "px";
//            gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
//        }
//        parentDiv.removeChild(grid);
// 
//        var dummyHeader = document.createElement("div");
//        dummyHeader.appendChild(table);
//        parentDiv.appendChild(dummyHeader);
//        var scrollableDiv = document.createElement("div");
//        if(parseInt(gridHeight) > ScrollHeight){
//             gridWidth = parseInt(gridWidth) + 17;
//        }
//        scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
//        scrollableDiv.appendChild(grid);
//        parentDiv.appendChild(scrollableDiv);
//    }
</script>--%>
    <script language="javascript" type="text/javascript">
// <!CDATA[
        //ANANTHAN UPD
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=divPrint.ClientID%>');

    //window.print();
    prtGrid.border = 0;
    //GridView1.Attributes(style) = "page-break-after:always"         
    var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
    prtwin.document.write(prtGrid.outerHTML);
    prtwin.document.close();
    prtwin.focus();
    prtwin.print();
    prtwin.close();
}

function Button1_onclick() {
     
    var DrpPrn = document.getElementById('<%=DrpPrinters.ClientID%>');
    var Prn = document.getElementById('<%=HdnText.ClientID%>');
    var ClPrinter1 = DrpPrn.options[DrpPrn.selectedIndex].text;
    printNew(Prn.value, ClPrinter1);   
}
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
var GridId = "<%=GvRpt.ClientID%>";
var ScrollHeight = 600;
window.onload = function () {
    var grid = document.getElementById(GridId);
    var gridWidth = grid.offsetWidth;
    var gridHeight = grid.offsetHeight;
    var headerCellWidths = new Array();
    for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
        headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
    }
    grid.parentNode.appendChild(document.createElement("div"));
    var parentDiv = grid.parentNode;

    var table = document.createElement("table");
    for (i = 0; i < grid.attributes.length; i++) {
        if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
            table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
        }
    }
    table.style.cssText = grid.style.cssText;
    table.style.width = gridWidth + "px";
    table.appendChild(document.createElement("tbody"));
    table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
    var cells = table.getElementsByTagName("TH");
    var multiheader = document.getElementById('HidMultiHeader').value;
    if (parseInt(multiheader) == 1) {
        var gridRow = grid.getElementsByTagName("TR")[1];
    }
    else {
        var gridRow = grid.getElementsByTagName("TR")[0];
    }
    for (var i = 0; i < cells.length; i++) {
        var width;
        if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
            width = headerCellWidths[i];
        }
        else {
            width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
        }
        cells[i].style.width = parseInt(width - 3) + "px";
        if (parseInt(multiheader) == 0) {
            gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
        }
       
    }
    parentDiv.removeChild(grid);
    
    var dummyHeader = document.createElement("div");
    dummyHeader.appendChild(table);
    parentDiv.appendChild(dummyHeader);
    var scrollableDiv = document.createElement("div");
    if (parseInt(gridHeight) > ScrollHeight) {
        gridWidth = parseInt(gridWidth) + 17;
    }
   // if (parseInt(multiheader) == 0) {
        scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
        scrollableDiv.appendChild(grid);
        parentDiv.appendChild(scrollableDiv);
    //}
    if (parseInt(multiheader) == 1) {
        MultiHeader();
    }
   
        }

function MultiHeader() {
    var grid = document.getElementById(GridId);
    var gridWidth = grid.offsetWidth;
    var gridHeight = grid.offsetHeight;
    var headerCellWidths = new Array();
    for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
        headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
    }
    grid.parentNode.appendChild(document.createElement("div"));
    var parentDiv = grid.parentNode;

    var table = document.createElement("table");
    for (i = 0; i < grid.attributes.length; i++) {
        if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
            table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
        }
    }
    table.style.cssText = grid.style.cssText;
    table.style.width = gridWidth + "px";
    table.appendChild(document.createElement("tbody"));
    table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
    var cells = table.getElementsByTagName("TH");
    var multiheader = document.getElementById('HidMultiHeader').value;
    if (parseInt(multiheader) == 1) {
        var gridRow = grid.getElementsByTagName("TR")[1];
    }
    else {
        var gridRow = grid.getElementsByTagName("TR")[0];
    }
    for (var i = 0; i < cells.length; i++) {
        var width;
        if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
            width = headerCellWidths[i];
        }
        else {
            width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
        }
        cells[i].style.width = parseInt(width - 3) + "px";
        if (parseInt(multiheader) == 1) {
            gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
        }

    }
    parentDiv.removeChild(grid);

    var dummyHeader = document.createElement("div");
    dummyHeader.appendChild(table);
    parentDiv.appendChild(dummyHeader);
    var scrollableDiv = document.createElement("div");
    if (parseInt(gridHeight) > ScrollHeight) {
        gridWidth = parseInt(gridWidth) + 17;
    }

    scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
    scrollableDiv.appendChild(grid);
    parentDiv.appendChild(scrollableDiv);
}
    </script>    

    <style type="text/css">
        .left_align
        {text-align:left}
        .auto-style1
        {
            width: 145px;
        }
        .auto-style2
        {
            width: 134px;
        }
    </style>
    
</head>
<body>
 <div id="divPrint"  runat="server">
    

    <form id="form1" runat="server">
       
        <div id ="DivHeader1" runat = "server" visible="false" style="text-align:center">            
            <asp:Label ID="lblHeading1" runat="server" ForeColor="#000099" Font-Bold="True" 
                Font-Size="Large"></asp:Label> 
        </div>
        <div id ="DivHeader2" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading2" runat="server"  ForeColor="#000099"></asp:Label> 
        </div>
        <div id ="DivHeader3" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading3" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivHeader4" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading4" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivHeader5" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading5" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivHeader6" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading6" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivHeader7" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading7" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivHeader8" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading8" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivHeader9" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading9" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivHeader10" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading10" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>

        <div id ="DivHeader11" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading11" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        
        <div id ="DivHeader12" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading12" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        
        <div id ="DivHeader13" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading13" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        
        <div id ="DivHeader14" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading14" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        
        <div id ="DivHeader15" runat = "server" visible="false" style="text-align:center">
            <asp:Label ID="lblHeading15" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        
        <div id ="DivGrid" runat = "server"  style="text-align:center">
            <br />
            <asp:GridView ID="GvRpt" runat="server" Width = "100%" CssClass = "grid-view">
                <AlternatingRowStyle BackColor="#FBFBFB" />

               
            </asp:GridView>
        </div>

        <div id ="DivFooter1" runat = "server" visible="false" style="text-align:left">            
            <asp:Label ID="lblFooter1" runat="server"  ForeColor="#000099"></asp:Label> 
        </div>
        <div id ="DivFooter2" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter2" runat="server"  ForeColor="#000099"></asp:Label> 
        </div>
        <div id ="DivFooter3" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter3" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter4" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter4" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter5" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter5" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter6" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter6" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter7" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter7" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter8" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter8" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter9" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter9" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter10" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter10" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter11" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter11" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter12" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter12" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter13" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter13" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter14" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter14" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
        <div id ="DivFooter15" runat = "server" visible="false" style="text-align:left">
            <asp:Label ID="lblFooter15" runat="server"  ForeColor="#000099"></asp:Label>  
        </div>
   
        <div id="ExportDiv" runat ="server">
            <div id ="DivBtns" runat = "server" visible="True" style="text-align:center">
            <asp:HiddenField ID="HdnText" runat="server" />
&nbsp;
    <applet archive="dist/jzebra.jar" code="jzebra.PrintApplet.class" height="25" 
           name="jZebra" width="25">
           <param name="printer" value="zebra" />
<!-- <param name="sleep" value="200"> -->
       </applet></div>       
            <table width = "100%">
        <tr>
        
        <td width ="20%" align="right">
                    
                    <asp:Label ID="LblRelation1" runat="server" Text="Start Page No"  Width="43%" 
                        Font-Size="Small" ForeColor="#6666FF" style="height: 15px"></asp:Label>
                </td>
        <td class="auto-style2">

                    <asp:TextBox ID="TxtPageNo" runat="server"   Width="54%" BorderStyle="Solid" 
                        TabIndex="7" AutoPostBack="True" Font-Size="X-Small" Height="16px"></asp:TextBox>

            </td>
        <td class="auto-style1">

                    <asp:Label ID="LblRelation3" runat="server" Text="End Page No"  Width="123%" 
                        Font-Size="Small" ForeColor="#6666FF"></asp:Label>

            </td>
        <td width ="20%" style="width: 10%">

                    <asp:TextBox ID="TxtPageNoTo" runat="server"   Width="26%" BorderStyle="Solid" 
                        TabIndex="7" AutoPostBack="True" Font-Size="Small"></asp:TextBox>

            </td>
        <td width ="20%" align="center">&nbsp;</td>
        <td width ="20%" align=right>
                    &nbsp;</td>
        <td width ="20%">
                    &nbsp;</td>
        </tr>      
        <tr>
        
        <td width ="20%" align="right">
                    <asp:Label ID="LblRelation2" runat="server" Text="Select Printer"  Width="50%" 
                        Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
        <td width ="20%" colspan="3">

        <asp:DropDownList ID="DrpPrinters" runat="server"  Width ="100%">
                    </asp:DropDownList>

            </td>
        <td width ="20%" align="center"><input id="Button1" type="button" value="Print" 
                onclick="return Button1_onclick()"                 
                style="font-family: 'Courier New', Courier, monospace; font-size: medium;background-color:#0066FF;color:White; height: 26px; font-weight: bold;"   
                onclick="return Button1_onclick()" />
                <asp:Button ID="CmdClose" 
                runat="server" BackColor="#0066FF" Font-Bold="True" 
                ForeColor="White" Height="26px" Text="Close" Width="74px" 
                Font-Names="Courier New" />
            <asp:Button ID="BtnWord" runat="server" Text="WORD" BackColor="#0066FF" 
                Font-Bold="True" ForeColor="White" />
                    &nbsp;</td>
        <td width ="20%" align=right>
                    <asp:Button ID="Button2" runat="server" Text="EXCEL" BackColor="#0066FF" 
                Font-Bold="True" ForeColor="White" />
                    
        </td>
        <td width ="20%">
                &nbsp;<asp:Button ID="BtnPDF" runat="server" Text="PDF" BackColor="#0066FF" 
                Font-Bold="True" ForeColor="White"  />
                </td>
        </tr>      
        <tr>
        
        <td width ="20%">
                    <asp:Label ID="LblPageno1" runat="server" Text="lable"  Width="50%" 
                        Font-Size="Medium" ForeColor="#FF3300"></asp:Label>
                      
            </td>
        <td width ="20%" colspan="3">

            &nbsp;</td>
        <td width ="20%" align="center">&nbsp;</td>
        <td width ="20%" align=right>
                    <a href="Print000.vb">Print000.vb</a></td>
        <td width ="20%">
                    &nbsp;</td>
        </tr>      
    </table>
        </div>
        <asp:HiddenField ID="HidMultiHeader" runat="server" Value="0" />
    </form>
     </div>   
  

<p>
    
    </p>
</body>
</html>
