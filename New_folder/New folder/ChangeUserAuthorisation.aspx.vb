Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Public Class ChangeUserAuthorisation
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1050
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            Load_DefaultValues()
            TxtUserName.Text = Session("UserID")
        End If
    End Sub

    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        'M_CommonBL.Fill_User(CmbUserName)
        M_CommonBL.Fill_User(CmbNewUserName)
        TxtFromDate.Text = Session("TransDate")
        TxtToDate.Text = Session("TransDate")
        M_CommonBL = Nothing
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        ClearData()
    End Sub
    Private Sub ClearData()

        TxtFromDate.Text = Session("TransDate")
        TxtToDate.Text = Session("TransDate")
        TxtReason.Text = ""
        'CmbUserName.SelectedIndex = 0
        CmbNewUserName.SelectedIndex = 0

    End Sub
    Private Sub GetId()
        Dim M_Sql As String
        Dim M_Dt As New DataTable
        Dim M_Dbconn As New Connection()
        M_Sql = "select User_Emp_ID from User_Details_M_Tbl where USER_NAME='" & TxtUserName.Text & "'"
        M_Dbconn.ExecuteDataTable(M_Dt, M_Sql, False)
        HdnUserId.Value = M_Dt.Rows(0)("User_Emp_ID").ToString()
        M_Dbconn = Nothing
        Return
    End Sub

    Protected Sub CmdSave_Click1(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_UserAllotmentBo As New SMARTCORE_BO.UserAllotmentBO
            Dim M_Dbconn As New Connection()
            Call GetId()
            Dim M_Sql As String
            Dim M_Dt As New DataTable
            M_Sql = "select User_Emp_ID from User_Details_M_Tbl where USER_NAME='" & CmbNewUserName.SelectedItem.Text & "'"
            M_Dbconn.ExecuteDataTable(M_Dt, M_Sql, False)
            HdnNewUserId.Value = M_Dt.Rows(0)("User_Emp_ID").ToString()
            M_Dbconn = Nothing


            With M_UserAllotmentBo
                .AllotedFrDate = TxtFromDate.Text
                .AllotedToDate = TxtToDate.Text
                '.SlNo = M_Dt.Rows(0).Item("SlNo")
                .UserAllotedTime = "1/1/1900"
                .UserAllotedID = HdnUserId.Value
                .UserAllotedToID = HdnNewUserId.Value
                .AllotedReason = TxtReason.Text

            End With

            Dim M_UserAllotmentBl As New SMARTCORE_BL.UserAllotmentBL
            Session("M_EntryMode") = "NEW"
            M_UserAllotmentBl.Save_Data(M_UserAllotmentBo, Session("M_EntryMode"))
            ShowAlertMessage("User Authorisation Changed")
            ClearData()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbNewUserName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNewUserName.SelectedIndexChanged
   

    End Sub

    'Protected Sub TxtUserName_TextChanged(sender As Object, e As EventArgs) Handles TxtUserName.TextChanged
    '    Dim M_Sql As String
    '    Dim M_Dt As New DataTable
    '    M_Sql = "select User_Emp_ID from User_Details_M_Tbl where USER_NAME='" & TxtUserName.Text & "'"
    '    M_Dbconn.ExecuteDataTable(M_Dt, M_Sql, False)
    '    HdnUserId.Value = M_Dt.Rows(0)("User_Emp_ID").ToString()
    'End Sub
End Class