<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BatchTran.aspx.vb" Inherits="SMARTCORE_UI.BatchTran" title="BATCH TRANSACTION" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>       
    <script type="text/javascript">
        function imgBatchDtlsDr_onclick() {
            $('.gradientBoxesWithOuterShadows').append('<div id="resultdiv" title="Batch Details" visible="false" >');
            objDDl = document.getElementById('<%=CmbBatchDr.ClientID%>');
            var selval = objDDl.options[objDDl.selectedIndex].value;
            $("#resultdiv").load("BatchDtls.aspx?BatchNo=" + selval, {}, function(data) {
                $("#resultdiv").show();
            });

            $("#resultdiv").dialog({
                autoOpen: false,
                height: 600,
                width: 900,
                //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                close: function(event, ui) { $(this).dialog("destroy"); },
                modal: true
            });
            $('#resultdiv').dialog('open');

        }

        function imgBatchDtlsCr_onclick() {
            $('.gradientBoxesWithOuterShadows').append('<div id="resultdiv" title="Batch Details" visible="false" >');
            objDDl = document.getElementById('<%=CmbBatchCr.ClientID%>');
            var selval = objDDl.options[objDDl.selectedIndex].value;
            $("#resultdiv").load("BatchDtls.aspx?BatchNo=" + selval, {}, function(data) {
                $("#resultdiv").show();
            });

            $("#resultdiv").dialog({
                autoOpen: false,
                height: 600,
                width: 900,
                //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                close: function(event, ui) { $(this).dialog("destroy"); },
                modal: true
            });
            $('#resultdiv').dialog('open');

        }   
        // Select/Deselect checkboxes based on header checkbox
//        function SelectheaderCheckboxes(headerchk) {
//            debugger
//            var gvcheck = document.getElementById('DgvDebit');
//            var i;
//            //Condition to check header checkbox selected or not if that is true checked all checkboxes
//            if (headerchk.checked) {
//                for (i = 0; i < gvcheck.rows.length; i++) {
//                    var inputs = gvcheck.rows[i].getElementsByTagName('input');
//                    inputs[0].checked = true;
//                }
//            }
//            //if condition fails uncheck all checkboxes in gridview
//            else {
//                for (i = 0; i < gvcheck.rows.length; i++) {
//                    var inputs = gvcheck.rows[i].getElementsByTagName('input');
//                    inputs[0].checked = false;
//                }
//            }
//        }
//        //function to check header checkbox based on child checkboxes condition
//        function Selectchildcheckboxes(header) {
//            var ck = header;
//            var count = 0;
//            var gvcheck = document.getElementById('DgvDebit');
//            var headerchk = document.getElementById(header);
//            var rowcount = gvcheck.rows.length;
//            //By using this for loop we will count how many checkboxes has checked
//            for (i = 1; i < gvcheck.rows.length; i++) {
//                var inputs = gvcheck.rows[i].getElementsByTagName('input');
//                if (inputs[0].checked) {
//                    count++;
//                }
//            }
//            //Condition to check all the checkboxes selected or not
//            if (count == rowcount - 1) {
//                headerchk.checked = true;
//            }
//            else {
//                headerchk.checked = false;
//            }
        //        }
        function ValidateSave() {
            objDDl = document.getElementById('<%=CmbBatchDr.ClientID%>');
            var selval = objDDl.options[objDDl.selectedIndex].value;
            if (selval == 'Select') {
                alertify.error("Please Select From Batch No");
                return false;
            }

            objDDl = document.getElementById('<%=CmbBatchCr.ClientID%>');
            var selval = objDDl.options[objDDl.selectedIndex].value;
            if (selval == 'Select') {
                alertify.error("Please Select To Batch No");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    
<ContentTemplate>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

<div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:15%; margin-right:15%; text-align:center">                
    <legend>BATCH TRANSACTION</legend>  
    
    <table width="100%">
        <tr>
            <td width="2%"></td>
            <td width="20%"><legend>BATCH FROM</legend></td>
            <td width="20%" align="right"><label>Batch From</label></td>
            <td width="20%">
                <asp:DropDownList ID="CmbBatchDr" runat="server" AutoPostBack="True" 
                    Width="80%">
                </asp:DropDownList>
                <asp:ImageButton ID="ImgBatchDr" runat="server" 
                    ImageAlign="Middle" ImageUrl="~/Images/Node.png" 
                    OnClientClick="return imgBatchDtlsDr_onclick();" />
            </td>
            <td width="20%"></td>
            <td width="16%"></td>
            <td width="2%"></td>
        </tr>
        
        <tr>
            <td></td>
            <td colspan="5">
                <asp:GridView ID="DgvBatchFrom" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid-view" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="DrDtls" HeaderText="Debit Details">
                            <HeaderStyle Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DrAmt" DataFormatString="{0:F2}" 
                            HeaderText="Debit Amount">
                            <HeaderStyle Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CrDtls" HeaderText="Credit Details">
                            <HeaderStyle Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CrAmt" DataFormatString="{0:F2}" 
                            HeaderText="Credit Amount">
                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td colspan="3" align="center"> <asp:Label ID="LblDiffFr" runat="server" 
                    Font-Bold="True" ForeColor="#FF3300"></asp:Label> </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><legend>BATCH TO</legend></td>
            <td width="20%" align="right"><label>Batch To</label></td>
            <td>
                <asp:DropDownList ID="CmbBatchCr" runat="server" AutoPostBack="True" 
                    Width="80%">
                </asp:DropDownList>
                <asp:ImageButton ID="ImgBatchCr" runat="server"
                    ImageAlign="Middle" ImageUrl="~/Images/Node.png" 
                    OnClientClick="return imgBatchDtlsCr_onclick();" />
            </td>
            <td></td>
            <td>
                &nbsp;</td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="5">
                <asp:GridView ID="DgvBatchTo" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid-view" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="DrDtls" HeaderText="Debit Details">
                            <HeaderStyle Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DrAmt" DataFormatString="{0:F2}" 
                            HeaderText="Debit Amount">
                            <HeaderStyle Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CrDtls" HeaderText="Credit Details">
                            <HeaderStyle Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CrAmt" DataFormatString="{0:F2}" 
                            HeaderText="Credit Amount">
                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td colspan="3" align="center">
                <asp:Label ID="LblDiffTo" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><legend>FILTER</legend></td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                &nbsp;</td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="5">
                <table width="100%">
                <tr>
                    <td width="15%"><asp:RadioButton ID="OptBatched" runat="server" Text="Batched" 
                            GroupName="BATCH" AutoPostBack="True" /></td>
                    <td width="20%"><asp:RadioButton ID="OptUnBatched" runat="server" Text="Not Batched" 
                            GroupName="BATCH" Checked="True" AutoPostBack="True"/></td>
                    <td width="15%"><asp:RadioButton ID="OptAll" runat="server" Text="ALL" 
                            GroupName="BATCH" AutoPostBack="True"/></td>
                    <td></td>
                    <td>
                        <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                            meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
                            TabIndex="35" />
                        <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                            meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                        <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                            meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" TabIndex="37" />
                    </td>
                    <td></td>
                </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
                    <td width="15%"> <%--<label>Amount From</label>--%></td>
                    <td width="18%">
                        <asp:TextBox ID="TxtAmtFr" runat="server" AutoPostBack="True" 
                            CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                            style="text-align:right"  visible="false"></asp:TextBox>
                    </td>
                    <td width="5%"><%--<label>To</label>--%></td>
                    <td width="17%">
                        <asp:TextBox ID="TxtAmtTo" runat="server" AutoPostBack="True" 
                            CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                            style="text-align:right"  visible="false"></asp:TextBox>
                    </td>

        </tr>
    </table>
</div>

     <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
         AssociatedUpdatePanelID="UpdatePanel1">
         <ProgressTemplate>
             <div ID="blur">
                 <asp:Panel ID="Panel1" runat="server">
                     <asp:Panel ID="Panel3" runat="server">
                         <asp:Panel ID="Panel4" runat="server">
                         </asp:Panel>
                     </asp:Panel>
                 </asp:Panel>
             </div>
             <div ID="progress">
                 <img alt="progress" src="Images/progress.gif" /> Please Wait...
             </div>
         </ProgressTemplate>
     </asp:UpdateProgress>

</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

