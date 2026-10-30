<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AccHeadTranEdit.aspx.vb" Inherits="SMARTCORE_UI.AccHeadTranEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>    
    <style type="text/css">
            
    </style>

       <script language="javascript" type="text/javascript">
           function fSplitValues(source, eventArgs) {
               var sStr;
               var extender = source.get_id();         //id of the source extender            
               if (document.getElementById("ctl00_ContentPlaceHolder1_TxtAccCode").value != "") {
                   sStr = document.getElementById("ctl00_ContentPlaceHolder1_TxtAccCode").value;
                   sStr = sStr.replace(/  /g, "")
                   sStr = sStr.replace("Code:", "")
                   sStr = sStr.replace(".", "")
                   if (sStr.indexOf("|") != -1) {
                       sStr = sStr.split("|");
                       document.getElementById("ctl00_ContentPlaceHolder1_TxtAccCode").value = sStr[1];
                       document.getElementById("ctl00_ContentPlaceHolder1_TxtAccName").value = sStr[0].replace(".", "");
                       document.getElementById("ctl00_ContentPlaceHolder1_TxtTransAmt").focus();
                   }
               }
           }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                CombineScripts="True" EnablePageMethods="True">
                <Scripts>
                    <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
                </Scripts>
            </asp:ToolkitScriptManager>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table style="width:100%; height: 154px;">
        <tr>
            <td class="style18">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style3" colspan="2">
                &nbsp;</td>
            <td class="style11" colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style19">
                <%--<label __designer:mapid="d1">Batch No</label></td>--%>
            <td class="style15">
                        <asp:DropDownList ID="CmbBatchNo" runat="server" Width="80%" Enabled="False" 
                            AutoPostBack="True" Visible=false>
                        </asp:DropDownList>
                        <asp:ImageButton ID="ImgBatch" runat="server"  
                            ImageUrl="~/Images/Node.png" ImageAlign=Middle Enabled="False" visible=false
                            OnClientClick="return imgBatchDtls_onclick();"/>
                      </td>
            <td class="style16">
                        <%--  <asp:Label ID="LblToBrnch" runat="server" Text="To Branch" Font-Size="10pt" 
                              ForeColor="#2C876B"></asp:Label></td>--%>
            <td class="style16">
                          <asp:DropDownList ID="DrpToBrnch" runat="server" Width="100%" 
                    Height="35px" Visible=false>
                          </asp:DropDownList>
                      </td>
            <td class="style17">
                <%--<label __designer:mapid="270">Transaction</label></td>--%>
            <td class="style14">
                          <asp:DropDownList ID="CmbAcheadTran" runat="server" Width="100%" 
                            AutoPostBack="True" TabIndex="29" Visible=false>
                        </asp:DropDownList>
                                </td>
            <td class="style14">
                &nbsp;</td>
            <td class="style14" colspan="2">
                                </td>
            <td class="style14">
                </td>
            <td class="style14">
                </td>
        </tr>
        <tr>
            <td class="style18">
                <%--<label>Account</label>--%></td>
            <td class="style10" colspan="3">
                          <asp:TextBox ID="TxtAccCode" runat="server" Width="23%" TabIndex="30"  
                              CssClass="twitterStyleTextbox" AutoPostBack="True" Height="20px" Visible=false></asp:TextBox>
                          <asp:autocompleteextender ID="AutoCompleteExtender1" runat="server" 
                                TargetControlID="TxtAccCode" MinimumPrefixLength="1" CompletionInterval="10" 
                                EnableCaching="true" CompletionSetCount="6"
                                UseContextKey="True" ServiceMethod="GetCompletionList" 
                                OnClientItemSelected="fSplitValues" 
                                CompletionListCssClass="wordWheel listMain .box" 
                                CompletionListItemCssClass="wordWheel itemsMain" 
                                
                    CompletionListHighlightedItemCssClass="wordWheel itemsSelected">
                            </asp:autocompleteextender>
                          <asp:TextBox ID="TxtAccName" runat="server" Width="58%" TabIndex="999"  Height="20px"
                              CssClass="twitterStyleTextbox" Visible=false></asp:TextBox>
                                </td>
            <td class="style12">
                <%--<label __designer:mapid="27d">Cash</label>--%></td>
            <td class="style11">
                          <asp:TextBox ID="TxtAcHeadTransAmt" runat="server" Width="96%" 
                              TabIndex="31" onkeypress="return isFloatKey(event);"  
                              CssClass="twitterStyleTextbox" AutoPostBack="True" Visible=false></asp:TextBox>
                      </td>
            <td>
         <%--       <label __designer:mapid="282">Transfer</label>--%></td>
            <td class="style7" colspan="2">
                          <asp:TextBox ID="TxtTransfer" runat="server" Width="65%" 
                              TabIndex="31" onkeypress="return isFloatKey(event);"  
                              CssClass="twitterStyleTextbox" AutoPostBack="True" Visible=false></asp:TextBox></td>
            <td>
                          &nbsp;</td>
            <td>
                      
                          <asp:Button ID="CmdOrnAdd" runat="server"  UseSubmitBehavior="False" 
                                    Width="27" TabIndex="32" BackColor="#9999FF" viible=false
                    CssClass="Add_btn" />
                            </td>
        </tr>
        <tr>
            <td class="style18">
                <label>&nbsp;Remarks </label></td>
            <td class="style2" colspan="9">
                <asp:TextBox ID="TxtRemarks" runat="server" Width="77%" 
                            meta:resourcekey="TxtLandPhoneResource1" TabIndex="33"  
                                CssClass="twitterStyleTextbox"></asp:TextBox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style18">
                &nbsp;</td>
            <td class="style2" colspan="9">
                <asp:TextBox ID="TxtRemarks1" runat="server" Width="77%" 
                            meta:resourcekey="TxtLandPhoneResource1" TabIndex="34"  
                                CssClass="twitterStyleTextbox"></asp:TextBox></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style18">
                <label __designer:mapid="298">Tocken No</label></td>
            <td class="style13">
                <asp:TextBox ID="TxtTockenNo" 
                            runat="server" Width="50%" 
                                meta:resourcekey="TxtLandPhoneResource1" TabIndex="999"  
                            CssClass="twitterStyleTextbox"></asp:TextBox></td>
            <td class="style3" colspan="2">
                        <asp:Label ID="LblWords" runat="server" Font-Bold="True" 
                    ForeColor="#FF3300"></asp:Label>
                      </td>
            <td class="style11" colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style18">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style3" colspan="2">
                &nbsp;</td>
            <td class="style11" colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1" colspan="10">
             <div id="TransDtls" style="width:100%">
                            <asp:GridView ID="DgvTransaction" runat="server" 
                        AutoGenerateColumns="False"  CssClass="grid-viewNew"
                                TabIndex="999" Width="100%" EnableModelValidation="True" >                                                         
                                <Columns>                                                     
                                   <%-- <asp:CommandField ButtonType="Button" HeaderText="Delete" CommandArgument='<%# Eval("Seq_No")%>'
                                        ShowDeleteButton="True" ShowHeader="True" />--%>
                                         <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImgDel" runat="server" 
                                                                                    CommandArgument='<%# Eval("Seq_No")%>'
                                                                                    CommandName="Delete" Width="15px" ImageUrl="~/Images/del.jpg" 
                                                                                   />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars">
                                     <ItemTemplate>
                                      <asp:TextBox ID="TxtAccCodeNew" runat="server"   AutoPostBack="True"  CssClass="twitterStyleTextbox"
                                             text='<%# Eval("AccCode")%>'  Height="20px" Width="20px" 
                                             ontextchanged="TxtAccCodeNew_TextChanged" ></asp:TextBox>
                                               <asp:autocompleteextender ID="AutoCompleteExtender1" runat="server" 
                                                TargetControlID="TxtAccCodeNew" MinimumPrefixLength="1" CompletionInterval="10" 
                                                 EnableCaching="true" CompletionSetCount="6"
                                                 UseContextKey="True" ServiceMethod="GetCompletionList" 
                                                 OnClientItemSelected="fSplitValues" 
                                                 CompletionListCssClass="wordWheel listMain .box" 
                                                 CompletionListItemCssClass="wordWheel itemsMain"                                
                                                 CompletionListHighlightedItemCssClass="wordWheel itemsSelected">
                            </asp:autocompleteextender>
                                    <asp:TextBox ID="TxtAccNameNew" runat="server"   CssClass="twitterStyleTextbox"
                                             text='<%# Eval("Acchead")%>' Height="20px" Width="150px"></asp:TextBox>
                                  </ItemTemplate>
                                  </asp:TemplateField>
                                <asp:TemplateField HeaderText="RctCash">
                                     <ItemTemplate>
                                    <asp:TextBox ID="txtRctCash" runat="server"   text='<%# Eval("RctCash")%>' 
                                             Height="20px" Width="50px"></asp:TextBox>
                                  </ItemTemplate>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="RctTransfer">
                                     <ItemTemplate>
                                    <asp:TextBox ID="txtRctTransfer" runat="server"  text='<%# Eval("RctTransfer")%>' Height="20px" Width="60px"></asp:TextBox>
                                  </ItemTemplate>
                                  </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PayCash">
                                     <ItemTemplate>
                                    <asp:TextBox ID="txtPayCash" runat="server" text='<%# Eval("PayCash")%>' Height="20px" Width="60px"></asp:TextBox>
                                  </ItemTemplate>
                                  </asp:TemplateField>
                                     <asp:TemplateField HeaderText="PayTransfer">
                                     <ItemTemplate>
                                    <asp:TextBox ID="txtPayTransfer" runat="server" 
                                             text='<%# Eval("PayTransfer")%>' Height="20px" Width="60px"></asp:TextBox>
                                  </ItemTemplate>
                                  </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seq_No" Visible="false" >
                                     <ItemTemplate>
                                    <asp:TextBox ID="txtSeq_No" runat="server" text='<%# Eval("Seq_No")%>'  ReadOnly="true" Visible="false" Width="60px"
                                             Height="20px"></asp:TextBox>
                                  </ItemTemplate>
                                  </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#F5F7FE" />
                            </asp:GridView>
                            </div>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style18">
                &nbsp;</td>
            <td class="style13">
                                    &nbsp;</td>
            <td class="style3" colspan="2">
                                    &nbsp;</td>
            <td class="style11" colspan="2">
                                    &nbsp;</td>
            <td>
                                    &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                                    &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style18">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style3" colspan="2">
                &nbsp;</td>
            <td class="style11" colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style18">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style3" colspan="2">
                &nbsp;</td>
            <td class="style5" colspan="5">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="35"  
                    OnClientClick="return ValidateSave();"/>
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="37" 
                    PostBackUrl="~/Main.aspx" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style18">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style3" colspan="2">
                &nbsp;</td>
            <td class="style11" colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
