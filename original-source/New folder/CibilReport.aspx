<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CibilReport.aspx.vb" Inherits="SMARTCORE_UI.CibilReport" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%;" >                
    
    <legend>CIBIL REPORT</legend>  
    <table width="100%">
    <tr>
    <td width="2%"></td>
    <td width="48%"><label>Report Type</label></td>
    <td width="48%">
       <asp:DropDownList ID="DDL_ReportType" runat="server" Width="100%" AutoPostBack="True">
           <asp:ListItem Value="CYBIL GL">CYBIL GL</asp:ListItem>
                <asp:ListItem Value="CYBIL EMI">CYBIL EMI</asp:ListItem>
                <asp:ListItem Value="OS TYPE2">OS TYPE2</asp:ListItem>
                 </asp:DropDownList>
        </td>
    <td width="2%"></td>
    </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>As on Date</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAsOnDate" runat="server" OnChange="validatedate(this);" Width="100%"></asp:TextBox>
                <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtAsOnDate">
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
            <td width="48%" align="center" colspan="2" style="width: 96%">
                <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png"/>
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" />
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>

                <tr>
              <asp:GridView ID="GridView1" runat="server" ></asp:GridView>
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
