Imports SMARTCORE_BL
Imports SMARTCORE_Common

Partial Public Class DepositMaster
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private EntryMode As String
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Private M_AccHead As String
    Shared ParameterDt As DataTable
    Shared NomineeTbl As New DataTable("Table")
    Public MultipleDepTbl As New DataTable("Table")
    Dim FormID As Integer = 1050
    Shared Obj_UserRights As Object
    Public Sub Load_DefaultValues()
        Ce_EffectiveDt.Text = Session("TransDate")
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Branch(DrpCanvassedBranch)
        M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
        M_CommonBL.Fill_Pickup(CmbInterestPayMode, 44, 0)
        M_CommonBL.Fill_Pickup(CmbIntPayMode, 44, 0)
        M_CommonBL.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, Session("AcOpenBranch"))
        M_CommonBL.Fill_CanvassedBy(CmbCanvassedBy)
        M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "DEPOSIT")
        M_CommonBL.Fill_Branch(CmbStandBr)
        M_CommonBL.GetSchemes(CmbStandSch, Session("TransDate"), "GROUP DEPOSIT','LOAN','DEPOSIT", "RD','SB','OD','GOLDLOAN','GEN.LOAN','DEP.LOAN','LF','LS','CA','GROUP DEPOSIT")
        M_CommonBL.Fill_BatchNo(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo)
        M_CommonBL.Fill_DebSeries(DrpDebSeries)
        DrpDebSeries.Items.Insert(0, "<--Select-->")
        CmbBatchNo.SelectedIndex = 0

        DgvTransaction.DataSource = Nothing
        Dim Dt As DataTable
        Dt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
        DgvTransaction.DataSource = Dt
        DgvTransaction.DataBind()
        PickupDt = DirectCast(Cache("PickupTbl"), DataTable)

        CmbCashTransfer.Items.Add("Cash")
        CmbCashTransfer.Items.Add("Transfer")
        CmbCashTransfer.SelectedIndex = 1
        Call CmbCashTransfer_SelectedIndexChanged(Me.CmbCashTransfer, System.EventArgs.Empty)

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

        CmbNomReq.Items.Add("No")
        CmbNomReq.Items.Add("Yes")
        CmbNomReq.SelectedIndex = 0
        Lbl_NoofNominees.Text = "Nominee"
        Txt_NoOfNomine.Visible = False

        Ddl_Split.SelectedIndex = 0

        M_CommonBL = Nothing
        cmdDelete.Visible = False
        Set_Grid()
    End Sub
    Private Sub Set_Grid()
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
        Session("DepTblsplit") = MultipleDepTbl
        MultipleDepTbl.Rows.Add(0, 0, "01/01/1900", 0, 0, "", "", "", "", "", "", "", "", "", 0, "", 0)
        Session("DepTbl") = MultipleDepTbl
        DgvDepDtls.DataSource = MultipleDepTbl
        DgvDepDtls.DataBind()

    End Sub


    Private Sub Assign_Values()
        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
        Dim Scheme As String = Session("AcOpenScheme")
        
        CmbOpMode.SelectedValue = Session("AcOpenOpMode")
        TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
        Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
        DgvCustomer.DataSource = dt

        DgvCustomer.DataBind()

        Dim M_CommonBL As New CommonBL
        M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)
        ParameterDt = M_CommonBL.GetDepositParameter(CmbScheme.SelectedValue)

        AgentCollection.Value = "N"
        If ParameterDt.Rows.Count > 0 Then
            AgentCollection.Value = ParameterDt.Rows(0)("Dep_AgentCollection").ToString
            Txt_Month.Text = ParameterDt.Rows(0)("Dep_MinPeriod").ToString
            HdnDoublingPeriod.Value = ParameterDt.Rows(0)("Dep_DoublingPeriod").ToString

            If Val(HdnDoublingPeriod.Value) > 0 Then ' Doubling Scheme
                CmbIntPayMode.SelectedValue = "623"
            End If

            Txt_InitialDeposit.Text = "0"
            Txt_Days.Text = "0"
            Calc_InterestRate()
        End If

        Ce_OpeningDate.Text = Session("TransDate")
        Ce_EffectiveDt.Text = Session("TransDate")
        Ce_DueDate.Text = Session("TransDate")
        Ce_IntUpto.Text = Session("TransDate")
        M_CommonBL = Nothing
        Set_SchemeCtrl()
        Set_TransactionGrid()
        Call Calc_DueDate()
        SchType6.Value = ""
    End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If Session("UserID") = "" Then
    '        Response.Redirect("~/Smartlogin.aspx")
    '        Exit Sub
    '    End If
    '    If Not IsPostBack Then

    '        'USer Rights
    '        Dim M_Comm As New CommonBL
    '        Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
    '        M_Comm = Nothing
    '        If Obj_UserRights.View_Right = False Then
    '            Response.Redirect("~/UnauthorisedAcces.ASPX")
    '        End If

    '        Load_DefaultValues()

    '        ' CmdSave.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
    '        ImgAdd.Attributes.Add("OnClick", "window.open('NomineeMaster.aspx','MyWindow','resizable=no,directories=no,height=350,width=750,toolbar=no,status=no,menubar=no,scrollbars=yes,model=yes');return true;")
    '        Session("M_EntryMode") = "NEW"

    '        Ce_DueDate.Attributes.Add("readonly", "readonly")
    '        Ce_OpeningDate.Attributes.Add("readonly", "readonly")
    '        Ce_IntUpto.Attributes.Add("readonly", "readonly")
    '        Txt_MaturityAmt.Attributes.Add("readonly", "readonly")
    '        Call Calc_DueDate()

    '        If Len(Request.QueryString("TRANID")) > 0 Then
    '            GetAuthoriseDtls("DEPMAS", Request.QueryString("TRANID"))
    '            If Len(Request.QueryString("DEL")) > 0 Then

    '                CmdSave.Visible = False
    '                CmdClear.Visible = False
    '                cmdDelete.Visible = True


    '            End If
    '        Else
    '            Assign_Values()
    '        End If


    '        'ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "enableDisableControls();", True)
    '    End If
    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '' Anil for debanture application
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

            ' CmdSave.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            ImgAdd.Attributes.Add("OnClick", "window.open('NomineeMaster.aspx','MyWindow','resizable=no,directories=no,height=350,width=750,toolbar=no,status=no,menubar=no,scrollbars=yes,model=yes');return true;")
            Session("M_EntryMode") = "NEW"

            Ce_DueDate.Attributes.Add("readonly", "readonly")
            Ce_OpeningDate.Attributes.Add("readonly", "readonly")
            Ce_IntUpto.Attributes.Add("readonly", "readonly")
            Txt_MaturityAmt.Attributes.Add("readonly", "readonly")
            Call Calc_DueDate()


            Dim M_Dt As DataTable
            Dim SuspSchm As String
            If Len(Request.QueryString("TRANID")) > 0 And Len(Request.QueryString("SCHEME")) > 0 Then

                'M_Dt = M_Comm.Check_SuspShareScheme(Request.QueryString("TRANID"))
                'SuspSchm = M_Dt.Rows(0).Item("SuspSchCode").ToString()
                SchType6.Value = "DEB"
                GetDebentureApplicationDetails(Request.QueryString("TRANID"))

                CmbNomReq.Enabled = True
                Ce_EffectiveDt.Enabled = True
                PnlAuthorise2.Visible = True
                HdnDoublingPeriod.Value = 0  ''gowtham

            ElseIf Len(Request.QueryString("TRANID")) > 0 Then

                GetAuthoriseDtls("DEPMAS", Request.QueryString("TRANID"))
                If Len(Request.QueryString("DEL")) > 0 Then

                    CmdSave.Visible = False
                    CmdClear.Visible = False
                    cmdDelete.Visible = True

                End If
            Else
                Assign_Values()
            End If



            'ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "enableDisableControls();", True)
            '' Anil for debanture application
        End If
     
    End Sub
    Private Sub GetDebentureApplicationDetails(ByVal TransID As String)
        '' Anil for debanture application
        Try

            '  Assign_Values()

            Dim M_CommonBL As New CommonBL
            Dim M_Dataset As New DataSet
            Dim TempDt As New DataTable
            M_Dataset = M_CommonBL.GetDebentureApplicationDetails(TransID)
            TempDt = M_CommonBL.Get_SchemeDtls(CmbScheme.SelectedValue)
            SchModuleCatg.Value = TempDt.Rows(0).Item("Sch_Module_Catg")
            SchSection.Value = TempDt.Rows(0).Item("Sch_Section_Name")
            Txt_IntRate.Attributes.Add("readonly", "readonly")

            Dim M_TranTbl As DataTable
            M_TranTbl = M_Dataset.Tables(0)


            'DgvTransaction.DataSource = M_Dataset.Tables(1)
            'Session("TransTbl") = M_Dataset.Tables(1)
            'DgvTransaction.DataBind()



            GetAccountDtlsFORDEBENTURE(M_TranTbl)
            PnlAuthorise.Enabled = False
            PnlAuthorise1.Enabled = True
            ' PnlAuthorise2.Enabled = False
            txtNoDebenture.Enabled = False
            txtDistictiveNo.Enabled = False
            'PnlAuthorise4.Enabled = False
            Ddl_Split.Enabled = False
            Ce_OpeningDate.Enabled = False

            Ddl_SI.Enabled = False
            Txt_MaturityAmt.Enabled = False
            Txt_InitialDeposit.Enabled = False
            'Calc_TranTotal()

            Session("AccHolderTbl") = M_Dataset.Tables(2)
            DgvCustomer.DataSource = M_Dataset.Tables(2)
            DgvCustomer.DataBind()
            ''TxtNoOfOperator.Text = M_Dataset.Tables(2).Rows.Count

           
            'Ce_OpeningDate.Text = Session("TransDate")
            'Ce_EffectiveDt.Text = Session("TransDate")
            'Ce_DueDate.Text = Session("TransDate")
            'Ce_IntUpto.Text = Session("TransDate")


            'Call Txt_InitialDeposit_TextChanged(Me.Txt_InitialDeposit, System.EventArgs.Empty)  '' gowtham
            Try
                Set_TransactionGrid()
                'Calc_InterestRate()
                Dim M_CommonCls As New CommonClass
                If Val(Txt_InitialDeposit.Text) > 0 Then
                    LblWords.Text = M_CommonCls.Towords(Val(Txt_InitialDeposit.Text)).ToUpper
                    M_CommonCls = Nothing
                End If

                Txt_InitialDeposit.Text = Format(Val(Txt_InitialDeposit.Text), "#0.00")
                Call CalcMaturityAmt(Val(Txt_InitialDeposit.Text), Val(Txt_IntPercent.Text), Val(Txt_Month.Text) + Val(Txt_Days.Text), IIf(Val(Txt_Month.Text) > 0, "M", "D"))
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
            M_CommonBL = Nothing
        Catch ex As Exception

        End Try
        '' Anil for debanture application
    End Sub
    Private Sub GetAccountDtlsFORDEBENTURE(ByVal M_AccTbl As DataTable)

        Dim M_ComBl As New CommonBL
        Dim M_Dt As New DataTable

        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        CmbOpMode.SelectedValue = M_AccTbl.Rows(0).Item("Dm_OperationMode")
        CmbScheme.SelectedValue = M_AccTbl.Rows(0).Item("Sch_Code")
        TxtNoOfOperator.Text = M_AccTbl.Rows(0).Item("NoOfOp")
        Ce_OpeningDate.Text = Session("TransDate")
        Ce_EffectiveDt.Text = Session("TransDate")
        Ce_DueDate.Text = Session("TransDate")
        'Txt_Month.Text = M_AccTbl.Rows(0).Item("Dm_Months")
        Txt_Days.Text = M_AccTbl.Rows(0).Item("Dm_Days")

        'CmbInterestPayMode.SelectedValue = M_AccTbl.Rows(0).Item("Dm_IntPayMode")
        Txt_InitialDeposit.Text = M_AccTbl.Rows(0).Item("Dm_DepAmount")
        Txt_IntPercent.Text = M_AccTbl.Rows(0).Item("Dm_IntRate")
        CmbAgent.SelectedValue = M_AccTbl.Rows(0).Item("Dm_Agent_Code")
        Txt_NoOfNomine.Text = M_AccTbl.Rows(0).Item("Dm_NomNos")
        Ddl_SI.SelectedValue = M_AccTbl.Rows(0).Item("Dep_Standing")
        Txt_MaturityAmt.Text = M_AccTbl.Rows(0).Item("Dm_MaturityAmt")
        'CmbCanvassedBy.SelectedValue = M_AccTbl.Rows(0).Item("Dm_CanvassedBy")
        Ddl_Split.SelectedValue = M_AccTbl.Rows(0).Item("Dm_Split")
        CmbCashTransfer.SelectedIndex = M_AccTbl.Rows(0).Item("Cash")

        txtNoDebenture.Text = M_AccTbl.Rows(0).Item("NOOFDEB")
        txtDistictiveNo.Text = M_AccTbl.Rows(0).Item("DISTINCTIVENO")
        Txt_Month.Text = M_AccTbl.Rows(0).Item("PERIODUPTO")
        Ce_IntUpto.Text = Session("TransDate")
        CmbInterestPayMode.SelectedValue = M_AccTbl.Rows(0).Item("PayMode")
        CmbCanvassedBy.SelectedValue = M_AccTbl.Rows(0).Item("CanvasedBy")
        DrpCanvassedBranch.SelectedValue = M_AccTbl.Rows(0).Item("CanvasedBrcode")
        Txt_Days.Text = M_AccTbl.Rows(0).Item("Days")
        CmbCashTransfer.SelectedIndex = 1
        Session("AcOpenPrivelage") = "417"

        M_Dt = M_ComBl.Get_DebSeries(M_AccTbl.Rows(0).Item("Sch_Code"), Session("TransDate"))
        If M_Dt.Rows.Count > 0 Then
            DrpDebSeries.SelectedValue = M_Dt.Rows(0).Item("seriesid")
            DrpDebSeries.Enabled = False
        End If
        Call Calc_DueDate()
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


    'Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

    '    Dim M_CommonBL As New CommonBL
    '    Dim M_Dataset As New DataSet
    '    M_Dataset = M_CommonBL.GetAuthoriseDtls("DEPMAS", TransID)
    '    M_CommonBL = Nothing
    '    Dim M_TranTbl As DataTable
    '    M_TranTbl = M_Dataset.Tables(0)
    '    DgvTransaction.DataSource = M_Dataset.Tables(1)
    '    Session("TransTbl") = M_Dataset.Tables(1)
    '    DgvTransaction.DataBind()
    '    M_CommonBL = Nothing

    '    GetAccountDtls(M_TranTbl)
    '    PnlAuthorise.Enabled = False
    '    'Calc_TranTotal()
    '    DgvCustomer.DataSource = M_Dataset.Tables(2)
    '    DgvCustomer.DataBind()
    '    TxtNoOfOperator.Text = M_Dataset.Tables(2).Rows.Count
    'End Sub
    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls(FromNm, TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        M_TranTbl = M_Dataset.Tables(0)
        DgvTransaction.DataSource = M_Dataset.Tables(1)
        Session("TransTbl") = M_Dataset.Tables(1)
        DgvTransaction.DataBind()
        M_CommonBL = Nothing

        GetAccountDtls(M_TranTbl)
        PnlAuthorise.Enabled = False
        PnlAuthorise1.Enabled = False
        PnlAuthorise3.Enabled = False
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
        CmbAgent.SelectedValue = M_AccTbl.Rows(0).Item("Dm_Agent_Code")
        Txt_NoOfNomine.Text = M_AccTbl.Rows(0).Item("Dm_NomNos")
        Ddl_SI.SelectedValue = M_AccTbl.Rows(0).Item("Dep_Standing")
        Txt_MaturityAmt.Text = M_AccTbl.Rows(0).Item("Dm_MaturityAmt")
        CmbCanvassedBy.SelectedValue = M_AccTbl.Rows(0).Item("Dm_CanvassedBy")
        DrpCanvassedBranch.SelectedValue = M_AccTbl.Rows(0).Item("CanvasedBrcode")
        Ddl_Split.SelectedValue = M_AccTbl.Rows(0).Item("Dm_Split")
        CmbCashTransfer.SelectedIndex = M_AccTbl.Rows(0).Item("Cash")

    End Sub




    Private Sub Locate_Data(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        On Error Resume Next

        '        If M_CustMaster.CustId = 0 Then
        '            Session("M_EntryMode") = "NEW"
        '            TxtCustID.Text = "NEW"
        '            Exit Sub
        '        End If
        '        With M_CustMaster
        '            TxtAge.TabIndex = .CustAge
        '            cmbCustomerType.SelectedValue = .CustTypeCode
        '            Fill_Pickup(CmbCustomerCatg, 7, cmbCustomerType.SelectedValue)
        '            CmbTitle.SelectedValue = .CustTitleCode
        '            CmbGender.SelectedValue = .CustGender
        '            CmbRelType.SelectedValue = .CustRelTypCode
        '            CmbGrading.SelectedValue = .CustGradeCode
        '            CmbDisability.SelectedValue = .CustDisabilityCode
        '            CmbCurrency.SelectedValue = .CustCurrencyCode
        '            '=.InstitutionCode
        '            CmbNationality.SelectedValue = .CustNationalityCode
        '            CmbMaritialStatus.SelectedValue = .CustMaritalStatusCode
        '            CmbIncomeRange.SelectedValue = .CustIncomeRangeCode
        '            CmbITDoc.SelectedValue = .CustItDocumentCode
        '            CmbLocation.SelectedValue = .CustLocationCode
        '            '=.CustGuardianRelationCode
        '            CmbReligion.SelectedValue = .CustReligionCode
        '            CmbMainCaste.SelectedValue = .CustCasteCode
        '            CmbOccupation.SelectedValue = .CustOccupationCode
        '            CmbEducation.SelectedValue = .CustEducationCode
        '            TxtDob.Text = .CustDobInc
        '            TxtDoa.Text = .CustRegDate
        '            DtpGrading.Text = .CustRatingDate
        '            '=.CustMinor
        '            '=.CustAuthorised
        '            CmbAdmBranch.SelectedValue = .BrCode
        '            TxtName.Text = .CustName
        '            '=.CustFirstName
        '            '=.CustMiddleName
        '            '=.CustLastName
        '            '=.CustShortName
        '            '=.CustPhone2
        '            TxtMobile.Text = .CustMobile
        '            TxtITDocNo.Text = .CustITDocRemarks
        '            '=.MakerID
        '            '=.CheckerID
        '            TxtAdhaarNo.Text = .CustAdhaarNo
        '            TxtIdentityMark.Text = .CustIdentificationMark
        '            CmbBloodGrp.SelectedValue = .CustBloodGroup
        '            TxtWebAdd.Text = .CustWebsite
        '            TxtFax.Text = .CustFax
        '            TxtEmail.Text = .CustEmail
        '            TxtLandPhone.Text = .CustPhone1
        '            TxtRelName.Text = .CustRelNm
        '            '=.CustGuardianId
        '            TxtIntroID.Text = .CustIntroCustId
        '            TxtRemarks.Text = .CustRemarks
        '            CmbCustomerCatg.SelectedValue = .CustCatgCode
        '            '=.CustIslanderCardNo
        '            '=.CustLocalCertNo
        '            '=.CustStaff
        '            '=.CustStatus
        '            '=.IsCompleted

        '        End With

        '        TxtPresHouseName.Text = M_PresAddress.AddAddress1
        '        TxtPresPlace.Text = M_PresAddress.AddAddress2
        '        TxtPresState.Text = M_PresAddress.AddAddress3
        '        TxtPresLand.Text = M_PresAddress.AddPostoffice
        '        TxtPresPin.Text = M_PresAddress.AddPinCode
        '        TxtPresHouseNo.Text = M_PresAddress.AddHouseNo
        '        TxtPresDist.Text = M_PresAddress.AddDist
        '        TxtPresCountry.Text = M_PresAddress.AddCountry
        '        TxtPermHouseName.Text = M_PermAddress.AddAddress1
        '        TxtPermPlace.Text = M_PermAddress.AddAddress2
        '        TxtPermState.Text = M_PermAddress.AddAddress3
        '        TxtPermLand.Text = M_PermAddress.AddPostoffice
        '        TxtPermPin.Text = M_PermAddress.AddPinCode
        '        TxtPermHouseNo.Text = M_PresAddress.AddHouseNo
        '        TxtPermDist.Text = M_PresAddress.AddDist
        '        TxtPermCountry.Text = M_PresAddress.AddCountry
        '        Session("M_EntryMode") = "VIEW"
        '        Exit Sub
        'ErrHndlr:


    End Sub
    Private Function Validate_Control() As Boolean

        If SchModuleCatg.Value = "FD" Or SchModuleCatg.Value = "RD" Or SchModuleCatg.Value = "DD" Then
            If Val(Txt_Month.Text) + Val(Txt_Days.Text) = 0 And Ddl_Split.SelectedIndex = 0 Then
                Txt_Month.Focus()
                ShowAlertMessage("INVALID DEPOSIT PERIOD ENTERED")
                Return False
            End If
            If Val(Txt_InitialDeposit.Text) <= 0 And SchModuleCatg.Value <> "DD" Then
                Txt_InitialDeposit.Focus()
                ShowAlertMessage("INVALID DEPOSIT AMOUNT ENTERED")
                Return False
            End If
            If Val(Txt_IntPercent.Text) <= 0 And Ddl_Split.SelectedIndex = 0 Then
                Txt_IntPercent.Focus()
                ShowAlertMessage("INVALID DEPOSIT INT RATE ENTERED")
                Return False
            End If

            If Ddl_Split.SelectedIndex = 0 Then
                If Validate_DayMonth(False) = False Then
                    Return False
                End If
            End If

        ElseIf SchModuleCatg.Value = "SB" Then
            'If Val(Txt_InitialDeposit.Text) <= 0 Then
            '    Txt_InitialDeposit.Focus()
            '    ShowAlertMessage("INVALID DEPOSIT AMOUNT ENTERED")
            '    Return False
            'End If
        End If
        If AgentCollection.Value = "Y" And CmbAgent.SelectedIndex = CmbAgent.Items.Count - 1 Then
            ShowAlertMessage("INVALID AGENT SELECTED")
            Return False
        End If

        If Ddl_Split.SelectedIndex = 1 Then
            Dim dt As DataTable = DirectCast(Session("DepTbl"), DataTable)
            Dim objSum As Object
            If dt.Rows.Count > 0 Then
                objSum = dt.Compute("Sum(Dep_Amt)", "")
            End If

            If Val(Txt_InitialDeposit.Text) > Val(objSum.ToString) Then
                Txt_InitialDeposit.Focus()
                ShowAlertMessage("TOTAL AMOUNT MISMATCH TO INITIAL DEPOSIT AMOUNT")
                Return False
            End If
        End If

        Return True
    End Function
    Private Shared CommCls As New CommonClass()
    'Protected Sub Btn_Save_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
    '    Dim SplitAcdt As DataTable
    '    Dim M_CommonBL As New CommonBL
    '    Dim M_BatchNo As String = ""

    '    If CmbCashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text

    '    If Len(Request.QueryString("TRANID")) > 0 Then

    '        If M_CommonBL.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
    '            ShowAlertMessage("Authotisation Failed")
    '            Response.Redirect("~/ManagerPassing.aspx")
    '            Exit Sub
    '        End If
    '        M_CommonBL = Nothing
    '        Response.Redirect("~/ManagerPassing.aspx?Passing=y")
    '        Exit Sub
    '    End If

    '    If Validate_Control = False Then
    '        Exit Sub
    '    End If


    '    M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)
    '    M_CommonBL = Nothing



    '    Dim M_DepositMaster As New SMARTCORE_BO.DepositMasterBO
    '    Try
    '        With M_DepositMaster
    '            .AccID = M_AccID
    '            .CustId = Val(DgvCustomer.Rows(0).Cells(4).Text)
    '            .DmMonths = Val(Txt_Month.Text)
    '            .DmDays = Val(Txt_Days.Text)
    '            .DmNomNos = IIf(NomDiv.Visible = False, 0, 1)
    '            .DmOperationMode = CmbOpMode.SelectedValue
    '            .DmAgentCode = Val(CmbAgent.SelectedValue)
    '            .DmPrevilage = Session("AcOpenPrivelage")
    '            .DmIntPayMode = CmbInterestPayMode.SelectedValue
    '            .LogID = "1"
    '            .DmRenewalDays = "0"
    '            .DmRenewalMonths = "0"

    '            .DmDepDate = Ce_OpeningDate.Text
    '            .DmWithEffDate = Ce_EffectiveDt.Text
    '            .DmDueDate = Ce_DueDate.Text
    '            .DmLastIntDate = Ce_IntUpto.Text
    '            .DmLastUpdate = Ce_IntUpto.Text

    '            .DmOpIntdate = "01/01/1900"
    '            .DmClosedDate = "01/01/1900"
    '            .MakingTime = DateTime.Now
    '            .CheckingTime = "01/01/1900"
    '            .Cashiertime = "01/01/1900"

    '            .DmDepAmount = Val(Txt_InitialDeposit.Text)
    '            .DmBalance = Val(Txt_InitialDeposit.Text)
    '            .DmMaturityAmt = Val(Txt_MaturityAmt.Text)
    '            .DmIntRate = Val(Txt_IntPercent.Text)
    '            .DmOpBal = 0
    '            .DmOpIntrate = Val(Txt_IntPercent.Text)
    '            .AccNo = M_AccNo
    '            .BaseAccNo = M_AccNo
    '            .SchCode = CmbScheme.SelectedValue
    '            .BrCode = CmbBranch.SelectedValue
    '            .AccCode = M_AccHead
    '            .DmBondNo = ""
    '            .CashierID = ""
    '            .BankAccNo = ""
    '            .DmCanvassedBy = CmbCanvassedBy.SelectedValue
    '            .DmAutoRenewal = Ddl_AutoRenew.SelectedValue
    '            .DmRemarks = Txt_Remarks.Text
    '            .DmIntroducedby = Txt_IntroBy.Text
    '            .DmTransferTo = Txt_MatAcNo.Text
    '            .TranNo = ""
    '            .MakerID = Session("UserID")
    '            .CheckerID = ""
    '            .DmBalType = "C"
    '            .DmTODStatus = "Y"
    '            .DmOpBalType = "C"
    '            .DmChqIssueStatus = Ddl_ChqIssue.SelectedValue
    '            .DmLinkedStatus = Ddl_SI.SelectedValue
    '            .DmStatus = "Y"
    '            If SchModuleCatg.Value = "DD" Then
    '                .Authorised = "Y"
    '            Else
    '                .Authorised = "N"
    '            End If

    '        End With

    '        If Ddl_Split.SelectedIndex = 0 Then ' No Split A/c (For Single A/c)
    '            Dim Row As DataRow
    '            SplitAcdt = DirectCast(Session("DepTbl"), DataTable)

    '            Dim Expression As String = "Dep_Amt=0"
    '            If SplitAcdt.[Select](Expression).Count > 0 Then SplitAcdt.Rows.RemoveAt(SplitAcdt.Rows.IndexOf(SplitAcdt.[Select](Expression)(0)))

    '            Row = SplitAcdt.NewRow
    '            Row("Dep_Mth") = Val(Txt_Month.Text)
    '            Row("Dep_Days") = Val(Txt_Days.Text)
    '            Row("Dep_DueDt") = Replace(CommCls.DbDate(Ce_DueDate.Text), "'", "") ' Format(Ce_DueDate.Text, "MM/dd/yyyy")
    '            Row("Dep_Amt") = Val(Txt_InitialDeposit.Text)
    '            Row("Dep_IntRate") = Val(Txt_IntPercent.Text)
    '            Row("Dep_IntPayMode") = CmbInterestPayMode.SelectedItem.Text
    '            If Txt_NomName.Text.Length > 0 Then
    '                Row("Dep_NomNm") = Txt_NomName.Text
    '                Row("Dep_NomRel") = CmbNomRel.SelectedItem.Text
    '                Row("Dep_NomAdd") = Txt_NomAdd.Text
    '            Else
    '                Row("Dep_NomNm") = ""
    '                Row("Dep_NomRel") = ""
    '                Row("Dep_NomAdd") = ""
    '            End If
    '            If TxtStandAcNo.Text.Length > 0 Then
    '                Row("Dep_Standing") = CmbStandBr.SelectedItem.Text + " " + CmbStandSch.SelectedItem.Text + " " + TxtStandAcNo.Text
    '            Else
    '                Row("Dep_Standing") = ""
    '            End If
    '            Row("Dep_IntPayModeCode") = CmbInterestPayMode.SelectedValue
    '            Row("Dep_StndBr") = CmbStandBr.SelectedValue
    '            Row("Dep_StndSch") = CmbStandSch.SelectedValue
    '            Row("Dep_StndAccNo") = TxtStandAcNo.Text
    '            Row("Dep_NomRelCode") = CmbNomRel.SelectedValue
    '            Row("Dep_NomPh") = Txt_NomPh.Text
    '            Row("Dep_MatureAmt") = Val(Txt_MaturityAmt.Text)

    '            Try
    '                SplitAcdt.Rows.Add(Row)
    '            Catch ex As Exception

    '            End Try

    '            Session("DepTbl") = SplitAcdt
    '        End If

    '        Dim AccHolderdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
    '        Dim M_AccHolderSw As New IO.StringWriter
    '        AccHolderdt.WriteXml(M_AccHolderSw)

    '        Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
    '        Dim M_TranSw As New IO.StringWriter
    '        Trandt.WriteXml(M_TranSw)

    '        Dim M_NomineeSw As New IO.StringWriter
    '        If NomDiv.Visible = False Then
    '            If Val(Txt_NoOfNomine.Text) > 0 Then
    '                Dim NomineeDt As DataTable = DirectCast(Session("NomTbl"), DataTable)
    '                NomineeDt.WriteXml(M_NomineeSw)
    '            Else
    '                Session("NomTbl") = ""
    '            End If
    '        Else
    '            Set_Nominee()
    '            Dim NomineeDt As DataTable = DirectCast(Session("NomTbl"), DataTable)
    '            NomineeDt.WriteXml(M_NomineeSw)
    '        End If

    '        Dim M_DepositMasterBL As New DepositMasterBL
    '        Dim M_Dt As New DataTable

    '        SplitAcdt = DirectCast(Session("DepTbl"), DataTable)
    '        Dim M_SplitAcSw As New IO.StringWriter
    '        SplitAcdt.WriteXml(M_SplitAcSw)

    '        Session("M_EntryMode") = "NEW"
    '        M_Dt = M_DepositMasterBL.Save_Data(M_DepositMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), _
    '                    M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), _
    '                    M_SplitAcSw.ToString.Replace("DocumentElement", "NewDataSet"), M_BatchNo)
    '        ShowAlertMessage(" DEPOSIT NO : " & M_Dt.Rows(0)("Acc_No").ToString)
    '        Session("NomTbl") = Nothing
    '        ' Bond Printing TO Do
    '        'If Session("SECTION") = "FD" Then
    '        '    Call PrintBond_FD(M_Dt.Rows(0)("Tran_No").ToString)
    '        'End If

    '        'Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & IIf(SchModuleCatg.Value = "FD", "&PrintBond=Y", "&PrintBond=N") & "&SECTION=" & SchModuleCatg.Value & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & SchSection.Value & "&DRCR=" & "C")
    '        Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & IIf((SchModuleCatg.Value = "FD" Or SchModuleCatg.Value = "RIS"), "&PrintBond=Y", "&PrintBond=N") & "&SECTION=" & SchModuleCatg.Value & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & SchSection.Value & "&DRCR=" & "C")
    '        M_DepositMasterBL = Nothing
    '        M_AccHead = ""
    '        'Response.Redirect("~/AccOpen.aspx")

    '    Catch ex As Exception
    '        ShowAlertMessage(ex.Message.ToString)
    '    End Try
    'End Sub
    Private Function CashControl() As Boolean
        Try
            If CmbCashTransfer.SelectedItem.Text <> "Cash" Then
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
            Dim custId As Integer = Val(DgvCustomer.Rows(0).Cells(4).Text)
            M_Cash = M_con.GetTanLimit(Session("DefaultBrCode").ToString(), CmbScheme.SelectedValue, custId, CashRctTotal + CashPayTotal, CmbCashTransfer.SelectedItem.Text, Session("TransDate"))

            If M_Cash.Rows(0).Item("Status") = "N" Then
                ShowAlertMessage("Customer Cash Limit Exceeded, Change Cash to Transfer")
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function
    Protected Sub Btn_Save_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim SplitAcdt As DataTable
        Dim M_CommonBL As New CommonBL
        Dim M_BatchNo As String = ""
        If Not ((Val(Session("MaxTranLimit").ToString) >= (Val(LblRctCashTotal.Text) + Val(LblRctTransferTotal.Text) + Val(LblPayCashTotal.Text) + Val(LblPayTransferTotal.Text)))) Then
            ShowAlertMessage("Maximum Transaction Limit is :" + Session("MaxTranLimit").ToString())
            Exit Sub
        End If
        If CashControl() = False Then
            Exit Sub
        End If
        If CmbCashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text

        If Len(Request.QueryString("TRANID")) > 0 And Len(Request.QueryString("SCHEME")) = 0 Then

            If M_CommonBL.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                ShowAlertMessage("Authotisation Failed")
                Response.Redirect("~/ManagerPassing.aspx")
                Exit Sub
            End If
            M_CommonBL = Nothing
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
            Exit Sub
        End If

        If Validate_Control = False Then
            Exit Sub
        End If


        M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)


        If Len(Request.QueryString("TRANID")) > 0 And Len(Request.QueryString("SCHEME")) > 0 Then
            Dim M_Dataset As New DataSet
            Dim Dt As New DataTable
            Dim TranNo As String = Request.QueryString("TRANID").ToString()
            M_Dataset = M_CommonBL.GetDebentureApplicationDetails(TranNo)
            Dt = M_Dataset.Tables(0)
            If Val(Dt.Rows(0).Item("Dm_DepAmount")) = 0 Then
                Response.Redirect("~/Main.aspx")
            End If

            If Len(TxtResNo.Text) = 0 Then
                ShowAlertMessage("Please Check Resolution No:")
                TxtResNo.Focus()
                Exit Sub
            End If
            If Len(TxtResDt.Text) = 0 Then
                ShowAlertMessage("Please Check Resolution Date")
                TxtResDt.Focus()
                Exit Sub
            End If
            M_Dataset = Nothing
        End If
        M_CommonBL = Nothing
        If CkBankDetails.Checked = True Then
            If DdlTranType.SelectedIndex = 0 Then
                ShowAlertMessage("Please select Bank Transaction Type....")
                DdlTranType.Focus()
                Exit Sub
            End If
            If Val(IIf(TxtBankAmnt.Text = "", "0", TxtBankAmnt.Text)) <= 0 Then
                ShowAlertMessage("Please Enter Bank Amnount....")
                TxtBankAmnt.Focus()
                Exit Sub
            End If
        End If
      
        Dim M_DepositMaster As New SMARTCORE_BO.DepositMasterBO
        Try
            With M_DepositMaster
                .AccID = M_AccID
                .CustId = Val(DgvCustomer.Rows(0).Cells(4).Text)
                .DmMonths = Val(Txt_Month.Text)
                .DmDays = Val(Txt_Days.Text)
                .DmNomNos = IIf(NomDiv.Visible = False, 0, 1)
                .DmOperationMode = CmbOpMode.SelectedValue
                .DmAgentCode = Val(CmbAgent.SelectedValue)
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
                .BankAccNo = ""
                .DmCanvassedBy = CmbCanvassedBy.SelectedValue
                .DmCanvassedBranch = DrpCanvassedBranch.SelectedValue
                .DmAutoRenewal = Ddl_AutoRenew.SelectedValue
                .DmRemarks = Txt_Remarks.Text
                .DmIntroducedby = Txt_IntroBy.Text
                .DmTransferTo = Txt_MatAcNo.Text
                .TranNo = ""
                .MakerID = Session("UserID")
                .CheckerID = ""
                .DmBalType = "C"
                .DmTODStatus = "Y"
                .DmOpBalType = "C"
                .DmChqIssueStatus = Ddl_ChqIssue.SelectedValue
                .DmLinkedStatus = Ddl_SI.SelectedValue
                .DmStatus = "Y"
                If SchModuleCatg.Value = "DD" Then
                    .Authorised = "Y"
                Else
                    .Authorised = "N"
                End If
                .DebSeries = IIf(DrpDebSeries.SelectedIndex = 0, "0", DrpDebSeries.SelectedValue)
            End With

            If Ddl_Split.SelectedIndex = 0 Then ' No Split A/c (For Single A/c)
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
                If Txt_NomName.Text.Length > 0 Then
                    Row("Dep_NomNm") = Txt_NomName.Text
                    Row("Dep_NomRel") = CmbNomRel.SelectedItem.Text
                    Row("Dep_NomAdd") = Txt_NomAdd.Text
                Else
                    Row("Dep_NomNm") = ""
                    Row("Dep_NomRel") = ""
                    Row("Dep_NomAdd") = ""
                End If
                If TxtStandAcNo.Text.Length > 0 Then
                    Row("Dep_Standing") = CmbStandBr.SelectedItem.Text + " " + CmbStandSch.SelectedItem.Text + " " + TxtStandAcNo.Text
                Else
                    Row("Dep_Standing") = ""
                End If
                Row("Dep_IntPayModeCode") = CmbInterestPayMode.SelectedValue
                Row("Dep_StndBr") = CmbStandBr.SelectedValue
                Row("Dep_StndSch") = CmbStandSch.SelectedValue
                Row("Dep_StndAccNo") = TxtStandAcNo.Text
                Row("Dep_NomRelCode") = CmbNomRel.SelectedValue
                Row("Dep_NomPh") = Txt_NomPh.Text
                Row("Dep_MatureAmt") = Val(Txt_MaturityAmt.Text)

                Try
                    SplitAcdt.Rows.Add(Row)
                Catch ex As Exception

                End Try

                Session("DepTbl") = SplitAcdt
            End If

            Dim AccHolderdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_AccHolderSw As New IO.StringWriter
            AccHolderdt.WriteXml(M_AccHolderSw)

            Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Trandt.WriteXml(M_TranSw)

            Dim M_NomineeSw As New IO.StringWriter
            If NomDiv.Visible = False Then
                If Val(Txt_NoOfNomine.Text) > 0 Then
                    Dim NomineeDt As DataTable = DirectCast(Session("NomTbl"), DataTable)
                    NomineeDt.WriteXml(M_NomineeSw)
                Else
                    Session("NomTbl") = ""
                End If
            Else
                Set_Nominee()
                Dim NomineeDt As DataTable = DirectCast(Session("NomTbl"), DataTable)
                NomineeDt.WriteXml(M_NomineeSw)
            End If

            Dim M_DepositMasterBL As New DepositMasterBL
            Dim M_Dt As New DataTable

            SplitAcdt = DirectCast(Session("DepTbl"), DataTable)
            Dim M_SplitAcSw As New IO.StringWriter
            SplitAcdt.WriteXml(M_SplitAcSw)

    
            Session("M_EntryMode") = "NEW"
            M_Dt = M_DepositMasterBL.Save_Data(M_DepositMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), _
                        M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), _
                        M_SplitAcSw.ToString.Replace("DocumentElement", "NewDataSet"), M_BatchNo)
            ShowAlertMessage(" DEPOSIT NO : " & M_Dt.Rows(0)("Acc_No").ToString)
            Session("NomTbl") = Nothing
            ''*********Bank Details**********
            If CmbCashTransfer.SelectedIndex = 1 And CkBankDetails.Checked = True Then
                Dim M_LoanMasterBL1 As New LoanMasterBL
                Dim dtTransferDetails As New DataTable
                dtTransferDetails = M_LoanMasterBL1.SaveTransferDetails_Data(M_Dt.Rows(0)("Tran_No").ToString, drpBank.SelectedItem.ToString(), drpbankAccno.SelectedItem.Text.ToString(), TxtCheque.Text.ToString(), txtChequeDate.Text.ToString(), drpTranferType.SelectedValue.ToString(), txtIFSC.Text.ToString(), txtaccno.Text.ToString(), txtCommission.Text.ToString(), Session("TransDate"), Val(TxtBankAmnt.Text), IIf(DdlTranType.SelectedIndex = 1, "C", "D"))
            End If
            ''*********Bank Details**********
            If Len(Request.QueryString("TRANID")) > 0 And Len(Request.QueryString("SCHEME")) > 0 Then

                Dim M_Dt1 As New DataTable
                M_Dt1 = M_DepositMasterBL.Save_Dep_application_folio(Val(txtNoDebenture.Text), CmbBranch.SelectedValue.ToString(), M_Dt.Rows(0)("Acc_No").ToString, Request.QueryString("TRANID").ToString(), txtDistictiveNo.Text.ToString(), Ce_OpeningDate.Text, Session("UserID").ToString(), TxtResNo.Text, TxtResDt.Text)


            End If

            ' Bond Printing TO Do
            'If Session("SECTION") = "FD" Then
            '    Call PrintBond_FD(M_Dt.Rows(0)("Tran_No").ToString)
            'End If

            'Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & IIf(SchModuleCatg.Value = "FD", "&PrintBond=Y", "&PrintBond=N") & "&SECTION=" & SchModuleCatg.Value & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & SchSection.Value & "&DRCR=" & "C")
            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & IIf((SchModuleCatg.Value = "FD" Or SchModuleCatg.Value = "RIS"), "&PrintBond=Y", "&PrintBond=N") & "&SECTION=" & SchModuleCatg.Value & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & SchSection.Value & "&DRCR=" & "C")
            M_DepositMasterBL = Nothing
            M_AccHead = ""
            'Response.Redirect("~/AccOpen.aspx")

        Catch ex As Exception
            ShowAlertMessage(ex.Message.ToString)
        End Try

    End Sub



    Private Sub Ce_EffectiveDt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ce_EffectiveDt.TextChanged
        Call Calc_DueDate(False)
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

    Protected Sub Txt_InitialDeposit_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_InitialDeposit.TextChanged
        Try
            Set_TransactionGrid()
            Calc_InterestRate()
            Dim M_CommonCls As New CommonClass
            If Val(Txt_InitialDeposit.Text) > 0 Then
                LblWords.Text = M_CommonCls.Towords(Val(Txt_InitialDeposit.Text)).ToUpper
                M_CommonCls = Nothing
            End If
      
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

            Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Ce_OpeningDate.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "C", "M", Val(Txt_InitialDeposit.Text))
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

            If ForSplitAc = False Then
                Txt_IntPercent.Text = Format(M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", Val(Txt_Days.Text) + Val(Txt_Month.Text), _
                            IIf(Val(Txt_Month.Text) > 0, "M", "D"), Val(Txt_InitialDeposit.Text), CommonBL.Intr_RateTypes.INTR_RATE_NORMAL, "", CmbInterestPayMode.SelectedValue.ToString()), "#0.00")

                If Session("AcOpenPrivelage") <> CommonBL.Intr_RateTypes.INTR_RATE_NORMAL Then
                    Txt_IntPercent.Text = Format(Val(Txt_IntPercent.Text) + M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", Val(Txt_Days.Text) + Val(Txt_Month.Text), _
                            IIf(Val(Txt_Month.Text) > 0, "M", "D"), Val(Txt_InitialDeposit.Text), Session("AcOpenPrivelage"), "", CmbInterestPayMode.SelectedValue.ToString()), "#0.00")
                End If
            Else
                Txt_IntRate.Text = Format(M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", Val(Txt_Dys.Text) + Val(Txt_Mth.Text), _
                IIf(Val(Txt_Mth.Text) > 0, "M", "D"), Val(Txt_DepAmt.Text), CommonBL.Intr_RateTypes.INTR_RATE_NORMAL, "", CmbInterestPayMode.SelectedValue.ToString()), "#0.00")

                If Session("AcOpenPrivelage") <> CommonBL.Intr_RateTypes.INTR_RATE_NORMAL Then
                    Txt_IntRate.Text = Format(Val(Txt_IntRate.Text) + M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", Val(Txt_Dys.Text) + Val(Txt_Mth.Text), _
                            IIf(Val(Txt_Mth.Text) > 0, "M", "D"), Val(Txt_DepAmt.Text), Session("AcOpenPrivelage"), "", CmbInterestPayMode.SelectedValue.ToString()), "#0.00")
                End If
            End If
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Function Validate_DayMonth(ByVal IsSpit As Boolean) As Boolean
        If IsSpit = False Then
            If Val(Txt_Days.Text) > 0 And Val(Txt_Month.Text) > 0 And Len(Request.QueryString("SCHEME")) = 0 Then
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
        Else
            If Val(Txt_Dys.Text) > 0 And Val(Txt_Mth.Text) > 0 And Len(Request.QueryString("SCHEME")) = 0 Then
                ShowAlertMessage("Enter Either Months/Days")
                Return False
            End If

            If HdnDoublingPeriod.Value > 0 Then
                If Val(HdnDoublingPeriod.Value) > Val(Txt_Mth.Text) Then
                    ShowAlertMessage("Deposit Months must be Multiple of " & HdnDoublingPeriod.Value)
                    Return False
                ElseIf (Val(Txt_Mth.Text) Mod Val(HdnDoublingPeriod.Value)) > 0 Then
                    ShowAlertMessage("Deposit Months must be Multiple of " & HdnDoublingPeriod.Value)
                    Return False
                End If
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
    Protected Sub Txt_IntPercent_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_IntPercent.TextChanged
        Call Validate_DayMonth(False)
        If Val(Txt_InitialDeposit.Text) > 0 Then
            Call CalcMaturityAmt(Val(Txt_InitialDeposit.Text), Val(Txt_IntPercent.Text), Val(Txt_Month.Text) + Val(Txt_Days.Text), IIf(Val(Txt_Month.Text) > 0, "M", "D"))
        End If
    End Sub

    Public Sub Calc_DueDate(Optional ByVal ForSplitAc As Boolean = False)
        Dim DueDt As Date

        If ForSplitAc = False Then
            If Val(Txt_Month.Text) > 0 Then
                DueDt = DateAdd(DateInterval.Month, Val(Txt_Month.Text), CDate(Ce_EffectiveDt.Text))
            Else
                DueDt = DateAdd(DateInterval.Day, Val(Txt_Days.Text), CDate(Ce_EffectiveDt.Text))
            End If
            Ce_DueDate.Text = DueDt
        Else
            If Val(Txt_Month.Text) > 0 Then
                DueDt = DateAdd(DateInterval.Month, Val(Txt_Mth.Text), CDate(Ce_EffectiveDt.Text))
            Else
                DueDt = DateAdd(DateInterval.Day, Val(Txt_Dys.Text), CDate(Ce_EffectiveDt.Text))
            End If
            Ce_DueDt.Text = DueDt
        End If
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
        If Len(Request.QueryString("TRANID")) > 0 Then
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
        Else
            Response.Redirect("~/Main.aspx")
        End If
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
                Ddl_SI.Enabled = False
                Ddl_Split.Enabled = False
            Else
                Ce_EffectiveDt.Enabled = True
                Ce_OpeningDate.Enabled = True
                Ce_DueDate.Enabled = True
                Txt_Month.Enabled = True
                Txt_Days.Enabled = True
                CmbInterestPayMode.Enabled = True
                Txt_IntPercent.Enabled = True
                CmbAgent.Enabled = False
                Txt_MatAcNo.Enabled = True
                Txt_MaturityAmt.Enabled = True
                Ddl_SI.Enabled = True
                Ddl_Split.Enabled = True
            End If

            If AgentCollection.Value <> "Y" Then
                CmbAgent.Enabled = False
            Else
                CmbAgent.Enabled = True
            End If
            If SchModuleCatg.Value = "RD" Then
                CmbIntPayMode.SelectedValue = "623"
                CmbInterestPayMode.Enabled = False
                Txt_Days.Enabled = False
                Ce_EffectiveDt.Enabled = False
            End If
            If SchModuleCatg.Value = "FD" Then
                Ddl_Split.Enabled = True
            Else
                Ddl_Split.Enabled = False
            End If
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Public Sub PrintBond_Athulya(ByVal Tran_No As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""


        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()


        M_Sql = "Select Top 1 B.*,T.Vr_No,Dbo.Get_AccountOperatorsWithAddres_Fn(B.Cust_ID,B.Br_Code,B.Sch_Code,B.Acc_No,GETDATE(),'D') As Operators "
        M_Sql = M_Sql & "From DepBond_Vw B Left Outer Join Transaction_T_Tbl  T On T.Tran_No=B.Tran_No And T.Br_Code=B.Br_Code And T.Sch_Code=B.Sch_Code And "
        M_Sql = M_Sql & "T.Acc_No=B.Acc_No And T.Acc_Code=T.Gl_Code  where B.Tran_No='" & Tran_No & "'"



        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        Session.Add("RptQry", M_Sql)
        Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/ADBOND.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptView.aspx", False)

    End Sub

    Public Sub PrintBond_FD(ByVal Tran_No As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""




        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()


        M_Sql = "Select Top 1 B.*,T.Vr_No,Dbo.Get_AccountOperatorsWithAddres_Fn(B.Cust_ID,B.Br_Code,B.Sch_Code,B.Acc_No,GETDATE(),'D') As Operators "
        M_Sql = M_Sql & "From DepBond_Vw B Left Outer Join Transaction_T_Tbl  T On T.Tran_No=B.Tran_No And T.Br_Code=B.Br_Code And T.Sch_Code=B.Sch_Code And "
        M_Sql = M_Sql & "T.Acc_No=B.Acc_No And T.Acc_Code=T.Gl_Code  where B.Tran_No='" & Tran_No & "'"



        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        Session.Add("RptQry", M_Sql)
        Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/FDBOND.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptView.aspx", False)

    End Sub

    Protected Sub Txt_NoOfNomine_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_NoOfNomine.TextChanged
        If Val(Txt_NoOfNomine.Text) > 0 Then
            ImgAdd.Visible = True
            ImgAdd.Focus()
        Else
            ImgAdd.Visible = False
        End If

    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

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

    Protected Sub Ddl_SI_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_SI.SelectedIndexChanged
        If Ddl_SI.SelectedIndex = 1 Then
            StandDiv.Visible = True
        Else
            StandDiv.Visible = False
        End If
    End Sub

    Protected Sub CmdAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdAdd.Click
        Try
            If Val(Txt_InitialDeposit.Text) = 0 Then
                Txt_InitialDeposit.Focus()
                ShowAlertMessage("INVALID INITIAL DEPOSIT AMOUNT ENTERED")
                Exit Sub
            End If

            If Val(Txt_DepAmt.Text) = 0 Then
                Txt_DepAmt.Focus()
                ShowAlertMessage("INVALID DEPOSIT AMOUNT ENTERED")
                Exit Sub
            End If


            If Val(Txt_Mth.Text) + Val(Txt_Dys.Text) = 0 Then
                Txt_Mth.Focus()
                ShowAlertMessage("INVALID DEPOSIT PERIOD ENTERED")
                Exit Sub
            End If

            If Val(Txt_IntRate.Text) = 0 Then
                Txt_IntRate.Focus()
                ShowAlertMessage("INVALID DEPOSIT RATE ENTERED")
                Exit Sub
            End If

            Dim dt As DataTable = DirectCast(Session("DepTbl"), DataTable)
            Dim objSum As Object
            If dt.Rows.Count > 0 Then
                objSum = dt.Compute("Sum(Dep_Amt)", "")
            End If
            If txtNoofSplit.Text = "" Then
                If Val(Txt_InitialDeposit.Text) < Val(objSum.ToString) + Val(Txt_DepAmt.Text) Then
                    Txt_InitialDeposit.Focus()
                    ShowAlertMessage("INITIAL DEPOSIT AMOUNT LESS THAN DEPOSIT AMOUNT ENTERED")
                    Exit Sub
                End If




                Dim Row As DataRow

                Row = dt.NewRow
                Row("Dep_Mth") = Val(Txt_Mth.Text)
                Row("Dep_Days") = Val(Txt_Dys.Text)
                Row("Dep_DueDt") = Replace(CommCls.DbDate(Ce_DueDt.Text), "'", "")
                Row("Dep_Amt") = Val(Txt_DepAmt.Text)
                Row("Dep_IntRate") = Val(Txt_IntRate.Text)
                Row("Dep_IntPayMode") = CmbIntPayMode.SelectedItem.Text
                If Txt_NomName.Text.Length > 0 Then
                    Row("Dep_NomNm") = Txt_NomName.Text
                    Row("Dep_NomRel") = CmbNomRel.SelectedItem.Text
                    Row("Dep_NomAdd") = Txt_NomAdd.Text
                Else
                    Row("Dep_NomNm") = ""
                    Row("Dep_NomRel") = ""
                    Row("Dep_NomAdd") = ""
                End If
                If TxtStandAcNo.Text.Length > 0 Then
                    Row("Dep_Standing") = CmbStandBr.SelectedItem.Text + " " + CmbStandSch.SelectedItem.Text + " " + TxtStandAcNo.Text
                Else
                    Row("Dep_Standing") = ""
                End If
                Row("Dep_IntPayModeCode") = CmbIntPayMode.SelectedValue
                Row("Dep_StndBr") = CmbStandBr.SelectedValue
                Row("Dep_StndSch") = CmbStandSch.SelectedValue
                Row("Dep_StndAccNo") = TxtStandAcNo.Text
                Row("Dep_NomRelCode") = CmbNomRel.SelectedValue
                Row("Dep_NomPh") = Txt_NomPh.Text
                Row("Dep_MatureAmt") = CalcMaturityAmt(Val(Txt_DepAmt.Text), Val(Txt_IntRate.Text), Val(Txt_Mth.Text) + Val(Txt_Dys.Text), IIf(Val(Txt_Mth.Text) > 0, "M", "D"), True)

                Try
                    dt.Rows.Add(Row)
                Catch ex As Exception

                End Try
                Try
                    Dim Expression As String = "Dep_Amt=0"
                    If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
                Catch ex As Exception

                End Try
            Else
                ' Dim M_Dt As New DataTable
                Dim Month As String = Txt_Month.Text.Trim()
                Dim Day As String = Txt_Days.Text.Trim()
                Dim dueDate As String = Ce_DueDate.Text.Trim()
                Dim Depamt As Integer = Txt_DepAmt.Text.Trim()
                Dim Split As Integer = Convert.ToInt32(txtNoofSplit.Text.Trim())
                Dim totalAmt As Decimal = Txt_InitialDeposit.Text.Trim()
                Dim MaturAmt As Decimal
                ' M_Dt = Session("DepTblsplit")
                dt = ClearDatatable(dt)
                Dim SplitAmt As Decimal = totalAmt / Split

                If SplitAmt >= Depamt Then



                    Dim i As Integer = 0
                    For i = 0 To Split - 1

                        If i = Split - 1 Then
                            Depamt = totalAmt
                        End If
                        MaturAmt = CalcMaturityAmt(Val(Depamt), Val(Txt_IntRate.Text), Val(Txt_Mth.Text) + Val(Txt_Dys.Text), IIf(Val(Txt_Mth.Text) > 0, "M", "D"), True)
                        ' Depamt = Math.Round(totalAmt / (Split - i))
                        dt.Rows.Add(Month, Day, dueDate, Depamt, Txt_IntRate.Text.Trim(), 0, "", "", "", "", "", "", "", "", 0, "", MaturAmt)
                        totalAmt = totalAmt - Depamt
                    Next
                Else
                    ShowAlertMessage("Enter Correct Split Amount")
                End If
            End If
            DgvDepDtls.DataSource = dt
            DgvDepDtls.DataBind()
            Session("DepTbl") = dt
            ClearDepDtls()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub ClearDepDtls()
        Txt_DepAmt.Text = ""
    End Sub

    Protected Sub Ddl_Split_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Split.SelectedIndexChanged
        If Ddl_Split.SelectedIndex = 1 Then
            DivMultipleDep.Visible = True
            Txt_Mth.Text = Txt_Month.Text
            Txt_Dys.Text = Txt_Days.Text
        Else
            DivMultipleDep.Visible = False
        End If
    End Sub

    Protected Sub Txt_Mth_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Mth.TextChanged
        Call Validate_DayMonth(True)
        Call Calc_InterestRate(True)
        Call Calc_DueDate(True)
    End Sub

    Protected Sub Txt_Dys_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Dys.TextChanged
        Call Validate_DayMonth(True)
        Call Calc_InterestRate(True)
        Call Calc_DueDate(True)
    End Sub

    Protected Sub Txt_DepAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_DepAmt.TextChanged
        Calc_InterestRate(True)
        Call Calc_DueDate(True)
    End Sub

    Protected Sub CmbStandSch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbStandSch.SelectedIndexChanged

    End Sub

    Protected Sub TxtStandAcNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtStandAcNo.TextChanged
        Dim Dt_AcDtlsN As DataTable
        Dim Dt_AcDtls As DataTable
        Dim M_CommonBL As New CommonBL
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
    End Sub

    Private Function CalcMaturityAmt(ByVal DepAmt As Double, ByVal IntRate As Double, ByVal Period As Integer, ByVal PrdType As String, Optional ByVal ForSpliAc As Boolean = False) As Double
        Dim MatAmt As Double
        Dim DoublingPeriod As Integer
        Dim I As Integer

        MatAmt = 0
        If SchModuleCatg.Value = "FD" Then
            If CmbScheme.SelectedValue = 777 Then
                Try
                    Dim M_DepositMasterBL As New DepositMasterBL
                    Dim M_Date As Date
                    M_Date = Session("TransDate")
                    MatAmt = M_DepositMasterBL.Get_RISMatAmt(CmbScheme.SelectedValue, M_Date, DepAmt, Val(Txt_Month.Text), Val(Txt_IntPercent.Text))
                    Txt_MaturityAmt.Text = Format(MatAmt, "#0.00")
                Catch ex As Exception
                    ShowAlertMessage(ex.Message)
                End Try
            Else
                MatAmt = (DepAmt * IntRate * Period) / IIf(PrdType = "M", 1200, 36500)
                MatAmt = CInt(MatAmt) + DepAmt
            End If
        ElseIf SchModuleCatg.Value = "RIS" Then
            ''DoublingPeriod = Val(HdnDoublingPeriod.Value.ToString())
            ''I = DoublingPeriod
            ''MatAmt = DepAmt
            ''For I = DoublingPeriod To Period Step DoublingPeriod
            ''    MatAmt = (MatAmt * 2)
            ''Next
            Try
                Dim M_DepositMasterBL As New DepositMasterBL
                Dim M_Date As Date
                M_Date = Session("TransDate")
                MatAmt = M_DepositMasterBL.Get_RISMatAmt(CmbScheme.SelectedValue, M_Date, DepAmt, Val(Txt_Month.Text), Val(Txt_IntPercent.Text))
                Txt_MaturityAmt.Text = Format(MatAmt, "#0.00")
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
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
        ElseIf SchType6.Value = "DEB" Then
            Try
                MatAmt = Val(Txt_InitialDeposit.Text)
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
        End Try
    End Sub

    Protected Sub CmbCashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.SelectedIndexChanged
        Try
            If CmbCashTransfer.SelectedIndex = 0 Then ' Cash
                CmbBatchNo.Enabled = False
                ImgBatch.Enabled = False
                CkBankDetails.Enabled = False
                CkBankDetails.Checked = False
                DivBankDet.Visible = False
            Else
                CmbBatchNo.Enabled = True
                ImgBatch.Enabled = True
                CkBankDetails.Enabled = True
            End If
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    'Protected Sub txtNoofSplit_TextChanged(sender As Object, e As EventArgs) Handles txtNoofSplit.TextChanged
    '    Try
    '        Dim M_Dt As New DataTable
    '        Dim Month As String = Txt_Month.Text.Trim()
    '        Dim Day As String = Txt_Days.Text.Trim()
    '        Dim dueDate As String = Ce_DueDate.Text.Trim()
    '        Dim Depamt As Integer = Txt_DepAmt.Text.Trim()
    '        Dim Split As Integer = Convert.ToInt32(txtNoofSplit.Text.Trim())
    '        Dim totalAmt As Decimal = Txt_InitialDeposit.Text.Trim()

    '        M_Dt = Session("DepTblsplit")
    '        M_Dt = ClearDatatable(M_Dt)
    '        Dim i As Integer = 0
    '        For i = 0 To Split - 1
    '            If i = Split - 1 Then
    '                Depamt = totalAmt
    '            End If
    '            ' Depamt = Math.Round(totalAmt / (Split - i))
    '            M_Dt.Rows.Add(Month, Day, dueDate, Depamt, 0, "", "", "", "", "", "", "", "", "", 0, "", 0)
    '            totalAmt = totalAmt - Depamt
    '        Next
    '        DgvDepDtls.DataSource = M_Dt
    '        DgvDepDtls.DataBind()
    '    Catch ex As Exception
    '        ShowAlertMessage(ex.Message)
    '    End Try
    'End Sub
    Private Function ClearDatatable(ByVal Dt As DataTable) As DataTable
        Dt.Rows.Clear()
        'For Each row As DataRow In Dt.Rows
        '    Dt.Rows.Remove(Dt.Rows)
        'Next
        Return Dt
    End Function

    Protected Sub CkBankDetails_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CkBankDetails.CheckedChanged
        Try
            If CkBankDetails.Checked = True Then

                DivBankDet.Visible = True
            Else

                DivBankDet.Visible = False
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub drpBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpBank.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
    End Sub

    Protected Sub CmbInterestPayMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbInterestPayMode.SelectedIndexChanged
        Calc_InterestRate()
        If Val(Txt_InitialDeposit.Text) > 0 Then
            Call CalcMaturityAmt(Val(Txt_InitialDeposit.Text), Val(Txt_IntPercent.Text), Val(Txt_Month.Text) + Val(Txt_Days.Text), IIf(Val(Txt_Month.Text) > 0, "M", "D"))
        End If
    End Sub
End Class