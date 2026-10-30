Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Partial Public Class DayOpenProcess
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private M_DBConn As New Connection
    Public Branch_Code As String
    Public User_Id As String
    Dim FormID As Integer = 1046
    Shared Obj_UserRights As Object
    Dim ClsComm As New CommonClass()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/SmartLogin.aspx")
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Txt_TransactionDate.Attributes.Add("readonly", "readonly")
                Load_DefaultValues()
                'Cmd_Next.Visible = False

                

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            Txt_TransactionDate.Text = Session("TransDate") 'M_CommonBL.Get_BranchNextWorkingDate(Session("DefaultBrCode"))
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Cmd_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Next.Click
        Try
            'If Session("DefaultBrCode") = "DC" Then

            '    Dim dt As New DataTable

            '    Dim com As New CommonBL()
            '    dt = com.Fill_CheckBankStatus(Session("TransDate"), "O", "H", "DC")
            '    If (dt.Rows.Count > 0) Then
            '        TblHO.Visible = True
            '        TblDate.Visible = False
            '        TblGrid.Visible = False


            '        com.Fill_BankStatus(Session("TransDate"), "O")


            '    Else
            '        ShowAlertMessage("Head Office Want Day End Last Day")
            '        Return
            '    End If

            'Else
            Dim dt1 As New DataTable

            Dim com As New CommonBL()
            dt1 = com.Fill_CheckBankStatus(Session("TransDate"), "O", "B", Session("DefaultBrCode"))

            'If (dt1.Rows.Count > 0) Then
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA
            Dim Dt As New DataTable
            M_DBConn = New Connection()
            Dim I As Integer
            Dim Terminal = Session("HostID")
            Branch_Code = Session("DefaultBrCode")
            User_Id = Session("UserID")

            M_Sql = "EXEC DayOpenProcess_Sp " & ClsComm.DbDate(Txt_TransactionDate.Text) & ",'" & Branch_Code & "','" & User_Id & "','" & Terminal & "'"
            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            M_Sql = ""

            M_Sql = " Select A.Details As DayEndProcess, Cast(B.Amount1 as decimal(10,2)) AS Data, Status,ProcStatus from RBI_ReportSetting_P_Tbl A"
            M_Sql = M_Sql & " Left Outer JOIN RBI_ReportData_R_Tbl B  ON A.Rpt_Id = B.Rpt_Id AND A.Row_Id = B.Row_Id Where A.Rpt_Id = '3' And B.Terminal='" & Terminal & "' ORDER  BY Right('000000000000000000' + A.Row_Id,18)"

            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Dgv_DayEndProcess.DataSource = M_Dt
            Dgv_DayEndProcess.DataBind()
            DayOpenProcess()
            Dim Bol As Boolean = True
            For I = 0 To Dgv_DayEndProcess.Rows.Count - 1
                If Dgv_DayEndProcess.Rows(I).Cells(3).Text = "N" Then
                    Bol = False
                End If
            Next

            If Bol = False Then
                Session("IsDayOpened") = "Y"
                ShowAlertMessage("Day Open Process Not Completed!")
                Exit Sub

            Else
                ShowAlertMessage("Day Open Process Completed!")
                Cmd_Next.Visible = True
                Response.Redirect("~/SmartLogin.aspx")
            End If


            'Else
            'ShowAlertMessage("Head Office not opened")
            'End If
            ' End If



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

    Private Sub DayOpenProcess()
        Try
            Dim ClsComm As New CommonClass()
            Dim M_SQL As String

            M_SQL = "EXEC INSERTUPDATE_Gen_DayOpenCloseLog_M_Tbl 'NEW','" & Session("DefaultBrCode") & "'," & ClsComm.DbDate(Session("TransDate")) & ",'O','" & Session("UserID") & "'"
            M_DBConn.ExecuteScalar(M_SQL, 0, False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

   
    Protected Sub BtnChk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnChk.Click
     
    End Sub
End Class