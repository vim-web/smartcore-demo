Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data
Partial Public Class Transaction
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Dim AcHeadDt1 As DataTable
    Shared BranchDt As DataTable
    Shared Dt_AcDtls As DataTable
    Shared drParam As DataRow()
    Dim AlertDt As DataTable
    Dim FormID As Integer = 1144
    Shared Obj_UserRights As Object
    Private Sub Page_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

    End Sub

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Session("UserID") = "" Then
                    Response.Redirect("~/SmartLogin.aspx")
                End If
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                Load_DefaultValues()
                'TxtInstrDate.Attributes.Add("readonly", "readonly")
                TxtRenewIntAmt.Attributes.Add("readonly", "readonly")
                TxtRenewPrnAmt.Attributes.Add("readonly", "readonly")
                TxtRenewDueDate.Attributes.Add("readonly", "readonly")
                TxtRenewEffDate.Attributes.Add("readonly", "readonly")
                lblModeOp.Attributes.Add("readonly", "readonly")
                Session("SearchType") = "ACCNO"
                HdnTrDate.Value = Session("TransDate")
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                ImgTrAccNo.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                If CmbTransType.SelectedIndex = 1 Then
                    Session("SearchType") = "CUSTOMER"
                End If

                ImgSrch0.Attributes.Add("OnClick", "window.open('Srch.aspx?SrType=CUST','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            End If

            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "ACCNO" Then
                    TxtAccNo.Text = Session("SearchResult")
                    TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
                End If

                If Session("SearchType") = "TRACCOUNTNO" Then
                    TxtTrfrAcNo.Text = Session("SearchResult")
                    TxtTrfrAcNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
                End If

            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "CUSTOMER" Then
                    TxtCustID.Text = Session("SearchResult")
                    TxtCustID_TextChanged(Me.TxtCustID, System.EventArgs.Empty)
                    TxtCustID.Focus()
                End If
                Session("SearchResult") = ""
            End If
            If Len(Request.QueryString("TRANID")) > 0 Then
                SchemeDt = DirectCast(Cache("SchemeTbl"), DataTable)
                CommonClass.Fill_Combo(CmbScheme, SchemeDt, "Sch_Name", "Sch_Code")
                CommonClass.Fill_Combo(CmbBranch, BranchDt, "Br_Name", "Br_Code")
                GetAuthoriseDtls(Request.QueryString("TRANID"))
                If Len(Request.QueryString("DEL")) > 0 Then

                    CmdSave.Visible = False
                    CmdClear.Visible = False
                    cmdDelete.Visible = True


                End If
            End If



        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Private Sub Load_DefaultValues()
        Try
            Session("BatchNo") = ""
            Session("Tran_No") = ""
            Session("Acc_No") = ""
            Dim M_CommonBL As New CommonBL

            M_CommonBL.Fill_Section(CmbSection)
            If CmbSection.Items.Count > 0 Then CmbSection.SelectedIndex = 1
            Call cmbsection_SelectedIndexChanged(Me.CmbSection, System.EventArgs.Empty)

            AlertDt = New DataTable
            AlertDt.Columns.Add("Type", GetType(String))
            AlertDt.Columns.Add("Msg", GetType(String))
            Session("AlertDt") = AlertDt

            CmbCashTransfer.Items.Add("Cash")
            CmbCashTransfer.Items.Add("Transfer")
            CmbCashTransfer.SelectedIndex = 0

            CmbTransType.Items.Add("Account No")
            CmbTransType.Items.Add("Account Head")
            CmbTransType.SelectedIndex = 0

            CmbSchTrans.Items.Add("Credit")
            CmbSchTrans.Items.Add("Debit")
            CmbSchTrans.SelectedIndex = 0

            CmbAcheadTran.Items.Add("Credit")
            CmbAcheadTran.Items.Add("Debit")
            CmbAcheadTran.SelectedIndex = 0


            M_CommonBL.Fill_Branch(CmbBranch, False, True)
            M_CommonBL.Fill_Branch(DrpToBrnch, False, True) '''for interbranch general voucher by gowtham


            ''------------Bank Details By Gowtham
            M_CommonBL.Fill_Pickup(drpTranferType, 108, 0)
            M_CommonBL.GetBranchBankDetails(drpBank, Session("DefaultBrCode").ToString())
            M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
            DdlTranType.Items.Add("<--Select-->")
            DdlTranType.Items.Add("Credit")
            DdlTranType.Items.Add("Debit")
            DdlTranType.SelectedIndex = 0
            dvRTGS.Visible = False
            ''------------Bank Details By Gowtham

            DrpToBrnch.SelectedValue = Session("DefaultBrCode")  ''''for interbranch general voucher by gowtham
            If (CmbTransType.SelectedIndex = 0) Then
                LblToBrnch.Visible = False
                DrpToBrnch.Visible = False
                LblCustNo.Visible = False
                LblCustName.Visible = False
                TxtCustID.Visible = False
                TxtCustName.Visible = False
                ImgSrch0.Visible = False

            End If
            If (CmbTransType.SelectedIndex = 1) Then
                LblCustNo.Visible = True
                LblCustName.Visible = True
                TxtCustID.Visible = True
                TxtCustName.Visible = True
                ImgSrch0.Visible = True
                DrpToBrnch.Visible = True
            End If
            'M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
            'sanoj
            ' SchemeDt = DirectCast(Cache("SchemeTbl"), DataTable)
            ' CommonClass.Fill_Combo(CmbScheme, SchemeDt, "Sch_Name", "Sch_Code")
            'sanoj
            'CommonClass.Fill_Combo(CmbBranch, BranchDt, "Br_Name", "Br_Code")

            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
            M_CommonBL.Fill_Branch(CmbMemBranch)
            M_CommonBL.GetSchemes(CmbMemType, Session("TransDate"), "SHARE", "SHARE")

            M_CommonBL.Fill_Branch(CmbStandBr)
            M_CommonBL.GetSchemes(CmbStandSch, Session("TransDate"), "DEPOSIT", "SB")
            M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)
            M_CommonBL.Fill_Pickup(CmbRenewIntPayMode, 44, 0)
            M_CommonBL.Fill_Pickup(CmbRenewPrivilage, 42, 0)
            M_CommonBL.Fill_BatchNo(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo)
            CmbBatchNo.SelectedIndex = 0

            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)


            ' AcHeadDt = DirectCast(Cache("Achead"), DataTable)

            Dim AccHeadTemp As DataTable
            If HttpContext.Current.Session("ASSIGNEDBRANCH") = "%" Then
                AccHeadTemp = DirectCast(Cache("Achead"), DataTable)
                Dim Rows() As DataRow = AccHeadTemp.Select(" Acc_Visible='Y'")
                If Rows.Length > -1 Then
                    AcHeadDt1 = Rows.CopyToDataTable()
                End If

            Else
                AccHeadTemp = DirectCast(Cache("Achead"), DataTable)
                Dim Rows() As DataRow = AccHeadTemp.Select("Acc_HoAc='N' AND Acc_Visible='Y'")
                If Rows.Length > -1 Then
                    AcHeadDt1 = Rows.CopyToDataTable()
                End If

                Dim DataView1 As New DataView
                DataView1 = AccHeadTemp.DefaultView()
                DataView1.RowFilter = "Acc_Branch = '" & Session("DefaultBrCode") & "'"
                For I = 0 To DataView1.Count - 1
                    AcHeadDt1.ImportRow(DataView1.Item(I).Row)
                Next I

            End If
            Session("AccountHeadTbl") = AcHeadDt1

            LblRctCashTotal.Text = "0.00"
            LblRctTransferTotal.Text = "0.00"
            LblPayCashTotal.Text = "0.00"
            LblPayTransferTotal.Text = "0.00"

            CmbRenewType.Items.Add("PRINCIPLE + INTEREST") ' With Interest
            CmbRenewType.Items.Add("PRINCIPLE ONLY")
            CmbRenewType.Items.Add("WITH ADDITIONAL AMOUNT")
            CmbRenewType.SelectedIndex = 0

            CmbInstType.Items.Add("-WS-")
            CmbInstType.Items.Add("Book")
            CmbInstType.Items.Add("Loose")
            CmbInstType.SelectedIndex = 0

            CmbNomReq.Items.Add("No")
            CmbNomReq.Items.Add("Yes")
            CmbNomReq.SelectedIndex = 0

            CmbBranch.SelectedValue = Session("DefaultBrCode")
            CmbStandBr.SelectedValue = Session("DefaultBrCode")
            CmbMemBranch.SelectedValue = Session("DefShareBranch")
            Session("SearchBranch") = Session("DefaultBrCode")


            M_CommonBL.Fill_Branch(CmbTrfrBr)
            M_CommonBL.GetSchemes(CmbTrfrSch, Session("TransDate"), "DEPOSIT','SHARE','BANKACCOUNT", "SB','CA','SHARE','BANKACCOUNT")
            CmbTrfrBr.SelectedValue = Session("DefaultBrCode")

            LblCredit.Text = ""
            LblDebit.Text = ""
            M_CommonBL = Nothing
            cmdDelete.Visible = False

            TxtMobileNo.Visible = False
            But_verfiy.Visible = False
            lblmobno.Visible = False
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub CmbTransType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbTransType.SelectedIndexChanged
        Try

            If CmbTransType.SelectedIndex = 0 Then
                AccNoDiv.Visible = True
                AcHeadDiv.Visible = False
                CmbCashTransfer.Visible = True
                LblCashTransfer.Visible = True
                DrpToBrnch.Visible = False
                LblToBrnch.Visible = False
                LblCustNo.Visible = False
                LblCustName.Visible = False
                TxtCustID.Visible = False
                TxtCustName.Visible = False
                ImgSrch0.Visible = False
                Session("SearchType") = "ACCNO"
            Else
                AccNoDiv.Visible = False
                AcHeadDiv.Visible = True
                CmbCashTransfer.Visible = False
                LblCashTransfer.Visible = False
                TrfrDiv.Visible = False
                CmbBranch.SelectedValue = Session("DefaultBrCode")
                Dim locAcHeadTbl As DataTable
                Dim M_CommonBL As New CommonBL
                locAcHeadTbl = M_CommonBL.Fill_VoucherHeads()
                DgvTransaction.DataSource = locAcHeadTbl
                Session("locAcHeadTbl") = locAcHeadTbl
                DgvTransaction.DataBind()
                M_CommonBL = Nothing
                DrpToBrnch.Visible = True
                LblToBrnch.Visible = True
                LblCustNo.Visible = True
                LblCustName.Visible = True
                TxtCustID.Visible = True
                TxtCustName.Visible = True
                ImgSrch0.Visible = True
                Session("SearchType") = "CUSTOMER"
            End If

        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub
    Private Sub Set_SchemeCtrl()

        Try
            DivShare.Visible = False
            DivResl.Visible = False
            DivSuspense.Visible = False
            DivInstl.Visible = False
            DivShare.Visible = False
            Dim locAcHeadTbl As DataTable
            Dim M_CommonBL As New CommonBL
            Dim Expression As String

            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
            locAcHeadTbl = M_CommonBL.Fill_SchemeHeads(CmbScheme.SelectedValue, IIf(CmbSchTrans.SelectedIndex = 0, "R", "P"))

            DgvTransaction.DataSource = locAcHeadTbl
            Session("locAcHeadTbl") = locAcHeadTbl
            DgvTransaction.DataBind()
            Session("SearchScheme") = CmbScheme.SelectedValue
            Session("SearchBranch") = CmbBranch.SelectedValue

            Dim TempDt As DataTable
            TempDt = M_CommonBL.Get_SchemeDtls(CmbScheme.SelectedValue)

            SchSection.Value = TempDt.Rows(0).Item("Sch_Section_Name").ToString
            If SchSection.Value = "SHARE" Then
                CmbSection.SelectedIndex = "3"
            ElseIf SchSection.Value = "BANK" Then
                CmbSection.SelectedIndex = "0"
            ElseIf SchSection.Value = "DEPOSIT" Then
                CmbSection.SelectedIndex = "1"
            ElseIf SchSection.Value = "LOAN" Then
                CmbSection.SelectedIndex = "2"
            ElseIf SchSection.Value = "INVESTMENT" Then
                CmbSection.SelectedIndex = "4"
            ElseIf SchSection.Value = "BORROWINGS" Then
                CmbSection.SelectedIndex = "4"
            ElseIf SchSection.Value = "TOURS & TRAVELS" Then
                CmbSection.SelectedIndex = "5"
            Else
                CmbSection.SelectedIndex = "4"
            End If
            SchAchead.Value = TempDt.Rows(0).Item("Sch_Name").ToString()
            SchModuleCatg.Value = TempDt.Rows(0).Item("Sch_Module_Catg").ToString()

            HdnChqValidityPeriod.Value = 0
            HdnChqValidityPeriodType.Value = "M" ' To Do

            If (SchSection.Value = "LOAN" And SchModuleCatg.Value = "OD" Or SchModuleCatg.Value = "CC") Or (SchSection.Value = "DEPOSIT" And SchModuleCatg.Value = "SB") Or (SchSection.Value = "DEPOSIT" And SchModuleCatg.Value = "CA") Then
                CmbInstType.Enabled = True
                TxtInstrNo.Enabled = True
                TxtInstrDate.Enabled = True
            Else
                CmbInstType.Enabled = False
                TxtInstrNo.Enabled = False
                TxtInstrDate.Enabled = False
            End If
            CmbSchTrans.Items.Clear()
            DivInstr.Visible = False
            If SchSection.Value = "DEPOSIT" Then
                Select Case SchModuleCatg.Value.ToUpper
                    Case "FD"
                        CmbSchTrans.Items.Add("Interest Payment")
                        CmbSchTrans.Items.Add("Renewal")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                    Case "RIS"
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.Items.Add("Renewal")
                        CmbSchTrans.SelectedIndex = 0
                    Case "RD"
                        CmbSchTrans.Items.Add("Deposit")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                        DivInstl.Visible = True
                    Case Else
                        CmbSchTrans.Items.Add("Credit")
                        CmbSchTrans.Items.Add("Debit")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                        DivInstr.Visible = True
                End Select

                drParam = M_CommonBL.GetDepositParameter(CmbScheme.SelectedValue).[Select]()
                If drParam.Count > 0 Then HdnChqValidityPeriod.Value = drParam(0)("Dep_ChqValidityDays").ToString

            ElseIf SchSection.Value = "INVESTMENT" Then
                Select Case SchModuleCatg.Value.ToUpper
                    Case "FD"
                        CmbSchTrans.Items.Add("Interest Receipt")
                        ' CmbSchTrans.Items.Add("Renewal")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                    Case "RIS"
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                    Case "RD"
                        CmbSchTrans.Items.Add("Deposit")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                        DivInstl.Visible = True
                    Case Else
                        CmbSchTrans.Items.Add("Credit")
                        CmbSchTrans.Items.Add("Debit")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                        DivInstr.Visible = True
                End Select

                drParam = M_CommonBL.GetDepositParameter(CmbScheme.SelectedValue).[Select]()
                If drParam.Count > 0 Then HdnChqValidityPeriod.Value = drParam(0)("Dep_ChqValidityDays").ToString
            ElseIf SchSection.Value = "SHARE" Then
                CmbSchTrans.Items.Add("Credit")
                CmbSchTrans.Items.Add("Debit")
                CmbSchTrans.Items.Add("Closure")
                CmbSchTrans.Items.Add("Divident Payment")
                CmbSchTrans.SelectedIndex = 0
                DivShare.Visible = True
                DivResl.Visible = True
                Expression = "Sch_Code = '" & CmbScheme.SelectedValue & "'"
                drParam = DirectCast(Cache("ShareParamTbl"), DataTable).[Select](Expression, "")

            ElseIf SchSection.Value = "GROUP DEPOSIT" Then
                CmbSchTrans.Items.Add("Credit")
                CmbSchTrans.Items.Add("Debit")
                CmbSchTrans.Items.Add("Closure")
                CmbSchTrans.SelectedIndex = 0
                DivInstl.Visible = True
                DivResl.Visible = True
            ElseIf SchSection.Value = "LOAN" Then
                Select Case SchModuleCatg.Value.ToUpper
                    Case "GOLDLOAN", "DEP.LOAN"
                        CmbSchTrans.Items.Add("Receipt")
                        CmbSchTrans.Items.Add("Closure")
                        'CmbSchTrans.SelectedIndex = 1
                        CmbSchTrans.SelectedIndex = 0
                    Case "GEN.LOAN"
                        CmbSchTrans.Items.Add("Receipt")
                        CmbSchTrans.Items.Add("Payment")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                    Case "OD"
                        CmbSchTrans.Items.Add("Credit")
                        CmbSchTrans.Items.Add("Debit")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                        DivInstr.Visible = True
                    Case "CC"
                        CmbSchTrans.Items.Add("Credit")
                        CmbSchTrans.Items.Add("Debit")
                        CmbSchTrans.Items.Add("Closure")
                        CmbSchTrans.SelectedIndex = 0
                        DivInstr.Visible = True
                        'drParam = M_CommonBL.GetLoanParameter(CmbScheme.SelectedValue).[Select]()
                        'If drParam.Count > 0 Then HdnChqValidityPeriod.Value = drParam(0)("Lnp_ChqValidityDays").ToString

                End Select
            ElseIf SchSection.Value = "SUSPENSE" Then
                CmbSchTrans.Items.Add("Credit")
                CmbSchTrans.Items.Add("Debit")
                CmbSchTrans.SelectedIndex = 0

            ElseIf SchSection.Value = "LOCKER" Then
                CmbSchTrans.Items.Add("Rent Collection")
                CmbSchTrans.Items.Add("Closure")
                CmbSchTrans.Items.Add("Breakage")
                CmbSchTrans.SelectedIndex = 0
                DivInstl.Visible = True
            Else
                CmbSchTrans.Items.Add("Credit")
                CmbSchTrans.Items.Add("Debit")
                CmbSchTrans.SelectedIndex = 0
            End If
            Call CmbSchTrans_SelectedIndexChanged(Me.CmbSchTrans, System.EventArgs.Empty)

            M_CommonBL = Nothing

        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try
    End Sub
    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            Session.Remove("SearchScheme")
            TxtSchCode.Text = CmbScheme.SelectedValue
            TxtTransAmt.Text = "0"
            Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub TxtTransAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTransAmt.TextChanged
        Try
            If Val(AccID.Value) = 0 Then Exit Sub





            '''''''Anju''''''''''
            Dim TranMode As String
            If CmbSchTrans.SelectedItem.Text = "Receipt" Then
                TranMode = "R"
            ElseIf CmbSchTrans.SelectedItem.Text = "Payment" Then
                TranMode = "P"
            End If


            Dim M_Dt1 As New DataTable
            Dim McommnBl As New CommonBL
            M_Dt1 = McommnBl.Check_TransferMode(CmbBranch.SelectedValue, Session("TransDate"), IIf(TxtTransAmt.Text = "", 0, TxtTransAmt.Text), TranMode)
            If M_Dt1.Rows.Count > 0 And CmbCashTransfer.SelectedItem.Text = "Cash" Then
                If M_Dt1.Rows(0).Item("Result").ToString() = "Pay" Or M_Dt1.Rows(0).Item("Result").ToString() = "Rct" Then
                    If CmbSchTrans.SelectedItem.Text = "Receipt" Or CmbSchTrans.SelectedItem.Text = "Payment" Then
                        ShowAlertMessage("Change CASH mode to TRANSFER mode.")


                        CmbCashTransfer.Focus()
                        'Txt_IssuedAmt.Text = ""
                        'Txt_BalanceAmount.Text = ""
                        Exit Sub
                    End If
                End If
            End If


            '''''''Anju''''''''''


            If Val(TxtTransAmt.Text) > 199999 And CmbCashTransfer.SelectedItem.Text = "Cash" Then
                ShowAlertMessage("Change CASH mode to TRANSFER mode.")
                CmbCashTransfer.Focus()
                Exit Sub
                CmbCashTransfer.SelectedIndex = 1
                CmbCashTransfer_SelectedIndexChanged(Me.CmbCashTransfer, EventArgs.Empty)
            End If

            Set_TranAmt()
            'TxtTransAmt.Text = ""
            If Val(TxtTransAmt.Text) > 0 Or SchSection.Value = "GROUP DEPOSIT" Then
                CmdSave.Enabled = True
                Dim M_CommonCls As New CommonClass
                Try
                    LblWords.Text = M_CommonCls.Towords(Val(TxtTransAmt.Text)).ToUpper
                Catch ex As Exception

                End Try

                M_CommonCls = Nothing
                TxtTransAmt.Text = Format(Val(TxtTransAmt.Text), "#0.00")

            End If

        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try
    End Sub
    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Try
            If True Then
                Dim M_AccountHead As New DataTable
                Dim Obj_Tran As New Transaction
                M_AccountHead = Obj_Tran.AccountHead()

                Dim Expression As String = "Acc_Name LIKE '" & prefixText & "%'"
                Dim SortingOrder As String = "Acc_Name ASC"
                Dim drFiltered As DataRow() = M_AccountHead.[Select](Expression, SortingOrder)
                Dim CompletionSet = New List(Of String)()
                For Each row As DataRow In drFiltered
                    CompletionSet.Add(row("Acc_Name").ToString().PadRight(40, Convert.ToChar(".")) & "|" & "Code:" & row("Acc_Code").ToString())
                Next
                Return CompletionSet.ToArray()
            End If
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Function

    Private Sub Set_TranAmt()

        Try
            If Val(AccID.Value) = 0 Then Exit Sub
            If CmbTransType.SelectedIndex <> 0 Then Exit Sub ' Blocking For General Voucher
            AlertDt = DirectCast(Session("AlertDt"), DataTable)
            AlertDt.Clear()

            Dim Sql As String = ""
            Dim dt As New DataTable("Table")
            Dim dtAlert As New DataTable
            Dim ds As DataSet
            DivTranGrid.Visible = True
            DivSuspense.Visible = False
            DivInvest.Visible = False
            Select Case SchModuleCatg.Value
                Case "SHARE"
                    Dim M_ShareMasterBL As New ShareMasterBL
                    ds = M_ShareMasterBL.Set_ShareAmt(AccID.Value, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtShareNo.Text), CmbSchTrans.SelectedItem.Text)
                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                    CmdSave.Enabled = True
                    M_ShareMasterBL = Nothing
                Case "OD"
                    Dim M_LoanMasterBL As New LoanMasterBL

                    If CmbSchTrans.SelectedItem.Text = "Credit" Then
                        ds = M_LoanMasterBL.Set_OD_Amt(Val(AccID.Value), Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "R")
                    ElseIf CmbSchTrans.SelectedItem.Text = "Debit" Then
                        ds = M_LoanMasterBL.Set_OD_Amt(Val(AccID.Value), Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "P")
                    ElseIf CmbSchTrans.SelectedItem.Text = "Closure" Then
                        ds = M_LoanMasterBL.Set_OD_Amt(Val(AccID.Value), Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "C")
                    Else
                        ds = M_LoanMasterBL.Set_OD_Amt(Val(AccID.Value), Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "")
                    End If

                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)

                Case "FD", "RIS"
                    If SchSection.Value = "DEPOSIT" Then
                        Dim M_DepositMasterBL As New DepositMasterBL

                        If CmbSchTrans.SelectedItem.Text = "Renewal" Then
                            Dim Duedate As Date

                            If Not Dt_AcDtls Is Nothing Then
                                'HdnRenewDueDt.Value = Dt_AcDtls.Rows(0)("DueDate")
                                Duedate = HdnRenewDueDt.Value 'Dt_AcDtls.Rows(0)("DueDate")
                                TxtRenewEffDate.Text = Duedate
                            End If

                            ds = M_DepositMasterBL.Set_DepositAmt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "RN")
                            If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                            If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)

                            If dt.Rows.Count > 0 Then
                                TxtRenewPrnAmt.Text = Format(Val(dt.Rows(0)("AccAmt").ToString), "#0.00")
                                TxtRenewIntAmt.Text = Format(Val(dt.Rows(0)("IntAmt").ToString), "#0.00")

                                If CmbRenewType.SelectedItem.Text = "PRINCIPLE + INTEREST" Then
                                    TxtRenewAmt.Text = Val(TxtRenewPrnAmt.Text) + Val(TxtRenewIntAmt.Text)
                                    TxtRenewAddiAmt.Text = "0.00"
                                    TxtRenewAddiAmt.Enabled = False
                                    TxtRenewEffDate.Text = HdnRenewDueDt.Value
                                ElseIf CmbRenewType.SelectedItem.Text = "PRINCIPLE ONLY" Then
                                    TxtRenewAmt.Text = Val(TxtRenewPrnAmt.Text)
                                    TxtRenewAddiAmt.Text = "0.00"
                                    TxtRenewAddiAmt.Enabled = False
                                    TxtRenewEffDate.Text = HdnRenewDueDt.Value
                                ElseIf CmbRenewType.SelectedItem.Text = "WITH ADDITIONAL AMOUNT" Then
                                    TxtRenewAmt.Text = Val(TxtRenewPrnAmt.Text) + Val(TxtRenewIntAmt.Text) + Val(TxtRenewAddiAmt.Text)
                                    TxtRenewEffDate.Text = Session("TransDate")
                                    TxtRenewAddiAmt.Enabled = True
                                End If
                            End If
                            TxtTransAmt.Text = TxtRenewAmt.Text
                            DivTranGrid.Visible = False
                            Dim DtNom As New DataTable
                            DtNom = M_DepositMasterBL.Get_NomineeDtls(AccID.Value)
                            If DtNom.Rows.Count > 0 Then
                                Txt_NomName.Text = DtNom.Rows(0)("Nm_Name").ToString()
                                Txt_NomAdd.Text = DtNom.Rows(0)("Nm_Address").ToString()
                                Txt_NomPh.Text = DtNom.Rows(0)("Nm_Phone").ToString()
                                CmbNomRel.SelectedValue = DtNom.Rows(0)("Nm_Relation").ToString()
                                CmbNomReq.SelectedIndex = 1
                                NomDiv.Visible = True
                            End If

                        ElseIf CmbSchTrans.SelectedItem.Text = "Interest Payment" Then
                            ds = M_DepositMasterBL.Set_DepositAmt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "I")
                            If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                            If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                        ElseIf CmbSchTrans.SelectedItem.Text = "Closure" Then
                            ds = M_DepositMasterBL.Set_DepositAmt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "C")
                            If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                            If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                        End If
                    ElseIf SchSection.Value = "INVESTMENT" Then   ' --------------  INVESTMENT
                        Dim M_InvestmentMasterBL As New InvestmentMasterBL
                        DivInvest.Visible = True
                        If CmbSchTrans.SelectedItem.Text = "Renewal" Then
                            Dim Duedate As Date

                            If Not Dt_AcDtls Is Nothing Then
                                'HdnRenewDueDt.Value = Dt_AcDtls.Rows(0)("DueDate")
                                Duedate = HdnRenewDueDt.Value ' Dt_AcDtls.Rows(0)("DueDate")
                                TxtRenewEffDate.Text = Duedate
                            End If

                            ds = M_InvestmentMasterBL.Set_InvestmentAmt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "RN")
                            If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                            If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)

                            If dt.Rows.Count > 0 Then
                                TxtRenewPrnAmt.Text = Format(Val(dt.Rows(0)("AccAmt").ToString), "#0.00")
                                TxtRenewIntAmt.Text = Format(Val(dt.Rows(0)("IntAmt").ToString), "#0.00")

                                If CmbRenewType.SelectedItem.Text = "PRINCIPLE + INTEREST" Then
                                    TxtRenewAmt.Text = Val(TxtRenewPrnAmt.Text) + Val(TxtRenewIntAmt.Text)
                                    TxtRenewAddiAmt.Text = "0.00"
                                    TxtRenewAddiAmt.Enabled = False
                                    TxtRenewEffDate.Text = HdnRenewDueDt.Value
                                ElseIf CmbRenewType.SelectedItem.Text = "PRINCIPLE ONLY" Then
                                    TxtRenewAmt.Text = Val(TxtRenewPrnAmt.Text)
                                    TxtRenewAddiAmt.Text = "0.00"
                                    TxtRenewAddiAmt.Enabled = False
                                    TxtRenewEffDate.Text = HdnRenewDueDt.Value
                                ElseIf CmbRenewType.SelectedItem.Text = "WITH ADDITIONAL AMOUNT" Then
                                    TxtRenewAmt.Text = Val(TxtRenewPrnAmt.Text) + Val(TxtRenewIntAmt.Text) + Val(TxtRenewAddiAmt.Text)
                                    TxtRenewEffDate.Text = Session("TransDate")
                                    TxtRenewAddiAmt.Enabled = True
                                End If
                            End If
                            TxtTransAmt.Text = TxtRenewAmt.Text
                            DivTranGrid.Visible = False
                        ElseIf CmbSchTrans.SelectedItem.Text = "Interest Receipt" Then
                            ds = M_InvestmentMasterBL.Set_InvestmentAmt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "I", Val(TxtInvestIntAmt.Text))
                            If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                            If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                        ElseIf CmbSchTrans.SelectedItem.Text = "Closure" Then
                            ds = M_InvestmentMasterBL.Set_InvestmentAmt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "C", Val(TxtInvestIntAmt.Text))
                            If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                            If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                        End If
                    End If
                Case "BANK"
                    Dim M_InvestmentMasterBL As New InvestmentMasterBL

                    If CmbSchTrans.SelectedItem.Text = "Credit" Then
                        ds = M_InvestmentMasterBL.Set_BankAmt(AccID.Value, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "C")
                    ElseIf CmbSchTrans.SelectedItem.Text = "Debit" Then
                        ds = M_InvestmentMasterBL.Set_BankAmt(AccID.Value, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "D")
                    End If

                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)

                Case "SB", "CA"
                    Dim M_DepositMasterBL As New DepositMasterBL

                    If CmbSchTrans.SelectedItem.Text = "Credit" Then

                        ' If Val(TxtTransAmt.Text) = 0 Then Exit Sub
                        ds = M_DepositMasterBL.Set_SBCA_Amt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "R")

                    ElseIf CmbSchTrans.SelectedItem.Text = "Debit" Then
                        'If Val(TxtTransAmt.Text) = 0 Then Exit Sub
                        ds = M_DepositMasterBL.Set_SBCA_Amt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "P")
                    ElseIf CmbSchTrans.SelectedItem.Text = "Closure" Then
                        ds = M_DepositMasterBL.Set_SBCA_Amt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "C")
                    Else
                        ds = M_DepositMasterBL.Set_SBCA_Amt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "")
                    End If

                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)

                Case "SL", "SA", "TT"


                    Dim M_SuspBl As New SuspMasterBL

                    If CmbSchTrans.SelectedItem.Text = "Credit" Then
                        ds = M_SuspBl.Set_Suspense_Amt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "R")
                    ElseIf CmbSchTrans.SelectedItem.Text = "Debit" Then
                        ds = M_SuspBl.Set_Suspense_Amt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "P")
                    ElseIf CmbSchTrans.SelectedItem.Text = "Closure" Then
                        ds = M_SuspBl.Set_Suspense_Amt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "C")
                    Else
                        ds = M_SuspBl.Set_Suspense_Amt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), "")
                    End If

                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)

                    Dim M_SuspMasterBL As New SuspMasterBL
                    If M_SuspMasterBL.SuspenseMemberLinked(AccID.Value) Then
                        DivSuspense.Visible = True
                    Else
                        DivSuspense.Visible = False
                    End If
                    M_SuspMasterBL = Nothing


                Case "DD"
                    Dim M_DepositMasterBL As New DepositMasterBL
                    ds = M_DepositMasterBL.Set_DDAmt(AccID.Value, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), IIf(CmbSchTrans.SelectedIndex = 2, Left(CmbSchTrans.SelectedItem.Text, 2), Left(CmbSchTrans.SelectedItem.Text, 1)))
                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                Case "RD"
                    Dim M_DepositMasterBL As New DepositMasterBL
                    ds = M_DepositMasterBL.Set_RDAmt(AccID.Value, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtInstNo.Text), Left(CmbSchTrans.SelectedItem.Text, 1))
                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                Case "GOLDLOAN", "LOAN", "GEN.LOAN", "DEP.LOAN"
                    Dim M_LoanMasterBL As New LoanMasterBL
                    Dim M_Dt As New DataTable
                    M_Dt = M_LoanMasterBL.Set_LoanAmt("", "", "", Session("TransDate"), "Cash", 0, "C", "Y", Val(AccID.Value)).Tables(0)

                    Dim objSum As Object
                    If M_Dt.Rows.Count > 0 Then
                        objSum = M_Dt.Compute("Sum(TranAmt)", "TranType <>''")
                        'LblTotal.Text = Format(Val(objSum.ToString()), "#0.00")
                        'TxtTotal.Text = LblTotal.Text
                        'HdnTotal.Value = Val(objSum.ToString())

                        objSum = M_Dt.Compute("Sum(TranAmt)", "TranType <>'*'")
                        LblClearance.Text = Format(Val(objSum.ToString()), "#0.00")

                        'Session("LnRctAmt") = TxtTotal.Text
                        If Val(TxtTransAmt.Text) = 0 Then
                            TxtTransAmt.Text = Val(objSum.ToString())
                        End If
                    Else
                        LblClearance.Text = Format(Val("0"), "#0.00")
                    End If

                    ds = M_LoanMasterBL.Set_LoanAmt(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtTransAmt.Text), CmbSchTrans.SelectedItem.Text.Substring(0, 1).ToUpper, , , Val(TxtRebate.Text))
                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)

                Case "GROUP DEPOSIT"
                    If CmbSchTrans.Text = "Debit" Then
                        CheckSureties()
                    End If

                    Dim M_DepositMasterBL As New DepositMasterBL
                    ds = M_DepositMasterBL.Set_ChittyDtls(AccID.Value, Session("TransDate"), Val(TxtInstNo.Text), CmbCashTransfer.SelectedItem.Text, "N", IIf(CmbSchTrans.SelectedIndex = 0, "C", "D"))
                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                Case "LOCKER"
                    Dim M_LockerMasterBL As New LockerMasterBL

                    If CmbSchTrans.SelectedItem.Text = "Closure" Then
                        ds = M_LockerMasterBL.Set_LockerAmt(AccID.Value, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtInstNo.Text), "C", "N", Val(TxtInstNo.Text))
                    Else
                        ds = M_LockerMasterBL.Set_LockerAmt(AccID.Value, Session("TransDate"), CmbCashTransfer.SelectedItem.Text, Val(TxtInstNo.Text), "R", "N", Val(TxtInstNo.Text))
                    End If


                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)


            End Select

            If CmbSchTrans.SelectedItem.Text <> "Renewal" Then
                DgvTransaction.DataSource = dt
                DgvTransaction.DataBind()

                If SchModuleCatg.Value = "GROUP DEPOSIT" Then
                    If ds.Tables.Count >= 3 And CmbSchTrans.SelectedIndex = 0 Then dt = ds.Tables(2)
                End If
                Session("locAcHeadTbl") = dt

                Call Calc_TranTotal()
            End If

            If dtAlert.Rows.Count > 0 Then
                If dtAlert.Rows(0)("AlertMsg").ToString.Length > 0 Then
                    Dim AlertStr As String
                    AlertStr = dtAlert.Rows(0)("AlertMsg").ToString
                    ShowAlertMessage(AlertStr)
                    If dtAlert.Rows(0)("To_Proceed").ToString = "N" Then
                        AlertDt.Rows.Add("M", AlertStr)
                    End If
                End If

                If dtAlert.Rows(0)("Narrarion").ToString.Length > 0 Then
                    TxtRemarks.Text = dtAlert.Rows(0)("Narrarion").ToString
                    Call TxtMemberNo_TextChanged(Me.TxtMemberNo, System.EventArgs.Empty)
                Else
                    TxtRemarks.Text = ""
                End If
                ' Assinging FD Interest * other closure Amts
                If CDbl(dtAlert.Rows(0)("TranAmt").ToString) > 0 And CmbSchTrans.SelectedItem.Text <> "Renewal" Then
                    TxtTransAmt.Text = Format(CDbl(dtAlert.Rows(0)("TranAmt").ToString), "#0.00")
                End If

                If dtAlert.Rows(0)("IsChkIssue").ToString.ToUpper = "Y" Then
                    Lbl_Status.Visible = True
                Else
                    Lbl_Status.Visible = False
                End If
            End If

            ' Enable / Diable Save Button
            If SchModuleCatg.Value <> "LOCKER" Then
                If Val(TxtTransAmt.Text) > 0 Then
                    Dim M_CommonCls As New CommonClass
                    LblWords.Text = M_CommonCls.Towords(Val(TxtTransAmt.Text)).ToUpper
                    M_CommonCls = Nothing
                    TxtTransAmt.Text = Format(Val(TxtTransAmt.Text), "#0.00")
                    CmdSave.Enabled = True
                End If
            Else
                CmdSave.Enabled = True
            End If


            Session("AlertDt") = AlertDt
            Call AddValidateMsg()

        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try
    End Sub

    Private Sub AddValidateMsg()
        Try

            AlertDt = DirectCast(Session("AlertDt"), DataTable)
            Dim I As Integer = 0

            HdnAlert.Value = ""
            'Dim Expression As String = "Msg=''"
            'If AlertDt.[Select](Expression).Count > 0 Then AlertDt.Rows.RemoveAt(AlertDt.Rows.IndexOf(AlertDt.[Select](Expression)(0)))

            For I = 0 To AlertDt.Rows.Count - 1
                If AlertDt.Rows(I)("Msg").ToString.Trim.Length > 0 Then HdnAlert.Value = HdnAlert.Value & AlertDt.Rows(I)("Msg").ToString & "\n"
            Next
        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub

    Public Sub Calc_DepositRenewDueDate()
        Try
            Dim DueDt As Date
            If Val(TxtRenewMonths.Text) > 0 Then
                DueDt = DateAdd(DateInterval.Month, Val(TxtRenewMonths.Text), CDate(TxtRenewEffDate.Text))
            Else
                DueDt = DateAdd(DateInterval.Day, Val(TxtRenewDays.Text), CDate(TxtRenewEffDate.Text))
            End If
            TxtRenewDueDate.Text = DueDt
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub


    Private Function Get_ServiceTax() As DataTable
        Try

            Dim M_CommonBL As New CommonBL
            Dim Trandt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter

            Trandt.TableName = "Table"

            Trandt.WriteXml(M_TranSw)
            Trandt = M_CommonBL.Get_ServiceTax(CmbBranch.SelectedValue, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), M_TranSw.ToString())

            DgvTransaction.DataSource = Trandt
            DgvTransaction.DataBind()
            Session("locAcHeadTbl") = Trandt

            Call Calc_TranTotal()

            Return Trandt
        Catch ex As Exception
            LoggError(ex)
        End Try
    End Function

    Private Sub Calc_TranTotal()
        Try

            Dim Trandt As New DataTable()
            Trandt = DirectCast(Session("locAcHeadTbl"), DataTable)

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
            LoggError(ex)
        End Try

    End Sub

    Protected Sub CmbSchTrans_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSchTrans.SelectedIndexChanged
        Try
            If CmbSection.Text = "LOAN" And CmbSchTrans.SelectedItem.Text = "Closure" Then
                TxtMobileNo.Visible = True
                But_verfiy.Visible = True
                lblmobno.Visible = True
            Else
                TxtMobileNo.Visible = False
                But_verfiy.Visible = False
                lblmobno.Visible = False
            End If
            If CmbSchTrans.SelectedItem.Text = "Renewal" Then
                DivFDRENEW.Visible = True
            Else
                DivFDRENEW.Visible = False
            End If

            If (SchModuleCatg.Value = "SB" And SchSection.Value = "DEPOSIT") Or ((SchModuleCatg.Value = "CC" Or SchModuleCatg.Value = "OD") And SchSection.Value.ToUpper() = "LOAN") Then
                If SchSection.Value = "DEPOSIT" Then
                    If CmbSchTrans.SelectedItem.Value = "Credit" Then
                        CmbInstType.Enabled = False
                    Else
                        CmbInstType.Enabled = True
                    End If
                End If
                If SchSection.Value.ToUpper() = "LOAN" Then
                    If CmbSchTrans.SelectedItem.Value = "Credit" Then
                        CmbInstType.Enabled = True
                        TxtInstNo.Enabled = True
                        TxtInstrDate.Enabled = True
                    Else
                        CmbInstType.Enabled = False
                        TxtInstNo.Enabled = False
                        TxtInstrDate.Enabled = False
                    End If
                End If
            End If

            Call TxtTransAmt_TextChanged(Me.TxtTransAmt, System.EventArgs.Empty)

            Call CmbInstType_SelectedIndexChanged(Me.CmbInstType, System.EventArgs.Empty)

            If CmbSchTrans.SelectedItem.Text = "Renewal" Then
                Calc_DepositRenewInterestRate()
                Calc_DepositRenewDueDate()
            End If

            If SchSection.Value = "GROUP DEPOSIT" Then
                If CmbSchTrans.SelectedItem.Text = "Debit" Then
                    DivResl.Visible = True
                Else
                    DivResl.Visible = False
                End If
            End If

            If SchSection.Value = "LOAN" Then
                If CmbSchTrans.SelectedItem.Text = "Closure" And CmbSchTrans.SelectedItem.Text = "Receipt" Then
                    DivRebate.Visible = True
                    TxtRebate.Text = ""
                Else
                    DivRebate.Visible = False
                    TxtRebate.Text = ""
                End If
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub CmbCashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.SelectedIndexChanged
        'Set_TranAmt()

        Try
            If CmbCashTransfer.SelectedIndex = 0 Then ' Cash
                CmbBatchNo.Enabled = False
                CmdBatch.Enabled = False
                ImgBatch.Enabled = False
                ChkQuickTransfer.Visible = False
            Else
                ChkQuickTransfer.Visible = True
                CmbBatchNo.Enabled = True
                CmdBatch.Enabled = True
                ImgBatch.Enabled = True
            End If

            Call TxtTransAmt_TextChanged(Me.TxtTransAmt, System.EventArgs.Empty)
            If TxtMemDtls.Text.Length > 0 Then
                TxtRemarks.Text = TxtRemarks.Text + "," + TxtMemberNo.Text + "-" + TxtMemDtls.Text
            End If

            If CmbCashTransfer.SelectedIndex = 1 Then
                ChkChaneCash.Visible = True
                CkBankDetails.Visible = True
            Else
                ChkChaneCash.Visible = False
                ChkChaneCash.checked = False
                ChangeCashDiv.Visible = False
                CkBankDetails.Visible = False
            End If

        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub

    Private Sub Clear_AccDtls()
        Try

            lblName.Text = ""
            lblMemNo.Text = ""
            lblAddress.Text = ""
            lblShadowBal.Text = ""
            lblCurBal.Text = ""
            lblMobileNo.Text = ""
            lblPendingCr.Text = ""
            lblPendingDr.Text = ""
            lblModeOp.Text = ""
            lblCustID.Text = ""

            ImgImage.Enabled = True
            ImgJointAc.Enabled = True
            ImgNominee.Enabled = True
            ImgPassbook.Enabled = True
            ImgDetails.Enabled = True
            ImgOrnaments.Enabled = True
            ImgAction.Enabled = True
            ImgLiab.Enabled = True
            CmdSave.Enabled = True

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub
    Private Sub GetAccountTranDtls()
        Try
            Clear_AccDtls()
            TxtInstNo.Text = ""
            TxtTransAmt.Text = ""

            CmdSave.Enabled = False

            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNo.Text

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                HdnRenewDueDt.Value = Dt_AcDtls.Rows(0)("DueDate")
                Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
                Dim ShadowBal As Double = Dt_AcDtls.Rows(0)("Shadow_Bal").ToString()
                AccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                HdnCustId.Value = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                lblName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                lblCustID.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                lblMemNo.Text = Dt_AcDtls.Rows(0)("CustMemNo").ToString()
                lblAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                lblShadowBal.Text = Format(Val(CStr(Math.Abs(ShadowBal))), "#0.00") & " " & IIf(ShadowBal < 0, "Dr", "Cr")
                lblCurBal.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")
                lblPendingCr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_CrAmt").ToString()), "#0.00")
                lblPendingDr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_DrAmt").ToString()), "#0.00")
                lblModeOp.Text = Dt_AcDtls.Rows(0)("ModeOfOperation").ToString()
                lblMobileNo.Text = Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                TxtMobileNo.Text = Dt_AcDtls.Rows(0)("Cust_MobileNoOrg").ToString()
                CmbSection.SelectedValue = CmbSection.Items.FindByText(Dt_AcDtls.Rows(0)("section").ToString()).Value

                If Len(Request.QueryString("TRANID")) = 0 Then
                    CmbSection_SelectedIndexChanged(Me.CmbSection, EventArgs.Empty)
                    CmbScheme.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                    CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
                End If


                If Val(Dt_AcDtls.Rows(0)("Catg").ToString()) = 0 Then
                    CmbRenewPrivilage.SelectedValue = 417
                Else
                    CmbRenewPrivilage.SelectedValue = Dt_AcDtls.Rows(0)("Catg").ToString()
                End If

                If CmbBranch.SelectedValue <> Dt_AcDtls.Rows(0)("Br_Code").ToString() Then
                    CmbBranch.SelectedValue = Dt_AcDtls.Rows(0)("Br_Code").ToString()
                End If
                TxtSchCode.Text = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                If CmbScheme.SelectedValue <> Dt_AcDtls.Rows(0)("Sch_Code").ToString() Then
                    CmbScheme.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()

                    If M_AccNo.Length >= 8 Then
                        CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
                    End If
                End If
                TxtAccNo.Text = M_AccNo

            Else
                Clear_AccDtls()
                ShowAlertMessage("INVALID ACCOUNT NO")
            End If
            M_CommonBL = Nothing
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAccNo.TextChanged
        Try
            AccID.Value = 0
            TxtMemberNo.Text = ""
            GetAccountTranDtls()

            If AccID.Value = 0 Then Exit Sub
            'For Loading Initial Values
            'Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
            Call CmbSchTrans_SelectedIndexChanged(Me.CmbSchTrans, System.EventArgs.Empty)
            ImgImage.Enabled = True
            ImgJointAc.Enabled = True
            ImgNominee.Enabled = True
            ImgPassbook.Enabled = True
            ImgLiab.Enabled = True
            If SchSection.Value = "LOAN" Or SchSection.Value = "GROUP DEPOSIT" Then
                ImgDetails.Enabled = True
                ImgAction.Enabled = True
                If SchModuleCatg.Value = "GOLDLOAN" Then ImgOrnaments.Enabled = True
            Else
                ImgDetails.Enabled = False
            End If
            If SchSection.Value = "GROUP DEPOSIT" Then
                Dim M_DepositMasterBL As New DepositMasterBL
                TxtInstNo.Text = M_DepositMasterBL.Get_ChittyInst(AccID.Value, Session("TransDate"))
                TxtInstNo_TextChanged(Me.TxtInstNo, System.EventArgs.Empty)
            ElseIf SchModuleCatg.Value = "RD" Then
                Dim M_DepositMasterBL As New DepositMasterBL
                TxtInstNo.Text = M_DepositMasterBL.Get_RDInst(AccID.Value, Session("TransDate"))
                TxtInstNo_TextChanged(Me.TxtInstNo, System.EventArgs.Empty)

            ElseIf SchSection.Value = "SUSPENSE" Then
                Dim M_SuspMasterBL As New SuspMasterBL
                If M_SuspMasterBL.SuspenseMemberLinked(AccID.Value) Then
                    DivSuspense.Visible = True
                Else
                    DivSuspense.Visible = False
                End If
                M_SuspMasterBL = Nothing
            End If

            If Len(lblMemNo.Text) > 0 Then CmbTransType.Focus()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "`")
                Dim M_CommonCls As New CommonClass

                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmdOrnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdOrnAdd.Click
        Try

            If Len(TxtAccCode.Text) = 0 Then
                ShowAlertMessage("INVALID ACCOUNT CODE")
                TxtAccCode.Focus()
                Exit Sub
            End If
            ''''''''Check Negative balance
            'If TxtAcHeadTransAmt.Text.Length > 0 And (CmbAcheadTran.SelectedIndex = 1) Then
            '    Dim M_Dt As New DataTable
            '    Dim M_CommBL As New CommonBL
            '    M_Dt = M_CommBL.Check_Cash_Balance(TxtAcHeadTransAmt.Text, IIf(CmbAcheadTran.SelectedIndex = 0, "C", "D"), Session("TransDate"), Session("DefaultBrCode"))
            '    If M_Dt.Rows.Count > 0 Then
            '        If M_Dt.Rows(0).Item("Status") = "Y" Then
            '            ShowAlertMessage("Cash Balance Is Not Enough")
            '            Exit Sub
            '        End If
            '    End If
            'End If
            ''''''''Check Negative balance
            Dim dt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            Dim Row As DataRow
            Row = dt.NewRow
            Row("AccCode") = TxtAccCode.Text
            Row("AccHead") = TxtAccName.Text.Replace(".", "")
            If CmbAcheadTran.SelectedIndex = 0 Then
                If CmbCashTransfer.SelectedIndex = 0 Then
                    Row("RctCash") = Val(TxtAcHeadTransAmt.Text)
                    Row("RctTransfer") = Val(TxtTransfer.Text)
                    Row("PayCash") = 0
                    Row("PayTransfer") = 0
                Else
                    Row("RctCash") = Val(TxtAcHeadTransAmt.Text)
                    Row("RctTransfer") = Val(TxtTransfer.Text)
                    Row("PayCash") = 0
                    Row("PayTransfer") = 0
                End If
            Else
                If CmbCashTransfer.SelectedIndex = 0 Then
                    Row("RctCash") = 0
                    Row("RctTransfer") = 0
                    Row("PayCash") = Val(TxtAcHeadTransAmt.Text)
                    Row("PayTransfer") = Val(TxtTransfer.Text)
                Else
                    Row("RctCash") = 0
                    Row("RctTransfer") = 0
                    Row("PayCash") = Val(TxtAcHeadTransAmt.Text)
                    Row("PayTransfer") = Val(TxtTransfer.Text)
                End If
            End If
            Row("Seq_No") = dt.Rows.Count
            Try
                dt.Rows.Add(Row)
            Catch ex As Exception
                ShowAlertMessage(Err.Description)
            End Try

            Try
                Dim Expression As String = "AccCode=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception
                ShowAlertMessage(Err.Description)
            End Try
            dt = Get_ServiceTax()
            DgvTransaction.DataSource = dt
            DgvTransaction.DataBind()
            Session("locAcHeadTbl") = dt
            ClearAcHead()
            TxtRemarks.Focus()
            Dim M_CommonBL As New CommonBL
            Dim AccHeadTemp As DataTable
            AccHeadTemp = M_CommonBL.Fill_AcHead()

            AcHeadDt1 = Session("AccountHeadTbl")
            If AcHeadDt1.Rows.Count = 0 Then
                If HttpContext.Current.Session("ASSIGNEDBRANCH") = "%" Then
                    Dim Rows() As DataRow = AccHeadTemp.Select(" Acc_Visible='Y'")
                    If Rows.Length > -1 Then
                        AcHeadDt1 = Rows.CopyToDataTable()

                    End If

                Else
                    Dim Rows() As DataRow = AccHeadTemp.Select("Acc_HoAc='N' AND Acc_Visible='Y'")
                    If Rows.Length > -1 Then
                        AcHeadDt1 = Rows.CopyToDataTable()
                    End If

                    Dim DataView1 As New DataView
                    DataView1 = AccHeadTemp.DefaultView()
                    DataView1.RowFilter = "Acc_Branch = '" & Session("DefaultBrCode") & "'"
                    For I = 0 To DataView1.Count - 1
                        AcHeadDt1.ImportRow(DataView1.Item(I).Row)
                    Next I

                End If
                Session("AccountHeadTbl") = AcHeadDt1
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            LoggError(ex)
        End Try
    End Sub
    Private Sub ClearAcHead()
        Try
            TxtAccCode.Text = ""
            TxtAccName.Text = ""
            TxtAcHeadTransAmt.Text = ""
            TxtTransfer.Text = ""
            LblWords.Text = ""
        Catch ex As Exception
            LoggError(ex)
        End Try
    End Sub
    Private Function CashControl() As Boolean
        Try
            If CmbTransType.SelectedIndex = 1 Then
                Return True
                Exit Function
            End If
            If CmbCashTransfer.SelectedItem.Text <> "Cash" Then
                Return True
                Exit Function
            End If
            Dim M_con As New CommonBL
            Dim M_Cash As New DataTable()
            Dim SchCash As Double
            Dim BrCash As Double
            Dim TransferDt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            Dim CashRctTotal As Double
            Dim CashPayTotal As Double
            Dim TransferRctTotal As Double
            Dim TransferPayTotal As Double


            CashRctTotal = TransferDt.Compute("SUM(RctCash)", "")
            CashPayTotal = TransferDt.Compute("SUM(PayCash)", "")
            TransferRctTotal = TransferDt.Compute("SUM(RctTransfer)", "")
            TransferPayTotal = TransferDt.Compute("SUM(PayTransfer)", "")
            'M_Cash = M_con.GetTanLimit(Session("DefaultBrCode").ToString(), CmbScheme.SelectedValue, AccID.Value, CashRctTotal + CashPayTotal, CmbCashTransfer.SelectedItem.Text, Session("TransDate"))
            Dim dtCustId As New DataTable
            dtCustId = M_con.GetCustIDFromAccId(AccID.Value)
            Dim CustId As Integer = dtCustId.Rows(0).Item("ACC_CustID")
            M_Cash = M_con.GetTanLimit(Session("DefaultBrCode").ToString(), CmbScheme.SelectedValue, CustId, CashRctTotal + CashPayTotal, CmbCashTransfer.SelectedItem.Text, Session("TransDate"))
            'SchCash = M_Cash.Rows(0).Item("SchCash")
            'BrCash = M_Cash.Rows(0).Item("BrCash")

            If M_Cash.Rows(0).Item("Status") = "N" Then
                ShowAlertMessage("Customer Cash Limit Exceeded, Change Cash to Transfer")
                Return False
            End If

            'If SchSection.Value = "LOAN" Then
            '    If (CashRctTotal - CashPayTotal) > BrCash Then
            '        ShowAlertMessage("Branch Cash Limit Exceeded, Change Cash to Bank", "Info", CommonBL.MessageType.infomsg)
            '        Return False
            '    End If
            '    If (CashRctTotal - CashPayTotal) > SchCash Then
            '        ShowAlertMessage("Scheme Cash Limit Exceeded, Change Cash to Bank", "Info", CommonBL.MessageType.infomsg)
            '        Return False
            '    End If
            'Else
            '    If CmbSchTrans.SelectedItem.Text = "Renewal" And SchSection.Value = "DEPOSIT" Then
            '        Dim M_RenewalCash As Double
            '        M_RenewalCash = (Val(TxtRenewIntAmt.Text) + Val(TxtRenewPrnAmt.Text) - Val(TxtRenewAmt.Text))
            '        M_RenewalCash = Math.Abs(M_RenewalCash)
            '        If CmbCashTransfer.SelectedItem.Text = "Cash" Then
            '            If M_RenewalCash > BrCash Then
            '                ShowAlertMessage("Branch Cash Limit Exceeded, Change Cash to Bank", "Info", CommonBL.MessageType.infomsg)
            '                Return False
            '            End If

            '            If M_RenewalCash > SchCash Then
            '                ShowAlertMessage("Scheme Cash Limit Exceeded, Change Cash to Bank", "Info", CommonBL.MessageType.infomsg)
            '                Return False
            '            End If
            '        End If

            '    Else


            '        If (CashPayTotal - CashRctTotal) > BrCash Then
            '            ShowAlertMessage("Branch Cash Limit Exceeded, Change Cash to Bank", "Info", CommonBL.MessageType.infomsg)
            '            Return False
            '        End If

            '        If (CashPayTotal - CashRctTotal) > SchCash Then


            '            ShowAlertMessage("Scheme Cash Limit Exceeded, Change Cash to Bank", "Info", CommonBL.MessageType.infomsg)
            '            Return False
            '        End If
            '    End If
            'End If

            Return True
        Catch ex As Exception

        End Try
    End Function
    Private Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            If Not ((Val(Session("MaxTranLimit").ToString) >= (Val(LblRctCashTotal.Text) + Val(LblRctTransferTotal.Text) + Val(LblPayCashTotal.Text) + Val(LblPayTransferTotal.Text)))) Then
                ShowAlertMessage("Maximum Transaction Limit is :" + Session("MaxTranLimit").ToString())
                Exit Sub
            End If

            If CashControl() = False Then
                Exit Sub
            End If
            If TxtRemarks.Text.Trim().Length <= 5 Then
                ShowAlertMessage("Please enter Remarks")
                Exit Sub
            End If
            '''''''Anju''''''''''
            If Len(Request.QueryString("TRANID")) = 0 Then
                Dim TranMode As String
                If CmbSchTrans.SelectedItem.Text = "Receipt" Then
                    TranMode = "R"
                ElseIf CmbSchTrans.SelectedItem.Text = "Payment" Then
                    TranMode = "P"
                End If
                If CmbSection.Text = "LOAN" And CmbSchTrans.SelectedItem.Text = "Closure" Then
                    If Hid_ODPStatus.Value = "N" Then
                        TxtMobileNo.Focus()
                        ShowAlertMessage("VERIFY CUSTOMER MOBILE NO")
                        Exit Sub
                    End If
                End If

                Dim M_Dt1 As New DataTable
                Dim McommnBl As New CommonBL
                M_Dt1 = McommnBl.Check_TransferMode(CmbBranch.SelectedValue, Session("TransDate"), IIf(TxtTransAmt.Text = "", 0, TxtTransAmt.Text), TranMode)
                If M_Dt1.Rows.Count > 0 And CmbCashTransfer.SelectedItem.Text = "Cash" Then
                    If M_Dt1.Rows(0).Item("Result").ToString() = "Pay" Or M_Dt1.Rows(0).Item("Result").ToString() = "Rct" Then
                        If CmbSchTrans.SelectedItem.Text = "Receipt" Or CmbSchTrans.SelectedItem.Text = "Payment" Then
                            ShowAlertMessage("Change CASH mode to TRANSFER mode.")


                            CmbCashTransfer.Focus()
                            'Txt_IssuedAmt.Text = ""
                            'Txt_BalanceAmount.Text = ""
                            Exit Sub
                        End If
                    End If
                End If
            End If


            '''''''Anju''''''''''

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

            If DivSuspense.Visible = True And Val(HdnReferAccID.Value) = 0 Then
                ShowAlertMessage("Invalid Member Details Entered")
                Exit Sub
            End If
            ''''''''Check Negative balance
            'If TxtAcHeadTransAmt.Text.Length > 0 And (CmbAcheadTran.SelectedIndex = 1) Then
            '    Dim M_Dt As New DataTable
            '    M_Dt = M_CommonBL.Check_Cash_Balance(TxtAcHeadTransAmt.Text, IIf(CmbAcheadTran.SelectedIndex = 0, "C", "D"), TxtAcHeadTransAmt.Text)
            '    If M_Dt.Rows.Count > 0 Then
            '        If M_Dt.Rows(0).Item("Status")="Y"
            '        ShowAlertMessage("Cash Balance Is Not Enough")
            '            Exit Sub
            '        End If
            '    End If
            'End If
            ''''''''Check Negative balance
            Dim Tran_No As String = ""


            If CkBankDetails.Checked = True Then
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

            SaveData(Tran_No)
            Tran_No = Session("Tran_No")
            If Session("BalanceControll") = "Y" Then
                Exit Sub
            End If
            If CmbTransType.SelectedIndex = 0 Then
                Dim Section As String = M_CommonBL.GetSchemeSection(CmbScheme.SelectedValue)
                Dim str1 As String
                Dim strArr() As String
                str1 = Session("Acc_No").ToString()
                strArr = str1.Split(",")


                'transfer mode saving-------------------------------------------

                If CmbCashTransfer.SelectedIndex = 1 And CkBankDetails.Checked = True Then

                    Dim M_LoanMasterBL1 As New LoanMasterBL
                    Dim dtTransferDetails As New DataTable
                    dtTransferDetails = M_LoanMasterBL1.SaveTransferDetails_Data(Tran_No, drpBank.SelectedItem.ToString(), drpbankAccno.SelectedItem.Text.ToString(), TxtCheque.Text.ToString(), txtChequeDate.Text.ToString(), drpTranferType.SelectedValue.ToString(), txtIFSC.Text.ToString(), TxtAccNo.Text.ToString(), txtCommission.Text.ToString(), Session("TransDate"), Val(TxtBankAmnt.Text), IIf(DdlTranType.SelectedIndex = 1, "C", "D"))


                End If

                'transfer mode saving------------------------------------------
                If strArr.Length > 1 Then
                    Response.Redirect("~/NewAccount.aspx?AccNo=" & Session("Acc_No") & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & Section & "&DRCR=" & IIf(CmbSchTrans.SelectedIndex = 0, "C", "D") & "&TRANCATG=" & "TRAN")
                Else
                    Response.Redirect("~/NewAccount.aspx?AccNo=" & CmbScheme.SelectedItem.Text & " - " & TxtAccNo.Text & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & Section & "&DRCR=" & IIf(CmbSchTrans.SelectedIndex = 0, "C", "D") & "&TRANCATG=" & "TRAN")
                End If
            Else
                Response.Redirect("~/NewAccount.aspx?AccNo=" & "GENERAL VOUCHER" & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & "VOU" & "&SEC=" & "GENERAL" & "&DRCR=" & IIf(CmbSchTrans.SelectedIndex = 0, "C", "D") & "&TRANCATG=" & "TRAN")
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try
    End Sub
    Private Sub drpBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpBank.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
    End Sub
    Private Sub Clear_Text()
        Try

            TxtAccNo.Text = ""
            lblName.Text = ""
            lblMemNo.Text = ""
            lblCurBal.Text = ""
            lblShadowBal.Text = ""
            lblPendingCr.Text = ""
            lblPendingDr.Text = ""
            TxtTransAmt.Text = ""
            TxtInstrNo.Text = ""
            TxtInstrDate.Text = ""
            TxtAccCode.Text = ""
            TxtAccName.Text = ""
            txtCntraAccCode.Text = ""
            txtCntraAccName.Text = ""
            TxtAcHeadTransAmt.Text = ""
            TxtTransfer.Text = ""
            TxtRemarks.Text = ""
            TxtTockenNo.Text = ""
            HdnAlert.Value = ""
            TxtMemberNo.Text = ""
            TxtMemDtls.Text = ""
            HdnReferAccID.Value = "0"
            lblAddress.Text = ""
            TxtShortCode.Text = ""
            LblDebit.Text = ""
            ' LblCredit.Text = ""
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            CmbStandBr.SelectedValue = Session("DefaultBrCode")
            CmbMemBranch.SelectedValue = Session("DefShareBranch")
            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub

    Private Sub Clear_Controls()
        Try
            Clear_Text()
            Hid_ODPStatus.Value = "N"
            Lbl_Status.Visible = False
            CmbCashTransfer.Enabled = True
            CmbBatchNo.SelectedIndex = 0

            CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try
            Clear_Controls()

            If CmbCashTransfer.SelectedIndex = 0 Then ' Cash
                CmbBatchNo.Enabled = False
            Else
                CmbBatchNo.Enabled = True
            End If
        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Try
            ' Response.Redirect("~/main.aspx")
        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub

    Private Sub Print_Voucher(ByVal M_TranNo As String)
        Try


            ' VOU PRINT
            Dim Prn As New Print
            Dim DtLoc As New DataTable
            Dim M_DBConn As New SMARTCORE_DAL.Connection
            Dim Qry As String

            If CmbTransType.SelectedIndex = 1 Then
                Qry = "select * from Transaction_T_Tbl where tran_no='" & M_TranNo & "'"
                M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
                Prn.SetUPPrint(Session("DefaultBrCode"), "GENERAL", "", M_TranNo, "1", IIf(CmbAcheadTran.SelectedIndex = 0, "C", "D"), 1, True, Session("TransDate"), Session("LoginID").ToString(), DtLoc)
            Else
                Qry = "select * from Transaction_T_Tbl where Tran_no='" & M_TranNo & "'"
                M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
                Prn.SetUPPrint(CmbBranch.SelectedValue, SchSection.Value, CmbScheme.SelectedValue, M_TranNo, DtLoc.Rows(0)("Vr_No").ToString(), IIf(CmbSchTrans.SelectedIndex = 0, "C", "D"), 1, True, Session("TransDate"), Session("LoginID").ToString(), DtLoc)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
            Session("SearchBranch") = CmbBranch.SelectedValue
            Set_TranAmt()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        Try
            'On Error Resume Next
            CmbScheme.SelectedValue = TxtSchCode.Text
            TxtSchCode.Text = CmbScheme.SelectedValue
            Set_SchemeCtrl()
            TxtShortCode.Text = ""
            TxtShortCode.Focus()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
            Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub CmbRenewType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbRenewType.SelectedIndexChanged
        Try
            'Set_TranAmt()
            Call TxtTransAmt_TextChanged(Me.TxtTransAmt, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtRenewDays_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtRenewDays.TextChanged

        Try

            Calc_DepositRenewInterestRate()
            Calc_DepositRenewDueDate()
        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub

    Protected Sub TxtRenewMonths_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtRenewMonths.TextChanged
        Try
            Calc_DepositRenewInterestRate()
            Calc_DepositRenewDueDate()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Calc_DepositRenewInterestRate()
        Try
            Dim M_CommonBL As New CommonBL '*****

            TxtRenewIntRate.Text = Format(M_CommonBL.Get_Intr_Rate(TxtRenewEffDate.Text, CmbScheme.SelectedValue, "", Val(TxtRenewDays.Text) + Val(TxtRenewMonths.Text), _
                        IIf(Val(TxtRenewMonths.Text) > 0, "M", "D"), Val(TxtRenewAmt.Text), CommonBL.Intr_RateTypes.INTR_RATE_NORMAL), "#0.00")

            If CmbRenewPrivilage.SelectedValue <> CommonBL.Intr_RateTypes.INTR_RATE_NORMAL Then
                TxtRenewIntRate.Text = Format(Val(TxtRenewIntRate.Text) + M_CommonBL.Get_Intr_Rate(TxtRenewEffDate.Text, CmbScheme.SelectedValue, "", Val(TxtRenewDays.Text) + Val(TxtRenewMonths.Text), _
                        IIf(Val(TxtRenewMonths.Text) > 0, "M", "D"), Val(TxtRenewAmt.Text), CmbRenewPrivilage.SelectedValue), "#0.00")
            End If
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub TxtRenewAddiAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtRenewAddiAmt.TextChanged
        Try

            'Set_TranAmt()
            Call TxtTransAmt_TextChanged(Me.TxtTransAmt, System.EventArgs.Empty)
            Calc_DepositRenewInterestRate()
        Catch ex As Exception
            LoggError(ex)
        End Try
    End Sub

    Protected Sub CmbInstType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbInstType.SelectedIndexChanged
        Try
            If CmbInstType.SelectedIndex = 0 Then
                TxtInstrNo.Enabled = False
                TxtInstrDate.Enabled = False
            Else
                TxtInstrNo.Enabled = True
                TxtInstrDate.Enabled = True
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Private Sub TxtShareNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtShareNo.TextChanged
        'Set_TranAmt()
        Try
            Call TxtTransAmt_TextChanged(Me.TxtTransAmt, System.EventArgs.Empty)
        Catch ex As Exception
            LoggError(ex)
        End Try
        'TxtTransAmt.Text = Format(Val(TxtShareNo.Text) * drParam(0)("Shp_ShareValue"), "#0.00")
        'If CmbSchTrans.SelectedIndex <> 0 Then
        '    If Val(TxtTransAmt.Text) > Val(lblCurBal.Text) Or Val(TxtTransAmt.Text) > Val(TxtShadowBal.Text) Then
        '        TxtTransAmt.Text = Format(Val(lblCurBal.Text), "#0.00")
        '        TxtShareNo.Text = Val(lblCurBal.Text) / drParam(0)("Shp_ShareValue")
        '        TxtTransAmt_TextChanged(Me.TxtShareNo, System.EventArgs.Empty)
        '    End If
        'End If
    End Sub

    Protected Sub ImgJointAc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgJointAc.Click
        ' Response.Redirect("~/JointAcc.aspx?AccID=" & AccID.Value)
    End Sub

    Protected Sub ImgNominee_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNominee.Click
        'Response.Redirect("~/NomDtls.aspx?AccID=" & AccID.Value)
    End Sub

    Protected Sub ImgPassbook_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgPassbook.Click
        'Response.Redirect("~/TranDtls.aspx?AccID=" & AccID.Value)
    End Sub


    Private Sub TxtInstNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtInstNo.TextChanged
        Try


            If SchSection.Value = "GROUP DEPOSIT" Then
                Set_TranAmt()
                'Dim Ds As New DataSet
                'Dim Dt As New DataTable
                'Dim M_DepositMasterBL As New DepositMasterBL
                'Ds = M_DepositMasterBL.Set_ChittyDtls(Val(AccID.Value), Session("TransDate"), Val(TxtInstNo.Text), CmbCashTransfer.SelectedItem.Text, "N")
                'DgvTransaction.DataSource = Ds.Tables(0)
                'DgvTransaction.DataBind()
                'Session("locAcHeadTbl") = Ds.Tables(2)
                'If Ds.Tables.Count >= 3 And CmbSchTrans.SelectedIndex = 0 Then Dt = Ds.Tables(2)
            End If
            Call TxtTransAmt_TextChanged(Me.TxtTransAmt, System.EventArgs.Empty)
        Catch ex As Exception
            LoggError(ex)
        End Try
    End Sub

    Protected Sub TxtInstrNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtInstrNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim Msg As String = ""

            AlertDt = DirectCast(Session("AlertDt"), DataTable)
            Dim Expression As String = "Type='TxtInstrNo'"
            If AlertDt.[Select](Expression).Count > 0 Then AlertDt.Rows.RemoveAt(AlertDt.Rows.IndexOf(AlertDt.[Select](Expression)(0)))

            If M_CommonBL.Verify_Cheque(Val(AccID.Value), TxtInstrNo.Text.Trim, Msg) = False Then
                AlertDt.Rows.Add("TxtInstrNo", Msg)
                ShowAlertMessage(Msg)
            End If

            Session("AlertDt") = AlertDt

            AddValidateMsg()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub TxtInstrDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtInstrDate.TextChanged
        Try
            Dim M_CommonCls As New CommonClass
            Dim Msg As String = ""

            AlertDt = DirectCast(Session("AlertDt"), DataTable)
            Dim Expression As String = "Type='TxtInstrDate'"
            If AlertDt.[Select](Expression).Count > 0 Then AlertDt.Rows.RemoveAt(AlertDt.Rows.IndexOf(AlertDt.[Select](Expression)(0)))

            If M_CommonCls.SfDateDiff(HdnChqValidityPeriodType.Value, CDate(TxtInstrDate.Text), CDate(HdnTrDate.Value)) > Val(HdnChqValidityPeriod.Value) Then
                Msg = "This Cheque Exeeds the Validity Period "
                AlertDt.Rows.Add("TxtInstrDate", Msg)
                ShowAlertMessage(Msg)
            ElseIf CDate(HdnTrDate.Value) < CDate(TxtInstrDate.Text) Then
                Msg = "Postdated Cheque Not Allowed "
                AlertDt.Rows.Add("TxtInstrDate", Msg)
                ShowAlertMessage(Msg)
            End If

            Session("AlertDt") = AlertDt

            AddValidateMsg()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub TxtMemberNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtMemberNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim MemAccID As Integer

            If TxtMemberNo.Text.Length > 0 Then
                TxtMemDtls.Text = M_CommonBL.GetAccountCustName(CmbMemBranch.SelectedValue, CmbMemType.SelectedValue, TxtMemberNo.Text, MemAccID)
                HdnReferAccID.Value = MemAccID
            End If


            If TxtMemDtls.Text.Length > 0 Then
                TxtRemarks.Text = TxtRemarks.Text + "," + TxtMemberNo.Text + "-" + TxtMemDtls.Text
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub



    Protected Sub CmbNomReq_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbNomReq.SelectedIndexChanged
        Try
            If CmbNomReq.SelectedIndex = 0 Then
                Txt_NomName.Text = ""
                NomDiv.Visible = False
            Else
                'If Txt_NomAdd.Text = "" Then
                '    Txt_NomAdd.Text = DgvCustomer.Rows(0).Cells(7).Text
                'End If
                NomDiv.Visible = True
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Ddl_SI_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_SI.SelectedIndexChanged
        Try
            If Ddl_SI.SelectedIndex = 1 Then
                StandDiv.Visible = True
            Else
                StandDiv.Visible = False
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtStandAcNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtStandAcNo.TextChanged
        Try
            Dim Dt_AcDtlsN As DataTable
            Dim Dt_AcDtls As DataTable
            Dim M_CommonBL As New CommonBL

            If TxtStandAcNo.Text.Trim.Length <= 7 Then
                TxtStandAcNo.Text = Right("000" + CmbStandBr.SelectedValue, 3) + Right("000" + CmbStandSch.SelectedValue, 3) + Right("0000000" + TxtStandAcNo.Text, 7)
            End If

            Dim M_AccNo As String = TxtStandAcNo.Text

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbStandBr.SelectedValue, CmbStandSch.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count = 0 Then
                ShowAlertMessage("Invalid Account Number")
                TxtStandAcNo.Focus()
                M_CommonBL = Nothing
                Exit Sub
            End If

            Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbStandBr.SelectedValue, CmbStandSch.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtlsN.Rows.Count > 0 Then
                TxtStandAcHoldenName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
                TxtStandAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAccCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccCode.TextChanged
        Try
            AcHeadDt1 = Session("AccountHeadTbl")
            Dim Expression As String = "Acc_Code='" & TxtAccCode.Text & "'"
            Dim drFiltered As DataRow() = AcHeadDt1.[Select](Expression)

            If drFiltered.Count > 0 Then
                TxtAccName.Text = drFiltered(0)("Acc_Name")
            Else
                TxtAccName.Text = ""
                ShowAlertMessage("Invalid A/c Head")
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub txtCntraAccCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCntraAccCode.TextChanged
        Try
            AcHeadDt1 = Session("AccountHeadTbl")
            Dim Expression As String = "Acc_Code='" & txtCntraAccCode.Text & "'"
            Dim drFiltered As DataRow() = AcHeadDt1.[Select](Expression)

            If drFiltered.Count > 0 Then
                txtCntraAccName.Text = drFiltered(0)("Acc_Name")
            Else
                txtCntraAccName.Text = ""
                ShowAlertMessage("Invalid A/c Head")
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub TxtAcHeadTransAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAcHeadTransAmt.TextChanged
        Dim M_CommonCls As New CommonClass
        LblWords.Text = M_CommonCls.Towords(Val(TxtAcHeadTransAmt.Text) + Val(TxtTransfer.Text)).ToUpper
        M_CommonCls = Nothing
    End Sub

    Protected Sub CmbRenewPrivilage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbRenewPrivilage.SelectedIndexChanged
        Call Calc_DepositRenewInterestRate()
    End Sub

    Private Sub SaveData(ByRef Tran_No As String)
        Try
            'Dim Trandt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As New DataTable
            Dim M_BatchNo As String = ""

            Dim Trandt As New DataTable("Table")
            Dim r As DataRow

            Trandt.Columns.Add("AccNo", GetType(String))
            Trandt.Columns.Add("AccCode", GetType(String))
            Trandt.Columns.Add("AccHead", GetType(String))
            Trandt.Columns.Add("RctCash", GetType(Double))
            Trandt.Columns.Add("RctTransfer", GetType(Double))
            Trandt.Columns.Add("PayCash", GetType(Double))
            Trandt.Columns.Add("PayTransfer", GetType(Double))
            Trandt.Columns.Add("Seq_No", GetType(String))

            For Each row As GridViewRow In DgvTransaction.Rows

                Dim AccNo As String = row.Cells(0).Text
                Dim AccCode As String = row.Cells(1).Text
                Dim AccHead As String = row.Cells(2).Text
                Dim RctCash As Double = Val(row.Cells(3).Text)
                Dim RctTransfer As Double = Val(row.Cells(4).Text)
                Dim PayCash As Double = Val(row.Cells(5).Text)
                Dim PayTransfer As Double = Val(row.Cells(6).Text)
                Dim Seq_No As String = row.Cells(7).Text
                Trandt.Rows.Add(AccNo.Replace("&nbsp;", ""), AccCode, AccHead, RctCash, RctTransfer, PayCash, PayTransfer, Seq_No)
            Next

            Trandt.WriteXml(M_TranSw)
            Tran_No = ""
            If CmbBranch.SelectedValue = "" Then
                Response.Redirect("~/SmartLogin.aspx")
                Exit Sub
            End If

            If CmbCashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text

            If CmbTransType.SelectedIndex = 0 Then
                ' FD Renewal
                If (SchModuleCatg.Value = "FD" Or SchModuleCatg.Value = "RIS") And SchSection.Value = "DEPOSIT" And CmbSchTrans.SelectedItem.Text = "Renewal" Then

                    Dim M_DepositMasterBL As New DepositMasterBL

                    Dim RenewTbl As New DataTable("Table")

                    RenewTbl.Columns.Add("Period", GetType(Integer))
                    RenewTbl.Columns.Add("PeriodType", GetType(String))
                    RenewTbl.Columns.Add("Privilage", GetType(Integer))
                    RenewTbl.Columns.Add("RenewAmt", GetType(Double))
                    RenewTbl.Columns.Add("AdditionAmt", GetType(Double))
                    RenewTbl.Columns.Add("RenewType", GetType(String))
                    RenewTbl.Columns.Add("EffectDate", GetType(Date))
                    RenewTbl.Columns.Add("IntRate", GetType(Double))
                    RenewTbl.Columns.Add("FrAccID", GetType(Double))
                    RenewTbl.Columns.Add("IntAmt", GetType(Double))
                    RenewTbl.Columns.Add("PrnAmt", GetType(Double))
                    RenewTbl.Columns.Add("Dep_IntPayMode", GetType(Double))
                    RenewTbl.Columns.Add("Dep_Privilage", GetType(Double))

                    RenewTbl.Columns.Add("Dep_NomNm", GetType(String))
                    RenewTbl.Columns.Add("Dep_NomAdd", GetType(String))
                    RenewTbl.Columns.Add("Dep_NomRelCode", GetType(Double))
                    RenewTbl.Columns.Add("Dep_NomPh", GetType(String))

                    RenewTbl.Columns.Add("Dep_StndBr", GetType(String))
                    RenewTbl.Columns.Add("Dep_StndSch", GetType(String))
                    RenewTbl.Columns.Add("Dep_StndAccNo", GetType(String))

                    If Not Dt_AcDtls Is Nothing Then
                        Dim RenewType As String = ""
                        Dim Row As DataRow

                        If CmbRenewType.SelectedItem.Text = "PRINCIPLE + INTEREST" Then
                            RenewType = "PI"
                        ElseIf CmbRenewType.SelectedItem.Text = "PRINCIPLE ONLY" Then
                            RenewType = "PO"
                        ElseIf CmbRenewType.SelectedItem.Text = "WITH ADDITIONAL AMOUNT" Then
                            RenewType = "WAM"
                        End If

                        Row = RenewTbl.NewRow
                        Row("Period") = IIf(Val(TxtRenewMonths.Text) > 0, Val(TxtRenewMonths.Text), Val(TxtRenewDays.Text))
                        Row("PeriodType") = IIf(Val(TxtRenewMonths.Text) > 0, "M", "D")
                        Row("Privilage") = Dt_AcDtls.Rows(0)("Catg")
                        Row("RenewAmt") = Val(TxtRenewAmt.Text)
                        Row("AdditionAmt") = Val(TxtRenewAddiAmt.Text)
                        Row("RenewType") = RenewType
                        Row("EffectDate") = TxtRenewEffDate.Text
                        Row("IntRate") = Val(TxtRenewIntRate.Text)
                        Row("FrAccID") = AccID.Value
                        Row("IntAmt") = Val(TxtRenewIntAmt.Text)
                        Row("PrnAmt") = Val(TxtRenewPrnAmt.Text)
                        Row("Dep_IntPayMode") = CmbRenewIntPayMode.SelectedValue
                        Row("Dep_Privilage") = CmbRenewPrivilage.SelectedValue

                        If CmbNomReq.SelectedIndex = 1 Then
                            Row("Dep_NomNm") = Txt_NomName.Text
                            Row("Dep_NomAdd") = Txt_NomAdd.Text
                            Row("Dep_NomRelCode") = CmbNomRel.SelectedValue
                            Row("Dep_NomPh") = Txt_NomPh.Text
                        Else
                            Row("Dep_NomNm") = ""
                            Row("Dep_NomAdd") = ""
                            Row("Dep_NomRelCode") = 0
                            Row("Dep_NomPh") = ""
                        End If

                        If Ddl_SI.SelectedIndex = 1 Then
                            Row("Dep_StndBr") = CmbStandBr.SelectedValue
                            Row("Dep_StndSch") = CmbStandSch.SelectedValue
                            Row("Dep_StndAccNo") = TxtStandAcNo.Text
                        Else
                            Row("Dep_StndBr") = ""
                            Row("Dep_StndSch") = ""
                            Row("Dep_StndAccNo") = ""
                        End If
                        RenewTbl.Rows.Add(Row)
                    End If

                    Dim M_RenewSw As New IO.StringWriter
                    RenewTbl.WriteXml(M_RenewSw)

                    M_Dt = M_DepositMasterBL.Save_RenewalData(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Session("TransDate"), Session("DefaultBrCode"), TxtRemarks.Text, Session("LoginID"), AccID.Value, TxtAccNo.Text, M_RenewSw.ToString.Replace("DocumentElement", "NewDataSet"), Left(CmbCashTransfer.SelectedItem.Text, 1), M_BatchNo, TxtRemarks1.Text, TxtTockenNo.Text)

                    If M_Dt.Rows.Count > 0 Then
                        Tran_No = M_Dt.Rows(0)("Tran_No").ToString()
                        ShowAlertMessage("UPDATED")
                        Dim Section As String = M_CommonBL.GetSchemeSection(CmbScheme.SelectedValue)
                        Response.Redirect("~/NewAccount.aspx?AccNo=" & CmbScheme.SelectedItem.Text & " - " & M_Dt.Rows(0)("ToAccNo").ToString() & "&TransId=" & Tran_No & "&PrintBond=Y" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & Section & "&DRCR=" & IIf(CmbSchTrans.SelectedIndex = 0, "C", "D") & "&TRANCATG=" & "TRAN")

                    Else
                        ShowAlertMessage("FAILED")
                    End If
                Else  ' Normal A/c Transaction
                    Dim ChqNo As String = "-WS-"
                    Dim ChqDate As String = #1/1/1900#

                    If TxtInstrNo.Text.Trim.Length > 0 Then
                        ChqNo = CmbInstType.SelectedItem.Text.Substring(0, 1) & "-" & TxtInstrNo.Text.Trim
                        ChqDate = IIf(TxtInstrDate.Text.Length > 0, TxtInstrDate.Text, #1/1/1900#)
                    End If

                    If (SchSection.Value = "SHARE") Or (SchSection.Value = "GROUP DEPOSIT" And CmbSchTrans.SelectedItem.Text = "Debit") Then
                        If TxtShReslNo.Text.Trim.Length > 0 Then TxtRemarks.Text = Trim(TxtRemarks.Text & " Res. " & TxtShReslNo.Text & " " & TxtShReslDt.Text)
                        M_Dt = M_CommonBL.Update_AccNoTran(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Session("TransDate"), Session("DefaultBrCode"), TxtRemarks.Text, Session("LoginID"), AccID.Value, TxtAccNo.Text, M_TranSw.ToString, Val(TxtShareNo.Text), , , , , , M_BatchNo, Val(HdnReferAccID.Value), IIf(CmbSchTrans.SelectedItem.Text = "Closure", True, False), TxtRemarks1.Text, CmbSchTrans.SelectedItem.Text, "TRAN", CmbCashTransfer.SelectedItem.Text, HdnTrfrAccID.Value, TxtTrfrRemarks.Text)
                    Else
                        M_Dt = M_CommonBL.Update_AccNoTran(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Session("TransDate"), Session("DefaultBrCode"), TxtRemarks.Text, Session("LoginID"), AccID.Value, TxtAccNo.Text, M_TranSw.ToString, TxtInstNo.Text.Trim, TxtTockenNo.Text, , _
                                                           ChqNo, ChqDate, , M_BatchNo, Val(HdnReferAccID.Value), IIf(CmbSchTrans.SelectedItem.Text = "Closure", True, False), TxtRemarks1.Text, CmbSchTrans.SelectedItem.Text, "TRAN", CmbCashTransfer.SelectedItem.Text, HdnTrfrAccID.Value, TxtTrfrRemarks.Text)
                    End If

                    If M_Dt.Rows.Count > 0 Then

                        Tran_No = M_Dt.Rows(0)("Tran_No").ToString()
                        If Session("Tran_No") = "" Then
                            Session("Tran_No") = Tran_No
                        Else
                            Session("Tran_No") = Session("Tran_No") + "," + Tran_No
                        End If
                        If Session("Acc_No") = "" Then
                            Session("Acc_No") = TxtAccNo.Text
                        Else
                            Session("Acc_No") = Session("Acc_No") + "," + TxtAccNo.Text
                        End If
                        Tran_No = Session("Tran_No")
                        M_BatchNo = M_Dt.Rows(0)("Batch_No").ToString()
                        ShowAlertMessage("UPDATED")
                    Else
                        ShowAlertMessage("FAILED")
                    End If
                End If

            Else  'Transaction Type=Account Head
                Dim M_Dt1 As New DataTable
                M_Dt1 = M_CommonBL.Check_CashBank_Balance(Session("DefaultBrCode"), Session("TransDate"), M_TranSw.ToString)
                If M_Dt1.Rows(0).Item("Status") = "Y" Then
                    ShowAlertMessage(M_Dt1.Rows(0).Item("Narration").ToString)
                    Session("BalanceControll") = "Y"
                    Exit Sub
                Else
                    Session("BalanceControll") = "N"
                End If

                M_Dt = M_CommonBL.Update_AccHeadTran(DrpToBrnch.SelectedValue, Session("TransDate"), Session("DefaultBrCode"), TxtRemarks.Text, Session("LoginID"), M_TranSw.ToString, TxtRemarks1.Text, , M_BatchNo, TxtTockenNo.Text, txtCntraAccCode.Text)
                If M_Dt.Rows.Count > 0 Then

                    Tran_No = M_Dt.Rows(0)("Tran_No").ToString()
                    If Session("Tran_No") = "" Then
                        Session("Tran_No") = Tran_No
                    Else
                        Session("Tran_No") = Session("Tran_No") + "," + Tran_No
                    End If
                    If Session("Acc_No") = "" Then
                        Session("Acc_No") = TxtAccNo.Text
                    Else
                        Session("Acc_No") = Session("Acc_No") + "," + TxtAccNo.Text
                    End If
                    Tran_No = Session("Tran_No")
                    M_BatchNo = M_Dt.Rows(0)("Batch_No").ToString()
                    ShowAlertMessage("UPDATED")
                Else
                    ShowAlertMessage("FAILED")
                End If
                If M_Dt.Rows.Count > 0 And TxtCustID.Text.Length > 0 Then
                    Tran_No = M_Dt.Rows(0)("Tran_No").ToString()
                    Dim Dt As New DataTable
                    ViewState("CustId") = ""
                    ViewState("CustName") = ""
                    Dt = M_CommonBL.InsertCustDtlsForVoucher(TxtCustID.Text, TxtCustName.Text, Tran_No)
                    ShowAlertMessage("UPDATED")
                Else
                    ShowAlertMessage("FAILED")
                End If
            End If
            'Print_Voucher(Tran_No)
            Session("BatchNo") = M_BatchNo
            M_CommonBL = Nothing
            'Clear_Controls()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
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
    Protected Function AccountHead() As DataTable
        Dim M_dt As New DataTable
        M_dt = Session("AccountHeadTbl")
        Return M_dt
    End Function
    Protected Sub CmdBatch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdBatch.Click
        Try
            Dim Tran_No As String = ""
            Dim M_CommonBL As New CommonBL

            SaveData(Tran_No)

            Clear_Controls()


            CmbCashTransfer.Enabled = False

            M_CommonBL.Fill_BatchNo(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo)
            CmbBatchNo.SelectedValue = Session("BatchNo")
            CmbBatchNo_SelectedIndexChanged(Me.CmbBatchNo, System.EventArgs.Empty)

            CmbBatchNo.Enabled = False

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub CmbBatchNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBatchNo.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As DataTable

            If CmbBatchNo.SelectedItem.Text <> "-- NEW --" Then
                M_Dt = M_CommonBL.Get_BatchTotal(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo.SelectedItem.Text)
                If M_Dt.Rows.Count > 0 Then
                    LblDebit.Text = Format(Val(M_Dt.Rows(0)("Debit").ToString), "#0.00") & " Dr"
                    LblCredit.Text = Format(Val(M_Dt.Rows(0)("Credit").ToString), "#0.00") & " Cr"
                End If
            Else
                LblDebit.Text = ""
                LblCredit.Text = ""
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try
    End Sub

    Private Sub GetAuthoriseDtls(ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        Dim M_TranTbl As DataTable
        Dim M_SecTbl As DataTable

        M_Dataset = M_CommonBL.GetAuthoriseDtls("TRAN", TransID)
        M_CommonBL = Nothing

        M_TranTbl = M_Dataset.Tables(0)

        If M_TranTbl.Rows(0).Item("SchVou") = "VOU" Then
            CmbTransType.SelectedIndex = 1
            AccNoDiv.Visible = False
            AcHeadDiv.Visible = True
            DgvTransaction.DataSource = M_Dataset.Tables(1)
            Session("locAcHeadTbl") = M_Dataset.Tables(1)
            DgvTransaction.DataBind()
            TxtRemarks.Text = M_TranTbl.Rows(0).Item("Narration")
            TxtRemarks1.Text = M_TranTbl.Rows(0).Item("Narration1")
            M_CommonBL = Nothing
        Else
            CmbBranch.SelectedValue = M_TranTbl.Rows(0).Item("Br_Code")
            CmbScheme.SelectedValue = M_TranTbl.Rows(0).Item("Sch_Code")

            TxtSchCode.Text = CmbScheme.SelectedValue
            Set_SchemeCtrl()
            AccID.Value = M_TranTbl.Rows(0).Item("Acc_ID")
            TxtAccNo.Text = M_TranTbl.Rows(0).Item("Acc_No")
            TxtShortCode.Text = M_TranTbl.Rows(0).Item("Acc_No")
            TxtRemarks.Text = M_TranTbl.Rows(0).Item("Narration")
            TxtRemarks1.Text = M_TranTbl.Rows(0).Item("Narration1")
            TxtInstNo.Text = M_TranTbl.Rows(0).Item("Chq_No")
            TxtInstrDate.Text = M_TranTbl.Rows(0).Item("Chq_Date")
            If TxtInstrDate.Text = "01/01/1900" Then TxtInstrDate.Text = ""
            DgvTransaction.DataSource = M_Dataset.Tables(1)
            Session("locAcHeadTbl") = M_Dataset.Tables(1)
            DgvTransaction.DataBind()
            M_CommonBL = Nothing
            GetAccountTranDtls()




            TxtTransAmt.Text = M_TranTbl.Rows(0).Item("Tran_Amt")
            CmbSchTrans.Text = M_TranTbl.Rows(0).Item("Tran_Type")

            If M_TranTbl.Rows(0).Item("Tran_Type") = "Renewal" Then
                DivFDRENEW.Visible = True
                NomDiv.Visible = True
                StandDiv.Visible = True
                TxtRenewPrnAmt.Text = M_TranTbl.Rows(0).Item("OldPrAmt")
                TxtRenewIntAmt.Text = M_TranTbl.Rows(0).Item("OldIntAmt")
                If M_TranTbl.Rows(0).Item("Renewtype") = "Y" Then
                    CmbRenewType.SelectedIndex = 0
                Else
                    CmbRenewType.SelectedIndex = 1
                End If
                TxtRenewEffDate.Text = M_TranTbl.Rows(0).Item("EffectDate")
                TxtRenewMonths.Text = M_TranTbl.Rows(0).Item("Months")
                TxtRenewDays.Text = M_TranTbl.Rows(0).Item("Days")
                TxtRenewDueDate.Text = M_TranTbl.Rows(0).Item("DueDate")
                TxtRenewIntRate.Text = M_TranTbl.Rows(0).Item("IntRate")
                TxtRenewAddiAmt.Text = M_TranTbl.Rows(0).Item("AdnlAmt")
                TxtRenewAmt.Text = M_TranTbl.Rows(0).Item("RenewAmt")
                Ddl_SI.SelectedIndex = M_TranTbl.Rows(0).Item("StInst")
                If Ddl_SI.SelectedIndex = 1 Then
                    CmbStandBr.SelectedValue = M_TranTbl.Rows(0).Item("StBranch")
                    CmbStandSch.SelectedValue = M_TranTbl.Rows(0).Item("StScheme")
                    TxtStandAcNo.Text = M_TranTbl.Rows(0).Item("StAccNo")
                    TxtStandAcHoldenName.Text = M_TranTbl.Rows(0).Item("StName")
                    TxtStandAddress.Text = M_TranTbl.Rows(0).Item("StAddress")
                End If
                CmbNomReq.SelectedIndex = M_TranTbl.Rows(0).Item("Nominee")
                If CmbNomReq.SelectedIndex = 1 Then
                    Txt_NomName.Text = M_TranTbl.Rows(0).Item("NomineeName")
                    CmbNomRel.SelectedValue = M_TranTbl.Rows(0).Item("NomineeRelation")
                    Txt_NomAdd.Text = M_TranTbl.Rows(0).Item("NomineeAddress")
                    Txt_NomPh.Text = M_TranTbl.Rows(0).Item("NomineePhone")
                End If
                CmbRenewIntPayMode.SelectedValue = M_TranTbl.Rows(0).Item("IntPayMode")
                CmbRenewPrivilage.SelectedValue = M_TranTbl.Rows(0).Item("Privilage")
            End If
        End If
        Dim M_CashTransfer As DataTable
        M_CashTransfer = M_Dataset.Tables(2)
        CmbCashTransfer.Text = M_CashTransfer.Rows(0).Item("Cash_Transfer")

        PnlAuthorise.Enabled = False
        PnlAuthorise1.Enabled = False
        PnlAuthorise2.Enabled = False
        PnlAuthorise3.Enabled = False
        CmdSave.Enabled = True
        CmdBatch.Enabled = False
        CmdClear.Enabled = True
        CmdClear0.Enabled = False
        Calc_TranTotal()

    End Sub

    Private Sub CheckSureties()
        Dim M_LoanAppBL As New LoanApplicationBL
        Dim Msg As String = ""
        Dim DS_Security As DataSet
        DS_Security = M_LoanAppBL.Check_Surety(AccID.Value)
        If DS_Security.Tables(0).Rows.Count = 0 Then
            ShowAlertMessage("Security Details  Not Added.")
        End If
        DS_Security = Nothing
    End Sub

    Protected Sub TxtInvestIntAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtInvestIntAmt.TextChanged
        If Val(AccID.Value) = 0 Then Exit Sub
        Set_TranAmt()
    End Sub

    Protected Sub TxtRebate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtRebate.TextChanged
        Set_TranAmt()
    End Sub

    Protected Sub TxtTrfrAcNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTrfrAcNo.TextChanged
        Try
            Dim Dt_AcDtlsN As DataTable
            Dim Dt_AcDtls As DataTable
            Dim M_CommonBL As New CommonBL
            TxtTrfrAcNo.Text = Right("000" + CmbTrfrBr.SelectedValue, 3) + Right("000" + CmbTrfrSch.SelectedValue, 3) + Right("0000000" + TxtTrfrAcNo.Text, 7)
            Dim M_AccNo As String = TxtTrfrAcNo.Text

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbTrfrBr.SelectedValue, CmbTrfrSch.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count = 0 Then
                HdnTrfrAccID.Value = "0"
                ShowAlertMessage("Invalid Account Number")
                TxtTrfrAcNo.Text = ""
                TxtTrfrAcNo.Focus()
                M_CommonBL = Nothing
                Exit Sub
            End If

            HdnTrfrAccID.Value = "0"
            Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbTrfrBr.SelectedValue, CmbTrfrSch.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtlsN.Rows.Count > 0 Then
                TxtTrfrAcHoldenName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
                TxtTrfrAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
                Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
                TxtTrfrBalAmt.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")
                HdnTrfrAccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                CmbTrfrSch.SelectedValue = Dt_AcDtls.Rows(0)("Br_Code").ToString()
                CmbTrfrBr.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
            End If

            M_CommonBL = Nothing

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtTrfrRemarks_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTrfrRemarks.TextChanged

    End Sub

    Protected Sub ChkQuickTransfer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkQuickTransfer.CheckedChanged
        Try
            If ChkQuickTransfer.Checked = True Then
                TrfrDiv.Visible = True
            Else
                TrfrDiv.Visible = False
            End If
            Session("SearchType") = "TRACCOUNTNO"
            Session("SearchScheme") = CmbTrfrSch.SelectedValue
            Session("SearchBranch") = CmbTrfrBr.SelectedValue
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSection.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        'Session("SearchSection") = CmbSection.SelectedItem.ToString
        If CmbSection.Text = "GROUP DEPOSIT" Then
            CmbScheme.Enabled = False
        Else
            Try

                M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.ToString)
                TxtSchCode.Text = CmbScheme.SelectedValue
                Set_SchemeCtrl()
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try

        End If
    End Sub

    Protected Sub CmbTrfrSch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTrfrSch.SelectedIndexChanged
        Session("SearchType") = "TRACCOUNTNO"
        Session("SearchScheme") = CmbTrfrSch.SelectedValue
        Session("SearchBranch") = CmbTrfrBr.SelectedValue
    End Sub

    Protected Sub CmbTrfrBr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTrfrBr.SelectedIndexChanged
        Session("SearchType") = "TRACCOUNTNO"
        Session("SearchScheme") = CmbTrfrSch.SelectedValue
        Session("SearchBranch") = CmbTrfrBr.SelectedValue
    End Sub

    Protected Sub TxtTransfer_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTransfer.TextChanged
        Dim M_CommonCls As New CommonClass
        LblWords.Text = M_CommonCls.Towords(Val(TxtAcHeadTransAmt.Text) + Val(TxtTransfer.Text)).ToUpper
        M_CommonCls = Nothing
    End Sub


    Private Sub Load_CustomerData()
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PhotoID As New SMARTCORE_BO.CustIdentityBO
            Dim M_ADDID As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL

            M_CustMaster = M_CustMasterBL.Locate_Data(Val(TxtCustID.Text), M_PresAddress, M_PermAddress, M_PhotoID, M_ADDID)
            Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtCustID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustID.TextChanged
        Load_CustomerData()
    End Sub


    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        On Error Resume Next
        If M_CustMaster.CustStatus = "I" Or M_CustMaster.CustStatus = "D" Then
            ShowAlertMessage("CUSTOMER IS NOT ACTIVE")
            Exit Sub
        End If

        With M_CustMaster
            TxtCustName.Text = M_CustMaster.CustName
        End With
    End Sub

    Protected Sub ChkChaneCash_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkChaneCash.CheckedChanged
        If ChkChaneCash.Checked = True Then
            ChangeCashDiv.Visible = True
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            Dim M_Dt1 As New DataTable
            'M_Dt = M_CommonBl.Get_SchemeTranHeads(CmbScheme.SelectedValue)

            Dim CashHeads As DataTable
            CashHeads = M_CommonBl.Fill_ChangeCashHeads()
            'gvDetails.DataSource = CashHeads
            Session("ChangeCash") = CashHeads
            'gvDetails.DataBind()

            M_Dt = Session("locAcHeadTbl")
            Dim Row1 As DataRow

            If M_Dt.Rows.Count > 0 Then
                'ChkTranHeads.DataSource = M_Dt
                'ChkTranHeads.DataTextField = "Display"
                'ChkTranHeads.DataValueField = "Acc_Code"
                'ChkTranHeads.DataBind()
                M_Dt1.Columns.Add("AccCode")
                M_Dt1.Columns.Add("AccHead")
                M_Dt1.Columns.Add("PayMode")
                M_Dt1.Columns.Add("Cash")
                M_Dt1.Columns.Add("Transfer")
                M_Dt1.Columns.Add("Total")
                M_Dt1 = DirectCast(Session("ChangeCash"), DataTable)

                Dim i As Integer = M_Dt.Rows.Count

                For i = 0 To M_Dt.Rows.Count - 1 Step 1
                    Row1 = M_Dt1.NewRow
                    Row1("AccCode") = M_Dt.Rows(i).Item("AccCode").ToString
                    Row1("AccHead") = M_Dt.Rows(i).Item("AccHead").ToString
                    If Val(M_Dt.Rows(i).Item("RctTransfer")) + Val(M_Dt.Rows(i).Item("RctCash")) > 0 Then
                        Row1("PayMode") = "R"
                    Else
                        Row1("PayMode") = "P"
                    End If

                    Row1("Cash") = Val(M_Dt.Rows(i).Item("PayCash")) + Val(M_Dt.Rows(i).Item("RctCash"))
                    Row1("Transfer") = Val(M_Dt.Rows(i).Item("RctTransfer")) + Val(M_Dt.Rows(i).Item("PayTransfer"))
                    Row1("Total") = Val(M_Dt.Rows(i).Item("RctTransfer")) + Val(M_Dt.Rows(i).Item("PayTransfer")) + Val(M_Dt.Rows(i).Item("PayCash")) + Val(M_Dt.Rows(i).Item("RctCash"))
                    ' syntax error here
                    Try
                        Dim Expression As String = "AccCode=''"
                        If M_Dt1.[Select](Expression).Count > 0 Then M_Dt1.Rows.RemoveAt(M_Dt1.Rows.IndexOf(M_Dt1.[Select](Expression)(0)))
                    Catch ex As Exception
                        ShowAlertMessage(Err.Description)
                    End Try

                    M_Dt1.Rows.Add(Row1)
                Next
                Session("ChangeCash") = M_Dt1
                gvDetails.DataSource = M_Dt1
                gvDetails.DataBind()


                ChangeCashDiv.Visible = True
            End If
        Else
            ChangeCashDiv.Visible = False
        End If

    End Sub
    Protected Sub gvDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetails.RowCommand
        Try





            If e.CommandName.ToUpper = "EDIT" Then
                Dim AccCode As String
                Dim AccHead As String
                Dim PayMode As String
                Dim Total As Double
                Dim Cash As Double
                Dim Transfer As Double
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim M_Dt As New DataTable
                Dim row As GridViewRow = gvDetails.Rows(index)

                M_Dt = DirectCast(Session("locAcHeadTbl"), DataTable)

                AccCode = gvDetails.DataKeys(index)("AccCode").ToString()
                AccHead = gvDetails.DataKeys(index)("AccHead").ToString()
                PayMode = gvDetails.DataKeys(index)("PayMode").ToString()
                Total = Val(gvDetails.DataKeys(index)("Total").ToString())
                Dim TotalCashSum As Decimal = gvDetails.Rows.Cast(Of GridViewRow)().
        Where(Function(r) r.RowType = DataControlRowType.DataRow).
        Sum(Function(r)
                Dim txt As TextBox = CType(r.FindControl("TxtChangeCash"), TextBox)
                If txt IsNot Nothing AndAlso Not String.IsNullOrEmpty(txt.Text) Then
                    Return Convert.ToDecimal(txt.Text)
                End If
                Return 0D
            End Function)
                Dim TxtCash As TextBox = TryCast(row.FindControl("TxtChangeCash"), TextBox)
                Dim TxtTransfer As TextBox = TryCast(row.FindControl("TxtChangeTran"), TextBox)
                Dim CashHandleChrg As Decimal = 0
                If AccHead = "CASH HANDLING CHARGES" Then PayMode = "R"
                If TotalCashSum > 0 Then
                    Dim DtCalcCashHndlchrg As New DataTable
                    Dim objCommonBL As New CommonBL
                    DtCalcCashHndlchrg = objCommonBL.CalcCashHndlchrg(CmbScheme.SelectedValue, TxtAccNo.Text, TotalCashSum)
                    If DtCalcCashHndlchrg.Rows.Count > 0 Then
                        If Not IsDBNull(DtCalcCashHndlchrg.Rows(0).Item("CashHndlChrg")) Then
                            CashHandleChrg = Val(DtCalcCashHndlchrg.Rows(0).Item("CashHndlChrg"))
                        End If
                    End If
                End If
                Cash = Val(TxtCash.Text)
                Transfer = Val(TxtTransfer.Text)

                'If AccHead <> "CASH HANDLING CHARGES" Then 'Avoid validation instructed by Mr.Rajesh
                If Cash + Transfer <> Total Then
                    ShowAlertMessage("Please Check the Total Amount")
                    Exit Sub
                End If
                'End If

                If PayMode = "R" Then
                    M_Dt.Rows(index).Item(3) = Format(Val(Cash), "#0.00")
                    M_Dt.Rows(index).Item(4) = Format(Val(Transfer), "#0.00")
                    If TotalCashSum > 0 Then
                        Dim dtrow = M_Dt.AsEnumerable().
                         FirstOrDefault(Function(x) x.Field(Of String)("AccHead").Trim() = "CASH HANDLING CHARGES")

                        If dtrow IsNot Nothing Then
                            dtrow("RctCash") = Convert.ToDecimal(CashHandleChrg)
                        End If
                    End If
                ElseIf PayMode = "P" Then
                    M_Dt.Rows(index).Item(5) = Format(Val(Cash), "#0.00")
                    M_Dt.Rows(index).Item(6) = Format(Val(Transfer), "#0.00")
                End If
                M_Dt.AcceptChanges()
                'CashChange(M_Dt)
                'Dim Mdt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub
    'Private Sub CashChange(ByRef TranData As DataTable)
    '    Dim x As New DataTable()
    '    x = TranData
    '    '  DgvTransaction.DataSource = Nothing
    '    DgvTransaction.DataSource = x
    '    DgvTransaction.DataBind()
    '    Session("locAcHeadTbl") = x
    '    Calc_TranTotal()
    'End Sub
    Protected Sub gvDetails_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDetails.RowEditing
        Dim M_Dt As New DataTable
        M_Dt = DirectCast(Session("locAcHeadTbl"), DataTable)
        DgvTransaction.DataSource = M_Dt
        DgvTransaction.DataBind()
        Calc_TranTotal()
    End Sub
    Protected Sub CkBankDetails_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CkBankDetails.CheckedChanged
        Try
            If CkBankDetails.Checked = True Then
                DivBank.Visible = True
            Else
                DivBank.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles But_verfiy.Click
        Try
            Dim M_CustMasterBL As New CustMasterBL
            Dim M_Datatable As New DataTable
            Dim MobileNo As String = TxtMobileNo.Text
            M_Datatable = M_CustMasterBL.OTPGenrate(MobileNo)
            Session("ODPTBL") = M_Datatable
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('img_process');show('cover');show('VerifyMob');", True)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub But_PopUpverify_Click(sender As Object, e As EventArgs) Handles But_PopUpverify.Click
        Dim M_CustMasterBL As CustMasterBL
        Dim OTPNO As String = txt_verifycode.Text
        Dim M_Datatable As New DataTable
        M_Datatable = Session("ODPTBL")

        Dim OTPCODE As String = M_Datatable.Rows(0)("OTP")
        Dim OtpDate As Date = Convert.ToDateTime(M_Datatable.Rows(0)("GenTime"))
        Session("OTPMOB") = M_Datatable.Rows(0)("MobileNo")
        OtpDate = OtpDate.AddMinutes(20)
        Dim Nowdate As Date = Now.ToString()

        If OtpDate >= Nowdate And TxtMobileNo.Text = Session("OTPMOB") Then
            If OTPNO = OTPCODE Then
                Hid_ODPStatus.Value = "Y"
                Session("ODPTBL") = Nothing
                txt_verifycode.Text = ""
                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('cover');hide('VerifyMob');hide('div_Verify');hide('imgverifyError');show('imgVerify');hide('lbl_sessionexp');hide('lbl_errMsg');  ", True)
            Else
                'ImgEditNo.Attributes.Add("style", "Visibility:false")

                '  ErrorMessage("Enter Correct OTP Code")
                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');hide('lbl_sessionexp');show('lbl_errMsg');", True)
            End If
        Else
            Session("ODPTBL") = Nothing
            ' ImgEditNo.Attributes.Add("style", "Visibility:False")
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');hide('div_Verify');show('imgverifyError');hide('imgVerify');show('lbl_sessionexp');hide('lbl_errMsg');", True)
        End If
    End Sub

End Class
