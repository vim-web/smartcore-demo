Imports SMARTCORE_DAL
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class LoginUserStatus
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1034
    Shared Obj_UserRights As Object
    Dim M_DBConn As New Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Loginstatus()
        End If
    End Sub
    Private Sub Loginstatus()
        Try

            Dim Dt As New DataTable
            Dim M_sql As String
            Dim Tr_date As Date
            Tr_date = Session("TransDate")
            M_sql = "select U.Emp_ID,U.User_Name,Emp_Name from Login_Tbl L Inner Join User_Details_Vw U on L.Username=U.User_Name where Br_Code='" & Session("DefaultBrCode") & "' and L.Tr_Date='" & Format(Tr_date, "MM/dd/yyyy") & "' and L.Status='Y' and L.Username <>'" & Session("UserID") & "'"
            M_DBConn.ExecuteDataTable(Dt, M_sql, False)
            Dgvlogin.DataSource = Dt
            Dgvlogin.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub Dgvlogin_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Dgvlogin.RowCommand
        Try
            Dim M_Logout As New LoginBL
            Dim EmpId As String
            Dim UserName As String
            Dim Dt As New DataTable
            Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
            EmpId = Dgvlogin.DataKeys(RowIndex)("Emp_ID").ToString()
            UserName = Dgvlogin.DataKeys(RowIndex)("User_Name").ToString()
            Dt = M_Logout.Logout(UserName, Session("TransDate"), Session("DefaultBrCode"))
            Loginstatus()
        Catch ex As Exception
        End Try
    End Sub
    'Private Sub Logout()

    '    Try
    '        Dim M_Logout As New LoginBL
    '        Dim Dt As New DataTable
    '        UserName = Dgvlogin.DataKeys(RowIndex)("User_Name").ToString()
    '        Dt = M_Logout.Logout(UserName, Session("TransDate"), Session("DefaultBrCode"))
    '        Loginstatus()
    '    Catch ex As ArithmeticException
    '    End Try

    'End Sub
End Class