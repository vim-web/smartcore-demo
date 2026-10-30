Imports SMARTCORE_BL
Partial Public Class LoanAction
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1080

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hdnAccid.Value = Request.QueryString("AccID")
                Load_AccHldrs()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_AccHldrs()
        Try
            Dim Dt As New DataTable
            Dim M_CommonBL As New CommonBL
            DgvJointAc.DataSource = M_CommonBL.Get_LoanAction(Val(hdnAccid.Value))
            DgvJointAc.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


End Class