Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports CrystalDecisions.Shared

Partial Public Class IntPostedList
    Inherits System.Web.UI.Page
    Dim M_Sql As String
    Dim M_ReportBl As New ReportsBL
    Private Shared CommCls As New CommonClass()

    Dim M_CommonDa As New CommonDA
    Shared Company_Name As String
    Dim StrSlf As String
    Dim OpCash As Double
    Dim ClCash As Double
    Dim Dt As DataTable
    Private Shared M_DBConn As New Connection
    Dim FormID As Integer = 1074
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

                'CmbOutput.Items.Add("Printer")
                'CmbOutput.Items.Add("Screen")
                'CmbOutput.SelectedIndex = 0
                Company_Name = M_CommonDa.GetCompanyDetails()
                Load_DefaultValues()
                Assign_Values()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        CmbSection.SelectedIndex = 0
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.Text)
        M_CommonBL = Nothing
        CmbBranch.SelectedValue = Session("DefaultBrCode")

        TxtFromDate.Text = Session("TransDate")
        TxtTodate.Text = Session("TransDate")


    End Sub

    Private Sub Assign_Values()
        Dim M_CommonBL As New CommonBL

        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")

        M_CommonBL = Nothing

    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSection.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.Text)
    End Sub


    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim RptName As String = ""
            Dim HeadingName As String = ""
            Dim ClsComm As New CommonClass()

            HeadingName = "INTEREST POSTED LIST"
            RptName = "InterestPosted_Datails_Rpt.rpt"


            If CmbSection.SelectedItem.Text = "DEPOSIT" Then


                If CmbScheme.SelectedItem.Text = "-- ALL --" Then

                    M_Sql = "SELECT Transaction_T_Tbl.Acc_No, Transaction_T_Tbl.Tr_Date, Transaction_T_Tbl.Narration, Deposit_Detail_Vw.Sch_Code, Deposit_Detail_Vw.Sch_Name, " & _
                              "Transaction_T_Tbl.Cash + Transaction_T_Tbl.Transfer AS Amount, Deposit_Detail_Vw.Dm_DepDate AS OpeningDate, Deposit_Detail_Vw.CustName, " & _
                              "(SELECT     TOP 1 Gen_TODMaster_M_Tbl.Issue_Date From Gen_TODMaster_M_Tbl WHERE Gen_TODMaster_M_Tbl.Status <> 'C' AND Gen_TODMaster_M_Tbl.Sch_Code = Deposit_Detail_Vw.Sch_Code AND " & _
                              "Gen_TODMaster_M_Tbl.Acc_No = Transaction_T_Tbl.Acc_No And Gen_TODMaster_M_Tbl.Br_Code = Transaction_T_Tbl.Br_Code) AS Issue_Date FROM Transaction_T_Tbl INNER JOIN Deposit_Detail_Vw ON Transaction_T_Tbl.Acc_No = Deposit_Detail_Vw.AcNo And  Transaction_T_Tbl.Sch_Code = Deposit_Detail_Vw.Sch_Code And Transaction_T_Tbl.Br_Code = Deposit_Detail_Vw.BrCode " & _
                              "WHERE (Transaction_T_Tbl.Tran_Function = '" & "IN" & "') AND (Transaction_T_Tbl.Gl_Code = Transaction_T_Tbl.Acc_Code) and dbo.Transaction_T_Tbl.Tr_Date >= " & ClsComm.DbDate(TxtFromDate.Text) & " and dbo.Transaction_T_Tbl.Tr_Date <= " & ClsComm.DbDate(TxtTodate.Text) & " And Transaction_T_Tbl.Br_Code = '" & CmbBranch.SelectedValue.ToString() & "'"

                Else



                    M_Sql = "SELECT Transaction_T_Tbl.Acc_No, Transaction_T_Tbl.Tr_Date, Transaction_T_Tbl.Narration, Deposit_Detail_Vw.Sch_Code, Deposit_Detail_Vw.Sch_Name, " & _
                              "Transaction_T_Tbl.Cash + Transaction_T_Tbl.Transfer AS Amount, Deposit_Detail_Vw.Dm_DepDate AS OpeningDate, Deposit_Detail_Vw.CustName, " & _
                              "(SELECT     TOP 1 Gen_TODMaster_M_Tbl.Issue_Date From Gen_TODMaster_M_Tbl WHERE Gen_TODMaster_M_Tbl.Status <> 'C' AND Gen_TODMaster_M_Tbl.Sch_Code = Deposit_Detail_Vw.Sch_Code AND " & _
                              "Gen_TODMaster_M_Tbl.Acc_No = Transaction_T_Tbl.Acc_No And Gen_TODMaster_M_Tbl.Br_Code = Transaction_T_Tbl.Br_Code) AS Issue_Date FROM Transaction_T_Tbl INNER JOIN Deposit_Detail_Vw ON Transaction_T_Tbl.Acc_No = Deposit_Detail_Vw.AcNo And  Transaction_T_Tbl.Sch_Code = Deposit_Detail_Vw.Sch_Code And Transaction_T_Tbl.Br_Code = Deposit_Detail_Vw.BrCode " & _
                              "WHERE (Transaction_T_Tbl.Tran_Function = '" & "in" & "') AND (Transaction_T_Tbl.Gl_Code = Transaction_T_Tbl.Acc_Code) and dbo.Transaction_T_Tbl.Tr_Date >= " & ClsComm.DbDate(TxtFromDate.Text) & " and dbo.Transaction_T_Tbl.Tr_Date <= " & ClsComm.DbDate(TxtTodate.Text) & " And Transaction_T_Tbl.Br_Code = '" & CmbBranch.SelectedValue.ToString() & "' and " & _
                              "dbo.Deposit_Detail_Vw.Sch_Code= '" & CmbScheme.SelectedValue.ToString() & "'"

                End If


            ElseIf CmbSection.SelectedItem.Text = "LOAN" Then

                If CmbScheme.SelectedItem.Text = "-- ALL --" Then
                    M_Sql = " SELECT dbo.Transaction_T_Tbl.Acc_No, dbo.Loan_Detail_Vw.CustName, dbo.Transaction_T_Tbl.Tr_Date, " & _
                              " dbo.Transaction_T_Tbl.Cash + dbo.Transaction_T_Tbl.Transfer AS Amount, dbo.Transaction_T_Tbl.Narration, dbo.Loan_Detail_Vw.Sch_Code, " & _
                              " dbo.Loan_Detail_Vw.Sch_Name, dbo.Loan_Detail_Vw.Lm_LoanDate AS OpeningDate, Null as Issue_Date FROM dbo.Transaction_T_Tbl INNER JOIN " & _
                              " dbo.Loan_Detail_Vw ON dbo.Transaction_T_Tbl.Acc_No = dbo.Loan_Detail_Vw.AcNo And  dbo.Transaction_T_Tbl.Sch_Code = dbo.Loan_Detail_Vw.Sch_Code And dbo.Transaction_T_Tbl.Br_Code = dbo.Loan_Detail_Vw.BrCode WHERE (dbo.Transaction_T_Tbl.Tran_Function = 'IN') AND  " & _
                              " (dbo.Transaction_T_Tbl.Gl_Code = dbo.Transaction_T_Tbl.Acc_Code) and dbo.Transaction_T_Tbl.Tr_Date >= " & ClsComm.DbDate(TxtFromDate.Text) & " and dbo.Transaction_T_Tbl.Tr_Date <= " & ClsComm.DbDate(TxtTodate.Text) & " And dbo.Transaction_T_Tbl.Br_Code = '" & CmbBranch.SelectedValue.ToString() & "'"
                Else
                    M_Sql = " SELECT dbo.Transaction_T_Tbl.Acc_No, dbo.Loan_Detail_Vw.CustName, dbo.Transaction_T_Tbl.Tr_Date, " & _
                              " dbo.Transaction_T_Tbl.Cash + dbo.Transaction_T_Tbl.Transfer AS Amount, dbo.Transaction_T_Tbl.Narration, dbo.Loan_Detail_Vw.Sch_Code, " & _
                              " dbo.Loan_Detail_Vw.Sch_Name, dbo.Loan_Detail_Vw.Lm_LoanDate AS OpeningDate,Null as Issue_Date FROM dbo.Transaction_T_Tbl INNER JOIN " & _
                              " dbo.Loan_Detail_Vw ON dbo.Transaction_T_Tbl.Acc_No = dbo.Loan_Detail_Vw.AcNo And dbo.Transaction_T_Tbl.Sch_Code = dbo.Loan_Detail_Vw.Sch_Code And dbo.Transaction_T_Tbl.Br_Code = dbo.Loan_Detail_Vw.BrCode WHERE (dbo.Transaction_T_Tbl.Tran_Function = 'IN') AND  " & _
                              " dbo.Transaction_T_Tbl.Gl_Code = dbo.Transaction_T_Tbl.Acc_Code and dbo.Transaction_T_Tbl.Tr_Date >= " & ClsComm.DbDate(TxtFromDate.Text) & " and dbo.Transaction_T_Tbl.Tr_Date <= " & ClsComm.DbDate(TxtTodate.Text) & "  and " & _
                              " dbo.Loan_Detail_Vw.Sch_Code= '" & CmbScheme.SelectedValue.ToString() & "' And dbo.Transaction_T_Tbl.Br_Code = '" & CmbBranch.SelectedValue.ToString() & "'"
                End If




            End If



            M_Sql = M_Sql & " ORDER BY RIGHT('00000000000000000' + dbo.Transaction_T_Tbl.Acc_No, 18)"

            ' Str = "Company|" & Bank_Name & "|Branch|" & Branch_Name & "|Heading|Deposit " & Scmb_Type.Text & " Details   " & Msk_FromDate & " To " & Msk_ToDate.Text & "|User|" & User_Id





            StrSlf = "Company|" & Company_Name & "|Branch|" & CmbBranch.SelectedItem.ToString & "|Heading|" & CmbSection.SelectedItem.Text & " " & HeadingName & " REPORT FROM " & TxtFromDate.Text & " TO " & TxtTodate.Text & " |User_ID|" & Session("LoginID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

            Session.Add("RptQry", M_Sql)

            Session.Add("RptPath", Server.MapPath("~/Reports/" & RptName))

            Session.Add("RptFormula", StrSlf)
            'If CmbOutput.SelectedIndex = 0 Then
            '    Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
            'Else
            '    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            'End If

            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub


End Class