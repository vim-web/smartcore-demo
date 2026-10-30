<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IMG_Upload.aspx.vb" Inherits="SMARTCORE_UI.IMG_Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/PageStyle.css" type="text/css" rel="stylesheet" />
     <link href="ImgCSS/imgareaselect-animated.css" type="text/css" rel="Stylesheet" />
      
       <link href="ImgCSS/imgareaselect-deprecated.css" type="text/css" rel="Stylesheet" />
    <script  type="text/javascript" src="JS/jquery.min.js"></script>
       <script  type="text/javascript" src="JS/jquery.imgareaselect.js"></script>
       <script  type="text/javascript" src="JS/jquery.imgareaselect.min.js"></script>
    
    <script type="text/javascript" >
        function openfile(id) {
            safe(id).click();
        }

        var safe = function (id)
        { return document.getElementById(id); }
        //function show(id) {
        //    safe(id).style.display = "block";
        //}

        //function hide(id) {
        //    safe(id).style.display = "none";
        //}



        //function readURL(input) {

        //    if (input.files && input.files[0]) {
        //        var reader11 = new FileReader();
        //        reader11.onload = function (e) {
        //            document.getElementById('img_upload').src = e.target.result;
        //            show('Upload');
        //            hide('index');
        //                // $('#ImgPhoto').animate({ width: "125px", height: "140px" });
        //                //show('overlay');
        //                //show('ImgPhoto');
        //            }
        //            reader11.readAsDataURL(input.files[0]);
        //    }
        //    var imageInByte = encodeURIComponent($('#img_upload').attr('src'));
        //    alert(imageInByte);

        //}

        //$(document).ready(function () {
        //    $('img#img_upload').imgAreaSelect({
        //        handles: true,
        //        onSelectEnd: preview
        //    });
        //});

        //$(function () {
        //    $('#img_upload').imgAreaSelect(
        //	{

        //	    aspectRatio: '1:1',

        //	    handles: true,
        //	    fadeSpeed: 200,

        //	    onSelectChange: preview
        //	});
        //});
        //function preview(img, selection) {
        //    alert(selection.width);
        //    if (!selection.width || !selection.height)
        //        return;
        //    var scaleX = 100 / selection.width;
        //    var scaleY = 100 / selection.height;
        //    //$('#preview img').css(
        //    //{
        //    //    width: Math.round(scaleX * 300),
        //    //    height: Math.round(scaleY * 300),
        //    //    marginLeft: -Math.round(scaleX * selection.x1),
        //    //    marginTop: -Math.round(scaleY * selection.y1)
        //    //});

        //    /*
        //        setting value for coordinator input fields.
        //    */

        //    $('#x1').val(selection.x1);
        //    $('#y1').val(selection.y1);
        //    $('#x2').val(selection.x2);
        //    $('#y2').val(selection.y2);
        //    $('#w').val(selection.width);
        //    $('#h').val(selection.height);
        //}

        //$(function () {
        //    $('#photo').imgAreaSelect(
        //	{

        //	    aspectRatio: '1:1',

        //	    handles: true,
        //	    fadeSpeed: 200,

        //	    onSelectChange: preview
        //	});
        //});


        //function readURL(input) {
            
        //    if (input.files && input.files[0]) {
        //        var reader11 = new FileReader();
        //        //var fso = new ActiveXObject("Scripting.FileSystemObject");
        //        //var f = fso.GetFile(document.getElementById("ctl00_ContentPlaceHolder1_Img_Upload").value);
               
        //        //alert(f.Size);
        //        //var f = fso.GetFile(photoM.value);
        //        reader11.onload = function (e) {
        //            var photoM = document.getElementById('img_upload');

        //            photoM.src = e.target.result;
        //            //document.getElementById('ctl00_ContentPlaceHolder1_hid_imgvalue').value = e.target.file[0];
        //            show('Upload');
        //            hide('index');

        //            var CurrW = photoM.width;
        //            var CurrH = photoM.height;
        //            var ratio = CurrH / CurrW
        //            if (CurrW >= 300 && ratio <= 1) {
        //                CurrW = 300;
        //                CurrH = CurrW * ratio;
        //            } else if (CurrH >= 300) {
        //                CurrH = 300;
        //                CurrW = CurrH / ratio
        //            }
        //            photoM.width = CurrW
        //            photoM.height = CurrH
        //            document.getElementById('imgw').value = CurrW
        //            document.getElementById('imgH').value = CurrH;

        //            if (CurrW >= 150 && ratio <= 1) {
        //                CurrW = 150;
        //                CurrH = CurrW * ratio;
        //            } else if (CurrH >= 150) {
        //                CurrH = 150;
        //                CurrW = CurrH / ratio
        //            }
        //            //var pre_img = document.getElementById('img_pre');
        //            //var pre_div = document.getElementById('preview');

        //            //pre_img.src = e.target.result;
        //            //pre_div.width = CurrW
        //            //pre_div.height = CurrH
        //            //pre_img.width = CurrW
        //            //pre_img.height=CurrH
        //            // $('#ImgPhoto').animate({ width: "125px", height: "140px" });
        //            //show('overlay');
        //            //show('ImgPhoto');
        //        }
        //        reader11.readAsDataURL(input.files[0]);
        //    }
        //    //var imageInByte = encodeURIComponent($('#img_upload').attr('src'));
        //    //alert(imageInByte);

        //}

        
</script>
</head>
<body>
    <form id="photoUpload" enctype="multipart/form-data" runat="server">
        <input type="hidden" runat="server" value="0" id="x1" />
        <input type="hidden" runat="server" value="0" id="y1" />
        <input type="hidden" runat="server" value="0" id="x2" />
        <input type="hidden" runat="server" value="0" id="y2" />
        <input type="hidden" runat="server" value="0" id="w" />
        <input type="hidden" runat="server" value="0" id="h" />
        <input type="hidden" runat="server" value="Photo" id="hid_imgty" />
         <input type="hidden" runat="server" value="Photo" id="hid_PhotoType" />
        <input type="hidden" runat="server"  id="imgH" value="0" />
        <input type="hidden" runat="server" id="imgw" value="0" />
        
        
         <div class="documentbut" style="width:40%; margin-top:30px;" onclick="openfile('fiPhoto');">File Upload</div>
        <input id="fiPhoto" type="file" runat="server" style="display:none;"  />
    
         
         <%--<div id="webCam" style="width:100%; height:100%;display:none;">
             <div id="webcamphoto">
                 <asp:Image ID="Image1" runat="server" ImageUrl="" />
             </div>
            
         </div>
         <div id="Upload" style="width:100%; height:100%;display:none; padding-left:5%;">
             <div style="width:49%;float:left">
            <%-- <asp:Image runat="server" ID="img_upload"  ImageUrl ="~/Images/no_photo.jpg" Width="150" Height="250" />
             <img src="~/Images/no_photo.jpg" alt="" id="img_upload"  />
                 </div>
             <div style="width:50%; float:left;" >
              <img ID="imgCapture" runat="server"  src="~/Images/ClearBtn.png" />
                 </div>
             <div  style="width:100%; float:left;" >

             </div>
             <input id="btnUpload" type="button" value="Upload Photo" />
         </div>--%>
    
        
    </form>
</body>
</html>
