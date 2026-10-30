<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="GDSDetails.aspx.vb" Inherits="SMARTCORE_UI.GDSDetails" %>
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
    ggggg<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>GDS Particulars</legend>  
        <table width="100%">
    <tr>
    <td width="2%"></td>
    <td width="24%" rowspan="5">
    <asp:Panel ID="Panel2" runat="server" BackColor="#E6F2FF" ForeColor="White">
                <asp:RadioButton ID="Opt_GdsNo" runat="server" AutoPostBack="True" 
                    Checked="True" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" 
                    ForeColor="White" GroupName="RptType" Text="No Of GDS" />
                <br />
                <asp:RadioButton ID="Opt_GdsDetais" runat="server" AutoPostBack="True" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="White" 
                    GroupName="RptType" Text="GDS Details" />
                <br />
                <asp:RadioButton ID="Opt_GdsDuedet" runat="server" AutoPostBack="True" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="White" 
                    GroupName="RptType" Text="GDS Due Details" />
                <br />
                <br />
                 </asp:Panel>
    </td>
    <td width="24%"></td>
    <td width="24%"></td>
    <td width="24%"></td>
    <td width="2%"></td>
    </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_Branch" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="Branch"></asp:Label>
            </td>
            <td width="24%">
                <asp:DropDownList ID="Cmb_Branch" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_Date" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                    ForeColor="#6666FF" Text="From Date"></asp:Label>
            </td>
            <td width="24%">
                <asp:TextBox ID="Txt_FrDate" runat="server" BackColor="White" OnChange="validatedate(this);"
                    CssClass="twitterStyleTextbox" TabIndex="4" Width="97%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="Txt_FrDate">
                </asp:CalendarExtender>
            </td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_DateTo" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="To Date"></asp:Label>
            </td>
            <td width="24%">
                <asp:TextBox ID="Txt_ToDate" runat="server" BackColor="White" OnChange="validatedate(this);"
                    CssClass="twitterStyleTextbox" TabIndex="4" Width="97%"></asp:TextBox>
                <asp:CalendarExtender ID="Txt_ToDate_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="Txt_ToDate">
                </asp:CalendarExtender>
            </td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td colspan="4">
                &nbsp;
                <table width="100%">
     
         <tr>
             <td width="40%">
                     <asp:Panel ID="PnlPage" runat="server" Visible="false">
                            <asp:Label ID="Lbl_AccHead0" runat="server" Font-Names="Tahoma" 
                                Font-Size="Medium" ForeColor="#6666FF" Text="Start Page" Visible="False"></asp:Label>
                            <asp:TextBox ID="Txt_DateFrom0" runat="server" BackColor="White" 
                                CssClass="twitterStyleTextbox" TabIndex="4" Width="20%"></asp:TextBox>
                        </asp:Panel>
                 &nbsp;</td>
            <td width="40%">
                      <asp:ImageButton ID="Cmd_Show" runat="server" Height="32px" 
                          ImageUrl="~/Images/Show.png" Width="96px" />
                      <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" 
                          ImageUrl="~/images/Close.png" Width="96px" />
             </td>
               
                    <td width="20%">
                 
                     
                
                 
                     
             </td>
           
         </tr>
     
    
  
     </table>
                </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
    </table>

  
</div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
