<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="GoldLoanOpenClose_Report.aspx.aspx.vb" Inherits="SMARTCORE_UI.GoldLoanOpenClose_Report_aspx" %>
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
    <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma;margin-top:10%;" >                
    <legend>Gold Loan Opening/Closure Report</legend> 
    <table width="100%">
        <tr>
            <td width="6%"> </td>
            <td width="18%"> </td>
            <td width="26%"> </td>
               <td width="3%"> </td>
            <td width="15%"> </td>
            <td width="26%"> </td>
            <td width="6%"> </td>
        </tr>
        <tr>
            <td > </td>
            <td > 
                <Label>Region</Label>
             </td>
            <td > 
                 <asp:DropDownList ID="DrpRegn" runat="server"  Width="100%" AutoPostBack="true">
          </asp:DropDownList>
             </td>
              <td > </td>
            <td > 
              <Label>Area</Label>
            </td>
            <td > 
                 <asp:DropDownList ID="DrpArea" runat="server"  Width="100%" AutoPostBack="true">
          </asp:DropDownList>
             </td>
            <td > </td>
        </tr>
         <tr>
            <td > </td>
            <td > 
                <Label>Branch</Label>
             </td>
            <td > 
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
              <td > </td>
            <td > 
              <Label>Scheme</Label>
            </td>
            <td > 
                <asp:DropDownList ID="DdlScheme" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > </td>
        </tr>
        
         <tr>
            <td > </td>
            <td > 
                <Label>From Date</Label>
             </td>
            <td > 
                <asp:TextBox ID="DtFrom" runat="server"  Width = "95%" TabIndex="2" CssClass="TwitterStyleTextbox"> </asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtFrom"> </asp:CalendarExtender>      
             </td>
             <td></td>
            <td > 
                <Label>To Date</Label>
            </td>
            <td > 
                <asp:TextBox ID="DtTo" runat="server" TabIndex="2" Width="95%" CssClass="TwitterStyleTextbox"></asp:TextBox>
                <asp:CalendarExtender ID="DtTo_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="DtTo">
                </asp:CalendarExtender>
             </td>
            <td > </td>
        </tr>
       
        <tr>
            <td>
               </td>
            <td>
               </td>
            <td>
               </td>
            <td>
               </td>
            <td>
               </td>
            <td>
               </td>
            <td>
               </td>
        </tr>
       
        <tr>
            <td > </td>
            <td > 
               </td>
            <td align="center" colspan="3" > 
                <asp:RadioButton ID="rbtnOpening" runat="server" AutoPostBack="True" 
                    Checked="True" OnCheckedChanged="rbtnOpening_CheckedChanged" Text="Opening" />
                <asp:RadioButton ID="rbtnClosing" runat="server" AutoPostBack="True" 
                    OnCheckedChanged="rbtnClosing_CheckedChanged" Text="Closing" />
            </td>
            <td > 
               </td>
            <td> </td>
        </tr>
       
        <tr>
            <td>
               </td>
            <td>
               </td>
            <td align="center" colspan="3">
               </td>
            <td>
               </td>
            <td>
               </td>
        </tr>
       
        <tr>
            <td > </td>
            <td > </td>
            <td colspan="3" align="center" > 
               
                <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                    ImageUrl="~/Images/Show.png" />
               <asp:ImageButton ID="Img_Close" runat="server" Height="25px" 
                    Width="75px" ImageUrl="~/Images/CloseBtn.png" />
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
            <td  width="20%">  <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
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
               </td>
            <td width="20%"> </td>
        </tr>
    </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <LINK REL=Stylesheet HREF= "CSS/MasterStyle.css" TYPE="text/css" MEDIA=screen>

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

