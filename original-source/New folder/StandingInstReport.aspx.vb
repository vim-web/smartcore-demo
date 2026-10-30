Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common

Partial Public Class StandingInstReport
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private M_DBConn As Connection
    Private Rpt_Qry As String
    Private Rpt_Path As String
    Private Rpt_Heading As String
    Private pt_Formula As String
    Private Rpt_ProcessQry As String
    Dim FormID As Integer = 1136
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
                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub BtnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnProcess.Click
        Try
            If ValidateFields() = False Then
                Exit Sub
            End If

            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA
            Dim Company_Name As String
            Dim StrSlf As String
            Dim Dt As DataTable


            M_DBConn = New Connection()
            Company_Name = M_CommonDa.GetCompanyDetails()

            Dim Terminal = System.Environment.MachineName 'Request.ServerVariables("REMOTE_HOST")

            'If DDlReportType.SelectedItem.Text = "CREDIT DETAILS" Then

            '    M_Sql = "SELECT E.*, Pr.Cr_Br_Code, Pr.CreditBranch, G.Acc_Name, Case When E.CrDr = 'C' Then Pr.CreditCust_Name Else Pr.DebitCust_Name End As Name" & _
            '            " FROM Executed_Standing_Inst_Tran E INNER JOIN Gen_Accountmaster_P_Tbl G ON E.Acc_Code = G.Acc_Code INNER JOIN" & _
            '            " StandInst_Processed_Tbl pr ON /*E.St_Type = pr.St_Type AND */ E.StID = pr.St_ID AND E.ExeDate = pr.Tr_Date" & _
            '            " WHERE " & _
            '            " ExeDate BETWEEN " & ClsComm.DbDate(Txt_FrDate.Text) & " AND " & ClsComm.DbDate(TxtToDate.Text) & " And CrDr = 'C' And Executed = 'Y' And Authorised = 'Y' And Authorsed = 'Y' "

            '    M_Sql = M_Sql & " And Pr.De_Br_Code='" & Ddl_Branch.SelectedValue.ToString & "' "

            '    M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            '    Session.Add("RptQry", M_Sql)

            '    If Opt_Summary.Checked Then
            '        Session.Add("RptPath", Server.MapPath("~/Reports/StandingInstruction/St_CrDr_Consolidated.rpt"))
            '    Else
            '        Session.Add("RptPath", Server.MapPath("~/Reports/StandingInstruction/St_Cr_Report.rpt"))
            '    End If

            '    StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Deposit Standing Report From : " & Txt_FrDate.Text & " To " & TxtToDate.Text & " |User_ID|" & Session("LoginID")
            '    StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            '    Session.Add("RptFormula", StrSlf)

            'ElseIf DDlReportType.SelectedItem.Text = "DEBIT DETAILS" Then

            '    M_Sql = "SELECT E.*, Pr.Cr_Br_Code, Pr.CreditBranch, G.Acc_Name, Case When E.CrDr = 'C' Then Pr.CreditCust_Name Else Pr.DebitCust_Name End As Name" & _
            '            " FROM Executed_Standing_Inst_Tran E INNER JOIN Gen_Accountmaster_P_Tbl G ON E.Acc_Code = G.Acc_Code INNER JOIN" & _
            '            " StandInst_Processed_Tbl pr ON /*E.St_Type = pr.St_Type AND */ E.StID = pr.St_ID AND E.ExeDate = pr.Tr_Date" & _
            '            " WHERE " & _
            '            " ExeDate BETWEEN " & ClsComm.DbDate(Txt_FrDate.Text) & " AND " & ClsComm.DbDate(TxtToDate.Text) & " And CrDr = 'D' And Executed = 'Y' And Authorised = 'Y' And Authorsed = 'Y' "

            '    M_Sql = M_Sql & " And Pr.De_Br_Code='" & Ddl_Branch.SelectedValue.ToString & "' "

            '    M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            '    Session.Add("RptQry", M_Sql)

            '    If Opt_Summary.Checked Then
            '        Session.Add("RptPath", Server.MapPath("~/Reports/StandingInstruction/St_CrDr_Consolidated.rpt"))
            '    Else
            '        Session.Add("RptPath", Server.MapPath("~/Reports/StandingInstruction/St_Dr_Report.rpt"))
            '    End If

            '    StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Deposit Standing Report From : " & Txt_FrDate.Text & " To " & TxtToDate.Text & " |User_ID|" & Session("LoginID")
            '    StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            '    Session.Add("RptFormula", StrSlf)
            'Else
            'M_Sql = "SELECT S.Tran_No,S.Tr_Date,S.St_Type,S.St_ID,S.De_Br_Code,S.De_Sch_Code,S.De_Acc_Code,D.Acc_No As De_Acc_No,S.De_Amount,S.St_RegDate,S.St_StartDate,S.St_EndDate,S.St_NextExecutionDate,S.St_TotalAmtExecuted,S.St_ApplicationNo,S.St_PayableTo," & _
            '        "S.St_Status,S.St_NoofExecution,S.DebitBranch,S.DebitScName,S.DebitCustID,S.DebitCust_Name,S.DebitAdd_Address1,S.DebitAdd_Address2,S.De_Balance,S.De_Dm_Status,S.FreqType,S.Cr_Br_Code, Cr_Sch_Code," & _
            '        "S.Cr_Acc_Code,S.Cr_Acc_No,S.Cr_Amount,S.Cr_AmtTyp,S.Cr_Status,S.CreditBranch,S.CreditScheme,S.CreditCustID,S.CreditCust_Name," & _
            '        "S.CreditAdd_Address1,S.CreditAdd_Address2,S.SectionName,S.Sch_Type,S.Cr_EffDate,S.Cr_DueDate,S.Cr_Balance,S.Cr_Dm_Status," & _
            '        "S.IsSuff_Balance,S.Inst_Amt,S.Inst_no,S.Fine_Amt,S.Inst_Upto,S.Terminal,S.Authorsed,S.MakerID,S.CheckerID," & _
            '        "S.Making_Time,S.Checking_Time,S.Status Processed FROM StandInst_Processed_Tbl S Left Outer Join Dep_Master_M_Tbl D ON D.Br_Code=S.De_Br_Code And D.Sch_Code=S.De_Sch_Code And D.Acc_No=S.De_Acc_No" & _
            '        " Where " & _
            '        " (Tr_Date BETWEEN " & ClsComm.DbDate(Txt_FrDate.Text) & " AND " & ClsComm.DbDate(Txt_FrDate.Text) & ") And S.Authorsed = 'Y'"
            'M_Sql = M_Sql & " And S.De_Br_Code='" & Ddl_Branch.SelectedValue.ToString & "' "

            'M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            'Session.Add("RptQry", M_Sql)

            'Session.Add("RptPath", Server.MapPath("~/Reports/StandingInstruction/Standing_Inst_Schwise.rpt"))
            'StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Deposit Standing Report From : " & Txt_FrDate.Text & " To " & TxtToDate.Text & " |User_ID|" & Session("LoginID")
            'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            'Session.Add("RptFormula", StrSlf)

            ''End If


            'Response.Redirect("~/RptPreview.aspx?Outputto=2", False)


            Dim HeaderArr(7, 4) As String
            Dim HeaderWidth(7, 1) As String
            HeaderArr(0, 0) = "SLNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "4"

            HeaderArr(1, 0) = "DrAccNo"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "13"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "DrScheme"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "2"
            HeaderArr(2, 3) = "2"

            HeaderArr(3, 0) = "DrCustName"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "50"
            HeaderArr(3, 3) = "13"


            HeaderArr(4, 0) = "CrAccNo"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "13"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "CrScheme"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "2"
            HeaderArr(5, 3) = "2"

            HeaderArr(6, 0) = "CrCustName"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "50"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "Amount"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "13"
            HeaderArr(7, 3) = "13"


            Session("RptHeader") = HeaderArr

            If ChkFailure.Checked Then
                M_Sql = "Exec StandingFailedList_Sp " & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "'"
            Else
                M_Sql = "Exec StandingInstRpt_Sp " & ClsComm.DbDate(Txt_FrDate.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "'"
            End If

            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try

            Txt_FrDate.Attributes.Add("readonly", "readonly")
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Ddl_Branch)
            Ddl_Branch.SelectedValue = Session("DefaultBrCode")


            Txt_FrDate.Text = Session("TransDate")

            M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), "DEPOSIT")
            Ddl_Scheme.Items.Add("<--ALL-->")
            Ddl_Scheme.SelectedIndex = Ddl_Scheme.Items.Count - 1

            Lbl_ToDt.Visible = False
            Txt_ToDate.Visible = False

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Function ValidateFields() As Boolean
        Try
            Dim Status As Boolean

            If Val(Txt_FrDate.Text.Length) > 0 Then
                Status = True
            Else
                ShowAlertMessage(" Invalid Date")
                Txt_FrDate.Focus()
                Return False
            End If

            Return Status
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Function

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub ChkFailure_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkFailure.CheckedChanged
        If ChkFailure.Checked Then
            Lbl_ToDt.Visible = True
            Txt_ToDate.Visible = True
        Else
            Lbl_ToDt.Visible = False
            Txt_ToDate.Visible = False
        End If
    End Sub
End Class