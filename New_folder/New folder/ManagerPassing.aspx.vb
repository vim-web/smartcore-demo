Imports System.Data
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Imports SMARTCORE_BL
Partial Public Class ManagerPassing
    Inherits System.Web.UI.Page
    Shared Obj_UserRights As Object
    Dim ClsCom As New CommonClass
    Dim FormID As Integer = 1105
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If


            If IsPostBack = False Then
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If



                DgvPassing.DataSource = GetTable()
                DgvPassing.DataBind()
            End If

            If Len(Request.QueryString("Passing")) > 0 Then
                Call Refresh()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub


    Public Function GetTable() As DataTable
        Try
            Dim PassingTbl As DataTable = New DataTable()
            PassingTbl.Columns.Add("Authorised", GetType(Boolean))
            PassingTbl.Columns.Add("VouNo", GetType(String))
            PassingTbl.Columns.Add("Particulars", GetType(String))
            PassingTbl.Columns.Add("RecCash", GetType(Double))
            PassingTbl.Columns.Add("RecTransfer", GetType(Double))
            PassingTbl.Columns.Add("PayCash", GetType(Double))
            PassingTbl.Columns.Add("PayTransfer", GetType(Double))
            PassingTbl.Columns.Add("MakerID", GetType(String))
            PassingTbl.Columns.Add("CheckerID", GetType(String))
            PassingTbl.Columns.Add("MakingTime", GetType(Date))
            PassingTbl.Columns.Add("Tran_No", GetType(String))
            PassingTbl.Columns.Add("Maker", GetType(String))
            PassingTbl.Rows.Add(False, "     ", "", "0", "0", "0", "0", "", "", "01/01/1900", "", "")

            Return PassingTbl
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Function
    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoggToFile(ByVal Str As String)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(Str)
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub
    'Protected Sub DgvPassing_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvPassing.RowCommand
    '    Try

    '        If Obj_UserRights.Authn_Right = False Then
    '            ShowAlertMessage("You Have No Permission To Authorise this Transaction!!!")
    '            Exit Sub
    '        End If


    '        If e.CommandName.ToUpper = "SELECT" Then
    '            Dim Tran_No As String
    '            Dim M_DBConn As New Connection()
    '            Dim SqlStr As String
    '            Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
    '            Dim M_CommonBL As New CommonBL
    '            Dim FromNm As String
    '            Dim MakerID As String
    '            Dim IsDelete As Boolean = False

    '            Tran_No = DgvPassing.DataKeys(RowIndex)("Tran_No").ToString()
    '            MakerID = DgvPassing.DataKeys(RowIndex)("Maker").ToString()
    '            IsDelete = DgvPassing.DataKeys(RowIndex)("Authorised").ToString()

    '            If IsDelete = True Then
    '                ShowAlertMessage(" Transaction is Authorised!!!")
    '                Exit Sub
    '            End If

    '            If MakerID = Session("LoginID").ToString() And Session("LoginID").ToString.ToUpper() <> "SA" Then
    '                ShowAlertMessage("Maker Can't Authorise Transaction!!!")
    '                Exit Sub
    '            End If



    '            FromNm = M_CommonBL.Get_TranFrom(Tran_No)


    '            'If (FromNm = "") Then
    '            '    Dim M_sql As String
    '            '    M_sql = "select Tran_No from Dep_Master_M_Tbl where Acc_No='" & Tran_No & "'"
    '            '    Tran_No = M_DBConn.ExecuteScalar(M_sql, 0, False)
    '            '    FromNm = M_CommonBL.Get_TranFrom(Tran_No)
    '            'End If

    '            Select Case FromNm
    '                Case "GOLD"
    '                    Response.Redirect("~/GoldOpen.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "DEPMAS"
    '                    Response.Redirect("~/DepositMaster.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "SHAREMAS"
    '                    Response.Redirect("~/ShareMaster.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "MLOAN"
    '                    Response.Redirect("~/LoanMaster.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "SUSPMAS", "TOURMAS"
    '                    Response.Redirect("~/SuspenseMaster.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "DLOAN"
    '                    Response.Redirect("~/DepositLoan.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "TRAN"
    '                    Response.Redirect("~/Transaction.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "LA"
    '                    Response.Redirect("~/LoanApplicationForm.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "LF"
    '                    Response.Redirect("~/LoanApplication.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "LS"
    '                    Response.Redirect("~/LoanApplication.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "GDSAUCT"
    '                    Response.Redirect("~/ChittyAuction.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "INVMAS"
    '                    Response.Redirect("~/InvestmentMaster.aspx?TRANID=" & Tran_No)
    '                Case "LOCKER"
    '                    Response.Redirect("~/LockerMaster.aspx?TRANID=" & Tran_No)
    '                Case "BLR"
    '                    Response.Redirect("~/BILLSREALISATION.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "BANKTR"
    '                    Response.Redirect("~/CashTr.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '                Case "BULKENTRY"
    '                    Response.Redirect("~/BulkTransactionEntry.aspx?TRANID=" & Tran_No)
    '                    Exit Sub
    '            End Select

    '            'Response.Redirect("~/Transaction.aspx?TRANID=" & DgvPassing.DataKeys(RowIndex)("Tran_No").ToString())
    '            'Exit Sub


    '            'Response.Redirect("~/GoldOpen.aspx?TRANID=" & DgvPassing.DataKeys(RowIndex)("Tran_No").ToString())
    '            'Exit Sub

    '            'If DgvPassing.DataKeys(RowIndex)("Authorised").ToString() = False Then


    '            SqlStr = "Exec Transaction_Authrise_Sp '" & Tran_No & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
    '                ",'" & Session("UserID") & "','" & Session("DefaultBrCode").ToString() & "'"
    '            M_DBConn.ExecuteScalar(SqlStr, , False)

    '            'Dim M_CommonBL As New CommonBL
    '            'If M_CommonBL.Authorise_Tran(Tran_No, Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
    '            '    ShowAlertMessage("Authotisation Failed")
    '            'End If
    '            'M_CommonBL = Nothing

    '            Call Refresh()
    '            'End If
    '        End If
    '    Catch ex As Exception
    '        ShowAlertMessage(ex.Message)
    '        LoggError(ex)
    '    End Try
    'End Sub

    Protected Sub DgvPassing_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvPassing.RowCommand
        ''Anil for Debanture Application 
        Try

            If Obj_UserRights.Authn_Right = False Then
                ShowAlertMessage("You Have No Permission To Authorise this Transaction!!!")
                Exit Sub
            End If


            If e.CommandName.ToUpper = "SELECT" Then
                Dim Tran_No As String
                Dim M_DBConn As New Connection()
                Dim SqlStr As String
                Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
                Dim M_CommonBL As New CommonBL
                Dim FromNm As String
                Dim MakerID As String
                Dim IsDelete As Boolean = False

                Tran_No = DgvPassing.DataKeys(RowIndex)("Tran_No").ToString()
                MakerID = DgvPassing.DataKeys(RowIndex)("Maker").ToString()
                IsDelete = DgvPassing.DataKeys(RowIndex)("Authorised").ToString()

                Dim M_SuspDt As DataTable
                Dim SuspSchCode As String = ""

                SqlStr = "Exec Check_Susp_ShareAuth_Tbl_Sp '" & Tran_No & "'"
                M_DBConn.ExecuteDataTable(M_SuspDt, SqlStr, False)
                SqlStr = ""


                'If IsDelete = True Then
                '    ShowAlertMessage(" Transaction is Authorised!!!")
                '    Exit Sub
                'End If

                If IsDelete = True And M_SuspDt.Rows(0).Item("Status") = "C" Then
                    ShowAlertMessage(" Transaction is Authorised!!!")
                    Exit Sub
                End If


                If MakerID = Session("LoginID").ToString() And Session("LoginID").ToString.ToUpper() <> "SA" Then
                    ShowAlertMessage("Maker Can't Authorise Transaction!!!")
                    Exit Sub
                End If



                FromNm = M_CommonBL.Get_TranFrom(Tran_No)


                If FromNm = "SHAREMAS" Then
                    SuspSchCode = M_SuspDt.Rows(0).Item("SuspSchCode")
                End If
                '' If M_SuspDt.Rows(0).Item("Status") = "N" And Val(M_SuspDt.Rows(0).Item("Hierarchy_Level")) = 3 Then
                If M_SuspDt.Rows(0).Item("Status") = "Y" Then
                    FromNm = "SUSDEBMAS"
                    SuspSchCode = M_SuspDt.Rows(0).Item("SuspSchCode")
                End If


                'If (FromNm = "") Then
                '    Dim M_sql As String
                '    M_sql = "select Tran_No from Dep_Master_M_Tbl where Acc_No='" & Tran_No & "'"
                '    Tran_No = M_DBConn.ExecuteScalar(M_sql, 0, False)
                '    FromNm = M_CommonBL.Get_TranFrom(Tran_No)
                'End If

                Select Case FromNm
                    Case "GOLD"
                        Response.Redirect("~/GoldOpen.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "DEPMAS"
                        Response.Redirect("~/DepositMaster.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "SUSDEBMAS"
                        Response.Redirect("~/DepositMaster.aspx?TRANID=" & Tran_No & "&SCHEME=" & SuspSchCode)
                        Exit Sub
                    Case "SHAREMAS"
                        Response.Redirect("~/ShareMaster.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "MLOAN"
                        Response.Redirect("~/LoanMaster.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "SUSPMAS", "TOURMAS"
                        Response.Redirect("~/SuspenseMaster.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "DLOAN"
                        Response.Redirect("~/DepositLoan.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "TRAN"
                        Response.Redirect("~/Transaction.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "LA"
                        Response.Redirect("~/LoanApplicationForm.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "LF"
                        Response.Redirect("~/LoanApplication.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "LS"
                        Response.Redirect("~/LoanApplication.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "GDSAUCT"
                        Response.Redirect("~/ChittyAuction.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "INVMAS"
                        Response.Redirect("~/InvestmentMaster.aspx?TRANID=" & Tran_No)
                    Case "LOCKER"
                        Response.Redirect("~/LockerMaster.aspx?TRANID=" & Tran_No)
                    Case "BLR"
                        Response.Redirect("~/BILLSREALISATION.aspx?TRANID=" & Tran_No)
                        Exit Sub

                    Case "BULKENTRY"
                        Response.Redirect("~/BulkTransactionEntry.aspx?TRANID=" & Tran_No)
                        Exit Sub

                    Case "BANKTR"
                        Response.Redirect("~/CashTr.aspx?TRANID=" & Tran_No)
                        Exit Sub
                    Case "SHRTR"
                        Response.Redirect("~/ShareTransfer.aspx?TRANID=" & Tran_No)
                        Exit Sub
                End Select

                'Response.Redirect("~/Transaction.aspx?TRANID=" & DgvPassing.DataKeys(RowIndex)("Tran_No").ToString())
                'Exit Sub


                'Response.Redirect("~/GoldOpen.aspx?TRANID=" & DgvPassing.DataKeys(RowIndex)("Tran_No").ToString())
                'Exit Sub

                'If DgvPassing.DataKeys(RowIndex)("Authorised").ToString() = False Then


                SqlStr = "Exec Transaction_Authrise_Sp '" & Tran_No & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
                    ",'" & Session("UserID") & "','" & Session("DefaultBrCode").ToString() & "'"
                M_DBConn.ExecuteScalar(SqlStr, , False)

                'Dim M_CommonBL As New CommonBL
                'If M_CommonBL.Authorise_Tran(Tran_No, Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                '    ShowAlertMessage("Authotisation Failed")
                'End If
                'M_CommonBL = Nothing

                Call Refresh()
                'End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub    ''Anil for Debanture Application

    Public Enum PassingType
        Pending = 0
        Passed
        All
        Application
    End Enum

    'Public Sub FillPassingData(ByRef Dgv As Object, Optional ByVal FillType As PassingType = PassingType.Pending)
    '    Try
    '        Dim SqlString As String = ""
    '        Dim Dt As DataTable = New DataTable()
    '        Dim M_DBConn As New Connection()

    '        SqlString = "Select Cast(Max(Case When Authorised='Y' Then 1 Else 0 End) As Bit) As Authorised,Max(T.Vr_No) As VouNo,CASE WHEN max(T.Sch_Code) ='VOU' THEN Isnull(Max(A.Acc_Name),'')+' - ' + max(Narration+' '+Narration1) WHEN MAX(T.Sch_Code) =''  THEN IsNull(Max(Narration+' '+Narration1),'') ELSE Isnull(Max(S.Sch_Name),'')+'-'+Isnull(Max(M.ACC_No),'') END As Particulars,"
    '        SqlString = SqlString & " RecCash = Sum((CASE WHEN TRAN_TYPE = 'C' THEN CASH ELSE 0 END)),"
    '        SqlString = SqlString & " RecTransfer = Sum((CASE WHEN TRAN_TYPE = 'C' THEN TRANSFER ELSE 0 END)),"
    '        SqlString = SqlString & " PayCash = Sum((CASE WHEN TRAN_TYPE = 'D' THEN CASH ELSE 0 END)),"
    '        SqlString = SqlString & " PayTransfer = Sum((CASE WHEN TRAN_TYPE = 'D' THEN TRANSFER ELSE 0 END)),max(T.MakerID) as Maker ,Max(Isnull(CU.Emp_Name,'')) As CheckerID,"
    '        If OptPassed.Checked = True Then
    '            SqlString = SqlString + " Max(Isnull(MU.Emp_Name,'')) As MakerID"
    '        Else
    '            SqlString = SqlString + " Max(Isnull(MU.Emp_Name,'')) As MakerID"
    '        End If
    '        SqlString = SqlString + ",Max(T.Making_Time) As MakingTime, "
    '        SqlString = SqlString & " T.Tran_No"
    '        SqlString = SqlString & " From Transaction_T_Tbl T"
    '        SqlString = SqlString & " Left Outer Join AccMain_M_Tbl M On M.ACC_ID=T.Acc_ID"
    '        SqlString = SqlString & " Left Outer Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.ACC_SchCode"
    '        SqlString = SqlString & " Left Outer Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=T.Gl_Code"
    '        SqlString = SqlString & " Left Outer Join User_Details_Vw MU On MU.User_Name=T.MakerID"
    '        SqlString = SqlString & " Left Outer Join User_Details_Vw CU On CU.User_Name=T.CheckerID"
    '        SqlString = SqlString & " Where T.Cash+T.Transfer>0 "
    '        SqlString = SqlString + " And Tr_date = " + ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
    '        "  And Org_Branch = '" & Session("DefaultBrCode").ToString() & "'"

    '        If OptPassed.Checked = True Then
    '            SqlString = SqlString + " And Authorised = 'Y' "
    '        ElseIf OptPending.Checked = True Then
    '            SqlString = SqlString + " And Authorised = 'N' "
    '        End If

    '        If ChkScheme.Checked = True Then
    '            SqlString = SqlString + " and T.Sch_Code like '" & TxtScheme.Text & "' "
    '        End If

    '        If ChkAccNo.Checked = True Then
    '            SqlString = SqlString + " and  T.Acc_No  like '%" & TxtAccno00.Text & "%' "
    '        End If


    '        If ChkAmount.Checked = True Then
    '            SqlString = SqlString + " and( T.Cash = " & Val(TxtAmt.Text) & "  or  T.Transfer =" & Val(TxtAmt.Text) & ") "
    '        End If

    '        If ChkMakerId.Checked = True Then
    '            SqlString = SqlString + " and  MU.Emp_Name  like '%" & TxtMakerId.Text & "%' "
    '        End If

    '        If ChkChekerId.Checked = True Then
    '            SqlString = SqlString + " and  CU.Emp_Name like '%" & TxtCheckerId.Text & "%' "
    '        End If

    '        If ChkCreditDebit.Checked = True Then
    '            SqlString = SqlString + " and T.TRAN_TYPE ='" & (Mid(CmbCreditdebit.SelectedItem.Text, 1, 1)) & "' "
    '        End If

    '        If ChkBatch.Checked = True Then
    '            SqlString = SqlString + " and T.Batch_No  like '%" & TxtBatch.Text & "%' "
    '        End If


    '        SqlString = SqlString & " Group By T.Tran_No"

    '        SqlString = SqlString & " union all "
    '        SqlString = SqlString & "Select Cast(Case When acc_Authorised='Y' Then 1 Else 0 End As Bit)   as Authorised,0 as VouNo,"
    '        SqlString = SqlString & "ACC_No + ' ' + C.Cust_Name as Particulars,0 as RecCash,0 as RecTransfer,0 as PayCash,0 as PayTransfer,"
    '        SqlString = SqlString & "ACC_EntryUser as Maker,"
    '        SqlString = SqlString & "ACC_AuthorisedUser  as CheckerID ,ACC_EntryUser as MakerID ,ACC_EntryTime as MakingTime,"
    '        SqlString = SqlString & " 'O-' +RIGHT('     '+Case when Sch_Section_Name = 'DEPOSIT' then 'DEPMAS' when Sch_Section_Name = 'LOAN' then 'MLOAN' when Sch_Section_Name = 'SUSPENSE' then 'SUSPMAS'  when Sch_Section_Name = 'TOURS & TRAVELS' then 'TOURMAS' when Sch_Section_Name = 'LOCKER' then 'LOCKER' Else'' End,7)+ ACC_No as Tran_No "
    '        SqlString = SqlString & "from AccMain_M_Tbl  A join Cust_Master_M_Tbl C on A.ACC_CustID = C.Cust_ID  join Gen_SchemeMaster_P_Tbl S on A.ACC_SchCode = S.Sch_Code "
    '        SqlString = SqlString & "Where ACC_BrCode ='" & Session("DefaultBrCode").ToString() & "' and ACC_Authorised ='N'"
    '        SqlString = SqlString & "and ACC_OpenDate =" & ClsCom.DbDate(CDate(Session("TransDate").ToString())) & "   and ACC_OpenAmt = 0"

    '        SqlString = SqlString & " Order By Max(T.Making_Time)"

    '        'If Cashier_Module = True Then
    '        '    ' For Avoiding Non Cashier Passed Credit Cash Entries
    '        '    SqlString = SqlString + " And Len(Isnull(Cashier_ID,'')+Case When Cash>0 And Tran_Type='C' Then '' Else MakerId End)>0 "
    '        'End If

    '        M_DBConn.ExecuteDataTable(Dt, SqlString, False)

    '        Dgv.DataSource = Dt
    '        Dgv.DataBind()

    '        If Dt.Rows.Count = 0 Then
    '            PnlStatus.Visible = True
    '        Else
    '            PnlStatus.Visible = False
    '        End If

    '        SqlString = "Exec NonTranction_AuthoriseFill_Sp '" & Session("DefaultBrCode").ToString() & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString()))
    '        If OptPassed.Checked = True Then
    '            SqlString = SqlString + ",'Y' "
    '        ElseIf OptPending.Checked = True Then
    '            SqlString = SqlString + ",'N' "
    '        End If
    '        M_DBConn.ExecuteDataTable(Dt, SqlString, False)
    '        DgvNonTranPassing.DataSource = Dt
    '        DgvNonTranPassing.DataBind()

    '        If PnlStatus.Visible = True Then
    '            If Dt.Rows.Count = 0 Then
    '                PnlStatus.Visible = True
    '            Else
    '                PnlStatus.Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowAlertMessage(ex.Message)
    '        LoggError(ex)
    '    End Try
    'End Sub
    Public Sub FillPassingData(ByRef Dgv As Object, Optional ByVal FillType As PassingType = PassingType.Pending)
        '' Anil for debanture application
        Try
            Dim SqlString As String = ""
            Dim Dt As DataTable = New DataTable()
            Dim M_DBConn As New Connection()

            SqlString = "Select Cast(Max(Case When Authorised='Y' Then 1 Else 0 End) As Bit) As Authorised,Max(T.Vr_No) As VouNo,CASE WHEN max(T.Sch_Code) ='VOU' THEN Isnull(Max(A.Acc_Name),'')+' - ' + max(Narration+' '+Narration1) WHEN MAX(T.Sch_Code) =''  THEN IsNull(Max(Narration+' '+Narration1),'') ELSE Isnull(Max(S.Sch_Name),'')+'-'+Isnull(Max(M.ACC_No),'') END As Particulars,"
            SqlString = SqlString & " RecCash = Sum((CASE WHEN TRAN_TYPE = 'C' THEN CASH ELSE 0 END)),"
            SqlString = SqlString & " RecTransfer = Sum((CASE WHEN TRAN_TYPE = 'C' THEN TRANSFER ELSE 0 END)),"
            SqlString = SqlString & " PayCash = Sum((CASE WHEN TRAN_TYPE = 'D' THEN CASH ELSE 0 END)),"
            SqlString = SqlString & " PayTransfer = Sum((CASE WHEN TRAN_TYPE = 'D' THEN TRANSFER ELSE 0 END)),max(T.MakerID) as Maker ,Max(Isnull(CU.Emp_Name,'')) As CheckerID,"
            If OptPassed.Checked = True Then
                SqlString = SqlString + " Max(Isnull(MU.Emp_Name,'')) As MakerID"
            Else
                SqlString = SqlString + " Max(Isnull(MU.Emp_Name,'')) As MakerID"
            End If
            SqlString = SqlString + ",Max(T.Making_Time) As MakingTime, "
            SqlString = SqlString & " T.Tran_No"
            SqlString = SqlString & " From Transaction_T_Tbl T"
            SqlString = SqlString & " Left Outer Join AccMain_M_Tbl M On M.ACC_ID=T.Acc_ID"
            SqlString = SqlString & " Left Outer Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.ACC_SchCode"
            SqlString = SqlString & " Left Outer Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=T.Gl_Code"
            SqlString = SqlString & " Left Outer Join User_Details_Vw MU On MU.User_Name=T.MakerID"
            SqlString = SqlString & " Left Outer Join User_Details_Vw CU On CU.User_Name=T.CheckerID"
            SqlString = SqlString & " Where T.Cash+T.Transfer>0 "
            SqlString = SqlString + " And Tr_date = " + ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
            "  And Org_Branch = '" & Session("DefaultBrCode").ToString() & "'"

            If OptPassed.Checked = True Then
                SqlString = SqlString + " And Authorised = 'Y' "
            ElseIf OptPending.Checked = True Then
                SqlString = SqlString + " And Authorised = 'N' "
            End If

            If ChkScheme.Checked = True Then
                SqlString = SqlString + " and T.Sch_Code like '" & TxtScheme.Text & "' "
            End If

            If ChkAccNo.Checked = True Then
                SqlString = SqlString + " and  T.Acc_No  like '%" & TxtAccno00.Text & "%' "
            End If


            If ChkAmount.Checked = True Then
                SqlString = SqlString + " and( T.Cash = " & Val(TxtAmt.Text) & "  or  T.Transfer =" & Val(TxtAmt.Text) & ") "
            End If

            If ChkMakerId.Checked = True Then
                SqlString = SqlString + " and  MU.Emp_Name  like '%" & TxtMakerId.Text & "%' "
            End If

            If ChkChekerId.Checked = True Then
                SqlString = SqlString + " and  CU.Emp_Name like '%" & TxtCheckerId.Text & "%' "
            End If

            If ChkCreditDebit.Checked = True Then
                SqlString = SqlString + " and T.TRAN_TYPE ='" & (Mid(CmbCreditdebit.SelectedItem.Text, 1, 1)) & "' "
            End If

            If ChkBatch.Checked = True Then
                SqlString = SqlString + " and T.Batch_No  like '%" & TxtBatch.Text & "%' "
            End If


            SqlString = SqlString & " Group By T.Tran_No"

        
            SqlString = SqlString & " union all "
            SqlString = SqlString & "Select Cast(Case When acc_Authorised='Y' Then 1 Else 0 End As Bit)   as Authorised,0 as VouNo,"
            SqlString = SqlString & "ACC_No + ' ' + C.Cust_Name as Particulars,0 as RecCash,0 as RecTransfer,0 as PayCash,0 as PayTransfer,"
            SqlString = SqlString & "ACC_EntryUser as Maker,"
            SqlString = SqlString & "ACC_AuthorisedUser  as CheckerID ,ACC_EntryUser as MakerID ,ACC_EntryTime as MakingTime,"
            SqlString = SqlString & " 'O-' +RIGHT('     '+Case when Sch_Section_Name = 'DEPOSIT' then 'DEPMAS' when Sch_Section_Name = 'LOAN' then 'MLOAN' when Sch_Section_Name = 'SUSPENSE' then 'SUSPMAS'  when Sch_Section_Name = 'TOURS & TRAVELS' then 'TOURMAS' when Sch_Section_Name = 'LOCKER' then 'LOCKER' Else'' End,7)+ ACC_No as Tran_No "
            SqlString = SqlString & "from AccMain_M_Tbl  A join Cust_Master_M_Tbl C on A.ACC_CustID = C.Cust_ID  join Gen_SchemeMaster_P_Tbl S on A.ACC_SchCode = S.Sch_Code "
            SqlString = SqlString & "Where ACC_BrCode ='" & Session("DefaultBrCode").ToString() & "' and ACC_Authorised ='N'"
            SqlString = SqlString & "and ACC_OpenDate =" & ClsCom.DbDate(CDate(Session("TransDate").ToString())) & "   and ACC_OpenAmt = 0"



            Dim M_Dt As New DataTable
            Dim M_CommonBl As New CommonBL
            'Dim Hierarchy As Integer

            'M_Dt = M_CommonBl.Get_AuthorisHierarchy_Level(Session("UserID"), "ALL")
            'Hierarchy = M_Dt.Rows(0).Item("Hierarchy_Lvl")
            SqlString = SqlString & " Order By Max(T.Making_Time)"
            
            If RadioApp.Checked = True Then
                SqlString = " "
                SqlString = SqlString & "   Select Cast(Max(Case When T.Authorised='Y' Then 1 Else 0 End) As Bit) As Authorised,Max(T.Vr_No) As VouNo,"
                SqlString = SqlString & " CASE WHEN max(T.Sch_Code) ='VOU' THEN Isnull(Max(A.Acc_Name),'')+' - ' + max(Narration+' '+Narration1)"
                SqlString = SqlString & " WHEN MAX(T.Sch_Code) =''  THEN IsNull(Max(Narration+' '+Narration1),'') ELSE Isnull(Max(S.Sch_Name),'')+'-'+Isnull(Max(M.ACC_No),'')"
                SqlString = SqlString & " END As Particulars, RecCash = Sum((CASE WHEN TRAN_TYPE = 'C' THEN CASH ELSE 0 END)), RecTransfer = Sum((CASE WHEN TRAN_TYPE = 'C'"
                SqlString = SqlString & " THEN TRANSFER ELSE 0 END)), PayCash = Sum((CASE WHEN TRAN_TYPE = 'D' THEN CASH ELSE 0 END)), PayTransfer ="
                SqlString = SqlString & " Sum((CASE WHEN TRAN_TYPE = 'D' THEN TRANSFER ELSE 0 END)),max(T.MakerID) as Maker ,Max(Isnull(CU.Emp_Name,'')) As CheckerID, "
                SqlString = SqlString & " Max(Isnull(MU.Emp_Name,'')) As MakerID,Max(T.Making_Time) As MakingTime,  T.Tran_No From Transaction_T_Tbl T "
                SqlString = SqlString & " Left Outer Join AccMain_M_Tbl M On M.ACC_ID=T.Acc_ID Left Outer Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.ACC_SchCode "
                SqlString = SqlString & " Left Outer Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=T.Gl_Code Left Outer Join User_Details_Vw MU On MU.User_Name=T.MakerID "
                SqlString = SqlString & " Left Outer Join User_Details_Vw CU On CU.User_Name=T.CheckerID"
                SqlString = SqlString & " RIGHT OUTER join Susp_ShareAuth_Tbl SS ON SS.Tran_No=T.Tran_No "
                SqlString = SqlString & "  left outer join Authorisation_Hierarchy_P_Tbl AH ON AH.Acc_Code=T.Acc_Code   left outer join User_Details_M_Tbl UD ON UD.Auth_Hierach=AH.User_Code"
                SqlString = SqlString & " Where(T.Cash + T.Transfer > 0) /*and   len(SS.Checker1+SS.Checker2+SS.Checker3)>0*/ and SS.Authorised<>'Y' AND UD.User_Name='" & Session("UserID") & "'  "
                SqlString = SqlString & " And Org_Branch = '" & Session("DefaultBrCode").ToString() & "'  and SS.Hierarchy_Level<> '0'   Group By T.Acc_No,T.Tran_No "
                SqlString = SqlString & " Order By cast(T.Acc_No as bigint),Max(T.Making_Time)"
            End If




            'If Cashier_Module = True Then
            '    ' For Avoiding Non Cashier Passed Credit Cash Entries
            '    SqlString = SqlString + " And Len(Isnull(Cashier_ID,'')+Case When Cash>0 And Tran_Type='C' Then '' Else MakerId End)>0 "
            'End If

            M_DBConn.ExecuteDataTable(Dt, SqlString, False)

            Dgv.DataSource = Dt
            Dgv.DataBind()

            If Dt.Rows.Count = 0 Then
                PnlStatus.Visible = True
            Else
                PnlStatus.Visible = False
            End If

            SqlString = "Exec NonTranction_AuthoriseFill_Sp '" & Session("DefaultBrCode").ToString() & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString()))
            If OptPassed.Checked = True Or RadioApp.Checked = True Then
                SqlString = SqlString + ",'Y' "
            ElseIf OptPending.Checked = True Then
                SqlString = SqlString + ",'N' "
            End If
            M_DBConn.ExecuteDataTable(Dt, SqlString, False)
            DgvNonTranPassing.DataSource = Dt
            DgvNonTranPassing.DataBind()

            If PnlStatus.Visible = True Then
                If Dt.Rows.Count = 0 Then
                    PnlStatus.Visible = True
                Else
                    PnlStatus.Visible = False
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
        '' Anil for debanture application
    End Sub


    

    Protected Sub DgvPassing_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvPassing.SelectedIndexChanged

    End Sub

    Protected Sub DgvPassing_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvPassing.PageIndexChanging
        Try
            DgvPassing.PageIndex = e.NewPageIndex
            BtnRefresh_Click(Nothing, EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub BtnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnRefresh.Click
        Call Refresh()
    End Sub

    Public Sub Refresh()
        If OptAll.Checked = True Then
            Call FillPassingData(DgvPassing, PassingType.All)
        ElseIf OptPassed.Checked = True Then
            Call FillPassingData(DgvPassing, PassingType.Passed)
        ElseIf OptPending.Checked = True Then
            Call FillPassingData(DgvPassing, PassingType.Pending)
        ElseIf RadioApp.Checked = True Then
            Call FillPassingData(DgvPassing, PassingType.Application)
        End If
       
    End Sub



    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub DgvNonTranPassing_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvNonTranPassing.RowCommand
        Try
            Dim Tran_No As String
            Dim Auth_Type As String
            Dim M_DBConn As New Connection()
            Dim SqlStr As String

            If e.CommandName.ToUpper = "SELECT" Then
                Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())

                'If DgvPassing.DataKeys(RowIndex)("Authorised").ToString() = False Then
                Tran_No = DgvNonTranPassing.DataKeys(RowIndex)("Tran_No").ToString()
                Auth_Type = DgvNonTranPassing.DataKeys(RowIndex)("Auth_Type").ToString()
                If Mid(Tran_No, 1, 6) = "BILLRE" Then
                    Response.Redirect("~/Bills.aspx?TRANID=" & Tran_No)

                    Exit Sub
                End If
                SqlStr = "Exec NonTranction_Authorise_Sp '" & Session("DefaultBrCode").ToString() & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString()))
                SqlStr = SqlStr & ",'" & Auth_Type & "','" & Tran_No & "','" & Session("UserID") & "'"


                M_DBConn.ExecuteScalar(SqlStr, , False)
                Call Refresh()
                'End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try

    End Sub

    Protected Sub Close_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Close.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub DgvNonTranPassing_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvNonTranPassing.PageIndexChanging
        Try
            DgvNonTranPassing.PageIndex = e.NewPageIndex
            BtnRefresh_Click(Nothing, EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    
End Class