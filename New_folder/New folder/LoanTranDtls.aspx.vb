Imports SMARTCORE_BL
Partial Public Class LoanTranDtls
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Load_LoanDtls()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_LoanDtls()
        Try
            Dim Dt As New DataTable
            Dim M_LoanMasterBL As New LoanMasterBL
            Dt = M_LoanMasterBL.Set_LoanAmt("", "", "", Session("TransDate"), "Cash", 0, "C", "Y", Val(Request.QueryString("AccID"))).Tables(0)
            DgvLoanTranDtls.DataSource = Dt
            DgvLoanTranDtls.DataBind()
            Dim objSum As Object
            If Dt.Rows.Count > 0 Then
                objSum = Dt.Compute("Sum(TranAmt)", "TranType <>''")
                LblTotal.Text = Format(Val(objSum.ToString()), "#0.00")
                TxtTotal.Text = LblTotal.Text
                HdnTotal.Value = Val(objSum.ToString())

                objSum = Dt.Compute("Sum(TranAmt)", "TranType <>'*'")
                LblDueClear.Text = Format(Val(objSum.ToString()), "#0.00")

                Session("LnRctAmt") = TxtTotal.Text
            Else
                HdnTotal.Value = 0
                Session("LnRctAmt") = 0
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtTotal_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTotal.TextChanged
        Try
            If Val(TxtTotal.Text) > Val(HdnTotal.Value) Then
                TxtTotal.Text = Format(HdnTotal.Value, "#0.00")
            End If
            Session("LnRctAmt") = TxtTotal.Text
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub ImgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgClose.Click
        Try
            Session("LnRctAmt") = TxtTotal.Text
            'ScriptManager.RegisterStartupScript(Me.UpdatePanel1, GetType(String), "script1", "divclose_LoanTranDtls(" & TxtTotal.Text & ");", True)
            'ScriptManager.RegisterStartupScript(Me.UpdatePanel1, GetType(String), "script1", "window.opener.document.forms[0].submit(); self.close();", True)
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