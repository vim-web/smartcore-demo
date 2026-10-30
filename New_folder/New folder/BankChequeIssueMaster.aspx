<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BankChequeIssueMaster.aspx.vb" Inherits="SMARTCORE_UI.BankChequeIssueMaster" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div  class="gradientBoxesWithOuterShadows" style= "margin-left:25%; margin-right:25%;margin-top:5%;">
<legend>BANK ACCOUNT CHEQUE DETAILS</legend>   
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
		<TD> &nbsp;</TD>
		<TD> 
		<asp:Label ID="Label3" runat="server" Text="Account Number" Width ="100%"></asp:Label>
		</TD>
		<TD>
		<asp:DropDownList ID="Cmb_AccNo" runat="server" Width="100%" AutoPostBack="True">
             </asp:DropDownList>
		 </TD>
		<TD> &nbsp;</TD>
		<TD>  &nbsp;</TD>
 	 </TR>
 	 
 		<TR>
		<TD> </TD>
		<TD> 
		<asp:Label ID="Label2" runat="server" Text="Bank" Width ="100%"></asp:Label>
		</TD>
		<TD>
            <asp:TextBox ID="TxtBankName" runat="server" Width="100%"></asp:TextBox>
		 </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	 	</TR>
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
		<asp:TextBox ID="TxtToChqNo" runat="server" Width ="100%"></asp:TextBox>
		 </TD>
		<TD> </TD>
		<TD>  </TD>
 	 </TR>
 	  	 <TR>
		<TD> </TD>
		<TD colspan="3"> 
		    &nbsp;</TD>
		<TD>  </TD>
 	 </TR>
 	  	 <TR>
		<TD>
            </TD>
		<TD colspan="3"> 
            &nbsp;</TD>
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
