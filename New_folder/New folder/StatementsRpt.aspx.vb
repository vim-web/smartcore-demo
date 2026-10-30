Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_BL

Partial Public Class StatementsRpt
    Inherits System.Web.UI.Page

    Public Shared Common As New CommonBL
    Public Shared ClsCommon As New CommonClass
    Public Shared M_DBConn As Connection
    Dim FormID As Integer = 1137
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If


            Call FillCombo()
            'Session.Add("File_Path", "E:\NEW SOURCE&DB\SMARTCORE\SMART_REPORTS")
        End If
    End Sub

    Public Sub FillCombo()
        Common.Fill_Branch(DdlBranch)

        Dim M_DataTable As DataTable = New DataTable()
        M_DBConn = New Connection()

        Dim M_Sql As String
        M_Sql = "SELECT Report_Name, RptGroup_ID, Report_ID From Rpt_ReportNames_R_Tbl WHERE (Rpt_Active = 'Y')"
        M_Sql = M_Sql & " order by Report_Name"
        M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)

        DdlRpt.DataTextField = "Report_Name"
        DdlRpt.DataValueField = "Report_ID"
        DdlRpt.DataSource = M_DataTable
        DdlRpt.DataBind()
        DdlRpt.Items.Add(" <--- SELECT --->")
        If DdlRpt.Items.Count > 0 Then DdlRpt.SelectedIndex = DdlRpt.Items.Count - 1
    End Sub

    Private Sub Process_Monthly_Progress_PACS()
        On Error GoTo Err_handler_Process
        Dim i As Integer
        Dim I_SchCount As Integer
        Dim IsFirstSch As String
        Dim SqlStr As String
        Dim M_Dt As DataTable = New DataTable()


        M_DBConn = New Connection()

        ' Processing R & D  Between From Date and To Date
        Lbl_Process.Text = "Processing TrialBalance on " & TxtToDate.Text

        SqlStr = "Delete From Tmp_RandD_P_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Deposit_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Loan_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Share_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Borrowing_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Investment_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)

        SqlStr = "Exec Acc_Get_RandD " & ClsCommon.DbDate(TxtFrDate.Text) & "," & ClsCommon.DbDate(TxtToDate.Text) & _
        ", '" & DdlBranch.SelectedValue & "','" & Session("HostId") & "','%','N'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        Lbl_Process.Text = ""

        I_SchCount = 0
        SqlStr = "Select Sch_Code,Sch_Name,Acc_Code,Sch_Section_Name from Gen_SchemeMaster_P_Tbl Where Sch_Section_Name In ('BORROWINGS','DEPOSIT','GROUP DEPOSIT','INVESTMENT','LOAN','SHARE','SUSPENSE')"
        M_DBConn.ExecuteDataTable(M_Dt, SqlStr, False)

        For i = 0 To M_Dt.Rows.Count - 1
            Lbl_Process.Text = "Processing " + M_Dt.Rows(i)("Sch_Name").ToString() + " Outstanding"
            SqlStr = ""
            Select Case M_Dt.Rows(i)("Sch_Section_Name").ToString()
                Case "DEPOSIT"
                    SqlStr = "Exec Deposit_Outstand_Shedule_Sp 'N','" & DdlBranch.SelectedValue & "', 'S' " & _
                        ",'" & M_Dt.Rows(i)("Sch_Code").ToString() & "',''," & _
                        ClsCommon.DbDate(TxtFrDate.Text) & "," & ClsCommon.DbDate(TxtToDate.Text) & ",'N','" & Session("LoginID") & "','" & Session("HostID") & "','N'"
                    M_DBConn.ExecuteScalar(SqlStr, , False)
                Case "LOAN"
                    'SqlStr = "Exec Process_LoanShedule_Sp  '" & DdlBranch.SelectedValue & "','S','" & M_Dt.Rows(i)("Sch_Code").ToString() & "',''," & ClsCommon.DbDate(TxtFrDate.Text) & "," & ClsCommon.DbDate(TxtToDate.Text) & ",'Y','Y','Y','" & Session("HostID") & "','" & Session("LoginID") & "'"
                    'M_DBConn.ExecuteScalar(SqlStr, , False)

                    SqlStr = "Exec [dbo].[Process_LoanAccrued_Sp] '" & DdlBranch.SelectedValue & "', 'S',"
                    SqlStr = SqlStr + "'" & M_Dt.Rows(i)("Sch_Code").ToString() & "' ,"
                    SqlStr = SqlStr + "''," + ClsCommon.DbDate(TxtFrDate.Text) & "," + ClsCommon.DbDate(TxtToDate.Text) & ",'N','Y','Y','Y','" & Session("HostID") & "','" & Session("LoginID") & "'"
                    M_DBConn.ExecuteScalar(SqlStr, , False)

                Case "SHARE"
                    'SqlStr = "Exec Share_Outstand_Shedule_Sp 'N','" & M_Dt.Rows(i)("Sch_Name").ToString() & "','" & DdlBranch.SelectedValue & "'," & ClsCommon.DbDate(TxtFrDate.Text) & "," & DbDate(TxtToDate.Text) & ",'" & Session("LoginID") & "','N'"
                    SqlStr = "Exec Share_Outstand_Sp 'N','" & M_Dt.Rows(i)("Sch_Code").ToString() & "','" & DdlBranch.SelectedValue & "'," + ClsCommon.DbDate(TxtToDate.Text) & ",'" & Session("LoginID") & "','Y','" & Session("HostId") & "'"
                    M_DBConn.ExecuteScalar(SqlStr, , False)
            End Select
            I_SchCount = I_SchCount + 1
            Lbl_Process.Text = ""
        Next i

        Lbl_Process.Text = "Processing Final Report"
        SqlStr = "Exec Monthly_Prog_Stmt_PACS_Sp " & ClsCommon.DbDate(TxtFrDate.Text) & "," & ClsCommon.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & DdlBranch.SelectedValue & "','" & Session("LoginID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        Lbl_Process.Text = ""

        'SFMsgBox("Processing completed Successfully.", vbInformation, "Completed")

        Exit Sub

Err_handler_Process:

    End Sub

    Protected Sub BtnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnProcess.Click
        M_DBConn = New Connection()
        Select Case DdlRpt.SelectedValue.ToString()
            'Case 1 : Call Process_Monthly_Progress()
            'Case 2 : Call Process_Quarterly_Loan_Stmt()
            'Case 3 : Call Process_Monthly_Progress_2()
            'Case 4 : Call Process_Monthly_Progress_2_Branch()
            'Case 5 : Call Process_Deposit_Mobilisation()
            'Case 8 : Call Process_Monthly_Progress_EKRM()
            Case 9 : Call Process_Monthly_Progress_PACS()
                'Case 12 : Call Process_Monthly_Progress_2_SchemeWise()
        End Select
    End Sub

    Protected Sub BtnShowRpt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnShowRpt.Click
        M_DBConn = New Connection()
        Select Case DdlRpt.SelectedValue.ToString()
            'Case 1 : Call Show_MonthlyProgress_Rpt()
            'Case 2 : Call Show_Quaterly_Loan_Stmt_Rpt()
            'Case 3 : Call Show_MonthlyProgress_Rpt_2()
            'Case 4 : Call Show_MonthlyProgress_Rpt_2_Branch()
            'Case 5 : Call Show_Dep_Mobilisation_Rpt()
            'Case 8 : Call Show_MonthlyProgress_Rpt_ERKM()
            Case 9 : Call Show_MonthlyProgress_Rpt_PACS()
                'Case 12 : Call Show_MonthlyProgress_Rpt_2_Schemewise()
        End Select
    End Sub

    Private Sub Show_MonthlyProgress_Rpt_PACS()
        Dim Rpt_Name As String
        Dim SubRpt_Name As String
        Dim Str_FormatedFlf As String
        Dim DayOpCash As Double
        Dim MonthOpCash As Double
        Dim YearOpCash As Double
        Dim ClCash As Double
        Dim YearOpDate As Date
        Dim SubRptQry(50) As String
        Dim SqlStr As String
        Dim M_Dt As DataTable = New DataTable()
        Dim Str_Flf As String

        On Error GoTo ErrHandler

        If OptBranchWise.Checked = True Then
            ' 01. AUTHORISED SHARE CAPITAL
            SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SqlStr = SqlStr & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 1"
            SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            ' 02 MEMBERSHIP AND PAID UP CAPITAL
            SubRptQry(0) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(0) = SubRptQry(0) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(0) = SubRptQry(0) & " From Rpt_Master_R_Tbl"
            SubRptQry(0) = SubRptQry(0) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(0) = SubRptQry(0) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(0) = SubRptQry(0) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(0) = SubRptQry(0) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(0) = SubRptQry(0) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(0) = SubRptQry(0) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(0) = SubRptQry(0) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 2"
            SubRptQry(0) = SubRptQry(0) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            ' 03. BORROWINGS
            SubRptQry(1) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(1) = SubRptQry(1) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(1) = SubRptQry(1) & " From Rpt_Master_R_Tbl"
            SubRptQry(1) = SubRptQry(1) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(1) = SubRptQry(1) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(1) = SubRptQry(1) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(1) = SubRptQry(1) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(1) = SubRptQry(1) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(1) = SubRptQry(1) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(1) = SubRptQry(1) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 3"
            SubRptQry(1) = SubRptQry(1) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            ' 04. DEPOSITS
            SubRptQry(2) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, "
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0) As Amount11,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0) As Amount13,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0) As Amount15,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0) As Amount17,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0) As Amount19,"
            SubRptQry(2) = SubRptQry(2) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(2) = SubRptQry(2) & " From Rpt_Master_R_Tbl"
            SubRptQry(2) = SubRptQry(2) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(2) = SubRptQry(2) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(2) = SubRptQry(2) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(2) = SubRptQry(2) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(2) = SubRptQry(2) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(2) = SubRptQry(2) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(2) = SubRptQry(2) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 4"
            SubRptQry(2) = SubRptQry(2) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            ' 05. RESERVE FUND AND OTHER RESERVES
            SubRptQry(3) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(3) = SubRptQry(3) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(3) = SubRptQry(3) & " From Rpt_Master_R_Tbl"
            SubRptQry(3) = SubRptQry(3) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(3) = SubRptQry(3) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(3) = SubRptQry(3) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(3) = SubRptQry(3) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(3) = SubRptQry(3) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(3) = SubRptQry(3) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(3) = SubRptQry(3) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 5"
            SubRptQry(3) = SubRptQry(3) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 06. DETAILS OF LIABILITIES AND ASSETS   -- LIABILITIES
            SubRptQry(4) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(4) = SubRptQry(4) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(4) = SubRptQry(4) & " From Rpt_Master_R_Tbl"
            SubRptQry(4) = SubRptQry(4) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(4) = SubRptQry(4) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(4) = SubRptQry(4) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(4) = SubRptQry(4) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(4) = SubRptQry(4) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(4) = SubRptQry(4) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(4) = SubRptQry(4) & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id =6 And Rpt_Generator_R_Tbl.Sub_Head_Id Between 2 And 9"
            SubRptQry(4) = SubRptQry(4) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 06. DETAILS OF LIABILITIES AND ASSETS   -- LIABILITIES
            SubRptQry(5) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(5) = SubRptQry(5) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(5) = SubRptQry(5) & " From Rpt_Master_R_Tbl"
            SubRptQry(5) = SubRptQry(5) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(5) = SubRptQry(5) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(5) = SubRptQry(5) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(5) = SubRptQry(5) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(5) = SubRptQry(5) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(5) = SubRptQry(5) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(5) = SubRptQry(5) & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id =6 And Rpt_Generator_R_Tbl.Sub_Head_Id Between 10 And 19"
            SubRptQry(5) = SubRptQry(5) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 07.DETAILS OF LOANS ISSUED TO MEMBERS DURING THE MONTH
            ' 08.DETAILS OF LOANS ISSUED TO MEMBERS DURING THE CURRENT YEAR FROM
            ' 09. DETAILS OF LOANS DEMANDED DURING THE MONTH(Opening Overdue+Current month Demand)
            ' 10. DETAILS OF LOANS COLLECTED DURING THE MONTH
            ' 11. DETAILS OF OVERDUE LOANS AT THE END OF THE MONTH
            ' 12. DETAILS OF OUTSTANDING LOANS AT THE END OF THE MONTH

            SubRptQry(6) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, "
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0) As Amount11,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0) As Amount13,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0) As Amount15,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0) As Amount17,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0) As Amount19,"
            SubRptQry(6) = SubRptQry(6) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(6) = SubRptQry(6) & " From Rpt_Master_R_Tbl"
            SubRptQry(6) = SubRptQry(6) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(6) = SubRptQry(6) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(6) = SubRptQry(6) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(6) = SubRptQry(6) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(6) = SubRptQry(6) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(6) = SubRptQry(6) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(6) = SubRptQry(6) & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id In (7,8,9,10,11,12)"
            SubRptQry(6) = SubRptQry(6) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            ' 13. DETAILS REGARDING PERCENTAGE OF OVERDUE TO DEMAND/OUTSTANDING
            SubRptQry(7) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(7) = SubRptQry(7) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(7) = SubRptQry(7) & " From Rpt_Master_R_Tbl"
            SubRptQry(7) = SubRptQry(7) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(7) = SubRptQry(7) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(7) = SubRptQry(7) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(7) = SubRptQry(7) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(7) = SubRptQry(7) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(7) = SubRptQry(7) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(7) = SubRptQry(7) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 13"
            SubRptQry(7) = SubRptQry(7) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 14. DETAILS OF NON CREDIT BUSINESS (QTY.IN MT)
            SubRptQry(8) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, "
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0) As Amount11,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0) As Amount13,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0) As Amount15,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0) As Amount17,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0) As Amount19,"
            SubRptQry(8) = SubRptQry(8) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(8) = SubRptQry(8) & " From Rpt_Master_R_Tbl"
            SubRptQry(8) = SubRptQry(8) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(8) = SubRptQry(8) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(8) = SubRptQry(8) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(8) = SubRptQry(8) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(8) = SubRptQry(8) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(8) = SubRptQry(8) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(8) = SubRptQry(8) & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id =14"
            SubRptQry(8) = SubRptQry(8) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 15. DETAILS OF IRDP LOANS
            SubRptQry(9) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, "
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0) As Amount11,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0) As Amount13,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0) As Amount15,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0) As Amount17,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0) As Amount19,"
            SubRptQry(9) = SubRptQry(9) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(9) = SubRptQry(9) & " From Rpt_Master_R_Tbl"
            SubRptQry(9) = SubRptQry(9) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(9) = SubRptQry(9) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(9) = SubRptQry(9) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(9) = SubRptQry(9) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(9) = SubRptQry(9) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(9) = SubRptQry(9) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(9) = SubRptQry(9) & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id =15"
            SubRptQry(9) = SubRptQry(9) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 16. DETAILS OF SELF EMPLOYEMENT LOANS
            SubRptQry(10) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(10) = SubRptQry(10) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(10) = SubRptQry(10) & " From Rpt_Master_R_Tbl"
            SubRptQry(10) = SubRptQry(10) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(10) = SubRptQry(10) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(10) = SubRptQry(10) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(10) = SubRptQry(10) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(10) = SubRptQry(10) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(10) = SubRptQry(10) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(10) = SubRptQry(10) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 16"
            SubRptQry(10) = SubRptQry(10) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 17.DETAILS OF SELF-HELP GROUPS
            SubRptQry(11) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(11) = SubRptQry(11) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(11) = SubRptQry(11) & " From Rpt_Master_R_Tbl"
            SubRptQry(11) = SubRptQry(11) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(11) = SubRptQry(11) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(11) = SubRptQry(11) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(11) = SubRptQry(11) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(11) = SubRptQry(11) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(11) = SubRptQry(11) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(11) = SubRptQry(11) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 17"
            SubRptQry(11) = SubRptQry(11) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            ' 18.DETAILS OF ACTION TAKEN FOR THE RECOVERY OF OVERDUE LOANS MENTIONED IN COL:11
            SubRptQry(12) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(12) = SubRptQry(12) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(12) = SubRptQry(12) & " From Rpt_Master_R_Tbl"
            SubRptQry(12) = SubRptQry(12) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(12) = SubRptQry(12) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(12) = SubRptQry(12) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(12) = SubRptQry(12) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(12) = SubRptQry(12) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(12) = SubRptQry(12) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(12) = SubRptQry(12) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 18"
            SubRptQry(12) = SubRptQry(12) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 19.POSITION OF IMPORTANT REGISTER
            ' 20.FLUID RESOURCES
            ' 21.GENERAL BODY
            ' 22.BOARD OF MANAGEMENT
            ' 23.POSITION OF AUDIT
            SubRptQry(13) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(13) = SubRptQry(13) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(13) = SubRptQry(13) & " From Rpt_Master_R_Tbl"
            SubRptQry(13) = SubRptQry(13) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(13) = SubRptQry(13) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(13) = SubRptQry(13) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(13) = SubRptQry(13) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(13) = SubRptQry(13) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(13) = SubRptQry(13) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(13) = SubRptQry(13) & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id  In (19,20,21,22,23)"
            SubRptQry(13) = SubRptQry(13) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            ' 24.DETAILS OF APPOINTMENTS
            SubRptQry(14) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(14) = SubRptQry(14) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(14) = SubRptQry(14) & " From Rpt_Master_R_Tbl"
            SubRptQry(14) = SubRptQry(14) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(14) = SubRptQry(14) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(14) = SubRptQry(14) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(14) = SubRptQry(14) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(14) = SubRptQry(14) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(14) = SubRptQry(14) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(14) = SubRptQry(14) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 24"
            SubRptQry(14) = SubRptQry(14) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            ' 25.DETAILS OF GOVT.ASSISTANCE OUTSTANDING
            SubRptQry(15) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(15) = SubRptQry(15) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(15) = SubRptQry(15) & " From Rpt_Master_R_Tbl"
            SubRptQry(15) = SubRptQry(15) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(15) = SubRptQry(15) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(15) = SubRptQry(15) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(15) = SubRptQry(15) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(15) = SubRptQry(15) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(15) = SubRptQry(15) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(15) = SubRptQry(15) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 25"
            SubRptQry(15) = SubRptQry(15) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 26.TSB/TP ACCOUNTS & SPECIAL SB ACCOUNTS IN DC BANK/KSC BANK
            SubRptQry(16) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(16) = SubRptQry(16) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(16) = SubRptQry(16) & " From Rpt_Master_R_Tbl"
            SubRptQry(16) = SubRptQry(16) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(16) = SubRptQry(16) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(16) = SubRptQry(16) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(16) = SubRptQry(16) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(16) = SubRptQry(16) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(16) = SubRptQry(16) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(16) = SubRptQry(16) & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 26"
            SubRptQry(16) = SubRptQry(16) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            ' 27.N.S.C
            ' 28.AGRICULTURAL DEBT RELIEF
            ' 29.GREEN CARD
            ' 30.AUDIT REPORT PENDING
            ' 31.K.S.R POST
            ' 32.REFERENCE PENDING IN AR'S OFFICE
            SubRptQry(17) = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)/1000 As Amount1, "
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)/1000 As Amount2,Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)/1000 As Amount3,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)/1000 As Amount4,Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)/1000 As Amount5,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)/1000 As Amount6,Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)/1000 As Amount7,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)/1000 As Amount8,Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)/1000 As Amount9,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)/1000 As Amount10,Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)/1000 As Amount11,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0)/1000 As Amount12,Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)/1000 As Amount13,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0)/1000 As Amount14,Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)/1000 As Amount15,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0)/1000 As Amount16,Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)/1000 As Amount17,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0)/1000 As Amount18,Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)/1000 As Amount19,"
            SubRptQry(17) = SubRptQry(17) & " Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0)/1000 As Amount20"
            SubRptQry(17) = SubRptQry(17) & " From Rpt_Master_R_Tbl"
            SubRptQry(17) = SubRptQry(17) & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            SubRptQry(17) = SubRptQry(17) & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            SubRptQry(17) = SubRptQry(17) & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            SubRptQry(17) = SubRptQry(17) & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            SubRptQry(17) = SubRptQry(17) & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID And Rpt_ProcDetails_R_Tbl.Br_Code='" & DdlBranch.SelectedValue & "'"
            SubRptQry(17) = SubRptQry(17) & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            SubRptQry(17) = SubRptQry(17) & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id In (27, 28, 29, 30, 31, 32)"
            SubRptQry(17) = SubRptQry(17) & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"


            Str_Flf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|" & DdlBranch.SelectedItem.Text & "|Heading|MONTHLY PROGRESS REPORT OF P.A.C.S Between " & TxtFrDate.Text & " And " & TxtToDate.Text
            'Str_Flf = Str_Flf & "|Address|" & Branch_Address & " , " & Branch_Phone

        ElseIf OptConsolidation.Checked = True Then

            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 1"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISRpt = MainConnection.Execute(SqlStr)

            '' 02 MEMBERSHIP AND PAID UP CAPITAL
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 2"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(0) = MainConnection.Execute(SqlStr)

            '' 03. BORROWINGS
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 3"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(1) = MainConnection.Execute(SqlStr)

            '' 04. DEPOSITS
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0)) As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0)) As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0)) As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0)) As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0)) As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 4"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(2) = MainConnection.Execute(SqlStr)

            '' 05. RESERVE FUND AND OTHER RESERVES
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 5"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(3) = MainConnection.Execute(SqlStr)

            '' 06. DETAILS OF LIABILITIES AND ASSETS   -- LIABILITIES
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id =6 And Rpt_Generator_R_Tbl.Sub_Head_Id Between 2 And 9"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(4) = MainConnection.Execute(SqlStr)

            '' 06. DETAILS OF LIABILITIES AND ASSETS   -- LIABILITIES
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id =6 And Rpt_Generator_R_Tbl.Sub_Head_Id Between 10 And 19"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(5) = MainConnection.Execute(SqlStr)

            '' 07.DETAILS OF LOANS ISSUED TO MEMBERS DURING THE MONTH
            '' 08.DETAILS OF LOANS ISSUED TO MEMBERS DURING THE CURRENT YEAR FROM
            '' 09. DETAILS OF LOANS DEMANDED DURING THE MONTH(Opening Overdue+Current month Demand)
            '' 10. DETAILS OF LOANS COLLECTED DURING THE MONTH
            '' 11. DETAILS OF OVERDUE LOANS AT THE END OF THE MONTH
            '' 12. DETAILS OF OUTSTANDING LOANS AT THE END OF THE MONTH

            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id In (7,8,9,10,11,12)"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(6) = MainConnection.Execute(SqlStr)

            '' 13. DETAILS REGARDING PERCENTAGE OF OVERDUE TO DEMAND/OUTSTANDING
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 13"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(7) = MainConnection.Execute(SqlStr)

            '' 14. DETAILS OF NON CREDIT BUSINESS (QTY.IN MT)
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id =14"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(8) = MainConnection.Execute(SqlStr)

            '' 15. DETAILS OF IRDP LOANS
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id =15"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(9) = MainConnection.Execute(SqlStr)

            '' 16. DETAILS OF SELF EMPLOYEMENT LOANS
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 16"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(10) = MainConnection.Execute(SqlStr)

            '' 17.DETAILS OF SELF-HELP GROUPS
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 17"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(11) = MainConnection.Execute(SqlStr)

            '' 18.DETAILS OF ACTION TAKEN FOR THE RECOVERY OF OVERDUE LOANS MENTIONED IN COL:11
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 18"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(12) = MainConnection.Execute(SqlStr)

            '' 19.POSITION OF IMPORTANT REGISTER
            '' 20.FLUID RESOURCES
            '' 21.GENERAL BODY
            '' 22.BOARD OF MANAGEMENT
            '' 23.POSITION OF AUDIT
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id  In (19,20,21,22,23)"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(13) = MainConnection.Execute(SqlStr)

            '' 24.DETAILS OF APPOINTMENTS
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 24"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(14) = MainConnection.Execute(SqlStr)

            '' 25.DETAILS OF GOVT.ASSISTANCE OUTSTANDING
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 25"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(15) = MainConnection.Execute(SqlStr)

            '' 26.TSB/TP ACCOUNTS & SPECIAL SB ACCOUNTS IN DC BANK/KSC BANK
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID = 9 And Rpt_Generator_R_Tbl.Group_Id = 26"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(16) = MainConnection.Execute(SqlStr)

            '' 27.N.S.C
            '' 28.AGRICULTURAL DEBT RELIEF
            '' 29.GREEN CARD
            '' 30.AUDIT REPORT PENDING
            '' 31.K.S.R POST
            '' 32.REFERENCE PENDING IN AR'S OFFICE
            'SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID, Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0))/1000 As Amount1, "
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0))/1000 As Amount2,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0))/1000 As Amount3,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0))/1000 As Amount4,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0))/1000 As Amount5,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0))/1000 As Amount6,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0))/1000 As Amount7,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0))/1000 As Amount8,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0))/1000 As Amount9,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0))/1000 As Amount10,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount11,0))/1000 As Amount11,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount12,0))/1000 As Amount12,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount13,0))/1000 As Amount13,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount14,0))/1000 As Amount14,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount15,0))/1000 As Amount15,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount16,0))/1000 As Amount16,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount17,0))/1000 As Amount17,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount18,0))/1000 As Amount18,Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount19,0))/1000 As Amount19,"
            'SqlStr = SqlStr & " Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount20,0))/1000 As Amount20"
            'SqlStr = SqlStr & " From Rpt_Master_R_Tbl"
            'SqlStr = SqlStr & " INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/"
            'SqlStr = SqlStr & " Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID AND"
            'SqlStr = SqlStr & " Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID "
            'SqlStr = SqlStr & "  And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & ClsCommon.DbDate(TxtToDate.Text)
            'SqlStr = SqlStr & " Where Rpt_Generator_R_Tbl.Report_ID =9 And Rpt_Generator_R_Tbl.Group_Id In (27, 28, 29, 30, 31, 32)"
            'SqlStr = SqlStr & " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Group_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order "
            'SqlStr = SqlStr & " Order By Cast(Rpt_Generator_R_Tbl.Group_ID As Int),Rpt_Generator_R_Tbl.Sub_Head_Order"

            'Rs_MISSubRpt(17) = MainConnection.Execute(SqlStr)

            'Str_Flf = "Company|" & HttpContext.Current.Cache("CompanyName")  & "|Branch|Consolidated|Heading|MONTHLY PROGRESS REPORT OF P.A.C.S Between " & MskEd_FromDate & " And " & TxtToDate.Text
            'Str_Flf = Str_Flf & "|Address|" & Branch_Address & " , " & Branch_Phone

        End If

        SubRpt_Name = "02|03|04|05|06A|06B|07|13|14|15|16|17|18|19|24|25|26|27to32"

        Str_Flf = Str_Flf + "|Footer1|Clerk|Footer2|Accountant|Footer3|Manager|User_ID|" & Session("LoginID")

        Session.Add("RptFormula", Str_Flf)
        Session.Add("RptQry", SqlStr)
        Session.Add("SubRptNames", SubRpt_Name)
        Session.Add("SubRptQry", SubRptQry)
        Session.Add("RptPath", Server.MapPath("~/Reports/Monthly_Progress_PACS.rpt"))
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

        Exit Sub

ErrHandler:

    End Sub


    '' Monthly Progress Report For Ernakulam Area
    'Private Sub Process_Monthly_Progress_EKRM()
    '    On Error GoTo Err_handler_Process
    '    Dim i               As Integer
    '    Dim Rs_Scheme       As New ADODB.Recordset
    '    Dim I_SchCount      As Integer
    '    Dim IsFirstSch      As String
    '    If Validate_Fields = False Then Exit Sub
    '    Me.MousePointer = vbHourglass
    '    DoEvents
    '    ' Processing R & D  Between From Date and To Date
    '    Lbl_Process = "Processing TrialBalance on " & TxtToDate.Text
    '
    '    MainConnection.CommandTimeout = 0
    '
    '    SqlStr = "Exec Acc_Get_RandD " & Format(MskEd_FromDate, "'mm-dd-yyyy'") & "," & Format(TxtToDate.Text, "'mm-dd-yyyy'") & _
    '    ",'" & User_Id & "','" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "','N'"
    '    MainConnection.Execute (SqlStr)
    '    Lbl_Process = ""
    '    DoEvents
    '
    '    MainConnection.Execute ("Delete From Loan_Outstand_R_Tbl Where Terminal='" & System_Name & "'")
    '    MainConnection.Execute ("Delete From Deposit_Outstand_R_Tbl Where Terminal='" & System_Name & "'")
    '    MainConnection.Execute ("Delete From Share_Outstand_R_Tbl Where Terminal='" & System_Name & "'")
    '
    '    For i = 0 To SCmb_Section.ListCount - 1
    '        I_SchCount = 0
    '        str_Sql = "Select Sch_Code,Sch_Name,Acc_Code from Gen_SchemeMaster_P_Tbl Where Sch_Section_Name ='" & Trim(SCmb_Section.List(i)) & "'"
    '        Set Rs_Scheme = FillRecordset(Rs_Scheme, str_Sql, True)
    '        Do Until Rs_Scheme.Eof
    '            IsFirstSch = IIf(I_SchCount = 0, "Y", "N")
    '            Lbl_Process = "Processing " + Rs_Scheme!Sch_Name + " Outstanding"
    '            DoEvents
    '            SqlStr = ""
    '            Select Case Trim(SCmb_Section.List(i))
    '                Case "DEPOSIT":
    '                    SqlStr = "Exec Deposit_Outstand_Shedule_Sp 'N','" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "', 'S','" & _
    '                        Rs_Scheme(0) & "','','" & Format(MskEd_FromDate, "MM/dd/yyyy") & "','" & Format(TxtToDate.Text, "MM/dd/yyyy") & "','N','" & User_Id & "','" & System_Name & "','N'"
    '
    '                Case "LOAN":
    '                    SqlStr = "Exec Process_LoanShedule_Sp '" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "','S','" & _
    '                        Rs_Scheme(0) & "',''," & ClsCommon.DbDate(TxtFrDate.Text) & "," & DbDate(TxtToDate.Text) & ",'Y','N','Y','" & System_Name & "','" & User_Id & "'"
    '                Case "SHARE":
    '                    SqlStr = "Exec Share_Outstand_Shedule_Sp 'N','" & Rs_Scheme(0) & "','" & _
    '                        Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "','" + Format(MskEd_FromDate, "MM/dd/yyyy") & _
    '                        "','" & Format(TxtToDate.Text, "MM/dd/yyyy") & "','" & User_Id & "','N'"
    '            End Select
    '
    '            MainConnection.Execute SqlStr
    '            SqlStr = ""
    '            Rs_Scheme.MoveNext
    '            I_SchCount = I_SchCount + 1
    '            Lbl_Process = ""
    '            DoEvents
    '        Loop
    '    Next
    '
    '    Lbl_Process = "Processing Final Report"
    '    DoEvents
    '    SqlStr = "Exec Monthly_Prog_Stmt_ERKM_Sp " & ClsCommon.DbDate(TxtFrDate.Text) & "," & DbDate(TxtToDate.Text) & ",'" & System_Name & "','" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "'"
    '    MainConnection.Execute (SqlStr)
    '    Lbl_Process = ""
    '
    '    SFMsgBox "Processing completed Successfully.", vbInformation, "Completed"
    '    Me.MousePointer = vbDefault
    '    Cmd_Show.Enabled = True
    '    DoEvents
    '    Set Rs_Scheme = Nothing
    '    Exit Sub
    '
    'Err_handler_Process:
    '    Call gHandleError
    '    Cmd_Show.Enabled = False
    '    Set Rs_Scheme = Nothing
    '    Me.MousePointer = vbDefault
    '    DoEvents
    '
    'End Sub
    '
    '
    'Private Sub Show_MonthlyProgress_Rpt_ERKM()
    'Dim Rpt_Name, SubRpt_Name           As String
    'Dim Rs_MISRpt                       As New ADODB.Recordset
    'Dim Rs_MISSubRpt(8)                 As New ADODB.Recordset
    '
    '
    'On Error GoTo ErrHandler
    '
    '    If Opt_BranchWise.Value = True Then
    '        ' ReportS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0) As Amount2, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3, Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0) As Amount4, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5, Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0) As Amount6, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7, Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0) As Amount8, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9, Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   And Rpt_ProcDetails_R_Tbl.Br_Code='" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "' " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 8 And Rpt_Generator_R_Tbl.Group_ID =0 " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISRpt = MainConnection.Execute(SqlStr)
    '
    '        ' 1. SHARES
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0) As Amount2, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3, Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0) As Amount4, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5, Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0) As Amount6, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7, Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0) As Amount8, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9, Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   And Rpt_ProcDetails_R_Tbl.Br_Code='" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "' " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 8 And Rpt_Generator_R_Tbl.Group_ID = 1   " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(0) = MainConnection.Execute(SqlStr)
    '
    '        ' 2. DEPOSITS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0) As Amount2, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3, Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0) As Amount4, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5, Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0) As Amount6, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7, Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0) As Amount8, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9, Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   And Rpt_ProcDetails_R_Tbl.Br_Code='" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "' " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 8 And Rpt_Generator_R_Tbl.Group_ID = 2  " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(1) = MainConnection.Execute(SqlStr)
    '
    '        ' 3. BORROWINGS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0) As Amount2, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3, Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0) As Amount4, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5, Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0) As Amount6, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7, Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0) As Amount8, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9, Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   And Rpt_ProcDetails_R_Tbl.Br_Code='" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "' " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 8 And Rpt_Generator_R_Tbl.Group_ID = 3  " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(2) = MainConnection.Execute(SqlStr)
    '
    '        ' 4. MDS
    '        SqlStr = "Select S.Sch_ShortName,G.SalaAmount,G.Sanction_NoDate,G.Register_ChalanNoAmt From GrpDeposit_Parameter_P_Tbl G " & _
    '            "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=G.Sch_Code " & _
    '            "Where G.Grp_EffectiveDate <" & ClsCommon.DbDate(TxtFrDate.Text) & " And G.GDS_DueDate > " & ClsCommon.DbDate(TxtFrDate.Text)
    '        Set Rs_MISSubRpt(3) = MainConnection.Execute(SqlStr)
    '
    '        ' 5. INVESTMENTS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0) As Amount2, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3, Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0) As Amount4, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5, Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0) As Amount6, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7, Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0) As Amount8, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9, Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   And Rpt_ProcDetails_R_Tbl.Br_Code='" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "' " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 8 And Rpt_Generator_R_Tbl.Group_ID = 5 " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(4) = MainConnection.Execute(SqlStr)
    '
    '        ' 6. LOANS TO MEMBERS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0) As Amount2, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3, Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0) As Amount4, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5, Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0) As Amount6, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7, Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0) As Amount8, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9, Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   And Rpt_ProcDetails_R_Tbl.Br_Code='" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "' " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 8 And Rpt_Generator_R_Tbl.Group_ID = 6 " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(5) = MainConnection.Execute(SqlStr)
    '
    '        ' 7. OTHER DETAILS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0) As Amount2, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3, Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0) As Amount4, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5, Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0) As Amount6, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7, Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0) As Amount8, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9, Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   And Rpt_ProcDetails_R_Tbl.Br_Code='" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "' " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 8 And Rpt_Generator_R_Tbl.Group_ID = 7 " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(6) = MainConnection.Execute(SqlStr)
    '
    '        ' 8. SPECIAL ACTIVITIES DONE
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0) As Amount1, Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0) As Amount2, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0) As Amount3, Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0) As Amount4, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0) As Amount5, Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0) As Amount6, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0) As Amount7, Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0) As Amount8, " & _
    '            "Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0) As Amount9, Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   And Rpt_ProcDetails_R_Tbl.Br_Code='" & Sfcmb_Branch.ArrayValue1(Sfcmb_Branch.ListIndex) & "' " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 8 And Rpt_Generator_R_Tbl.Group_ID = 8 " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(7) = MainConnection.Execute(SqlStr)
    '        Str_Flf = "Company|" & HttpContext.Current.Cache("CompanyName")  & "|Branch|" & Sfcmb_Branch.Text & "|Heading|Monthly Progress Report Between " & MskEd_FromDate & " And " & TxtToDate.Text
    '
    '    ElseIf Opt_Consolidation.Value = True Then
    '        ' Report
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)) As Amount2, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)) As Amount4, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)) As Amount6, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)) As Amount8, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 3 And Rpt_Generator_R_Tbl.Group_ID =0  " & _
    '            " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISRpt = MainConnection.Execute(SqlStr)
    '        ' 1. SHARE
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)) As Amount2, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)) As Amount4, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)) As Amount6, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)) As Amount8, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 3 And Rpt_Generator_R_Tbl.Group_ID =1  " & _
    '            " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(0) = MainConnection.Execute(SqlStr)
    '        ' 2. DEPOSITS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)) As Amount2, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)) As Amount4, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)) As Amount6, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)) As Amount8, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 3 And Rpt_Generator_R_Tbl.Group_ID =2  " & _
    '            " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(1) = MainConnection.Execute(SqlStr)
    '        ' 3. BORROWINGS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)) As Amount2, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)) As Amount4, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)) As Amount6, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)) As Amount8, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 3 And Rpt_Generator_R_Tbl.Group_ID =3  " & _
    '            " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(2) = MainConnection.Execute(SqlStr)
    '        ' 4. MDS
    '        SqlStr = "Select S.Sch_ShortName,G.SalaAmount,G.Sanction_NoDate,G.Register_ChalanNoAmt From GrpDeposit_Parameter_P_Tbl G " & _
    '            "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=G.Sch_Code " & _
    '            "Where G.Grp_EffectiveDate <" & ClsCommon.DbDate(TxtFrDate.Text) & " And G.GDS_DueDate > " & ClsCommon.DbDate(TxtFrDate.Text)
    '        Set Rs_MISSubRpt(3) = MainConnection.Execute(SqlStr)
    '
    '        ' 5. INVESTMENTS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)) As Amount2, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)) As Amount4, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)) As Amount6, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)) As Amount8, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 3 And Rpt_Generator_R_Tbl.Group_ID =5  " & _
    '            " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(4) = MainConnection.Execute(SqlStr)
    '
    '        ' 6. LOANS TO MEMBERS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)) As Amount2, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)) As Amount4, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)) As Amount6, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)) As Amount8, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 3 And Rpt_Generator_R_Tbl.Group_ID =6  " & _
    '            " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(5) = MainConnection.Execute(SqlStr)
    '
    '        ' 7. OTHER DETAILS
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)) As Amount2, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)) As Amount4, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)) As Amount6, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)) As Amount8, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 3 And Rpt_Generator_R_Tbl.Group_ID =7  " & _
    '            " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(6) = MainConnection.Execute(SqlStr)
    '
    '        ' 8. SPECIAL ACTIVITIES DONE
    '        SqlStr = "SELECT Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID," & _
    '            " Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount1,0)) As Amount1, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount2,0)) As Amount2, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount3,0)) As Amount3, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount4,0)) As Amount4, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount5,0)) As Amount5, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount6,0)) As Amount6, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount7,0)) As Amount7, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount8,0)) As Amount8, " & _
    '            "Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount9,0)) As Amount9, Sum(Isnull(Rpt_ProcDetails_R_Tbl.Amount10,0)) As Amount10 " & _
    '            "From Rpt_Master_R_Tbl INNER JOIN Rpt_Generator_R_Tbl ON Rpt_Master_R_Tbl.Report_ID = Rpt_Generator_R_Tbl.Report_ID " & _
    '            "AND Rpt_Master_R_Tbl.Group_ID = Rpt_Generator_R_Tbl.Group_ID /*AND Rpt_Master_R_Tbl.Head_ID = Rpt_Generator_R_Tbl.Head_ID*/  " & _
    '            "Left Outer JOIN Rpt_ProcDetails_R_Tbl ON Rpt_Generator_R_Tbl.Report_ID = Rpt_ProcDetails_R_Tbl.Report_ID " & _
    '            "AND Rpt_Generator_R_Tbl.Group_ID = Rpt_ProcDetails_R_Tbl.Group_ID AND " & _
    '            "Rpt_Generator_R_Tbl.Head_ID = Rpt_ProcDetails_R_Tbl.Head_ID And Rpt_Generator_R_Tbl.Sub_Head_ID = Rpt_ProcDetails_R_Tbl.Sub_Head_ID   " & _
    '            " And Rpt_ProcDetails_R_Tbl.Fr_Date=" & ClsCommon.DbDate(TxtFrDate.Text) & " And Rpt_ProcDetails_R_Tbl.To_Date=" & DbDate(TxtToDate.Text) & "  " & _
    '            "Where Rpt_Generator_R_Tbl.Report_ID = 3 And Rpt_Generator_R_Tbl.Group_ID =8  " & _
    '            " Group By Rpt_Generator_R_Tbl.Report_ID,Rpt_Generator_R_Tbl.Head_ID,Rpt_Generator_R_Tbl.Sub_Head_ID,Rpt_Master_R_Tbl.Group_Id,Rpt_Master_R_Tbl.Heading,Rpt_Generator_R_Tbl.Sub_Heading,Rpt_Generator_R_Tbl.Sub_Head_Order " & _
    '            "Order By Rpt_Generator_R_Tbl.Sub_Head_Order "
    '        Set Rs_MISSubRpt(7) = MainConnection.Execute(SqlStr)
    '
    '        Str_Flf = "Company|" & HttpContext.Current.Cache("CompanyName")  & "|Branch|Consolidation|Heading|Monthly Progress Report Between " & MskEd_FromDate & " And " & TxtToDate.Text
    '    End If
    '    SubRpt_Name = "SHARE|DEPOSIT|BORROWINGS|MDS|INVESTMENT|LOANS|OTHERS|SPECIAL"
    '
    '    Str_Flf = Str_Flf + "|Footer1|Clerk|Footer2|Accountant|Footer3|Manager|User_ID|" & User_Id
    '
    '    Call DisplayCrystalReport(File_Path & "\REPORTS\Monthly_Progress_ERKM.rpt", Rs_MISRpt, Str_Flf, True, , "Monthly Progress Report ERKM", SubRpt_Name, Rs_MISSubRpt)
    '    'If Rs_MISRpt.State = 1 Then Rs_MISRpt.Close
    '    Set Rs_MISRpt = Nothing
    '
    '    For i = 0 To 7
    '        Set Rs_MISSubRpt(i) = Nothing
    '    Next i
    '
    '    Exit Sub
    '
    'ErrHandler:
    '
    '        If Rs_MISRpt.State = 1 Then Rs_MISRpt.Close
    '        Set Rs_MISRpt = Nothing
    '        Set Rs_MISSubRpt(0) = Nothing
    '        Set Rs_MISSubRpt(1) = Nothing
    '        Call gHandleError
    'End Sub
    '



End Class