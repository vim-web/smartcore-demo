<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CustDocuments.aspx.vb" Inherits="SMARTCORE_UI.CustDocuments" 
    title="Customer Documents" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

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
    </script>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .fb8
{
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
 
  background  : -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgb(181,181,181)), color-stop(100%,rgb(100,100,100))); /* Chrome,Safari4+ */
background : #CC6600;
}
        .fb8:hover
{
    border-style: solid;
    color: White;
    background: #FF9900;
    border: none;
    outline: none;
}
    </style>
 
   <script type="text/javascript" src="Scripts/jquery.minphoto.js"></script>
    <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/CustDocuments.aspx") %>';
        $(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "save",
                swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                    $('#camStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                    $.ajax({
                        type: "POST",
                        url: pageUrl + "/GetCapturedImage",
                        data: '',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            $("[id*=imgCapture]").css("visibility", "visible");
                            $("[id*=imgCapture]").attr("src", r.d);
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                }
            });
        });
        function Capture() {
            webcam.capture();
            return false;
        }
    </script>

 
    <style type="text/css">

        .fb8
{
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
 
  background  : -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgb(181,181,181)), color-stop(100%,rgb(100,100,100))); /* Chrome,Safari4+ */
background : #CC6600;
}
        </style>

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 --%>
     <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;margin-top:5%;" >                
    
    <legend>Customer Documents</legend>  
    <table width="100%">
    <tr><td width="10%"></td>
    <td width="30%">
        <label>Customer ID</label></td>
        
    <td width="50%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtCustID" runat="server" Width="70%" 
                meta:resourcekey="TxtCustIDResource1" AutoPostBack="True"></asp:TextBox>
            <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                ImageUrl="~/images/Search.png" Width="23px" />                
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%">
        <label>Customer Name</label></td>
        
    <td width="50%">
        
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtName" runat="server" Width="70%" 
                meta:resourcekey="TxtNameResource1" TabIndex="5"></asp:TextBox>
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%">
        <label>Address</label></td>
        
    <td width="50%">
        
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAddress" runat="server" Width="70%" TextMode="MultiLine"
                meta:resourcekey="TxtNameResource1" TabIndex="5"></asp:TextBox>
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%">&nbsp;</td>
    <td width="30%">
        <label>Branch</label></td>
        
    <td width="50%">
        
                        <asp:DropDownList ID="CmbAdmBranch" runat="server" Width="70%" 
                meta:resourcekey="CmbAdmBranchResource1" TabIndex="11" Enabled="False">
            </asp:DropDownList>
            </td>
    <td width="10%">&nbsp;</td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%">
        </td>
        
    <td width="50%">
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%">
       <label>Photo Type</label></td>
        
    <td width="50%">
        <asp:DropDownList ID="CmbPhotoType" runat="server" Enabled="True" 
            Width="70%" AutoPostBack="True">
        </asp:DropDownList>
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%">

       </td>
        
    <td width="50%">
       </td>
    <td width="10%"></td>
    </tr>
        <tr>
            <td width="10%">
               </td>
            <td width="30%">
                <asp:Image ID="ImgPhoto" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                    Height="151px" ImageUrl="~/Images/no_photo.jpg" TabIndex="56" Width="130px" />
               </td>
            <td width="50%">
                <input ID="TxtBrowse" runat="server"  style="width: 216px; " 
                    tabindex="48" type="file" visible="True" /><asp:Button ID="btnCapture0" 
                    Text="AttachPhoto" runat="server" CssClass="fb8" 
                     />
                </td>
            <td width="10%">
               </td>
        </tr>
        <tr>
            <td width="10%">
               </td>
            <td width="30%">
               </td>
            <td width="50%">
               </td>
            <td width="10%">
               </td>
        </tr>
        <tr>
            <td width="10%">
            </td>
            <td width="30%">
            
             <img ID="imgCapture" runat="server"  style=" visibility: hidden; width: 320px;height: 240px"  />
                
            </td>
            <td width="50%">
                <div ID="cam" runat="server">
                    <div ID="webcam">
                    </div>
                </div>
            </td>
            <td width="10%">
            </td>
        </tr>
         <tr>
             <td width="10%">
                </td>
             <td width="30%">
                </td>
             <td width="50%">
    <asp:Button ID="btnCapture" Text="AttachPhoto" runat="server" CssClass="fb8" OnClientClick="return Capture();" />
                </td>
             <td width="10%">
                </td>
        </tr>
        <tr>
            <td width="10%">
               </td>
            <td width="30%">
               </td>
            <td width="50%">
                <%--<asp:Button ID="ButViewPhoto" runat="server"  Height="20px" 
                    OnClientClick="return Capture();"
                    TabIndex="50" Text="Attach Photo" UseSubmitBehavior="False" Width="130px" />--%>
            </td>
            <td width="10%">
               </td>
        </tr>
        <tr>
            <td width="10%">
               </td>
            <td width="30%">
               </td>
            <td width="50%">
        <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
            onclick="CmdSave_Click1" OnClientClick="return ValidateSave();" />
        <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
        <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" 
            PostBackUrl="~/Main.aspx"/>
            </td>
            <td width="10%">
               </td>
        </tr>
         </table>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    
</asp:Content>

