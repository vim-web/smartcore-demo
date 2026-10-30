Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Partial Public Class UserDiary
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim FormID As Integer = 1149
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Load_DefaultValues()
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

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_UserEmployee(CmbEmployeeName)
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub clear()
        Try
            TxtDate.Text = ""
            TxtDescription.Text = ""
            TxtSubject.Text = ""
            CmbEmployeeName.SelectedIndex = 0
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_DiaryBO As New UserDiaryBO
            With M_DiaryBO
                .TrDate = TxtDate.Text
                .UserId = CmbEmployeeName.SelectedValue
                .Subject = TxtSubject.Text
                .Description = TxtDescription.Text
                .UserAssignedBy = Session("UserID")
            End With
            Dim M_DiaryBl As New UserDiaryBL
            Dim str As String
            str = M_DiaryBl.Save_Data(M_DiaryBO, "NEW")
            If str > 0 Then
                ShowAlertMessage("Saved Successfully")
                clear()
            Else
                ShowAlertMessage("Not Saved Successfully")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Try
            clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
End Class