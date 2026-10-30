Imports SMARTCORE_BL

Partial Public Class ChequeIssueDtls
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1019



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hdnAccid.Value = Request.QueryString("AccID")
                Load_ChqDtls()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_ChqDtls()
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_ds As DataSet
            M_ds = M_CommonBl.Get_ChequeStaus(Val(hdnAccid.Value))

            ' StatusDtls
            DgvStatusDtls.DataSource = M_ds.Tables(0)
            DgvStatusDtls.DataBind()
            ' IssueDtls
            DgvIssueDtls.DataSource = M_ds.Tables(1)
            DgvIssueDtls.DataBind()
            ' TranDtls
            DgvTranDtls.DataSource = M_ds.Tables(2)
            DgvTranDtls.DataBind()
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

    Protected Sub DgvIssueDtls_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvIssueDtls.SelectedIndexChanged

    End Sub
End Class