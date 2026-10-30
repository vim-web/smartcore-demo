<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="NextNumber.aspx.vb" Inherits="SMARTCORE_UI.NextNumber" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:20%; margin-right:10%;margin-top:8%;">    
<legend> NEXT NUMBER CHANGE </legend>

    <table  width ="100%">
        <tr>
            <td  width ="5%">
                 
            </td>
            <td width ="20%">
                 
            </td>
            <td width ="30%">
                 
            </td>
                        <td width ="20%">
                 
            </td>
                        <td width ="100%">
                 
            </td>
                 
            </td>
        </tr>
        <tr>
            <td  width ="5%">
                 
                &nbsp;</td>
            <td width ="20%">
                 
                &nbsp;</td>
            <td width ="30%">
                 
                &nbsp;</td>
                        <td width ="20%">
                 
                            &nbsp;</td>
                        <td width ="100%">
                 
                            &nbsp;</td>
                 
        </tr>
        <tr>
            <td  width ="5%">
                 
                &nbsp;</td>
            <td width ="20%">
                 
                &nbsp;</td>
            <td width ="30%">
                 
                &nbsp;</td>
                        <td width ="20%">
                 
                            &nbsp;</td>
                        <td width ="100%">
                 
                            &nbsp;</td>
                 
        </tr>
        <tr>
                    <td>
                         </td>
                        <td>
                             <label>
                             Branch Name</label></td>
            <td>
                    <asp:DropDownList ID="CmbBranch" runat="server" TabIndex="2" Width="100%" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                 </td>
            <td>
                 </td>
                        <td>
                             </td>
            <td>
                 </td>
        </tr>
        <tr>
                    <td class="style1">
                 
            </td>
                        <td class="style1">
                 
                            <label>
                            Scheam Type</label></td>
            <td class="style1">
                 
                <table width="100%">
                    <tr>
                        <td width="20%">
                            <asp:TextBox ID="TxtSchCode" runat="server" AutoPostBack="True" 
                                CssClass="twitterStyleTextbox" TabIndex="3" Width="100%"></asp:TextBox>
                        </td>
                        <td width="2%">
                        </td>
                        <td width="78%">
                            <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                                TabIndex="3" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="style1">
                 
            </td>
                        <td class="style1">
                 
            </td>
            <td class="style1">
                 
            </td>
        </tr>
        <tr>
                    <td>
                         </td>
                        <td>
                             <label>
                             Agent Wise</label></td>
            <td>
          <asp:DropDownList ID="CmbAgent" runat="server" Width ="100%" Enabled="False"  AutoPostBack ="true"  >
          </asp:DropDownList>
                 </td>
            <td>
<asp:CheckBox ID="ChkAgent" runat="server" Text="With Agent" AutoPostBack="True" />
                    </td>
                        <td>
                             </td>
            <td>
                 </td>
        </tr>
        <tr>
                    <td>
                 
            </td>
                        <td>
                 
                            <label>
                            &nbsp;Last Number</label></td>
            <td>
            
<table width ="100%">
<tr>
<td width ="30%">
                            <asp:TextBox ID="TxtOldNo" runat="server" AutoPostBack="True" 
                                CssClass="twitterStyleTextbox" TabIndex="3" 
        Width="100%" ReadOnly="True"></asp:TextBox>
                        </td>
<td width ="5%"></td>
<td width ="30%">
    <label>
                             Next Number </label></td>
<td width ="5%"></td>
<td width ="30%">
                            <asp:TextBox ID="TxtNewNo" runat="server" AutoPostBack="True" 
                                CssClass="twitterStyleTextbox" TabIndex="3" 
        Width="100%"></asp:TextBox>
                        </td>


</tr>

</table>
            
                 
            </td>
            <td>
                 
            </td>
                        <td>
                 
            </td>
                        <td>
                 
            </td>
        </tr>
        <tr>
                    <td>
                 
                        &nbsp;</td>
                        <td>
                 
                            &nbsp;</td>
            <td>
            
            <table width ="100%">
            <tr>
            <td WIDTH="50%"> 
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="21"  
                    OnClientClick="return ValidateSave();"/>  </td>
            <td WIDTH="50%"> 
            
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="23" />
              </td>
            
            </tr>
            
            </table>
            
            
                    </td>
            <td>
                 
                &nbsp;</td>
                        <td>
                 
                            &nbsp;</td>
                        <td>
                 
                            &nbsp;</td>
        </tr>
    </table>
    </div>
</asp:Content>
