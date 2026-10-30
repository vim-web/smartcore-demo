<%@ Page Language="vb" MasterPageFile ="~/SMARTCORE.Master"  AutoEventWireup="false" CodeBehind="DepositLoan.aspx.vb" Inherits="SMARTCORE_UI.DepositLoan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="scripts/jquery_json.js"></script>
<link rel="stylesheet" href="CSS/Main.css" />
<script type="text/javascript" src="Scripts/jquery_json.js"></script>

    <script type="text/javascript">
        $(function() {
            var Ctl = document.getElementById('<%=Txt_SanctionedAmt.ClientID%>');
            Ctl.focus();
        });

        function Save() {

            var r = confirm("Do You Want to Save ? ");
            if (r == true) {
                r = true;
            }
            else {
                r = false;
            }

            return r;
        }  
        
        
        
    </script>
 
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
   
      
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

 <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;" >  
    
      <asp:Panel ID="PnlAuthorise" runat="server">
                              
    <legend>DEPOSIT LOAN</legend>   
    
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<div>
    
    
        <table width="100%">
                        
        </table>
    
       <table width="100%" border="0" style="font-family: Tahoma">
      <tr>
        <td width="2%"></td>
        <td width="20%"><label>Branch</label></td>
        <td width="27%">
                <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" TabIndex="999">
                </asp:DropDownList>
            </td>
	    <td width="2%"></td>
        <td width="20%"><label>Mode of Operation</label></td>
        <td width="27%">
                <asp:DropDownList ID="CmbOpMode" runat="server" Width="100%" TabIndex="999">
                </asp:DropDownList>
            </td>
        <td width="2%"></td>
      </tr>
      <tr>
        <td></td>
        <td><label>Scheme</label></td>
        <td>
                <asp:DropDownList ID="CmbScheme" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        <td></td>
        <td><label>No of Operators</label></td>
        <td>
                <asp:TextBox ID="TxtNoOfOperator" runat="server" Width = "100%" TabIndex="999"></asp:TextBox>
            </td>
        <td></td>
      </tr>
    </table>
     <table width="100%">
          <tr>
          <td width="2%"></td>
          <td width="96%">
                        <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="CustID" TabIndex="30" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
                                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                        Width="15%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MemBrCode" HeaderText="Member BrCode" 
                                    Visible="False" />
                                <asp:BoundField DataField="MemSchCode" HeaderText="MemSchCode" 
                                    Visible="False" />
                                <asp:BoundField DataField="MemNo" HeaderText="Member No" Visible="False">
                                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CustID" HeaderText="Customer ID">
                                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="8%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                                    HeaderText="Name">
                                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                        Width="17%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Relation" HeaderText="Relation">
                                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                        Width="20%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CustAdd" HeaderText="Address">
                                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                        Width="30%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CustType" HeaderText="CustType" Visible="False" />
                                <asp:BoundField DataField="CustCatg" HeaderText="CustCatg" Visible="False" />
                                <asp:BoundField DataField="HouseNm" HeaderText="HouseNm" Visible="False" />
                                <asp:BoundField DataField="Place" HeaderText="Place" Visible="False" />
                                <asp:BoundField DataField="Dist" HeaderText="Dist" Visible="False" />
                                <asp:BoundField DataField="State" HeaderText="State" Visible="False" />
                                <asp:BoundField DataField="Country" HeaderText="Country" Visible="False" />
                                <asp:BoundField DataField="Pincode" HeaderText="PinCode" Visible="False" />
                                <asp:BoundField DataField="MblNo" HeaderText="MblNo" Visible="False" />
                                <asp:BoundField DataField="LandPh" HeaderText="LandPh" Visible="False" />
                                <asp:BoundField DataField="RelType" HeaderText="RelType" Visible="False" />
                                <asp:BoundField DataField="Dob" HeaderText="Dob">
                                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                        Width="6%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Age" HeaderText="Age">
                                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                        Width="4%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                                    ShowDeleteButton="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F4FF" />
                        </asp:GridView>
                </td>
                
            <td width="2%"></td>
        </tr>
    </table>
        <table  width="100%">
        
              <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="10%">&nbsp;</td>
        <td width="1%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="10%">&nbsp;</td>
        <td width="1%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="10%">&nbsp;</td>
        <td width="1%">&nbsp;</td>
        <td width="18%">&nbsp;</td>
        <td width="2%" >&nbsp;</td>
      </tr>
      
            <tr>
            <td></td>
            <td colspan="10">
    
                <asp:GridView ID="DgvSecDeposit" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="AccID" Font-Names="Tahoma" Font-Size="Medium" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="AccID" HeaderText="AccID" Visible="False">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Branch" HeaderText="Branch">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Scheme" HeaderText="Scheme">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccNo" HeaderText="Acc No">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BALANCE" HeaderText="BALANCE">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" 
                                HorizontalAlign="Right" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BALTYPE" HeaderText="BAL TYPE">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" 
                                HorizontalAlign="Center" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="INTRATE" HeaderText="INT RATE">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" 
                                HorizontalAlign="Right" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DUEDATE" HeaderText="DUEDATE">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                            ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
                </td>
                <td></td>
            </tr>
            </table>
            
            <table width="100%">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="2%" >&nbsp;</td>
      </tr>
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Loan Date</label></td>
        <td width="15%"><asp:TextBox ID="TxtLoanDt" runat="server" Width="100%" ></asp:TextBox></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Sec Dep Amount</label></td>
        <td width="15%"><asp:TextBox ID="Txt_SecDepAmt" runat="server" Width="100%"> </asp:TextBox></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Eligible Amt</label></td>
        <td width="15%" colspan="2"><asp:TextBox ID="Txt_ElgibleAmt" runat="server"  Width="100%"></asp:TextBox></td>
        <td width="2%" >&nbsp;</td>
      </tr>
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%"> <label>Month</label></td>
        <td width="15%"><asp:TextBox ID="Txt_Month" runat="server"  Width="100%"></asp:TextBox></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Days</label></td>
        <td width="15%"><asp:TextBox ID="Txt_Days" runat="server"  Width="100%"></asp:TextBox></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Due Date</label></td>
        <td width="15%">
            <asp:TextBox ID="Ce_DueDate" runat="server"  Width="100%"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="Ce_DueDate">
                </asp:CalendarExtender>
                </td>
                <td>
                <asp:Image ID="ImgDoa" runat="server" BorderStyle="Outset" Height="24px" 
                ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="17px" />
        </td>
        <td width="2%">&nbsp;</td>
        <td width="2%" >&nbsp;</td>
        </tr>
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Sanctioned Amt</label></td>
        <td width="15%"><asp:TextBox ID="Txt_SanctionedAmt" runat="server"  Width="100%" 
                AutoPostBack="True"></asp:TextBox></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Interest %</label></td>
        <td width="15%"><asp:TextBox ID="Txt_InterestRate" runat="server"  Width="100%"></asp:TextBox></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Sanctioned By</label></td>
        <td width="15%" colspan="2"><asp:DropDownList ID="CmbSanctionedBy" runat="server" Width="100%" >
            </asp:DropDownList>
        </td>
        <td width="2%" >&nbsp;</td>
      </tr>
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Purpose</label></td>
        <td width="15%"><asp:DropDownList ID="CmbPurpose" runat="server"  Width="100%" 
                AutoPostBack="True">
                    </asp:DropDownList>
        </td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Sub Purpose</label></td>
        <td width="15%"><asp:DropDownList ID="CmbSubPurpose" runat="server"  Width="100%">
                    </asp:DropDownList></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Priority</label></td>
        <td width="15%" colspan ="2" ><asp:DropDownList ID="Ddl_Priority" runat="server"  Width="100%">
                    </asp:DropDownList></td>
        <td width="2%" >&nbsp;</td>
      </tr>
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Tocken No</label></td>
        <td width="15%"><asp:TextBox ID="Txt_TockenNo" runat="server"  Width="100%"></asp:TextBox></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"><label>Cash/Transfer</label></td>
        <td width="15%"><asp:DropDownList ID="CmbCashTransfer" runat="server"  Width="100%" 
                AutoPostBack="True"></asp:DropDownList></td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="2%" >&nbsp;</td>
      </tr>            
            <tr>
            <td width="2%">&nbsp;</td>
            <td colspan="9">
                <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
                    TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccHead" HeaderText="AccHead">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="30%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PayCash" HeaderText="Payment Cash">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                    </Columns>
                </asp:GridView> 
                </td>
                <td width="2%">&nbsp;</td>
            </tr>
           
            <tr>
            <td></td>
            <td>
                <asp:HiddenField ID="HdnSecDepType" runat="server" EnableViewState="False" />
                </td>
            <td></td>
            <td></td>
            <td></td>  
            <td></td>
            <td></td>
              </asp:Panel>
           </table>
            <table width= "100%">   
            <td>
                <asp:ImageButton ID="Cmd_Save" runat="server" ImageAlign="Right" 
                    ImageUrl="~/images/Save.png"  OnClientClick="return Save();"  />
                </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton5" runat="server" 
                    ImageUrl="~/images/Close.png" />
                </td>          
            </tr>
                       
        </table>
    
    </div>
         </ContentTemplate>
        </asp:UpdatePanel> 
        </asp:Content>


    
