Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class HolidayForm
    Inherits System.Web.UI.Page
    Dim M_commCl As New CommonClass
    Dim M_CommnBL As New CommonBL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                txt_Date.Text = Session("TransDate")
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub but_save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles but_save.Click
        Try
            Dim StrDate As String = M_commCl.DbDate(txt_Date.Text.Trim())
            Dim Discription As String = txt_Description.Text.Trim()
            Dim M_Valid As Boolean = M_CommnBL.HolidayInsert(StrDate, Discription)
            If M_Valid = True Then
                ShowAlertMessage("Successful Saved")
                txt_Description.Text = ""
            Else
                ShowAlertMessage("Sorry. Server Error. Try Again")
            End If

        Catch ex As Exception
            ShowAlertMessage("Sorry. Server Error. Try Again")
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
End Class