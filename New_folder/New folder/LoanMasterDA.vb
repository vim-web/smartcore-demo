
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Public Class LoanMasterDA
    Private M_DBConn As Connection
    Private M_AccID As Integer
    Public Function Save_Data(ByRef M_LoanMaster As LoanMasterBO, ByVal M_AccHolderSw As String, ByVal M_OrnSw As String, ByVal M_TranSw As String, ByVal M_EntryMode As String, ByVal M_SecDepSw As String, ByVal M_LoanAppNo As String, ByVal Batch_No As String, ByVal Tr_AccID As String) As DataTable
        Dim M_Dt As DataTable

        Try
            Dim M_Sql As String

            With M_LoanMaster
                M_Sql = "Exec InsertUpdate_Loan_Master_M_Tbl "
                M_Sql = M_Sql & "'" & M_EntryMode & "',"
                M_Sql = M_Sql & .AccID & ","
                M_Sql = M_Sql & "'" & .AccNo & "',"
                M_Sql = M_Sql & "'" & .SchCode & "',"
                M_Sql = M_Sql & "'" & .BrCode & "',"
                M_Sql = M_Sql & "'" & .LmApplSlno & "',"
                M_Sql = M_Sql & "'" & .AccCode & "',"
                M_Sql = M_Sql & .CustId & ","
                M_Sql = M_Sql & "'" & .LmShrBrCode & "',"
                M_Sql = M_Sql & "'" & .LmShrSchCode & "',"
                M_Sql = M_Sql & "'" & .LmMemNo & "',"
                M_Sql = M_Sql & .LmLoanAmount & ","
                M_Sql = M_Sql & .LmBalance & ","
                M_Sql = M_Sql & "'" & .LmBalType & "',"
                M_Sql = M_Sql & "'" & Format(.LmLoanDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & .LmPeriod & ","
                M_Sql = M_Sql & "'" & .LmPeriodType & "',"
                M_Sql = M_Sql & .LmHolidayPeriod & ","
                M_Sql = M_Sql & "'" & .LmHolidayPeriodType & "',"
                M_Sql = M_Sql & "'" & Format(.LmDueDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & Format(.LmLastIntDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & .LmMarketRate & ","
                M_Sql = M_Sql & .LmBankRate & ","
                M_Sql = M_Sql & .LmIntRate & ","
                M_Sql = M_Sql & .LmPenalRate & ","
                M_Sql = M_Sql & .LmInstallmentNo & ","
                M_Sql = M_Sql & .LmInstallmentAmount & ","
                M_Sql = M_Sql & .LmApprValue & ","
                M_Sql = M_Sql & .LmApprID & ","
                M_Sql = M_Sql & .LmMainpurpose & ","
                M_Sql = M_Sql & .LmSubpurpose & ","
                M_Sql = M_Sql & .LmPriority & ","
                M_Sql = M_Sql & "'" & .LmResNo & "',"
                M_Sql = M_Sql & "'" & Format(.LmResDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & .LmSanctionBy & ","
                M_Sql = M_Sql & "'" & .LmSanctionNo & "',"
                M_Sql = M_Sql & "'" & Format(.LmLastUpdate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & .LmNomNos & ","
                M_Sql = M_Sql & "'" & .LmSecurityDetails & "',"
                M_Sql = M_Sql & "'" & .LmOperationMode & "',"
                M_Sql = M_Sql & "'" & .LmRemarks & "',"
                M_Sql = M_Sql & "'" & .LmInsurance & "',"
                M_Sql = M_Sql & "'" & .LmRegType & "',"
                M_Sql = M_Sql & "'" & .LmRegNo & "',"
                M_Sql = M_Sql & "'" & .LmLbrCode & "',"
                M_Sql = M_Sql & "'" & .LmLbrActCode & "',"
                M_Sql = M_Sql & .LmCumType & ","
                M_Sql = M_Sql & "'" & .LmIntroducedby & "',"
                M_Sql = M_Sql & .LmOpBal & ","
                M_Sql = M_Sql & "'" & .LmOpBalType & "',"
                M_Sql = M_Sql & .LmOpPayAmt & ","
                M_Sql = M_Sql & "'" & Format(.LmOpIntdate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & .LmOpIntrate & ","
                M_Sql = M_Sql & "'" & .LmStatus & "',"
                M_Sql = M_Sql & "'" & .LmEnhancement & "',"
                M_Sql = M_Sql & "'" & .LmLegalStatus & "',"
                M_Sql = M_Sql & "'" & Format(.LmLegalDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & .LmNpaStatus & "',"
                M_Sql = M_Sql & "'" & Format(.LmNpaDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & Format(.LmFirstNpaDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & .LmFirstNpaStatus & "',"
                M_Sql = M_Sql & "'" & Format(.LmClosedDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & .Authorised & "',"
                M_Sql = M_Sql & .LogID & ","
                M_Sql = M_Sql & "'" & .TranNo & "',"
                M_Sql = M_Sql & "'" & .MakerID & "',"
                M_Sql = M_Sql & "'" & .CheckerID & "',"
                M_Sql = M_Sql & "'" & Format(.MakingTime, "MM/dd/yyyy HH:mm:ss") & "',"
                M_Sql = M_Sql & "'" & Format(.CheckingTime, "MM/dd/yyyy HH:mm:ss") & "',"
                M_Sql = M_Sql & "'" & .LmOrnamentsReleased & "',"
                M_Sql = M_Sql & "'" & Format(.LmOrnamentsReleasedDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & .CashierID & "',"
                M_Sql = M_Sql & "'" & Format(.Cashiertime, "MM/dd/yyyy HH:mm:ss") & "',"
                M_Sql = M_Sql & "'" & .BankAccNo & "',"
                M_Sql = M_Sql & .LmAgentCode & ","
                M_Sql = M_Sql & "'" & .LmStandingForSecDeposit & "',"
                M_Sql = M_Sql & .LmJewName & ","
                M_Sql = M_Sql & "'" & .LmJewBillno & "',"
                M_Sql = M_Sql & "'" & Format(.LmjewBillDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & .LmBillAmt & ","
                M_Sql = M_Sql & .LmNetWt & ","
                M_Sql = M_Sql & .LmGrossWt & ","
                M_Sql = M_Sql & "'" & M_AccHolderSw & "',"
                M_Sql = M_Sql & "'" & M_OrnSw & "',"
                M_Sql = M_Sql & "'" & M_TranSw & "',"
                M_Sql = M_Sql & "'" & M_SecDepSw & "',"
                M_Sql = M_Sql & "'" & M_LoanAppNo & "',"
                M_Sql = M_Sql & "'" & .LMWardCode & "',"
                M_Sql = M_Sql & .LMReligion & ","
                M_Sql = M_Sql & .LMCaste & ","
                M_Sql = M_Sql & .LMGender & ","
                M_Sql = M_Sql & "'" & .LMRegOffice & "',"
                M_Sql = M_Sql & "'" & .LMExtend & "',"
                M_Sql = M_Sql & "'" & .LMSurveyNo & "',"
                M_Sql = M_Sql & "'" & .LMDocumentNo & "',"
                M_Sql = M_Sql & "'" & .LMResurveyNo & "',"
                M_Sql = M_Sql & "'" & .LM_Cent & "',"
                M_Sql = M_Sql & "@Batch_No='" & Batch_No & "',"
                M_Sql = M_Sql & "@Director='" & .LMDirector & "',"
                M_Sql = M_Sql & "@TransferAccID='" & Tr_AccID & "',"
                M_Sql = M_Sql & "@LM_SroNo='" & .LM_SroNo & "'"

            End With

            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Return M_Dt

        Catch ex As Exception
            '  MsgBox(ex.Message)
            Return M_Dt
        End Try

    End Function


    Public Function SecurityDeposit_Details_Fn(ByVal AsonDate As Date, ByVal SecDep_XML As String) As DataTable
        Try
            Dim M_Dt As DataTable
            Dim M_Sql As String

            M_Sql = "Exec Loan_SecurityDeposit_Details_Sp " & CommonClass.DbDate(AsonDate) & ",'" & SecDep_XML & "'"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Return M_Dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Locate_Data(ByVal M_AccID As String) As LoanMasterBO
        Try
            Dim M_Dt As DataTable
            Dim M_LoanMaster As New LoanMasterBO
            Dim Qry As String
            Qry = "Select * from Loan_Master_M_Tbl where Acc_ID = " & M_AccID
            M_DBConn.ExecuteDataTable(M_Dt, Qry, False)
            If M_Dt.Rows.Count > 0 Then
                With M_LoanMaster


                    .AccID = M_Dt.Rows(0).Item("Acc_ID")
                    .CustId = M_Dt.Rows(0).Item("Cust_Id")
                    .LmHolidayPeriod = M_Dt.Rows(0).Item("Lm_HolidayPeriod")
                    .LmInstallmentNo = M_Dt.Rows(0).Item("Lm_InstallmentNo")
                    .LmApprID = M_Dt.Rows(0).Item("Lm_ApprID")
                    .LmMainpurpose = M_Dt.Rows(0).Item("Lm_Mainpurpose")
                    .LmAgentCode = M_Dt.Rows(0).Item("Lm_Agent_Code")
                    .LmJewName = M_Dt.Rows(0).Item("Lm_Jew_Name")
                    .LmSubpurpose = M_Dt.Rows(0).Item("Lm_Subpurpose")
                    .LmPriority = M_Dt.Rows(0).Item("Lm_Priority")
                    .LmSanctionBy = M_Dt.Rows(0).Item("Lm_SanctionBy")
                    .LmNomNos = M_Dt.Rows(0).Item("Lm_NomNos")
                    .LmCumType = M_Dt.Rows(0).Item("Lm_CumType")
                    .LogID = M_Dt.Rows(0).Item("Log_ID")
                    .LmLoanDate = M_Dt.Rows(0).Item("Lm_LoanDate")
                    .LmDueDate = M_Dt.Rows(0).Item("Lm_DueDate")
                    .LmLastIntDate = M_Dt.Rows(0).Item("Lm_LastIntDate")
                    .LmResDate = M_Dt.Rows(0).Item("Lm_ResDate")
                    .LmLastUpdate = M_Dt.Rows(0).Item("Lm_LastUpdate")
                    .LmOpIntdate = M_Dt.Rows(0).Item("Lm_OpIntdate")
                    .LmOrnamentsReleasedDate = M_Dt.Rows(0).Item("Lm_OrnamentsReleasedDate")
                    .Cashiertime = M_Dt.Rows(0).Item("Cashier_time")
                    .LmjewBillDate = M_Dt.Rows(0).Item("Lm_jew_BillDate")
                    .LmLegalDate = M_Dt.Rows(0).Item("Lm_LegalDate")
                    .LmNpaDate = M_Dt.Rows(0).Item("Lm_NpaDate")
                    .LmFirstNpaDate = M_Dt.Rows(0).Item("Lm_FirstNpaDate")
                    .LmClosedDate = M_Dt.Rows(0).Item("Lm_ClosedDate")
                    .MakingTime = M_Dt.Rows(0).Item("Making_Time")
                    .CheckingTime = M_Dt.Rows(0).Item("Checking_Time")
                    .LmLoanAmount = M_Dt.Rows(0).Item("Lm_LoanAmount")
                    .LmBalance = M_Dt.Rows(0).Item("Lm_Balance")
                    .LmPeriod = M_Dt.Rows(0).Item("Lm_Period")
                    .LmMarketRate = M_Dt.Rows(0).Item("Lm_MarketRate")
                    .LmBankRate = M_Dt.Rows(0).Item("Lm_BankRate")
                    .LmIntRate = M_Dt.Rows(0).Item("Lm_IntRate")
                    .LmBillAmt = M_Dt.Rows(0).Item("Lm_Bill_Amt")
                    .LmPenalRate = M_Dt.Rows(0).Item("Lm_PenalRate")
                    .LmInstallmentAmount = M_Dt.Rows(0).Item("Lm_InstallmentAmount")
                    .LmApprValue = M_Dt.Rows(0).Item("Lm_ApprValue")
                    .LmOpBal = M_Dt.Rows(0).Item("Lm_OpBal")
                    .LmOpPayAmt = M_Dt.Rows(0).Item("Lm_OpPayAmt")
                    .LmOpIntrate = M_Dt.Rows(0).Item("Lm_OpIntrate")
                    .AccNo = M_Dt.Rows(0).Item("Acc_No")
                    .SchCode = M_Dt.Rows(0).Item("Sch_Code")
                    .BrCode = M_Dt.Rows(0).Item("Br_Code")
                    .LmApplSlno = M_Dt.Rows(0).Item("Lm_Appl_Slno")
                    .AccCode = M_Dt.Rows(0).Item("Acc_Code")
                    .LmShrBrCode = M_Dt.Rows(0).Item("Lm_Shr_Br_Code")
                    .CheckerID = M_Dt.Rows(0).Item("CheckerID")
                    .CashierID = M_Dt.Rows(0).Item("Cashier_ID")
                    .BankAccNo = M_Dt.Rows(0).Item("Bank_Acc_No")
                    .LmJewBillno = M_Dt.Rows(0).Item("Lm_Jew_Billno")
                    .LmRegNo = M_Dt.Rows(0).Item("Lm_RegNo")
                    .LmLbrCode = M_Dt.Rows(0).Item("Lm_LbrCode")
                    .LmLbrActCode = M_Dt.Rows(0).Item("Lm_LbrActCode")
                    .LmIntroducedby = M_Dt.Rows(0).Item("Lm_Introducedby")
                    .TranNo = M_Dt.Rows(0).Item("Tran_No")
                    .MakerID = M_Dt.Rows(0).Item("MakerID")
                    .LmShrSchCode = M_Dt.Rows(0).Item("Lm_Shr_Sch_Code")
                    .LmMemNo = M_Dt.Rows(0).Item("Lm_MemNo")
                    .LmResNo = M_Dt.Rows(0).Item("Lm_ResNo")
                    .LmSanctionNo = M_Dt.Rows(0).Item("Lm_SanctionNo")
                    .LmSecurityDetails = M_Dt.Rows(0).Item("Lm_SecurityDetails")
                    .LmRemarks = M_Dt.Rows(0).Item("Lm_Remarks")
                    .LmBalType = M_Dt.Rows(0).Item("Lm_BalType")
                    .LmPeriodType = M_Dt.Rows(0).Item("Lm_PeriodType")
                    .LmHolidayPeriodType = M_Dt.Rows(0).Item("Lm_HolidayPeriodType")
                    .LmOperationMode = M_Dt.Rows(0).Item("Lm_OperationMode")
                    .LmInsurance = M_Dt.Rows(0).Item("Lm_Insurance")
                    .LmRegType = M_Dt.Rows(0).Item("Lm_RegType")
                    .Authorised = M_Dt.Rows(0).Item("Authorised")
                    .LmOrnamentsReleased = M_Dt.Rows(0).Item("Lm_OrnamentsReleased")
                    .LmStandingForSecDeposit = M_Dt.Rows(0).Item("Lm_StandingFor_Sec_Deposit")
                    .LmOpBalType = M_Dt.Rows(0).Item("Lm_OpBalType")
                    .LmStatus = M_Dt.Rows(0).Item("Lm_Status")
                    .LmEnhancement = M_Dt.Rows(0).Item("Lm_Enhancement")
                    .LmLegalStatus = M_Dt.Rows(0).Item("Lm_LegalStatus")
                    .LmNpaStatus = M_Dt.Rows(0).Item("Lm_NpaStatus")
                    .LmFirstNpaStatus = M_Dt.Rows(0).Item("Lm_FirstNpaStatus")

                End With
            End If
            Return M_LoanMaster
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function Get_ExtraIntr_Rates(ByVal Sch_Code As String, ByVal AsonDate As Date) As DataTable

        Try
            Dim M_Dt As DataTable
            Dim M_Sql As String

            '   Get Eligible percetages..................
            M_Sql = "SELECT DL_IntPercenttype, DL_IntPercentage, DL_EligiblePercent, DL_MaxEligiblePercent,PreClosure_Perctype,PreClosure_Percentage"
            M_Sql = M_Sql & " From Gen_ExIntRateMaster_P_Tbl"
            M_Sql = M_Sql & " WHERE Sch_Code = '" & Sch_Code & "' "
            M_Sql = M_Sql & " AND (Effective_Date = (SELECT MAX(Effective_Date) From Gen_ExIntRateMaster_P_Tbl"
            M_Sql = M_Sql & " WHERE (Sch_Code = '" & Sch_Code & "') AND (Effective_Date <= '" & Format(AsonDate, "MM/dd/yyyy") & "')))"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Return M_Dt
        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Sub New()
        M_DBConn = New Connection
    End Sub

    Public Function Set_LoanAmt(ByVal BrCode As String, ByVal SchCode As String, ByVal AccNo As String, ByVal TrDate As Date, ByVal CashTransfer As String, ByVal RctAmt As Double, ByVal TranType As String, Optional ByVal TranDisplay As String = "N", Optional ByVal AccID As Int32 = 0, Optional ByVal M_RebateAMt As Double = 0) As DataSet
        Try

            Dim M_DataTset As New DataSet
            Dim M_Sql As String
            M_Sql = " Exec Set_LoanAmt_Sp '" & BrCode & "','" & SchCode & "','" & AccNo & "','" & Format(TrDate, "MM/dd/yyyy") & "','" & CashTransfer & "'," & RctAmt & ",'" & TranType & "'" & ",'" & TranDisplay & "'," & AccID.ToString & "," & M_RebateAMt
            M_DBConn.ExecuteDataSet(M_DataTset, M_Sql, False)
            Return M_DataTset

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function Set_OD_Amt(ByVal Acc_ID As Integer, ByVal TrDate As Date, ByVal CashTransfer As String, ByVal RctAmt As Double, ByVal TranMode As String) As DataSet
        Try

            Dim M_Dataset As New DataSet
            Dim M_Sql As String
            M_Sql = " Exec Set_OD_Amt_Sp '" & Acc_ID & "','" & Format(TrDate, "MM/dd/yyyy") & "','" & CashTransfer & "'," & RctAmt & ",'" & TranMode & "'"
            M_DBConn.ExecuteDataSet(M_Dataset, M_Sql, False)
            Return M_Dataset

        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Protected Overrides Sub Finalize()
        Try

            M_DBConn = Nothing
            MyBase.Finalize()

        Catch ex As Exception

        End Try
    End Sub

    Public Function PostLoanInterest(ByVal BrCode As String, ByVal SchCode As String, ByVal FrDate As Date, ByVal ToDate As Date, ByVal TrDate As Date, ByVal AccNo As String, ByVal IsPost As Boolean, ByVal IsSelect As Boolean, ByVal UserID As String, Optional ByRef ProcessQry As String = Nothing) As DataSet
        Try

            Dim M_Dataset As New DataSet
            Dim M_Sql As String

            M_Sql = "Exec PostLoanInterest_Sp '" & BrCode & "','" & SchCode & "','" & Format(FrDate, "MM/dd/yyyy") & "','" & _
                Format(ToDate, "MM/dd/yyyy") & "','" & Format(TrDate, "MM/dd/yyyy") & "','" & AccNo & "','" & IIf(IsPost, "Y", "N") & "','" & _
                IIf(IsSelect, "Y", "N") & "','" & UserID & "'"
            If IsNothing(ProcessQry) = False Then ProcessQry = M_Sql
            If IsPost = True Then M_DBConn.ExecuteDataSet(M_Dataset, M_Sql, False)
            Return M_Dataset

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function Get_ActionLoanTable(ByVal AccID As String) As DataTable

        Try
            Dim M_Dt As DataTable
            Dim M_Sql As String

            '   Get Charge Posted  List for a specified Account
            M_Sql = "SELECT    Convert(Varchar(12),m.Reg_Date ,103) as Reg_Date , CASE WHEN m.Reg_No ='L' tHEN 'LONEE' ELSE 'SUERTIES' END SendTo, m.Amount, a.Acc_Name, p.Pkc_Description, m.Sl_No"
            M_Sql = M_Sql & " FROM         Loan_ActionTaken_M_Tbl AS m INNER JOIN Gen_Accountmaster_P_Tbl AS a ON m.GL_Code = a.Acc_Code INNER JOIN  PickupCode_P_Tbl AS p ON m.Action_Type = p.Pkc_Code"
            M_Sql = M_Sql & " WHERE m.Acc_ID = '" & AccID & "' order by m.Reg_Date "
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Return M_Dt
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function Limitsetting(ByVal Scheme As String, ByVal MemNo As String) As DataTable
        Dim Dt As New DataTable
        Dim M_Sql As String
        M_Sql = " Exec LoanLimit '" & MemNo & "','" & Scheme & "'"
        M_DBConn.ExecuteDataTable(Dt, M_Sql, False)
        Return Dt

    End Function



End Class

