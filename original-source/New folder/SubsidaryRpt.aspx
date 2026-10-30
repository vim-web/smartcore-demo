<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="SubsidaryRpt.aspx.vb" Inherits="SMARTCORE_UI.SubsidaryRpt" 
    title="Subsidary Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>Subsidary Report</legend>  
     <table width="100%">
     <tr>
    <td width="2%"></td>
     <td width="32%">
      
       <asp:RadioButton ID="Opt_Both" runat="server" AutoPostBack="True" 
            Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" 
                    ForeColor="White" GroupName="RptType" Text="Both" Checked="True" />
        </td>
        <td width="32%">
   <asp:RadioButton ID="Opt_Receipt" runat="server" AutoPostBack="True" Font-Bold="True" 
                Font-Names="Tahoma" Font-Size="Small" 
                    ForeColor="White" GroupName="RptType" Text="Receipt" /></td> 
    <td width="32%">
       <asp:RadioButton ID="Opt_Payment" runat="server" AutoPostBack="True" 
            Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" 
                    ForeColor="White" GroupName="RptType" Text="Payment" />
        </td>
    <td width="2%"></td>
    </tr>
    
     <tr>
    <td width="2%"></td>
    <td width="48%"><label>Branch</label></td>
    <td width="48%">
         <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
        </td>
    <td width="2%"></td>
    </tr>
    
    <tr>
            <td width="2%">
                </td>
            <td width="48%">
                <label>
                From Date</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtFromDate" runat="server" CssClass="twitterStyleTextbox" 
                    OnChange="validatedate(this);"></asp:TextBox>
                <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="TxtFromDate">
                </asp:CalendarExtender>
            </td>
            <td width="2%">
                </td>
        </tr>
        <tr>
            <td width="2%">
                </td>
            <td width="48%">
                <label>To Date</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtToDate" runat="server" OnChange="validatedate(this);" CssClass="twitterStyleTextbox"></asp:TextBox>
                <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="TxtToDate">
                </asp:CalendarExtender>
            </td>
            <td width="2%">
                </td>
        </tr>


  <tr>
    <td width="2%"></td>
     <td width="48%">
     
   <asp:RadioButton ID="Opt_Scheme" runat="server" AutoPostBack="True" 
                    Checked="True" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" 
                    ForeColor="White" GroupName="RptType1" Text="Scheme Wise" /></td> 
    <td width="48%">
       <asp:RadioButton ID="Opt_GlHeadWise" runat="server" AutoPostBack="True" 
            Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" 
                    ForeColor="White" GroupName="RptType1" Text="GL Head Wise" />
        </td>
    <td width="2%"></td>
    </tr>
    

    <tr>
    <td width="2%"></td>
    <td width="48%"><label>Section</label></td>
    <td width="48%">
        <asp:DropDownList ID="CmbSection" runat="server" Width="100%" 
            AutoPostBack="True">
        </asp:DropDownList>
        </td>
    <td width="2%"></td>
    </tr>
        <tr>
            <td width="2%">
             </td>
               <td width="48%"></td>
               
            <td width="48%">
                <asp:DropDownList ID="CmbHead" runat="server" Width="100%" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="2%">
                </td>
        </tr>
                <tr>
            <td width="2%">
                </td>
            <td width="48%" align="center" colspan="2" style="width: 96%">
            <asp:ImageButton ID="ImgProcess" runat="server" Height="25px" 
                    ImageUrl="~/Images/ProcessBtn.png" />
                <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" 
                    style="height: 23px" />
                <asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" />
            </td>
            <td width="2%">
                </td>
        </tr>
    </table>
     <table width="100%">
        <tr>
            <td width="20%"> 
                &nbsp;</td>
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

