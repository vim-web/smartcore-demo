<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IncentiveCanvasserDetails_Rpt.aspx.vb" Inherits="SMARTCORE_UI.IncentiveCanvasserDetails_Rpt" MasterPageFile="~/SMARTCORE.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>   
 <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                

     <table width="100%">
        <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <legend>Canvassed By Reports</legend></td>
            <td width="48%"> 
                &nbsp;</td>
            <td width="2%">
            </td>
        </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Canvasser Id</label></td>
             <td width="45%">
                  <asp:TextBox ID="Txt_Canvasserid" runat="server" Width="70%" AutoPostBack="True"></asp:TextBox>
                  <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                    ImageUrl="~/images/Search.png" Width="23px" />
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
      <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Canvasser Name</label></td>
             <td width="45%">
                  <asp:TextBox ID="Txt_CanvasserName" runat="server" Width="70%"></asp:TextBox>
                 
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
      
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 From Date</label></td>
             <td width="45%">
                 <asp:TextBox ID="TxtFromDate" runat="server" Width="70%" OnChange="validatedate(this);"></asp:TextBox>
                 <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" Format="dd/MM/yyyy"  runat="server" 
                     TargetControlID="TxtFromDate">
                 </asp:CalendarExtender>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 To Date</label></td>
             <td width="45%">
                 <asp:TextBox ID="TxtTodate" runat="server" Width="70%" OnChange="validatedate(this);"></asp:TextBox>
                 <asp:CalendarExtender ID="TxtTodate_CalendarExtender" Format="dd/MM/yyyy"  runat="server" 
                     TargetControlID="TxtTodate">
                 </asp:CalendarExtender>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td align="center" colspan="2" style="width: 96%" width="48%">
                 <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                 <asp:ImageButton ID="CmdClose" runat="server" 
                     ImageUrl="~/Images/CloseBtn.png" />
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         
           <tr>
            <td width="2%">
            </td>
            <td width="48%">
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
            <td width="48%"> 
                &nbsp;</td>
            <td width="2%">
            </td>
        </tr>
        </table>
     </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
  <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  

</asp:Content>


