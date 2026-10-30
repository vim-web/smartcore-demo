Imports System.IO
Imports System.Drawing
Imports System.Web.UI.HtmlControls
Imports System.Drawing.Drawing2D
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO.MemoryStream

Public Class IMG_Upload
    Inherits System.Web.UI.Page
    Private Const SCRIPT_TEMPLATE As String = "<" + "script " + "type=""text/javascript"">window.parent.photoUploadComplete('{0}', {1});" + "<" + "/script" + ">"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            'Dim UpPath As String
            'UpPath = "C:\UploadedUserFiles"
            'If Not Directory.Exists(UpPath) Then
            '    Directory.CreateDirectory("C:\UploadedUserFiles\")
            'End If
            'Sleeping for 10 seconds, fake delay, You should not it try at home. 
            ' System.Threading.Thread.Sleep(3000)
            UploadPhoto()
        End If
    End Sub
    'Private Sub UploadPhoto()
    '    Dim script As String = String.Empty
    '    If (fiPhoto.PostedFile IsNot Nothing) AndAlso (fiPhoto.PostedFile.ContentLength > 0) Then
    '        If Not IsValidImageFile(fiPhoto) Then
    '            script = String.Format(SCRIPT_TEMPLATE, "The uploaded file is not a valid image file.", "true")
    '            'Else
    '            '    Session("imginputfile") = fiPhoto
    '        End If

    '        'Else
    '        '    If Session("imginputfile") IsNot Nothing Then
    '        '        Dim fil As HtmlInputFile = DirectCast(Session("imginputfile"), HtmlInputFile)

    '        '        If Not IsValidImageFile(fiPhoto) Then
    '        '            script = String.Format(SCRIPT_TEMPLATE, "The uploaded file is not a valid image file.", "true")
    '        '        End If
    '        '    Else
    '        script = String.Format(SCRIPT_TEMPLATE, "Please specify a valid file.", "true")
    '        'End If


    '    End If

    '    If String.IsNullOrEmpty(script) Then
    '        'Uploaded file is valid, now we can do whatever we like to do, copying it file system, 
    '        'saving it in db etc. 

    '        Dim strFileName As String
    '        strFileName = fiPhoto.PostedFile.FileName
    '        Dim c As String = System.IO.Path.GetFileName(strFileName)
    '        Try

    '            ' fiPhoto.PostedFile.SaveAs("C:\UploadedUserFiles\" + c)
    '            CropImg()

    '            script = String.Format(SCRIPT_TEMPLATE, "File was uploaded.", "false")
    '        Catch exp As Exception
    '            script = String.Format(SCRIPT_TEMPLATE, "File upload FAILED.", "false")

    '        End Try
    '    End If

    '    'Now inject the script which will fire when the page is refreshed. 
    '    ClientScript.RegisterStartupScript(Me.[GetType](), "uploadNotify", script)
    'End Sub





    Private Sub UploadPhoto()

        Dim script As String = String.Empty
        Dim Phototype As String = hid_PhotoType.Value
        If Phototype = "photo" Then
            If (fiPhoto.PostedFile IsNot Nothing) AndAlso (fiPhoto.PostedFile.ContentLength > 0) Then
                If Not IsValidImageFile(fiPhoto) Then
                    script = String.Format(SCRIPT_TEMPLATE, "The uploaded file is not a valid image file.", "true")
                    'Else
                    '    Session("imginputfile") = fiPhoto
                End If

            Else
                '    If Session("imginputfile") IsNot Nothing Then
                '        Dim fil As HtmlInputFile = DirectCast(Session("imginputfile"), HtmlInputFile)

                '        If Not IsValidImageFile(fiPhoto) Then
                '            script = String.Format(SCRIPT_TEMPLATE, "The uploaded file is not a valid image file.", "true")
                '        End If
                '    Else
                script = String.Format(SCRIPT_TEMPLATE, "Please specify a valid file.", "true")
                'End If


            End If
        End If

        If String.IsNullOrEmpty(script) Then
            'Uploaded file is valid, now we can do whatever we like to do, copying it file system, 
            'saving it in db etc. 

            Dim strFileName As String
            strFileName = fiPhoto.PostedFile.FileName
            Dim c As String = System.IO.Path.GetFileName(strFileName)
            Try

                ' fiPhoto.PostedFile.SaveAs("C:\UploadedUserFiles\" + c)
                Dim UploadStatus As String = CropImg()
                If UploadStatus = "Y" Then
                    script = String.Format(SCRIPT_TEMPLATE, "File was uploaded.", "false")
                Else
                    script = String.Format(SCRIPT_TEMPLATE, "File upload Failed.", "true")
                End If


            Catch exp As Exception
                script = String.Format(SCRIPT_TEMPLATE, "File upload FAILED.", "true")

            End Try
        End If

        'Now inject the script which will fire when the page is refreshed. 
        ClientScript.RegisterStartupScript(Me.[GetType](), "uploadNotify", script)
    End Sub

    Private Shared Function IsValidImageFile(ByVal file As HtmlInputFile) As Boolean
        Try
            Using bmp As New Bitmap(file.PostedFile.InputStream)
                Return True
            End Using
            'throws exception if not valid image 
        Catch generatedExceptionName As ArgumentException
        End Try

        Return False
    End Function

    Private Function CropImg() As String
        Try
            Dim CropFileName As String = ""
            Dim TempfileName As String
            Dim orgImg1 As System.Drawing.Image
            Dim orgImg As Image

            Dim Phototype As String = hid_PhotoType.Value
            If Phototype = "photo" Then
                orgImg = System.Drawing.Image.FromStream(fiPhoto.PostedFile.InputStream)
            ElseIf Phototype = "webcame" Then

                Dim Webcamepath As String = Session("CapturedImageType")
                orgImg = System.Drawing.Image.FromFile(Server.MapPath(Webcamepath))

            Else
                Return "N"
            End If

            Dim newWidth As Integer = Convert.ToInt32(imgw.Value)
            Dim newHeight As Integer = Val(imgH.Value)
            If newWidth > 0 And newHeight > 0 Then

                Dim thumbnailBitmap = New Bitmap(newWidth, newHeight)
                Dim thumbnailGraph = Graphics.FromImage(thumbnailBitmap)
                thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality
                thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality
                thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic
                Dim imageRectangle = New Rectangle(0, 0, newWidth, newHeight)
                thumbnailGraph.DrawImage(orgImg, imageRectangle)

                If hid_imgty.Value = "Photo" Then
                    Session("Imgphoto") = "~/CustTempImages/Photo" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                    CropFileName = Session("Imgphoto").ToString()
                End If
                If hid_imgty.Value = "Signature" Then
                    Session("ImgSignature") = "~/CustTempImages/Signature" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                    CropFileName = Session("ImgSignature").ToString()
                End If
                If hid_imgty.Value = "Document" Then
                    Session("ImgDocument") = "~/CustTempImages/Document" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                    CropFileName = Session("ImgDocument").ToString()
                End If

                If hid_imgty.Value = "Aadhar" Then
                    Session("ImgAadhaar") = "~/CustTempImages/Aadhaar" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                    CropFileName = Session("ImgAadhaar").ToString()
                End If
                If hid_imgty.Value = "Pancard" Then
                    Session("ImgPancard") = "~/CustTempImages/Pancard" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                    CropFileName = Session("ImgPancard").ToString()
                End If

                'End Using
                'End Using
                Dim areaToCrop As New Rectangle(Convert.ToInt32(x1.Value), Convert.ToInt32(y1.Value), Convert.ToInt32(w.Value), Convert.ToInt32(h.Value))

                If areaToCrop.Width > 0 Or areaToCrop.Height > 0 Then
                    Try
                        TempfileName = "~/CustTempImages/Temp" + Session("HostID").ToString() + "NewImg.Png"
                        thumbnailBitmap.Save(Server.MapPath(TempfileName))
                        'Dim filName As String = "NewImg.Png"
                        Dim FilePat As String = Server.MapPath(TempfileName)
                        'Dim FilePat As String = Path.Combine(Server.MapPath("~/CustTempImages"), filName)
                        orgImg1 = System.Drawing.Image.FromFile(FilePat)
                        Dim bitMap As New Bitmap(areaToCrop.Width, areaToCrop.Height)
                        Using g As Graphics = Graphics.FromImage(bitMap)
                            g.DrawImage(orgImg1, New Rectangle(0, 0, bitMap.Width, bitMap.Height), areaToCrop, GraphicsUnit.Pixel)
                        End Using
                        If hid_imgty.Value = "Photo" Then
                            Session("Imgphoto") = "~/CustTempImages/Photo" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                            CropFileName = Session("Imgphoto").ToString()
                        End If
                        If hid_imgty.Value = "Signature" Then
                            Session("ImgSignature") = "~/CustTempImages/Signature" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                            CropFileName = Session("ImgSignature").ToString()
                        End If
                        If hid_imgty.Value = "Document" Then
                            Session("ImgDocument") = "~/CustTempImages/Document" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                            CropFileName = Session("ImgDocument").ToString()
                        End If

                        If hid_imgty.Value = "Aadhar" Then
                            Session("ImgAadhar") = "~/CustTempImages/Aadhar" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                            CropFileName = Session("ImgAadhar").ToString()
                        End If
                        If hid_imgty.Value = "Pancard" Then
                            Session("ImgPancard") = "~/CustTempImages/Pancard" + Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".PNG"
                            CropFileName = Session("ImgPancard").ToString()
                        End If
                        bitMap.Save(Server.MapPath(CropFileName))
                        orgImg.Dispose()
                        bitMap = Nothing
                        Return "Y"
                    Catch ex As Exception

                        Return "N"
                    Finally
                        x1.Value = "0"
                        y1.Value = "0"
                        w.Value = "0"
                        h.Value = "0"
                    End Try
                Else
                    thumbnailBitmap.Save(Server.MapPath(CropFileName))
                    Return "Y"
                End If
            Else
                Return "N"
            End If
            Return "N"
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            Return "N"
        End Try
    End Function

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

End Class