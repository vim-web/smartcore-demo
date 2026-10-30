<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DayOpenProcess.aspx.vb" Inherits="SMARTCORE_UI.DayOpenProcess" 
    title="Day Open Process" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
    
 
   
     
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            height: 28px;
        }
    </style>
   
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>

<div class="gradientBoxesWithOuterShadows" style ="width: 80%; margin-left:10%;margin-right:10%;margin-top:10%;font-family: Tahoma" >   
        <legend>Day Open Process</legend> 
         <table id="TblHO" visible="false" runat="server" width="100%">
        <tr>
        
        <td align="center">
            <b><label>Day Open Process Completed</label></b>
        </td></tr>
        <tr>
        
        <td align="center">
          
                <asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" PostBackUrl="~/SmartLogin.aspx"/>
        </td></tr>
        </table>            
    <table width="100%" id="TblDate" runat="server" visible="false">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="20%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
       <td width="2%">&nbsp;</td>
        <td width="10%">&nbsp;</td>
         <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
         <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>
          <tr>
           
           <td class="style2"></td>
           <td class="style2">
               &nbsp;</td>
           <td class="style2"></td>
           <td class="style2">
               <asp:Label ID="Label1" runat="server" Text="Transaction Date" 
                   ForeColor="#0033CC"></asp:Label>
            
              </td>
              <td class="style2"></td>
           <td class="style2" >
               <asp:TextBox ID="TextBox1" runat="server" Width="177px"></asp:TextBox>             
           </td>
           <td class="style2"></td>
            <td class="style2">
                &nbsp;</td>
           <td class="style2"></td>  <td class="style2">
              &nbsp;</td> <td class="style2"></td>
              
               
          
          </tr> 
           <tr>
           <td></td><td></td><td></td><td>
               </td><td></td><td>
               
               </td><td></td><td></td><td></td><td>
              
               </td><td></td>
           
          </tr> 
          
       
          <tr>
           <td></td><td></td><td></td>
               <td colspan ="3" align="center">
                   <asp:ImageButton ID="ImageButton1" runat="server" 
                       ImageUrl="~/Images/Dtls.png" />
              </td>
               <td></td>
               <td></td><td></td><td>
               
               </td><td></td>
           
          </tr> 
        </table>
        
        <table width="100%" id="TblGrid" runat="server">
        <tr> 
        <td width="2%">&nbsp;</td>
        <td class="style1">
            <asp:GridView ID="GridView1" runat="server" Width="100%" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="2px" 
                CellPadding="4" AutoGenerateColumns="False">
                <RowStyle BackColor="White" ForeColor="#003399" />
                <Columns>
                    <asp:BoundField DataField="DayEndProcess" HeaderText="DayOpenProcess" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Data" HeaderText="Values" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status">
                        <ItemStyle Font-Bold="False" Font-Names="Times New Roman" ForeColor="#009999" 
                            HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProcStatus" HeaderText="ProcessingStatus" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <EditRowStyle BorderStyle="Solid" />
            </asp:GridView>
            </td>
        
         <td width="2%">&nbsp;</td>
        </tr>
        
        </table>            
    <table width="100%">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="20%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
       <td width="2%" colspan="2">&nbsp;</td>
        <td width="10%">&nbsp;</td>
         <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
         <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>
          <tr>
           
           <td class="style2"></td>
           <td class="style2">
               &nbsp;</td>
           <td class="style2"></td>
           <td class="style2">
               <asp:Label ID="Lbl_TranDate" runat="server" Text="Transaction Date" 
                   ForeColor="#0033CC"></asp:Label>
            
              </td>
              <td class="style2" colspan="2"></td>
           <td class="style2" >
               <asp:TextBox ID="Txt_TransactionDate" runat="server" Width="177px"></asp:TextBox>             
           </td>
           <td class="style2"></td>
            <td class="style2">
                &nbsp;</td>
           <td class="style2"></td>  <td class="style2">
              &nbsp;</td> <td class="style2"></td>
              
               
          
          </tr> 
           <tr>
           <td></td><td></td><td></td><td>
               </td><td colspan="2"></td><td>
               
               </td><td></td><td></td><td></td><td>
              
               </td><td></td>
           
          </tr> 
          
       
          <tr>
           <td></td><td></td><td></td>
               <td colspan ="2" align="center">
                  
                   &nbsp;</td>
               <td align="center" colspan="2">
                   <asp:TextBox ID="TxtChk" runat="server" Width="177px" Visible="False"></asp:TextBox>
              </td>
               <td></td>
               <td> 
                   <asp:Button ID="BtnChk" runat="server" Text="CHECKSUM" Visible="False" />
              </td><td></td><td>
               
               </td><td></td>
           
          </tr> 
        </table>
        
        <table width="100%">
        <tr> 
        <td width="2%">&nbsp;</td>
        <td class="style1">
            <asp:GridView ID="Dgv_DayEndProcess" runat="server" Width="100%" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="2px" 
                CellPadding="4" AutoGenerateColumns="False">
                <RowStyle BackColor="White" ForeColor="#003399" />
                <Columns>
                    <asp:BoundField DataField="DayEndProcess" HeaderText="DayOpenProcess" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Data" HeaderText="Values" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status">
                        <ItemStyle Font-Bold="False" Font-Names="Times New Roman" ForeColor="#009999" 
                            HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProcStatus" HeaderText="ProcessingStatus" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <EditRowStyle BorderStyle="Solid" />
            </asp:GridView>
            </td>
        
         <td width="2%">&nbsp;</td>
        </tr>
        
        </table>
        
           <tr>
        <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
      
      </tr>
      <table width ="100%">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
       <td width="2%">&nbsp;</td>
       <td width="15%">
           <asp:ImageButton ID="Cmd_Next" runat="server" ImageUrl="~/Images/Dtls.png" />
          </td>
       <td>
     
                
         <td width="2%">&nbsp;</td>
        
         <td width="2%">&nbsp;</td>
         <td>
             &nbsp;</td>
         <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="15%">
                  <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                      AssociatedUpdatePanelID="UpdatePanel1">
                      <ProgressTemplate>
                          <div ID="blur">
                          </div>
                          <div ID="progress">
                              <img alt="progress" src="Images/progress.gif" /> Please Wait...
                          </div>
                      </ProgressTemplate>
                  </asp:UpdateProgress>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="15%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
              <td width="15%" align="center">
                  &nbsp;</td>
              <td>
                  &nbsp;<td width="2%">
                      &nbsp;</td>
                  <td width="2%">
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
                  <td width="2%">
                      &nbsp;</td>
                  <td width="15%">
                      &nbsp;</td>
                  <td width="2%">
                      &nbsp;</td>
              </td>
          </tr>
    </table>
        
      <tr>
        <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
      
      </tr>
     </table>
    
    </div>
  </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>


