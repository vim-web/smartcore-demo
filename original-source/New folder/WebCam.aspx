<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebCam.aspx.vb" Inherits="SMARTCORE_UI.WebCam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div>
    <video id="video" width="320" height="240" autoplay="autoplay"></video>
<br />

<button type="button" onclick="capture()">Capture</button>

<canvas id="canvas" width="320" height="240" style="display:none;"></canvas>

<asp:Image ID="imgCaptured" runat="server" Width="320" Height="240" />
    </div>--%>

<button type="button" onclick="openCamera()">Open Camera</button>

<div id="camDiv" style="display:none; border:1px solid #ccc; padding:10px;">
    <video id="video" width="320" height="240"></video>
    <br />

    <button type="button" onclick="capture()">Capture</button>
    <button type="button" onclick="closeCamera()">Close</button>

    <canvas id="canvas" width="320" height="240" style="display:none;"></canvas>
</div>

<br />
<asp:Image ID="imgCaptured" runat="server" Width="320" Height="240" />




    </form>
<%--<script type="text/javascript">
    var video = document.getElementById("video");

    navigator.mediaDevices.getUserMedia({ video: true })
        .then(function (stream) {
            video.srcObject = stream;
            video.play();
        })
        .catch(function (err) {
            alert("Camera error: " + err);
        });

    function capture() {

        if (video.readyState !== video.HAVE_ENOUGH_DATA) {
            alert("Camera not ready");
            return;
        }

        var canvas = document.getElementById("canvas");
        var ctx = canvas.getContext("2d");

        ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

        var imgData = canvas.toDataURL("image/png");

        // Show captured image immediately
        document.getElementById("<%= imgCaptured.ClientID %>").src = imgData;
        //alert("Capture clicked");
        // Optional: send to server
        fetch("WebCam.aspx/SaveImage", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify({ imageData: imgData })
        });
    }
</script>--%>


    <script type="text/javascript">
        var video = document.getElementById("video");
        var stream = null;

        function openCamera() {
            document.getElementById("camDiv").style.display = "block";

            if (stream !== null) return;

            navigator.mediaDevices.getUserMedia({ video: true })
                .then(function (s) {
                    stream = s;
                    video.srcObject = stream;
                    video.play();
                })
                .catch(function (err) {
                    alert("Camera error: " + err);
                });
        }

        function capture() {
            if (!stream) {
                alert("Camera not started");
                return;
            }

            var canvas = document.getElementById("canvas");
            var ctx = canvas.getContext("2d");

            ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

            var imgData = canvas.toDataURL("image/png");

            document.getElementById("<%= imgCaptured.ClientID %>").src = imgData;
    }

    function closeCamera() {
        if (stream) {
            stream.getTracks().forEach(function (track) {
                track.stop();
            });
            stream = null;
        }

        video.srcObject = null;
        document.getElementById("camDiv").style.display = "none";
    }
</script>


</body>
</html>
