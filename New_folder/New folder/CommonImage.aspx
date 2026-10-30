<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CommonImage.aspx.vb" Inherits="SMARTCORE_UI.CommonImage" %>


 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="scripts/jquery_json.js"></script>
<link rel="stylesheet" href="CSS/Main.css" />


<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script type = "text/javascript" src = "http://www.aspsnippets.com/scripts/ScrollableGridPlugin.js?22_12"></script>
    <script type = "text/javascript">
    
    // To Bind the function to the textbox for moving the cursor from one textbox to another  
            $(document).ready(function () {  
                $("input:text:first").focus();  
                $("input:text").bind("keydown", function (e) {  
                    if (e.which == 13) {  
                        e.preventDefault();  
                        var nextIndex = $('input:text').index(this) + 1;  
                        if ($('input:text')[nextIndex] != null)  
                            $('input:text')[nextIndex].focus();  
                    }  
                });  
            });  
            
          </script>


   

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

 <div>
 <table>
 <tr>
 <td>
 
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
         Height="322px" Width="513px" BackColor="LightGoldenrodYellow" 
         BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" 
         GridLines="None">
      <Columns>
          <asp:BoundField HeaderText="Customer Id" DataField="Cust_Id" />
          <asp:BoundField HeaderText="ImageType" DataField="Img_ID" />
          <asp:TemplateField HeaderText="Image">
              <ItemTemplate>
                <img src="ShowImage.ashx?Cust_Id=<%# Eval("Cust_Id").ToString() %>" width="150" height="100" />
              </ItemTemplate>
          </asp:TemplateField>
      </Columns>
      <FooterStyle BackColor="Tan" />
      <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
          HorizontalAlign="Center" />
      <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
      <HeaderStyle BackColor="Tan" Font-Bold="True" />
      <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
 
     
 
 <td>
   <asp:Button ID="Button1" runat="server" Text="Back" />

 </td>
 
 </td>
 
 
 </tr>
 
 
 </table>
 
 </div> 
    
   
            </ContentTemplate>
        </asp:UpdatePanel> 
        </asp:Content>


    
    
    
    
    
    
    
    
    




    
    
        
    
       
        
          
          
     
    
    
        
    
         
     
   