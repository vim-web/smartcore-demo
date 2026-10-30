Imports System.Web.SessionState
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.IO
Imports System.IO.Compression

Public Class Global_asax
    Inherits System.Web.HttpApplication
    Dim FormID As Integer = 1062

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started


        Dim M_CommonBL As New CommonBL
        Dim PickupDt As New DataTable
        Dim SchDt As New DataTable
        Dim AcheadDt As New DataTable
        Dim ShareParamDt As New DataTable
        Dim BranchDt As New DataTable
        Dim AssetDt As New DataTable
        AssetDt = M_CommonBL.Fill_Asset

        PickupDt = M_CommonBL.Fill_FullPickup
        SchDt = M_CommonBL.Fill_Schemes
        AcheadDt = M_CommonBL.Fill_AcHead
        ShareParamDt = M_CommonBL.Get_ShareParameters
        HttpContext.Current.Cache("Achead") = AcheadDt
        HttpContext.Current.Cache("PickupTbl") = PickupDt
        HttpContext.Current.Cache("SchemeTbl") = SchDt
        HttpContext.Current.Cache("ShareParamTbl") = ShareParamDt
        HttpContext.Current.Cache("CompanyName") = M_CommonBL.GetCompanyDetails()
        HttpContext.Current.Cache("Asset") = AssetDt

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        Try
            Dim ee As Exception = Server.GetLastError().GetBaseException()
            Dim ComonCls As New CommonClass
            Call ComonCls.LogErrorToLogFile(ee, "")
            ComonCls = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
        Session.Abandon()
        'Dim ComonCls As New CommonClass
        'Call ComonCls.LogErrorToLogFile("Seesion Close")
        'ComonCls = Nothing
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub
    'Private Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim app As HttpApplication = TryCast(sender, HttpApplication)
    '    Dim acceptEncoding As String = app.Request.Headers("Accept-Encoding")
    '    Dim prevUncompressedStream As Stream = app.Response.Filter

    '    If Not (TypeOf app.Context.CurrentHandler Is Page OrElse app.Context.CurrentHandler.[GetType]().Name = "SyncSessionlessHandler") OrElse app.Request("HTTP_X_MICROSOFTAJAX") IsNot Nothing Then
    '        Return
    '    End If

    '    If acceptEncoding Is Nothing OrElse acceptEncoding.Length = 0 Then
    '        Return
    '    End If

    '    acceptEncoding = acceptEncoding.ToLower()

    '    If acceptEncoding.Contains("deflate") OrElse acceptEncoding = "*" Then
    '        ' defalte
    '        app.Response.Filter = New DeflateStream(prevUncompressedStream, CompressionMode.Compress)
    '        app.Response.AppendHeader("Content-Encoding", "deflate")
    '    ElseIf acceptEncoding.Contains("gzip") Then
    '        ' gzip
    '        app.Response.Filter = New GZipStream(prevUncompressedStream, CompressionMode.Compress)
    '        app.Response.AppendHeader("Content-Encoding", "gzip")
    '    End If
    'End Sub

    'Private Sub Application_PostReleaseRequestState(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim contentType As String = Response.ContentType

    '    If contentType = "text/html" OrElse contentType = "text/css" OrElse contentType = "application/x-javascript" OrElse contentType = "text/javascript" Then
    '        Response.Cache.VaryByHeaders("Accept-Encoding") = True

    '        Dim acceptEncoding As String = Request.Headers("Accept-Encoding")

    '        If acceptEncoding IsNot Nothing Then
    '            If acceptEncoding.Contains("gzip") Then
    '                Response.Filter = New GZipStream(Response.Filter, CompressionMode.Compress)
    '                Response.AppendHeader("Content-Encoding", "gzip")
    '            ElseIf acceptEncoding.Contains("deflate") Then
    '                Response.Filter = New DeflateStream(Response.Filter, CompressionMode.Compress)
    '                Response.AppendHeader("Content-Encoding", "deflate")
    '            End If
    '        End If
    '    End If
    'End Sub
End Class