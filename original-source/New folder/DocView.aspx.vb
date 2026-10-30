Imports SMARTCORE_BL
Partial Public Class DocView
    Inherits System.Web.UI.Page
    Private Shared prevPage As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim M_DocNo As Integer = Convert.ToInt32(Request.QueryString("DocID"))
        Dim Dt As New DataTable
        Try
            Dim ContentTyp As String

            Dim M_DocumentMasterBL As New DocumentMasterBL
            Dt = M_DocumentMasterBL.Locate_file(M_DocNo)
            M_DocumentMasterBL = Nothing
            ContentTyp = Dt.Rows(0)("ContentType").ToString()
            If ContentTyp = "application/vnd.openxmlformats-officedocument.word" Then
                ContentTyp = "application/msword"
            End If
            Response.ContentType = ContentTyp '"application/msword"
            Response.AddHeader("content-disposition", "inline; filename=" + Dt.Rows(0)("FLNM").ToString())
            Response.BinaryWrite(Dt.Rows(0)("BinaryData"))
            prevPage = Request.UrlReferrer.ToString()

        Catch ex As Exception

        End Try
        'Response.Redirect(prevPage)

    End Sub

End Class