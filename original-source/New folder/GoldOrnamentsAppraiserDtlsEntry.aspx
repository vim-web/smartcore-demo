<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="GoldOrnamentsAppraiserDtlsEntry.aspx.vb" Inherits="SMARTCORE_UI.GoldOrnamentsAppraiserDtlsEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
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
                <table>
                    <tr>

                        <td>
                            <%--<asp:GridView ID="GridView1" Width="100%"  AutoGenerateColumns="False" HeaderStyle-BackColor="#2063b7" Font-Bold="False"  HeaderStyle-ForeColor="White"
                  runat="server" BackColor="#D9D9D9" ForeColor="#282C31" DataKeyNames="ID,BrCode,Acc_Code" EnableModelValidation="True" >--%>
       <asp:GridView ID="GvDetails" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#2063b7" Font-Bold="False" HeaderStyle-ForeColor="White"
                    runat="server" BackColor="#D9D9D9" ForeColor="#282C31" EnableModelValidation="True" DataKeyNames="Acc_ID,slno">
                                <Columns>          
                                    <asp:BoundField DataField="Loan_Date" HeaderText="Loan Date"></asp:BoundField>
                                    <asp:BoundField DataField="Sch_Name" HeaderText="Scheme"></asp:BoundField>
                                    <asp:BoundField DataField="Acc_No" HeaderText="Account No."></asp:BoundField>
                                    <asp:BoundField DataField="Lm_LoanAmount" HeaderText="Loan Amount"></asp:BoundField>
                                    <asp:BoundField DataField="Pkc_Description" HeaderText="Item"></asp:BoundField>
                                    <asp:BoundField DataField="Orn_Count" HeaderText="Count"></asp:BoundField>
                                    <asp:BoundField DataField="Orn_GrossWt" HeaderText="Gross Weight"></asp:BoundField>
                                    <asp:BoundField DataField="Orn_NetWt" HeaderText="Net Weight"></asp:BoundField>
                                    <%--<asp:BoundField DataField="Purity" HeaderText="Purity"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Appraisal Gross Weight">
                                    <ItemTemplate ><asp:TextBox ID="TxtAppGw" runat="server" Text='<%# Eval("Appraiser_Gw")%>'></asp:TextBox></ItemTemplate>
                            
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stone">
                                    <ItemTemplate ><asp:TextBox ID="TxtStone" runat="server" Text='<%# Eval("Stone")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appraisal Net Weight">
                                    <ItemTemplate ><asp:TextBox ID="TxtAppNw" runat="server" Text='<%# Eval("Appraiser_Nw")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excess">
                                    <ItemTemplate ><asp:TextBox ID="TxtExcess" runat="server" Text='<%# Eval("Excess")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="916H">
                                    <ItemTemplate ><asp:TextBox ID="Txt916H" runat="server" Text='<%# Eval("916H")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="916">
                                    <ItemTemplate ><asp:TextBox ID="Txt916" runat="server" Text='<%# Eval("916")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="22ct">
                                    <ItemTemplate ><asp:TextBox ID="Txt22ct" runat="server" Text='<%# Eval("22ct")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="20ct">
                                    <ItemTemplate ><asp:TextBox ID="Txt20ct" runat="server" Text='<%# Eval("20ct")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="18ct">
                                    <ItemTemplate ><asp:TextBox ID="Txt18ct" runat="server" Text='<%# Eval("18ct")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate ><asp:TextBox ID="TxtRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                 <%--   <asp:BoundField DataField="Appraiser_Gw" HeaderText="Appraisal Gross Weight"></asp:BoundField>
                                    <asp:BoundField DataField="Stone" HeaderText="Stone"></asp:BoundField>
                                    <asp:BoundField DataField="Appraiser_Nw" HeaderText="Appraisal Net Weight"></asp:BoundField>
                                    <asp:BoundField DataField="Excess" HeaderText="Excess"></asp:BoundField>
                                    <asp:BoundField DataField="916H" HeaderText="916H"></asp:BoundField>
                                    <asp:BoundField DataField="916" HeaderText="916"></asp:BoundField>
                                    <asp:BoundField DataField="22ct" HeaderText="22ct"></asp:BoundField>
                                    <asp:BoundField DataField="20ct" HeaderText="20ct"></asp:BoundField>
                                    <asp:BoundField DataField="18ct" HeaderText="18ct"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>--%>

                                    <asp:BoundField DataField="Acc_ID" HeaderText="Acc_ID" Visible="False">
                                        <ItemStyle CssClass="hidden" />
                                        <HeaderStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="slno" HeaderText="slno" Visible="False">
                                        <ItemStyle CssClass="hidden" />
                                        <HeaderStyle CssClass="hidden" />
                                    </asp:BoundField>
                                   <%-- <asp:TemplateField HeaderText="Test">
                                    <ItemTemplate ><asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("18ct")%>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:ButtonField ButtonType="Button" CommandName="Save" Text="Save" />
                                </Columns>
                                <HeaderStyle BackColor="#2063B7" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>


                <br />
                <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
