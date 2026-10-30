<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reports.aspx.vb" Inherits="SMARTCORE_UI.Reports" MasterPageFile="~/SMARTCORE.Master" %>
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
    <div class="gradientBoxesWithOuterShadows" height: 640px;width: 100%; margin-left:25%; margin-right:25%;>    
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
                <asp:Label ID="Label6" runat="server" Text="Section"></asp:Label>
            </td>
            <td >  
                <asp:DropDownList ID="DdlSection" runat="server" Width="98%" 
                    AutoPostBack="True" CausesValidation="True">
                </asp:DropDownList>
            </td>
            <td >   
                <asp:Label ID="Label5" runat="server" Text="Report Type"></asp:Label>
            </td>
            <td >   
                <asp:DropDownList ID="DdlRptType" runat="server" Width="98%" 
                    AutoPostBack="True" CausesValidation="True">
                </asp:DropDownList>
            </td>
            <td> </td>
        </tr>
         <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDdl1" runat="server" Text="Branch"></asp:Label>
             </td>
            <td > 
                <asp:DropDownList ID="Ddl1" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > 
                <asp:Label ID="LblDdl2" runat="server" Text="Scheme"></asp:Label>
            </td>
            <td > 
                <asp:DropDownList ID="Ddl2" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > </td>
        </tr>
        
         <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDt1" runat="server" Text="From Date"></asp:Label>
             </td>
            <td > 
                <asp:TextBox ID="Dt1" runat="server"  Width = "98%" TabIndex="2"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="Dt1"> </asp:CalendarExtender>      
             </td>
            <td > 
                <asp:Label ID="LblDt2" runat="server" Text="To Date"></asp:Label>
            </td>
            <td > 
               <asp:TextBox ID="Dt2" runat="server"  Width = "98%" TabIndex="2" 
                    AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="Dt2"> </asp:CalendarExtender>      
             </td>
            <td > </td>
        </tr>
        <tr>
            <td > </td>
            <td > <asp:Label ID="LblTxt1" runat="server" Text="Text"></asp:Label></td>
            <td > <asp:TextBox ID="Txt1" runat="server"  Width = "98%" TabIndex="2"></asp:TextBox> </td>
            <td > 
                <asp:Label ID="LblTxt2" runat="server" Text="Text"></asp:Label>
            </td>
            <td > 
                <asp:TextBox ID="Txt2" runat="server" TabIndex="2" Width="98%"></asp:TextBox>
            </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDt3" runat="server" Text="d"></asp:Label>
            </td>
            <td > 
                <asp:TextBox ID="Dt3" runat="server" TabIndex="2" Width="98%"></asp:TextBox>
                <asp:CalendarExtender ID="Dt3_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="Dt3">
                </asp:CalendarExtender>
            </td>
            <td > 
                <asp:Label ID="LblDt4" runat="server" Text="d"></asp:Label>
            </td>
            <td > 
                <asp:TextBox ID="Dt4" runat="server" AutoCompleteType="Disabled" TabIndex="2" 
                    Width="98%"></asp:TextBox>
                <asp:CalendarExtender ID="Dt4_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="Dt4">
                </asp:CalendarExtender>
            </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td> 
              <asp:Label ID="Lbl_Date0" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                  ForeColor="#6666FF" Text="Output To"></asp:Label>
              </td>
            </td>
            <td > 
                  <asp:DropDownList ID="CmbOutput" runat="server" Width = "75%">
                  </asp:DropDownList>
            </td>
            <td > </td>
            <td > </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td > </td>
            <td > </td>
            <td > </td>
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
            <td  width="20%"> </td>
            <td  colspan="3">  
                <asp:Button ID="BtnProcess" runat="server" Text="Process" Width="30%" />
                <asp:Button ID="BtnShow" runat="server" Text="Show Report"  Width="30%" />
                <asp:Button ID="BtnClose" runat="server" Text="Close"  Width="30%" />
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
margin: 0px;
padding: 0px;
}

    </style>
</asp:Content>
