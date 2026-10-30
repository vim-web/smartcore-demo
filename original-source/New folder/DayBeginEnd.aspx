<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DayBeginEnd.aspx.vb" Inherits="SMARTCORE_UI.DayBeginEnd" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


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
   
     
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            height: 28px;
        }
    </style>
   
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>

<div class="gradientBoxesWithOuterShadows" style ="width: 80%; margin-left:10%;margin-right:10%;margin-top:10%;font-family: Tahoma" >   
        <legend>Day End Process</legend>             
    <table width="100%">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="20%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
       <td width="2%">&nbsp;</td>
        <td width="10%">&nbsp;</td>
         <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
         <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>
          <tr>
           
           <td class="style2"></td>
           <td class="style2">
               &nbsp;</td>
           <td class="style2"></td>
           <td class="style2">
               <asp:Label ID="Lbl_TranDate" runat="server" Text="Transaction Date" 
                  CssClass="lblCSS"  ></asp:Label>
            
              </td>
              <td class="style2"></td>
           <td class="style2" >
               <asp:TextBox ID="Txt_TransactionDate" runat="server" Width="177px" OnChange="validatedate(this);"></asp:TextBox>             
           </td>
           <td class="style2"></td>
            <td class="style2">
                &nbsp;</td>
           <td class="style2"></td>  <td class="style2">
              &nbsp;</td> <td class="style2"></td>
              
               
          
          </tr> 
           <tr>
           <td></td><td></td><td></td><td>
               <asp:Label ID="Lbl_NextTranDate" runat="server" Text="Next Transaction Date" 
                  CssClass="lblCSS"  ></asp:Label>
               </td><td></td><td>
               <asp:TextBox ID="Txt_NextTranDate" runat="server" Width="177px" OnChange="validatedate(this);"></asp:TextBox>
               
               </td><td></td><td></td><td></td><td>
               <asp:ImageButton ID="Img_Show" runat="server" Height="16px" 
                   ImageUrl="~/Images/Show.png" Width="78px" Visible="False" />
               </td><td></td>
           
          </tr> 
          
            <tr>
           <td></td><td></td><td></td><td>
             
               </td><td></td><td>
               
               
               </td><td></td><td></td><td></td><td>
              
               </td><td></td>
           
          </tr> 
          <tr>
           <td></td><td></td><td></td>
               <td colspan ="3" align="center">
                   <asp:ImageButton ID="Cmd_Next" runat="server" 
                       ImageUrl="~/Images/Dtls.png" />
                   <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/CloseBtn.png" 
                       TabIndex="28" />
              </td>
               <td></td>
               <td></td><td></td><td>
               
               </td><td></td>
           
          </tr> 
        </table>
        
        <table width="100%">
        <tr> 
        <td width="2%">&nbsp;</td>
        <td class="style1">
            <asp:GridView ID="Dgv_DayEndProcess" runat="server" Width="100%" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="2px" 
                CellPadding="4" AutoGenerateColumns="False">
                <RowStyle BackColor="White" ForeColor="#003399" />
                <Columns>
                    <asp:BoundField DataField="DayEndProcess" HeaderText="DayEndProcess" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Data" HeaderText="Values" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status">
                        <ItemStyle Font-Bold="False" Font-Names="Times New Roman" ForeColor="#009999" 
                            HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProcStatus" HeaderText="ProcessingStatus" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <EditRowStyle BorderStyle="Solid" />
            </asp:GridView>
            </td>
        
         <td width="2%">&nbsp;</td>
        </tr>
        
        </table>
        
           <tr>
        <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
      
      </tr>
      <table width ="100%">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
       <td width="2%">&nbsp;</td>
       <td width="15%">&nbsp;</td>
       <td>
     
                
         <td width="2%">&nbsp;</td>
        
         <td width="2%">&nbsp;</td>
         <td>
             &nbsp;</td>
         <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="15%">
                  <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                      AssociatedUpdatePanelID="UpdatePanel1">
                      <ProgressTemplate>
                          <div ID="blur">
                          </div>
                          <div ID="progress">
                              <img alt="progress" src="Images/progress.gif" /> Please Wait...
                          </div>
                      </ProgressTemplate>
                  </asp:UpdateProgress>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="15%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
              <td width="15%" align="center">
                  <asp:ImageButton ID="Img_Post" runat="server" 
                      ImageUrl="~/Images/DAYEND.png" />
              </td>
              <td>
                  <asp:Button ID="BtnCashDenomination" runat="server" BackColor="#99CCFF" 
                      Font-Bold="True" Font-Italic="False" ForeColor="#003366" Text="Denomination" />
                  <td width="2%">
                      &nbsp;</td>
                  <td width="2%">
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
                  <td width="2%">
                      &nbsp;</td>
                  <td width="15%">
                      &nbsp;</td>
                  <td width="2%">
                      &nbsp;</td>
              </td>
          </tr>
    </table>
         <table width="100%" style="height: 161px">
        <tr> 
        <td width="2%">&nbsp;</td>
        <td width="96%">
            <asp:GridView ID="Dgv_DayEndPosting" runat="server" Width="100%" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="2px" 
                CellPadding="4" AutoGenerateColumns="False">
                <RowStyle BackColor="White" ForeColor="#003399" />
                <Columns>
                    <asp:BoundField DataField="DayEndProcess" HeaderText="Posting/Resetting" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Status" HeaderText="Status">
                        <ItemStyle Font-Bold="False" Font-Names="Times New Roman" ForeColor="#FF0066" 
                            HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProcStatus" HeaderText="PostingStatus" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <EditRowStyle BorderStyle="Solid" />
            </asp:GridView>
            
            </td>
      <td width="2%">&nbsp;</td>
            
      </table >
      
      
      <tr>
        <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
      
      </tr>
     </table>
    
    </div>
  </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

