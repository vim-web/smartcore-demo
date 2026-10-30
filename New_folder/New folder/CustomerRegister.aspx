<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CustomerRegister.aspx.vb" Inherits="SMARTCORE_UI.CustomerRegister" title="Customer Register"  ValidateRequest="false"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
  <link href="ImgCSS/imgareaselect-default.css"  type="text/css" rel="Stylesheet" /> 
   <link href="ImgCSS/imgareaselect-animated.css" type="text/css" rel="Stylesheet" />     
       <link href="ImgCSS/imgareaselect-deprecated.css" type="text/css" rel="Stylesheet" /> 
<script type='text/javascript' src="scripts/jquery_json.js"></script>    
    <script type="text/javascript" src="Scripts/jquery.minphoto.js"></script>  
      <script type="text/javascript" src="JS/jquery.imgareaselect.pack.js"></script>
<script type="text/javascript" >
    function openfile(id)
    {
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
    

    function preview(img, selection) {
        
        if (!selection.width || !selection.height)
            return;
        var scaleX = 100 / selection.width;
        var scaleY = 100 / selection.height;
        //$('#preview img').css(
        //{
        //    width: Math.round(scaleX * 300),
        //    height: Math.round(scaleY * 300),
        //    marginLeft: -Math.round(scaleX * selection.x1),
        //    marginTop: -Math.round(scaleY * selection.y1)
        //});

        /*
            setting value for coordinator input fields.
        */
        _ifrPhoto.contentWindow.document.getElementById('x1').value =selection.x1
            _ifrPhoto.contentWindow.document.getElementById('y1').value =selection.y1
            _ifrPhoto.contentWindow.document.getElementById('x2').value =selection.x2
            _ifrPhoto.contentWindow.document.getElementById('y2').value =selection.y2
            _ifrPhoto.contentWindow.document.getElementById('w').value =selection.width
            _ifrPhoto.contentWindow.document.getElementById('h').value =selection.height
       
        //$('#ctl00_ContentPlaceHolder1_h').val(selection.height);
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
                //if (CurrW >= 150 && ratio <= 1) {
                //    CurrW = 150;
                //    CurrH = CurrW * ratio;
                //} else if (CurrH >= 150) {
                //    CurrH = 150;
                //    CurrW = CurrH / ratio
                //}
                //var pre_img = document.getElementById('img_pre');
                //var pre_div = document.getElementById('preview');

                //pre_img.src = e.target.result;
                //pre_div.width = CurrW
                //pre_div.height = CurrH
                //pre_img.width = CurrW
                //pre_img.height=CurrH
                 //$('#ImgPhoto').animate({ width: "125px", height: "140px" });
                //show('overlay');
                //show('ImgPhoto');
            }
            reader11.readAsDataURL(input.files[0]);
        }
        //var imageInByte = encodeURIComponent($('#img_upload').attr('src'));
        //alert(imageInByte);
        
    }

    var safe = function (id)
    { return document.getElementById(id); }
    function show(id)
    {
        safe(id).style.display = "block";
    }

    function hide(id)
    {
        safe(id).style.display = "none";
    }
    $(document).ready(function () {
  
      
  $('#ImgGuardianClose').click(function()
  {
    $('.floatingDivMinor').slideUp('slow');
     return false;
  });           
});  


</script>

<script language="javascript" type="text/javascript"> 
   
    function CheckMinor()
    {
	    var check=parseInt($('#ctl00_ContentPlaceHolder1_TxtAge').val());	
	    if(check < 18)
	    {	
		    $('.floatingDivMinor').slideDown('slow');	
	    }
	    else
	    {
	        $('.floatingDivMinor').slideUp('slow');
	    }
	    return false;
    }        
    function init()
    {
        var window_dimensions = "toolbars=no,menubar=no,location=no,scrollbars=yes,resizable=yes,status=yes";  
        window.opener=self;
        window.close();
        window.open("b.htm","_blank", window_dimensions);  
        window.moveTo(0,0);  
        window.resizeTo(screen.width,screen.height-100); 
    }              
    function Update() {
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermHouseNo').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresHouseNo').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermHouseName').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresHouseName').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermPlace').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresPlace').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermDist').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresDist').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermState').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresState').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermCountry').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresCountry').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermPin').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresPin').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermLand').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresLand').value;
        //document.getElementById('ctl00_ContentPlaceHolder1_TxtPermIDProof').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresIDProof').value;    
    
     document.getElementById('ctl00_ContentPlaceHolder1_TxtPermtaluk').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPrestaluk').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPermvillage').value = document.getElementById('ctl00_ContentPlaceHolder1_txtPresvillage').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPrempancynt').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPrespancynt').value;
        document.getElementById('ctl00_ContentPlaceHolder1_TxtPremward').value = document.getElementById('ctl00_ContentPlaceHolder1_TxtPresward').value;

    }

    

    function ImgIDClose1_onclick() {
        alert ("SD");
         return false;
    }        

</script>




<script language="javascript" type="text/javascript">
    function textboxMultilineMaxNumber(txt, maxLen) {
        if (txt.value.length > maxLen - 1) {
            alertify.error("Maximum No Of Words Limit Reached");
        }
            return true;
        }
        function phonenumber(inputtxt) {
            var phoneno = /^\d{10}$/;
            if ((inputtxt.value.match(phoneno))) {

                return true;
            }
            else {
                inputtxt.value = ""
                alertify.error("Invalid Mobile Number");
                return false;
            }
        }
</script>  

<script language="javascript" type="text/javascript">
function onlyNumbers(event) {
        var charCode = (event.which) ? event.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    </script>
<script language="javascript" type="text/javascript">
        function testDate(str) {
            var t = str.match(/^(\d{2})\/(\d{2})\/(\d{4})$/);
            if (t === null)
                return false;
            var d = +t[1], m = +t[2], y = +t[3];
            //below should be more acurate algorithm
            if (m >= 1 && m <= 12 && d >= 1 && d <= 31) {
                return true;
            }
            else
            return false;
        }
    </script>

    <script type="text/javascript">
        function onlyAlphabets(event) {
            var Key = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (Key == 13 || (Key >= 48 && Key <= 57)) return false;
            else return true;
        }
      
    </script>
     
  
            <script type="text/javascript" >
                $(document).ready(function () {
                    //Attach the change event 
                    $('[id$=CmbPhotoIDProof]').change(function (evt) {
                       
                    
                        if ($(this).val() != "0") {
                            //Enable the textbox
                            $('#<%=TxtPhotoIDNoProof.ClientID%>').val('');
                            $('#<%=TxtPhotoIdentityDtls.ClientID%>').val('');
                            $('#<%=TxtPhotoIssDate.ClientID%>').val('');
                            $('#<%=TxtPhotoExpiryDate.ClientID%>').val('');
                            $('#<%=TxtPhotoRemarks.ClientID%>').val('');
                            $('#<%=TxtPhotoIssAuth.ClientID%>').val('');

                           
                            // This stops the Postback
                            evt.preventDefault();
                        }
                    });
                });
            </script>
     <script type="text/javascript" >
         $(document).ready(function () {
             //Attach the changeTxtAddProofNo event 
             $('[id$=CmbAddProof]').change(function (evt) {
         
                 if ($(this).val() != "0") {
                     //Enable the textbox
              
                            $('#<%=TxtAddProofNo.ClientID%>').val('');
                            $('#<%=TxtAddIssAuth.ClientID%>').val('');
                            $('#<%=TxtAddIdentittyDtls.ClientID%>').val('');
                            $('#<%=TxtAddIssDate.ClientID%>').val('');
                     $('#<%=TxtAddExpiryDate.ClientID%>').val('');
                     $('#<%=TxtAddRemarks.ClientID%>').val('');


                            // This stops the Postback
                            evt.preventDefault();
                        }
                    });
                });
            </script>
<style type="text/css">                    
.mfcolor
{
    color:Red
   
}
</style>
    
<style type="text/css">                    
   
    
    .TblColor {
	    color: #92D0F5;
	    font-weight: bold
    }
    .TblClr {
	    border-top-style: groove;
	    border-right-style: groove;
	    border-bottom-style: groove;
	    border-left-style: groove;
	    border-top-color:  #03F;
	    border-right-color: #92D0F5;
	    border-bottom-color: #92D0F5;
	    border-left-color: #92D0F5;
    }
    
        .floatingDiv
        {
          position:absolute;         
          top: 630px;
          box-shadow: 10px 10px 10px #888888;
          color: #42a2d7;
          background-color:#FFFFFF;
          border:1px solid #000000; 
          border-radius:3px; 
          width:450px; 
          height:auto; 
          display:none; z-index:1000;
        }
        .floatingDivMinor
        {
          position:absolute;         
          top: 630px;
          box-shadow: 10px 10px 10px #888888;
          color: #42a2d7;
          background-color:#FFFFFF;
          border:1px solid #000000; 
          border-radius:3px; 
          width:450px; 
          height:auto; 
          display:none;
        }
    .twitterStyleTextbox {}
    </style>  
      <script language="javascript" type="text/javascript">
          function checkEmail() {
             
              var email = document.getElementById("<%=TxtEmail.ClientID%>");
            
              var pattern = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
              if (!pattern.test(email.value)) {
                  alertify.error('Please provide a valid email address');
                  return false;
              }
              else
              return true;
          }


          function ValidatePAN(Obj) {

              if (Obj == null) Obj = window.event.srcElement;
              var PanId = document.getElementById('<%=CmbAddProof.ClientID%>');
              var PanId1 = document.getElementById('<%=CmbPhotoIDProof.ClientID%>');
             
              if (PanId.value == "683" ) {
                  if (Obj.value != "") {

                      ObjVal = Obj.value;
                      var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                      var code = /([C,P,H,F,A,T,B,L,J,G])/;
                      var code_chk = ObjVal.substring(3, 4).toUpperCase();
                      if (ObjVal.search(panPat) == -1) {


                          Obj.value = '';
                          alertify.error('PAN No. Entered is Invalid, Please Check the format[ABCDE1234F]');


                          return false;
                      }
                      else {
                          document.getElementById('ContentPlaceHolder1_HidIsTrue').value = 'True';
                      }

                  }
              }
          }
          function ValidatePAN1(Obj) {

              if (Obj == null) Obj = window.event.srcElement;
           

              var PanId1 = document.getElementById('<%=CmbPhotoIDProof.ClientID%>');
            
              if (PanId1.value == "683") {
                  if (Obj.value != "") {

                      ObjVal = Obj.value;
                      var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                      var code = /([C,P,H,F,A,T,B,L,J,G])/;
                      var code_chk = ObjVal.substring(3, 4).toUpperCase();
                      if (ObjVal.search(panPat) == -1) {


                          Obj.value = '';
                          alertify.error('PAN No. Entered is Invalid, Please Check the format[ABCDE1234F]');


                          return false;
                      }
                      else {
                          document.getElementById('ContentPlaceHolder1_HidIsTrue').value = 'True';
                      }

                  }
              }
          }

          function PinCodevalid() {
              var pin_codeTep = document.getElementById('<%=TxtPresPin.ClientID%>');
              var pin_codePer = document.getElementById('<%=TxtPermPin.ClientID%>');
              
              var pat1 = /^\d{6}$/;
             
              if (pin_codeTep.value != "") {
                  if (!pat1.test(pin_codeTep.value)) {
                      alertify.error("Pin code should be 6 digits");

                      return false;
                  }
              }
              if (pin_codePer.value != "") {
                  if (!pat1.test(pin_codePer.value)) {
                      alertify.error("Pin code should be 6 digits");

                      return false;
                  }
              }
          }

</script>

<script language="javascript" type="text/javascript">
    

    function validatedate(inputText)
  {  
  var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;  
  // Match the date format through regular expression  
  if(inputText.value.match(dateformat))  
  {
     
//  document.form1.text1.focus();  
  //Test which seperator is used '/' or '-'  
  var opera1 = inputText.value.split('/');  
  var opera2 = inputText.value.split('-');  
  lopera1 = opera1.length;  
  lopera2 = opera2.length;  
  // Extract the string into month, date and year  
  if (lopera1>1)  
  {  
  var pdate = inputText.value.split('/');  
  }  
  else if (lopera2>1)  
  {  
  var pdate = inputText.value.split('-');  
  }  
  var dd = parseInt(pdate[0]);  
  var mm  = parseInt(pdate[1]);  
  var yy = parseInt(pdate[2]); 
  // Create list of days of a month [assume there is no leap year by default]  
  var ListofDays = [31,28,31,30,31,30,31,31,30,31,30,31];  
  if (mm==1 || mm>2)  
  {  
  if (dd>ListofDays[mm-1])  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
      inputText.value = "";
  return false;  
  }  
  }  
  if (mm==2)  
  {  
  var lyear = false;  
  if ( (!(yy % 4) && yy % 100) || !(yy % 400))   
  {  
  lyear = true;  
  }  
  if ((lyear==false) && (dd>=29))  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
      inputText.value = ""; 
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
      inputText.value = "";
  return false;  
  }   
  }   
  }   
  else  
  {   
      alertify.error("Invalid date format! Please enter a valid date!");
      inputText.value = "";
  document.form1.text1.focus();  
  return false;  
  }
        return true
  }  


    function CalculateAge(DOB) {
       
        if (DOB.value != '') {
           
            if (validatedate(DOB)) {
                
                now = new Date()
                dob = DOB.value.split('/');
                if (dob.length === 3) {
                    born = new Date(dob[2], dob[1] * 1 - 1, dob[0]);
                    if (now.getTime() > born.getTime()) {
                        age = Math.floor((now.getTime() - born.getTime()) / (365.25 * 24 * 60 * 60 * 1000));
                        if (isNaN(age)) {
                            document.getElementById('ctl00_ContentPlaceHolder1_TxtAge').value = '';
                            alertify.error('Input date is incorrect!'); DOB.value = '';
                        }
                        else {
                            document.getElementById('ctl00_ContentPlaceHolder1_TxtAge').value = age;
                            if (age < 18) {
                                $('.floatingDivMinor').slideDown('slow');
                            }
                            else {
                                $('.floatingDivMinor').slideUp('slow');
                            }

                        }
                    }
                    else { alertify.error('Select Less Then Current Date'); DOB.value = ''; }
                }
            }
           
        }
    }


function GetIDproofNo(input)
{
document.getElementById('ctl00_ContentPlaceHolder1_TxtPhotoIDProof').value=input.value;
}
function GetADDproofNo(input)
{
document.getElementById('ctl00_ContentPlaceHolder1_TxtAddIDProof').value=input.value;
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
                 if (PhotoType.value == "photo")
                     {
                 var filPhoto = _ifrPhoto.contentWindow.document.getElementById('fiPhoto');
              var fileName=filPhoto.value
                 //Baisic validation for Photo
                 _divUploadMessage.style.display = 'none';
              
                 if (filPhoto.value.length == 0) {
                     _divUploadMessage.innerHTML = '<span style=\"color:#ff0000\">Please specify the file.</span>';
                     _divUploadMessage.style.display = 'block';
                     filPhoto.focus();
                     return;
                 }
                 var imgtype = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
             
                 var regExp = /^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpeg|.JPEG|.jpg|.JPG|.gif|.GIF|.png|.PNG|.bmp|.BMP)$/;

                 if (!(imgtype == "jpg" || imgtype == "jpeg" || imgtype == "gif" || imgtype == "png" || imgtype == "bmp")) //Somehow the expression does not work in Opera
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

         function Setimgtyped(input)
         {
             
             _ifrPhoto.contentWindow.document.getElementById('hid_imgty').value = input.value
         }

         function MobileNochange()
         {
             show('div_Verify'); hide('imgverifyError'); hide('imgVerify');
             document.getElementById('ctl00_ContentPlaceHolder1_Hid_ODPStatus').value = "N";
         }


         function initWebcame()
         {
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
         /* ]]> */

          function MobileNovaliadation()
         {
             var valid = document.getElementById('<%=TxtMobile.ClientID%>');
             if (valid.value.length !=10  )
             {
                 alertify.error('INVALID MOBILE NO.ENTER 10 INTEGER');
                 return false;

             }
             hide('div_Verify'); show('img_process')
             return true;
         }

    </script>

    <script type="text/javascript">
        function ChangeNumber() {
            if (confirm("Do you want to Change the Mobile Number?")) {
                return true;
            }
            return false;
        }
       
 </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
   
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>
           <script type="text/javascript">
                           Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                           function BeginRequestHandler(sender, args) {

                               args.get_postBackElement().disabled = true;
                           }
</script>
    
    <div id="cover"></div> <input type="hidden" id="hid_CustID" runat="server" />
    <div class="popup" id="VerifyMob" style="width:200px;height:100px; margin-left:75%; margin-top:300px; ">
          <div class="popupclose" onclick="hide('VerifyMob');hide('cover');"></div>
        <div style="float:left; width:100%; margin-top:5px;">
            <asp:TextBox runat="server" ID="txt_verifycode" Width="70%" style="margin-left:20px;" ></asp:TextBox>
            <input type="hidden" value="N" runat="server" id="Hid_ODPStatus" />
        </div>
          <div style="float:left; width:100%; margin-top:5px; ">
              <asp:Button ID="But_verify" runat="server" Text="Verify" style="margin-left:100px;" CssClass="btn" BorderStyle="None" />
          </div>
         <div style="float:left; width:100%; margin-top:5px; ">
          <label id="lbl_sessionexp" style="font-size:11px; color:red; display:none;">Session Expired</label>
             <label id="lbl_errMsg" style="font-size:11px; color:red; display:none;">Enter Correct OTP Code</label> 
          </div>
    </div>               
    <div id="popup_Document" class="popup"  style="width:600px;" > 
        <div class="popupclose" onclick="closeCamera();hide('popup_Document');hide('cover');hide('webCam');hide('Upload');show('index');hidesel();hide('divUploadMessage');"></div>
         <div id="index" style="width:100%; height:100%;">
           <div class="documentbut" style="width:40%; margin-top:100px;" onclick="hide('index');show('webCam');openCamera();">Web Cam</div>
             <iframe id="ifrPhoto" onload="initPhotoUpload()" scrolling="no" frameborder="0" hidefocus="true" style="text-align:center;vertical-align:middle;border-style:none;float:left; width:100%;height:100%" src="IMG_Upload.aspx"></iframe>      
          
         </div>
        <div id="webCam" style="width: 100%; height: 100%; display: none;">
            <%--<iframe id="IfrWebcame" onload="openCamera()"  scrolling="no" frameborder="0" hidefocus="true" style="text-align:center;vertical-align:middle;border-style:none;float:left; width:100%;height:100%" src="WebCame.aspx"></iframe>--%>
                    
            <table >
<tr>
    <td>

<div id="camDiv" style="display:none; border:1px solid #ccc; padding:10px;">

            <video id="video" width="300" height="200"></video>
    <br />

    

    <canvas id="canvas" width="200" height="200" style="display:none;"></canvas>
</div>
    </td>
    <td>
<br />
<asp:Image ID="imgCaptured" runat="server" Width="220" Height="225" />
       <%--<input type="hidden" id="imgData" runat="server" clientidmode="Static" />--%>
        <input type="hidden" name="imgData" id="imgData" />
    </td>
</tr>
               <tr>
                   <td>
                        <%--<asp:Button ID="BtnCapture" runat="server" BorderStyle="None" CssClass="btn" 
    OnClick="capture();" 
    style="width:50px; height:20px; margin-left:4px; margin-top:5px; font-size:12px; " 
                     Text="Capture" /> --%> 
                       <button type="button" onclick="capture()">Capture</button>
                       <%--<asp:Button ID="BtnCloseWebCam" runat="server" BorderStyle="None" CssClass="btn" 
    OnClick="closeCamera();hide('webCam');hide('Upload'); hide('divUploadMessage'); show('index');hidesel();" 
    style="width:50px; height:20px; margin-left:4px; margin-top:5px; font-size:12px; " 
                     Text="Close" />--%>
    <button type="button" onclick="closeCamera();hide('webCam');hide('Upload'); hide('divUploadMessage'); show('index');hidesel();">Close</button>
                   </td>
                   <td>
                       <%-- <asp:DropDownList ID="DrpPhotoTypeWebCam" runat="server" Width="90" style="float:left" onchange="Setimgtyped(this)" >
                     <asp:ListItem>Photo</asp:ListItem>
                      <asp:ListItem >Signature</asp:ListItem>
                      <asp:ListItem >Document</asp:ListItem>
                      <asp:ListItem >Aadhar</asp:ListItem>
                      <asp:ListItem >Pancard</asp:ListItem>
                  </asp:DropDownList>--%>
                   <%--<button type="button" onclick="upload()">Upload</button>--%> 
                       <asp:Button ID="BtnWebCamUpload" runat="server" BorderStyle="None" CssClass="btn" 
                     style="width:50px; height:23px; margin-left:4px; margin-top:5px; font-size:12px; " 
                     Text="Upload"  />   
                   </td>
                    <td>

                       </td>
                   <td></td>
               </tr>
          
          

           
   </table>       
            
        </div>
         <div id="Upload" style="width:100%; height:100%;display:none; padding-left:5%;">
             <div style="width:300px;float:left">
            <%-- <asp:Image runat="server" ID="img_upload"  ImageUrl ="~/Images/no_photo.jpg" Width="150" Height="250" />
           --%>  <img src="~/Images/no_photo.jpg" alt="" id="img_upload"  />
                 </div>
             <div style="width:200px; float:left; display:none;" >
              <%--<img ID="imgCapture" runat="server"  src="~/Images/ClearBtn.png"  />--%>
                 </div>
              <div  style="width:100%; float:left; margin-top:15px;"  >
                  <asp:DropDownList ID="Ddl_phototype" runat="server" Width="100" style="float:left" onchange="Setimgtyped(this)" >
                     <asp:ListItem>Photo</asp:ListItem>
                      <asp:ListItem >Signature</asp:ListItem>
                      <asp:ListItem >Document</asp:ListItem>
                      <asp:ListItem >Aadhar</asp:ListItem>
                      <asp:ListItem >Pancard</asp:ListItem>
                  </asp:DropDownList>

                   <input id="btnUpload" type="button" value="Upload Photo" style="float:left; margin-left:15px;" />

                  <input id="but_Cancel" type="button" value="Close" style="float:left; margin-left:10px;" onclick="hide('Upload'); hide('divUploadMessage'); show('index');hidesel();" />
             </div>
             <div  style="width:100%; float:left;" >
                     <div id="divUploadMessage" style="padding-top:4px;display:none"></div>
                   <div id="divUploadProgress" style="padding-top:4px;display:none">
                    <span style="font-size:smaller">Uploading photo...</span>
                    <div>
                        <table border="0" cellpadding="0" cellspacing="2" style="width:20%">
                            <tbody>
                                <tr>
                                    <td id="tdProgress1">&nbsp;</td>
                                    <td id="tdProgress2">&nbsp; </td>
                                    <td id="tdProgress3">&nbsp;</td>
                                    <td id="tdProgress4">&nbsp; </td>
                                    <td id="tdProgress5">&nbsp;</td>
                                    <td id="tdProgress6">&nbsp; </td>
                                    <td id="tdProgress7">&nbsp; </td>
                                   
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
             </div>
            

         </div>
        
         </div>
       
        
    
    <div id="IDprofSearch" class="popup" style="width:400px;height:300px; margin-left:75%; margin-top:300px; overflow:hidden;">
        <div></div>
    </div>
    
   
<div  style="width: 100%;" onkeypress="return WebForm_FireDefaultButton(event, '<%= CmdSave.ClientID %>')" >                
	<div id="Individual"  >
    <table width="100%"  class="sample">
      <tr>
        <td width="2%">&nbsp;</td>        
        <td width="13%">
            <label>
            Customer ID</label></td>
        <td width="9%" colspan="2" style="width: 34%">
            <asp:TextBox ID="TxtCustID" runat="server" autocomplete="off" AutoPostBack="True" CssClass="twitterStyleTextbox" meta:resourcekey="TxtCustIDResource1" Width="75%"></asp:TextBox>
            <asp:ImageButton ID="ImgSrch" runat="server" Height="20px" ImageUrl="~/images/Search.png" Width="23px"  />
            &nbsp;</td>
        <td class="style1">&nbsp;</td>
        <td width="14%">
            <label>
            Admission Branch<label class="mfcolor">*</label></label></td>
        <td colspan="4" style="width: 24%">
            <asp:DropDownList ID="CmbAdmBranch" runat="server" Enabled="False" meta:resourcekey="CmbAdmBranchResource1" TabIndex="11" Width="100%">
            </asp:DropDownList>
          </td>
        <td width="2%">&nbsp;</td>        
      </tr>
        <tr>
            <td width="2%">&nbsp;</td>
            <td width="13%">
                <label>
                Admission Date</label></td>
            <td colspan="2" style="width: 34%" width="9%">
               
                <asp:TextBox ID="TxtDoa" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtDoaResource1" TabIndex="1" Width="75%"></asp:TextBox>
                    <asp:Image ID="ImgDoa" runat="server"  BorderStyle="Outset" Height="20px" ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="24px" style="margin-top:10px;" />
               
            </td>
            <td class="style1">
                <label>
                
                </label>
            </td>
            <td width="14%">
                <label>
                Occupation</label></td>
            <td colspan="4" style="width: 24%">
                <asp:DropDownList ID="CmbOccupation" runat="server" meta:resourcekey="CmbOccupationResource1" TabIndex="12" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">&nbsp;</td>
        </tr>
        <tr>
            <td width="2%">&nbsp;</td>
            <td width="13%">
                <label>
                Customer Type</label></td>
            <td width="9%" colspan="2" style="width: 34%">
                <asp:DropDownList ID="cmbCustomerType" runat="server" meta:resourcekey="cmbCustomerTypeResource1" TabIndex="2" Width="99%" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td class="style1">&nbsp;</td>
            <td width="14%">
                <label>
                Education</label></td>
            <td colspan="4" style="width: 24%">
                <asp:DropDownList ID="CmbEducation" runat="server" meta:resourcekey="CmbEducationResource1" TabIndex="13" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">&nbsp;</td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td width="13%">
                <label>
                Customer Category&nbsp;
                </label>
                <label class="mfcolor">
                &nbsp;</label></td>
            <td colspan="2">
                <asp:DropDownList ID="CmbCustomerCatg" runat="server" meta:resourcekey="CmbCustomerCatgResource1" TabIndex="3" Width="99%">
                </asp:DropDownList>
            </td>
            <td class="style1"></td>
            <td width="14%">
                <label>
                 Maritial Status</label></td>
            <td colspan="2" width="16%">
                <asp:DropDownList ID="CmbMaritialStatus" runat="server" meta:resourcekey="CmbMaritialStatusResource1" TabIndex="14" Width="90%">
                </asp:DropDownList>
            </td>
            <td width="8%">
                <label>
                Gender<label class="mfcolor">*</label></label></td>
            <td width="8%">
                <asp:DropDownList ID="CmbGender" runat="server" meta:resourcekey="CmbGenderResource1" TabIndex="15" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%"></td>
        </tr>
      <tr>
        <td></td>
        <td >
            <label>
            Name<label class="mfcolor">*</label></label></td>
        <td width="9%">
                <asp:DropDownList ID="CmbTitle" runat="server" meta:resourcekey="CmbTitleResource1" TabIndex="4" Width="90%">
                </asp:DropDownList>
            </td>
            <td width="25%">
                <asp:TextBox ID="TxtName" runat="server" autocomplete="off" CssClass="twitterStyleTextbox" meta:resourcekey="TxtNameResource1" TabIndex="5" Width="97%" style="text-transform:uppercase"></asp:TextBox>
            </td>
        <td class="style1"></td>
        <td><label>Date of Birth<label class="mfcolor">*</label></label></td>
        <td width = "13%">
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtDob" placeholder="DD/MM/YYYY" runat="server" Width="95%"  OnChange="CalculateAge(this);"
                meta:resourcekey="TxtDobResource1" TabIndex="16" autocomplete="off"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="TxtDob">
        </asp:CalendarExtender>
                
        </td>
        <td  width = "2%"><asp:Image ID="Image3" runat="server" BorderStyle="Outset" 
                Height="19px" ImageUrl="~/Images/Cal.jpg" Width="18px" 
                meta:resourcekey="Image3Resource1" /></td>
        <td><label>Age</label></td>
    	<td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAge" runat="server" 
                Width="90%" onkeypress="return onlyNumbers(event)" onkeydown="return textboxMultilineMaxNumber(this,5);"
                onblur="return CheckMinor();" meta:resourcekey="TxtAgeResource1"  ReadOnly="true"
                TabIndex="17" AutoPostBack="True"></asp:TextBox></td>
        <td></td>
      </tr>
      <div id="DivRelDob" runat="server">
        </div>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
               Relative Name</label></td>
                  <td>
            <asp:DropDownList ID="CmbRelType" runat="server" Width="90%" 
                meta:resourcekey="CmbRelTypeResource1" TabIndex="6">
            </asp:DropDownList>            
	    </td>
        <td>            
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtRelName" runat="server" Width="97%" 
                meta:resourcekey="TxtRelNameResource1" TabIndex="7" autocomplete="off" style="text-transform:uppercase"></asp:TextBox>
        </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                <label>
                Religion<label class="mfcolor">*</label></label></td>
            <td colspan="2">
                <asp:DropDownList ID="CmbReligion" runat="server" AutoPostBack="True" 
                    meta:resourcekey="CmbReligionResource1" TabIndex="17" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <label>
                Caste</label></td>
            <td>
                <asp:DropDownList ID="CmbMainCaste" runat="server" 
                    meta:resourcekey="CmbMainCasteResource1" TabIndex="18" Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>

        </tr>
      <tr>
        <td></td>
        <td>
             <label>
                Introduced by</label><label class="mfcolor">*</label>
          </td>
        <td colspan="2">
            <asp:TextBox ID="TxtIntroID" runat="server" autocomplete="off" AutoPostBack="True" CssClass="twitterStyleTextbox" meta:resourcekey="TxtIntroIDResource1" TabIndex="8" Width="22%"></asp:TextBox>
            <asp:TextBox ID="TxtIntroNm" runat="server" autocomplete="off" CssClass="twitterStyleTextbox" meta:resourcekey="TxtIntroNmResource1" TabIndex="9" Width="65%"  style="margin-left:2%" ></asp:TextBox>
            <asp:ImageButton ID="ImgSrch1" runat="server" Height="24px" ImageUrl="~/images/Search.png" Width="23px" />
          </td>
        <td class="style1">
            &nbsp;</td>
        <td >
           
            
            <label>
          Financial Status</label></td>
        <td colspan="2">
            <asp:DropDownList ID="CmbSubCaste" runat="server" AutoPostBack="True" 
                meta:resourcekey="CmbSubCasteResource1" TabIndex="19" Width="100%" >
            </asp:DropDownList>
        </td>
        <td>
            <label>
            Blood Group</label></td>
        <td >
	        <asp:DropDownList ID="CmbBloodGrp" runat="server" AutoPostBack="True" meta:resourcekey="CmbBloodGrpResource1" TabIndex="20" Width="100%">
            </asp:DropDownList>
        </td>
        <td ></td>
      </tr>
          <tr>
            <td>
                &nbsp;</td>
            <td><label>Mother Name</label></td>
            <td colspan="2">
                <asp:TextBox ID="TxtMotherName" runat="server" autocomplete="off" CssClass="twitterStyleTextbox" meta:resourcekey="TxtIntroIDResource1" TabIndex="8"  Width="98%" ></asp:TextBox>
            </td>
            <td class="style1"> &nbsp;
                </td>
            <td><label>Last Modified Details</label>
               </td>
            <td colspan="2">      <asp:TextBox ID="TxtLastModiDate" runat="server" autocomplete="off" CssClass="twitterStyleTextbox" Width="208%" ReadOnly ="true"  TextMode="MultiLine"  ></asp:TextBox>       </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td>
                &nbsp;</td>
            <td><label>State</label></td>
            <td colspan="2">
                <asp:DropDownList ID="CmbState" AutoPostBack="true" Width="100%" runat="server">
                </asp:DropDownList>
            </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                District</label> &nbsp;</td>
            <td colspan="2">
                <asp:DropDownList ID="CmbDist" runat="server" AutoPostBack="True" Width="100%">
                </asp:DropDownList>
            </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="2">
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
        <td>
          <label>Location</label>
        </td>
        <td colspan="2" >
            <asp:DropDownList ID="CmbLocation" runat="server" 
                meta:resourcekey="CmbLocationResource1" TabIndex="10" Width="100%" 
                AutoPostBack="True">
            </asp:DropDownList>
        </td>
        
        <td >
            &nbsp;</td>
        <td>
          
           
            &nbsp;</td>
        <td colspan="4">
            &nbsp;</td>
        <td></td>
    </tr>

</table>

<div id ="DivGroup" runat ="server" visible ="false" >
    <table width="100%" border="0.5" class="sample">
      <tr>
        <td width="2%"></td>        
        <td width="14%"> </td>
        <td width="8%">
             </td>
        <td width="25%">                
             </td>
        <td width="3%"></td>
        <td width="14%"> </td>
        <td width="16%" colspan="2">
             </td>
        <td width="8%">  </td>
        <td width="8%">
             </td>
        <td width="2%"></td>        
      </tr>

         <tr>
        <td></td>
        <td><label>Grpup Details</label></td>
        <td colspan="2">
            <asp:DropDownList ID="CmbGroup" runat="server" Width="100%" 
                meta:resourcekey="CmbLocationResource1" TabIndex="10">
            </asp:DropDownList>
        </td>
        <td></td>
        <td> </td>
        <td colspan="2">
             </td>
        <td> </td>
        <td>
             </td>
        <td></td>
    </tr>
    </table>
</div>

    <div id = "GuardianDiv" class="floatingDivMinor" runat="server" style="left: 57%;top : 52%;width:30% ">                
            <table width="100%" bgcolor="#DCEBFC" border="0" bordercolor ="#DCEBFC">
                                      
              <tr>
                <td width="3%"> </td>
                <td width="37%"></td>
                <td width="3%"></td>
                <td width="54%"></td>
                <td width="3%">
                    <img id="ImgGuardianClose" alt="" src="Images/CloseIcon.png" style="width: 16px; height: 10px"  onclick ="hide('GuardianDiv')" />    
                </td>
              </tr>
              <tr>
                <td> </td>
                <td><label>Guardian Cust ID</label></td>
                <td><label>:</label></td>
                <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtGuardianID" runat="server" Width ="100%" meta:resourcekey="TextBox8Resource1"></asp:TextBox></td>
                <td></td>
              </tr>
              <tr>
                <td> </td>
                <td><label>Guardian Name</label></td>
                <td><label>:</label></td>
                <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtGuardianNm" runat="server" Width ="100%" meta:resourcekey="TextBox9Resource1"></asp:TextBox></td>
                <td> </td>
              </tr>
              <tr>
                <td> </td>
                <td><label>Guardian Relation</label></td>
                <td><label>:</label></td>
                <td><asp:DropDownList ID="CmbGuardianRel" runat="server" Width="100%" meta:resourcekey="DropDownList2Resource1"> </asp:DropDownList></td>
                <td> </td>
              </tr>
            </table>
    </div>
<br />

<div class="sample" id = "Address">
	<table width="100%" border="0" >
      <tr>
        <td width="2%" ></td>
        <td width="48%" align="center" bgcolor="#92D0F5">
            <asp:label ID="Label1" runat="server" 
                Font-Bold="True" Font-Italic="True" ForeColor="White" Font-Size="Small" 
                meta:resourcekey="Label1Resource1">PRESENT 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ADDRESS</asp:label></td>
        <td width="3%" bgcolor ="#92D0F5"></td>
        <td width="40%" align="center" bgcolor ="#92D0F5" border-top-color="#92D0F5"  border-right-color="#92D0F5"  border-bottom-color="#92D0F5" border-left-color="#92D0F5">
            <asp:label ID="Label2" runat="server" 
                Font-Bold="True" Font-Italic="True" ForeColor="White" Font-Size="Small" 
                meta:resourcekey="Label2Resource1">PERMANENT 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ADDRESS</asp:label>
                </td>
        <td width="5%" align="center" bgcolor="#92D0F5">
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                Font-Italic="True" ForeColor="White" OnClientClick = "Update();" 
                meta:resourcekey="LinkButton1Resource1" TabIndex="28">Copy</asp:LinkButton>
          </td>      
          
        <td width="2%"></td>
      </tr>
	</table>
</DIV>	
<div>
  
	<table width="100%" border="0" class="sample">
      <tr>
        <td width="2%"></td>
        <td width="14%"><label>House No</label></td>
        <td width="33%">
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPresHouseNo" runat="server" Width="100%" 
                meta:resourcekey="TxtPresHouseNoResource1" TabIndex="21"></asp:TextBox></td>
        <td width="3%"></td>
        <td width="14%"><label>House No</label></td>
        <td width="31%">
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPermHouseNo" runat="server" Width="100%" 
                meta:resourcekey="TxtPermHouseNoResource1" TabIndex="29"></asp:TextBox></td>
        <td width="2%"></td>
      </tr>
      <tr>
        <td></td>
        <td><label>House Name<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPresHouseName" runat="server" Width="100%" 
                meta:resourcekey="TxtPresHouseNameResource1" TabIndex="22" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
        <td><label>House Name<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPermHouseName" runat="server" Width="100%" 
                meta:resourcekey="TxtPermHouseNameResource1" TabIndex="30" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
      </tr>
      <tr>
        <td></td>
        <td><label>Post office<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPresPlace" runat="server" Width="100%" 
                meta:resourcekey="TxtPresPlaceResource1" TabIndex="23" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
        <td><label>Post office<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPermPlace" runat="server" Width="100%" 
                meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
      </tr>
       <tr>
            <td>&nbsp;</td>
            <td><label>Ward<label class="mfcolor"></label></label></td>
            <td>
                <asp:TextBox ID="TxtPresward" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" Width="100%" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td><label>Ward<label class="mfcolor"></label></label></td>
            <td>
                <asp:TextBox ID="TxtPremward" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" Width="100%" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><label>Panchayat<label class="mfcolor"></label></label></td>
            <td>
                <asp:TextBox ID="TxtPrespancynt" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" Width="100%" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td><label>Panchayat<label class="mfcolor"></label></label></td>
            <td>
                <asp:TextBox ID="TxtPrempancynt" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" Width="100%" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><label>Village<label class="mfcolor">*</label></label></td>
            <td>
                <asp:TextBox ID="txtPresvillage" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" Width="100%" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td><label>Village<label class="mfcolor">*</label></label></td>
            <td>
                <asp:TextBox ID="TxtPermvillage" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" Width="100%" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><label>Taluk<label class="mfcolor">*</label></label></td>
            <td>
                <asp:TextBox ID="TxtPrestaluk" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" Width="100%" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td><label>Taluk<label class="mfcolor">*</label></label></td>
            <td>
                <asp:TextBox ID="TxtPermtaluk" runat="server" CssClass="twitterStyleTextbox" meta:resourcekey="TxtPermPlaceResource1" TabIndex="31" Width="100%" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
      <tr>
        <td></td>
        <td><label>District<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPresDist" runat="server" Width="100%" 
                meta:resourcekey="TxtPresDistResource1" TabIndex="24" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
        <td><label>District<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPermDist" runat="server" Width="100%" 
                meta:resourcekey="TxtPermDistResource1" TabIndex="32" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
      </tr>
      <tr>
        <td></td>
        <td><label>State</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPresState" runat="server" Width="100%" 
                meta:resourcekey="TxtPresStateResource1" TabIndex="25" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
        <td><label>State</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPermState" runat="server" Width="100%" 
                meta:resourcekey="TxtPermStateResource1" TabIndex="33" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
      </tr>
      <tr>
        <td></td>
        <td><label>Country</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPresCountry" runat="server" Width="100%" 
                meta:resourcekey="TxtPresCountryResource1" TabIndex="25" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
        <td><label>Country</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPermCountry" runat="server" Width="100%" 
                meta:resourcekey="TxtPermCountryResource1" TabIndex="34" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
      </tr>
      <tr>
        <td></td>
        <td><label>Pin Code<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPresPin" runat="server" Width="100%" 
                meta:resourcekey="TxtPresPinResource1" TabIndex="26" Onchange = "PinCodevalid()"  ></asp:TextBox></td>
        <td></td>
        <td><label>Pin Code<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPermPin" runat="server" Width="100%" 
                meta:resourcekey="TxtPermPinResource1" TabIndex="35" Onchange = "PinCodevalid()" ></asp:TextBox></td>
        <td></td>
      </tr>
      <tr>
        <td></td>
        <td><label>Land Mark</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPresLand" runat="server" Width="100%" 
                meta:resourcekey="TxtPresLandResource1" TabIndex="27" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
        <td><label>Land Mark</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPermLand" runat="server" Width="100%" 
                meta:resourcekey="TxtPermLandResource1" TabIndex="36" style="text-transform:uppercase"></asp:TextBox></td>
        <td></td>
      </tr>
	</table>  	
  </div>
  <br/>
  
	<DIV class="sample" id="OtherDtls"  >
    <table width="100%" border="0.5" class="sample">
      <tr>
        <td width="2%"></td>
        <td width="14%"><label>Land Phone No</label></td>
        <td width="33%">
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtLandPhone" runat="server" Width="100%" 
                meta:resourcekey="TxtLandPhoneResource1" TabIndex="37"></asp:TextBox></td>
        <td width="3%"></td>
        <td width="14%"><label>Mobile No<label class="mfcolor">*</label></label></td>
        <td width="31%">
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtMobile" runat="server" Width="70%" 
                meta:resourcekey="TxtMobileResource1" TabIndex="38" style="float:left" onkeypress="return onlyNumbers(event)"  onchange="MobileNochange()" onfocusout="phonenumber(this)"></asp:TextBox>
             <div style="float:left;" id="div_Verify">
                 <asp:Button ID="But_verfiy" runat="server" BorderStyle="None" CssClass="btn" OnClientClick="return MobileNovaliadation()" 
                     style="width:50px; height:20px; margin-left:4px; margin-top:5px; font-size:12px; " 
                     Text="Verify" />
            </div>
           <img src="Images/opc-ajax-loader.GIF" id="img_process" style=" float:left; display:none;"   />
            <img src="Images/verify.jpg" style=" float:left; display:none;" id="imgVerify" />
           <img src="Images/Verifyerror.jpg" style=" float:left;display:none;" id="imgverifyError" />
           <asp:ImageButton ID="ImgEditNo" runat="server" onclientclick="return ChangeNumber()" ImageUrl="Images/Edit_Mob.png"  style=" float:left; visibility:hidden;"   />
        </td>
        <td width="2%"></td>
      </tr>
      <tr>
        <td></td>
        <td><label>Email</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtEmail" runat="server" Width="100%" 
                meta:resourcekey="TxtEmailResource1" TabIndex="39" onchange="checkEmail()" ></asp:TextBox></td>
        <td></td>
        <td><label>CKYC No</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtFax" runat="server" Width="100%" 
                meta:resourcekey="TxtFaxResource1" TabIndex="40"></asp:TextBox></td>
        <td></td>
      </tr>
      <tr>
        <td></td>
        <td><label>Web address</label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtWebAdd" runat="server" Width="100%" 
                meta:resourcekey="TxtWebAddResource1" TabIndex="41"></asp:TextBox></td>
        <td></td>
        <td>
            <label>
            Adhaar No</label></td>
        <td>
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAdhaarNo" runat="server" Width="100%" 
                meta:resourcekey="TxtAdhaarNoResource1" TabIndex="42"></asp:TextBox>
             <asp:RegularExpressionValidator 
    ID="revName"
    runat="server"
    ControlToValidate="TxtAdhaarNo"
    ValidationExpression="^[a-zA-Z0-9 ]*$"
    ErrorMessage="Special characters not allowed"
    ForeColor="Red" />
          </td>
        <td></td>
      </tr>            
      <tr>
        <td></td>
        <td><label>Photo Identity Proof<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPhotoIDProof" runat="server" Width="88%" 
                meta:resourcekey="TxtPhotoIDProofResource1" TabIndex="43"   AutoPostBack="true"></asp:TextBox>
            <img id="imgIDProof1" alt="" src="Images/id.png"  onclick="show('cover');show('IDPRoof1Div');"
                style="width: 25px; height: 15px" />
        </td>
        <td>    
        
        <div id = "IDPRoof1Div" class="floatingDiv" style="left: 17%;">            
        <table width="100%" bgcolor="#DCEBFC" border="0" bordercolor ="#DCEBFC">
          <tr>
            <td width="3%"> </td>
            <td width="37%"></td>
            <td width="3%"></td>
            <td width="54%">
            </td>
            <td width="3%">
                <img id="ImgIDClose1" alt="" src="Images/CloseIcon.png" style="width: 16px; "  onclick="hide('cover');hide('IDPRoof1Div');" />    
            </td>
          </tr>
            
          <tr>
            <td> </td>
            
            
            <td><label>Identity Proof</label></td>
            <td><label>:</label></td>
            <td>
                <asp:DropDownList ID="CmbPhotoIDProof" runat="server" Width="100%" 
                    meta:resourcekey="CmbIDProofResource1" >
                </asp:DropDownList>
            </td>
            <td></td>
          </tr>
             <tr>
            <td> </td>
            
            
            <td><label>Type of Proof</label></td>
            <td><label>:</label></td>
            <td>
                <asp:RadioButtonList ID="radio_PhotoProof" runat="server"   RepeatDirection="Horizontal">
                    <asp:ListItem Value="O">Original</asp:ListItem>
                      <asp:ListItem Selected Value="D">Copy</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Number</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPhotoIDNoProof" runat="server" Width ="100%" 
                    meta:resourcekey="TxtIDNoResource1"  onblur="GetIDproofNo(this)" onchange="ValidatePAN1(this)" ></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Issuing Authority</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPhotoIssAuth" runat="server" Width ="100%" 
                    meta:resourcekey="TxtIssAuthResource1"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Identity Details</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPhotoIdentityDtls" runat="server" Width ="100%" 
                    meta:resourcekey="TextBox7Resource1"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Issuing Date</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPhotoIssDate" runat="server" Width ="100%" 
                    meta:resourcekey="TxtIssDateResource1"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Expiry Date</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPhotoExpiryDate" runat="server" Width ="100%" 
                    meta:resourcekey="TxtExpiryDateResource1"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Remarks</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtPhotoRemarks" runat="server" Width ="100%" 
                    meta:resourcekey="TxtIDProofRemarksResource1"></asp:TextBox></td>
            <td> </td>
          </tr>

        </table>
        </div>
        
        
        </td>
        <td><label>Address ID Proof<label class="mfcolor">*</label></label></td>
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAddIDProof" runat="server" Width="88%" 
                meta:resourcekey="TxtAddIDProofResource1" TabIndex="44" 
                AutoPostBack="True"></asp:TextBox>
            <img id="imgIDProof2" alt="" src="Images/id.png"  onclick="show('cover');show('IDPRoof2Div');"
                style="width: 25px; "/></td>


        <td>
        <div id = "IDPRoof2Div" class="floatingDiv" style="left: 50%;">                
        <table width="100%" bgcolor="#DCEBFC" border="0" bordercolor ="#DCEBFC">
          <tr>
            <td width="3%"> </td>
            <td width="37%"></td>
            <td width="3%"></td>
            <td width="54%">
            </td>
            <td width="3%">
                <img id="ImgIDClose2" alt="" src="Images/CloseIcon.png" style="width: 16px; height: 10px"  onclick="hide('cover');hide('IDPRoof2Div');" />    
            </td>
          </tr>
          <tr>
            <td> </td>
            
            
            <td><label>Identity Proof</label></td>
            <td><label>:</label></td>
            <td>
                <asp:DropDownList ID="CmbAddProof" runat="server" Width="100%" 
                    meta:resourcekey="CmbAddProofResource1">
                </asp:DropDownList>
            </td>
            <td></td>
          </tr>
             <tr>
            <td> </td>
            
            
            <td><label>Type of ID Proof</label></td>
            <td><label>:</label></td>
            <td>
                <asp:RadioButtonList ID="RadioIDproofType" runat="server"  RepeatDirection="Horizontal" >
                    <asp:ListItem Value="O">Original</asp:ListItem>
                      <asp:ListItem Selected Value="D">Copy</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Number</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAddProofNo" runat="server" Width ="100%" 
                    meta:resourcekey="TextBox1Resource1"  onblur="GetADDproofNo(this)"  onchange="ValidatePAN(this)"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Issuing Authority</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAddIssAuth" runat="server" Width ="100%" 
                    meta:resourcekey="TextBox2Resource1"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Itentity Details</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAddIdentittyDtls" runat="server" Width ="100%" 
                    meta:resourcekey="TextBox6Resource1"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Issuing Date</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAddIssDate" runat="server" Width ="100%" 
                    meta:resourcekey="TextBox3Resource1"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Expiry Date</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAddExpiryDate" runat="server" Width ="100%" 
                    meta:resourcekey="TextBox4Resource1"></asp:TextBox></td>
            <td> </td>
          </tr>
          <tr>
            <td> </td>
            <td><label>Remarks</label></td>
            <td><label>:</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtAddRemarks" runat="server" Width ="100%" 
                    meta:resourcekey="TextBox5Resource1"></asp:TextBox></td>
            <td> </td>
          </tr>

        </table>
        </div>
        </td>
      </tr>
      
      <tr>
        <td></td>
        <td>
            <table border="0.5" class="sample" width="100%">
                <tr>
                    <td>
                        <label>
                        Customer Grading</label></td>
                </tr>
            </table>
          </td>
        <td>
            <asp:TextBox  CssClass="twitterStyleTextbox"  ID="DtpGrading" runat="server" Width="20%" 
                meta:resourcekey="DtpGradingResource1" TabIndex="45"></asp:TextBox>
    
            <asp:DropDownList ID="CmbGrading" runat="server" Width="76%" 
                meta:resourcekey="CmbGradingResource1" TabIndex="46">
            </asp:DropDownList>
          </td>
        <td></td>
        <td><label>Remarks</label></td>       
        <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtRemarks" runat="server" Width="100%" 
                meta:resourcekey="TxtRemarksResource1" TabIndex="47"></asp:TextBox></td>
        <td></td>
      </tr>
        <tr>
            <td>
                 </td>
            <td>
                 </td>
            <td>
                <div class="documentbut" style="margin-left:1%; width:50%"  onclick="show('cover');show('popup_Document');" >Image Upload</div>
                 </td>
            <td>
                 </td>
            <td>
                 </td>
            <td>
                 </td>
            <td>
                 </td>
        </tr>
</table>
    </DIV>	
    <div id ="OtherDtls" runat="server" visible="false">
	    <table border="0.5" class="sample" width="100%">
            <tr>
                <td width="2%">
                </td>
                <td width="14%">
                    <label>
                    Nationality</label></td>
                <td width="33%">
                    <asp:DropDownList ID="CmbNationality" runat="server" Width="100%" 
                        meta:resourcekey="CmbNationalityResource1" TabIndex="48">
                    </asp:DropDownList>
                </td>
                <td width="3%">
                </td>
                <td width="14%">
                     </td>
                <td width="31%">
                     </td>
                <td width="2%">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <label>
                    Identification Mark</label></td>
                <td>
                    <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtIdentityMark" runat="server" Width="100%" 
                        meta:resourcekey="TxtIdentityMarkResource1" TabIndex="49" style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <label>
                    Income Range</label></td>
                <td>
                    <asp:DropDownList ID="CmbIncomeRange" runat="server" Width="100%" 
                        meta:resourcekey="CmbIncomeRangeResource1" TabIndex="50">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <label>
                    Disablity</label></td>
                <td>
                    <asp:DropDownList ID="CmbDisability" runat="server" Width="100%" 
                        meta:resourcekey="CmbDisabilityResource1" TabIndex="51">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                    <label>
                    NRI</label></td>
                <td>
                    <asp:DropDownList ID="CmbNRI" runat="server" Width="100%" 
                        meta:resourcekey="CmbNRIResource1" TabIndex="52">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <label>
                    IT Document</label></td>
                <td>
                    <asp:DropDownList ID="CmbITDoc" runat="server" Width="48%" 
                        meta:resourcekey="CmbITDocResource1" TabIndex="53">
                    </asp:DropDownList>
 <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtITDocNo" runat="server" Width="46%" 
                        meta:resourcekey="TxtITDocNoResource1" TabIndex="54"></asp:TextBox>&nbsp;</td>
                <td>
                    <div id="IDPRoof1Div0" class="floatingDiv" style="left: 17%;">
                        <table bgcolor="#DCEBFC" border="0" bordercolor="#DCEBFC" width="100%">
                            <tr>
                                <td width="3%">
                                     </td>
                                <td width="37%">
                                </td>
                                <td width="3%">
                                </td>
                                <td width="54%">
                                </td>
                                <td width="3%">
                                    <img id="ImgIDClose3" alt="" onclick="this.close" src="Images/CloseIcon.png" 
                                        style="width: 16px; height: 10px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     </td>
                                <td>
                                    <label>
                                    Identity Proof</label></td>
                                <td>
                                    <label>
                                    :</label></td>
                                <td>
                                    <asp:DropDownList ID="CmbIDProofAdd" runat="server" Width="100%" 
                                        meta:resourcekey="CmbIDProof0Resource1">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     </td>
                                <td>
                                    <label>
                                    Number</label></td>
                                <td>
                                    <label>
                                    :</label></td>
                                <td>
                                    <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtIDNo0" runat="server" Width="100%" 
                                        meta:resourcekey="TxtIDNo0Resource1" ></asp:TextBox>
                                </td>
                                <td>
                                     </td>
                            </tr>
                            <tr>
                                <td>
                                     </td>
                                <td>
                                    <label>
                                    Issuing Authority</label></td>
                                <td>
                                    <label>
                                    :</label></td>
                                <td>
                                    <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtIssAuth0" runat="server" Width="100%" 
                                        meta:resourcekey="TxtIssAuth0Resource1"></asp:TextBox>
                                </td>
                                <td>
                                     </td>
                            </tr>
                            <tr>
                                <td>
                                     </td>
                                <td>
                                    <label>
                                    Identity Details</label></td>
                                <td>
                                    <label>
                                    :</label></td>
                                <td>
                                    <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TextBox16" runat="server" Width="100%" 
                                        meta:resourcekey="TextBox16Resource1"></asp:TextBox>
                                </td>
                                <td>
                                     </td>
                            </tr>
                            <tr>
                                <td>
                                     </td>
                                <td>
                                    <label>
                                    Issuing Date</label></td>
                                <td>
                                    <label>
                                    :</label></td>
                                <td>
                                    <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtIssDate0" runat="server" Width="100%" 
                                        meta:resourcekey="TxtIssDate0Resource1"></asp:TextBox>
                                </td>
                                <td>
                                     </td>
                            </tr>
                            <tr>
                                <td>
                                     </td>
                                <td>
                                    <label>
                                    Expiry Date</label></td>
                                <td>
                                    <label>
                                    :</label></td>
                                <td>
                                    <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtExpiryDate0" runat="server" Width="100%" 
                                        meta:resourcekey="TxtExpiryDate0Resource1"></asp:TextBox>
                                </td>
                                <td>
                                     </td>
                            </tr>
                            <tr>
                                <td>
                                     </td>
                                <td>
                                    <label>
                                    Remarks</label></td>
                                <td>
                                    <label>
                                    :</label></td>
                                <td>
                                    <asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtIDProofRemarks0" runat="server" Width="100%" 
                                        meta:resourcekey="TxtIDProofRemarks0Resource1"></asp:TextBox>
                                </td>
                                <td>
                                     </td>
                            </tr>
                        </table>
                    </div>
                    <div id="IDPRoof2Div0" class="floatingDiv" style="left: 64%;">
                        <table bgcolor="#DCEBFC" border="0" bordercolor="#DCEBFC" width="100%">
                            <tr>
                                <td width="3%">
                                    &nbsp;</td>
                                <td width="37%"></td>
                                <td width="3%"></td>
                                <td width="54%"></td>
                                <td width="3%"><img id="ImgIDClose4" alt="" onclick="this.close" src="Images/CloseIcon.png" style="width: 16px; height: 10px" /></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td><label>Identity Proof</label></td>
                                <td><label>:</label></td>
                                <td><asp:DropDownList ID="DropDownList3" runat="server" Width="100%" meta:resourcekey="DropDownList3Resource1"></asp:DropDownList></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td><label>Number</label></td>
                                <td><label>:</label></td>
                                <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TextBox17" runat="server" Width="100%" meta:resourcekey="TextBox17Resource1"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td><label>Issuing Authority</label></td>
                                <td><label>:</label></td>
                                <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TextBox18" runat="server" Width="100%" meta:resourcekey="TextBox18Resource1"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td><label>Itentity Details</label></td>
                                <td><label>:</label></td>
                                <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TextBox19" runat="server" Width="100%" meta:resourcekey="TextBox19Resource1"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td><label>Issuing Date</label></td>
                                <td><label>:</label></td>
                                <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TextBox20" runat="server" Width="100%" meta:resourcekey="TextBox20Resource1"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td><label>Expiry Date</label></td>
                                <td><label>:</label></td>
                                <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TextBox21" runat="server" Width="100%" meta:resourcekey="TextBox21Resource1"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td><label>Remarks</label></td>
                                <td><label>:</label></td>
                                <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TextBox22" runat="server" Width="100%" meta:resourcekey="TextBox22Resource1"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td><label>Currency</label></td>
                <td><asp:DropDownList ID="CmbCurrency" runat="server" Width="100%" 
                        meta:resourcekey="CmbCurrencyResource1" TabIndex="55"></asp:DropDownList></td>
                <td></td>
            </tr>                        
            <tr>
                <td> </td>
                <td> </td>
                <td> </td>
                <td> </td>
                <td> </td>
                <td> </td>
                <td> </td>
            </tr>
        </table>
    </div>
<%--    <div id="DivGroup0" runat="server" visible="false">
        GROUP DETAILS 
        <table width="100%">
     
            <tr>
                <td width="2%">
                     </td>
                <td width="14%">
                    <label>
                    Customer Group</label></td>
                <td width="12%">
                    <asp:DropDownList ID="CmbGroup" runat="server" 
                        TabIndex="500" Width="100%">

                    </asp:DropDownList>
                </td>
                <td width="2%">
                     </td>
                <td width="8%">
                     </td>
                <td width="12%">
                     </td>
                <td width="2%">
                     </td>
                <td width="14%">
                     </td>
                <td width="32%">
                     </td>
                <td width="2%">
                     </td>
            </tr>
        </table>
    </div>--%>

    <div id="DivModification" runat="server" visible="false">
        <legend>MODIFICATION DETAILS</legend> 
        <table width="100%">
            <tr>
                <td width="2%">&nbsp;</td>
                <td width="14%"><label>Letter No</label></td>
                <td width="12%">
                    <asp:TextBox ID="TxtLetterNO" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td width="2%">&nbsp;</td>
                <td width="8%"><label>Letter Date</label></td>
                <td width="12%">
                    <asp:TextBox ID="TxtLetterDate" runat="server" Width="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                        Format="dd/MM/yyyy" TargetControlID="TxtLetterDate">
                    </asp:CalendarExtender>
                </td>
                <td width="2%">&nbsp;</td>
                <td width="14%"><label>Modification Reason</label></td>
                <td width="32%">
                    <asp:TextBox ID="TxtChangeReason" runat="server" Width="100%" style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td width="2%">&nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="14%">
                    <label>
                    Customer Status</label></td>
                <td width="12%">
                    <asp:DropDownList ID="CmbStatus" runat="server" 
                        TabIndex="500" Width="100%">
                        <asp:ListItem>ACTIVE</asp:ListItem>
                        <asp:ListItem>INACTIVE</asp:ListItem>
                        <asp:ListItem>DEAD</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="2%">
                    &nbsp;</td>
                <td width="8%">
                    &nbsp;</td>
                <td width="12%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
                <td width="14%">
                    &nbsp;</td>
                <td width="32%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
        </table>
    </div>

    <div class="floatingDiv">
        <table width="100%" border="0">
          <tr>
            <td width="2%">&nbsp;</td>
            <td width="20%"><label>Village</label></td>
            <td width="24%"><asp:DropDownList ID="CmbVillage" runat="server" Width="100%"></asp:DropDownList></td>
            <td width="2%">&nbsp;</td>
            <td width="20%"><label>District</label></td>
            <td width="24%"><asp:DropDownList ID="CmbDistrict" runat="server" Width="100%"></asp:DropDownList></td>
            <td width="2%">&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td><label>Ward No</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtWardNo" runat="server" Width="100%"></asp:TextBox></td>
            <td>&nbsp;</td>
            <td><label>Post Office</label></td>
            <td><asp:DropDownList ID="CmbPostOffice" runat="server" Width="100%"></asp:DropDownList></td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td><label>Amsom</label></td>
            <td><asp:DropDownList ID="CmbAmsom" runat="server" Width="100%"></asp:DropDownList></td>
            <td>&nbsp;</td>
            <td><label>Desam</label></td>
            <td><asp:DropDownList ID="CmdDesam" runat="server" Width="100%"></asp:DropDownList></td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td><label>Block Name</label></td>
            <td><asp:DropDownList ID="CmbBlockName" runat="server" Width="100%"></asp:DropDownList></td>
            <td>&nbsp;</td>
            <td><label>Panchayat/Muncipaity/Corporation</label></td>
            <td></td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td><label>Tehsil</label></td>
            <td><asp:DropDownList ID="CmbTehsil" runat="server" Width="100%"></asp:DropDownList></td>
            <td>&nbsp;</td>
            <td><label>Type</label></td>
            <td><asp:DropDownList ID="CmbLocType" runat="server" Width="100%"></asp:DropDownList></td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td><label>STD Code</label></td>
            <td><asp:TextBox  CssClass="twitterStyleTextbox"  ID="TxtSTD" runat="server" Width="100%"></asp:TextBox></td>
            <td>&nbsp;</td>
            <td><label>Name</label></td>
            <td><asp:DropDownList ID="CmbLocName" runat="server" Width="100%"></asp:DropDownList></td>
            <td>&nbsp;</td>
          </tr>
        </table>
    </div>
	    <br />
	    

	    <table width="100%" style="height: 24px">
	        <tr>
	            <td width = "35%">
                    <asp:ImageButton ID="ImgImage" runat="server" ImageUrl="~/Images/Images.png" 
                        TabIndex="58" />
                </td>
	            <td width = "30%" align="center">
                    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                        meta:resourcekey="CmdSaveResource1" TabIndex="56"  />
                    <asp:ImageButton ID="butClear" runat="server" ImageUrl="~/Images/Clear.png" />
                    <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                        meta:resourcekey="CmdClearResource1" TabIndex="57" />
                </td>
	            <td width = "35%">
                    &nbsp;</td>
	        </tr>
	    </table>
	</div>
	<br/>	
          </ContentTemplate>
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TxtIntroID" EventName="TextChanged" />
        </Triggers>
<%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnWebCamUpload" />
        </Triggers>--%>
        </asp:UpdatePanel> 
       <%--     <script type="text/javascript">
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

                var dataurl = canvas.toDataURL("image/png");
               
                document.getElementById("<%= imgCaptured.ClientID %>").src = dataurl;
                document.getElementById("imgData").value = dataurl;
            }
            function upload() {
                var DataUrl = document.getElementById("imgData").value;
                console.log(DataUrl)
                if (!DataUrl) {
                    alert("Please capture image first");
                    return;
                }

                fetch("CustomerRegister.aspx/SaveImage", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    body: JSON.stringify({
                        imageBase64: DataUrl
                    })
                })
                .then(function (res) {
                    return res.json();
                })
                .then(function () {
                    alert("Image uploaded successfully");
                })
                .catch(function (err) {
                    console.error(err);
                    alert("Upload failed");
                });
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

</asp:Content>

