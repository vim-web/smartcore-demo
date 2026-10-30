<%@ Page Language="vb" MasterPageFile="~/SMARTCORE.Master"  AutoEventWireup="false" CodeBehind="Suspense.aspx.vb" Inherits="SMARTCORE_UI.Suspense" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="scripts/jquery_json.js"></script>
<link rel="stylesheet" href="CSS/Main.css" />


<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script type = "text/javascript" src = "http://www.aspsnippets.com/scripts/ScrollableGridPlugin.js?22_12"></script>
    <script type = "text/javascript">
    
    // To Bind the function to the textbox for moving the cursor from one textbox to another  
            $(document).ready(function () {  
                $("input:text:first").focus();  
                $("input:text").bind("keydown", function (e) {  
                    if (e.which == 13) {  
                        e.preventDefault();  
                        var nextIndex = $('input:text').index(this) + 1;  
                        if ($('input:text')[nextIndex] != null)  
                            $('input:text')[nextIndex].focus();  
                    }  
                });  
            });  
            
          </script>




<style type="text/css">
    .style1
    {
        width: 238px;
    }
    .style2
    {
        width: 82%;
    }
</style>




</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<div class="gradientBoxesWithOuterShadows" width: 100%; margin-left:25%; margin-right:25%; >                
<br />

<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
</asp:ToolkitScriptManager>
<table width="100%" border="0" style="font-family: Tahoma">
  <tr>
    <td width="2%"></td>
    <td width="20%"><label>Branch</label></td>
    <td width="27%">
            <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
	<td width="2%"></td>
    <td width="20%"><label>Mode of Operation</label></td>
    <td width="27%">
            <asp:DropDownList ID="CmbOpMode" runat="server" Width="100%">
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
            <asp:TextBox ID="TxtNoOfOperator" runat="server" Width = "100%"></asp:TextBox>
        </td>
    <td></td>
  </tr>
</table>
<table width="100%" border="0">
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
<br />
    
    <table width="100%" 
         style="font-family: Tahoma; font-size: medium; color: #0066FF">

            <tr>
                <td></td>
                <td class="style1" >
                    <asp:Label ID="LblAdmitDate" runat="server" Text="Admit Date"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TxtAdmitdate" runat="server" Width="97%"></asp:TextBox>
                </td>
                <td">
                    </td>
                    <td></td>
                <td>
                    <asp:Label ID="LblAccountDetails" runat="server" Text="Account Details" 
                        Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    </td>
            </tr>
            <tr>
            <td></td>
                <td class="style1" >
                    <asp:Label ID="lblReftype" runat="server" Text="Ref.Type"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dRPREFTYPE" runat="server" Height="100%" Width="100%">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                    
                <td>
                    <asp:Label ID="LblSchemes" runat="server" Text="Schemes"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="Drpschemes" runat="server" Height="100%" Width="54%">
                    </asp:DropDownList>
                </td>
                <td >
                    </td>
            </tr>
            <tr>
            <td></td>
                <td class="style1" >
                    <asp:Label ID="LblTockenNo" runat="server" Text="Tocken No"></asp:Label>
                </td>
                <td  colspan="2">
                    <asp:TextBox ID="TxtTocknNo" runat="server" Width="90%" Height="100%"></asp:TextBox>
                </td>
               
                <td colspan="1" >
                    <asp:Label ID="LblAccno" runat="server" Text="Acc No"></asp:Label>
                </td>
                <td colspan="3">
                
                    <asp:TextBox ID="TxtAccno" runat="server" Height="100%" Width="53%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td class="style1" >
                    <asp:Label ID="LblNarration" runat="server" Width="100%" Text="Narration"></asp:Label>
                </td>
                    
                <td>
                    
                    <asp:TextBox ID="TxtNarration" runat="server" Width="200%" ></asp:TextBox>
                    
                </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    
            </tr>
            <tr>
            <td></td>
            
                <td colspan="5">
   
     
   
     <asp:GridView ID="DgvShareMaster" runat="server" Width="100%"  AutoGenerateColumns="False"
        CssClass="grid" TabIndex="17" ShowFooter="True" 
        >
         <Columns>
             <asp:BoundField DataField="SlNo" HeaderText="Sl NO" />
             <asp:TemplateField HeaderText="Account Head">
                 <ItemTemplate>
                     <asp:DropDownList ID="Ddl_AccountHead" runat="server" Height="23px" 
                         Width="193px">
                     </asp:DropDownList>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Receipt Cash">
                 <ItemTemplate>
                     <asp:TextBox ID="Txt_ReceiptCash" runat="server"></asp:TextBox>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Receipt Transfer">
                 <ItemTemplate>
                     <asp:TextBox ID="Txt_ReceiptTransfer" runat="server"></asp:TextBox>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Payment Cash">
                 <ItemTemplate>
                     <asp:TextBox ID="Txt_PaymentCash" runat="server"></asp:TextBox>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Payment Transfer">
                 <FooterTemplate>
                     <asp:ImageButton ID="ImageButton3" runat="server" Height="40px" 
                         ImageUrl="~/images/AddBtn.png" Width="81px" />
                 </FooterTemplate>
                 <ItemTemplate>
                     <asp:TextBox ID="Txt_PaymentTransfer" runat="server"></asp:TextBox>
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
         <FooterStyle CssClass="gridFooterRow"></FooterStyle>
           

    </asp:GridView>
    
    
    
    
    
                </td>
              
                <td>
                    </td>
                  <td></td>
            </tr>
            </table>
    
    
<table width="100%">
	<tr>
	<td class="style2"></td>
	<td width = "30%">
            <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" />
            <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" />
    </td>
	<td width = "35%"></td>
	</tr>
	</table>
       
    
   </div> 
    
            </ContentTemplate>
        </asp:UpdatePanel> 
        </asp:Content>


    
    
    