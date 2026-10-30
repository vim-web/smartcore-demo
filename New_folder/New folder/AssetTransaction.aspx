<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AssetTransaction.aspx.vb" Inherits="SMARTCORE_UI.AssetTransaction" 
    title="Asset Transaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script> 
    <script language="javascript" type="text/javascript">        
function fSplitValues(source, eventArgs) {
var sStr;
var extender = source.get_id();         //id of the source extender            
    if (document.getElementById("ctl00_ContentPlaceHolder1_TxtItemCode").value != "") {          
        sStr = document.getElementById("ctl00_ContentPlaceHolder1_TxtItemCode").value;
        sStr = sStr.replace(/  /g,"")
        sStr = sStr.replace("Code:","")
        sStr = sStr.replace(".","")
        if (sStr.indexOf("|") != -1) {                
            sStr = sStr.split("|");
            document.getElementById("ctl00_ContentPlaceHolder1_TxtItemCode").value = sStr[1];
            document.getElementById("ctl00_ContentPlaceHolder1_TxtItemName").value = sStr[0].replace(".","");                        
            document.getElementById("ctl00_ContentPlaceHolder1_CmbRefNo").focus();
        }
    }
}

</script>
     
      <style type="text/css">
        .listMain
{
    background-image:url('images/wordWheelBack.gif');
    background-repeat:repeat-x;
    background-color:#FFFFFF;
    z-index:1000;
    width:795px !important;
    max-height:310px;
    list-style:none;
    overflow-y:scroll;
    scrollbar-arrow-color:#B89020;
    scrollbar-base-color:#8E6E1C;
    scrollbar-face-color:#B6C5D4;
    scrollbar-3dlight-color:#8E6E1C;
    scrollbar-highlight-color:#EED47D;
    scrollbar-shadow-color:#959595;
    scrollbar-darkshadow-color:#00337E;
    margin-left:0px;
    border-bottom:1px solid #B5C6D4;
    border-left:1px solid #B5C6D4;
    margin-top:0px;
    font-family : Courier New ;
    font-size:small;
    text-align : left;   
}
.wordWheel .itemsMain
{
    background:none;
    border-collapse:collapse;
    color:#00337E;
    white-space:nowrap;
    font-family : Courier New ;
    font-size:small;
    text-align : left;
}
.wordWheel .itemsSelected
{
    background-color:#3366CC;  
    color:Snow;  
    font-family : Courier New ;
    font-size:small;
    text-align : left;
}
.TitleBar {
    background-color: #33F;
    border-top-color: #39F;
    border-right-color: #39F;
    border-bottom-color: #39F;
    border-left-color: #39F;
    padding: 2px;
    font-size: 12px;
    font-weight: bold;
    color: #FFF;
    text-decoration: blink;
}
.floatingDiv
{
  position:absolute;         
  top: 630px;
  box-shadow: 10px 10px 10px #888888;
  color: #42a2d7;
  background-color:#FFFFFF;
  border:1px solid #000000; 
  border-radius:3px; 
  width:450px; 
  height:auto; 
  display:none;
}        
        #progress
        {
            width: 135px;
        }
    </style>
   </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
       
        </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;margin-top:5%; " >                
        &nbsp;&nbsp;                
        <legend>
            <asp:Label ID="Head" runat="server" Text=""></asp:Label></legend> <br />
<table width="100%">
<tr>
<td width="2%"></td>
<td width="20%">
    </td>
<td width="28%">
    &nbsp;</td>
     <td width="2%"></td>
<td width="18%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="20%">
    <label>
    ID</label></td>
<td width="28%">
    <asp:TextBox ID="TxtID" runat="server" AutoPostBack="True" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        Width="96%"></asp:TextBox>
    </td>
    <td width="2%"></td>
<td width="18%">
    <label>
    Date</label> </td>
<td width="25%">
    <asp:TextBox ID="DtpDate" runat="server" CssClass="twitterStyleTextbox" 
        Width="100%" TabIndex="1"></asp:TextBox>
    <asp:CalendarExtender ID="DtpDate_CalendarExtender" runat="server" 
        format="dd/MM/yyyy" TargetControlID="DtpDate">
    </asp:CalendarExtender>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="20%">
    <label>
    From Branch</label> </td>
<td width="28%">
    <asp:DropDownList ID="CmbBranch" runat="server" AppendDataBoundItems="True" 
        width="100%" TabIndex="2">
    </asp:DropDownList>
    </td>
     <td width="2%"></td>
<td width="18%">
    <label>
    <asp:Label ID="lblToBranch" runat="server" Text="To Branch" Visible="False"></asp:Label>
    <asp:Label ID="lbldealer" runat="server" Text="Dealer" Visible="False"></asp:Label>
    </label>
    </td>
<td width="25%"><asp:TextBox ID="TxtDealer" runat="server" Visible="false" 
        Width="100%" CssClass="twitterStyleTextbox" TabIndex="3"  ></asp:TextBox>
    <asp:DropDownList ID="CmbTransBranch" runat="server" width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>
    <tr>
        <td width="2%">
            </td>
        <td width="20%">
            </td>
        <td width="28%">
            </td>
        <td width="2%">
            </td>
        <td width="18%">
            </td>
        <td width="25%">
            </td>
        <td width="5%">
            </td>
    </tr>
</table>
<div style="width:100%; ">
<table width="100%">
    <tr>
        <td width="2%">
        </td>
        <td width="13%">
        </td>
        <td width="13%">
            <label>
            Asset</label></td>
        <td width="13%">
            <label>
            Ref No.</label>
        </td>
        <td width="13%">
            <label>
            Department</label>
        </td>
        <td width="13%">
            <label>
            Qty</label>
        </td>
        <td width="13%">
            <label>
            Rate</label>
        </td>
        <td width="13%">
            <label>
            Value</label>
        </td>
        <td width="5%">
        </td>
        <td width="2%">
        </td>
    </tr>
    <tr>
        <td width="2%">
           </td>
        <td width="13%">
            <asp:TextBox ID="TxtItemCode" runat="server" width="99%" 
                CssClass="twitterStyleTextbox" AutoPostBack="True" TabIndex="4"></asp:TextBox>
              <asp:autocompleteextender ID="AutoCompleteExtender1" runat="server" 
                                TargetControlID="TxtItemCode" MinimumPrefixLength="1" CompletionInterval="10" 
                                EnableCaching="true" CompletionSetCount="6"
                                UseContextKey="True" ServiceMethod="GetCompletionList" 
                                OnClientItemSelected="fSplitValues" 
                                CompletionListCssClass="wordWheel listMain .box" 
                                CompletionListItemCssClass="wordWheel itemsMain" 
                                CompletionListHighlightedItemCssClass="wordWheel itemsSelected">
                            </asp:autocompleteextender>
        </td>
         <td width="13%"><asp:TextBox ID="TxtItemName" runat="server" width="95%" 
                 CssClass="twitterStyleTextbox" TabIndex="5"></asp:TextBox>
            </td>   
        <td width="13%">
            <asp:TextBox ID="CmbRefNo" runat="server" width="95%" 
                CssClass="twitterStyleTextbox" TabIndex="6"></asp:TextBox>
                
        </td>
        <td width="13%">
            <asp:DropDownList ID="CmbDept" runat="server" width="100%" TabIndex="7">
            </asp:DropDownList>
        </td>
        <td width="13%">
            <asp:TextBox ID="TxtQty" runat="server" width="95%" AutoPostBack="True" onkeypress="return isFloatKey(event);" 
                CssClass="twitterStyleTextbox" TabIndex="8"></asp:TextBox>
        </td>
        <td width="13%">
            <asp:TextBox ID="TxtRate" runat="server" width="95%" AutoPostBack="True" onkeypress="return isFloatKey(event);" 
                CssClass="twitterStyleTextbox" TabIndex="9" ></asp:TextBox>
        </td>
        <td width="13%">
            <asp:TextBox ID="TxtValue" runat="server" width="95%" onkeypress="return isFloatKey(event);" 
                CssClass="twitterStyleTextbox" TabIndex="10"></asp:TextBox>
        </td>
        <td width="5%">
            <asp:ImageButton ID="CmdAdd" runat="server" Height="28px" 
                ImageUrl="~/Images/AddBtn.png" TabIndex="11" Width="81px" />
        </td>
        <td width="2%">
           </td>
    </tr>
    <tr>
        <td width="2%">
           </td>
        <td colspan="7" align="center" width="91%">
            <asp:GridView ID="Dgv_Trans" runat="server" AutoGenerateColumns="False" 
                TabIndex="30" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Item_Code" HeaderText="CODE">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="8%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Item_Name" HeaderText="NAME" >
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="18%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="FATrans_RefNo" HeaderText="REF No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FATrans_DeptID" HeaderText="DEPARTMENT ID" Visible="false"/>
                       
                    <asp:BoundField DataField="FATrans_DeptName" HeaderText="DEPARTMENTNAME">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Item_Qty" HeaderText="QUANTITY">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="14%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Item_Rate" HeaderText="RATE">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Item_Value" HeaderText="VALUE" >
                   <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F4FF" />
            </asp:GridView>
        </td>
        <td width="5%"></td>
        <td width="2%">
           </td>
    </tr>
<tr>
        <td width="2%">
          </td>
        <td width="10%">&nbsp;</td>
         <td colspan="4">
            </td> 
        <td colspan="3" style="width: 36%">
           
          </td>
        <td width="2%">
          </td>
    </tr>
    <tr>
        <td width="2%">
        </td>
        <td width="13%">
        </td>
        <td width="13%">
       </td>
        <td width="13%">
            <label>
            Remarks</label>
        </td>
        <td width="13%" colspan="2" style="width: 18%">
            <asp:TextBox ID="TxtRemarks" runat="server" CssClass="twitterStyleTextbox" 
                Width="95%" TabIndex="12"></asp:TextBox>
        </td>
        <td width="13%">
<%--            <label>
            Currency
            </label>--%>
        </td>
        <td width="13%">
            <asp:DropDownList ID="CmbCurrency" runat="server" AutoPostBack="True" 
                TabIndex="29" Width="100%" Visible ="false" >
            </asp:DropDownList>
        </td>
        <td width="5%">
        </td>
        <td width="2%">
        </td>
    </tr>
        <tr>
        <td width="2%">
        </td>
        <td width="13%">
        </td>
        <td width="13%">
       </td>
        <td width="13%">
            &nbsp;</td>
        <td width="13%" colspan="2" style="width: 18%">

        </td>
        <td width="13%">
        </td>
        <td width="13%">
        </td>
        <td width="5%">
        </td>
        <td width="2%">
        </td>
    </tr>
    <tr>
        <td width="2%">
        </td>
        <td width="13%">
        </td>
        <td width="13%">
        </td>
        <td align="center" colspan="3" style="width: 26%">
            <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
                TabIndex="13" />
            <asp:ImageButton ID="CmdClear1" runat="server" ImageUrl="~/Images/Clear.png" 
                meta:resourcekey="CmdSaveResource1" TabIndex="14" />
            <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" 
                TabIndex="15" />
        </td>
        <td width="13%">
        </td>
        <td width="13%">
        </td>
        <td width="5%">
        </td>
        <td width="2%">
        </td>
    </tr>
    


</table>
        </div>

</div>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
