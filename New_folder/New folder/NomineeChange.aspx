<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="NomineeChange.aspx.vb" Inherits="SMARTCORE_UI.NamineeChange" 
    title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type='text/javascript'>
    function ValidateSave() {


        var Ctl = document.getElementById('<%=Txt_NomAdd.ClientID%>');
        if (Ctl.value == "") {
            alert('Address Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        return true 
    }

</script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    



    
    
    
        <ContentTemplate>
            <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
            <br />
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
              <div class="gradientBoxesWithOuterShadows" style="width: 65%; margin-left: 18%; margin-right: 10%;">
<legend> CHANGE NOMINEE </legend>   
<div id="DivAccDetails">
     
   <table width="100%">
      <tr>
        <td width="12%"> </td>
        <td width="22%"> 
        
        </td>
        <td width="25%"> </td>
        <td width="2%"> </td>
        <td width="22%"> </td>
        <td width="15%"> </td>
        <td width="2%"> </td>
      </tr>
          <tr>
              <td width="12%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="15%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
      </tr>
      <tr>
          <td width="12%">
              &nbsp;</td>
          <td width="22%">
              &nbsp;</td>
          <td width="25%">
              &nbsp;</td>
          <td width="2%">
              &nbsp;</td>
          <td width="22%">
              &nbsp;</td>
          <td width="15%">
              &nbsp;</td>
          <td width="2%">
              &nbsp;</td>
      </tr>
          <tr>
           
           <td></td>
                <td >
                    <asp:Label ID="LblBranch" runat="server" Text="Branch"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                        TabIndex="2" Width="100%">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td></td>
            </tr>
           
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblScheme" runat="server" Font-Size="Medium" ForeColor="#6666FF" 
                        Text="Scheme" Width="100%"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="CmbScheme" runat="server" TabIndex="1" Width="100%">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
      </tr>
           
            <tr>
            
            
            
    
            <td></td>
                <td >
                    <asp:Label ID="LblAccountNo" runat="server" Text="Account No"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <table width="100%">
                        <tr>
                            <td width="20%">
                                <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" 
                                    CssClass="twitterStyleTextbox" TabIndex="4" Width="100%"></asp:TextBox>
                            </td>
                            <td width="2%">
                            </td>
                            <td width="67%">
                                <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" 
                                    CssClass="twitterStyleTextbox" TabIndex="4" Width="99%" 
                                    AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td width="2%">
                            </td>
                            <td align="right" width="8%">
                                <asp:ImageButton ID="ImgSrch0" runat="server" Height="24px" 
                                    ImageUrl="~/images/Search.png" Width="26px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td  >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                  <td></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                                       
      </tr>
      </table>  
     
</div>              
<div id="DivGridNominee" runat ="server" visible="false"> 
<legend> PRESENT NOMINEE DETAILS</legend>

      
      <table width ="100%">
           <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="96%">
                  &nbsp;<td width="2%">
                      &nbsp;</td>
              </td>
          </tr>
      
      <tr>
        <td width="2%"> </td>
        <td width="96%"> 

            <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Nm_Nom_No" PageSize="2" TabIndex="17" Width="100%">
                <Columns>
                     <asp:BoundField DataField="Nm_Nom_No" HeaderText="Nm_Nom_No"   Visible="False" />
                     <asp:BoundField DataField="Br_Code" HeaderText="Br_Code"   Visible="False" />
                     <asp:BoundField DataField="Sch_Code" HeaderText="Sch_Code"   Visible="False" />
                     
                    <asp:BoundField DataField="Acc_No" HeaderText="Account No">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                     
                     
                    <asp:BoundField DataField="Nm_Name" HeaderText="Name">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>

                       <asp:BoundField DataField="Nm_Address" HeaderText="Address">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>   
                    
                     <asp:BoundField DataField="Pkc_Description" HeaderText="Relation" Visible ="true" >
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>     

                     <asp:BoundField DataField="Nm_Relation" HeaderText="Nm_Relation" Visible ="false" >
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>                 
                    
                       <asp:BoundField DataField="Nm_Gender" HeaderText="Nm_Gender" Visible ="false" >
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>

                       <asp:BoundField DataField="Nm_Phone" HeaderText="Phone">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>

                    
                     <asp:BoundField DataField="Nm_Occupation" HeaderText="Nm_Occupation"   Visible="false" />
                     <asp:BoundField DataField="Nm_DOB" HeaderText="Nm_DOB"   Visible="false" />
                     <asp:BoundField DataField="Nm_Age" HeaderText="Nm_Age"   Visible="false" />
                     <asp:BoundField DataField="Nm_Minor" HeaderText="Nm_Minor"   Visible="false" />
                     <asp:BoundField DataField="Nm_GName" HeaderText="Nm_GName"   Visible="false" />
                     <asp:BoundField DataField="Nm_Grelation" HeaderText="Nm_Grelation"   Visible="false" />
                     <asp:BoundField DataField="Nm_Narration" HeaderText="Nm_Narration"   Visible="false" />
                     <asp:BoundField DataField="Nm_GAddress" HeaderText="Nm_GAddress"   Visible="false" />

                     
                       <asp:BoundField DataField="Nm_EffectDate" HeaderText="Effect Date"   >
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    
                     <asp:BoundField DataField="Nm_Shr_Percent" HeaderText="Nm_Shr_Percent"   Visible="false" />
                     <asp:BoundField DataField="Log_Id" HeaderText="Log_Id"   Visible="false" />
                     
    

                </Columns>
                <AlternatingRowStyle BackColor="#F5F4FF" />
            </asp:GridView>

        <td width="2%"> </td>
      </tr>
      
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="96%">
                  &nbsp;<td width="2%">
                      &nbsp;</td>
              </td>
          </tr>
      
      </table>
      
      
</div> 
      
  <div id="DivNewNominee" runat ="server" visible ="false" > 
<legend> ENTER NEW NOMINEE DETAILS</legend>   
<div id ="NomDiv" class="gradientBoxesWithOuterShadows" runat="server" visible="true" style="margin-left:2%;margin-right:2%;margin-bottom:2%;margin-top:2%;">
     
        <table width = "100%">
         <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
	        <tr>
		        <td width = "2%" > </td>
		        <td width = "15%"> <label>Nominee Name</label></td>
		        <td width = "49%"> <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomName" runat="server"  Width="99%" TabIndex="30"></asp:TextBox></td>
		        <td width = "2%" > </td>
		        <td width = "15%"> <label>Relation</label></td>
		        <td width = "15%"> 
                    <asp:DropDownList ID="CmbNomRel" runat="server" Width="100%" TabIndex="31">
                    </asp:DropDownList>
                </td>
		        <td width = "2%" > </td>
	        </tr>	
            <tr>
                <td></td>
                <td><label>Nominee Address</label></td>
                <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomAdd" runat="server"  Width="99%" TabIndex="32"></asp:TextBox></td>
		        <td></td>
		        <td><label>Phone No</label></td>
		        <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomPh" runat="server"  Width="96%" TabIndex="33"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
		        <td></td>
		        <td></td>
		        <td>
                    <asp:HiddenField ID="HdnAccID" runat="server" />
                </td>
                <td></td>
            </tr>
           
        </table>
</div>    

 
<%--      <table width ="100%">
      <tr>
        <td width="2%"> </td>
        <td width="22%"> 
        
        </td>
        <td width="25%"> </td>
        <td width="2%"> </td>
        <td width="22%"> </td>
        <td width="25%"> </td>
        <td width="2%"> </td>
      </tr>
        
      
      
      
      
      
      
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="22%">
                    <asp:Label ID="LblName" runat="server" Font-Size="Medium" ForeColor="#6666FF" 
                        Text="Name" Width="100%"></asp:Label>
                </td>
                <td width="25%">
                    <asp:TextBox ID="TxtName" runat="server" TabIndex="4" Width="100%"></asp:TextBox>
                </td>
                <td width="2%">
                    &nbsp;</td>
                <td width="22%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
          </tr>
      
      
      
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  <asp:Label ID="LblAddress" runat="server" Font-Size="Medium" 
                      ForeColor="#6666FF" Text="Address" Width="100%"></asp:Label>
              </td>
              <td width="25%">
                  <asp:TextBox ID="TxtAddress" runat="server" Height="45px" TabIndex="5" 
                      TextMode="MultiLine" Width="100%"></asp:TextBox>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  <asp:Label ID="LblDateOfBirth" runat="server" Font-Size="Medium" 
                      ForeColor="#6666FF" Text="Date Of Birth" Width="100%"></asp:Label>
              </td>
              <td width="25%">
                  <asp:TextBox ID="TxtDateOfBirth" runat="server" TabIndex="12" Width="100%"></asp:TextBox>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  <asp:Label ID="LblPhone" runat="server" Font-Size="Medium" ForeColor="#6666FF" 
                      Text="Phone" Width="100%"></asp:Label>
              </td>
              <td width="25%">
                  <asp:TextBox ID="TxtPhone" runat="server" TabIndex="7" Width="100%"></asp:TextBox>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  <asp:Label ID="LblRelation" runat="server" Font-Size="Medium" 
                      ForeColor="#6666FF" Text="Relation" Width="100%"></asp:Label>
              </td>
              <td width="25%">
                  <asp:DropDownList ID="Ddl_Relation" runat="server" TabIndex="6" Width="100%">
                  </asp:DropDownList>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  <asp:Label ID="LblMinor" runat="server" Font-Size="Medium" ForeColor="#6666FF" 
                      Text="Minor" Width="100%"></asp:Label>
              </td>
              <td width="25%">
                  <asp:DropDownList ID="Ddl_Minor" runat="server" AutoPostBack="True" 
                      TabIndex="9" Width="100%">
                      <asp:ListItem Value="N">NO</asp:ListItem>
                      <asp:ListItem Value="Y">YES</asp:ListItem>
                  </asp:DropDownList>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
  
      
      
      </table>--%>
</div>     
      
   <div id="Miner" visible ="false"  runat ="server" >
      <table width="100%">
                <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  <asp:Label ID="LblGuardian0" runat="server" Font-Size="Medium" 
                      ForeColor="#6666FF" Text="Guardian" Width="100%"></asp:Label>
                    </td>
              <td width="25%">
                  <asp:TextBox ID="TxtGuardian" runat="server" Enabled="False" TabIndex="10" 
                      Width="100%">Nil</asp:TextBox>
                    </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  <asp:Label ID="LblGaurdianAddress" runat="server" Font-Size="Medium" 
                      ForeColor="#6666FF" Text="Guardian Address" Width="100%"></asp:Label>
              </td>
              <td width="25%">
                  <asp:TextBox ID="TxtGaurdianAddress" runat="server" Enabled="False" 
                      Height="45px" TabIndex="11" TextMode="MultiLine" Width="100%">Nil</asp:TextBox>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="22%">
                  &nbsp;</td>
              <td width="25%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
      
                <tr>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        <asp:Label ID="LblAge" runat="server" Font-Size="Medium" ForeColor="#6666FF" 
                            Text="Age" Width="100%"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TxtAge" runat="server" TabIndex="13" Width="100%"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        <asp:Label ID="LblRelation1" runat="server" Font-Size="Medium" 
                            ForeColor="#6666FF" Text="Relation" Width="100%"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:DropDownList ID="Ddl_GuardianRelation" runat="server" Enabled="False" 
                            TabIndex="14" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                </tr>
      
      </table>
   </div>   
      <table width ="100%">
         <tr>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Right" 
                            ImageUrl="~/images/Save.png" TabIndex="15" Width="89px" OnClientClick="return ValidateSave();"/>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Close.png" 
                            TabIndex="16" />
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                </tr>
                   <tr>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="22%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                </tr>
      </table>
      
      

         </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
