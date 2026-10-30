<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AssetStock_rpt.aspx.vb" Inherits="SMARTCORE_UI.AssetStock_rpt" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script> 
    
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnablePageMethods="True" CombineScripts="True"> 
       
        </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; " >                
        <legend>REGISTER REPORT</legend> <br />
        <table width="100%">
            <tr>
                <td width="2%"></td>
                <td width="48%"> </td>
                 <td width="48%"> </td>
                  <td width="2%"></td>               
            </tr>
            <tr>
                <td width="2%"></td>
                <td width="48%"><label>From Date</label></td>
                 <td width="48%"> 
                     <asp:TextBox ID="dtpDateTo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                     <asp:CalendarExtender ID="dtpDateTo_CalendarExtender" runat="server" Enabled="True" TargetControlID="dtpDateTo" Format="dd/MM/YYYY">
                     </asp:CalendarExtender>
                </td>
                  <td width="2%"></td>               
            </tr>
            <tr>
                <td width="2%"  ></td>
                <td width="48%"  ></td>
                 <td width="48%" >    
                     <asp:CheckBox ID="ChkGroup" runat="server" AutoPostBack="True" 
                         Text="Select Group" />
                </td>              
              
                  <td width="2%" ></td>               
            </tr>
            <tr>
                <td width="2%"  ></td>
                <td width="48%"  ><label>Select Group</label></td>
                 <td width="48%" >    
                     <asp:DropDownList ID="CmbGroup" runat="server" Width="100%" Enabled="False">
                     </asp:DropDownList>
                </td>              
              
                  <td width="2%" ></td>               
            </tr>
             <tr>
                <td width="2%"></td>
                <td width="48%" align="right"> 
                    <asp:ImageButton ID="CmdShow" runat="server" Height="25px" ImageUrl="~/Images/Show.png" />
                 </td>
                 <td width="48%"> 
                     <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/CloseBtn.png" />
                 </td>
                  <td width="2%"></td>               
            </tr>
        </table>
        </div>
    </ContentTemplate>
         </asp:UpdatePanel>

</asp:Content>

