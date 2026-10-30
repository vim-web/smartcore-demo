 <%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ShareTransfer.aspx.vb" Inherits="SMARTCORE_UI.ShareTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ValidateSave() {
            var Ctl = document.getElementById('<%=TxtCustID.ClientID%>');
            if (Ctl.value == "") {
                alert("Enter Customer Id..."); 
                return false;
            }

        }
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= GridTranDetails.ClientID%>');
    for (var i = 1; i < gridView.rows.length; i++) {
        var inputs = gridView.rows[i].getElementsByTagName('input');
        if (inputs != null && inputs[0] != null) {  
            if (inputs[0].type == "checkbox") {
                if (inputs[0].checked) {
                    isValid = true;
                    return true;
                }
            }
        }
    }
    alert("Please select atleast one checkbox");
    return false;
}
    </script>
    <script type ="text/javascript">
        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }



        function validateAccNo() {
            var Custid = document.getElementById('<%= TxtCustID.ClientID%>').value;
            var CustidTransfre = document.getElementById('<%= TxtTranCustId.ClientID%>').value;
            var CustidTransName = document.getElementById('<%= TxtTranCustName.ClientID%>').value;
            var CustidTransfreAddr = document.getElementById('<%= TxtTranCustAddr.ClientID%>').value;
            if (Custid == CustidTransfre) {
            
               
                alertify.error("The customer id and transfre customer Id is same....Please check");
                CustidTransfre = "";
                CustidTransName = "";
                CustidTransfreAddr = "";

                return false;
            }
        }
        function validateFromToNO() {
            var DistNoFrom = parseInt(document.getElementById('<%= TxtTranDisFrom.ClientID%>').value);
            var DistNoTo = parseInt(document.getElementById('<%= TxtTranDisTo.ClientID%>').value);
            //alert(DistNoFrom);
            //alert(DistNoTo);
            if (DistNoFrom > DistNoTo) {
                alertify.error("The Distinctive No From cannot exceeds the distinction no To");
                return false;
            } 
        }
        function FindShare() {
          

                var DistNoFrom = parseInt(document.getElementById('<%= TxtTranDisFrom.ClientID%>').value);
                var DistNoTo = parseInt(document.getElementById('<%= TxtTranDisTo.ClientID%>').value);
            if (DistNoTo >= DistNoFrom) {
                var share = DistNoTo - DistNoFrom;

                document.getElementById('<%= TxtTranCustShare.ClientID%>').value = share+1;
            }
            else{
                alertify.error("The Distinctive No From cannot exceeds the distinction no To");
            document.getElementById('<%= TxtTranCustShare.ClientID%>').value = "";
                }
        }
        function ValidateCustomerId() {
            if (this.value == "" || this.value.length > 0) {
                var Custid = document.getElementById('<%= TxtTranCustId.ClientID%>').value;
               
                alertify.error("The customer Id is not entered");
            }
        }
</script>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .fb8 {
            /*background: transparent url(../../images/buttonround.gif) no-repeat;*/
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            outline: none;
            font-weight: bolder;
            font-size: 10px;
            background: blue;
            color: White;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            margin-right: 0px;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgb(181,181,181)), color-stop(100%,rgb(100,100,100))); /* Chrome,Safari4+ */
            background: #CC6600;
        }

            .fb8:hover {
                border-style: solid;
                color: White;
                background: #FF9900;
                border: none;
                outline: none;
            }
    </style>

    <script type="text/javascript" src="Scripts/jquery.minphoto.js"></script>



    <style type="text/css">
        .fb8 {
            /*background: transparent url(../../images/buttonround.gif) no-repeat;*/
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            outline: none;
            font-weight: bolder;
            font-size: 10px;
            background: #CC6600;
            color: White;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            margin-right: 0px;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgb(181,181,181)), color-stop(100%,rgb(100,100,100))); /* Chrome,Safari4+ */
            background: #CC6600;
        }
    .hiddencol
    {
        display:none;
    }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"></asp:ToolkitScriptManager>
            <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left: 10%; margin-right: 10%; float: left;">
                <legend>SHARE TRANSFER </legend>
                   <asp:Panel ID="Pnlauthorise1" runat="server">          
                    <table class="style1" width="100%">
                         
                         <tr>
                            <td width="3%">&nbsp;</td>
                            <td width="23%">
                               <label>Branch</label></td>
                            <td width="23%">
                                <asp:DropDownList ID="DrpBranch" Width="70%" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td width="2%">&nbsp;</td>
                            <td width="23%">
                                <label>TranDate</label></td>
                            <td width="23%">
                                <asp:TextBox ID="TxtTranDate" runat="server" CssClass="twitterStyleTextbox" 
                                    meta:resourcekey="TxtNameResource1" TabIndex="5" Width="70%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                    TargetControlID="TxtTranDate">
                                </asp:CalendarExtender>
                                
                            </td>
                            <td width="3%">&nbsp;</td>
                        </tr>
                         <tr>
                             <td width="3%">
                                 &nbsp;</td>
                             <td width="23%">
                                 <label>
                                 Share Type</label></td>
                             <td width="23%">
                                 <asp:DropDownList ID="CmbShareType" runat="server" AutoPostBack="True" 
                                     Width="70%">
                                     <asp:ListItem>EQUITY</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td width="2%">
                                 &nbsp;</td>
                             <td width="23%">
                                 <label>
                                 Scheme Name</label></td>
                             <td width="23%">
                                 <asp:DropDownList ID="CmbShare" runat="server" AutoPostBack="True" Width="70%">
                                 </asp:DropDownList>
                             </td>
                             <td width="3%">
                                 &nbsp;</td>
                         </tr>
                        <tr>
                            <td width="3%"></td>
                            <td width="23%">
                                <label>
                                    Customer ID</label></td>
                            <td width="23%">

                               <asp:TextBox CssClass="twitterStyleTextbox" ID="TxtCustID" runat="server" Width="70%"
                                    meta:resourcekey="TxtCustIDResource1" AutoPostBack="True"></asp:TextBox>
                                <asp:ImageButton ID="ImgSrch" runat="server" Height="24px"
                                    ImageUrl="~/images/Search.png" Width="23px" />

                            </td>
                            <td width="2%">&nbsp;</td>
                            <td width="23%">
                                <label>
                                    Customer Name</label></td>
                            <td width="23%">
                                  <asp:TextBox CssClass="twitterStyleTextbox" ID="TxtName" runat="server" Width="70%"
                                    meta:resourcekey="TxtNameResource1" TabIndex="5"></asp:TextBox>
                            </td>
                            <td width="3%">&nbsp;</td>
                        </tr>

                             <tr>
                            <td width="3%"></td>
                            <td width="23%">
                                <label>
                                    Customer Address</label></td>
                            <td width="23%">

                                <asp:TextBox CssClass="twitterStyleTextbox" ID="TxtAddress" runat="server" Width="70%" TextMode="MultiLine"
                                    meta:resourcekey="TxtNameResource1" TabIndex="5"></asp:TextBox>


                            </td>
                            <td width="2%">&nbsp;</td>
                            <td width="23%">
                                <label>
                                    Resolution No</label></td>
                            <td width="23%">
                                  <asp:TextBox CssClass="twitterStyleTextbox" ID="TxtResoluNo" runat="server" Width="70%"
                                    meta:resourcekey="TxtNameResource1" TabIndex="5"></asp:TextBox>
                            </td>
                            <td width="3%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td width="23%">
                                <label>
                                    Resolution Date</label></td>
                            <td width="23%">

                                <asp:TextBox CssClass="twitterStyleTextbox" ID="TxtResDate" runat="server" Width="70%" 
                                    meta:resourcekey="TxtNameResource1" TabIndex="5"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtResDate">
                               </asp:CalendarExtender>

                            </td>
                             <td width="2%">&nbsp;</td>
                            <td width="23%">
                                </td>
                            <td width="23%">
                                  &nbsp;</td>
                            <td width="3%">&nbsp;</td>
                        </tr>

      
                       




<%--                        <tr>
                            <td width="3%">&nbsp;</td>
                            <td width="23%">&nbsp;</td>
                            <td width="23%">&nbsp;</td>
                            <td width="2%">&nbsp;</td>
                            <td width="23%">&nbsp;</td>
                            <td width="23%">&nbsp;</td>
                            <td width="3%">&nbsp;</td>
                        </tr>--%>
                        <tr>
                            <td width="3%">&nbsp;</td>
                            <td colspan="5">
                                <table class="style1" width="100%">
                                    <tr>
                                        <td width="15%">&nbsp;</td>
                                        <td width="70%">
                                            <asp:GridView ID="DgvShareDetails" runat="server" AutoGenerateColumns="False"
                                                CssClass="grid-viewNew" TabIndex="30" Width="100%">
                                                <Columns>

                                                      <asp:BoundField DataField="Acc_id" HeaderText="Acc_Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                                   <asp:BoundField DataField="SlNo" HeaderText="Serial No" ></asp:BoundField>
                                                     <asp:BoundField DataField="ACC_No" HeaderText="Acc No" ></asp:BoundField>
                                                    <asp:BoundField DataField="SeriesNo" HeaderText="Batch_No"></asp:BoundField>
                                                    <asp:BoundField DataField="Date_Allot" HeaderText="Date of Allotment"></asp:BoundField>
                                                    <asp:BoundField DataField="Share" HeaderText="No of Share"></asp:BoundField>

                                                    <asp:BoundField DataField="MinDistNo" HeaderText="Min DistNo" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                                    <asp:BoundField DataField="MaxDistNo" HeaderText="Max DistNo" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>


                                                    <asp:TemplateField HeaderText="Distinctive No">
                                                        <ItemTemplate>
                                                            <%# Eval("MinDistNo")%>-
                                                           <%#Eval("MaxDistNo")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



<%--                                                    <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" HeaderStyle-Width="3%"
                                            ShowDeleteButton="True" DeleteImageUrl="~/Images/DeleteN.png"
                                            EditImageUrl="~/Images/DeleteN.png"
                                            NewImageUrl="~/Images/DeleteN.png" SelectImageUrl="~/Images/DeleteN.png">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                        <td width="15%">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td width="3%">&nbsp;</td>
                        </tr>
                          <tr>
                            <td width="3%"></td>
                               <td width="10%"></td>
                            
                           
                             
                            <td width="23%">
                                <label style="color:red; font-size:large;  ">
                                    Share Balance :</label>
                           

                                <asp:Label ID="LblShareBal" runat="server" ></asp:Label>
                              </td>
                          
                             <td width="2%">&nbsp;</td>
                            <td width="23%">
                                </td>
                            <td width="23%">
                                  &nbsp;</td>
                            <td width="3%">&nbsp;</td>
                        </tr>
            </div>
            <tr>
                <td width="3%">&nbsp;</td>
                <td width="23%">&nbsp;<legend>TRANSFEREE DETAILS</legend> </td>
                <td width="23%">&nbsp;</td>
                <td width="2%">&nbsp; </td>
                <td width="23%">&nbsp;</td>
                <td width="23%">&nbsp;   </td>
                <td width="3%">&nbsp;</td>
            </tr>
        
              
                <tr>
                    <td width="3%"></td>
                    <td width="23%">
                        <label>
                        Customer ID</label></td>
                    <td width="23%">
                        <asp:TextBox ID="TxtTranCustId" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" meta:resourcekey="TxtCustIDResource1" Width="80%"></asp:TextBox>
                        <asp:ImageButton ID="Imgcust" runat="server" Height="24px" ImageUrl="~/images/Search.png" Width="23px" />
                    </td>
                    <td width="2%">&nbsp;</td>
                    <td width="23%">
                        <label>
                        Customer Name</label></td>
                    <td width="23%">
                        <asp:TextBox ID="TxtTranCustName" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" Width="80%"></asp:TextBox>
                    </td>
                    <td width="3%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="3%">&nbsp;</td>
                    <td width="23%">
                        <label>
                        Address</label></td>
                    <td width="23%">
                        <asp:TextBox ID="TxtTranCustAddr" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" TextMode="MultiLine" Width="80%"></asp:TextBox>
                    </td>
                     <td width="2%">&nbsp;</td>
                  
                    <td width="23%">
                          <asp:CheckBox ID="Chk_Operators" runat="server" Text="Operators"  AutoPostBack="true"/>
                    </td>
                    <td width="3%">&nbsp;&nbsp;</td>
                </tr>
            <div runat="server" id="DivOperators" visible="false">
     
             <tr>
                    <td width="3%"></td>
                    <td width="23%">
                        <label>
                       Mode of operators</label></td>
                    <td width="23%">
                      <asp:DropDownList ID="CmbOpMode" runat="server" Width="80%" 
                AutoPostBack="True" TabIndex="1">
            </asp:DropDownList>
                    </td>
                    <td width="2%">&nbsp;</td>
                    <td width="23%">
                        <label>
                        No of Operators</label></td>
                    <td width="23%">
                        <asp:TextBox ID="TxtNoofoperators" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" Width="80%"></asp:TextBox>
                         
                    </td>
                    <td width="3%">&nbsp;</td>
                </tr>
                  <tr>
                    <td width="3%"></td>
                    <td width="23%">
                        <label>
                     Joint  Customer ID</label></td>
                    <td width="23%">
                      <asp:TextBox ID="Txt_JointCustId" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" meta:resourcekey="TxtCustIDResource1" Width="80%"></asp:TextBox>
                        <asp:ImageButton ID="ImgJointcust" runat="server" Height="24px" ImageUrl="~/images/Search.png" Width="23px" />
              

                    </td>
                    <td width="2%">&nbsp;</td>
                    <td width="23%">
                        <label>
                      Joint Customer Name</label></td>
                    <td width="23%">
                        <asp:TextBox ID="Txt_JointCustName" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" Width="80%"></asp:TextBox>
                        
                    </td>
                    <td width="3%">&nbsp;</td>
                </tr>
                  <tr>
                    <td width="3%">&nbsp;</td>
                    <td width="23%">
                        <label>
                      Joint  customer Address</label></td>
                    <td width="23%">
                        <asp:TextBox ID="Txt_JointCustAddr" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" TextMode="MultiLine" Width="75%"></asp:TextBox>
                   <asp:ImageButton ID="CmdAddOperator" runat="server" Height="15px" ImageUrl="~/Images/AddBtn.png" TabIndex="7" />
                          </td>
                    <td width="2%">&nbsp;</td>
                  
                 
                    <td width="3%">&nbsp;&nbsp;</td>
                </tr>
         </div>
                <%--     <tr>
                <td width="3%">&nbsp;</td>
                <td width="23%">&nbsp;</td>
                <td width="23%">&nbsp;</td>
                <td width="2%">&nbsp;</td>
                <td width="23%">&nbsp;</td>
                <td width="23%">&nbsp;</td>
                <td width="3%">&nbsp;</td>
            </tr>


            <tr>
                <td width="3%">&nbsp;</td>
                <td width="23%">&nbsp;</td>
                <td width="23%">&nbsp;</td>
                <td width="2%">&nbsp;</td>
                <td width="23%"></td>
                <td width="23%">&nbsp;</td>
                <td width="3%">&nbsp;</td>
            </tr>--%>
                </table>

            </asp:Panel>
            <asp:Panel ID="PnlAuthorise2" runat="server">
            
                <table class="style1" width="100%">
                    <div id="DivTranDetails" runat="server" visible="true">
                        <tr>
                            <td width="3%">&nbsp;</td>
                            <td width="23%">&nbsp;</td>
                            <td width="23%">&nbsp;</td>
                            <td width="2%">&nbsp;</td>
                            <td width="23%">&nbsp;</td>
                            <td width="23%">&nbsp;</td>
                            <td width="3%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="3%">&nbsp;</td>
                            <td colspan="5">
                                <panel ID="PnlTranDetails" runat="server" visible="false">
                                    <table class="style1" width="100%">
                                        <tr>
                                            <td width="15%">&nbsp;</td>
                                            <td width="70%">
                                                <asp:GridView ID="GridTranDetails" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew" TabIndex="35" Width="70%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkView" runat="server" onclick="CheckOne(this)"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Acc_id" HeaderText="Acc_Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                        <asp:BoundField DataField="ACC_No" HeaderText="Acc No" />
                                                        <asp:BoundField DataField="Share_Count" HeaderText="Share Count" />
                                                        <asp:BoundField DataField="Date_Allot" HeaderText="Open Date" />
                                                        <%--   <asp:BoundField DataField="Outstand" HeaderText="Outstand"></asp:BoundField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td width="3%">&nbsp;</td>
                                        </tr>
                                    </table>
                                </panel> 
                                   <panel ID="PnlJointOperators" runat="server" >
                                    <table class="style1" width="100%">
                                        <tr>
                                            <td width="15%">&nbsp;</td>
                                            <td width="70%">
                                                <asp:GridView ID="GridJointTranDetails" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew" TabIndex="35" Width="70%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Cust_Id" HeaderText="Cust_Id"  />
                                                        <asp:BoundField DataField="Cust_name" HeaderText="Cust_name" />
                                                     
                                                        <asp:BoundField DataField="Cust_Addr" HeaderText="Cust_Addr"  />
                                                       

                                                        <asp:CommandField DeleteImageUrl="~/Images/DeleteN.png" DeleteText="&lt;img src='./images/del.jpg'&gt;" EditImageUrl="~/Images/DeleteN.png" HeaderStyle-Width="3%" NewImageUrl="~/Images/DeleteN.png" SelectImageUrl="~/Images/DeleteN.png" ShowDeleteButton="True">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:CommandField>



                                                        <%--   <asp:BoundField DataField="Outstand" HeaderText="Outstand"></asp:BoundField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td width="3%">&nbsp;</td>
                                        </tr>
                                    </table>
                                </panel> 
                                <tr>
                                    <td width="3%"></td>
                                    <td width="23%">
                                        <label>
                                        Seial No
                                        </label>
                                    </td>
                                    <td width="23%">
                                        <asp:TextBox ID="TxtBatch_No" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" Width="80%"></asp:TextBox>
                                    </td>
                                    <td width="2%">&nbsp;</td>
                                    <td width="23%">
                                        <label>
                                        Date of Transfer</label></td>
                                    <td width="23%">
                                        <asp:TextBox ID="TxtDate_Tran" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" Width="80%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtDate_Tran">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="3%"></td>
                                    <td width="23%">
                                        <label>
                                        Distinctive No From
                                        </label>
                                    </td>
                                    <td width="23%">
                                        <asp:TextBox ID="TxtTranDisFrom" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" Width="30%" onChange="validateFromToNO();"></asp:TextBox>
                                        <label>
                                        To</label>
                                        <asp:TextBox ID="TxtTranDisTo" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" onblur="return FindShare();" onChange="validateFromToNO();" TabIndex="5" Width="30%"></asp:TextBox>
                                    </td>
                                    <td width="2%">&nbsp;</td>
                                    <td width="23%">
                                        <label>
                                        No of Share Transfer
                                        </label>
                                    </td>
                                    <td width="23%">
                                        <asp:TextBox ID="TxtTranCustShare" runat="server" CssClass="twitterStyleTextbox" Enabled="false" meta:resourcekey="TxtNameResource1" TabIndex="5" Width="75%"></asp:TextBox>
                                        <asp:ImageButton ID="CmdAdd" runat="server" Height="15px" ImageUrl="~/Images/AddBtn.png" TabIndex="7" />
                                    </td>
                                    <td width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="3%">&nbsp;</td>
                                    <td align="center" colspan="5">&nbsp;</td>
                                    <td width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="3%">&nbsp;</td>
                                    <td colspan="5">
                                        <table class="style1" width="100%">
                                            <tr>
                                                <td width="15%">&nbsp;</td>
                                                <td width="70%">
                                                    <asp:GridView ID="GridTrans" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" TabIndex="30" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="SeriesNo" HeaderText="Batch_No" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                            <asp:BoundField DataField="CustId" HeaderText="Cust Id" />
                                                            <asp:BoundField DataField="CustName" HeaderText="Name Of Transferee" />
                                                            <asp:BoundField DataField="FolioNo" HeaderText="Foilo No" />
                                                            <asp:BoundField DataField="Share" HeaderText="No of Share before Transfer" />
                                                            <%--   <asp:BoundField DataField="DistNo" HeaderText="Distinctive number"></asp:BoundField>--%>
                                                            <asp:BoundField DataField="TranShare" HeaderText="No of Share  Transfer" />
                                                            <asp:BoundField DataField="DistNo" HeaderText="Distinctive No" />
                                                            <asp:BoundField DataField="DistFrom" HeaderStyle-CssClass="hiddencol" HeaderText="Distinctive No" ItemStyle-CssClass="hiddencol" />
                                                            <asp:BoundField DataField="DistTo" HeaderStyle-CssClass="hiddencol" HeaderText="Distinctive No" ItemStyle-CssClass="hiddencol" />
                                                              <asp:BoundField DataField="Transfre"  HeaderText="Transferee AccNo"  /> 
                                                              <asp:BoundField DataField="ACC_No" HeaderText="Acc_no" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  />
                                                            <asp:BoundField DataField="ACC_ID" HeaderStyle-CssClass="hiddencol" HeaderText="Acc_id" ItemStyle-CssClass="hiddencol" />
                                                            <asp:BoundField DataField="TransfereeAcc_ID" HeaderStyle-CssClass="hiddencol" HeaderText="TransfereeAcc_id" ItemStyle-CssClass="hiddencol" />
                                                             <asp:BoundField DataField="Mode_of_opn" HeaderText="Mode_of_opn" />                                                     
                                                        <asp:BoundField DataField="Mode_of_opn_ID" HeaderText="Mode_of_opn_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                        <asp:BoundField DataField="Operators" HeaderText="Operators" />
                                                        <asp:BoundField DataField="Operators_Id" HeaderText="Operators_Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                        <asp:BoundField DataField="NoOf_Oper" HeaderText="NoOf_Oper" />
                                                     
                                                            <asp:CommandField DeleteImageUrl="~/Images/DeleteN.png" DeleteText="&lt;img src='./images/del.jpg'&gt;" EditImageUrl="~/Images/DeleteN.png" HeaderStyle-Width="3%" NewImageUrl="~/Images/DeleteN.png" SelectImageUrl="~/Images/DeleteN.png" ShowDeleteButton="True">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                                <td width="15%">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <asp:Label ID="Label1" runat="server" ></asp:Label>
                                    <td width="3%">&nbsp;</td>
                                    <td colspan="5">&nbsp;</td>

                                    <td width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="3%">&nbsp;</td>
                                    <td align="center" colspan="5">
                                        <asp:Label ID="LblTran" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="11pt" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td width="3%">&nbsp;</td>
                                </tr>
                            </td>
                        </tr>
                    </div>
                </table>
                </asp:Panel>
                <%--            <table class="style1" width="100%">
                <tr>
                    <td width="3%">&nbsp;</td>
                    <td width="17%">&nbsp;</td>
                    <td width="75%">&nbsp;</td>
                    <td width="5%">&nbsp;</td>
                </tr>
               
             
                <tr>
                    <td width="3%">&nbsp;</td>
                    <td width="17%">&nbsp;</td>
                    <td width="75%">&nbsp;</td>
                    <td width="5%">&nbsp;</td>
                </tr>
            </table>--%>
                <table class="style1" width="100%">
                    <tr>
                        <td width="25%">
                            <asp:HiddenField ID="HdnAdvAccId" runat="server" />
                        </td>
                        <td align="center" colspan="2" style="width: 50%" width="25%">
                            <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return validateCheckBoxes();" />
                            <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                            <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />
                        </td>
                        <td width="25%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">&nbsp;</td>
                        <td width="25%">&nbsp;</td>
                        <td width="25%">&nbsp;</td>
                        <td width="25%">&nbsp;</td>
                    </tr>
                </table>
                </div>
            </caption>

    
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

