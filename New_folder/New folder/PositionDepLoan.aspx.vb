Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class ReportsMonthly
    Inherits System.Web.UI.Page
    Dim TemArr(150) As String
    Dim i As Integer
    Dim COUNT As Integer
    Dim DepBal As Double
    Dim LoanBal As Double
    Dim M_DepLoanBL As New PositionDepLoanBL
    Dim M_Comman As New CommonClass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub But_Process_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles But_Process.Click
        Try
            Dim StrDate As String = M_Comman.DbDate(txt_date.Text.Trim, True)
            Dim M_valid As Boolean = M_DepLoanBL.Proc_depLoan(StrDate)

            If M_valid = True Then
                ShowAlertMessage("Successful Processed")
            Else
                ShowAlertMessage("Sorry !. Server Error. Try Again.")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "`")
                Dim M_CommonCls As New CommonClass

                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub but_Show_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles but_Show.Click
        Try
            Dim M_valid As Boolean = M_DepLoanBL.ShowReport_Deploan(txt_date.Text.Trim())

            If M_valid = True Then
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            Else
                ShowAlertMessage("Sorry !. Server Error. Try Again")
            End If

        Catch ex As Exception

        End Try

    End Sub
End Class