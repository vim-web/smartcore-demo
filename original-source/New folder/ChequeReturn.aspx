<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChequeReturn.aspx.vb" Inherits="SMARTCORE_UI.ChequeReturn" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div  class="gradientBoxesWithOuterShadows" style= "margin-left:10%; margin-right:15%;margin-top:5%;">
<legend>CHEQUE RETURN MASTER</legend>   
<TABLE WIDTH = "100%" >
	<TR>
		<TD WIDTH = "20%"> </TD>
		<TD WIDTH = "25%"> </TD>
		<TD WIDTH = "35%"> </TD>
		<TD WIDTH = "2%"> </TD>
		<TD WIDTH = "17%">  </TD>
 	</TR>
 		<TR>
		<TD> </TD>
		<TD> 
            <asp:Label ID="Label1" runat="server" Text="Branch" Width ="100%"></asp:Label>
		</TD>
		<TD> 
		<asp:DropDownList ID="CmbBranch" runat="server" Width ="100%">
            </asp:DropDownList>
		</TD>
		<TD> 
            <input id="Hidden1" type="hidden" />
            </TD>
		<TD> 
             </TD>
 	 </TR>
 	 
 		<TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label2" runat="server" Text="Scheme" Width ="100%"></asp:Label>
		</TD>
		<TD>
		<table width="100%">
                        <tr>
                            <td width = "20%">
                                
                                <asp:TextBox ID="TxtSchCode" runat="server" AutoPostBack="True" 
                                    CssClass="twitterStyleTextbox" TabIndex="3" Width="100%"></asp:TextBox>
                                
                            </td>
                            <td width = "2%"></td>
                            <td width = "78%">
                                <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                                    TabIndex="3" Width="100%">
                                </asp:DropDownList>                            
                            </td>
                        </tr>
                    </table>
		 </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	 	</TR>
 		<TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label3" runat="server" Text="Account Number" Width ="100%"></asp:Label>
		</TD>
		<TD> 
            <table width="100%">
                        <tr>
                            <td width ="20%">                     
                               <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" TabIndex="4" Width="100%"  CssClass="twitterStyleTextbox"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width ="65%">
                                <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" TabIndex="4" Width="90%"  CssClass="twitterStyleTextbox"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width ="10%">
                                    <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                                        ImageUrl="~/images/Search.png" Width="23px" />
                            </td>
                        </tr>
                    </table>
                    
                    </TD>
		<TD> 
		    &nbsp;</TD>
		<TD> 
		     </TD>
 	 </TR>
 	 	</TR>
 	 	

 		<TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label9" runat="server" Text="Name" Width ="100%"></asp:Label>
		</TD>
		<TD> 
            <asp:TextBox ID="TxtName" runat="server" Width="100%" 
                CssClass="twitterStyleTextbox"></asp:TextBox></TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	 	
 	 	
 	 	
 		<TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label4" runat="server" Text="Address" Width ="100%"></asp:Label>
		   
		</TD>
		<TD> 
            <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" Width="100%" 
                CssClass="twitterStyleTextbox"></asp:TextBox></TD>
		<TD> 
             </TD>
		<TD>  </TD>
 	 </TR>
 	 <TR>
		<TD> </TD>
		<TD> 
            <asp:Label ID="Label5" runat="server" Text="Account Balance" Width ="100%"></asp:Label>
		</TD>
		<TD>
            <asp:TextBox ID="TxtBalance" runat="server" Width ="100%" 
                CssClass="twitterStyleTextbox"></asp:TextBox>
		 </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	 
 	 
 	  		<TR>
		<TD> </TD>
		<TD> 
		
		
            <asp:Label ID="Label8" runat="server" Text="Type of Books" Width ="100%"></asp:Label>
		</TD>
		<TD>
		<asp:DropDownList ID="DDlChqType" runat="server" Width ="100%" AutoPostBack="True">
		<asp:ListItem Selected="True">&lt;--SELECT--&gt;</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
                
            </asp:DropDownList>
		 </TD>
		<TD>
           <%-- <asp:ListBox ID="Lvw_NoOfBooks" runat="server" Width ="100%"></asp:ListBox>--%>
             </TD>
		<TD>  </TD>
 	 </TR>
<%-- 	 	</TR>
 		
 	 	</TR>--%>
<%-- 		<TR>
		<TD></TD>
		<TD>  Issued Books
		    </TD>
		<TD> 
            <asp:DropDownList ID="DropDownList1" runat="server" Width ="100%" AutoPostBack ="true" >
            </asp:DropDownList>
            </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>--%>
<%-- 		<TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label6" runat="server" Text="From Cheque No" Width ="100%"></asp:Label>
		</TD>
		<TD> 
            <asp:TextBox ID="TxtFromChqno" runat="server" Width ="100%" AutoPostBack="True" 
                CssClass="twitterStyleTextbox"></asp:TextBox>
		</TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>--%>
 	 	</TR>
<%-- 	 <TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label7" runat="server" Text="To Cheque No" Width ="100%"></asp:Label>
		</TD>
		<TD>
		<asp:TextBox ID="TxtToChqNo" runat="server" Width ="100%" ReadOnly="True" 
                CssClass="twitterStyleTextbox"></asp:TextBox>
		 </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>--%>
 	  	 <TR>
		<TD> </TD>
		<TD colspan="3"> 
		<asp:Label ID="LblStock" runat="server" Text="Avilable Cheque Books" Width ="100%" 
                Visible="False"></asp:Label>
		
		</TD>
		<TD>  </TD>
 	 </TR>
 	  	 <TR>
		<TD>
            </TD>
		<TD colspan="3"> 
		<asp:GridView ID="GrViewCheque" runat="server" AutoGenerateColumns="False" 
            TabIndex="8" Width="100%" AllowPaging="True" DataKeyNames="ACC_ID">
            <RowStyle Height="20px" />
            <Columns>
<%--                <asp:CommandField SelectText="SELECT" ShowSelectButton="True">
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" Width="5%" />
                </asp:CommandField>
                <asp:CheckBoxField >
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" Width="2%" />
                </asp:CheckBoxField>--%>
                <asp:CommandField ShowSelectButton="True" />
                
                
                <asp:BoundField  HeaderText="Cheque No From" DataField="Cm_Chq_No_From">
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" Width="3%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField  HeaderText="Cheque No To" DataField="Cm_Chq_No_To">
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" Width="32%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField  HeaderText="Issue Date" DataField="Cm_Chq_Date">
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" Width="8%" HorizontalAlign="Right" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField  DataFormatString="{0:F2}" 
                    HeaderText="Book Type" DataField="Cm_Chq_BookType">
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" HorizontalAlign="Right" Width="8%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ACC_ID" DataField="ACC_ID">
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" HorizontalAlign="Right" Width="8%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Scheme" DataField="Sch_Code" >
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" HorizontalAlign="Right" Width="8%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Acc_No" HeaderText="Acc_No" >
                
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" HorizontalAlign="Left" Width="5%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" HorizontalAlign="Left" />
                </asp:BoundField>
                    <asp:BoundField DataField="Cm_Chq_Status" HeaderText="Cm_Chq_Status"   >
                
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" HorizontalAlign="Left" Width="5%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" HorizontalAlign="Left" />
                </asp:BoundField>
                                   
                        
              
<%--                <asp:BoundField DataField="MakingTime" HeaderText="Branch" >
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                        Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" HorizontalAlign="Left" />
                </asp:BoundField>--%>
                <asp:BoundField DataField="Tran_No" HeaderText="Tran_No" Visible="False" />
            </Columns>

            <AlternatingRowStyle BackColor="#F5F4FF" />
        </asp:GridView>
		
		   </TD>
		<TD>                 
		     &nbsp;</TD>
 	 </TR>
 	  	 <TR>
		<TD>
            &nbsp;</TD>
		<TD> 
<asp:Label ID="Label6" runat="server" Text="Select Stop Payment/Return" Width ="100%"></asp:Label>
		    </TD>
		<TD>                 
		
<asp:DropDownList ID="DDlStstusType" runat="server" Width ="100%">
		<%--<asp:ListItem Selected="True">&lt;--SELECT--&gt;</asp:ListItem>--%>
                <asp:ListItem>Stop Payment</asp:ListItem>
                <asp:ListItem>Stop Payment Revoke</asp:ListItem>
                <asp:ListItem>Returned</asp:ListItem>
 
                
            </asp:DropDownList>
		
        </TD>
		<TD>                 
		    &nbsp;</TD>
		<TD>                 
		     &nbsp;</TD>
 	 </TR>
 	  	 <TR>
		<TD>
            &nbsp;</TD>
		<TD> 
		<asp:Label ID="Label10" runat="server" Text="Change Status " Width ="100%"></asp:Label>
		</TD>
		<TD>                 
            <%--                <asp:CommandField SelectText="SELECT" ShowSelectButton="True">
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" Width="5%" />
                </asp:CommandField>
                <asp:CheckBoxField >
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" Width="2%" />
                </asp:CheckBoxField>--%>            
             <%--                <asp:BoundField DataField="MakingTime" HeaderText="Branch" >
                    <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                        Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Small" HorizontalAlign="Left" />
                </asp:BoundField>--%>
            
            <asp:CheckBoxList ID="ChkStChequeNo" runat="server" RepeatColumns="5" 
                RepeatDirection="Horizontal">
            </asp:CheckBoxList>
		</TD>
		<TD>                 
		    &nbsp;</TD>
		<TD>                 
		     &nbsp;</TD>
 	 </TR>
 	  	 <TR>
		<TD>
            <input id="HddAccID" type="hidden"  runat ="server" /> 
              <input id="HddStChequeNo" type="hidden"  runat ="server" /> 
               <input id="HddSch" type="hidden"  runat ="server" /> 
                  <input id="Hidden3" type="hidden"  runat ="server" /> 
            
            
            </TD>
		<TD> 
            <%--<asp:Button ID="Button2" runat="server" Text="Button" />--%>
             </TD>
		<TD>                 <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="56" /></TD>
		<TD>                 
		<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" TabIndex="57" />
		</TD>
		<TD>                 
		     </TD>
 	 </TR>
 	  	 <TR>
		<TD> </TD>
		<TD> </TD>
		<TD> 
            
		
		</TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	  	 <TR>
		<TD> </TD>
		<TD> </TD>
		<TD> </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	 <TR>
		<TD> </TD>
		<TD> </TD>
		<TD> </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	
 	
 	
 	
</TABLE>
</div>

</asp:Content>
