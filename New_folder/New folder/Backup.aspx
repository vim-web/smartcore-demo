<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Backup.aspx.vb" Inherits="SMARTCORE_UI.Backup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
     <script type='text/javascript'>

         function ValidateSave() {

             return true;
         }

</script>

    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>   
<head id="Head1" runat="server">
    <title>Dackup Data</title>
</head>
<body>
    <form id="form1" runat="server">


   <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:15%;" >  
<legend>Download Data</legend>  
<br />
<div id ="DivFxDetails" runat ="server"  >

        <table width="100%">
            <tr>
                <td width="2%" ></td>
                <td width="40%"></td>
                <td width="20%"></td>
                <td width="18%"></td>
                <td width="18%"></td>
                <td width="2%"></td>                
            </tr>

            
                           <tr>
                <td width="2%" ></td>
                <td  colspan ="4" width="96%">
                
                <table width ="100%">
                <tr>
                <td Width ="12%">
                    &nbsp;</td>
                <td Width ="12%">
                    <asp:DropDownList ID="Cmb_FxBranch" runat="server" Width="95%" Visible="False">
                    </asp:DropDownList>
                    </td>
                <td Width ="12%">
                    &nbsp;</td>
                <td Width ="12%">
                    <asp:TextBox ID="Txt_FrDate" runat="server" BackColor="White" 
                         OnChange="validatedate(this);" TabIndex="4" 
                        Width="90%" Visible="False"></asp:TextBox>

                    </td>
                <td Width ="12%">
                    &nbsp;</td>
                <td Width ="12%">
                    <asp:TextBox ID="Txt_ToDate" runat="server" BackColor="White" 
                         OnChange="validatedate(this);" TabIndex="5" 
                        Width="90%" Visible="False"></asp:TextBox>

                    </asp:CalendarExtender>
                    </td>
                <td Width ="12%">
                    &nbsp;</td>
                <td Width ="16%">
<%--        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer runat="server" id="UpdateTimer" interval="1000" ontick="UpdateTimer_Tick" />
         <asp:UpdatePanel runat="server" id="TimedPanel" updatemode="Conditional">
            <ContentTemplate>
                <asp:Label ID="DateStampLabel" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
            </Triggers>
        </asp:UpdatePanel>--%>
                    </td>
                </tr>
                
                
                </table>

                
                
                 <td width="2%" ></td>
                 
               </tr>
               
<%--                           <tr>
                <td width="2%" ></td>
                <td  colspan ="4" width="96%">
                


                
                
                 <td width="2%" ></td>
                 
               </tr>--%>
              <tr>
                <td width="2%" ></td>
                <td width="40%">
                
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Download.jpg"  Height="50%" />
                </td>
                <td width="20%">
                    <asp:HiddenField ID="HdnCounter" runat="server" />
                    <asp:HiddenField ID="HdnCounter1" runat="server" />
                  </td>
                <td width="18%">
                   
                    
   <table width ="100%">
                <tr>
                <td width="85%">
                    <asp:Button ID="SHOW" runat="server" Text="BACKUP" Width="95%" />
                                   </td>
                <td width="15%">
   <div id ="HomeIcon">         
         <asp:ImageButton ID="ImageButton1" runat="server" 
             ImageUrl="~/Images/Home.png" />
    </div>
                
                </td>
                
                </tr>
                
                </table>
                    
                    </td>
                    
                <td width="18%">
                    &nbsp;</td>
                <td width="2%"></td>                
            </tr>
            

                        <tr>
                <td width="2%" ></td>
                <td  colspan ="4" width="96%">
<div id ="Div1" runat ="server"  class="rounded_corners" >
           
                <asp:GridView ID="DgvForex" runat="server" AutoGenerateColumns="False" 
                              CellPadding="2" DataKeyNames="FilePath" ForeColor="#333333" Width ="100%" BorderStyle="None" >
                              <Columns>

                                  <asp:BoundField DataField="FilePath" HeaderText="Forex No" 
                                      SortExpression="FilePath"   Visible ="false"  />
                                  <asp:TemplateField HeaderText="Sl.No" SortExpression="SLNO">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtFx_No" runat="server" BorderStyle="none" ForeColor="Blue" Width ="97%"
                                              ReadOnly="true" Text='<%# Bind("SLNO") %>' Font-Bold ="true"  >
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                  
                                  <asp:TemplateField HeaderText="Name" SortExpression="FileName">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtTrDate" runat="server" BorderStyle="none" ForeColor="Blue" Width ="97%"
                                              ReadOnly="true" Text='<%# Bind("FileName") %>'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
             
                                  
                                  <asp:TemplateField HeaderText="Download" SortExpression="FilePath">
                                      <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  
    <%--                              
                                  
                                  <asp:TemplateField HeaderText="Amount" SortExpression="ApprovedAmt">
                                      <ItemTemplate>
                                          <asp:TextBox ID="Txt_Amount" runat="server" BorderStyle="none" 
                                              BorderWidth="0px" ForeColor="Blue" ReadOnly="true" 
                                              Text='<%# Bind("ApprovedAmt") %>'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  --%>
                                  
   
                                  
                                  
                                    

                                  
                              </Columns>
                          </asp:GridView>
 </div>                
                
                </td>
                <td width="2%"></td>                
            </tr>
              <tr>
                <td width="2%" ></td>
                <td width="40%"></td>
                <td width="20%"></td>
                <td width="18%"></td>
                <td width="18%">
                    &nbsp;</td>
                <td width="2%"></td>                
            </tr>
                          <tr>
                <td width="2%" ></td>
                <td width="40%">
                    &nbsp;</td>
                <td width="20%"></td>
                <td width="18%"></td>
                <td width="18%">
                    &nbsp;</td>
                <td width="2%"></td>                
            </tr
                          <tr>
                <td width="2%" ></td>
                <td width="40%"></td>
                <td width="20%"></td>
                <td width="18%"></td>
                <td width="18%">
                    &nbsp;</td>
                <td width="2%"></td>                
            </tr
           </table>
         
</div>

        </div>
    </form>
</body>
</html>
