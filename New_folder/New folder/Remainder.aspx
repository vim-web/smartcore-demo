<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Remainder.aspx.vb" Inherits="SMARTCORE_UI.Remainder" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<script type='text/javascript'>

    function ValidateSave() {

        var Ctl = document.getElementById('<%=CmbRemndrTo.ClientID%>');
        var RemType = Ctl.options[Ctl.selectedIndex].text.toUpperCase();

        if (RemType  == 'ACCOUNT') {
            var Ctl = document.getElementById('<%=TxtAccNo.ClientID%>');
            if (Ctl.value == "") {
                alert('Account No Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
        }



        if (RemType == 'USER') {
            var Ctl = document.getElementById('<%=TxtUserId.ClientID%>');
            if (Ctl.value == "") {
                alert('User Details Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
        }

        if (RemType == 'CUSTOMER') {
            var Ctl = document.getElementById('<%=TxtCid.ClientID%>');
            if (Ctl.value == "") {
                alert('Customer Details Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
        }




        var Ctl = document.getElementById('<%=TxtSubject.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Subject');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtDetails.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Details');
            Ctl.focus();
            return false;
        }


        return true;
    }

</script>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  
<script type="text/jscript">
function enableDisableDiv(value, type,divId)
{
if (type == "select")
{
document.getElementById(divId).style.visibility = "visible";
}
</script>  
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:25%; margin-right:20%;">
     
     <table width="100%">
           <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                &nbsp;</td>
            <td width="48%">
                <asp:HiddenField ID="HdnCustID" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
               </td>
            <td width="2%">
                &nbsp;</td>
            </tr>
           <tr>
               <td width="2%">
               </td>
               <td width="48%">
                   <label>
                   Remainder Type</label></td>
               <td width="48%">
                   <asp:DropDownList ID="CmbRemndrTo" runat="server" AutoPostBack="True" 
                       Width="100%" Enabled="True">
                       <asp:ListItem>Account</asp:ListItem>
                       <asp:ListItem>User</asp:ListItem>
                       <asp:ListItem>Customer</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td width="2%">
               </td>
           </tr>
     </table>
 <div id="DivCustomer" runat ="server" visible =false  >    
          <table width="100%">
           <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                CustomerId</label></td>
            <td width="48%">
            <asp:TextBox ID="TxtCid" Width="100%" runat="server" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width="2%">
                <asp:ImageButton ID="ImgSrch" runat="server" Height="14px" 
                    ImageUrl="~/images/Search.png" Width="14px" />
            </td>
              </tr>
              
                         <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Name</label></td>
            <td width="48%">
           <asp:TextBox ID="TxtCustName" Width="100%" runat="server"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
                                       <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Address</label></td>
            <td width="48%">
           <asp:TextBox ID="TxtCustAddress" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
     </table>
   </div>  
   
   
    <div id="DivAccountNo" runat ="server" visible =false  >    
          <table width="100%">
           <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Branch Name</label></td>
            <td width="48%">
            
            <table width ="100%">
            <tr>
            <td width ="30%">
            <asp:TextBox ID="TxtBranch" Width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
            
            
            </td>
            <td width ="70%">
                <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True">
             </asp:DropDownList>
            </td>
            </tr>
            </table>
            
            
            
                </td>
            <td width="2%">
            </td>
              </tr>
              
                         <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Scheam Name</label></td>
            <td width="48%">
     <table width ="100%">
            <tr>
            <td width ="30%">
            <asp:TextBox ID="TxtScheme" Width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
            
            
            </td>
            <td width ="70%">
                <asp:DropDownList ID="CmbScheme" Width="100%" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            </tr>
            </table>
                
             </td>
            <td width="2%">
            </td>
              </tr>
                                       <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Account Number</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAccNo" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
                                       <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Name</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAcHoldrName" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
                                       <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Address</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAcHoldrAddr" runat="server" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
              <tr>
                  <td width="2%">
                      </td>
                  <td width="48%">
                      </td>
                  <td width="48%">
                      </td>
                  <td width="2%">
                      </td>
              </tr>
     </table>
   </div>  
   
   
   
    <div id="DivUser" runat ="server" visible =false  >    
          <table width="100%">
           <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                User Name</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbUser" runat="server" AutoPostBack="True" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
              </tr>
              
                         <tr>
                             <td width="2%">
                             </td>
                             <td width="48%">
                                 <label>
                                 User CustomerID</label></td>
                             <td width="48%">
                                 <asp:TextBox ID="TxtUserId" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
                             </td>
                             <td width="2%">
                             </td>
              </tr>
              
                         <tr>
            <td width="2%">
            </td>
            <td width="48%">
            <label> User Name</label></td>
            <td width="48%">
           <asp:TextBox ID="TxtUserName" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
                                                     <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Address</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtUserAddress" runat="server" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
              </tr>
                                       <tr>
            <td width="2%">
            </td>
            <td width="48%">
                </td>
            <td width="48%">
                </td>
            <td width="2%">
            </td>
              </tr>
     </table>
   </div>  
   
    <div id="DivDetails" runat ="server"  >    
      <table width="100%">
      <tr>
            <td width="2%">
            </td>
      <td width="20%">
       <label> Subject</label>
        
      </td>
      
      <td width="76%">
      
          <asp:TextBox ID="TxtSubject" runat="server" Width="100%"></asp:TextBox>
      
      </td>
      

         
             <td width="2%">
            </td>   
            
                  </tr> 
   <tr>
            <td width="2%">
            </td>
      <td width="20%">
          <label>
          Details</label></td>
      
      <td width="76%">
          <asp:TextBox ID="TxtDetails" runat="server" Width="100%"></asp:TextBox>
      </td>
      

         
             <td width="2%">
            </td>   
            
                  </tr> 
     </table>
     
     <table width ="100%">
     <tr>
     <td align="center" >
    &nbsp;</td>
     </tr>
     
         <tr>
             <td align="center">
                 <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();" />
                 &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                 &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
             </td>
         </tr>
     
     </table>
     
     
   </div>  
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
<%--    
<tr>
<td colspan="4"><legend>Remainder</legend></td>
<td></td>
</tr>
        <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Remainder Type</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbRemndrTo" runat="server" Width="100%">
                    <asp:ListItem>Account</asp:ListItem>
                    <asp:ListItem>User</asp:ListItem>
                    <asp:ListItem>Customer</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="2%">
            </td>
        </tr>
<tr>
<td></td>
<td colspan="2">
<div id="Customer">
<table width="100%">
<tr>
<td width="48%">
<label>CustomerId</label></td>
<td width="48%">
    <asp:TextBox ID="TxtCid" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>
<tr>
<td width="48%">
    <label>Name</label>&nbsp;</td>
<td width="48%">
    <asp:TextBox ID="TxtCustName" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>
<tr>
<td width="48%">
    <label>Address</label>&nbsp;</td>
<td width="48%">
    <asp:TextBox ID="TxtCustAddress" runat="server" Width="100%" 
        TextMode="MultiLine"></asp:TextBox>
    </td>
</tr>
</table>
</div>
</td>
<td></td>
</tr>
<tr>
<td>&nbsp;</td>
<td colspan="2">
<div>
<table width="100%">
<tr>
<td width="48%">
<label>User Id</label></td>
<td width="48%">
    <asp:TextBox ID="TxtUserId" runat="server" Width="100%"></asp:TextBox>
    </td>
</tr>
<tr>
<td width="48%">
    <label> User Name</label>&nbsp;</td>
<td width="48%">
    <asp:TextBox ID="TxtUserName" runat="server" Width="100%"></asp:TextBox>
    </td>
</tr>
</table>
</div>
</td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td colspan="2">
<div>
<table width="100%">
<tr>
<td width="48%">
<label>Branch</label></td>
<td width="48%">
    <table width="100%">
    <tr>
    <td width="30%">
    <asp:TextBox ID="TxtBranch" Width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
    </td>
    <td width="70%">
    <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    </td>
    </tr>
    </table>
    </td>
</tr>
<tr>
<td width="48%">
    <label>Scheme</label>&nbsp;</td>
<td width="48%">
    <table width="100%">
    <tr>
    <td width="30%">
    <asp:TextBox ID="TxtScheme" Width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
    </td>
    <td width="70%">
    <asp:DropDownList ID="CmbScheme" Width="100%" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    </td>
    </tr>
    </table> 
    </td>
</tr>
<tr>
<td width="48%">
    <label>Account No</label>&nbsp;</td>
<td width="48%">
    <asp:TextBox ID="TxtAccNo" runat="server" Width="100%"></asp:TextBox>
    </td>
</tr>
<tr>
<td width="48%">
    <label>Name</label>&nbsp;</td>
<td width="48%">
    <asp:TextBox ID="TxtAcHoldrName" runat="server" Width="100%"></asp:TextBox>
    </td>
</tr>
<tr>
<td width="48%" class="style1">
    <label>Address</label>&nbsp;</td>
<td width="48%" class="style1">
    <asp:TextBox ID="TxtAcHoldrAddr" runat="server" Width="100%" 
        TextMode="MultiLine"></asp:TextBox>
    </td>
</tr>
</table>
</div>
</tr>
<tr>
<td>&nbsp;</td>
<td>
<label>Subject</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtSubject" runat="server" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
<label>Details</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtDetails" runat="server" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="center" colspan="2">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
    <asp:ImageButton ID="CmdClear" runat="server" 
        ImageUrl="~/Images/Clear.png" />
    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
    </td>
<td>&nbsp;</td>
</tr>
</table>--%>
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

