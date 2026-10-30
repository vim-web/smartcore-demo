Imports SMARTCORE_BL
Imports SMARTCORE_Common

Partial Public Class InvestmentMaster
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private EntryMode As String
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Shared M_AccHead As String
    Shared InvParameterDt As DataTable
    Public MultipleDepTbl As New DataTable("Table")
    Dim FormID As Integer = 1076
    Shared Obj_UserRights As Object

    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
            M_CommonBL.Fill_Pickup(CmbInterestPayMode, 44, 0)
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "INVESTMENT")

            DgvTransaction.DataSource = Nothing
            Dim Dt As DataTable
            Dt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
            DgvTransaction.DataSource = Dt
            DgvTransaction.DataBind()
            PickupDt = DirectCast(Cache("PickupTbl"), DataTable)

            CmbCashTransfer.Items.Add("Cash")
            CmbCashTransfer.Items.Add("Transfer")
            CmbCashTransfer.SelectedIndex = 0

            M_CommonBL = Nothing

            Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Set_Grid()
        Try
            Dim column As New DataColumn()
            column.DataType = System.Type.[GetType]("System.Int32")
            column.AutoIncrement = True
            column.AutoIncrementSeed = 1000
            column.AutoIncrementStep = 1
            column.ColumnName = "SlNo"
            ' Add the column to a new DataTable.
            MultipleDepTbl.Columns.Add("Dep_Mth", GetType(Double))
            MultipleDepTbl.Columns.Add("Dep_Days", GetType(Double))
            MultipleDepTbl.Columns.Add("Dep_DueDt", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_Amt", GetType(Double))
            MultipleDepTbl.Columns.Add("Dep_IntRate", GetType(Double))
            MultipleDepTbl.Columns.Add("Dep_IntPayMode", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_NomNm", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_NomRel", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_NomAdd", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_Standing", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_IntPayModeCode", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_StndBr", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_StndSch", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_StndAccNo", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_NomRelCode", GetType(Double))
            MultipleDepTbl.Columns.Add("Dep_NomPh", GetType(String))
            MultipleDepTbl.Columns.Add("Dep_MatureAmt", GetType(Double))
            MultipleDepTbl.Columns.Add(column)
            MultipleDepTbl.Rows.Add(0, 0, "01/01/1900", 0, 0, "", "", "", "", "", "", "", "", "", 0, "", 0)


            Session("DepTbl") = MultipleDepTbl
            DgvDepDtls.DataSource = MultipleDepTbl
            DgvDepDtls.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Private Sub Assign_Values()
        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
        CmbOpMode.SelectedValue = Session("AcOpenOpMode")
        TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
        Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
        DgvCustomer.DataSource = dt
        DgvCustomer.DataBind()

        Dim M_CommonBL As New CommonBL
        M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)
        InvParameterDt = M_CommonBL.GetInvestmentParameter(CmbScheme.SelectedValue)

        HdnDoublingPeriod.Value = 0

        AgentCollection.Value = "N"
        If InvParameterDt.Rows.Count > 0 Then
            AgentCollection.Value = InvParameterDt.Rows(0)("Dep_AgentCollection").ToString
            Txt_Month.Text = InvParameterDt.Rows(0)("Dep_MinPeriod").ToString
            HdnDoublingPeriod.Value = InvParameterDt.Rows(0)("Dep_DoublingPeriod").ToString

            If Val(HdnDoublingPeriod.Value) > 0 Then ' Doubling Scheme
                CmbInterestPayMode.SelectedValue = "623"
            End If

            Txt_InitialDeposit.Text = "0"
            Calc_InterestRate()
        End If

        Ce_OpeningDate.Text = Session("TransDate")
        Ce_EffectiveDt.Text = Session("TransDate")
        Ce_DueDate.Text = Session("TransDate")
        Ce_IntUpto.Text = Session("TransDate")
        M_CommonBL = Nothing
        Set_SchemeCtrl()
        Set_TransactionGrid()
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


            Load_DefaultValues()



            If Len(Request.QueryString("TRANID")) > 0 Then
                GetAuthoriseDtls("INVMAS", Request.QueryString("TRANID"))
            Else
                Assign_Values()
            End If

            ' CmdSave.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Session("M_EntryMode") = "NEW"

            Ce_DueDate.Attributes.Add("readonly", "readonly")
            Ce_OpeningDate.Attributes.Add("readonly", "readonly")
            Ce_IntUpto.Attributes.Add("readonly", "readonly")
            Txt_MaturityAmt.Attributes.Add("readonly", "readonly")
            Call Calc_DueDate()

            'ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "enableDisableControls();", True)
        End If
    End Sub

    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("INVMAS", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        M_TranTbl = M_Dataset.Tables(0)
        DgvTransaction.DataSource = M_Dataset.Tables(1)
        ' Session("TransTbl") = M_Dataset.Tables(1)
        DgvTransaction.DataBind()
        M_CommonBL = Nothing
        GetAccountDtls(M_TranTbl)
        PnlAuthorise.Enabled = False
        'Calc_TranTotal()
        DgvCustomer.DataSource = M_Dataset.Tables(2)
        DgvCustomer.DataBind()
        TxtNoOfOperator.Text = M_Dataset.Tables(2).Rows.Count
    End Sub

    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable)
        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        CmbOpMode.SelectedValue = M_AccTbl.Rows(0).Item("Dm_OperationMode")
        CmbScheme.SelectedValue = M_AccTbl.Rows(0).Item("Sch_Code")
        TxtNoOfOperator.Text = M_AccTbl.Rows(0).Item("NoOfOp")
        Ce_OpeningDate.Text = M_AccTbl.Rows(0).Item("Dm_DepDate")
        Ce_EffectiveDt.Text = M_AccTbl.Rows(0).Item("Dm_WithEffDate")
        Ce_DueDate.Text = M_AccTbl.Rows(0).Item("Dm_DueDate")
        Txt_Month.Text = M_AccTbl.Rows(0).Item("Dm_Months")
        Txt_Days.Text = M_AccTbl.Rows(0).Item("Dm_Days")
        CmbInterestPayMode.SelectedValue = M_AccTbl.Rows(0).Item("Dm_IntPayMode")
        Txt_InitialDeposit.Text = M_AccTbl.Rows(0).Item("Dm_DepAmount")
        Txt_IntPercent.Text = M_AccTbl.Rows(0).Item("Dm_IntRate")
        Txt_MaturityAmt.Text = M_AccTbl.Rows(0).Item("Dm_MaturityAmt")
        CmbCashTransfer.SelectedIndex = M_AccTbl.Rows(0).Item("Cash")
        Txt_BankAcNo.Text = M_AccTbl.Rows(0).Item("BankAccNo")

    End Sub

    Private Function Validate_Control() As Boolean

        If SchModuleCatg.Value = "FD" Or SchModuleCatg.Value = "RD" Or SchModuleCatg.Value = "DD" Then
            If Val(Txt_Month.Text) + Val(Txt_Days.Text) = 0 Then
                Txt_Month.Focus()
                ShowAlertMessage("INVALID DEPOSIT PERIOD ENTERED")
                Return False
            End If
            If Val(Txt_InitialDeposit.Text) <= 0 And SchModuleCatg.Value <> "DD" Then
                Txt_InitialDeposit.Focus()
                ShowAlertMessage("INVALID DEPOSIT AMOUNT ENTERED")
                Return False
            End If
            If Val(Txt_IntPercent.Text) <= 0 Then
                Txt_IntPercent.Focus()
                ShowAlertMessage("INVALID DEPOSIT INT RATE ENTERED")
                Return False
            End If

        ElseIf SchModuleCatg.Value = "SB" Then
            If Val(Txt_InitialDeposit.Text) <= 0 Then
                Txt_InitialDeposit.Focus()
                ShowAlertMessage("INVALID DEPOSIT AMOUNT ENTERED")
                Return False
            End If
        End If

        Return True
    End Function
    Private Shared CommCls As New CommonClass()
    Protected Sub Btn_Save_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim SplitAcdt As DataTable
        Dim M_CommonBL As New CommonBL
        If Len(Request.QueryString("TRANID")) > 0 Then

            If M_CommonBL.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                ShowAlertMessage("Authotisation Failed")
                Response.Redirect("~/ManagerPassing.aspx.vb")
                Exit Sub
            End If
            M_CommonBL = Nothing
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
            Exit Sub
        End If




        If Validate_Control = False Then
            Exit Sub
        End If

        Dim M_InvestmentMaster As New SMARTCORE_BO.InvestmentMasterBO
        Try
            With M_InvestmentMaster
                .AccID = M_AccID
                .CustId = Val(DgvCustomer.Rows(0).Cells(4).Text)
                .DmMonths = Val(Txt_Month.Text)
                .DmDays = Val(Txt_Days.Text)
                .DmNomNos = 0
                .DmOperationMode = CmbOpMode.SelectedValue
                .DmAgentCode = 0
                .DmPrevilage = Session("AcOpenPrivelage")
                .DmIntPayMode = CmbInterestPayMode.SelectedValue
                .LogID = "1"
                .DmRenewalDays = "0"
                .DmRenewalMonths = "0"

                .DmDepDate = Ce_OpeningDate.Text
                .DmWithEffDate = Ce_EffectiveDt.Text
                .DmDueDate = Ce_DueDate.Text
                .DmLastIntDate = Ce_IntUpto.Text
                .DmLastUpdate = Ce_IntUpto.Text

                .DmOpIntdate = "01/01/1900"
                .DmClosedDate = "01/01/1900"
                .MakingTime = DateTime.Now
                .CheckingTime = "01/01/1900"
                .Cashiertime = "01/01/1900"

                .DmDepAmount = Val(Txt_InitialDeposit.Text)
                .DmBalance = Val(Txt_InitialDeposit.Text)
                .DmMaturityAmt = Val(Txt_MaturityAmt.Text)
                .DmIntRate = Val(Txt_IntPercent.Text)
                .DmOpBal = 0
                .DmOpIntrate = Val(Txt_IntPercent.Text)
                .AccNo = M_AccNo
                .BaseAccNo = M_AccNo
                .SchCode = CmbScheme.SelectedValue
                .BrCode = CmbBranch.SelectedValue
                .AccCode = M_AccHead
                .DmBondNo = ""
                .CashierID = ""
                .BankAccNo = Txt_BankAcNo.Text
                .DmCanvassedBy = 0
                .DmAutoRenewal = Ddl_AutoRenew.SelectedValue
                .DmRemarks = ""
                .DmIntroducedby = Txt_IntroBy.Text
                .DmTransferTo = Txt_MatAcNo.Text
                .TranNo = ""
                .MakerID = Session("UserID")
                .CheckerID = ""
                .DmBalType = "C"
                .DmTODStatus = "Y"
                .DmOpBalType = "C"
                .DmChqIssueStatus = Ddl_ChqIssue.SelectedValue
                .DmLinkedStatus = "N"
                .DmStatus = "Y"
                .Authorised = "N"

            End With

            Dim Row As DataRow
            SplitAcdt = DirectCast(Session("DepTbl"), DataTable)

            Dim Expression As String = "Dep_Amt=0"
            If SplitAcdt.[Select](Expression).Count > 0 Then SplitAcdt.Rows.RemoveAt(SplitAcdt.Rows.IndexOf(SplitAcdt.[Select](Expression)(0)))

            Row = SplitAcdt.NewRow
            Row("Dep_Mth") = Val(Txt_Month.Text)
            Row("Dep_Days") = Val(Txt_Days.Text)
            Row("Dep_DueDt") = Replace(CommCls.DbDate(Ce_DueDate.Text), "'", "") ' Format(Ce_DueDate.Text, "MM/dd/yyyy")
            Row("Dep_Amt") = Val(Txt_InitialDeposit.Text)
            Row("Dep_IntRate") = Val(Txt_IntPercent.Text)
            Row("Dep_IntPayMode") = CmbInterestPayMode.SelectedItem.Text

            Row("Dep_NomNm") = ""
            Row("Dep_NomRel") = ""
            Row("Dep_NomAdd") = ""

            Row("Dep_Standing") = ""
            Row("Dep_IntPayModeCode") = CmbInterestPayMode.SelectedValue
            Row("Dep_StndBr") = ""
            Row("Dep_StndSch") = ""
            Row("Dep_StndAccNo") = ""
            Row("Dep_NomRelCode") = 0
            Row("Dep_NomPh") = ""
            Row("Dep_MatureAmt") = Val(Txt_MaturityAmt.Text)

            Try
                SplitAcdt.Rows.Add(Row)
            Catch ex As Exception

            End Try

            Session("DepTbl") = SplitAcdt

            Dim AccHolderdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_AccHolderSw As New IO.StringWriter
            AccHolderdt.WriteXml(M_AccHolderSw)

            Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Trandt.WriteXml(M_TranSw)

            Dim M_NomineeSw As New IO.StringWriter
           
            Session("NomTbl") = ""

            Dim M_InvestmentMasterBL As New InvestmentMasterBL
            Dim M_Dt As New DataTable

            SplitAcdt = DirectCast(Session("DepTbl"), DataTable)
            Dim M_SplitAcSw As New IO.StringWriter
            SplitAcdt.WriteXml(M_SplitAcSw)

            Session("M_EntryMode") = "NEW"
            M_Dt = M_InvestmentMasterBL.Save_Data(M_InvestmentMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), _
                        M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), _
                        M_SplitAcSw.ToString.Replace("DocumentElement", "NewDataSet"))
            ShowAlertMessage(" Investment NO : " & M_Dt.Rows(0)("Acc_No").ToString)
            Session("NomTbl") = Nothing
            ' Bond Printing TO Do
            'If Session("SECTION") = "FD" Then
            '    Call PrintBond_FD(M_Dt.Rows(0)("Tran_No").ToString)
            'End If

            'Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & IIf(SchModuleCatg.Value = "FD", "&PrintBond=Y", "&PrintBond=N") & "&SECTION=" & SchModuleCatg.Value & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & SchSection.Value & "&DRCR=" & "C")
            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=" & SchModuleCatg.Value & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & SchSection.Value & "&DRCR=" & "C")
            M_InvestmentMasterBL = Nothing
            'Response.Redirect("~/AccOpen.aspx")

        Catch ex As Exception
            ShowAlertMessage(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub Txt_InitialDeposit_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_InitialDeposit.TextChanged
        Try
            Set_TransactionGrid()
            Calc_InterestRate()
            Dim M_CommonCls As New CommonClass
            LblWords.Text = M_CommonCls.Towords(Val(Txt_InitialDeposit.Text)).ToUpper
            M_CommonCls = Nothing
            Txt_InitialDeposit.Text = Format(Val(Txt_InitialDeposit.Text), "#0.00")
            Call CalcMaturityAmt(Val(Txt_InitialDeposit.Text), Val(Txt_IntPercent.Text), Val(Txt_Month.Text) + Val(Txt_Days.Text), IIf(Val(Txt_Month.Text) > 0, "M", "D"))
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Set_TransactionGrid()
        Try
            Dim M_CommonBL As New CommonBL
            Dim Ds As DataSet

            Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Ce_OpeningDate.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "D", "M", Val(Txt_InitialDeposit.Text))
            Dim Dt As DataTable = Ds.Tables(0)
            DgvTransaction.DataSource = Dt
            DgvTransaction.DataBind()
            Session("TransTbl") = Dt
            Dim objSum As Object
            If Dt.Rows.Count > 0 Then
                objSum = Dt.Compute("Sum(RctCash)", "")
                LblRctCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
                objSum = Dt.Compute("Sum(RctTransfer)", "")
                LblRctTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
                objSum = Dt.Compute("Sum(PayCash)", "")
                LblPayCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
                objSum = Dt.Compute("Sum(PayTransfer)", "")
                LblPayTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            End If

            If Ds.Tables(1).Rows.Count > 0 Then
                If Ds.Tables(1).Rows(0)("AlertMsg").ToString.Length > 0 Then
                    Dim AlertStr As String
                    AlertStr = Ds.Tables(1).Rows(0)("AlertMsg").ToString
                    ShowAlertMessage(AlertStr)
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbCashTransfer_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.TextChanged
        Set_TransactionGrid()
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Public Sub Calc_InterestRate(Optional ByVal ForSplitAc As Boolean = False)
        Try
            Dim M_CommonBL As New CommonBL

            Txt_IntPercent.Text = Format(M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", Val(Txt_Days.Text) + Val(Txt_Month.Text), _
                            IIf(Val(Txt_Month.Text) > 0, "M", "D"), Val(Txt_InitialDeposit.Text), CommonBL.Intr_RateTypes.INTR_RATE_NORMAL), "#0.00")

            If Session("AcOpenPrivelage") <> CommonBL.Intr_RateTypes.INTR_RATE_NORMAL Then
                Txt_IntPercent.Text = Format(Val(Txt_IntPercent.Text) + M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", Val(Txt_Days.Text) + Val(Txt_Month.Text), _
                        IIf(Val(Txt_Month.Text) > 0, "M", "D"), Val(Txt_InitialDeposit.Text), Session("AcOpenPrivelage")), "#0.00")
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Function Validate_DayMonth(ByVal IsSpit As Boolean) As Boolean
        If Val(Txt_Days.Text) > 0 And Val(Txt_Month.Text) > 0 Then
            ShowAlertMessage("Enter Either Months/Days")
            Return False
        End If

        If HdnDoublingPeriod.Value > 0 Then
            If Val(HdnDoublingPeriod.Value) > Val(Txt_Month.Text) Then
                ShowAlertMessage("Deposit Months must be Multiple of " & HdnDoublingPeriod.Value)
                Return False
            ElseIf (Val(Txt_Month.Text) Mod Val(HdnDoublingPeriod.Value)) > 0 Then
                ShowAlertMessage("Deposit Months must be Multiple of " & HdnDoublingPeriod.Value)
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub Txt_Month_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Month.TextChanged
        Call Validate_DayMonth(False)
        Calc_InterestRate()
        Call Calc_DueDate()

        If Val(Txt_InitialDeposit.Text) > 0 Then
            Call CalcMaturityAmt(Val(Txt_InitialDeposit.Text), Val(Txt_IntPercent.Text), Val(Txt_Month.Text) + Val(Txt_Days.Text), IIf(Val(Txt_Month.Text) > 0, "M", "D"))
        End If
    End Sub

    Public Sub Calc_DueDate(Optional ByVal ForSplitAc As Boolean = False)
        Dim DueDt As Date

        If Val(Txt_Month.Text) > 0 Then
            DueDt = DateAdd(DateInterval.Month, Val(Txt_Month.Text), CDate(Ce_EffectiveDt.Text))
        Else
            DueDt = DateAdd(DateInterval.Day, Val(Txt_Days.Text), CDate(Ce_EffectiveDt.Text))
        End If
        Ce_DueDate.Text = DueDt

    End Sub

    Protected Sub Txt_Days_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Days.TextChanged
        Call Validate_DayMonth(False)
        Calc_InterestRate()
        Call Calc_DueDate()

        If Val(Txt_InitialDeposit.Text) > 0 Then
            Call CalcMaturityAmt(Val(Txt_InitialDeposit.Text), Val(Txt_IntPercent.Text), Val(Txt_Month.Text) + Val(Txt_Days.Text), IIf(Val(Txt_Month.Text) > 0, "M", "D"))
        End If
    End Sub

    Protected Sub Btn_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Set_SchemeCtrl()
    End Sub

    Private Sub Set_SchemeCtrl()
        Try
            Dim M_CommonBL As New CommonBL
            Dim TempDt As DataTable
            TempDt = M_CommonBL.Get_SchemeDtls(CmbScheme.SelectedValue)
            SchModuleCatg.Value = TempDt.Rows(0).Item("Sch_Module_Catg")
            SchSection.Value = TempDt.Rows(0).Item("Sch_Section_Name")
            If TempDt.Rows(0).Item("Sch_Module_Catg") = "SB" Or TempDt.Rows(0).Item("Sch_Module_Catg") = "CA" Then
                Ce_EffectiveDt.Enabled = False
                Ce_OpeningDate.Enabled = False
                Ce_DueDate.Enabled = False
                Txt_Month.Enabled = False
                Txt_Days.Enabled = False
                CmbInterestPayMode.Enabled = False
                Txt_IntPercent.Enabled = False
                'CmbAgent.Enabled = False
                Txt_MatAcNo.Enabled = False
                Txt_MaturityAmt.Enabled = False
                
            Else
                Ce_EffectiveDt.Enabled = True
                Ce_OpeningDate.Enabled = True
                Ce_DueDate.Enabled = True
                Txt_Month.Enabled = True
                Txt_Days.Enabled = True
                CmbInterestPayMode.Enabled = True
                Txt_IntPercent.Enabled = True
                'CmbAgent.Enabled = True
                Txt_MatAcNo.Enabled = True
                Txt_MaturityAmt.Enabled = True
             
            End If

            If SchModuleCatg.Value = "RD" Then
                CmbInterestPayMode.SelectedValue = "623"
                CmbInterestPayMode.Enabled = False
                Txt_Days.Enabled = False
                Ce_EffectiveDt.Enabled = False
            End If
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

    End Sub


   
    Private Function CalcMaturityAmt(ByVal DepAmt As Double, ByVal IntRate As Double, ByVal Period As Integer, ByVal PrdType As String, Optional ByVal ForSpliAc As Boolean = False) As Double
        Dim MatAmt As Double
        Dim DoublingPeriod As Integer
        Dim I As Integer

        MatAmt = 0
        If SchModuleCatg.Value = "FD" Then
            MatAmt = (DepAmt * IntRate * Period) / IIf(PrdType = "M", 1200, 36500)
            MatAmt = CInt(MatAmt) + DepAmt
        ElseIf SchModuleCatg.Value = "RIS" Then
            DoublingPeriod = Val(HdnDoublingPeriod.Value.ToString())
            I = DoublingPeriod
            MatAmt = DepAmt
            For I = DoublingPeriod To Period Step DoublingPeriod
                MatAmt = (MatAmt * 2)
            Next
        ElseIf SchModuleCatg.Value = "RD" Then
            Try
                Dim M_DepositMasterBL As New DepositMasterBL
                Dim M_Date As Date
                M_Date = Session("TransDate")
                MatAmt = M_DepositMasterBL.Get_RDMatAmt(CmbScheme.SelectedValue, M_Date, DepAmt, Val(Txt_Month.Text))
                Txt_MaturityAmt.Text = Format(MatAmt, "#0.00")
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        End If
        MatAmt = CInt(MatAmt)
        If ForSpliAc = False Then Txt_MaturityAmt.Text = Format(MatAmt, "#0.00")

        Return MatAmt
    End Function

    Private Sub DgvDepDtls_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvDepDtls.RowCommand

    End Sub

    Protected Sub DgvDepDtls_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvDepDtls.SelectedIndexChanged

    End Sub

    Protected Sub DgvDepDtls_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvDepDtls.RowDeleting
        Dim dt As DataTable = DirectCast(Session("DepTbl"), DataTable)
        dt.Rows(e.RowIndex).Delete()

        DgvDepDtls.DataSource = dt
        DgvDepDtls.DataBind()
        Session("DepTbl") = dt

        If dt.Rows.Count <= 0 Then Call Set_Grid()
    End Sub

End Class