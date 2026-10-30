<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="WebCamTest.aspx.vb" Inherits="SMARTCORE_UI.WebCamTest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script> 
    <script type="text/javascript" src="JS/sweetalert.min.js"></script>
    <link href="CSS/sweetalert.min.css" rel="stylesheet" type="text/css" />   
    <link rel="stylesheet" href="assets/ventura/assets/css/style.css"/>
    <link href="assets/ventura/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/Main.css" />
    <script src="JS/WebCam.js" type="text/javascript"></script>

    

<link href="assets/ventura/slide.css" rel="stylesheet" type="text/css" />


<style type="text/css">    
.upload_div
    {
        display: grid;
        justify-content: center;
        justify-items: center;
        align-items: center;
        height: 100%;
    }
    .upload_div h3
    {
        
    }
.drop-zone {
  width: 200px;
  height: 200px;
  padding: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  font-family: "Quicksand", sans-serif;
  font-weight: 500;
  font-size: 20px;
  cursor: pointer;
  color: #cccccc;
  border: 3px dashed #65978d;
  border-radius: 10px;
}

.drop-zone--over {
  border-style: solid;
}

.drop-zone__input {
  display: none;
}

.drop-zone__thumb {
  width: 100%;
  height: 100%;
  border-radius: 10px;
  overflow: hidden;
  background-color: #cccccc;
  background-size: contain;
  background-repeat: no-repeat;
  position: relative;
}

.drop-zone__thumb::after {
  content: attr(data-label);
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  padding: 5px 0;
  color: #ffffff;
  background: rgba(0, 0, 0, 0.75);
  font-size: 14px;
  text-align: center;
}
.card-header {
    padding: 1.20rem 1.25rem;
    box-shadow: rgb(145 158 171 / 20%) 0px 0px 10px 0px, rgb(145 158 171 / 12%) 0px 12px 24px -4px;
    border-radius: 16px !important;
    background: #fff;
}                
.mfcolor
{
    color:Red
}
.text-center
{
    padding-top:10px;
}
.Operators
{
margin-top: 10px;
float: left;
box-shadow: 5px 5px 5px 5px #C4C4C4;
border-radius: 7px;
background: transparent radial-gradient(ellipse at center center , #E5E5E5 0%, #FFF 100%) repeat scroll 0% 0%;
font: 600 15px "Open Sans",Arial,sans-serif;
}
    .form-control
    {
        font-size:12px;
    }

    .imgCapture
    {
        width:260px ;
        height:195px ;
    }
    .webcam_btn
    {
        background-color: #f47f27;
        border: 0;
        color: #fff;
        font-size: 12px;
        padding: 5px 20px !important;
        border-radius: 5px;
        text-align: center;
    }
</style>  

<script type="text/javascript">
    $(function () {
        $("#webcam_image").click(function () {

            Webcam.set({
                width: 260,
                height: 240,
                image_format: 'jpeg',
                jpeg_quality: 90
            });
            Webcam.attach('#webcam_img');
        });
        $(".popupclose").click(function () {
            Webcam.reset();
        });
        $("#ctl00_ContentPlaceHolder1_BtnUpload").click(function () {
            Webcam.reset();
        });
        $("#btnCapture").click(function () {
            Webcam.snap(function (data_uri) {
                /*$("#imgCapture")[0].src = data_uri;*/
                $("#imgCapture_image")[0].src = data_uri;
                $("#btnUpload").removeAttr("disabled");

            });

            $.ajax({
                type: "POST",
                url: "Customer_Register.aspx/CapturedImage",
                data: "{data: '" + $("#imgCapture_image")[0].src + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) { }
            });

        });


        var parameter = Sys.WebForms.PageRequestManager.getInstance();

        parameter.add_endRequest(function () {
            $(function () {
                $("#webcam_image").click(function () {

                    Webcam.set({
                        width: 260,
                        height: 240,
                        image_format: 'jpeg',
                        jpeg_quality: 90
                    });
                    Webcam.attach('#webcam_img');
                });
                $(".popupclose").click(function () {
                    Webcam.reset();
                });
                $("#ctl00_ContentPlaceHolder1_BtnUpload").click(function () {
                    Webcam.reset();
                });
                $("#btnCapture").click(function () {
                    Webcam.snap(function (data_uri) {
                        /*$("#imgCapture")[0].src = data_uri;*/
                        $("#imgCapture_image")[0].src = data_uri;
                        $("#btnUpload").removeAttr("disabled");

                    });

                    $.ajax({
                        type: "POST",
                        url: "Customer_Register.aspx/CapturedImage",
                        data: "{data: '" + $("#imgCapture_image")[0].src + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) { }
                    });

                });
            });
        });
    });



</script>


<script type="text/javascript">
    $(function () {
        $("#webcamsign").click(function () {

            Webcam.set({
                width: 260,
                height: 240,
                image_format: 'jpeg',
                jpeg_quality: 90
            });
            Webcam.attach('#webcam_sign');
        });
        $(".popupclose").click(function () {
            Webcam.reset();
        });
        $("#ctl00_ContentPlaceHolder1_BtnUploadSign").click(function () {
            Webcam.reset();
        });
        $("#btnCapture_sign").click(function () {
            Webcam.snap(function (data_uri) {
                /*$("#imgCapture")[0].src = data_uri;*/
                $("#imgCapture_sign")[0].src = data_uri;
                $("#btnUpload").removeAttr("disabled");

            });

            $.ajax({
                type: "POST",
                url: "Customer_Register.aspx/CapturedSign",
                data: "{data: '" + $("#imgCapture_sign")[0].src + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) { }
            });

        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();

        parameter.add_endRequest(function () {
            $(function () {
                $("#webcamsign").click(function () {

                    Webcam.set({
                        width: 260,
                        height: 240,
                        image_format: 'jpeg',
                        jpeg_quality: 90
                    });
                    Webcam.attach('#webcam_sign');
                });
                $(".popupclose").click(function () {
                    Webcam.reset();
                });
                $("#ctl00_ContentPlaceHolder1_BtnUploadSign").click(function () {
                    Webcam.reset();
                });
                $("#btnCapture_sign").click(function () {
                    Webcam.snap(function (data_uri) {
                        /*$("#imgCapture")[0].src = data_uri;*/
                        $("#imgCapture_sign")[0].src = data_uri;
                        $("#btnUpload").removeAttr("disabled");

                    });

                    $.ajax({
                        type: "POST",
                        url: "Customer_Register.aspx/CapturedSign",
                        data: "{data: '" + $("#imgCapture_sign")[0].src + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) { }
                    });

                });
            });
        });
    });


</script>




<script type="text/javascript" >

    function Charvalid() {
        alert = "safe"
        var Ename = document.getElementById(inputText);
        var EnamePatrn = /^[A-Za-z]+$/;
        if (!EnamePatrn.test(Ename.value)) {
            alert("Enter Characters Only");
            return false;
        }
        else
            return true;
    }


    function openfile(id) {
        safe(id).click();
    }

    $(document).ready(function () {
        $('img#img_upload').imgAreaSelect({
            handles: true,
            onSelectEnd: preview
        });
    });
    function hidesel() {
        $('img#img_upload').imgAreaSelect({ hide: true });
    }

    function upper(ustr) {
        var str = ustr.value;
        ustr.value = str.toUpperCase();
    }

    function lower(ustr) {
        var str = ustr.value;
        ustr.value = str.toLowerCase();
    }

    function preview(img, selection) {


        if (!selection.width || !selection.height)
            return;
        var scaleX = 100 / selection.width;
        var scaleY = 100 / selection.height;

        _ifrPhoto.contentWindow.document.getElementById('x1').value = selection.x1
        _ifrPhoto.contentWindow.document.getElementById('y1').value = selection.y1
        _ifrPhoto.contentWindow.document.getElementById('x2').value = selection.x2
        _ifrPhoto.contentWindow.document.getElementById('y2').value = selection.y2
        _ifrPhoto.contentWindow.document.getElementById('w').value = selection.width
        _ifrPhoto.contentWindow.document.getElementById('h').value = selection.height

    }



    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader11 = new FileReader();
            reader11.onload = function (e) {
                var photoM = document.getElementById('img_upload');
                photoM.src = e.target.result;
                var PhotoType = _ifrPhoto.contentWindow.document.getElementById('hid_PhotoType');
                PhotoType.value = "photo"

                show('Upload');
                hide('index');

                var CurrW = 200;
                var CurrH = 250;
                var ratio = CurrH / CurrW
                if (CurrW >= 300 && ratio <= 1) {
                    CurrW = 300;
                    CurrH = CurrW * ratio;
                } else if (CurrH >= 300) {
                    CurrH = 300;
                    CurrW = CurrH / ratio
                }
                photoM.width = CurrW
                photoM.height = CurrH

                _ifrPhoto.contentWindow.document.getElementById('imgw').value = 240;
                _ifrPhoto.contentWindow.document.getElementById('imgH').value = 280;

                $('img#img_upload').imgAreaSelect({
                    handles: true,
                    onSelectEnd: preview
                });

            }
            reader11.readAsDataURL(input.files[0]);
        }


    }

    var safe = function (id)
    { return document.getElementById(id); }
    function show(id) {
        safe(id).style.display = "block";
    }

    function hide(id) {
        safe(id).style.display = "none";
    }

</script>


 <script type="text/javascript">
     /* <![CDATA[ */
     var PROGRESS_INTERVAL = 500;
     var PROGRESS_COLOR = '#000080';

     var _divFrame;
     var _divUploadMessage;
     var _divUploadProgress;
     var _ifrPhoto;
     var ifrWebcame;
     var _loopCounter = 1;
     var _maxLoop = 7;
     var _photoUploadProgressTimer;

     function initPhotoUpload() {

         _divFrame = document.getElementById('popup_Document');
         _divUploadMessage = document.getElementById('divUploadMessage');
         _divUploadProgress = document.getElementById('divUploadProgress');
         _ifrPhoto = document.getElementById('ifrPhoto');

         var filPhoto = _ifrPhoto.contentWindow.document.getElementById('fiPhoto');
         var PhotoType = _ifrPhoto.contentWindow.document.getElementById('hid_PhotoType');

         filPhoto.onchange = function (e) {
             _ifrPhoto.contentWindow.document.getElementById('x1').value = "0";
             _ifrPhoto.contentWindow.document.getElementById('y1').value = "0";
             _ifrPhoto.contentWindow.document.getElementById('x2').value = "0";
             _ifrPhoto.contentWindow.document.getElementById('y2').value = "0";
             _ifrPhoto.contentWindow.document.getElementById('w').value = "0";
             _ifrPhoto.contentWindow.document.getElementById('h').value = "0";
             readURL(this)
         }
         // var btnUpload = _ifrPhoto.contentWindow.document.getElementById('btnUpload');
         var btnUpload = document.getElementById('btnUpload');

         btnUpload.onclick = function (event) {
             if (PhotoType.value == "photo") {
                 var filPhoto = _ifrPhoto.contentWindow.document.getElementById('fiPhoto');
                 var fileName = filPhoto.value
                 //Baisic validation for Photo
                 _divUploadMessage.style.display = 'none';

                 if (filPhoto.value.length == 0) {
                     _divUploadMessage.innerHTML = '<span style=\"color:#ff0000\">Please specify the file.</span>';
                     _divUploadMessage.style.display = 'block';
                     filPhoto.focus();
                     return;
                 }
                 var imgtype = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();

                 var regExp = /^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.bmp|.BMP)$/;

                 if (!(imgtype == "jpg" || imgtype == "gif" || imgtype == "png" || imgtype == "bmp")) //Somehow the expression does not work in Opera
                 {

                     _divUploadMessage.innerHTML = '<span style=\"color:#ff0000\">Invalid file type. Only supports jpg, gif, png and bmp.</span>';
                     _divUploadMessage.style.display = 'block';
                     filPhoto.focus();
                     return;
                 }
             }
             beginPhotoUploadProgress();
             _ifrPhoto.contentWindow.document.getElementById('photoUpload').submit();
             // _divFrame.style.display = 'none';
         }
     }

     function imgchange(f) {
         var filePath = $('#file').val();
         var reader = new FileReader();
         reader.onload = function (e) {
             $('#img2').attr('src', e.target.result);
         };
         reader.readAsDataURL(f.files[0]);
     }


     function beginPhotoUploadProgress() {
         _divUploadProgress.style.display = 'block';
         clearPhotoUploadProgress();
         _photoUploadProgressTimer = setTimeout(updatePhotoUploadProgress, PROGRESS_INTERVAL);
     }

     function clearPhotoUploadProgress() {
         for (var i = 1; i <= _maxLoop; i++) {
             document.getElementById('tdProgress' + i).style.backgroundColor = 'transparent';
         }

         document.getElementById('tdProgress1').style.backgroundColor = PROGRESS_COLOR;
         _loopCounter = 1;
     }

     function updatePhotoUploadProgress() {
         _loopCounter += 1;

         if (_loopCounter <= _maxLoop) {
             document.getElementById('tdProgress' + _loopCounter).style.backgroundColor = PROGRESS_COLOR;
         }
         else {
             clearPhotoUploadProgress();
         }

         if (_photoUploadProgressTimer) {
             clearTimeout(_photoUploadProgressTimer);
         }

         _photoUploadProgressTimer = setTimeout(updatePhotoUploadProgress, PROGRESS_INTERVAL);
     }

     function photoUploadComplete(message, isError) {
         clearPhotoUploadProgress();

         if (_photoUploadProgressTimer) {
             clearTimeout(_photoUploadProgressTimer);
         }

         _divUploadProgress.style.display = 'none';
         _divUploadMessage.style.display = 'none';
         //_divFrame.style.display = '';

         if (message.length) {
             var color = (isError) ? '#ff0000' : '#008000';

             _divUploadMessage.innerHTML = '<span style=\"color:' + color + '\;font-weight:bold">' + message + '</span>';
             _divUploadMessage.style.display = 'block';

             if (isError) {
                 _ifrPhoto.contentWindow.document.getElementById('filPhoto').focus();
             }
         }
     }

     function Setimgtyped(input) {

         _ifrPhoto.contentWindow.document.getElementById('hid_imgty').value = input.value
     }

     function initWebcame() {
         ifrWebcame = document.getElementById('IfrWebcame');
         var Okclick = ifrWebcame.contentWindow.document.getElementById('btn_ok');
         var imgsrc = ifrWebcame.contentWindow.document.getElementById('hid_imgsrc');
         var Webcame_Close = ifrWebcame.contentWindow.document.getElementById('WebCameClose');
         Okclick.onclick = function (e) {

             var PhotoType = _ifrPhoto.contentWindow.document.getElementById('hid_PhotoType');
             PhotoType.value = "webcame"
             document.getElementById('img_upload').src = imgsrc.value;
             _ifrPhoto.contentWindow.document.getElementById('imgw').value = 320
             _ifrPhoto.contentWindow.document.getElementById('imgH').value = 240;
             hide('webCam'); show('Upload');
             $('img#img_upload').imgAreaSelect({
                 handles: true,
                 onSelectEnd: preview
             });
         }

         Webcame_Close.onclick = function (e) { hide('webCam'); show('index'); }
     }

    </script>
   

    <style type="text/css">
        .modal-backdrop {
            height: 0;
        }
        .modal-dialog {
            margin: 4.75rem auto;
        }
        .modal-open .modal {
            background: #000000a3;
        }
         .floatingDivMinor
        {
        position:fixed;         
        top: 100px;
        box-shadow: 1px 1px 10px #424242;
        color: #42a2d7;
        background-color:#FFFFFF;
        padding-bottom: 20px;
        border-radius:3px; 
        width:450px; 
        height:auto; 
        display:none;
        z-index:1000;
        }
        #image-tag
        {
            width: 191px!important;
            height: 191px!important;
            top: 2px!important;
            right: 0px!important;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <input type="button" id="btnCapture" class="webcam_btn" value="Capture" />
</asp:Content>
