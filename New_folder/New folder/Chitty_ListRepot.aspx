<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Chitty_ListRepot.aspx.vb" Inherits="SMARTCORE_UI.Chitty_ListRepot" MasterPageFile="~/SMARTCORE.Master" %>
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
    <legend>Chitty List / Chitty At a Glance</legend>   
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
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%" AutoPostBack="True">
                </asp:DropDownList>
             </td>
            <td > 
                <label>Group</label>
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
                <label>As On Date</label>
             </td>
            <td > 
                <asp:TextBox ID="DtFrom" runat="server"  Width = "98%" TabIndex="2"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtFrom"> </asp:CalendarExtender>      
             </td>
            <td > 
                &nbsp;</td>
            <td > 
                &nbsp;</td>
            <td > </td>
        </tr>
        <tr>
            <td  > </td>
            <td  > &nbsp;</td>
            <td  >  
           
           
                &nbsp;<td  > 
                    &nbsp;</td>
            <td  > 
                &nbsp;</td>
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
                &nbsp;
                <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                    ImageUrl="~/Images/Show.png" />
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" PostBackUrl="~/Main.aspx" />
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
