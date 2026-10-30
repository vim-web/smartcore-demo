Imports SMARTCORE_BL
Partial Public Class BatchDtls
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As DataTable
            HdnBatchNo.Value = Request.QueryString("BatchNo")

            M_Dt = M_CommonBL.Get_BatchDetails(Session("DefaultBrCode"), Session("TransDate"), HdnBatchNo.Value)
            DgvBatch.DataSource = M_Dt
            DgvBatch.DataBind()

            Dim objSum As Object
            If M_Dt.Rows.Count > 0 Then
                objSum = Val(M_Dt.Compute("Sum(DrAmt)", "")) - Val(M_Dt.Compute("Sum(CrAmt)", ""))
                LblDiffrence.Text = Format(Val(objSum.ToString()) / 2, "#0.00")

                If Val(LblDiffrence.Text) > 0 Then
                    LblDiffrence.Text = "Difference : " & LblDiffrence.Text + " Cr"
                Else
                    LblDiffrence.Text = "Difference : " & Format(Math.Abs(Val(LblDiffrence.Text)), "#0.00") + " Dr"
                End If

            End If
            If Not IsPostBack Then

            End If
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