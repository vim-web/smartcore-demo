Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Partial Public Class ChangePwd
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim FormID As Integer = 1018
    Shared Obj_UserRights As Object
    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        TxtUsrName.Text = Session("UserID")
        M_CommonBL = Nothing
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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


    End Sub


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Dim M_User As New UserBL
        Dim M_UsrBr As String
        If M_User.Validate_User(TxtUsrName.Text, TxtCurrPwd.Text, M_UsrBr) = True Then
        Else
            ShowAlertMessage("Current Password is not Match ")
            TxtCurrPwd.Focus()
            Exit Sub
        End If


        Dim M_UsrRegBO As New UserBO
        Dim M_UsrRegBL As New UserBL

        Try
            With M_UsrRegBO
                .UserName = TxtUsrName.Text
                .UserType = "0"
                .UserID = "1"
                .UserEmpID = Session("UserID")
                .UserPwd = M_UsrRegBL.EncodeText(TxtPassword.Text)
                .UserPwdValidUpto = "01/01/1900"
                .UserStatus = "Y"
                .UserAllotedUser = Session("LoginID")
                .UserAllotedTime = "01/01/1900"
                .UserActive = "Y"
                .UserStatusChgUser = Session("LoginID")
                .UserStatusChgTime = "01/01/1900"
                .UserTbar = ""
                .AssignedBranch = "0"

            End With
            M_UsrRegBL.Save_Data(M_UsrRegBO, "UPDATE")
            ShowAlertMessage("Saved Successfully")
            ClearData()
            Response.Redirect("~/SmartLogin.aspx")
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        ClearData()
    End Sub

    Private Sub ClearData()
        TxtConPassword.Text = ""
        TxtPassword.Text = ""
        TxtConPassword.Text = ""
        TxtCurrPwd.Text = ""
    End Sub

    Protected Sub TxtCurrPwd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCurrPwd.TextChanged
        'Dim M_User As New UserBL
        'Dim M_UsrBr As String
        'If M_User.Validate_User(TxtUsrName.Text, TxtCurrPwd.Text, M_UsrBr) = False Then
        '    ShowAlertMessage("Current Password is not Match ")
        '    TxtCurrPwd.Text = ""
        '    M_User = Nothing
        '    TxtCurrPwd.Focus()
        '    Exit Sub
        'End If
    End Sub
End Class