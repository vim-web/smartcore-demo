<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanLbrReport.aspx.vb" Inherits="SMARTCORE_UI.LoanLbrReport" MasterPageFile="~/SMARTCORE.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
        .style1
        {
            height: 33px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>Loan LBR Report</legend>

    <table width="100%">
        <tr>
            <td width="6%"> </td>
            <td width="18%"> </td>
            <td width="26%"> </td>
            <td width="18%"> </td>
            <td width="26%"> </td>
            <td width="6%"> </td>
        </tr>
        
        <tr>
            <td > </td>
            <td ><label>From Date</label> </td>
            <td > 
                <asp:TextBox ID="TxtDtFrom" runat="server" Width="95%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtDtFrom"> </asp:CalendarExtender>      
            </td>
            <td ><label>To Date</label> </td>
            <td > 
                <asp:TextBox ID="TxtDtTo" runat="server" Width="95%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtDtTo"> </asp:CalendarExtender>      
            </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td > 
                <asp:RadioButton ID="Opt_Detailed" runat="server" Checked="True" 
                    GroupName="Main" Text="Detailed" Visible="False" />
            </td>
            <td > 
                <asp:RadioButton ID="Opt_SchemeWise" runat="server" GroupName="Main" 
                    Text="SchemeWise" Visible="False" />
            </td>
            <td > 
                <asp:RadioButton ID="Opt_DateWise" runat="server" GroupName="Main" 
                    Text="DateWise" Visible="False" />
            </td>
            <td > 
                <asp:RadioButton ID="Opt_ActivityWise" runat="server" GroupName="Main"  Visible="False"
                    Text="Activity Wise" />
            </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td > </td>
            <td colspan="2" > 
                <asp:DropDownList ID="Cmb_Wise" runat="server" AutoPostBack="True" Width="50%">
                </asp:DropDownList>
            </td>
            <td > </td>
            <td> </td>
        </tr>
        </table>
        <table width="100%">
        <tr>
            <td width="20%" class="style1"> 
                
            </td>
            <td  width="20%" class="style1"> </td>
             <td align="center" colspan="3" class="style1">
                  <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
              </td>
            <td width="20%" class="style1"> </td>
        </tr>
           <tr>
            <td width="6%" class="style2"> </td>
            <td width="18%" class="style2"> </td>
            <td width="26%" class="style2"> 
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
            <td width="18%" class="style2"> &nbsp;</td>
            <td width="26%" class="style2"> </td>
            <td width="6%" class="style2"> </td>
        </tr>
    </table>
            </div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
      