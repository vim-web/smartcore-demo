Imports SMARTCORE_BL
Partial Public Class DepositLoan
    Inherits System.Web.UI.Page

    Public SecDepTbl As New DataTable("Table")
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Shared M_AccHead As String
    Shared ParameterDt As DataTable
    Dim FormID As Integer = 1049
    Shared Obj_UserRights As Object

    Public Sub Load_DefaultValues()

        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
        M_CommonBL.Fill_Pickup(CmbSanctionedBy, 33, 0)

        M_CommonBL.Fill_Pickup(CmbPurpose, 31, 0)
        M_CommonBL.Fill_Pickup(CmbSubPurpose, 32, 0)

        Ddl_Priority.Items.Add("No")
        Ddl_Priority.Items.Add("Yes")
        Ddl_Priority.SelectedIndex = 0

        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN", "DEP.LOAN")

        DgvTransaction.DataSource = Nothing
        Dim Dt As DataTable
        Dt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
        DgvTransaction.DataSource = Dt
        DgvTransaction.DataBind()


        M_CommonBL = Nothing
        PickupDt = DirectCast(Cache("PickupTbl"), DataTable)
    End Sub

    Private Sub Assign_Values()
        Dim LoanIntRate As Double
        Dim M_CommonBL As New CommonBL
        Dim SecTDSchCode As String

        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
        CmbOpMode.SelectedValue = Session("AcOpenOpMode")
        TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))

        Dim AccDt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
        DgvCustomer.DataSource = AccDt
        DgvCustomer.Columns(0).HeaderText = "Security Deposit Details"
        DgvCustomer.DataBind()

        If AccDt.Rows.Count >= 1 Then
            SecTDSchCode = AccDt.Rows(0)("MemSchCode")
        End If

        Dim M_SecSw As New IO.StringWriter
        AccDt.WriteXml(M_SecSw)

        ' Setting Security Deposit Details
        Txt_SecDepAmt.Text = "0"
        Dim SecDepDt As DataTable
        Dim M_LoanMasterBL As New LoanMasterBL
        SecDepDt = M_LoanMasterBL.SecurityDeposit_Details_Fn(Session("TransDate"), M_SecSw.ToString.Replace("DocumentElement", "NewDataSet"))
        DgvSecDeposit.DataSource = SecDepDt
        DgvSecDeposit.DataBind()
        Session("SecDepTbl") = SecDepDt

        Ce_DueDate.Text = Session("TransDate")
        TxtLoanDt.Text = Session("TransDate")

        ' Setting Duedate & Period  
        Dim SortingOrder As String = "DueDate Asc"
        Dim drFiltered As DataRow() = SecDepDt.[Select]("", SortingOrder)
        For Each row As DataRow In drFiltered
            Ce_DueDate.Text = FormatDateTime(row("DueDate").ToString(), DateFormat.ShortDate)
            Exit For
        Next

        ' Validating Duedate
        Dim FilterExpression As String = "DueDate<'" & Format(Session("TransDate"), "#{0:MM/dd/yyyy}#") & "'"
        drFiltered = SecDepDt.[Select](FilterExpression, "")
        If drFiltered.Count > 0 Then
            ShowAlertMessage("INVALID DEPOSIT DUEDATE, DUEDATE SHOULD LESS THAN OR EQUAL TO TRANSACTION DATE")
            Cmd_Save.Enabled = False
            'Response.Redirect("~/Main.aspx")
        Else
            Cmd_Save.Enabled = True

        End If

        ParameterDt = M_CommonBL.GetLoanParameter(CmbScheme.SelectedValue)

        ' Setting Interest Rate
        SortingOrder = "IntRate Desc"
        drFiltered = SecDepDt.[Select]("", SortingOrder)
        For Each row As DataRow In drFiltered
            LoanIntRate = row("IntRate").ToString()
            Exit For
        Next

        ' ---- CHECKING MAXIMUM LOAN PERIOD ----
        Dim MaxPeriod As Double = 0
        Dim MaxDueDate As Date
        Dim PeriodType As String = "D"
        Dim PrdDays As Integer = 0

        If ParameterDt.Rows.Count > 0 Then
            MaxPeriod = ParameterDt.Rows(0)("Lnp_MaxPeriod")
            PeriodType = ParameterDt.Rows(0)("Lnp_PeriodType")
        End If

        PrdDays = DateDiff(DateInterval.Day, CDate(TxtLoanDt.Text), CDate(Ce_DueDate.Text))

        If PeriodType = "D" Then
            MaxDueDate = DateAdd(DateInterval.Day, MaxPeriod, CDate(TxtLoanDt.Text))
        Else
            MaxDueDate = DateAdd(DateInterval.Month, MaxPeriod, CDate(TxtLoanDt.Text))
        End If

        If PrdDays > DateDiff(DateInterval.Day, CDate(TxtLoanDt.Text), MaxDueDate) Then
            PrdDays = DateDiff(DateInterval.Day, CDate(TxtLoanDt.Text), MaxDueDate)
            Ce_DueDate.Text = MaxDueDate
        End If

        Txt_Days.Text = PrdDays
        ' ---- CHECKING MAXIMUM LOAN PERIOD ----

        ' Getting Interest Rate
        Dim IntRateDt As DataTable
        IntRateDt = M_LoanMasterBL.Get_ExtraIntr_Rates(SecTDSchCode, TxtLoanDt.Text)
        If IntRateDt.Rows.Count > 0 Then
            If IntRateDt.Rows(0)("DL_IntPercenttype") = "F" Then ' Fixed Interest Rate
                LoanIntRate = CDbl(IntRateDt.Rows(0)("DL_IntPercentage"))
            Else
                LoanIntRate = LoanIntRate + CDbl(IntRateDt.Rows(0)("DL_IntPercentage"))
            End If
        End If
        Txt_InterestRate.Text = Format(LoanIntRate, "#0.00")

        ' Getting Eligible Amount
        Txt_ElgibleAmt.Text = CDbl(Txt_SecDepAmt.Text) * (CDbl(IntRateDt.Rows(0)("DL_EligiblePercent")) / 100)

        ' Security Deposit Scheme Code
        If AccDt.Rows.Count > 0 Then
            HdnSecDepType.Value = AccDt.Rows(0)("MemSchCode").ToString
        End If

        ' Fill Dropdown Lists

        M_CommonBL.Fill_SchemePurpose(CmbPurpose, CmbScheme.SelectedValue)
        M_CommonBL.Fill_Pickup(CmbSubPurpose, 32, Val(CmbPurpose.SelectedValue))
        M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)

        M_CommonBL = Nothing
        M_LoanMasterBL = Nothing

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            Set_Grid()
            Load_DefaultValues()

            Session("M_EntryMode") = "NEW"

            CmbCashTransfer.Items.Add("Cash")
            CmbCashTransfer.Items.Add("Transfer")

            Ce_DueDate.Attributes.Add("readonly", "readonly")
            Txt_Days.Attributes.Add("readonly", "readonly")
            Txt_Month.Attributes.Add("readonly", "readonly")
            TxtLoanDt.Attributes.Add("readonly", "readonly")


            If Len(Request.QueryString("TRANID")) > 0 Then
                GetAuthoriseDtls("DLOAN", Request.QueryString("TRANID"))
            Else
                Assign_Values()
            End If


        End If
    End Sub

    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("DLOAN", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        Dim M_MemTbl As DataTable
        Dim M_FDTbl As DataTable

        M_TranTbl = M_Dataset.Tables(0)
        M_MemTbl = M_Dataset.Tables(1)
        M_FDTbl = M_Dataset.Tables(2)
        DgvTransaction.DataSource = M_Dataset.Tables(3)
        Session("TransTbl") = M_Dataset.Tables(3)
        DgvTransaction.DataBind()
        M_CommonBL = Nothing

        GetAccountDtls(M_TranTbl, M_MemTbl, M_FDTbl)
        PnlAuthorise.Enabled = False
        'Calc_TranTotal()
        Set_TransactionGrid()
    End Sub

    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable, ByVal M_MemTbl As DataTable, ByVal M_FDTbl As DataTable)
        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        CmbOpMode.SelectedValue = M_AccTbl.Rows(0).Item("Lm_OperationMode")
        CmbScheme.SelectedValue = M_AccTbl.Rows(0).Item("Sch_Code")
        TxtNoOfOperator.Text = M_AccTbl.Rows(0).Item("NoOfOp")
        TxtLoanDt.Text = M_AccTbl.Rows(0).Item("Lm_LoanDate")
        Txt_SecDepAmt.Text = M_AccTbl.Rows(0).Item("Ls_DepAmt")
        Txt_ElgibleAmt.Text = M_AccTbl.Rows(0).Item("Ls_EligibleAmt")
        Txt_Days.Text = M_AccTbl.Rows(0).Item("Lm_Period")
        Ce_DueDate.Text = M_AccTbl.Rows(0).Item("Lm_DueDate")
        Txt_SanctionedAmt.Text = M_AccTbl.Rows(0).Item("Lm_LoanAmount")
        Txt_InterestRate.Text = M_AccTbl.Rows(0).Item("Lm_IntRate")
        CmbPurpose.SelectedValue = M_AccTbl.Rows(0).Item("Lm_Mainpurpose")
        CmbSubPurpose.SelectedValue = M_AccTbl.Rows(0).Item("Lm_Subpurpose")
        CmbSanctionedBy.SelectedValue = M_AccTbl.Rows(0).Item("Lm_SanctionBy")
        Ddl_Priority.SelectedIndex = M_AccTbl.Rows(0).Item("Lm_Priority")
        CmbCashTransfer.SelectedIndex = M_AccTbl.Rows(0).Item("Cash")

        Session("AccHolderTbl") = M_MemTbl
        DgvCustomer.DataSource = M_MemTbl
        DgvCustomer.DataBind()

        Session("SecDepTbl") = M_FDTbl
        DgvSecDeposit.DataSource = M_FDTbl
        DgvSecDeposit.DataBind
    End Sub

    Private Function Validate_Control() As Boolean

        If Val(Txt_SanctionedAmt.Text) <= 0 Then
            Txt_SanctionedAmt.Focus()
            ShowAlertMessage("INVALID LOAN AMOUNT ENTERED")
            Return False
        End If

        Return True
    End Function

    Private Sub Cmd_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Save.Click

        Dim M_CommonBL As New CommonBL
       
        If Len(Request.QueryString("TRANID")) > 0 Then

            If M_CommonBL.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                ShowAlertMessage("Authotisation Failed")
                Response.Redirect("~/ManagerPassing.aspx")
                Exit Sub
            End If
            M_CommonBL = Nothing
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
            Exit Sub
        End If

        If Validate_Control() = False Then
            Exit Sub
        End If

        Dim M_LoanMaster As New SMARTCORE_BO.LoanMasterBO
        With M_LoanMaster
            .AccID = M_AccID
            .CustId = DgvCustomer.Rows(0).Cells(4).Text
            .LmHolidayPeriod = 0
            .LmInstallmentNo = 1
            .LmApprID = 0
            .LmMainpurpose = CmbPurpose.SelectedValue
            .LmAgentCode = 0
            .LmJewName = 0
            .LmSubpurpose = CmbSubPurpose.SelectedValue
            .LmPriority = Ddl_Priority.SelectedIndex
            .LmSanctionBy = CmbSanctionedBy.SelectedValue
            .LmNomNos = 0
            .LmCumType = 0
            .LogID = 0
            .LmLoanDate = TxtLoanDt.Text
            .LmDueDate = Ce_DueDate.Text
            .LmLastIntDate = TxtLoanDt.Text
            .LmResDate = TxtLoanDt.Text
            .LmLastUpdate = TxtLoanDt.Text
            .LmOpIntdate = TxtLoanDt.Text
            .LmOrnamentsReleasedDate = "01/01/1900"
            .Cashiertime = Date.Now
            .LmjewBillDate = Date.Now
            .LmLegalDate = Date.Now
            .LmNpaDate = "01/01/1900"
            .LmFirstNpaDate = "01/01/1900"
            .LmClosedDate = "01/01/1900"
            .MakingTime = Date.Now
            .CheckingTime = "01/01/1900"
            .LmLoanAmount = Val(Txt_SanctionedAmt.Text)
            .LmBalance = Val(Txt_SanctionedAmt.Text)
            .LmPeriod = Val(Txt_Days.Text)
            .LmMarketRate = 0
            .LmBankRate = 0
            .LmIntRate = Val(Txt_InterestRate.Text)
            .LmBillAmt = 0
            .LmPenalRate = 0
            .LmInstallmentAmount = Val(Txt_SanctionedAmt.Text)
            .LmApprValue = 0
            .LmOpBal = 0
            .LmOpPayAmt = 0
            .LmOpIntrate = Val(Txt_InterestRate.Text)
            .AccNo = M_AccNo
            .SchCode = CmbScheme.SelectedValue
            .BrCode = CmbBranch.SelectedValue
            .LmApplSlno = 0
            .AccCode = M_AccHead
            .LmShrBrCode = ""
            .CheckerID = ""
            .CashierID = ""
            .BankAccNo = ""
            .LmJewBillno = ""
            .LmRegNo = ""
            .LmLbrCode = CmbBranch.SelectedValue
            .LmLbrActCode = ""
            .LmIntroducedby = ""
            .TranNo = ""
            .MakerID = Session("UserID")
            .LmShrSchCode = DgvCustomer.Rows(0).Cells(2).Text
            .LmMemNo = DgvCustomer.Rows(0).Cells(3).Text
            .LmResNo = ""
            .LmSanctionNo = ""
            .LmSecurityDetails = ""
            .LmRemarks = ""
            .LmBalType = "D"
            .LmPeriodType = "D"  ' To Do
            .LmHolidayPeriodType = "D"
            .LmOperationMode = CmbOpMode.SelectedValue
            .LmInsurance = "N"
            .LmRegType = "R"
            .Authorised = "N"
            .LmOrnamentsReleased = "N"
            .LmStandingForSecDeposit = "N"
            .LmOpBalType = "D"
            .LmStatus = "Y"
            .LmEnhancement = "N"
            .LmLegalStatus = "N"
            .LmNpaStatus = "N"
            .LmFirstNpaStatus = "1"
            .LMDirector = "0"

            Dim AccHolderdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_AccHolderSw As New IO.StringWriter
            AccHolderdt.WriteXml(M_AccHolderSw)

            Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Trandt.WriteXml(M_TranSw)

            Dim SecDepdt As DataTable = DirectCast(Session("SecDepTbl"), DataTable)
            Dim M_SecDepSw As New IO.StringWriter
            SecDepdt.WriteXml(M_SecDepSw)

            Dim M_LoanMasterBL As New LoanMasterBL
            Dim M_Dt As New DataTable
            Session("M_EntryMode") = "NEW"
            M_Dt = M_LoanMasterBL.Save_Data(M_LoanMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), "", M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), M_SecDepSw.ToString.Replace("DocumentElement", "NewDataSet"))

            ShowAlertMessage(" LOAN NO : " & M_Dt.Rows(0)("Acc_No").ToString)
            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=DL" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=LOAN" & "&DRCR=" & "D")

        End With

        'Try
        '    Dim M_DepositLoanBL As New DepositLoanBL
        '    M_AccID = M_DepositLoanBL.Save_Data(M_DepositLoan)
        '    MsgBox("AccID : " & M_AccID)
        '    M_DepositLoanBL = Nothing
        'Catch ex As Exception

        'End Try
    End Sub


    Protected Sub ImageButton5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton5.Click
        If Len(Request.QueryString("TRANID")) > 0 Then
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
        Else
            Response.Redirect("~/Main.aspx")
        End If
    End Sub

    Private Sub Set_Grid()

        SecDepTbl.Columns.Add("AccID", GetType(Double))
        SecDepTbl.Columns.Add("Branch", GetType(String))
        SecDepTbl.Columns.Add("Scheme", GetType(String))
        SecDepTbl.Columns.Add("AccNo", GetType(String))
        SecDepTbl.Columns.Add("BALANCE", GetType(Double))
        SecDepTbl.Columns.Add("BALTYPE", GetType(String))
        SecDepTbl.Columns.Add("DUEDATE", GetType(Date))
        SecDepTbl.Columns.Add("INTRATE", GetType(Double))
        SecDepTbl.Columns.Add("AccStatus", GetType(String))

        SecDepTbl.Rows.Add("0", "", "", "", 0, "Cr", "01/01/1900", 0, "")

        Dim columns(1) As DataColumn
        columns(0) = SecDepTbl.Columns("AccID")
        SecDepTbl.PrimaryKey = columns
        Session("SecDepTbl") = SecDepTbl
        DgvSecDeposit.DataSource = SecDepTbl
        DgvSecDeposit.DataBind()
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmbPurpose_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbPurpose.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Pickup(CmbSubPurpose, 32, CmbPurpose.SelectedValue)
        M_CommonBL = Nothing
    End Sub

    Protected Sub DgvSecDeposit_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvSecDeposit.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(7).Text = [String].Format("{0:dd/MM/yyyy}", [Date].Parse(e.Row.Cells(7).Text))
            Txt_SecDepAmt.Text = Val(Txt_SecDepAmt.Text) + Val(e.Row.Cells(4).Text)
        End If
    End Sub

    Protected Sub Txt_SanctionedAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_SanctionedAmt.TextChanged
        Set_TransactionGrid()
    End Sub

    Private Sub Set_TransactionGrid()
        Dim M_CommonBL As New CommonBL
        Dim Ds As DataSet

        If Val(Txt_SanctionedAmt.Text) > Val(Txt_ElgibleAmt.Text) Then
            Txt_SanctionedAmt.Text = Txt_ElgibleAmt.Text
        End If

        Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtLoanDt.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "D", "M", Val(Txt_SanctionedAmt.Text))
        Dim Dt As DataTable = Ds.Tables(0)
        DgvTransaction.DataSource = Dt
        DgvTransaction.DataBind()
        Session("TransTbl") = Dt
        M_CommonBL = Nothing

        If Ds.Tables(1).Rows.Count > 0 Then
            If Ds.Tables(1).Rows(0)("AlertMsg").ToString.Length > 0 Then
                Dim AlertStr As String
                AlertStr = Ds.Tables(1).Rows(0)("AlertMsg").ToString
                ShowAlertMessage(AlertStr)
            End If
        End If
    End Sub

    Protected Sub CmbCashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.SelectedIndexChanged
        Set_TransactionGrid()
    End Sub


End Class