<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="VerifyBnkDtls.aspx.vb" Inherits="SMARTCORE_UI.VerifyBnkDtls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery_json.js"></script>
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>

    <script type="text/javascript">

        function fileCheck(obj) {
            var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
            if ($.inArray($(obj).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only '.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.");
                $(obj).attr("value", "");
            }
        }

        //          function ValidateAccNo() {
        //              var objDDl = document.getElementById('<%=CmbScheme.ClientID%>');
        //              var Sch = objDDl.options[objDDl.selectedIndex].text;
        //              if (Sch == 'Select') {
        //                  alertify.error('Please select the scheme');
        //                  document.getElementById("<%=TxtAccNo.ClientID%>").value = "";
        //                  return false;
        //              }
        //          }




        function AllowIFSC() {
            var ifsc = document.getElementById('<%=TxtIfscCode.ClientID%>').value;
              var reg = /[A-Z|a-z]{4}[0][a-zA-Z0-9]{6}$/;

              if (ifsc.match(reg)) {
                  return true;
              }
              else {
                  alertify.error("Wrong IFSC Code");

                  document.getElementById("<%=TxtIfscCode.ClientID%>").value = "";
                  document.getElementById("<%=TxtIfscCode.ClientID%>").focus();

                  return false;
              }

          }



          function ValidateShow() {
              var objDDl = document.getElementById('<%=CmbScheme.ClientID%>');
              var Sch = objDDl.options[objDDl.selectedIndex].text;
              if (Sch == 'Select') {
                  alertify.error('Please select the scheme');
                  return false;
              }
              if (document.getElementById("<%=TxtAccNo.ClientID%>").value == "") {
                  alertify.error('Please Enter the Account No');
                  return false;
              }
              if (document.getElementById("<%=TxtBnkAccNo.ClientID%>").value == "") {
                  alertify.error('Please Enter the Bank Account No');
                  return false;
              }

          }

          function ValidateSave() {
              var objDDl = document.getElementById('<%=CmbScheme.ClientID%>');
              var Sch = objDDl.options[objDDl.selectedIndex].text;
              if (Sch == 'Select') {
                  alertify.error('Please select the scheme');
                  return false;
              }
              if (document.getElementById("<%=TxtAccNo.ClientID%>").value == "") {
                  alertify.error('Please Enter the Account No');
                  return false;
              }
              if (document.getElementById("<%=TxtBnkAccNo.ClientID%>").value == "") {
                  alertify.error('Please Enter the Bank Account No');
                  return false;
              }
              if (document.getElementById("<%=TxtIfscCode.ClientID%>").value == "") {
                  alertify.error('Please Enter the IFSC Code');
                  return false;
              }

          }

    </script>


    <style type="text/css">
        .style3 {
            width: 212px;
        }

        .style9 {
        }

        .style11 {
            width: 114px;
        }

        .style12 {
            width: 337px;
        }

        .style13 {
            width: 364px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left: 20%; margin-right: 20%;">
        <legend>Customer Bank Details</legend>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td class="style11">&nbsp;</td>
                        <td class="style9">&nbsp;</td>
                        <td class="style12">&nbsp;</td>
                        <td class="style3">&nbsp;</td>
                        <td class="style13">&nbsp;</td>
                        <td>

                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"
                                EnablePageMethods="True" CombineScripts="True">
                                <Scripts>
                                    <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
                                </Scripts>
                            </asp:ToolkitScriptManager>

                        </td>
                    </tr>
                    <tr>
                        <td class="style11">&nbsp;</td>
                        <td class="style9">
                            <label>Branch</label></td>
                        <td class="style12">
                            <asp:DropDownList ID="CmbBranch" runat="server" Width="75%" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td class="style3">
                            <label>Scheme </label>
                        </td>
                        <td class="style13">
                            <asp:DropDownList ID="CmbScheme" runat="server" Width="80%"
                                AutoPostBack="True" TabIndex="1">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style11">&nbsp;</td>
                        <td class="style3">
                            <label>
                                Account No</label></td>
                        <td class="style12">
                            <asp:TextBox ID="TxtAccNo"
                                runat="server" AutoPostBack="True"
                                CssClass="twitterStyleTextbox" TabIndex="6" Width="76%" MaxLength="13"></asp:TextBox>
                            <asp:ImageButton ID="ImgSrch" runat="server" Height="20px"
                                ImageUrl="~/images/Search.png" Width="23px" />
                        </td>
                        <td class="style3">
                            <label>
                                Customer Id</label></td>
                        <td class="style13">
                            <asp:TextBox ID="Txt_CustId" runat="server" Width="76%" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style11">&nbsp;</td>
                        <td class="style9">
                            <label>
                                Customer Name</label></td>
                        <td class="style12">
                            <asp:TextBox ID="Txt_CustName" runat="server" TabIndex="7" Width="76%"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="style3">
                            <label>
                                Customer Address</label></td>
                        <td class="style13">
                            <asp:TextBox ID="Txt_CustAddress" runat="server" TabIndex="8"
                                TextMode="MultiLine" Width="76%" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                       <tr>
                        <td class="style11">&nbsp;</td>
                        <td class="style9">
                            <label>
                               Benificiary Relation</label></td>
                        <td class="style12">
                           <asp:DropDownList ID="CmbRelType" runat="server" Width="76%"
                meta:resourcekey="CmbRelTypeResource1" TabIndex="6">
            </asp:DropDownList>
                        </td>
                        <td class="style3">
                            <label>
                                 Benificiary Name</label></td>
                        <td class="style13">
                            <asp:TextBox ID="Txt_BenfName" runat="server"
                                CssClass="twitterStyleTextbox" 
                                TabIndex="6" Width="76%"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style11">&nbsp;</td>
                        <td class="style9">
                            <label>
                                Bank Name</label></td>
                        <td class="style12">
                            <asp:TextBox ID="Txt_BAnkName" runat="server"
                                CssClass="twitterStyleTextbox" TabIndex="6" Width="76%"></asp:TextBox>
                        </td>
                        <td class="style3">
                            <label>
                                Bank Branch</label></td>
                        <td class="style13">
                            <asp:TextBox ID="Txt_BAnkBranch" runat="server"
                                CssClass="twitterStyleTextbox" 
                                TabIndex="6" Width="76%"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style11">&nbsp;</td>
                        <td class="style9">
                            <label>
                                Bank Account No</label></td>
                        <td class="style12">
                            <asp:TextBox ID="TxtBnkAccNo" runat="server" AutoPostBack="True"
                                CssClass="twitterStyleTextbox" MaxLength="16" TabIndex="6" Width="76%"></asp:TextBox>
                        </td>
                        <td class="style3">
                            <label>
                                IFSC Code</label></td>
                        <td class="style13">
                            <asp:TextBox ID="TxtIfscCode" runat="server" AutoPostBack="True"
                                CssClass="twitterStyleTextbox" MaxLength="11" onchange="return AllowIFSC();"
                                TabIndex="6" Width="76%"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

            
                    <tr>
                        <td class="style11">&nbsp;</td>
                          
                        <td class="style9">&nbsp;</td>
                       
                        
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>

                        <td width="20%"></td>
                        <td width="60%"> <asp:ImageButton ID="CmdSave0" runat="server" ImageUrl="~/images/save.png"
                                meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();"
                                TabIndex="35" />
                         
                           <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png"
                                meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                            
                           <asp:ImageButton ID="CmdClose0" runat="server" Height="31px"
                                ImageUrl="~/images/Close.png" meta:resourcekey="CmdClearResource1"
                                PostBackUrl="~/Main.aspx" TabIndex="37" /> </td>
                        <td width="20%"></td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>

                <%-- <asp:PostBackTrigger ControlID = "CmdAdd" />
           <asp:PostBackTrigger ControlID = "btn_Upload" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
