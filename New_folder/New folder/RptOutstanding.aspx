<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="RptOutstanding.aspx.vb" Inherits="SMARTCORE_UI.RptOutstanding" title="OUTSTANDING REPORT" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
        
        .my_btn{ 
          font-family:Arial; 
          font-size:10pt; 
          font-weight:normal; 
          height:30px; 
          line-height:30px; 
          width:98px; 
          border:0px;
          /*background-image:url('../Images/Save.png');*/ 
          cursor:pointer;
          background-repeat:no-repeat;
        }

        .lblcss
        {margin-left:10%;
        }
        
    </style>
    
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
      alertify.error('Invalid date format! Please enter a valid date!');
      
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
      alertify.error('Invalid date format! Please enter a valid date!');
       
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
     
  return false;  
  }   
  }   
  }   
  else  
  {   
      alertify.error("Invalid date format! Please enter a valid date!");
      
  document.form1.text1.focus();  
  return false;  
  }  
  }  

</script>
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width: 90%; float:left; margin-left:5%; margin-right:5%;margin-top:4%; ">  
    <div style="width:100% ; float:left;"> 
<legend>&nbsp;OUTSTANDING/SCHEDULE REPORTS </legend>
        </div> 
    <div style="width:46%; float:left;padding-left:4%;">
        <table style="width:100%;">
    <tr>
        <td width="25%">
             <label>Report Type</label></td>
           
        <td width="70%">
            <asp:RadioButtonList ID="RdoOutstanding" runat="server" Width ="100%" 
                RepeatDirection="Horizontal" AutoPostBack="true">
                <asp:ListItem>Outstanding</asp:ListItem>
                <asp:ListItem>Schedule</asp:ListItem>
                <asp:ListItem>Loan Type</asp:ListItem>
            </asp:RadioButtonList>
         </td>
     
             
        
    </tr>
    <tr>
        <td >
              <label>Region</label></td>
        <td >
                        <asp:DropDownList ID="DrpRegn" Width="100%" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
        </td>
    </tr>
            <tr>
                <td>
<label>Area</label></td>
                <td>
                    <asp:DropDownList ID="DrpArea"  Width="100%" runat="server" AutoPostBack="True">
                        </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <label>
                    Branch</label>
                </td>
                <td>
                    <asp:DropDownList ID="DDlBranch" runat="server" AutoPostBack="true" 
                        Width="100%">
                    </asp:DropDownList>
                </td>
            </tr>
    <tr>
        <td >
             <asp:Label ID="lblAsonDate" runat="server" Text="From Date" ForeColor="#3366FF"></asp:Label>
            </td>
        <td >
          <table width ="100%">
          <tr>
          <td width = "35%">
          <asp:TextBox ID="TxtAsOnDate" runat="server" OnChange="validatedate(this);"
                meta:resourcekey="TxtDobResource1" TabIndex="16"  width = "100%"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                Format="dd/MM/yyyy" TargetControlID="TxtAsOnDate" >
            </asp:CalendarExtender>

          </td>
          <td width = "30%">

            &nbsp;&nbsp;  <asp:Label ID="lblToDate" runat="server" Text="To Date" ForeColor="#3366FF"></asp:Label>

        </td>
          <td width = "35%">
                    <asp:TextBox ID="TxtToDate" runat="server" OnChange="validatedate(this);"
                meta:resourcekey="TxtDobResource1" TabIndex="16" width = "100%"></asp:TextBox>
                
                
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="TxtToDate">
        </asp:CalendarExtender>
          </td>
          </tr>
          </table>
                        
                        
                        </td>
    </tr>
    <tr>
        <td>
             <label> Section </label>
             </td>
        <td>
            <asp:DropDownList ID="DDlSection" runat="server" Width ="100%" 
                AutoPostBack="True">
            </asp:DropDownList>
             </td>
    </tr>
          <tr>
                 <td>

              <asp:Label ID="LblType" runat="server" Font-Size="90%" Text="Loan Type" ForeColor="#008080" ></asp:Label>

        </td>
                <td>
                    <asp:DropDownList ID="DDlType" runat="server" AutoPostBack="True" 
                        Width="100%">
                    </asp:DropDownList>
                </td>
            </tr>
    <tr>
        <td>
              <label> Scheme </label>
             </td>
        <td>
             
             <asp:DropDownList ID="DDlScheme" runat="server" Width="100%"  >
             </asp:DropDownList>
             
             </td>
    </tr>
       
    
   
</table>

        <div id="DivCon" runat="server" >
       <table width="100%">
    <tr>
        <td width="30%" >
         <label> Report Type </label>
         </td>
        <td width="70%" >
            <asp:RadioButtonList ID="RDoDetailed" runat="server" Width ="100%"  Height="12"
                RepeatDirection="Horizontal">
                <asp:ListItem>Summary</asp:ListItem>
                <asp:ListItem>Detailed</asp:ListItem>
            </asp:RadioButtonList>
            
            </td>
    </tr>
    <tr >
        <td>
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="ChkAccrued" runat="server" Text="With Interest" />
            &nbsp;
            <asp:CheckBox ID="ChkAgent" runat="server" AutoPostBack="True" 
                Text="With Agent" />
            &nbsp;
            <asp:CheckBox ID="ChkWithAddress" runat="server" AutoPostBack="True" 
                Text="With Address" />
        </td>
    </tr>
    <tr>
        <td >
            <label> Agent </label>
            </td>
        <td >
        <asp:DropDownList ID="DDlAgent" runat="server" Width ="100%" Enabled="False">
            </asp:DropDownList>
            </td>
    </tr>
           <tr id="RptType" runat="server">
        <td>
            <label>Report Option</label>
        </td>
        <td>
            <asp:DropDownList ID="CmbRptWith" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
        
        
    </tr>
           </table>
    </div>
        <div  style="width:100%; float:left;" >
            <table width="100%">
                 <tr>
        
        <td width="40%">
           </td>
        <td width="20%">
           
            <asp:ImageButton ID="BtnPro" runat="server" ImageUrl="~/Images/ProcessBtn.png" />
        </td>
        <td>
        <table  width="100%">
        <tr>
        <td>
          
            <asp:ImageButton ID="Btn_Show" runat="server" ImageUrl="~/Images/Show.png" Width="98px" />
            </td>
        <td>
              
            <asp:ImageButton ID="Btn_Close" runat="server" ImageUrl="~/Images/Close.png" />
        </td>
        </tr>
        </table>
        </td>
        
    </tr>
    <tr >
        <td colspan="4">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                
                    <div id="progress">
                        <img alt="progress" src="Images/progress.gif" /> Please Wait...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
            </table>
        </div>

    </div>
    <div runat="server" style="width:41%;  float:left; padding-left:7%;"  id="DivShare">
        <table style="width: 100%;">
                    <tr>
                        
                        <td width="45%" style="margin-left: 40px">
                            <label> Individual/Institution </label>  
                        </td>
                        <td width="45%">
                            <asp:DropDownList ID="ddl_IndivInst" runat="server" Width ="100%"></asp:DropDownList> 
                        </td>    
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label>Gender</label>
                        </td>
                        <td width="45%">
                           <asp:DropDownList ID="ddl_Gender" runat="server" Width ="100%"></asp:DropDownList> 
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label> Religion </label>
                        </td>
                        <td width="45%">
                            <asp:DropDownList ID="ddl_Religion" runat="server" Width ="100%" 
                                AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label> Caste </label> 
                        </td>
                        <td width="45%">
                            <asp:DropDownList ID="ddl_Caste" runat="server" Width ="100%"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>

                        <td style="margin-left: 40px" width="45%">
                            <label> Balance From </label> 
                        </td>
                        <td width="45%">
                            <asp:TextBox ID="txt_BalAmtFr" runat="server" Width ="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label> Balance To </label> 
                        </td>
                        <td width="45%">
                            <asp:TextBox ID="txt_BalAmtTo" runat="server" Width ="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label> Admission Date From </label>
                        </td>
                        <td width="45%">
                            <asp:TextBox ID ="Ce_AddmFrDate" runat ="server" Width ="98%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                                Format="dd/MM/yyyy" TargetControlID="Ce_AddmFrDate" >
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label> Admission Date To </label>  
                        </td>
                        <td width="45%">
                           <asp:TextBox ID ="Ce_AddmToDate" runat ="server" Width ="98%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" 
                                Format="dd/MM/yyyy" TargetControlID="Ce_AddmToDate" >
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label> Occupation </label>
                        </td>
                        <td width="45%">
                           <asp:DropDownList  ID ="ddl_Occupation" runat ="server" Width ="100%"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label> Amsom </label>  
                        </td>
                        <td width="45%">
                            <asp:DropDownList  ID ="ddl_Amsom" runat ="server" Width ="100%"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label> Desom </label>
                        </td>
                        <td width="45%">
                            <asp:DropDownList  ID ="ddl_desom" runat ="server" Width ="100%"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px" width="45%">
                            <label>
                            Sangam</label></td>
                        <td width="45%">
                            <asp:DropDownList ID="ddl_Sangam" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
    </div>

</div>
    </ContentTemplate> 
    </asp:UpdatePanel> 
    
    
</asp:Content>


