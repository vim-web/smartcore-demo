Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common

Partial Public Class PersonalLedger
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1114
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
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

                Session("SearchType") = "ACCOUNTNO"
                
                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1
                Load_DefaultValues()
            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "ACCOUNTNO" Then
                    Txt_AccNo.Text = Session("SearchResult")
                    Txt_AccNo_TextChanged(Me.Txt_AccNo, System.EventArgs.Empty)
                End If
                Session("SearchResult") = ""
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Ddl_Branch)
            M_CommonBL.Fill_Section(CmbSection)
            'M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"))
            Txt_FromDate.Text = Session("TransDate")
            Txt_ToDate.Text = Session("TransDate")
            Ddl_Branch.SelectedValue = Session("DefaultBrCode")
            Session("SearchBranch") = Ddl_Branch.SelectedValue.ToString()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub Txt_AccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_AccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim Dt_AcDtls As DataTable

            If Txt_AccNo.Text.Trim.Length <= 7 Then
                Txt_AccNo.Text = Right("000" + Ddl_Branch.SelectedValue, 3) + Right("000" + Ddl_Scheme.SelectedValue, 3) + Right("0000000" + Txt_AccNo.Text, 7)
            End If

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(Ddl_Branch.SelectedValue, Ddl_Scheme.SelectedValue, Txt_AccNo.Text, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                Txt_CustId.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Txt_CustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                Txt_CustAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString() & Space(5) & Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                CmbSection.SelectedValue = CmbSection.Items.FindByText(Dt_AcDtls.Rows(0)("section")).Value
                'CmbSection.Items.FindByText("LOAN").Selected = True
                CmbSection_SelectedIndexChanged(Me.CmbSection, System.EventArgs.Empty)
                Ddl_Scheme.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
            Else
                ShowAlertMessage("Invalid Account Number!!!")
                Txt_AccNo.Text = ""
            End If
            M_CommonBL = Nothing

            If Len(Txt_CustId.Text) > 0 Then CmbOutput.Focus()
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA
            Dim Company_Name As String
            Dim StrSlf As String

            M_DBConn = New Connection()
            Company_Name = M_CommonDa.GetCompanyDetails()

            Dim Terminal = System.Environment.MachineName
            Dim Sch_Section As String

            ' Sch_Section = M_CommonDa.GetSchemeSection(Ddl_Scheme.SelectedValue)

            Sch_Section = CmbSection.Text

            If Sch_Section = "DEPOSIT" Then

                Dim HeaderArr(5, 4) As String
                Dim HeaderWidth(5, 1) As String
                HeaderArr(0, 0) = "Date"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "10"

                HeaderArr(1, 0) = "Payment"
                HeaderArr(1, 1) = "R"
                HeaderArr(1, 2) = "10"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "Receipt"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "10"

                HeaderArr(3, 0) = "INT"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "8"
                HeaderArr(3, 3) = "8"

                HeaderArr(4, 0) = "Balance"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Narration"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "30"
                HeaderArr(5, 3) = "30"

                Session("RptHeader") = HeaderArr

                M_Sql = "Exec DepositPersonalLedgerRpt_Sp '" & Ddl_Branch.SelectedValue & "','DEPOSIT' ,'" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                'Session("RptQry") = M_Sql
                'Response.Redirect("~/PrnView.aspx?PageType=80", False)

                ' Exit Sub

                M_Sql = "Exec PersonalLedger_Sp '" & Ddl_Branch.SelectedValue & "','DEPOSIT' ,'" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                M_Sql = ""

                M_Sql = "Select * From PersonalLedger_R_Tbl Where Sch_Code = '" & Ddl_Scheme.SelectedValue & "' And Acc_No = '" & Txt_AccNo.Text & "'  And Terminal = '" & Terminal & "' Order By Tr_Date,Tran_ID "
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

                Session.Add("RptQry", M_Sql)
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/Deposit_PersonalLedger.rpt"))
            ElseIf Sch_Section = "SHARE" Then

                Dim HeaderArr(5, 4) As String
                Dim HeaderWidth(5, 1) As String
                HeaderArr(0, 0) = "Date"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "10"

                HeaderArr(1, 0) = "Payment"
                HeaderArr(1, 1) = "R"
                HeaderArr(1, 2) = "10"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "Receipt"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "10"

                HeaderArr(3, 0) = "CHARGES"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "8"
                HeaderArr(3, 3) = "8"


                HeaderArr(4, 0) = "Balance"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Narration"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "15"
                HeaderArr(5, 3) = "15"

                Session("RptHeader") = HeaderArr

                M_Sql = "Exec SharePersonalLedgerRpt_Sp '" & Ddl_Branch.SelectedValue & "','SHARE' ,'" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                Session("RptQry") = M_Sql
                'Response.Redirect("~/PrnView.aspx?PageType=80", False)

                ' Exit Sub


                M_Sql = "Exec PersonalLedger_Sp '" & Ddl_Branch.SelectedValue & "','SHARE' ,'" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                M_Sql = ""

                M_Sql = "Select * From PersonalLedger_R_Tbl Where Sch_Code = '" & Ddl_Scheme.SelectedValue & "' And Acc_No = '" & Txt_AccNo.Text & "' And Terminal = '" & Terminal & "' Order By Tr_Date ,Tran_ID "
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)


                Session.Add("RptQry", M_Sql)
                Session.Add("RptPath", Server.MapPath("~/Reports/Share/Share_PersonalLedger.rpt"))

            ElseIf Sch_Section = "SUSPENSE" Then

                Dim HeaderArr(5, 4) As String
                Dim HeaderWidth(5, 1) As String
                HeaderArr(0, 0) = "Date"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "10"

                HeaderArr(1, 0) = "Narration"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "25"
                HeaderArr(1, 3) = "25"

                HeaderArr(2, 0) = "Credit"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "10"

                HeaderArr(3, 0) = "Debit"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "Balance"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Balance Type"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "5"
                HeaderArr(5, 3) = "5"

                Session("RptHeader") = HeaderArr

                M_Sql = "Exec [Suspense_PersonalLedger] '" & Ddl_Branch.SelectedValue & "','" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                'Session("RptQry") = M_Sql
                'Response.Redirect("~/PrnView.aspx?PageType=80", False)
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                M_Sql = ""
                M_Sql = "SELECT SL.*,GSM.Sch_Name,Sm_MemNo,Cust_Name,GSM_Share.Sch_Name as ShareType,GB.Br_Name,SL.Br_Code	 FROM SuspLedger_R_Tbl AS SL"
                M_Sql = M_Sql + " INNER JOIN Gen_SchemeMaster_P_Tbl AS GSM ON SL.Sch_Code = GSM.Sch_Code"
                M_Sql = M_Sql + " INNER JOIN Cust_Master_M_Tbl AS CM ON CM.Cust_ID = SL.Cust_ID "
                M_Sql = M_Sql + " LEFT OUTER JOIN Share_Master_M_Tbl AS SM ON SM.Cust_ID=CM.Cust_ID LEFT OUTER JOIN"
                M_Sql = M_Sql + " Gen_SchemeMaster_P_Tbl GSM_Share ON GSM_Share.Sch_Code = SM.Sch_Code"
                M_Sql = M_Sql + " INNER Join"
                M_Sql = M_Sql + " Gen_BranchDetails_P_Tbl AS GB ON"
                M_Sql = M_Sql + " GB.Br_Code = SL.Br_Code"
                M_Sql = M_Sql + " ORDER BY Tran_Date,Sl_No "
                'M_Sql = "Select * From PersonalLedger_R_Tbl Where Sch_Code = '" & Ddl_Scheme.SelectedValue & "' And Acc_No = '" & Txt_AccNo.Text & "' And Terminal = '" & Terminal & "' Order By Tr_Date ,Tran_ID "
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

                Session.Add("RptQry", M_Sql)
                Session.Add("RptPath", Server.MapPath("~/Reports/Suspense/Susp_Personal_Ledger.rpt"))
               

                ''Exit Sub

                ''M_Sql = "Exec PersonalLedger_Sp '" & Ddl_Branch.SelectedValue & "','SHARE' ,'" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                ''M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                ''M_Sql = ""

                ''M_Sql = "Select * From PersonalLedger_R_Tbl Where Sch_Code = '" & Ddl_Scheme.SelectedValue & "' And Acc_No = '" & Txt_AccNo.Text & "' And Terminal = '" & Terminal & "' Order By Tr_Date ,Tran_ID "
                ''M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)


                ''Session.Add("RptQry", M_Sql)
                ''Session.Add("RptPath", Server.MapPath("~/Reports/Share/Share_PersonalLedger.rpt"))

            ElseIf Sch_Section = "LOAN" Then

                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String
                HeaderArr(0, 0) = "Date"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "6"
                HeaderArr(0, 3) = "12"

                HeaderArr(1, 0) = "Payment"
                HeaderArr(1, 1) = "R"
                HeaderArr(1, 2) = "10"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "Receipt"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "10"

                HeaderArr(3, 0) = "Interest"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "Charges"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "8"

                HeaderArr(5, 0) = "Balance"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "7"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "Narration"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "25"
                HeaderArr(6, 3) = "25"



                'Session("RptHeader") = HeaderArr

                'M_Sql = "Exec LoanPersonalLedgerRpt_Sp '" & Ddl_Branch.SelectedValue & "','LOAN' ,'" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                'If ChkLoanARC.Checked = True Then
                '    M_Sql = M_Sql & ",'Y'"
                'Else
                '    M_Sql = M_Sql & ",'N'"
                'End If
                'Session("RptQry") = M_Sql
                'Response.Redirect("~/PrnView.aspx?PageType=80", False)

                'Exit Sub

                M_Sql = "Exec PersonalLedger_Sp '" & Ddl_Branch.SelectedValue & "','LOAN' ,'" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                M_Sql = ""

                M_Sql = "Select * From PersonalLedger_R_Tbl Where Sch_Code = '" & Ddl_Scheme.SelectedValue & "' And Acc_No = '" & Txt_AccNo.Text & "' And Terminal = '" & Terminal & "'  Order By Tr_Date,Tran_ID "
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)


                Session.Add("RptQry", M_Sql)
                Session.Add("RptPath", Server.MapPath("~/Reports/LOANS/Loan_PersonalLedger.rpt"))


            ElseIf Sch_Section = "GROUP DEPOSIT" Then

                Dim HeaderArr(12, 4) As String
                Dim HeaderWidth(12, 1) As String
                HeaderArr(0, 0) = "Date"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "10"

                HeaderArr(1, 0) = "INO"
                HeaderArr(1, 1) = "R"
                HeaderArr(1, 2) = "5"
                HeaderArr(1, 3) = "5"

                HeaderArr(2, 0) = "Auc Date"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "10"

                HeaderArr(3, 0) = "Credit"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "Debit"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Discount"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "IntAmt"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "6"
                HeaderArr(6, 3) = "6"

                HeaderArr(7, 0) = "Forfiet"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "10"
                HeaderArr(7, 3) = "10"

                HeaderArr(8, 0) = "BankChr"
                HeaderArr(8, 1) = "R"
                HeaderArr(8, 2) = "10"
                HeaderArr(8, 3) = "10"

                HeaderArr(9, 0) = "Chrg Dr"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "10"
                HeaderArr(9, 3) = "10"

                HeaderArr(10, 0) = "Chrg Dr"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "10"
                HeaderArr(10, 3) = "10"

                HeaderArr(11, 0) = "Balance"
                HeaderArr(11, 1) = "R"
                HeaderArr(11, 2) = "10"
                HeaderArr(11, 3) = "10"

                HeaderArr(12, 0) = "REMARKS"
                HeaderArr(12, 1) = "L"
                HeaderArr(12, 2) = "15"
                HeaderArr(12, 3) = "15"


                Session("RptHeader") = HeaderArr

                M_Sql = "Exec GDSPersonalLedgerRpt_Sp '" & Ddl_Branch.SelectedValue & "','" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Terminal & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub

            End If

            StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Personal Ledger Between  " & Txt_FromDate.Text & " - " & Txt_ToDate.Text & " |User_ID|" & Session("UserID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

            ' Session.Add("PersonalLedger", "~/PersonalLedger.aspx")
            Session.Add("RptFormula", StrSlf)
            If CmbOutput.SelectedIndex = 0 Then
                Response.Redirect("~/RptPreView.aspx?Outputto=1", False)
            Else
                Response.Redirect("~/RptPreView.aspx?Outputto=2", False)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSection.SelectedIndexChanged
        If CmbSection.Text = "GROUP DEPOSIT" Then
            Ddl_Scheme.Enabled = False
        Else
            Try
                M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), CmbSection.SelectedItem.ToString)
                Ddl_Scheme.SelectedIndex = 0
                Session("SearchSection") = CmbSection.SelectedItem.Text()
                Session("SearchScheme") = Ddl_Scheme.SelectedValue.ToString()
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try

        End If

        If CmbSection.Text = "LOAN" Then
            ChkLoanARC.Visible = True
        Else
            ChkLoanARC.Visible = False
        End If

    End Sub

    Protected Sub Ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged
        Try
            Session("SearchBranch") = Ddl_Branch.SelectedValue.ToString()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ddl_Scheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Scheme.SelectedIndexChanged
        Try
            Session("SearchScheme") = Ddl_Scheme.SelectedValue.ToString()
        Catch ex As Exception

        End Try

    End Sub

End Class