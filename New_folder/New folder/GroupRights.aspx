<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="GroupRights.aspx.vb" Inherits="SMARTCORE_UI.GroupRights" 
    title="User Rights" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="Stylesheet" type="text/css" /> 
    <%--<script type="text/javascript">
        document.onkeypress = function (e) {
            e = e ? e : window.event;
            if (e.keyCode == 13 || e.keyCode == 39)
                return false;
        }
    </script>--%>
    <script type = "text/javascript">

        function Check_Click(objRef) {
            var grid = document.getElementById('<%= GvGroup.ClientID %>');
            var index = objRef.parentNode.parentNode.rowIndex;
            var row = objRef.parentNode.parentNode;
            var cell = grid.rows[index].cells[4];
            if (objRef.checked) {
                cell.style.backgroundColor = "LightGreen";
            }
            else {
                cell.style.backgroundColor = "LightSalmon";
            }
        }

        function CheckModify_Click(objRef) {
            var grid = document.getElementById('<%= GvGroup.ClientID %>');
            var index = objRef.parentNode.parentNode.rowIndex;
            var row = objRef.parentNode.parentNode;
            var cell2 = grid.rows[index].cells[5];
            if (objRef.checked) {
                cell2.style.backgroundColor = "LightGreen";
            }
            else {
                cell2.style.backgroundColor = "LightSalmon";
            }
        }

      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UPPanSearch" runat="server">
    <ContentTemplate>
    
    <table style="width:100% ; margin-top:20px;">
     <tr>
     <td align="center" colspan="2"   >
        <%-- <asp:ScriptManagerProxy ID="ScrptPrxy" runat="server"></asp:ScriptManagerProxy>--%>
     </td>
     
     </tr>     
                
        <tr>
        <td valign="top" align="center" colspan="2">
           <%--<asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="UPPanSearch" DisplayAfter="1">
                 <ProgressTemplate>
               
                   <div id="progress">
                   <img alt="progress" src="../../Images/progress.gif" />Please wait...
                    </div>
                 </ProgressTemplate>
          </asp:UpdateProgress>--%>
       </td>
       
       </tr>
       <tr>
       <td width="40%" >  
       <asp:Label ID="LblGroupID" runat="server" Text="Group ID"  CssClass="lblCSS" style="margin-left:70%;" ></asp:Label>
                    </td>
       <td width="60%" >
         <%--<asp:TextBox ID="TxtGroup" runat="server" AutoPostBack="true" Height="16px" CssClass="twitterStyleTextbox" 
                    onkeypress="return onlyNumbers(event,'');" Width="50%" CausesValidation="True"   
               ></asp:TextBox>--%>
          <%-- <asp:DropDownList ID="Drop_GropList" runat="server" AutoPostBack="true" Width="50%"></asp:DropDownList>--%>
           <asp:DropDownList ID="Drop_Group" runat="server"  Width="50%"  
               AutoPostBack="True"  >
           
           </asp:DropDownList>
      
           <%-- <asp:ImageButton ID="ImgButGroup" runat="server" Height="17px" 
                    ImageUrl="~/Images/Srch.png" meta:resourcekey="ImgButCustomerResource1"   
                      OnClientClick="openmodalWin('GroupRights','Pkc_Code','ctl00_Body_TxtGroup|ctl00_Body_TxtGroupName','0|1','ctl00_Body_TxtGroup|ctl00_Body_TxtGroupName');document.getElementById('ctl00_Body_TxtGroup').focus();__doPostBack('TxtGroup',''); return false;"    
                      Width="22px" />
                       <asp:Button ID="ButShow" runat="server" CssClass="btn" Height="16px" 
           onclick="ButShow_Click" onclientclick="return GroupShow();" Text="Show" />--%>
           </td>
       </tr>
       
       
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="true"  style="margin-left:50%;"
                     Text="All" />
            </td>
                <td>
                    &nbsp;</td>
        </tr>
       <tr>
       <td valign="top" align="center" colspan="2">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"> 
           <ContentTemplate>       
         <div align="center" style="overflow: scroll; margin-left:10%; width: 70%; height: 334px; "> 
           <asp:GridView ID="GvGroup" runat="server" CssClass="grid-viewNew"
                            ShowFooter="True" Width="100%" 
                            AutoGenerateColumns="False" >               
                            
                            <Columns>
                            <asp:BoundField HeaderText="Interface ID" DataField="Mnuid">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Menu">
                                    <ItemTemplate>
                                        <asp:Label ID="LblMenu" runat="server" Text='<%# Eval("MnuCaption")  %>'></asp:Label>
                                    </ItemTemplate>
                                     <HeaderTemplate>                                     
                                        <asp:DropDownList ID="DrpSearch" runat="server"  Visible="false"
                                             Width="130px" AutoPostBack="true"  />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Rights">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkView" runat="server" Enabled="false"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modify" Visible="false" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkAdd" runat="server" Enabled="false"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />

                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="User Rights">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkViewNew" runat="server" onclick="Check_Click(this)" AutoPostBack="false"  />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modify" Visible="false" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkAddNew" runat="server" onclick="CheckModify_Click(this)" AutoPostBack="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />

                                </asp:TemplateField>
                                
                             </Columns>

                               
       </asp:GridView>
       </div>
       </ContentTemplate>
        </asp:UpdatePanel>
       </td>
       </tr>
         <tr><td align="center" colspan="2">
            
                    <br />
            
                    <asp:Button ID="ButSave" runat="server" CssClass="btn" onclick="ButSave_Click"   BorderStyle="None"
                       Text="Save" Height="30px" onclientclick="return GroupRightSave();"
                        Width="100px" />
&nbsp;<asp:Button ID="ButReset" runat="server" CssClass="btn" Text="Reset" onclick="ButReset_Click"  BorderStyle="None"
                        Height="30px" Width="100px" />
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>

 </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
