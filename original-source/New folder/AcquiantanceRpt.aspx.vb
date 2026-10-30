Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common


Partial Public Class AcquiantanceRpt
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1005
    Shared Obj_UserRights As Object







    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.Items.Insert(0, "All")
            CmbStafftype.Items.Add("Bank")
            CmbStafftype.Items.Add("Neethi")
            CmbStafftype.Items.Add("Contigent")
            CmbStafftype.Items.Add("All")
            CmbStafftype.SelectedIndex = 3
            TxtEffDate.Text = Session("TransDate")
            ChkPost.Checked = False
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            Session("SearchBranch") = CmbBranch.SelectedValue.ToString()
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String
        Dim StrSlf As String
        Dim Dt As DataTable
        Dim DTE As DateTime

        M_DBConn = New Connection()
        Company_Name = M_CommonDa.GetCompanyDetails()


        DTE = Convert.ToDateTime(TxtEffDate.Text)
        If ChkPost.Checked = True Then
            If TxtEffDate.Text = Session("TransDate") Then
                ' M_Sql = "EXEC EmpPayDetails_Sp " & CommonClass.DbDate(TxtEffDate.Text) & ",103,'Y','1','" & Session("UserID") & "'"
                M_Sql = "EXEC EmpPayDetails_Sp " & CommonClass.DbDate(TxtEffDate.Text) & ",0,'Y','" & IIf(CmbBranch.SelectedIndex = 0, "%", CmbBranch.SelectedValue) & "','" & Session("UserID") & "'"
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            End If
        Else
            If TxtEffDate.Text = Session("TransDate") Then
                ' M_Sql = "EXEC EmpPayDetails_Sp " & CommonClass.DbDate(TxtEffDate.Text)
                M_Sql = "EXEC EmpPayDetails_Sp " & CommonClass.DbDate(TxtEffDate.Text) & ",0,'N','" & IIf(CmbBranch.SelectedIndex = 0, "%", CmbBranch.SelectedValue) & "'"
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

            End If
        End If
        Dim str As String
        str = IIf(CmbStafftype.SelectedIndex = 3, "%", CmbStafftype.SelectedIndex)
        '  M_Sql = "EXEC EmpAcquiantance_Sp  " & CommonClass.DbDate(TxtEffDate.Text) & ",'" & IIf(CmbBranch.SelectedIndex = 0, "%", CmbBranch.SelectedIndex) & "','" & str & "','" & Session("UserID") & "'"
        M_Sql = "EXEC EmpAcquiantance_Sp  " & CommonClass.DbDate(TxtEffDate.Text) & ",'" & IIf(CmbBranch.SelectedIndex = 0, "%", CmbBranch.SelectedValue) & "'"
        M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

        'M_Sql = ""

        'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        Session.Add("RptQry", M_Sql)
        Session.Add("RptPath", Server.MapPath("~/Reports/EmployeeAcquiantance.rpt"))

        StrSlf = "Company|" & Company_Name & "|Heading|EMPLOYEE ACQUIANTANCE FOR THE MONTH OF  " & MonthName(DTE.Date.Month) & " - " & Year(DTE.Date)
        'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

        Session.Add("RptFormula", StrSlf)

        Response.Redirect("~/RptPreView.aspx?Outputto=2", False)

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdPrint.Click
        Dim M_PayrollBL As New PayrollBL
        Dim M_Dt As New DataTable
        Dim Prn As New Print
        Dim Qry As String
        Dim i As Int32
        Dim DtLoc As New DataTable
        Dim Tran_No As String

        M_DBConn = New Connection()
        M_Dt = M_PayrollBL.Fill_PayVoucher(TxtEffDate.Text)
        If M_Dt.Rows.Count > 0 Then
            Tran_No = M_Dt.Rows(0)("Tran_No").ToString
            Qry = "select * from Transaction_T_Tbl where tran_no='" & Tran_No & "'"
            M_DBConn.ExecuteDataTable(DtLoc, Qry, False)


            Prn.SetUPPrintSal(Session("DefaultBrCode"), "GENERAL", "93", Tran_No, "1", 1, True, Session("TransDate"), Session("HostID").ToString(), DtLoc, M_Dt, "SAL")





        End If

    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            Session("SearchBranch") = CmbBranch.SelectedValue.ToString()
        Catch ex As Exception

        End Try
    End Sub
End Class