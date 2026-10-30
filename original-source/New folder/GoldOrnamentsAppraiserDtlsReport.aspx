<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="GoldOrnamentsAppraiserDtlsReport.aspx.vb" Inherits="SMARTCORE_UI.GoldOrnamentsAppraiserDtlsReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .style1 
        {
            width: 90px;
        }
     </style>

    <script language="javascript" type="text/javascript">

        function validatedate(inputText) {
            var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            // Match the date format through regular expression  
            if (inputText.value.match(dateformat)) {
                //  document.form1.text1.focus();  
                //Test which seperator is used '/' or '-'  
                var opera1 = inputText.value.split('/');
                var opera2 = inputText.value.split('-');
                lopera1 = opera1.length;
                lopera2 = opera2.length;
                // Extract the string into month, date and year  
                if (lopera1 > 1) {
                    var pdate = inputText.value.split('/');
                }
                else if (lopera2 > 1) { 
                    var pdate = inputText.value.split('-');
                }
                var dd = parseInt(pdate[0]);
                var mm = parseInt(pdate[1]);
                var yy = parseInt(pdate[2]);
                // Create list of days of a month [assume there is no leap year by default]  
                var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
                if (mm == 1 || mm > 2) {
                    if (dd > ListofDays[mm - 1]) {
                        alert('Invalid date format! Please enter a valid date!');
                        return false;
                    }
                }
                if (mm == 2) {
                    var lyear = false;
                    if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                        lyear = true;
                    }
                    if ((lyear == false) && (dd >= 29)) {
                        alert('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alert('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                }
            }
            else {
                alert("Invalid date format! Please enter a valid date!");

                document.form1.text1.focus();
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"></asp:ToolkitScriptManager>
            <%--<div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left: 15%; margin-right: 15%; font-family: Tahoma">--%>

                <legend>Gold Ornaments Appraiser Details</legend>
                <table width="100%">
                    <tr>
                        <td width="1%"></td>
                        <td width="15%">
                            <td width="2%"></td>
                            <td width="20%"></td>
                            <td width="2%"></td>
                            <td></td>
                            <td width="30%"></td>
                            <td width="2%"></td>
                    </tr>

                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style1"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                        <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style1">
                            <asp:Label ID="Label1" runat="server" Font-Names="Tahoma"
                                Font-Size="Medium" ForeColor="#6666FF" Text="Region"></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            <asp:DropDownList ID="DdlRegion" runat="server" Height="26px" Width="298px"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style1">
                            <asp:Label ID="Label2" runat="server" Font-Names="Tahoma"
                                Font-Size="Medium" ForeColor="#6666FF" Text="Area"></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            <asp:DropDownList ID="DdlArea" runat="server" Height="26px" Width="298px"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style1">
                            <asp:Label ID="Lbl_Branch" runat="server" Font-Names="Tahoma"
                                Font-Size="Medium" ForeColor="#6666FF" Text="Branch"></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            <asp:DropDownList ID="Ddl_Branch" runat="server" Height="26px" Width="298px"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                              <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style1">
                            <asp:Label ID="Label3" runat="server" Font-Names="Tahoma"
                                Font-Size="Medium" ForeColor="#6666FF" Text="Today's Gold Rate"></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="TxtTdayGrate" runat="server" Height="26px" Width="298px"
                                AutoPostBack="True">
                            </asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style1">
                            <asp:Label ID="Lbl_FromDate" runat="server" Font-Names="Tahoma"
                                Font-Size="Medium" ForeColor="#6666FF" Text="From Date"></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="Txt_FrDate" runat="server" BackColor="White" OnChange="validatedate(this);"
                                TabIndex="4" Height="22px" Width="243px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="Txt_FrDate">
                            </asp:CalendarExtender>
                            <asp:Image ID="ImgDoa" runat="server" BorderStyle="Outset" Height="24px"
                                ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1"
                                Width="21px" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style1">
                            <asp:Label ID="Lbl_ToDate" runat="server" Font-Names="Tahoma"
                                Font-Size="Medium" ForeColor="#6666FF" Text="To Date"></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="Txt_ToDate" runat="server" BackColor="White" Height="22px" OnChange="validatedate(this);"
                                TabIndex="4" Width="243px" AutoPostBack="True"></asp:TextBox>
                            <asp:CalendarExtender ID="Txt_ToDate_CalendarExtender" runat="server"
                                Format="dd/MM/yyyy" TargetControlID="Txt_ToDate">
                            </asp:CalendarExtender>
                            <asp:Image ID="ImgDoa0" runat="server" BorderStyle="Outset" Height="24px"
                                ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="21px" />

                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style1"></td>
                        <td></td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td width="50%">
                                        <asp:ImageButton ID="BtnShow" runat="server" Height="22px"
                                            ImageUrl="~/Images/Show.png" Width="80px" OnClick="ImageButton2_Click1" />
                                    </td>
                                    <td><asp:Button ID="Button1" runat="server" ForeColor="#9933FF" Text="Button"  /></td>
                                    <td><asp:Button ID="Button2" runat="server" ForeColor="#9933FF" Text="Button1"  /></td>
                                    <td width="50%">
                                        <asp:ImageButton ID="BtnClose" runat="server" Height="22px"
                                            ImageUrl="~/Images/CloseBtn.png" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <br />
                <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
