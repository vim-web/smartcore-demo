Imports System.Web.Services
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Public Class WebCam
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Shared Sub SaveImage(imageData As String)

        Dim base64Data As String = imageData.Replace("data:image/png;base64,", "")
        Dim bytes As Byte() = Convert.FromBase64String(base64Data)

        Dim path As String = HttpContext.Current.Server.MapPath("~/Captured/")
        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If

        Dim fileName As String = "cam_" & DateTime.Now.Ticks & ".png"
        File.WriteAllBytes(path & fileName, bytes)

        ' Optional: Save bytes to database instead
    End Sub

End Class