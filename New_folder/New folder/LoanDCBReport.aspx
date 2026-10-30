<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanDCBReport.aspx.vb" Inherits="SMARTCORE_UI.LoanDCBReport" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  
<style type="text/css">
.TextColor {
	color: #03F;
	font-weight: bold;
	font-size:small;
}
.TextColorNormal {
	color: #03F;
	font-weight:normal;
}
.TextColorRed{
	color:Red;
	font-weight: bold;
}

.ButtonStyle {
	color: #FFF;
	background-color: #039;
	text-align: center;
	display: inline;
        }
.CalendarCSS  
{  
    background-color:#3366CC;  
    color:Snow;  
}  

</style>
   <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    <legend>Loan Demand Collection Balance Report</legend>   
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
            <td > 
                <label>Branch</label>
             </td>
            <td > 
                <asp:DropDownList ID="CmbBranch" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > 
                <label>Scheme</label>
            </td>
            <td > 
                <asp:DropDownList ID="CmbScheme" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > </td>
        </tr>
        
         <tr>
            <td > </td>
            <td > 
                <label>From Date</label>
             </td>
            <td > 
                <asp:TextBox ID="TxtFrDate" runat="server"  Width = "98%" TabIndex="2"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtFrDate"> </asp:CalendarExtender>      
             </td>
            <td > 
                <label>To Date</label>
            </td>
            <td > 
                <asp:TextBox ID="TxtToDate" runat="server"  Width = "98%" TabIndex="2"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtToDate"> </asp:CalendarExtender>  
             </td>
            <td > </td>
        </tr>
        <tr>
            <td  > </td>
            <td  > &nbsp;</td>
            <td  >  
           <asp:RadioButton ID="rbtnDetailed" runat="server" Text="Detailed" 
                    Checked="True" AutoPostBack="True"  /><td  > 
                    &nbsp;</td>
            <td  > 
                <asp:RadioButton ID="rbtnSummary" runat="server" Text="Summary" 
                    AutoPostBack="True" Visible="False"  /></td>
            <td > </td>
        </tr>
        
        <tr>
            </td>
            <td > </td>
            <td> 
                &nbsp;</td>
            <td > 
                  &nbsp;</td>
            <td > </td>
            <td > </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td > </td>
            <td colspan="2" > 
                <asp:ImageButton ID="ImgProcess" runat="server" Height="25px" 
                    ImageUrl="~/Images/ProcessBtn.png" />
                &nbsp;
                <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                    ImageUrl="~/Images/Show.png" />
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" />
            </td>
            <td > </td>
            <td> </td>
        </tr>
        </table>
        <table width="100%">
        <tr>
            <td width="20%"> 
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                    EnablePageMethods="true">
                </asp:ToolkitScriptManager>
            </td>
            <td  width="20%"> <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="blur">
                        </div>
                        <div ID="progress">
                            <img alt="progress" src="Images/progress.gif" /> Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress> </td>
            <td  colspan="3">  
                &nbsp;</td>
            <td width="20%"> </td>
        </tr>
    </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">


*
{
padding: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 0px;
        }

    </style>
</asp:Content>


