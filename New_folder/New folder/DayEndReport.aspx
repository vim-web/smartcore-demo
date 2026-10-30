
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DayEndReport.aspx.vb" Inherits="SMARTCORE_UI.DayEndReport" 
     title="Day End Report" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>Day End Report</legend>

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
                <asp:Label ID="LblDdl1" runat="server" Text="Branch" ForeColor="#6699FF"></asp:Label>
             </td>
            <td > 
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > 
                &nbsp;</td>
            <td > 
                &nbsp;</td>
            <td > </td>
        </tr>
       
        
        
        
       
         <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDt1" runat="server" Text="As On Date" ForeColor="#6699FF"></asp:Label>
             </td>
            <td > 
                <asp:TextBox ID="DtFrom" runat="server"  Width = "98%" TabIndex="2"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtFrom"> </asp:CalendarExtender>      
             </td>
            
            <td > </td>
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
                
            </td>
            <td  width="20%"> </td>
             <td align="center" colspan="3">
                  &nbsp; <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" style="height: 23px" />
              </td>
            <td width="20%"> </td>
        </tr>
        
           <tr>
            <td width="6%"> </td>
            <td width="18%"> </td>
            <td width="26%"> 
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
            <td width="18%"> </td>
            <td width="26%"> </td>
            <td width="6%"> </td>
        </tr>
        
    </table>
            </div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
      
      
