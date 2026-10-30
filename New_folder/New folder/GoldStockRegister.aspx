<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="GoldStockRegister.aspx.vb" Inherits="SMARTCORE_UI.GoldStockRegister" 
    title="Gold Stock Register" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>
     
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
  <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>Gold Stock Register</legend>  
    
    
     <table width="100%">
          <tr>
        <td width="1%"> </td>
        <td width="15%"> </td>
        <td width="2%"> </td>
        <td width="10%">
             </td>
        <td width="2%"> </td>
        <td width="15%"> </td>
        <td width="30%"> </td>
        <td width="2%"> </td>
      </tr>
      
      <tr>
      <td></td>
      <td> </td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
      </tr>
      
      <tr>
      <td></td>
      <td>
          
          </td>
      <td></td>
      <td>
          <asp:Label ID="Lbl_Branch" runat="server" Font-Names="Tahoma" 
              Font-Size="Medium" ForeColor="#6666FF" Text="Branch"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:DropDownList ID="Ddl_Branch" runat="server" >
          </asp:DropDownList>
          </td>
      <td></td>
      </tr>
      
      <tr>
      <td></td>
      <td>

          </td>
      <td></td>
      <td>
          <asp:Label ID="Lbl_Scheme" runat="server" Font-Names="Tahoma" 
              Font-Size="Medium" ForeColor="#6666FF" Text="Scheme"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:DropDownList ID="Ddl_Scheme" runat="server" >
          </asp:DropDownList>
          </td>
      <td></td>
      </tr>
      
      <tr>
      <td></td>
      <td>
           </td>
      <td></td>
      <td>
          <asp:Label ID="Lbl_FromDate" runat="server" Font-Names="Tahoma" 
              Font-Size="Medium" ForeColor="#6666FF" Text="From Date"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:TextBox ID="Txt_ToDate" runat="server" BackColor="White" 
              BorderColor="#3333CC" BorderStyle="Solid" BorderWidth="2px" TabIndex="4"></asp:TextBox>
          <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
              TargetControlID="Txt_ToDate">
          </asp:CalendarExtender>
          <asp:Image ID="ImgDoa" runat="server" BorderStyle="Outset" Height="24px" 
              ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="17px" />
          </td>
      <td></td>
      </tr>
      
      <tr>
      <td></td>
      <td>
           </td>
      <td></td>
      <td>
          <asp:Label ID="Lbl_ToDate" runat="server" Font-Names="Tahoma" 
              Font-Size="Medium" ForeColor="#6666FF" Text="To Date"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:TextBox ID="Txt_FromDate0" runat="server" BackColor="White" 
              BorderColor="#3333CC" BorderStyle="Solid" BorderWidth="2px" TabIndex="4"></asp:TextBox>
          <asp:CalendarExtender ID="Txt_FromDate0_CalendarExtender" runat="server" 
              Format="dd/MM/yyyy" TargetControlID="Txt_FromDate0">
          </asp:CalendarExtender>
          <asp:Image ID="ImgDoa0" runat="server" BorderStyle="Outset" Height="24px" 
              ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="17px" />
          </td>
      <td></td>
      </tr>
      
      <tr>
      <td></td>
      <td></td>
      <td></td>
      <td>
         <asp:Label ID="Label1" runat="server" Font-Names="Tahoma" 
              Font-Size="Medium" ForeColor="#6666FF" Text="Report Type"></asp:Label>
      </td>
      <td></td>
      <td>
      <table   width ="100%">
      <tr>
      <td> 
      
      <asp:RadioButton ID="Opt_Detailed" runat="server" Font-Bold="True" 
              Font-Names="Tahoma" Font-Size="Medium" Text="Detailed" 
              AutoPostBack="True" GroupName="RptType" Width ="100%" />
      
      </td>
      <td>           <asp:RadioButton ID="Opt_Summary" runat="server" Font-Bold="True" 
              Font-Names="Tahoma" Font-Size="Medium" Text="Summary" 
              AutoPostBack="True" GroupName="RptType"  Width ="100%"  /></td>
      </tr>
      
      </table>
      
      </td>
      <td></td>
      </tr>
        <tr>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
      </tr>
       
      
     </table>
     
     <table width="100%">
     
         <tr>
             <td width="25%">
                  </td>
                  <td width="25%">
                  </td>
                  <td width="25%">
                  <asp:ImageButton ID="ImageButton1" runat="server" Height="32px" 
                          ImageUrl="~/Images/Show.png" Width="96px"  />
                     <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" 
                          ImageUrl="~/Images/CloseBtn.png" Width="96px"  />
            
 
            
             </td>
                    <td width="25%">
                  </td>
           
         </tr>
     
         <tr>
        <td width="1%"> </td>
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
        <td width="2%"> </td>
        <td width="10%">
             </td>
        <td width="2%"> </td>
        <td width="15%"> </td>
        <td width="30%"> </td>
        <td width="2%"> </td>
      </tr>
  
     </table>
 
 
 
 
<br />
<br />
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
