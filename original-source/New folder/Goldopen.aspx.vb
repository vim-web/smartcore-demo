Imports SMARTCORE_BL
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO.MemoryStream
Imports System.IO
Partial Public Class Goldopen
    Inherits System.Web.UI.Page
    Public OrnamentTbl As New DataTable("Table")
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Shared M_AccHead As String
    Shared ParameterDt As DataTable
    Shared EmiParameter As DataTable
    Shared Emi_IRRParameter As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Set_Grid()
            Load_DefaultValues()
            Assign_Values()
            TxtMarketRate.Attributes.Add("readonly", "readonly")
            TxtMarketPrice.Attributes.Add("readonly", "readonly")
            TxtEligibleAmt.Attributes.Add("readonly", "readonly")
            TxtLoanDt.Attributes.Add("readonly", "readonly")
            TxtIntRate.Attributes.Add("readonly", "readonly")
            TxtDueDate.Attributes.Add("readonly", "readonly")
            'TxtPrd.Attributes.Add("readonly", "readonly")
            TxtPIntRate.Attributes.Add("readonly", "readonly")
            Session("Tran_Dt") = Nothing
            If Len(Request.QueryString("TRANID")) > 0 Then
                Hid_ODPStatus.Value = "Y"
                GetAuthoriseDtls("GOLD", Request.QueryString("TRANID"))
                Load_Img(Request.QueryString("TRANID"))
                FileUpload4.Enabled = False
                FileUpload5.Enabled = False
                If Len(Request.QueryString("DEL")) > 0 Then
                    CmdSave.Visible = False
                    CmdClear.Visible = False
                    cmdDelete.Visible = True
                End If
            End If
            'butSetTransferdet.Attributes.Add("OnClick", "window.open('TransfermodeSetting.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If
    End Sub

    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        If True Then
            Dim Expression As String = "Pkc_Type = 30 and Pkc_Description LIKE '" & prefixText & "%'"
            Dim SortingOrder As String = "Pkc_Description ASC"
            Dim drFiltered As DataRow() = PickupDt.[Select](Expression, SortingOrder)
            Dim CompletionSet = New List(Of String)()
            For Each row As DataRow In drFiltered
                CompletionSet.Add(row("Pkc_Description").ToString().PadRight(40, Convert.ToChar(".")) & "|" & "Code:" & row("Pkc_Code").ToString())
            Next
            Return CompletionSet.ToArray()
        End If
    End Function
    Private Sub Assign_Values()
        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
        CmbOpMode.SelectedValue = Session("AcOpenOpMode")
        TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_SchemePurpose(CmbPurpose, CmbScheme.SelectedValue)
        M_CommonBL.Fill_Pickup(CmbSubPurpose, 32, Val(CmbPurpose.SelectedValue))

        If Len(Request.QueryString("TRANID")) = 0 Then
            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            If dt.Rows.Count > 0 Then
                'TxtMobileNo.Text = dt.Rows(0)("MblNo").ToString()
                DgvCustomer.DataSource = dt
                DgvCustomer.DataBind()
            End If

            HdnMemBrCode.Value = dt.Rows.Item(0)(1).ToString()
            HdnMemType.Value = dt.Rows.Item(0)(2).ToString()
            HdnMemNo.Value = dt.Rows.Item(0)(4).ToString()
            HdnCustID.Value = dt.Rows.Item(0)(5).ToString()
            TxtMobileNo.Text = dt.Rows(0).Item("Mblno").ToString()
            Dim M_BankRate As Double
            Dim DueDays As Double

            TxtMarketRate.Text = M_CommonBL.GetGoldMarketRate(CmbScheme.SelectedValue, M_BankRate, Session("DefaultBrCode"), Session("TransDate"))
            M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)
            TxtBankRate.Text = M_BankRate
            ParameterDt = M_CommonBL.GetLoanParameter(CmbScheme.SelectedValue)
            TxtLoanDt.Text = Session("TransDate")
            TxtPrd.Text = ParameterDt.Rows(0).Item("Lnp_MinPeriod")
            DueDays = Val(TxtPrd.Text) * 30
            TxtDueDate.Text = DateAdd(DateInterval.Day, DueDays, CDate(TxtLoanDt.Text))
            EmiParameter = M_CommonBL.Get_EMIType(CmbScheme.SelectedValue)

            HdnPrdType.Value = ParameterDt.Rows(0).Item("Lnp_PeriodType")
            ViewState("Lnp_EMIType") = ParameterDt.Rows(0).Item("Lnp_EMIType")

            If ParameterDt.Rows(0).Item("AccTranType") = "T" Then
                CmbCashTransfer.SelectedIndex = 1
                CmbCashTransfer.Enabled = False
            ElseIf ParameterDt.Rows(0).Item("AccTranType") = "C" Then
                CmbCashTransfer.SelectedIndex = 0
                CmbCashTransfer.Enabled = False
            End If

            TxtAdvEmi.Text = ParameterDt.Rows(0).Item("Lnp_Adv_EmiNos")
            If ParameterDt.Rows(0).Item("Lnp_PeriodType") = "D" Then
                TxtDueDate.Text = DateAdd(DateInterval.Day, Val(TxtPrd.Text), CDate(TxtLoanDt.Text))
            Else
                DueDays = Val(TxtPrd.Text) * 30
                TxtDueDate.Text = DateAdd(DateInterval.Day, DueDays, CDate(TxtLoanDt.Text))
            End If

            If (EmiParameter.Rows.Count > 0) Then
                If EmiParameter.Rows(0)("Lnp_EMIType") = "Y" Then
                    btnEMI.Visible = True
                Else
                    btnEMI.Visible = False
                    ''TxtIntRate.Attributes.Add("readonly", "readonly")
                End If

            End If

        End If
        M_CommonBL = Nothing
    End Sub

    Private Sub Calc_TranTotal()
        Try

            Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)

            Dim objSum As Object
            If Trandt.Rows.Count > 0 Then
                objSum = Trandt.Compute("Sum(RctCash)", "")
                LblRctCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
                objSum = Trandt.Compute("Sum(RctTransfer)", "")
                LblRctTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
                objSum = Trandt.Compute("Sum(PayCash)", "")
                LblPayCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
                objSum = Trandt.Compute("Sum(PayTransfer)", "")
                LblPayTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            End If
        Catch ex As Exception
            'LoggError(ex)
        End Try

    End Sub
    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdDelete.Click
        Try


            Dim Tran_No As String
            Dim M_DBConn As New SMARTCORE_DAL.Connection()
            Dim ClsCom As New SMARTCORE_Common.CommonClass
            Dim SqlStr As String
            Dim DeleteBranch As String
            DeleteBranch = Request.QueryString("DelBranch").ToString()
            Tran_No = Request.QueryString("TRANID").ToString()

            SqlStr = "Exec Transaction_Delete_Sp '" & Tran_No & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
                ",'" & Session("UserID") & "','" & DeleteBranch & "'"
            M_DBConn.ExecuteScalar(SqlStr, , False)

            Response.Redirect("~/trancancellation.ASPX")

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
        M_CommonBL.Fill_Pickup(CmbSanctionedBy, 33, 0)
        M_CommonBL.Fill_Pickup(CmbPurpose, 31, 0)
        M_CommonBL.Fill_Pickup(CmbPurity, 95, 0)
        M_CommonBL.Fill_Pickup(CmbSubPurpose, 32, CmbPurpose.SelectedValue)
        ' M_CommonBL.Fill_Agent(CmbAppraiser, CommonBL.AgentType.Appraiser)
        M_CommonBL.Fill_CanvassedBy(CmbCanvassedBy)
        M_CommonBL.Fill_User(CmbAppraiser)
        CmbAppraiser.SelectedItem.Text = Session("LoginID")
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        DgvTransaction.DataSource = Nothing
        Dim Dt As DataTable
        Dt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
        DgvTransaction.DataSource = Dt
        DgvTransaction.DataBind()
        PickupDt = DirectCast(Cache("PickupTbl"), DataTable)
        M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)

        CmbPriority.Items.Add("NO")
        CmbPriority.Items.Add("YES")
        CmbPriority.SelectedIndex = 0
        CmbCashTransfer.Items.Add("CASH")
        CmbCashTransfer.Items.Add("TRANSFER")
        CmbCashTransfer.SelectedIndex = 0

        M_CommonBL.Fill_Pickup(drpTranferType, 108, 0)
        M_CommonBL.GetBranchBankDetails(drpBank, Session("DefaultBrCode").ToString())
        M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
        DdlTranType.Items.Add("<--Select-->")
        DdlTranType.Items.Add("Credit")
        DdlTranType.Items.Add("Debit")
        DdlTranType.SelectedIndex = 0
        dvRTGS.Visible = False
        M_CommonBL = Nothing

        CmbNomReq.Items.Add("No")
        CmbNomReq.Items.Add("Yes")
        CmbNomReq.SelectedIndex = 0
        CheckBox1.Visible = False
        cmdDelete.Visible = False
    End Sub

    Private Sub Set_Grid()

        OrnamentTbl.Columns.Add("CODE", GetType(String))
        OrnamentTbl.Columns.Add("NAME", GetType(String))
        OrnamentTbl.Columns.Add("DETAILS", GetType(String))
        OrnamentTbl.Columns.Add("COUNT", GetType(Double))
        OrnamentTbl.Columns.Add("GrossWt", GetType(Double))
        OrnamentTbl.Columns.Add("NetWt", GetType(Double))
        OrnamentTbl.Columns.Add("EligibleAmt", GetType(Double))
        OrnamentTbl.Columns.Add("Remarks", GetType(String))
        OrnamentTbl.Columns.Add("Purity", GetType(Double))
        OrnamentTbl.Rows.Add("", "", "", 0, 0, 0, 0, "", "0")


        Dim columns(2) As DataColumn
        columns(0) = OrnamentTbl.Columns("CODE")
        columns(1) = OrnamentTbl.Columns("Purity")
        columns(2) = OrnamentTbl.Columns("GrossWt")
        OrnamentTbl.PrimaryKey = columns
        Session("OrnTbl") = OrnamentTbl
        DgvOrnaments.DataSource = OrnamentTbl
        DgvOrnaments.DataBind()
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdOrnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdOrnAdd.Click

        If Val(TxtCount.Text) <= 0 Then
            ShowAlertMessage("INVALID ORNAMENT COUNT ENTERED")
            TxtCount.Focus()
            Exit Sub
        End If

        If Val(TxtNetWt.Text) > Val(TxtGrossWt.Text) Then
            ShowAlertMessage("INVALID WEIGHT ENTERED")
            TxtGrossWt.Focus()
            Exit Sub
        End If
        Dim dt As DataTable = DirectCast(Session("OrnTbl"), DataTable)
        Dim Row As DataRow
        Row = dt.NewRow
        Row("CODE") = TxtOrnCode.Text
        Row("NAME") = TxtOrnName.Text
        Row("DETAILS") = TxtOrnDetails.Text
        Row("COUNT") = Val(TxtCount.Text)
        Row("GrossWt") = Val(TxtGrossWt.Text)
        Row("NetWt") = Val(TxtNetWt.Text)
        Dim ElgblAmnt As Double = EligibleAmountCalc()
        Row("EligibleAmt") = ElgblAmnt
        Row("Remarks") = TxtRemarks.Text
        Row("Purity") = CmbPurity.SelectedValue
        Try
            dt.Rows.Add(Row)
        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "CODE=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try
        DgvOrnaments.DataSource = dt
        DgvOrnaments.DataBind()
        Session("OrnTbl") = dt



        Call Calc_OrnTot()
        ClearOrnaments()

    End Sub

    Private Sub Calc_OrnTot()
        Dim dt As DataTable = DirectCast(Session("OrnTbl"), DataTable)
        If Len(Request.QueryString("TRANID")) = 0 Then
            Dim objSum As Object
            objSum = dt.Compute("Sum(COUNT)", "")
            TxtTotalCount.Text = Format(Val(objSum.ToString()), "#0.00")

            objSum = dt.Compute("Sum(GrossWt)", "")
            TxtTotalGrossWt.Text = Format(Val(objSum.ToString()), "#0.000")
            If Val(TxtTotalGrossWt.Text) = 0 Then
                TxtTotalGrossWt.Enabled = True
            Else
                TxtTotalGrossWt.Enabled = False
            End If

            objSum = dt.Compute("Sum(NetWt)", "")
            'TxtTotalNetWt.Text = Format(Val(objSum.ToString()), "#0.000")
            Dim objCmnBl As New CommonBL
            Dim DtWaDe As New DataTable
            DtWaDe = objCmnBl.WastageDeduction(Val(objSum.ToString()))
            TxtTotalNetWt.Text = Format(DtWaDe.Rows(0).Item("TotalNw"), "#0.000")
            If Val(TxtTotalNetWt.Text) = 0 Then
                TxtTotalNetWt.Enabled = True
            Else
                TxtTotalNetWt.Enabled = False
            End If
        End If
        TxtMarketPrice.Text = Format(Val(TxtTotalNetWt.Text) * Val(TxtMarketRate.Text), "#0.00")
        'TxtEligibleAmt.Text = Format(Val(TxtTotalNetWt.Text) * Val(TxtBankRate.Text), "#0.00")
        'EligibleAmountCalc()
        Dim objElSum As Object
        objElSum = dt.Compute("Sum(EligibleAmt)", "")
        TxtEligibleAmt.Text = Format(Val(objElSum.ToString()), "#0.000")
        TxtAppraiserAmt.Text = TxtEligibleAmt.Text

    End Sub


    Private Sub ClearOrnaments()
        TxtOrnCode.Text = ""
        TxtOrnName.Text = ""
        TxtOrnDetails.Text = ""
        TxtCount.Text = ""
        TxtGrossWt.Text = ""
        TxtNetWt.Text = ""
        TxtRemarks.Text = ""
        TxtOrnCode.Focus()
    End Sub

    Protected Sub TxtOrnCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtOrnCode.TextChanged
        On Error Resume Next
        If Len(TxtOrnCode.Text) = 0 Then
            TxtCount.Focus()
            Exit Sub
        End If
        Dim Expression As String = "Pkc_Code = " & TxtOrnCode.Text
        Dim SortingOrder As String = "Pkc_Description ASC"
        Dim drFiltered As DataRow() = PickupDt.[Select](Expression, SortingOrder)
        For Each row As DataRow In drFiltered
            TxtOrnName.Text = row("Pkc_Description").ToString()
            TxtOrnDetails.Focus()
        Next
    End Sub

    Protected Sub CmbPurpose_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbPurpose.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Pickup(CmbSubPurpose, 32, CmbPurpose.SelectedValue)
        M_CommonBL = Nothing
    End Sub
    Private Function Validate_Control() As Boolean
        If Val(TxtPrd.Text) = 0 Then
            TxtPrd.Focus()
            ShowAlertMessage("INVALID LOAN PERIOD ENTERED")
            Return False
        End If
        'If Val(TxtCount.Text) <= 0 Then
        '    TxtCount.Focus()
        '    ShowAlertMessage("INVALID ORNAMENT COUNT ENTERED")
        '    Return False
        'End If

        'If Val(TxtTotalGrossWt.Text) <= 0 Then
        '    TxtGrossWt.Focus()
        '    ShowAlertMessage("INVALID GROSS WEIGHT ENTERED")
        '    Return False
        'End If

        'If Val(TxtTotalNetWt.Text) <= 0 Then
        '    TxtNetWt.Focus()
        '    ShowAlertMessage("INVALID NET WEIGHT ENTERED")
        '    Return False
        'End If
        If ImageID.Value = "N" Then
            ShowAlertMessage("Please upload ornament image")
            Return False
        End If
        If ImageID2.Value = "N" Then
            ShowAlertMessage("Please upload ornament image")
            Return False
        End If
        'Dim rowCount As String
        'rowCount = GridView1.Rows.Count
        'If rowCount = 0 Then
        '    ShowAlertMessage("Please upload ornament image")
        '    Return False
        'End If
        If Val(CmbSubPurpose.SelectedItem.Text.Length) <= 0 Then
            CmbSubPurpose.Focus()
            ShowAlertMessage("INVALID SUB PURPOSE. CONTACT ADMIN")
            Return False
        End If
        If Val(TxtSanctionedAmt.Text) <= 0 Then
            TxtSanctionedAmt.Focus()
            ShowAlertMessage("INVALID SANCTIONED AMT ENTERED")
            Return False
        End If

        If Val(TxtSanctionedAmt.Text) > Val(TxtEligibleAmt.Text) Then
            TxtSanctionedAmt.Focus()
            ShowAlertMessage("SANCTIONED AMT IS GREATER THAN ELIGIBLE AMOUNT")
            Return False
        End If

        If CmbCanvassedBy.SelectedItem.Text = "<-- Select -->" Then
            CmbCanvassedBy.Focus()
            ShowAlertMessage("KINDLY PLEASE SELECT CANVASSER")
            Return False
        End If
        Dim M_Cash As New DataTable
        Dim M_CommonBL As New CommonBL
        M_Cash = M_CommonBL.ValidSactionAmntLmt(CmbScheme.SelectedValue, Val(TxtSanctionedAmt.Text))

        If M_Cash.Rows(0).Item("stats") = "N" Then
            ShowAlertMessage("Loan amount should not match as per scheme requirements")
            Return False
        End If

        Return True
    End Function
    Public Sub Calc_InterestRate()
        Dim M_CommonBL As New CommonBL
        Dim dt As New DataTable
        'TxtIntRate.Text = Format(M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", Val(TxtPrd.Text), HdnPrdType.Value, Val(TxtSanctionedAmt.Text), CommonBL.Intr_RateTypes.INTR_RATE_NORMAL, "", "", "Y"), "#0.00")
        'TxtPIntRate.Text = Format(M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", Val(TxtPrd.Text), HdnPrdType.Value, Val(TxtSanctionedAmt.Text), CommonBL.Intr_RateTypes.INTR_RATE_PENAL, "", "", "Y"), "#0.00")
        dt = M_CommonBL.Get_NewIntr_Rate(CmbScheme.SelectedValue)
        TxtIntRate.Text = Format(dt.Rows(0).Item("intrest"), "#0.00")
        TxtPIntRate.Text = 0
        M_CommonBL = Nothing
    End Sub
    Private Function CashControl() As Boolean
        Try
            If CmbCashTransfer.SelectedItem.Text <> "CASH" Then
                Return True
                Exit Function
            End If
            Dim M_con As New CommonBL
            Dim M_Cash As New DataTable()
            Dim SchCash As Double
            Dim BrCash As Double
            Dim TransferDt As DataTable = DirectCast(Session("TransTbl"), DataTable)
            Dim CashRctTotal As Double
            Dim CashPayTotal As Double
            Dim TransferRctTotal As Double
            Dim TransferPayTotal As Double


            CashRctTotal = TransferDt.Compute("SUM(RctCash)", "")
            CashPayTotal = TransferDt.Compute("SUM(PayCash)", "")
            TransferRctTotal = TransferDt.Compute("SUM(RctTransfer)", "")
            TransferPayTotal = TransferDt.Compute("SUM(PayTransfer)", "")
            'M_Cash = M_con.GetTanLimit(Session("DefaultBrCode").ToString(), CmbScheme.SelectedValue, AccID.Value, CashRctTotal + CashPayTotal, CmbCashTransfer.SelectedItem.Text, Session("TransDate"))
            Dim custId As Integer = Val(DgvCustomer.Rows(0).Cells(4).Text)
            M_Cash = M_con.GetTanLimit(Session("DefaultBrCode").ToString(), CmbScheme.SelectedValue, CustId, CashRctTotal + CashPayTotal, CmbCashTransfer.SelectedItem.Text, Session("TransDate"))

            If M_Cash.Rows(0).Item("Status") = "N" Then
                ShowAlertMessage("Customer Cash Limit Exceeded, Change Cash to Transfer")
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function
    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_LoanMasterBL As New LoanMasterBL
        Dim M_CommonBL As New CommonBL
        Dim M_Dt11 As New DataTable

        Dim TranID As String = ""

        If Len(Request.QueryString("TRANID")) > 0 Then

            If M_CommonBL.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                ShowAlertMessage("Authotisation Failed")
                'Response.Redirect("~/ManagerPassing.aspx")
                Exit Sub
            End If
            TranID = (Request.QueryString("TRANID"))
            M_Dt11 = M_LoanMasterBL.Print_Data(TranID.ToString)
            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt11.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt11.Rows(0)("Tran_No").ToString & "&PrintBond=Y" & "&SECTION=GOLD" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=LOAN" & "&DRCR=" & "C" & "&SaveType=" & "N")

            'M_CommonBL = Nothing
            'Response.Redirect("~/ManagerPassing.aspx?Passing=y")
            Exit Sub
        End If
        If CashControl() = False Then
            Exit Sub
        End If
        If Validate_Control() = False Then
            Exit Sub
        End If
        If ChkBank.Checked = True Then
            If DdlTranType.SelectedIndex = 0 Then
                ShowAlertMessage("Please select Bank Transaction....")
                DdlTranType.Focus()
                Exit Sub
            End If
            If Val(IIf(TxtBankAmnt.Text = "", "0", TxtBankAmnt.Text)) <= 0 Then
                ShowAlertMessage("Please Enter Bank Amnount....")
                TxtBankAmnt.Focus()
                Exit Sub
            End If
        End If
        Dim Dt As New DataTable
        'Dim DtLimit As New DataTable
        'DtLimit = M_CommonBL.GetCashLimit(Session("DefaultBrCode"))
        'If DtLimit.Rows.Count > 0 Then
        '    If Val(TxtSanctionedAmt.Text) > DtLimit.Rows(0)("Br_CashLimit") And CmbCashTransfer.SelectedItem.Text = "CASH" Then
        '        ShowAlertMessage("Change CASH mode to TRANSFER mode.")
        '        'CmbCashTransfer.SelectedItem.Text = "TRANSFER"

        '        CmbCashTransfer.Focus()
        '        Calc_InterestRate()
        '        'Else
        '        '    CmbCashTransfer.SelectedItem.Text = "CASH"


        '        Exit Sub
        '    End If
        'End If
        Dim M_LoanMaster As New SMARTCORE_BO.LoanMasterBO
        Dim M_CmmnBL As New CommonBL
        EmiParameter = M_CmmnBL.Get_EMIType(CmbScheme.SelectedValue)
        Dim InstalAmnt As Double
        Dim InstalNo As Integer
        InstalAmnt = Val(TxtSanctionedAmt.Text)
        InstalNo = 1
        If ViewState("Lnp_EMIType") = "Y" Then
            InstalAmnt = Val(TxtInstAmnt.Text)
            InstalNo = Val(TxtPrd.Text)
        End If
        'Emi_IRRParameter = M_CmmnBL.Get_IRR(CmbScheme.SelectedValue)
        With M_LoanMaster
            .AccID = M_AccID
            .CustId = DgvCustomer.Rows(0).Cells(4).Text
            .LmHolidayPeriod = 0
            .LmInstallmentNo = InstalNo
            .LmApprID = CmbAppraiser.SelectedIndex
            .LmMainpurpose = CmbPurpose.SelectedValue
            .LmAgentCode = 0
            .LmJewName = 0
            .LmSubpurpose = CmbSubPurpose.SelectedValue
            .LmPriority = CmbPriority.SelectedIndex
            .LmSanctionBy = CmbSanctionedBy.SelectedValue
            .LmNomNos = IIf(NomDiv.Visible = False, 0, 1)
            .LmCumType = 0
            .LogID = 0
            .LmLoanDate = TxtLoanDt.Text
            .LmDueDate = TxtDueDate.Text
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
            .LmLoanAmount = Val(TxtSanctionedAmt.Text)
            .LmBalance = Val(TxtSanctionedAmt.Text)
            .LmPeriod = Val(TxtPrd.Text)
            .LmMarketRate = Val(TxtMarketRate.Text)
            .LmBankRate = Val(TxtBankRate.Text)
            .LmIntRate = Val(TxtIntRate.Text)
            'If (EmiParameter.Rows.Count > 0) Then
            '    If EmiParameter.Rows(0)("Lnp_EMIType") = "N" Then
            '        .LmIntRate = Val(TxtIntRate.Text)
            '    Else
            '        .LmIntRate = Emi_IRRParameter.Rows(0)("Int_IRR")
            '    End If
            'End If
            .LmBillAmt = 0
            .LmPenalRate = Val(TxtPIntRate.Text)
            .LmInstallmentAmount = InstalAmnt
            .LmApprValue = Val(TxtSanctionedAmt.Text)
            .LmOpBal = 0
            .LmOpPayAmt = 0
            .LmOpIntrate = Val(TxtIntRate.Text)
            .AccNo = M_AccNo
            .SchCode = CmbScheme.SelectedValue
            .BrCode = CmbBranch.SelectedValue
            .LmApplSlno = 0
            .AccCode = M_AccHead
            .CheckerID = ""
            .CashierID = ""
            .BankAccNo = ""
            .LmJewBillno = ""
            .LmRegNo = ""
            .LmLbrCode = CmbBranch.SelectedValue
            .LmLbrActCode = ""
            .LmIntroducedby = CmbCanvassedBy.SelectedValue
            .TranNo = ""
            .MakerID = Session("UserID")
            .LmShrBrCode = HdnMemBrCode.Value
            .LmShrSchCode = HdnMemType.Value
            .LmMemNo = HdnMemNo.Value
            .LmResNo = ""
            .LmSanctionNo = ""
            .LmSecurityDetails = ""
            .LmRemarks = ""
            .LmBalType = "D"
            .LmPeriodType = HdnPrdType.Value
            .LmHolidayPeriodType = "M"
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
            .LmGrossWt = Val(TxtTotalGrossWt.Text)
            .LmNetWt = Val(TxtTotalNetWt.Text)
            .LMDirector = "0"
            .LM_Cent = "0"
            .LMSurveyNo = TxtSurveyNo.Text.Trim()
            .LM_EMI_IRR = Val(TxtIRR.Text)
            .LM_AdvEMI = Val(TxtAdvEmi.Text)
            '.LmIntroducedby = CmbCanvassedBy.SelectedValue
        End With

        Try


            Dim AccHolderdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_AccHolderSw As New IO.StringWriter
            AccHolderdt.WriteXml(M_AccHolderSw)

            Dim Orndt As DataTable = DirectCast(Session("OrnTbl"), DataTable)

            'If Val(Orndt.Compute("SUM(NetWt)", "")) = 0 Then
            '    ShowAlertMessage("Please Check Entered Ornament List")
            '    Exit Sub
            'End If

            Dim M_OrnSw As New IO.StringWriter
            Orndt.WriteXml(M_OrnSw)

            Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Trandt.WriteXml(M_TranSw)


            Dim M_NomineeSw As New IO.StringWriter
            If NomDiv.Visible = False Then
                Session("NomTbl") = ""
            Else
                Set_Nominee()
                Dim NomineeDt As DataTable = DirectCast(Session("NomTbl"), DataTable)
                NomineeDt.WriteXml(M_NomineeSw)
            End If

            'Dim M_LoanMasterBL As New LoanMasterBL
            Dim M_Dt As New DataTable

            'If CmbCashTransfer.SelectedItem.Text = "TRANSFER" And ChkBank.Checked = False Then
            '    ShowAlertMessage("Please Enter Bank Details")
            '    ChkBank.Focus()
            '    ChkBank.Font.Bold = True
            '    ChkBank.ForeColor = Drawing.Color.Red
            '    Exit Sub
            'End If

            Dim M_Dt2 As New DataTable()
            M_Dt2 = M_CommonBl.Get_CashBalance(Session("DefaultBrCode"), Session("TransDate"))
            If M_Dt2.Rows.Count > 0 Then
                If CmbCashTransfer.SelectedItem.Text = "CASH" And Val(TxtSanctionedAmt.Text) - IIf(TxtTransfer.Text = "", 0, Val(TxtTransfer.Text)) > Val(M_Dt2.Rows(0).Item("CashBalance")) Then
                    TxtSanctionedAmt.Text = 0
                    Set_TransactionGrid()
                    ShowAlertMessage("Please Check Your Cash Balance")
                    Exit Sub
                End If
            End If

            If ChkBank.Checked = True And CmbCashTransfer.SelectedItem.Text = "TRANSFER" Then
                Dim M_Dt1 As New DataTable
                Dim McommnBl As New CommonBL
                M_Dt1 = McommnBl.Get_BankBalance(drpbankAccno.SelectedItem.Text, Session("TransDate"))
                If Val(TxtBankAmnt.Text) > Val(M_Dt1.Rows(0).Item("BankBalance").ToString()) And DdlTranType.SelectedIndex = 1 Then
                    ShowAlertMessage("Please Check The Selected Bank Balance, Available Balance = '" & M_Dt1.Rows(0).Item("BankBalance").ToString() & "'")
                    Exit Sub
                End If
            End If

            Session("M_EntryMode") = "NEW"
            M_Dt = M_LoanMasterBL.Save_Data(M_LoanMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_OrnSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), , , , , M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"))
            UpLoadImageFile(M_Dt.Rows(0)("Acc_No").ToString)
            ' UploadMultiImageFile(M_Dt.Rows(0)("Acc_No").ToString)
            ShowAlertMessage(" LOAN NO : " & M_Dt.Rows(0)("Acc_No").ToString)

            'transfer mode saving-------------------------------------------

            If CmbCashTransfer.SelectedItem.Text = "TRANSFER" And ChkBank.Checked = True Then

                Dim M_LoanMasterBL1 As New LoanMasterBL
                Dim dtTransferDetails As New DataTable
                dtTransferDetails = M_LoanMasterBL1.SaveTransferDetails_Data(M_Dt.Rows(0)("Tran_No").ToString, drpBank.SelectedItem.ToString(), drpbankAccno.SelectedItem.Text.ToString(), TxtCheque.Text.ToString(), txtChequeDate.Text.ToString(), drpTranferType.SelectedValue.ToString(), txtIFSC.Text.ToString(), txtaccno.Text.ToString(), txtCommission.Text.ToString(), Session("TransDate"), Val(TxtBankAmnt.Text), IIf(DdlTranType.SelectedIndex = 1, "C", "D"))


            End If

            'transfer mode saving------------------------------------------

            ' Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=Y" & "&SECTION=GOLD")
            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=Y" & "&SECTION=GOLD" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=LOAN" & "&DRCR=" & "C" & "&SaveType=" & "Y")

            PrintBond_Gold(M_Dt.Rows(0)("Tran_No").ToString)

            '' VOU PRINT
            'Dim Prn As New Print
            'Dim DtLoc As New DataTable
            'Dim M_DBConn As New SMARTCORE_DAL.Connection
            'Dim Qry As String

            'Qry = "select * from Transaction_T_Tbl where tran_no='" & M_Dt.Rows(0)("Tran_No").ToString & "' and tran_type='C'"
            'M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
            'Prn.SetUPPrint(M_LoanMaster.BrCode, "LOAN", M_LoanMaster.SchCode, M_Dt.Rows(0)("Tran_no").ToString(), DtLoc.Rows(0)("Vr_No").ToString(), "C", 1, True, M_LoanMaster.LmLoanDate, Session("LoginID").ToString(), DtLoc)

            'M_LoanMasterBL = Nothing
            ''Response.Redirect("~/AccOpen.aspx")

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    'Private Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
    '    Try
    '        Dim dt As DataTable = DirectCast(Session("tran_dt"), DataTable)
    '        dt.Rows(e.RowIndex).Delete()
    '        dt.AcceptChanges()
    '        GridView1.DataSource = dt
    '        Session("tran_dt") = dt
    '        If dt.Rows.Count <= 0 Then Call Set_EmptyDataTable()
    '        GridView1.DataBind()
    '        ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "settab", "wizardCheck('three');", True)
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
    '    If e.CommandName.ToUpper = "SELECT" Then
    '        Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
    '        'Dim DocID As Integer = GridView1.DataKeys(RowIndex)("Id").ToString()
    '        Dim FileName As String
    '        FileName = GridView1.Rows(RowIndex).Cells(3).Text
    '        Me.ImgPhoto.ImageUrl = Convert.ToString("~/Photos/") & FileName
    '        Exit Sub
    '    End If
    'End Sub
    Public Sub PrintBond_Gold(ByVal Tran_No As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""

        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        M_Sql = "Exec Loan_Bond_Sp '" & Tran_No & "'"
        M_DBConn.ExecuteScalar(M_Sql, , False)

        M_Sql = "Select * FROM LoanBond_T_Tbl Where Tran_No='" & Tran_No & "'"
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        Session.Add("RptQry", M_Sql)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldBond.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptView.aspx", False)

    End Sub
    Private Sub Set_Nominee()
        Try
            Dim NomineeTbl As New DataTable("Table")
            NomineeTbl.Columns.Add("Nm_Nom_No", GetType(String))
            NomineeTbl.Columns.Add("Br_Code", GetType(String))
            NomineeTbl.Columns.Add("Sch_Code", GetType(String))
            NomineeTbl.Columns.Add("Acc_No", GetType(String))
            NomineeTbl.Columns.Add("Nm_Name", GetType(String))
            NomineeTbl.Columns.Add("Nm_Gender", GetType(String))
            NomineeTbl.Columns.Add("Nm_Relation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_Phone", GetType(String))
            NomineeTbl.Columns.Add("Nm_Address", GetType(String))
            NomineeTbl.Columns.Add("Nm_Occupation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_DOB", GetType(Date))
            NomineeTbl.Columns.Add("Nm_Age", GetType(Double))
            NomineeTbl.Columns.Add("Nm_Minor", GetType(String))
            NomineeTbl.Columns.Add("Nm_GName", GetType(String))
            NomineeTbl.Columns.Add("Nm_Grelation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_GAddress", GetType(String))
            NomineeTbl.Columns.Add("Nm_Narration", GetType(String))
            NomineeTbl.Columns.Add("Nm_EffectDate", GetType(Date))
            NomineeTbl.Columns.Add("Nm_Shr_Percent", GetType(Double))
            NomineeTbl.Columns.Add("Log_Id", GetType(Double))
            NomineeTbl.Rows.Add("", CmbBranch.SelectedValue, CmbScheme.SelectedValue, "", Txt_NomName.Text, "M", CmbNomRel.SelectedValue, Txt_NomPh.Text, Txt_NomAdd.Text, 0, "01/01/1900", 0, "N", "", 0, "", "Nil", Session("TransDate"), 0, 0)
            Session("NomTbl") = NomineeTbl

        Catch ex As Exception
            ShowAlertMessage(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub TxtSanctionedAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSanctionedAmt.TextChanged
        Try
            Dim DtLimit As New DataTable
            Dim Dt As New DataTable
            Dim M_CommonBL As New CommonBL
            Calc_InterestRate()
            DtLimit = M_CommonBL.GetCashLimit(Session("DefaultBrCode"))
            If DtLimit.Rows.Count > 0 Then
                If Val(TxtSanctionedAmt.Text) > DtLimit.Rows(0)("Br_CashLimit") And CmbCashTransfer.SelectedItem.Text = "CASH" Then
                    ShowAlertMessage("Change CASH mode to TRANSFER mode.")
                    TxtTransfer.Text = Val(TxtSanctionedAmt.Text) - (DtLimit.Rows(0)("Br_CashLimit"))
                    'TxtSanctionedAmt.Text = DtLimit.Rows(0)("Br_CashLimit")
                    CmbCashTransfer.Focus()
                    'Calc_InterestRate()
                    Set_TransactionGrid()
                    Exit Sub

                End If
            End If
            Dim M_LoanMasterBL As New LoanMasterBL
            If Val(TxtEligibleAmt.Text) < Val(TxtSanctionedAmt.Text.Trim()) Then
                TxtSanctionedAmt.Text = TxtEligibleAmt.Text.Trim()
                ShowAlertMessage("Enter Sanctione Amount Less Then Eligible Amount")
                Exit Sub
            End If

            Dim ParamDt As New DataTable

            ParamDt = M_CommonBL.GetLoanParameter(CmbScheme.SelectedValue)
            If ParamDt.Rows.Count > 0 Then
                If ParamDt.Rows(0)("Lnp_MaxLimit") < Val(TxtSanctionedAmt.Text) Then
                    ShowAlertMessage("Maximum Loan Limit : " & Format(Val(ParamDt.Rows(0)("Lnp_MaxLimit")), "#0.00"))
                    TxtSanctionedAmt.Text = 0
                    Set_TransactionGrid()
                    Exit Sub
                End If
            End If
            Dt = M_LoanMasterBL.LimitSetting(CmbScheme.SelectedValue, HdnMemNo.Value)
            If Dt.Rows.Count > 0 Then
                If Dt.Rows(0)("Amount") >= ParameterDt.Rows(0)("Lnp_MaxLimit") Then
                    'ShowAlertMessage("This Member Have Already Balance " & Format(Val(Dt.Rows(0)("Amount")), "#0.00") & " on " & Dt.Rows(0)(0).ToString & " ")
                    'CmdSave.Enabled = False
                    Exit Sub
                ElseIf ((Dt.Rows(0)("Amount")) + (Val(TxtSanctionedAmt.Text))) > (ParameterDt.Rows(0)("Lnp_MaxLimit")) Then
                    'ShowAlertMessage("Issued Amount is Greater Than Max Loan Limit")
                    'CmdSave.Enabled = False
                    Exit Sub
                End If

            End If
            CmdSave.Enabled = True
            'Calc_InterestRate()
            Set_TransactionGrid()
            Calc_IRR()
            Get_InstalmentDetails()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Set_TransactionGrid()
        Dim M_CommonBL As New CommonBL
        Dim Ds As DataSet
        Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtLoanDt.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "D", "M", Val(TxtSanctionedAmt.Text), Val(HdnCustID.Value), , , Val(TxtTransfer.Text))
        Dim Dt As DataTable = Ds.Tables(0)
        DgvTransaction.DataSource = Dt
        DgvTransaction.DataBind()
        Session("TransTbl") = Dt

        If Ds.Tables(1).Rows.Count > 0 Then
            If Ds.Tables(1).Rows(0)("AlertMsg").ToString.Length > 0 Then
                Dim AlertStr As String
                AlertStr = Ds.Tables(1).Rows(0)("AlertMsg").ToString
                ShowAlertMessage(AlertStr)
            End If
        End If
        Calc_TranTotal()
    End Sub

    Protected Sub CmbCashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.SelectedIndexChanged

        Set_TransactionGrid()
        If CmbCashTransfer.SelectedValue = "TRANSFER" Then
            CheckBox1.Visible = True
            ChkBank.Visible = True

        Else
            ChkBank.Visible = False
            CheckBox1.Visible = False
            DivBankDetail.Visible = False

        End If


    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click

    End Sub

    Protected Sub TxtTotalGrossWt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTotalGrossWt.TextChanged

    End Sub

    Protected Sub TxtTotalNetWt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTotalNetWt.TextChanged
        If Val(TxtTotalNetWt.Text) > Val(TxtTotalGrossWt.Text) Then
            ShowAlertMessage("INVALID WEIGHT ENTERED")
            TxtTotalGrossWt.Focus()
            Exit Sub
        End If

        TxtMarketPrice.Text = Format(Val(TxtTotalNetWt.Text) * Val(TxtMarketRate.Text), "#0.00")
        'TxtEligibleAmt.Text = Format(Val(TxtTotalNetWt.Text) * Val(TxtBankRate.Text), "#0.00")
        'EligibleAmountCalc()
        TxtAppraiserAmt.Text = TxtEligibleAmt.Text
    End Sub
    Private Function EligibleAmountCalc()
        Dim M_CommonBL As New CommonBL
        Dim DtElgbleAmnt As New DataTable
        DtElgbleAmnt = M_CommonBL.GetEligibleAmnt(Val(TxtMarketRate.Text), CmbPurity.SelectedValue, Val(TxtNetWt.Text), CmbScheme.SelectedValue, CmbBranch.SelectedValue)
        M_CommonBL = Nothing
        If DtElgbleAmnt.Rows.Count > 0 Then
            'TxtEligibleAmt.Text = Val(TxtEligibleAmt.Text) + DtElgbleAmnt.Rows(0).Item("ElgAmnt")
            Return DtElgbleAmnt.Rows(0).Item("ElgAmnt")
        End If
    End Function
    Protected Sub TxtIntRate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtIntRate.TextChanged
        Calc_IRR()
    End Sub

    Protected Sub CmbAppraiser_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbAppraiser.SelectedIndexChanged

    End Sub

    Protected Sub DgvOrnaments_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvOrnaments.SelectedIndexChanged

    End Sub

    Protected Sub DgvOrnaments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvOrnaments.RowDeleting
        Dim dt As DataTable = DirectCast(Session("OrnTbl"), DataTable)
        dt.Rows(e.RowIndex).Delete()

        DgvOrnaments.DataSource = dt
        DgvOrnaments.DataBind()
        Session("OrnTbl") = dt

        If dt.Rows.Count <= 0 Then Call Set_Grid()

        Call Calc_OrnTot()
    End Sub

    '    Private Sub DeleteOrnament(ByVal OrnCode As String)
    '        Dim i As Integer
    '        Dim dt As DataTable = DirectCast(Session("OrnTbl"), DataTable)
    '        For i = 0 To DgvOrnaments.Rows.Count - 1
    '            If dt.Rows.Item(i).Item(0).ToString = OrnCode Then
    '                dt.Rows.Item(i).Delete()
    '                GoTo otherow
    '            End If
    '        Next i
    'otherow:
    '        DgvOrnaments.DataSource = dt
    '        DgvOrnaments.DataBind()
    '        Session("OrnTbl") = dt

    '        If dt.Rows.Count <= 0 Then Call Set_Grid()

    '    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        If Len(Request.QueryString("TRANID")) > 0 Then
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
        Else
            Response.Redirect("~/Main.aspx")
        End If
    End Sub


    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("GOLD", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        Dim M_OrnTbl As DataTable
        Dim M_MemTbl As DataTable
        M_TranTbl = M_Dataset.Tables(0)
        M_OrnTbl = M_Dataset.Tables(1)
        M_MemTbl = M_Dataset.Tables(2)
        DgvTransaction.DataSource = M_Dataset.Tables(3)
        Session("TransTbl") = M_Dataset.Tables(3)
        DgvTransaction.DataBind()
        M_CommonBL = Nothing

        GetAccountDtls(M_TranTbl, M_OrnTbl, M_MemTbl)
        PnlAuthorise.Enabled = False
        Calc_TranTotal()

    End Sub

    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable, ByVal M_OrnTbl As DataTable, ByVal M_MemTbl As DataTable)
        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        CmbOpMode.SelectedValue = M_AccTbl.Rows(0).Item("Lm_OperationMode")
        CmbScheme.SelectedValue = M_AccTbl.Rows(0).Item("Sch_Code")
        TxtNoOfOperator.Text = M_AccTbl.Rows(0).Item("NoOfOp")
        TxtLoanDt.Text = M_AccTbl.Rows(0).Item("Lm_LoanDate")
        TxtPrd.Text = M_AccTbl.Rows(0).Item("Lm_Period")
        TxtDueDate.Text = M_AccTbl.Rows(0).Item("Lm_DueDate")
        TxtTotalGrossWt.Text = M_AccTbl.Rows(0).Item("Lm_GrossWt")
        TxtTotalNetWt.Text = M_AccTbl.Rows(0).Item("Lm_NetWt")
        TxtSanctionedAmt.Text = M_AccTbl.Rows(0).Item("Lm_LoanAmount")
        CmbAppraiser.SelectedItem.Text = M_AccTbl.Rows(0).Item("Lm_ApprID")
        CmbSanctionedBy.SelectedValue = M_AccTbl.Rows(0).Item("Lm_SanctionBy")
        TxtIntRate.Text = M_AccTbl.Rows(0).Item("Lm_IntRate")
        TxtPIntRate.Text = M_AccTbl.Rows(0).Item("Lm_PenalRate")
        CmbPurpose.SelectedValue = M_AccTbl.Rows(0).Item("Lm_Mainpurpose")
        CmbSubPurpose.SelectedValue = M_AccTbl.Rows(0).Item("Lm_Subpurpose")
        CmbPriority.SelectedIndex = M_AccTbl.Rows(0).Item("Lm_Priority")
        TxtMarketRate.Text = M_AccTbl.Rows(0).Item("Lm_MarketRate")
        TxtBankRate.Text = M_AccTbl.Rows(0).Item("Lm_BankRate")
        TxtInstAmnt.Text = M_AccTbl.Rows(0).Item("Lm_InstallmentAmount")
        TxtIRR.Text = M_AccTbl.Rows(0).Item("LM_IRR_Rate")
        TxtAdvEmi.Text = M_AccTbl.Rows(0).Item("LM_AdvEMI")
        CmbCanvassedBy.SelectedValue = M_AccTbl.Rows(0).Item("Lm_Introducedby")
        Session("OrnTbl") = M_OrnTbl
        DgvOrnaments.DataSource = M_OrnTbl
        DgvOrnaments.DataBind()

        Session("AccHolderTbl") = M_MemTbl
        DgvCustomer.DataSource = M_MemTbl
        DgvCustomer.DataBind()


        Calc_OrnTot()
    End Sub

    Protected Sub TxtOrnName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtOrnName.TextChanged

    End Sub

    Protected Sub CmbNomReq_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbNomReq.SelectedIndexChanged
        If CmbNomReq.SelectedIndex = 0 Then
            Txt_NomName.Text = ""
            NomDiv.Visible = False
        Else
            If Txt_NomAdd.Text = "" Then
                Txt_NomAdd.Text = DgvCustomer.Rows(0).Cells(7).Text
            End If
            NomDiv.Visible = True
        End If
    End Sub

    Protected Sub But_verfiy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles But_verfiy.Click
        'Try
        '    Dim M_CustMasterBL As New CustMasterBL
        '    Dim M_Datatable As New DataTable
        '    Dim MobileNo As String = TxtMobileNo.Text
        '    M_Datatable = M_CustMasterBL.OTPGenrate(MobileNo)
        '    Session("ODPTBL") = M_Datatable
        '    ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');", True)

        'Catch ex As Exception

        'End Try
        Try
            Dim M_CustMasterBL As New CustMasterBL
            Dim M_Datatable As New DataTable
            Dim MobileNo As String = TxtMobileNo.Text
            M_Datatable = M_CustMasterBL.OTPGenrate(MobileNo)
            Session("ODPTBL") = M_Datatable
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');", True)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub But_verify_Click(ByVal sender As Object, ByVal e As EventArgs) Handles But_verify.Click
        Try
            Dim M_CustMasterBL As CustMasterBL
            Dim OTPNO As String = txt_verifycode.Text
            Dim M_Datatable As New DataTable
            M_Datatable = Session("ODPTBL")

            Dim OTPCODE As String = M_Datatable.Rows(0)("OTP")
            Dim OtpDate As Date = Convert.ToDateTime(M_Datatable.Rows(0)("GenTime"))
            OtpDate = OtpDate.AddMinutes(20)
            Dim Nowdate As Date = Now.ToString()

            If OtpDate >= Nowdate Then
                If OTPNO = OTPCODE Then
                    Hid_ODPStatus.Value = "Y"
                    Session("ODPTBL") = Nothing
                    ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('cover');hide('VerifyMob');hide('div_Verify');hide('imgverifyError');show('imgVerify');hide('lbl_sessionexp');hide('lbl_errMsg');", True)
                Else
                    ' ErrorMessage("Enter Correct OTP Code")
                    ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');hide('lbl_sessionexp');show('lbl_errMsg');", True)
                End If
            Else
                Session("ODPTBL") = Nothing
                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');hide('div_Verify');show('imgverifyError');hide('imgVerify');show('lbl_sessionexp');hide('lbl_errMsg');", True)
            End If

            Hid_ODPStatus.Value = "Y"
            Session("ODPTBL") = Nothing
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('cover');hide('VerifyMob');hide('div_Verify');hide('imgverifyError');show('imgVerify');hide('lbl_sessionexp');hide('lbl_errMsg');", True)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TxtPrd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtPrd.TextChanged
        Calc_InterestRate()
        Dim DueDays As Double
        If HdnPrdType.Value = "M" Then
            DueDays = Val(TxtPrd.Text) * 30
            TxtDueDate.Text = DateAdd(DateInterval.Day, DueDays, CDate(TxtLoanDt.Text))
        Else
            TxtDueDate.Text = DateAdd(DateInterval.Day, Val(TxtPrd.Text), CDate(TxtLoanDt.Text))
        End If
        Calc_IRR()
        Get_InstalmentDetails()
    End Sub

    Protected Sub TxtTransfer_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTransfer.TextChanged
        If Val(TxtTransfer.Text) > Val(TxtSanctionedAmt.Text) Then
            ShowAlertMessage("Invalid Transfer Amount Entered")
            TxtTransfer.Text = ""
            Exit Sub
        End If
        Set_TransactionGrid()
    End Sub

    Protected Sub btnEMI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEMI.Click
        Dim CommBL As New CommonBL
        'Emi_IRRParameter = CommBL.Get_IRR(CmbScheme.SelectedValue)
        Dim InstDt As Date = Convert.ToDateTime(Session("TransDate"))
        Dim LoanAmnt As Double = Val(TxtSanctionedAmt.Text)
        Dim Dt As DataTable = CommBL.Get_EMITable(LoanAmnt - (Val(TxtAdvEmi.Text) * Val(TxtInstAmnt.Text)), Val(TxtIRR.Text), (Val(TxtPrd.Text) - Val(TxtAdvEmi.Text)), InstDt, CmbScheme.SelectedValue, 0, HdnPrdType.Value, Val(TxtAdvEmi.Text), Session("TransDate"))
        CommBL = Nothing
        Session("EMITbl") = Dt
        Dim PrEMI As New Print
        PrEMI.ShowPrintDialog("LoanEMIChart.aspx?AccID=999999999", "_blank", "menubar=0,width=750,height=600,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes")
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim Ds As DataSet
            If CheckBox1.Checked = True Then
                Ds = M_CommonBL.Set_TransactionProcGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtLoanDt.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "D", "M", Val(TxtSanctionedAmt.Text), Val(HdnCustID.Value), , , Val(TxtTransfer.Text))
                Dim Dt As DataTable = Ds.Tables(0)
                DgvTransaction.DataSource = Dt
                DgvTransaction.DataBind()
                Session("TransTbl") = Dt
            Else
                Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtLoanDt.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "D", "M", Val(TxtSanctionedAmt.Text), Val(HdnCustID.Value), , , Val(TxtTransfer.Text))
                Dim Dt As DataTable = Ds.Tables(0)
                DgvTransaction.DataSource = Dt
                DgvTransaction.DataBind()
                Session("TransTbl") = Dt
            End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ChkBank_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkBank.CheckedChanged
        If ChkBank.Checked = True Then
            DivBankDetail.Visible = True
        Else
            DivBankDetail.Visible = False
        End If
    End Sub

    Protected Sub drpTranferType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpTranferType.SelectedIndexChanged
        If drpTranferType.SelectedItem.Text = "Cheque" Then
            dvRTGS.Visible = False
        Else
            dvRTGS.Visible = True
        End If
    End Sub
    Private Sub drpBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpBank.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
    End Sub
    Protected Sub Calc_IRR()
        Try
            If (Val(TxtSanctionedAmt.Text) - Val(TxtTransfer.Text)) > 0 And Val(TxtPrd.Text) > 0 And Val(TxtIntRate.Text) > 0 And ViewState("Lnp_EMIType") = "Y" Then
                Dim M_CommBL As New CommonBL
                Dim Dt As New DataTable
                Dim LoanAmnt As Double = Val(TxtSanctionedAmt.Text)
                Dt = M_CommBL.Get_Loan_IRR(LoanAmnt, Val(TxtIntRate.Text), Val(TxtPrd.Text), HdnPrdType.Value, Val(TxtPrd.Text), Val(TxtAdvEmi.Text))
                TxtIRR.Text = Dt.Rows(0).Item("IRR_Rate")
            Else
                TxtIRR.Text = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Function Get_InstalmentDetails() As DataTable
        Try
            Dim CommBL As New CommonBL
            Dim M_LoanMasterBl As New LoanMasterBL
            Dim M_Ds As New DataSet
            Dim M_Dt As New DataTable
            Dim M_Dt1 As New DataTable
            Dim InstDt As Date = Convert.ToDateTime(Session("TransDate"))
            Dim LoanAmnt As Double = Val(TxtSanctionedAmt.Text)
            Dim PrdType As String
            PrdType = HdnPrdType.Value

            If ViewState("Lnp_EMIType") = "Y" Then
                Dim A As Double
                Dim B As Double
                Dim C As Double
                A = (LoanAmnt * Val(TxtIntRate.Text) * Val(TxtPrd.Text))
                If PrdType = "M" Then
                    B = 1200
                ElseIf PrdType = "D" Then
                    B = 36500
                ElseIf PrdType = "W" Then
                    B = 36500
                End If
                C = Math.Round(((A / B) + (LoanAmnt)) / Val(TxtPrd.Text), 0)
                If Val(TxtIntRate.Text) > 0 And LoanAmnt > 0 And Val(TxtPrd.Text) > 0 Then
                    TxtInstAmnt.Text = C.ToString()
                End If
            Else
                TxtInstAmnt.Text = Math.Round((LoanAmnt) / Val(TxtPrd.Text), 0)
            End If
        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    'Protected Sub btn_Upload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Upload.Click
    '    If FileUpload4.HasFile = False Then
    '        ShowAlertMessage("Invalid File Uploaded ")
    '        Exit Sub
    '    End If
    '    If FileUpload4.PostedFile IsNot Nothing Then
    '        Dim path As String
    '        ViewState("OrnamentPath") = Nothing
    '        Dim ImgIdentitybyte As Byte() = New Byte(-1) {}
    '        Dim dt As New DataTable
    '        Dim converter As New ImageConverter()
    '        dt = DirectCast(Session("tran_dt"), DataTable)
    '        If dt Is Nothing Then
    '            Set_EmptyDataTable()
    '            dt = DirectCast(Session("tran_dt"), DataTable)
    '        End If
    '        Dim FileName As String = System.IO.Path.GetFileName(FileUpload4.PostedFile.FileName)
    '        FileName = Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + FileName
    '        'Save files to images folder
    '        'FileUpload4.SaveAs(Server.MapPath(Convert.ToString("~/Photos/") & FileName))
    '        Me.ImgPhoto.ImageUrl = Convert.ToString("~/Photos/") & FileName
    '        Dim FName As String = FileUpload4.FileName
    '        FileUpload4.PostedFile.SaveAs(Server.MapPath("~\Photos\" + FileName.Trim()))
    '        ViewState("OrnamentPath") = "~\Photos\" + FileName.Trim()
    '        ImageID.Value = "Y"
    '        path = "~\Photos\" + FileName.Trim()
    '        Dim ext As String = System.IO.Path.GetExtension(FileName)
    '        Dim FileType As String = String.Empty

    '        'Set the contenttype based on File Extension
    '        Select Case ext
    '            'Case ".doc"
    '            '    FileType = "application/vnd.ms-word"
    '            '    Exit Select
    '            'Case ".docx"
    '            '    FileType = "application/vnd.ms-word"
    '            '    'FileType = "application/vnd.openxmlformats-officedocument.word"
    '            '    Exit Select
    '            'Case ".xls"
    '            '    FileType = "application/vnd.ms-excel"
    '            '    Exit Select
    '            'Case ".xlsx"
    '            '    FileType = "application/vnd.ms-excel"
    '            '    Exit Select
    '            Case ".jpg"
    '                FileType = "image/jpg"
    '                Exit Select
    '            Case ".png"
    '                FileType = "image/png"
    '                Exit Select
    '            Case ".gif"
    '                FileType = "image/gif"
    '                Exit Select
    '                'Case ".pdf"
    '                '    FileType = "application/pdf"
    '                '    Exit Select
    '        End Select
    '        Dim Imge1 As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(path))
    '        ImgIdentitybyte = DirectCast(converter.ConvertTo(Imge1, ImgIdentitybyte.[GetType]()), Byte())
    '        Dim Expression As String = ""
    '        Dim Row As DataRow
    '        Row = dt.NewRow
    '        Row("Id") = 1
    '        'Row("txt_DocDetails") = DdlPhotoType.SelectedItem.Text
    '        Row("Doc_Typ") = ext
    '        Row("Doc_Name") = FileName
    '        Row("FileType") = FileType
    '        Row("FileData") = ImgIdentitybyte
    '        Row("FilePath") = path
    '        'Row("DocUploadType") = DdlPhotoType.SelectedValue
    '        dt.Rows.Add(Row)
    '        Expression = "Id=0"
    '        If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
    '        GridView1.DataSource = dt
    '        GridView1.DataBind()
    '        Session("tran_dt") = Nothing
    '        Session("tran_dt") = dt
    '        'ImgPhoto.ImageUrl = Nothing
    '    End If
    'End Sub
    Protected Sub btn_Upload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Upload.Click
        If FileUpload4.HasFile = False Then
            ShowAlertMessage("Invalid Ornament Uploaded ")
            Exit Sub
        End If
        If FileUpload4.PostedFile IsNot Nothing Then

            ViewState("OrnamentPath") = Nothing

            Dim FileName As String = System.IO.Path.GetFileName(FileUpload4.PostedFile.FileName)
            FileName = Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + FileName
            'Save files to images folder
            'FileUpload4.SaveAs(Server.MapPath(Convert.ToString("~/Photos/") & FileName))
            Me.ImgPhoto.ImageUrl = Convert.ToString("~/Photos/") & FileName
            Dim FName As String = FileUpload4.FileName
            FileUpload4.PostedFile.SaveAs(Server.MapPath("~\Photos\" + FileName.Trim()))
            ViewState("OrnamentPath") = "~\Photos\" + FileName.Trim()
            ImageID.Value = "Y"
            '  path = "~\Photos\" + FileName.Trim()

        End If
    End Sub
    Protected Sub btn_Upload2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Upload2.Click
        If FileUpload5.HasFile = False Then
            ShowAlertMessage("Invalid Weight Uploaded ")
            Exit Sub
        End If
        If FileUpload5.PostedFile IsNot Nothing Then

            ViewState("OrnamentPath2") = Nothing

            Dim FileName As String = System.IO.Path.GetFileName(FileUpload5.PostedFile.FileName)
            FileName = Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + FileName
            'Save files to images folder
            'FileUpload4.SaveAs(Server.MapPath(Convert.ToString("~/Photos/") & FileName))
            Me.ImgPhoto2.ImageUrl = Convert.ToString("~/Photos/") & FileName
            Dim FName As String = FileUpload5.FileName
            FileUpload5.PostedFile.SaveAs(Server.MapPath("~\Photos\" + FileName.Trim()))
            ViewState("OrnamentPath2") = "~\Photos\" + FileName.Trim()
            ImageID2.Value = "Y"
            '  path = "~\Photos\" + FileName.Trim()

        End If
    End Sub
    Private Sub Set_EmptyDataTable()
        Try
            Dim Tbl_SetEmpty As New DataTable("Table")
            Tbl_SetEmpty.Columns.Add("Id", GetType(Integer))
            Tbl_SetEmpty.Columns.Add("txt_DocDetails", GetType(String))
            Tbl_SetEmpty.Columns.Add("Doc_Typ", GetType(String))
            Tbl_SetEmpty.Columns.Add("Doc_Name", GetType(String))
            Tbl_SetEmpty.Columns.Add("FileType", GetType(String))
            Tbl_SetEmpty.Columns.Add("FileData", GetType(Byte()))
            Tbl_SetEmpty.Columns.Add("FilePath", GetType(String))
            Tbl_SetEmpty.Columns.Add("DocUploadType", GetType(Integer))
            Tbl_SetEmpty.Rows.Add(0, "", "", "", "", 0, "", 0)
            Session("tran_dt") = Tbl_SetEmpty
        Catch ex As Exception
        End Try
    End Sub
    Private Sub UpLoadImageFile(ByVal ACC_NO As String)
        Try
            Dim objConn As SqlConnection
            Dim objCom As SqlCommand
            Try
                Dim converter As New ImageConverter()
                objConn = New SqlConnection(ConfigurationManager.AppSettings("SQL_CONNSTR").ToString())
                Dim imgPath As String
                Dim ImageDB As String = ConfigurationManager.AppSettings("ImageDB").ToString()
                If ViewState("OrnamentPath") <> "" Then
                    imgPath = ViewState("OrnamentPath")
                    Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))
                    Dim imgBytes As Byte() = New Byte(-1) {}
                    imgBytes = DirectCast(converter.ConvertTo(Imge, imgBytes.[GetType]()), Byte())
                    Dim InsQry As String

                    InsQry = " INSERT INTO " + ImageDB + ".dbo.OrnamentImage_M_Tbl SELECT [dbo].[Get_AccID_Fn]('" + ACC_NO + "'),@Picture"
                    ''InsQry = " UPDATE  Gold_Ornaments_M_Tbl SET  imagedetails = @Picture WHERE  Acc_ID=[dbo].[Get_AccID_Fn]('" + ACC_NO + "')"
                    objCom = New SqlCommand(InsQry, objConn)
                    Dim pictureParameter As SqlParameter = New SqlParameter("@Picture", SqlDbType.Image)
                    pictureParameter.Value = imgBytes
                    objCom.Parameters.Add(pictureParameter)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    ViewState("OrnamentPath") = ""
                End If
                If ViewState("OrnamentPath2") <> "" Then
                    imgPath = ViewState("OrnamentPath2")
                    Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))
                    Dim imgBytes As Byte() = New Byte(-1) {}
                    imgBytes = DirectCast(converter.ConvertTo(Imge, imgBytes.[GetType]()), Byte())
                    Dim InsQry As String

                    InsQry = " INSERT INTO " + ImageDB + ".dbo.OrnamentImage_M_Tbl2 SELECT [dbo].[Get_AccID_Fn]('" + ACC_NO + "'),@Picture"
                    ''InsQry = " UPDATE  Gold_Ornaments_M_Tbl SET  imagedetails = @Picture WHERE  Acc_ID=[dbo].[Get_AccID_Fn]('" + ACC_NO + "')"
                    objCom = New SqlCommand(InsQry, objConn)
                    Dim pictureParameter As SqlParameter = New SqlParameter("@Picture", SqlDbType.Image)
                    pictureParameter.Value = imgBytes
                    objCom.Parameters.Add(pictureParameter)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    ViewState("OrnamentPath2") = ""
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
                objConn.Close()
            End Try
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    'Private Sub UploadMultiImageFile(ByVal ACC_NO As String)

    '    Try
    '        Dim objConn As SqlConnection
    '        Dim objCom As SqlCommand
    '        objConn = New SqlConnection(ConfigurationManager.AppSettings("SQL_CONNSTR").ToString())
    '        Dim ImageDB As String = ConfigurationManager.AppSettings("ImageDB").ToString()
    '        Dim converter As New ImageConverter()
    '        Dim M_DocumentMasterBL As New DocumentMasterBL
    '        Dim todaysdate As String = String.Format("{0:MM/dd/yyyy}", DateTime.Now)
    '        Dim M_Dt As DataTable = DirectCast(Session("tran_dt"), DataTable)
    '        Dim Expression As String = ""
    '        Expression = "Id=0"
    '        If M_Dt.[Select](Expression).Count > 0 Then M_Dt.Rows.RemoveAt(M_Dt.Rows.IndexOf(M_Dt.[Select](Expression)(0)))

    '        Dim imgPath As String
    '        Dim Doc_Typ, txt_DocDetails, FileType, DocName, UploadDocType As String
    '        Dim M_CustMasterBL As New CustMasterBL

    '        If M_Dt.Rows.Count > 0 Then
    '            'M_CustMasterBL.Delete_Cust_Docuplod(CustID)
    '            For i = 0 To M_Dt.Rows.Count - 1
    '                Doc_Typ = M_Dt.Rows(i)(2).ToString()
    '                txt_DocDetails = M_Dt.Rows(i)(2).ToString()
    '                DocName = M_Dt.Rows(i)(3).ToString()
    '                FileType = M_Dt.Rows(i)(4).ToString()
    '                imgPath = M_Dt.Rows(i)(6).ToString()
    '                UploadDocType = M_Dt.Rows(i)(7).ToString()

    '                Dim imgBytes As Byte() = New Byte(-1) {}
    '                Dim File_Data As Byte()
    '                If FileType = "application/pdf" Then
    '                    ' Dim str As Stream = fileUpEx.PostedFile.InputStream
    '                    Dim fs As Stream = New FileStream(Server.MapPath(imgPath), FileMode.Open)
    '                    Dim br As New BinaryReader(fs)
    '                    File_Data = br.ReadBytes(DirectCast((fs.Length), Long))
    '                Else
    '                    Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))

    '                    imgBytes = M_Dt.Rows(i)(5)
    '                    imgBytes = DirectCast(converter.ConvertTo(Imge, imgBytes.[GetType]()), Byte())
    '                End If

    '                'M_CustMasterBL.Insert_Cust_Docuplod(0, 0, CustID, Doc_Typ, txt_DocDetails, DocName, FileType, IIf(FileType = "application/pdf", File_Data, imgBytes), imgPath, Session("UserID"), UploadDocType)
    '                Dim InsQry As String
    '                If i = 0 Then
    '                    InsQry = " INSERT INTO " + ImageDB + ".dbo.OrnamentImage_M_Tbl SELECT [dbo].[Get_AccID_Fn]('" + ACC_NO + "'),@Picture"
    '                ElseIf i = 1 Then 'Insert second image in a separate table to avoid confusion in gold bond
    '                    InsQry = " INSERT INTO " + ImageDB + ".dbo.OrnamentImage_M_Tbl2 SELECT [dbo].[Get_AccID_Fn]('" + ACC_NO + "'),@Picture,@DocName"
    '                End If

    '                ''InsQry = " UPDATE  Gold_Ornaments_M_Tbl SET  imagedetails = @Picture WHERE  Acc_ID=[dbo].[Get_AccID_Fn]('" + ACC_NO + "')"
    '                objCom = New SqlCommand(InsQry, objConn)
    '                Dim pictureParameter As SqlParameter
    '                pictureParameter = New SqlParameter("@Picture", SqlDbType.Image)
    '                pictureParameter = New SqlParameter("@DocName", SqlDbType.NVarChar)
    '                pictureParameter.Value = imgBytes
    '                pictureParameter.Value = DocName
    '                objCom.Parameters.Add(pictureParameter)
    '                objConn.Open()
    '                objCom.ExecuteNonQuery()
    '                objConn.Close()
    '                ViewState("OrnamentPath") = ""
    '            Next
    '        End If
    '        Session("Tran_Dt") = Nothing
    '    Catch ex As Exception

    '        ShowAlertMessage("Data Upload Failed")
    '    End Try
    'End Sub
    Private Sub Load_Img(TranId)
        Dim M_Qry As String
        Dim M_CommonBL As New CommonBL
        Dim M_Dt As New DataSet
        Try
            M_Dt = M_CommonBL.Get_OrnamentImgDet(TranId, 0)

            If M_Dt.Tables.Count > 0 Then
                Dim Tbl1 As DataTable = M_Dt.Tables(0)
                Dim Tbl2 As DataTable = M_Dt.Tables(1)
                If Tbl1.Rows.Count > 0 Then

                    'Dim dt As DataTable = DirectCast(Session("OrnImgTbl"), DataTable)
                    Dim bytes As Byte() = DirectCast(Tbl1.Rows(0).Item("ImageDetails"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    ImgPhoto.ImageUrl = Convert.ToString("data:image/png;base64,") & base64String
                End If

                If Tbl2.Rows.Count > 0 Then
                    Dim bytes2 As Byte() = DirectCast(Tbl2.Rows(0).Item("ImageDetails"), Byte())
                    Dim base64String2 As String = Convert.ToBase64String(bytes2, 0, bytes2.Length)
                    ImgPhoto2.ImageUrl = Convert.ToString("data:image/png;base64,") & base64String2
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class