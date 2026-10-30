<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebCame.aspx.vb" Inherits="SMARTCORE_UI.WebCame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/PageStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery.minphoto.js"></script>
     <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/CustomerRegister.aspx")%>';
        $(function () {
            
            jQuery("#webcam").webcam({
                width: 240,
                height: 280,
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
                           
                            // $("[id*=imgWebcame]").css("visibility", "visible");
                            document.getElementById('hid_imgsrc').value = r.d;
                            document.getElementById('divimg').style.display = 'block';
                            $("[id*=imgWebcame]").attr("src", r.d);
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
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" value="" id="hid_imgsrc" />
        <div style="float:left; height:340px; width:100% ">
            <div style="float:left; width:250px; height:340px; background:#eae0e0; " >
    <div id="webcam" style=" float:left; margin-left: 5px; width: 240px;height: 280px; border:3px solid #e8c8c8;">    
    </div>
                <div style="float: left; width: 100%; margin-top: 10px;">
                <div style="float: left; margin-left: 50px; width: 50px; cursor: pointer;" class="btn" onclick="Capture()">Click</div>
                     <div id="WebCameClose" style="float: left; margin-left: 10px; width: 50px; cursor: pointer;" class="btn" >Close</div>
            </div>
                </div>
            <div id="divimg" style="float:left; margin-left:20px; width:240px; height:280px; padding-top:50px; display:none; background:#eae0e0;border:3px solid #e8c8c8;  " >
                <img id="imgWebcame" src=""  style="float:left" width="230" height="200"  />
                <div style="float: left; width: 100%; margin-top: 30px;">
                <div style="float: left; margin-left: 30px; width: 50px; cursor: pointer;" class="btn" id="btn_ok" >OK</div>
            </div>
            </div>
            
            </div>
    </form>
</body>
</html>
