<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="MIS_setting.aspx.vb" Inherits="SMARTCORE_UI.MIS_setting" 
    title="MIS Settings" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
     .txt_class{color:#7465C9; font-family:Arial; text-align:center; font-size:90%; font-weight:500; }
   .grid-view tr.rowHover:hover{background-color:#DFD9FD; font-family:Arial; color:#FFF; }
   .grid-view tr.rowHover{color:#4C3CA4;}
    </style>
    <script>
        function validation(ddl) {

            var grd = document.getElementById('<%=Grid_reportdata.ClientID %>');
            var ddl_Txt = ddl.options[ddl.selectedIndex].text;
            for (var i = 1; grd.rows.length > 0; i++) {
                var Acc_head = grd.rows[i].cells[1].innerHTML;
               
                if (ddl_Txt == Acc_head) {
                    alertify.error(' This Account Head Already Selected');
                    ddl.value = '777';
                    return false;
                }
            }
            return true;
        }

        function Vald_Save() {
            
            var valid = document.getElementById('<%=hid_gird_row.ClientID %>');
            if (valid.value == "NEW") {
                alertify.error('Please Add Transaction');
                return false;
            }
           
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 <div style="width:100%;margin:0 auto;">
 <div style="width:1000px; margin:0 auto;" >
 
 
 
<div class="gradientBoxesWithOuterShadows" style="width: 80%; margin:0 auto; margin-top:40px;  font-family: Tahoma" >                
   
    <div style="width:100%;height:40px; padding-top:20px; text-align:center; font-weight:bold; color:#7B67EB;" >
   MIS Setting

 </div>

   <table style="width:80%;  margin:0 auto; margin-top:30px;">
   <tr  >
   <td style="text-align:center;" class="txt_class" >Report Name</td>
   <td colspan="2" >
       <asp:DropDownList ID="ddl_report" runat="server"  style="width:100%" 
           AutoPostBack="True" >
           <asp:ListItem>Select</asp:ListItem>
       </asp:DropDownList>
       </td>
   </tr>
   <tr  >
   <td style="width:30%;text-align:center;" class="txt_class" >Account Head</td>
   <td style="width:70%" colspan="2">
       <asp:DropDownList ID="ddl_accounthead" runat="server" onchange="return validation(this)" style="width:100%"  >
           <asp:ListItem>Select</asp:ListItem>
       </asp:DropDownList>
       </td>
   </tr>
   <tr  >
   <td style="width:30%">  </td>
   <td style="width:30%">
       <asp:RadioButtonList ID="Radio_report" runat="server" 
           RepeatDirection="Horizontal">
           <asp:ListItem Value="A" Selected >Add</asp:ListItem>
           <asp:ListItem Value="S">Subtract</asp:ListItem>
       </asp:RadioButtonList>
       </td>
   <td style="width:40%">
   <input type="hidden" id="hid_gird_row" value="NEW" runat="server"  />
       <%--<asp:Button ID="but_add" runat="server" Text="Add" />--%>
          <asp:ImageButton ID="but_add1" runat="server" ImageUrl="~/Images/AddBtn.png" />
                        </td>
   </tr>
    
   <tr  >
   <td style="width:30%;  "> </td>
   <td style="width:70%; padding-top:30px; overflow:auto;" colspan="2"  >
       <asp:GridView ID="Grid_reportdata"  CssClass="grid-view" RowStyle-CssClass="rowHover"  Width="100%"  runat="server" AutoGenerateColumns="False">
       <Columns>
               <asp:BoundField DataField="Rpt_Code" HeaderText="Rpt_Code"  />
               <asp:BoundField HeaderText="Report Name" DataField="Rpt_Name" />
               <asp:BoundField DataField="Acc_Code" HeaderText="Acc_Code" />
               <asp:BoundField HeaderText="Account Head" DataField="Acc_Name" />
               <asp:BoundField HeaderText="Transaction" DataField="Acc_Type" />
               <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/ClearBtn.png" 
                   Text="Delete" CommandName="Delete" />
           </Columns>
       </asp:GridView>
                        </td>
   </tr>
   <tr  >
   <td style="width:30%">&nbsp;</td>
   <td style="width:70%; padding-top:20px; overflow:auto;" colspan="2"  >
       <asp:ImageButton ID="CmdShow" runat="server" Height="37px" 
                      ImageUrl="~/Images/Save.png" 
           OnClientClick="return Vald_Save();" Width="96px" />
                  &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" Height="36px" Width="109px" />
                        </td>
   </tr>
   </table>
       </div>


</div>
 </div>
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
