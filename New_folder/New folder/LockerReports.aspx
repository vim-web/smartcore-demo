<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LockerReports.aspx.vb" Inherits="SMARTCORE_UI.LockerReports" 
    title="Loan Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>LOCKER REPORTS</legend>

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
                <asp:Label ID="LblDdl2" runat="server" Text="Scheme" ForeColor="#6699FF"></asp:Label>
            </td>
            <td > 
                <asp:DropDownList ID="DdlScheme" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > </td>
        </tr>
       
        <tr>
            <td width="6%"> </td>
            <td width="18%"> 
                <asp:Label ID="Lbl_ReportOpt" runat="server" ForeColor="#6699FF" 
                    Text="Report Options"></asp:Label>
            </td>
            <td width="26%" colspan="2" style="width: 44%"> 
                <asp:DropDownList ID="DdlReportOptions" runat="server" AutoPostBack="True" 
                    Width="98%">
                </asp:DropDownList>
            </td>
            <td width="26%"> </td>
            <td width="6%"> </td>
        </tr>
        
        
       
         <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDt1" runat="server" Text="" ForeColor="#6699FF"></asp:Label>
             </td>
            <td > 
                <asp:TextBox ID="DtFrom" runat="server"  Width = "98%" TabIndex="2"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtFrom"> </asp:CalendarExtender>      
             </td>
            <td > 
                <asp:Label ID="LblDt2" runat="server" Text="To Date" ForeColor="#6699FF" 
                    Visible="False"></asp:Label>
            </td>
            <td > 
               <asp:TextBox ID="DtTo" runat="server"  Width = "98%" TabIndex="2" 
                    AutoCompleteType="Disabled" Visible="False"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtTo"> </asp:CalendarExtender>      
             </td>
            <td > </td>
        </tr>
        <tr>
            <td > </td>
            <td>
                  <label>Output To</label></td>
              <td>
                  <asp:DropDownList ID="CmbOutput" runat="server" Width="100%">
                  </asp:DropDownList>
              </td>

            </td>
            <td > 
                  &nbsp;</td>
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
                
            </td>
            <td  width="20%"> </td>
             <td align="center" colspan="3">
                  <asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                      ImageUrl="~/Images/ProcessBtn.png"  />
                  &nbsp; <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
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
      
      
