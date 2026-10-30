Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common

Partial Public Class DayBeginEnd
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Public Branch_Code As String
    Public User_Id As String
    Dim FormID As Integer = 1044
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                Img_Post.Visible = False
                BtnCashDenomination.Visible = False
                Txt_TransactionDate.Attributes.Add("readonly", "readonly")
                Txt_NextTranDate.Attributes.Add("readonly", "readonly")
                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Public Sub Load_DefaultValues()
        Try
            Txt_TransactionDate.Text = Session("TransDate")
            'Txt_NextTranDate.Enabled = False

            Dim M_CommonBL As New CommonBL
            If Session("DefaultBrCode") = "DC" Then
                Txt_NextTranDate.Text = M_CommonBL.Get_BranchNextDate(Session("DefaultBrCode"))
            Else
                Txt_NextTranDate.Text = M_CommonBL.Get_BranchNextWorkingDate(Session("DefaultBrCode"))
                M_CommonBL = Nothing
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub



    Protected Sub Cmd_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Next.Click
        Try
            'If Session("DefaultBrCode") = "DC" Then

            '    Dim dt As New DataTable

            '    Dim com As New CommonBL()
            '    dt = com.Fill_CheckBankStatus(Session("TransDate"), "C", "H", "DC")
            '    If (dt.Rows.Count > 0) Then

            '        Dim str As String
            '        For i As Integer = 0 To dt.Rows.Count - 1
            '            str = str + " " + dt.Rows(i)(0).ToString() + ","

            '        Next
            '        ShowAlertMessage("The'" & str & "' Branches want to day end")
            '        Return
            '    Else
            '        Img_Post.Visible = True

            '    End If
            'Else
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA
            Dim Dt As New DataTable
            Dim LoginCount As String
            M_DBConn = New Connection()
            Dim I As Integer
            Dim Terminal = System.Environment.MachineName 'Request.ServerVariables("REMOTE_HOST")
            Branch_Code = Session("DefaultBrCode")
            User_Id = Session("UserID")
            Session("CloseBal") = ""


            M_Sql = "EXEC DayEndProcess_Sp " & ClsComm.DbDate(Txt_TransactionDate.Text) & ",'" & Branch_Code & "','" & User_Id & "','" & Terminal & "'," & ClsComm.DbDate(Txt_TransactionDate.Text) & ",'N'"
            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            M_Sql = ""

            M_Sql = " Select A.Details As DayEndProcess, Cast(B.Amount1 as decimal(10,2)) AS Data, Status,ProcStatus from RBI_ReportSetting_P_Tbl A"
            M_Sql = M_Sql & " Left Outer JOIN RBI_ReportData_R_Tbl B  ON A.Rpt_Id = B.Rpt_Id AND A.Row_Id = B.Row_Id Where A.Rpt_Id = '1' ORDER  BY Right('000000000000000000' + A.Row_Id,18)"

            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            Session("CloseBal") = M_Dt.Rows(5)("Data").ToString()
            Dgv_DayEndProcess.DataSource = M_Dt
            Dgv_DayEndProcess.DataBind()


            M_Sql = "select Br_Code,COUNT(Username) from Login_Tbl where Status='Y' and Br_Code='" & Session("DefaultBrCode") & "' and Tr_Date=" & ClsComm.DbDate(Txt_TransactionDate.Text) & " and UserName<> '" & Session("UserID") & "'Group by Br_Code"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            If M_Dt.Rows.Count > 0 Then
                LoginCount = M_Dt.Rows(0)(1).ToString
                ShowAlertMessage("Please Check Login Status '" & LoginCount & "' User is Login!")
                Exit Sub
            Else
                For I = 0 To Dgv_DayEndProcess.Rows.Count - 1
                    If Dgv_DayEndProcess.Rows(I).Cells(3).Text = "N" Then
                        ShowAlertMessage("Day End Process Not Completed!")
                        Exit Sub
                    End If

                Next
                Img_Post.Visible = True
                BtnCashDenomination.Visible = True
            End If
            'End If
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
 
    Protected Sub Img_Show_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Img_Show.Click
        Try
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim NextTrandate As Date
            Txt_NextTranDate.Enabled = True
            Dim Terminal = System.Environment.MachineName



            If Txt_NextTranDate.Text = "" Then
                ShowAlertMessage("Please Select Next Transaction Date")
                Txt_NextTranDate.Focus()
                Exit Sub
            End If

            Txt_NextTranDate.Enabled = True
            NextTrandate = DateAdd(DateInterval.Day, 1, Convert.ToDateTime(Txt_TransactionDate.Text))

            Txt_NextTranDate.Text = NextTrandate
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Img_Post.Click
        Try
            If Session("DefaultBrCode") = "DC" Then


                Dim com As New CommonBL()
                com.Fill_BankStatus(Session("TransDate"), "C")
                ShowAlertMessage("Day End Process Completed!")

                Session("LoginID") = ""
                Session("UserID") = ""

                Response.Redirect("~/SmartLogin.aspx")
                Return

            End If
            Dim M_Sql As String
            Dim M_SqlD As String
            Dim M_Dt1 As New DataTable()
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim Terminal = System.Environment.MachineName
            Branch_Code = Session("DefaultBrCode")
            User_Id = Session("UserID")


            M_SqlD = "select * from DENOMINATION where TrDate=" & ClsComm.DbDate(Txt_TransactionDate.Text) & " and Status='Y' and BrCode='" & Branch_Code & "'"
            M_DBConn.ExecuteDataTable(M_Dt1, M_SqlD, False)


            If M_Dt1.Rows.Count = 0 Then
                ShowAlertMessage("Please Enter Denomination")
                Exit Sub

            End If


            'M_Sql = "EXEC DayEndPosting_Sp " & ClsComm.DbDate(Txt_TransactionDate.Text) & ",'" & Branch_Code & "','" & User_Id & "','" & Terminal & "'," & ClsComm.DbDate(Txt_NextTranDate.Text) & ",'Y'"
            'M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            'M_Sql = ""
            DayEndProcess()
            M_Sql = "EXEC DayEndProcess_Sp " & ClsComm.DbDate(Txt_TransactionDate.Text) & ",'" & Branch_Code & "','" & User_Id & "','" & Terminal & "'," & ClsComm.DbDate(Txt_NextTranDate.Text) & ",'Y'"
            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            M_Sql = ""

            M_Sql = " Select A.Details As DayEndProcess,  Status,ProcStatus from RBI_ReportSetting_P_Tbl A"
            M_Sql = M_Sql & " Left Outer JOIN RBI_ReportData_R_Tbl B  ON A.Rpt_Id = B.Rpt_Id AND A.Row_Id = B.Row_Id Where A.Rpt_Id = '2' ORDER  BY Right('000000000000000000' + A.Row_Id,18)"

            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            'Threading.Thread.Sleep(1000)

            Dgv_DayEndPosting.DataSource = M_Dt
            Dgv_DayEndPosting.DataBind()


            M_Sql = "Update Login_Tbl set Status='N'  where  Tr_Date=" & ClsComm.DbDate(Txt_TransactionDate.Text) & " and Username='" & Session("UserID") & "'And Br_Code='" & Session("DefaultBrCode") & "'"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            ShowAlertMessage("Day End Process Completed!")

            Session("LoginID") = ""
            Session("UserID") = ""

            Response.Redirect("~/SmartLogin.aspx")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub DayEndProcess()
        Try

            Dim ClsComm As New CommonClass()
            Dim M_SQL As String

            M_SQL = "EXEC INSERTUPDATE_Gen_DayOpenCloseLog_M_Tbl 'NEW','" & Session("DefaultBrCode") & "'," & ClsComm.DbDate(Session("TransDate")) & ",'C','" & Session("UserID") & "'"
            M_DBConn.ExecuteScalar(M_SQL, 0, False)



        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub BtnCashDenomination_Click(sender As Object, e As EventArgs) Handles BtnCashDenomination.Click
        Dim PageURl As String = "~/CashDenomination.aspx?Closebal=" + Session("CloseBal")
        Response.Redirect(PageURl)
    End Sub
End Class