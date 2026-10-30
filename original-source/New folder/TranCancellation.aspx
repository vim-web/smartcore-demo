<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="TranCancellation.aspx.vb" Inherits="SMARTCORE_UI.TranCancellation" MasterPageFile="~/SMARTCORE.Master"  EnableSessionState="True" %>


<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    
        <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
        <script type='text/javascript' src="scripts/jquery_json.js"></script>
       
<style type="text/css">                    
    .TblColor {
	    color: #92D0F5;
    }
    .TblClr {
	    border-top-style: groove;
	    border-right-style: groove;
	    border-bottom-style: groove;
	    border-left-style: groove;
	    border-top-color:  #03F;
	    border-right-color: #92D0F5;
	    border-bottom-color: #92D0F5;
	    border-left-color: #92D0F5;
    }
    table.sample {
	    border-width: thin;
	    border-spacing: 1px;
	    border-style: solid;
	    border-color: rgb(255, 248, 236);
	    border-collapse: collapse;
	    color: #92D0F5;
    }
    table.sample th {
	    border-width: 1px;
	    padding: 0px;
	    border-style: solid;
	    border-color: rgb(255, 248, 236);
    }
    table.sample td {
	    border-width: 1px;
	    padding: 0px;
	    border-style: solid;
	    border-color: rgb(255, 248, 236);
    }
        .floatingDiv
        {
          position:absolute;   
          top: 300px;
          box-shadow: 10px 10px 10px #888888;
          color: #42a2d7;
          background-color:#FFFFFF;
          border:1px solid #000000; 
          border-radius:3px; 
          width:800px; 
          height:auto; 
          left:0px;
          display:none;
        }

    </style>  
        
<script type="text/javascript" >
  $(document).ready(function () {  
  $('#imgIDProof1').click(function()
  {
    $('#PassingDiv').slideToggle('slow');
     return false;
  });     
  $('#ImgIDClose1').click(function()
  {
    $('#PassingDiv').slideToggle('slow');
     return false;
  });     
});  
</script>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div style="float:left; width:100%;"  > 
               <table width ="100%">
               <tr>
                <td width ="15%">
                     </td>
                <td width ="15%">
                     </td>
                <td width ="40%">
                <table  width ="100%">
                <tr>
                <td width ="15%">
                </td>
                <td  width ="70%">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial Black" 
                        Font-Size="14pt" ForeColor="#CC0000" Text="TRANSACTION CANCELLATION" 
                        Width="100%"></asp:Label>
                </td>
                </tr>
                
                </table>
                      </td>
                <td width ="15%">
                     </td>
                <td width ="15%">
                     </td>
               </tr>

                   <tr>
                       <td width="15%">
                           <asp:RadioButton ID="OptPassed" runat="server" GroupName="Passing" 
                               Text="Passed Entries" Width="100%" />
                       </td>
                       <td width="15%">
                           &nbsp;</td>
                       <td width="40%">
                       <table width ="100%">
                           <tr>
                               <td width ="40%">
                                   <label>
                                   Search With Scheme </label></td>
                               <td width ="5%">
                                   <asp:CheckBox ID="ChkScheme" runat="server" />
                               
                               </td>
                               <td width ="40%">
                                   <asp:TextBox ID="TxtScheme" runat="server" Width="95%"></asp:TextBox>
                               </td>
                               <td width ="15%">
                               </td>

                           </tr>


                       </table>
                       
                       </td>
                       <td width="15%">
                       
                                   <label>No of Passed Transaction </label>

                       </td>
                       <td width="15%">
                           <asp:TextBox ID="TxtPassed" runat="server" Width="50%"></asp:TextBox>
                       </td>
                   </tr>

               <tr>
                <td width ="15%">
                    <asp:RadioButton ID="OptPending" runat="server" Checked="True" 
                        GroupName="Passing" Text="Pending Entries" width="100%" />
                </td>
                <td width ="15%">
                     </td>
                <td width ="40%">
                       <table width ="100%">
                           <tr>
                               <td width ="40%">
                                   <label>
                                   Search With Amount</label></td>
                               <td width ="5%">
                                   <asp:CheckBox ID="ChkAmount" runat="server" />
                               </td>
                               <td width ="40%">
                                   <asp:TextBox ID="TxtAmt" runat="server" Width="95%"></asp:TextBox>
                               </td>
                               <td width ="15%">
                               </td>

                           </tr>


                       </table>
                </td>
                <td width ="15%">
                    <asp:Button ID="BtnAuthorise" runat="server" Height="27px" Text="Authorise" 
                        Visible="False" Width="40%" />
                </td>
                <td width ="15%">
                     </td>
               </tr>

               <tr>
                <td width ="15%">
                    <asp:RadioButton ID="OptAll" runat="server" GroupName="Passing" 
                        Text="All Entries" Width="100%" />
                </td>
                <td width ="15%">
                    &nbsp;</td>
                <td width ="40%">
                       <table width ="100%">
                           <tr>
                               <td width ="40%">
                                   <label>
                                   Search   Account No</label></td>
                               <td width ="5%">
                                   <asp:CheckBox ID="ChkAccNo" runat="server" />
                               </td>
                               <td width ="40%">
                                   <asp:TextBox ID="TxtAccno00" runat="server" Width="95%"></asp:TextBox>
                                    </td>
                               <td width ="15%">
                               </td>

                           </tr>


                       </table>
                     </td>
                <td width ="15%">
                 <label>No of Pending Transaction</label>

                </td>
                <td width ="15%">
                    <asp:TextBox ID="TxtPending" runat="server" width="50%"></asp:TextBox>
                </td>
               </tr>
                   <tr>
                       <td colspan="5">
                       <hr  />
                          </td>
                   </tr>
                   <tr>
                       <td width="15%">
                           <label>Branch</label> </td>
                       <td width="15%">
                           <asp:DropDownList ID="DrpBranch" runat="server" Width="100%">
                           </asp:DropDownList>
                       </td>
                       <td width="40%">
                           <asp:Button ID="BtnRefresh" runat="server" Height="27px" Text="Refresh" 
                               Width="40%" />
                           <asp:Button ID="Close" runat="server" Height="27px" Text="Close" Width="40%" />
                       </td>
                       <td width="15%">
                            &nbsp;</td>
                       <td width="15%">
                            &nbsp;</td>
                   </tr>
               </table>   

            </div>

 
         <div width: 100%>
        <asp:GridView ID="DgvPassing" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
            TabIndex="8" Width="100%" AllowPaging="True"  
                 onrowcommand="DgvPassing_RowCommand" DataKeyNames="Tran_No,Authorised,VouNo" PageSize="15">
            <RowStyle Height="20px" />
            <Columns>
                <asp:CommandField SelectText="Delete" ShowSelectButton="True">
                    
                </asp:CommandField>
                <asp:CheckBoxField DataField="Authorised">
                    
                </asp:CheckBoxField>
               
                
                <asp:BoundField  HeaderText="Vou No" DataField="VouNo">
                  
                </asp:BoundField>
                <asp:BoundField  HeaderText="Particulars" DataField="Particulars">
                   
                </asp:BoundField>
                <asp:BoundField  HeaderText="Rec Cash" DataField="RecCash">
                   
                </asp:BoundField>
                <asp:BoundField  DataFormatString="{0:F2}" 
                    HeaderText="Rec Transfer" DataField="RecTransfer">
                    
                </asp:BoundField>
                <asp:BoundField HeaderText="Pay Cash" DataField="PayCash">
                   
                </asp:BoundField>
                <asp:BoundField HeaderText="Pay Transfer" DataField="PayTransfer" >
                   
                </asp:BoundField>
                <asp:BoundField DataField="MakerID" HeaderText="MakerID" >
                
                   
                </asp:BoundField>
                
                <asp:BoundField DataField="MakingTime" HeaderText="Making Time" >
                   
                </asp:BoundField>
                

                <asp:BoundField DataField="Tran_No" HeaderText="Tran_No" Visible="False" />
            </Columns>

            <AlternatingRowStyle BackColor="#F5F4FF" />
        </asp:GridView>
        
         </div>
         
        <div id="NonTranDtls" width: 100%>
        
             <asp:GridView ID="DgvNonTranPassing" runat="server" AllowPaging="True" 
                 AutoGenerateColumns="False" DataKeyNames="Tran_No,Auth_Type" TabIndex="8" Width="100%" 
                 PageSize="5">
                 <RowStyle Height="20px" />
                 <Columns>
                     <asp:CommandField SelectText="Delete" ShowSelectButton="True">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" Width="5%" />
                     </asp:CommandField>
                     <asp:CheckBoxField DataField="Authorised">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" ForeColor="White" Width="2%" />
                     </asp:CheckBoxField>
                     <asp:BoundField DataField="Auth_Type" HeaderText="Type">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" ForeColor="White" Width="20%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Particulars" HeaderText="Particulars">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" ForeColor="White" Width="42%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="MakerID" HeaderText="MakerID">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" ForeColor="White" HorizontalAlign="Left" Width="5%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="MakingTime" HeaderText="Making Time">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                             BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                             Width="10%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Tran_No" HeaderText="Tran_No" Visible="False" />
                 </Columns>
                 <AlternatingRowStyle BackColor="#F5F4FF" />
             </asp:GridView>
        
         </div>
         
         
         <asp:Panel ID="PnlStatus" runat="server" ForeColor="#3333FF" Visible="false">
                    <table width="100%">
                        <tr>
                            <td width="100%" align="center">
                                <asp:Label ID="Label1" runat="server" Text="NO ENTRIES EXIST" Font-Bold="True" 
                                    Font-Size="X-Large" ForeColor="#3333CC"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>    
         
         
         
<div id = "PassingDiv" class="floatingDiv" style="left: 17%;">        
        <table width="100%" bgcolor="#DCEBFC" border="0" bordercolor ="#DCEBFC">
          <tr>
            <td width="3%">&nbsp;</td>
            <td width="12%"></td>
            <td width="35%"></td>
            <td width="12%"></td>
            <td width="35%"></td>
            <td width="3%">
                <img id="ImgIDClose1" alt="" src="Images/CloseIcon.png" style="width: 16px; height: 10px"  onClick="this.close" />    
            </td>
          </tr>
          <tr>
            <td></td>
            <td><label>Branch</label></td>
            <td><asp:DropDownList ID="DdlBranch" runat="server" Width="98%"> </asp:DropDownList></td>
            <td><label>Scheme</label></td>
            <td><asp:DropDownList ID="DdlScheme" runat="server" Width="98%"> </asp:DropDownList></td>
            <td></td>
          </tr>
          <tr>
            <td></td>
            <td><label>A/c Number</label></td>
            <td><asp:TextBox ID="TxtAccNO" runat="server" Width ="98%"></asp:TextBox></td>
            <td><label>Name</label></td>
            <td><asp:TextBox ID="TxtCustName" runat="server" Width ="98%"></asp:TextBox></td>
            <td></td>
          </tr>

        </table>
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
              </div>  
            </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <LINK REL=Stylesheet HREF= "CSS/MasterStyle.css" TYPE="text/css" MEDIA=screen>

</asp:Content>

