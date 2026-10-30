<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="WebForm4.aspx.vb" Inherits="SMARTCORE_UI.WebForm4" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <script language="javascript" type="text/javascript">
// <!CDATA[

      function Button1_onclick() {

      }

      // ]]>


      var newsText = new Array();
      newsText[0] = " 2001...";
//      newsText[1] = "This is a text-typing DHTML demo. \nScript featured on http://javascriptkit.com";
//      newsText[2] = "Make sure you read the comments before you configure the script...";
//      newsText[3] = "Programmed by: Haitham Al-Beik...\n\nCopyright (c) Haitham M. Al-Beik, 2001";
      newsText[1] = "2005..."

      var ttloop = 1;    // Repeat forever? (1 = True; 0 = False)
      var tspeed = 50;   // Typing speed in milliseconds (larger number = slower)
      var tdelay = 1000; // Time delay between newsTexts in milliseconds

      // ------------- NO EDITING AFTER THIS LINE ------------- \\
      var dwAText, cnews = 0, eline = 0, cchar = 0, mxText;

      function doNews() {
          mxText = newsText.length - 1;
          dwAText = newsText[cnews];
          setTimeout("addChar()", 1000)
      }
      function addNews() {
          var Ctl = document.getElementById('<%=news2.ClientID%>');
          cnews += 1;
          if (cnews <= mxText) {
              dwAText = newsText[cnews];
              if (dwAText.length != 0) {
                  Ctl.value = "";
                  eline = 0;
                  setTimeout("addChar()", tspeed)
              }
          }
      }
      function addChar() {

          var Ctl = document.getElementById('<%=news2.ClientID%>');
          if (eline != 1) {
              if (cchar != dwAText.length) {
                  nmttxt = ""; for (var k = 0; k <= cchar; k++) nmttxt += dwAText.charAt(k);
                  Ctl.value = nmttxt;
                  cchar += 1;
                  if (cchar != dwAText.length) Ctl.value += "_";
              } else {
                  cchar = 0;
                  eline = 1;
              }
              if (mxText == cnews && eline != 0 && ttloop != 0) {
                  cnews = 0; setTimeout("addNews()", tdelay);
              } else setTimeout("addChar()", tspeed);
          } else {
              setTimeout("addNews()", tdelay)
          }
      }

      doNews()






    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<br />
<br />
<br />
    <asp:TextBox ID="TextBox1" runat="server" BackColor="#DFEAFF"></asp:TextBox>
<br />

    <input id="Button1" type="button" value="button" onclick="return Button1_onclick()" />
<asp:TextBox runat ="server"   ID ="news2" Width ="100%">

</asp:TextBox>


</asp:Content>

