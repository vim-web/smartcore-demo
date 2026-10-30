<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChequeIssueMaster.aspx.vb" Inherits="SMARTCORE_UI.ChequeIssueMaster" 
    title="CHEQUE ISSUE MASTER" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div  class="gradientBoxesWithOuterShadows" style= "margin-left:25%; margin-right:25%;margin-top:5%;">
<legend>CHEQUE ISSUE MASTER</legend>   
<TABLE WIDTH = "100%" >
	<TR>
		<TD WIDTH = "20%"> </TD>
		<TD WIDTH = "30%"> </TD>
		<TD WIDTH = "30%"> </TD>
		<TD WIDTH = "2%"> </TD>
		<TD WIDTH = "17%">  </TD>
 	</TR>
 		<TR>
		<TD> </TD>
		<TD> 
            <asp:Label ID="Label1" runat="server" Text="Branch" Width ="100%"></asp:Label>
		</TD>
		<TD> 
		<asp:DropDownList ID="DDlBranch" runat="server" Width ="100%">
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
		<asp:DropDownList ID="DDlScheme" runat="server" Width="100%" AutoPostBack="True">
             </asp:DropDownList>
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
            <asp:TextBox ID="TxtAccNo" runat="server" Width ="100%" AutoPostBack="True"></asp:TextBox></TD>
		<TD> 
		<asp:ImageButton ID="ImgSrch" runat="server" Height="20px" ImageUrl="~/images/Search.png" Width="20px" />                
		</TD>
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
            <asp:TextBox ID="TxtName" runat="server" Width="100%"></asp:TextBox></TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	 	
 	 	
 	 	
 		<TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label4" runat="server" Text="Address" Width ="100%"></asp:Label>
		</TD>
		<TD> 
		    <asp:TextBox ID="TxtAddress" runat="server" Width="100%" Height="58px" TextMode="MultiLine"></asp:TextBox>
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
            <asp:TextBox ID="TxtBalance" runat="server" Width ="100%"></asp:TextBox>
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
 	 	</TR>
 		
 	 	</TR>
 		<TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label6" runat="server" Text="From Cheque No" Width ="100%"></asp:Label>
		</TD>
		<TD> 
            <asp:TextBox ID="TxtFromChqno" runat="server" Width ="100%" AutoPostBack="True"></asp:TextBox>
		</TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	 	</TR>
 	 <TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label7" runat="server" Text="To Cheque No" Width ="100%"></asp:Label>
		</TD>
		<TD>
		<asp:TextBox ID="TxtToChqNo" runat="server" Width ="100%" ReadOnly="True"></asp:TextBox>
		 </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
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
            <asp:GridView ID="GrViewCheque" runat="server" Width ="100%" PageSize="5" CssClass="grid-view">
            </asp:GridView>
		</TD>
		<TD>                 
		     &nbsp;</TD>
 	 </TR>
 	  	 <TR>
		<TD>
            <input id="HddAccID" type="hidden"  runat ="server" /> 
               <input id="HddSch" type="hidden"  runat ="server" /> 
                  <input id="Hidden3" type="hidden"  runat ="server" /> 
            
            
            </TD>
		<TD> </TD>
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
		<TD> </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	  	 <TR>
		<TD> </TD>
		<TD> 
    <asp:HiddenField ID="SchSection" runat="server" />
    <asp:HiddenField ID="ChqGroup" runat="server" />
             </TD>
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
