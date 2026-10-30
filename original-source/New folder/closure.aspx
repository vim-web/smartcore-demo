<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="closure.aspx.vb" Inherits="SMARTCORE_UI.closure" 
    title=" Closure " %>
        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   <script language="javascript" type="text/javascript">
     
  function validatedate(inputText)  
  {  
  var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;  
  // Match the date format through regular expression  
  if(inputText.value.match(dateformat))  
  {  
//  document.form1.text1.focus();  
  //Test which seperator is used '/' or '-'  
  var opera1 = inputText.value.split('/');  
  var opera2 = inputText.value.split('-');  
  lopera1 = opera1.length;  
  lopera2 = opera2.length;  
  // Extract the string into month, date and year  
  if (lopera1>1)  
  {  
  var pdate = inputText.value.split('/');  
  }  
  else if (lopera2>1)  
  {  
  var pdate = inputText.value.split('-');  
  }  
  var dd = parseInt(pdate[0]);  
  var mm  = parseInt(pdate[1]);  
  var yy = parseInt(pdate[2]);  
  // Create list of days of a month [assume there is no leap year by default]  
  var ListofDays = [31,28,31,30,31,30,31,31,30,31,30,31];  
  if (mm==1 || mm>2)  
  {  
  if (dd>ListofDays[mm-1])  
  {
      alert('Invalid date format! Please enter a valid date!');
      
  return false;  
  }  
  }  
  if (mm==2)  
  {  
  var lyear = false;  
  if ( (!(yy % 4) && yy % 100) || !(yy % 400))   
  {  
  lyear = true;  
  }  
  if ((lyear==false) && (dd>=29))  
  {
      alert('Invalid date format! Please enter a valid date!');
      
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
      alert('Invalid date format! Please enter a valid date!');
      
  return false;  
  }   
  }   
  }   
  else  
  {   
      alert("Invalid date format! Please enter a valid date!");
      
  document.form1.text1.focus();  
  return false;  
  }  
  }  

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>Share Closure Request</legend>
     <table width="100%">
          <tr>
          <td width="2%"></td>
        <td width="23%">&nbsp;</td>
        <td width="23%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="23%">&nbsp;</td>
        <td width="23%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>
      
      <tr>
      <td>
          &nbsp;</td>
      <td><label>Branch</label></td>
      <td>
          <asp:DropDownList ID="Ddl_Branch" runat="server" Width="100%" 
              TabIndex="1" >
          </asp:DropDownList>
          </td>
      <td></td>
      <td>
          &nbsp;</td>
      <td>
          &nbsp;</td>
      <td>
          &nbsp;</td>
      </tr>
      
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Scheme</label></td>
              <td>
                  <asp:DropDownList ID="Ddl_Scheme" runat="server" TabIndex="2" Width="100%">
                  </asp:DropDownList>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:HiddenField ID="HiddenAccid" runat="server" />
              </td>
              <td>
                  &nbsp;</td>
          </tr>
      
         <tr>
 <td>
     &nbsp;</td>
 <td>&nbsp;</td>
 <td>
     &nbsp;</td>
 
 <td></td>
 <td>
     &nbsp;</td>
 <td>
     &nbsp;</td>
 <td>
     &nbsp;</td>
 </tr>
      
      
     </table> 
 
    <table width="100%">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Account No</label></td>
            <td>
                <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" 
                    CssClass="twitterStyleTextbox" TabIndex="5" Width="20%"></asp:TextBox>
                <asp:TextBox ID="Txt_AccNo" runat="server" AutoPostBack="True" TabIndex="5" 
                    Width="40%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Customer Id</label></td>
            <td>
                <asp:TextBox ID="Txt_CustId" runat="server" Width="65%" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Customer Name</label></td>
            <td colspan="4">
                <asp:TextBox ID="Txt_CustName" runat="server" Height="26px" TabIndex="7" 
                    Width="100%" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Customer Address</label></td>
            <td colspan="4">
                <asp:TextBox ID="Txt_CustAddress" runat="server" TabIndex="8" 
                    TextMode="MultiLine" Width="100%" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Balance</label></td>
            <td colspan="4">
                <asp:TextBox ID="Txt_balance" runat="server" Width="32%" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Last Tran Date</label></td>
            <td>
                <asp:TextBox ID="Txt_LasttranDate" runat="server" ReadOnly="True" Width="44%"></asp:TextBox>
                <asp:Image ID="ImgDoa" runat="server" BorderStyle="Outset" Height="24px" 
                    ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="10%" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center" colspan="4">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
                    TabIndex="35" />
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" TabIndex="37" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="progress">
                            &nbsp;Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
 
   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
      

