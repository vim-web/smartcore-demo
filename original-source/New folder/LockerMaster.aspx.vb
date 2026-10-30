Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class LockerMaster
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private EntryMode As String
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Shared M_AccHead As String
    Shared ParameterDt As DataTable
    Shared NomineeTbl As New DataTable("Table")
    Public MultipleDepTbl As New DataTable("Table")
    Dim FormID As Integer = 1098
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

                Load_DefaultValues()
                Assign_Values()
                ' CmdSave.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                ImgAdd.Attributes.Add("OnClick", "window.open('NomineeMaster.aspx','MyWindow','resizable=no,directories=no,height=350,width=750,toolbar=no,status=no,menubar=no,scrollbars=yes,model=yes');return true;")
                Session("M_EntryMode") = "NEW"

                Ce_DueDate.Attributes.Add("readonly", "readonly")
                Ce_OpeningDate.Attributes.Add("readonly", "readonly")
                Ce_IntUpto.Attributes.Add("readonly", "readonly")
                ' Call Calc_DueDate()
                'ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "enableDisableControls();", True)
                If Len(Request.QueryString("TRANID")) > 0 Then
                    GetAuthoriseDtls("LOCKER", Request.QueryString("TRANID"))
                Else
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("LOCKER", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        M_TranTbl = M_Dataset.Tables(1)
        DgvTransaction.DataSource = M_Dataset.Tables(0)
        Session("TransTbl") = M_Dataset.Tables(0)
        DgvTransaction.DataBind()
        M_CommonBL = Nothing

        GetAccountDtls(M_TranTbl)
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
        Ce_OpeningDate.Text = M_AccTbl.Rows(0).Item("Lrm_Opendate")
        Ce_EffectiveDt.Text = M_AccTbl.Rows(0).Item("Lrm_Opendate")
        Ce_DueDate.Text = M_AccTbl.Rows(0).Item("Lrm_Duedate")
        Txt_NoOfNomine.Text = M_AccTbl.Rows(0).Item("Nominee")
        Ddl_SI.SelectedValue = M_AccTbl.Rows(0).Item("Standing")
        CmbCashTransfer.SelectedIndex = M_AccTbl.Rows(0).Item("Cash")
        Cmb_LockerType.SelectedItem.Text = M_AccTbl.Rows(0).Item("Lrd_Type")
        Txt_Lkr.Text = M_AccTbl.Rows(0).Item("Lri_LockerNo")
        Txt_KeyNo.Text = M_AccTbl.Rows(0).Item("Lri_LockerKeyNo")
        Txt_RowNo.Text = M_AccTbl.Rows(0).Item("Lri_RowNo")
        Txt_ColumnNo.Text = M_AccTbl.Rows(0).Item("Lri_ColNo")
        Enabled()
    End Sub
    Private Sub Enabled()
        CmbBranch.Enabled = False
        CmbOpMode.Enabled = False
        CmbScheme.Enabled = False
        TxtNoOfOperator.Enabled = False
        Ce_OpeningDate.Enabled = False
        Ce_EffectiveDt.Enabled = False
        Ce_DueDate.Enabled = False
        Txt_NoOfNomine.Enabled = False
        Ddl_SI.Enabled = False
        CmbCashTransfer.Enabled = False
        Cmb_LockerType.Enabled = False
        Cmb_LockerNo.Visible = False
        Panel_Lkr.Visible = True
        Txt_KeyNo.Enabled = False
        Txt_RowNo.Enabled = False
        Txt_ColumnNo.Enabled = False
        Txt_RentAmount.Enabled = False
        Txt_RentPaidYears.Enabled = False
        Txt_Period.Enabled = False
        Txt_TotalRent.Enabled = False
        Txt_NoOfNomine.Enabled = False
        Txt_TockenNo.Enabled = False
        CmbCanvassedBy.Enabled = False
        CmbBatchNo.Enabled = False
        Ddl_SEC.Enabled = False
    End Sub
    Public Sub Load_DefaultValues()
        Try

            CmbBranch.SelectedValue = Session("AcOpenBranch")
            CmbScheme.SelectedValue = Session("AcOpenScheme")
            Txt_Period.Text = "0"

            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
            M_CommonBL.Fill_Pickup(CmbIntPayMode, 44, 0)
            M_CommonBL.Fill_CanvassedBy(CmbCanvassedBy)
            M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOCKER")
            M_CommonBL.Fill_Branch(CmbStandBr)
            M_CommonBL.Fill_Branch(CmbSecBranch)
            M_CommonBL.GetLockerType(Cmb_LockerType, Session("DefaultBrCode"))
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
            M_CommonBL.Fill_BatchNo(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo)
            M_CommonBL.GetSchemes(CmbSceScheme, Session("TransDate"), "DEPOSIT")
            CmbBatchNo.SelectedIndex = 0

            Txt_RentAmount.Attributes.Add("readonly", "readonly")
            Txt_TotalRent.Attributes.Add("readonly", "readonly")

            DgvTransaction.DataSource = Nothing
            Dim Dt As DataTable
            Dt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
            DgvTransaction.DataSource = Dt
            DgvTransaction.DataBind()
            PickupDt = DirectCast(Cache("PickupTbl"), DataTable)

            CmbCashTransfer.Items.Add("Cash")
            CmbCashTransfer.Items.Add("Transfer")
            CmbCashTransfer.SelectedIndex = 0


            CmbNomReq.Items.Add("No")
            CmbNomReq.Items.Add("Yes")
            CmbNomReq.SelectedIndex = 0
            Lbl_NoofNominees.Text = "Nominee"
            Txt_NoOfNomine.Visible = False
            TxtAccHolAddress.Enabled = False
            TxtAccHolName.Enabled = False
            'Cmb_LockerType.Items.Add("--SELECT--")
            'Cmb_LockerNo.Items.Add("--SELECT--")
            M_CommonBL = Nothing
            Set_Grid()
            Panel_Lkr.Visible = False

            Call Cmb_LockerType_SelectedIndexChanged(Cmb_LockerType, System.EventArgs.Empty)
            Call Cmb_LockerNo_SelectedIndexChanged(Cmb_LockerNo, System.EventArgs.Empty)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_Grid()

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
        MultipleDepTbl.Rows.Add(0, 0, "01/01/1900", 0, 0, "", "", "", "", "", "", "", "", "", 0, "")

        Session("DepTbl") = MultipleDepTbl
        DgvDepDtls.DataSource = MultipleDepTbl
        DgvDepDtls.DataBind()

    End Sub
    Private Sub Assign_Values()
        Try


            Dim M_CommonBL As New CommonBL

            CmbBranch.SelectedValue = Session("AcOpenBranch")
            CmbScheme.SelectedValue = Session("AcOpenScheme")
            CmbOpMode.SelectedValue = Session("AcOpenOpMode")
            TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            DgvCustomer.DataSource = dt
            DgvCustomer.DataBind()

            Dim HeadDt As DataTable
            HeadDt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
            Session("locAcHeadTbl") = HeadDt
            DgvTransaction.DataSource = HeadDt
            DgvTransaction.DataBind()

            M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)
            ParameterDt = M_CommonBL.GetDepositParameter(CmbScheme.SelectedValue)

            AgentCollection.Value = "N"
            'If ParameterDt.Rows.Count > 0 Then
            '    AgentCollection.Value = ParameterDt.Rows(0)("Dep_AgentCollection").ToString
            '    Txt_Month.Text = ParameterDt.Rows(0)("Dep_MinPeriod").ToString
            '    Txt_InitialDeposit.Text = "0"
            '    Calc_InterestRate()
            'End If

            Ce_OpeningDate.Text = Session("TransDate")
            Ce_EffectiveDt.Text = Session("TransDate")
            Ce_DueDate.Text = Session("TransDate")
            Ce_IntUpto.Text = Session("TransDate")
            M_CommonBL = Nothing
            'Set_SchemeCtrl()
            Set_TransactionGrid()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Set_TransactionGrid()
        Try


            Dim M_CommonBL As New CommonBL
            Dim Ds As DataSet
            Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Ce_OpeningDate.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "C", "M", Val(Txt_TotalRent.Text))
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

        End Try
    End Sub
  
    Protected Sub Cmb_LockerType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cmb_LockerType.SelectedIndexChanged
        Try

 
            Dim M_CommonBL As New CommonBL

            M_CommonBL.GetLockerNO(Cmb_LockerNo, CmbBranch.SelectedValue, Cmb_LockerType.SelectedValue)
            Call Cmb_LockerNo_SelectedIndexChanged(Me.Cmb_LockerNo, System.EventArgs.Empty)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Cmb_LockerNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cmb_LockerNo.SelectedIndexChanged
        Try


            Dim M_CommonBL As New CommonBL
            Dim TempDt As DataTable
            TempDt = M_CommonBL.Get_LockerDtls(CmbBranch.SelectedValue, Cmb_LockerType.SelectedValue, Cmb_LockerNo.SelectedValue)
            Txt_KeyNo.Text = TempDt.Rows(0).Item("Lri_LockerKeyNo")
            Txt_ColumnNo.Text = TempDt.Rows(0).Item("Lri_ColNo")
            Txt_RowNo.Text = TempDt.Rows(0).Item("Lri_RowNo")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ddl_SI_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_SI.SelectedIndexChanged
        If Ddl_SI.SelectedIndex = 1 Then
            StandDiv.Visible = True
        Else
            StandDiv.Visible = False
        End If
    End Sub

    Protected Sub Txt_NoOfNomine_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_NoOfNomine.TextChanged
        If Val(Txt_NoOfNomine.Text) > 0 Then
            ImgAdd.Visible = True
            ImgAdd.Focus()
        Else
            ImgAdd.Visible = False
        End If
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

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Dim SplitAcdt As DataTable
        Dim M_BatchNo As String = ""

        If CmbCashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text
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
        Dim M_LockerMaster As New SMARTCORE_BO.LockerMasterBO
        With M_LockerMaster

            .AccID = M_AccID
            .LrmCode = Cmb_LockerType.SelectedValue
            .LrmHirePeriod = Val(Txt_Period.Text)
            .LrmOpendate = Ce_OpeningDate.Text
            .LrmHiredate = Ce_EffectiveDt.Text
            .LrmDuedate = Ce_DueDate.Text
            .LrmRentUpto = DateSerial(Year(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")) + Val(Txt_RentPaidYears.Text), Month(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")), Day(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")))
            .LrmLocNo = Cmb_LockerNo.SelectedValue
            .TranNo = ""


            .CustId = Val(DgvCustomer.Rows(0).Cells(4).Text)
            '.AccNo = M_AccNo
            .SchCode = CmbScheme.SelectedValue
            .BrCode = CmbBranch.SelectedValue
            .LmAgentCode = 0
            .MakerID = Session("UserID")
            .CheckerID = ""
            .MakingTime = Date.Now
            .CheckingTime = "01/01/1900"
            .LrmRentAmount = Val(Txt_TotalRent.Text)

            If Ddl_SI.SelectedIndex = 0 Then
                .StBrCode = ""
                .StSchCode = ""
                .StAccNo = "'"
            Else
                .StBrCode = CmbStandBr.SelectedValue
                .StSchCode = CmbStandSch.SelectedValue
                .StAccNo = TxtStandAcNo.Text
            End If

            If Ddl_SEC.SelectedIndex = 0 Then
                .SecAccId = "0"
            Else
                .SecAccId = SecAccid.Value
            End If
        End With

        'Dim Row As DataRow
        'SplitAcdt = DirectCast(Session("DepTbl"), DataTable)
        'If Txt_NomName.Text.Length > 0 Then
        '    Row("Dep_NomNm") = Txt_NomName.Text
        '    Row("Dep_NomRel") = CmbNomRel.SelectedItem.Text
        '    Row("Dep_NomAdd") = Txt_NomAdd.Text
        'Else
        '    Row("Dep_NomNm") = ""
        '    Row("Dep_NomRel") = ""
        '    Row("Dep_NomAdd") = ""
        'End If



        Try
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



            Dim M_LockerMasterBL As New LockerMasterBL
            Dim M_Dt As DataTable

            Session("M_EntryMode") = "NEW"
            M_Dt = M_LockerMasterBL.Save_Data(M_LockerMaster, M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_BatchNo)
            '  ShowAlertMessage(" LOCKER NO : " & M_Dt.Rows(0)("Acc_No").ToString)
            Session("NomTbl") = Nothing
            M_LockerMasterBL = Nothing
            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & IIf((SchModuleCatg.Value = "FD" Or SchModuleCatg.Value = "RIS"), "&PrintBond=Y", "&PrintBond=N") & "&SECTION=" & SchModuleCatg.Value & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & SchSection.Value & "&DRCR=" & "C")
        Catch ex As Exception

        End Try
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



    Private Function Validate_Control() As Boolean

        If Val(Txt_RentAmount.Text) <= 0 Then
            Txt_RentAmount.Focus()
            ShowAlertMessage("INVALID LOCKER RENT AMOUNT ENTERED")
            Return False
        End If

        If AgentCollection.Value = "Y" And CmbCanvassedBy.SelectedIndex = CmbCanvassedBy.Items.Count - 1 Then
            ShowAlertMessage("INVALID AGENT SELECTED")
            Return False
        End If

        Return True
    End Function

'Private Sub Locate_Data(ByVal M_LockerMaster As SMARTCORE_BO.LockerMasterBO)

'If M_LockerMaster.AccID = 0 Then
'LblEntryMode.Text = "-- NEW --"
'Exit Sub
'End If
'With M_LockerMaster

'=.AccID
'=.LrmCode
'=.LrmHirePeriod
'=.LrmOpendate
'=.LrmHiredate
'=.LrmDuedate
'=.LrmRentUpto
'=.LrmLocNo
'=.TranNo

'End With
'LblEntryMode.Text = "-- VIEW --"
'End Sub


'Private Sub Txt_ID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_ID.Validating
'If Txt_ID.Text.Length = 0 Then
'Call Clear_Controls()
'Else
'Try
'Dim M_LockerMaster As New SMARTCORE_BO.LockerMasterBO
'Dim M_LockerMasterBL As New LockerMasterBL
'M_LockerMaster = M_LockerMasterBL.Locate_Data(Txt_ID.Text)
'Locate_Data(M_LockerMaster)
'M_LockerMasterBL = Nothing
'Catch ex As Exception

'End Try
'End If
    '    End Sub


    'Protected Sub Txt_RentAmount_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_RentAmount.TextChanged
    '    Set_TransactionGrid()
    '    Dim M_CommonCls As New CommonClass
    '    LblWords.Text = M_CommonCls.Towords(Val(Txt_RentAmount.Text)).ToUpper
    '    M_CommonCls = Nothing
    '    Txt_RentAmount.Text = Format(Val(Txt_RentAmount.Text), "#0.00")

    'End Sub

    Protected Sub CmbCashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.SelectedIndexChanged
        Try
            Set_TransactionGrid()

            If CmbCashTransfer.SelectedIndex = 0 Then ' Cash
                CmbBatchNo.Enabled = False
                ImgBatch.Enabled = False
            Else
                CmbBatchNo.Enabled = True
                ImgBatch.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_TotalRent_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_TotalRent.TextChanged
        Try
            Set_TransactionGrid()
            Dim M_CommonCls As New CommonClass
            LblWords.Text = M_CommonCls.Towords(Val(Txt_TotalRent.Text)).ToUpper
            M_CommonCls = Nothing
            Txt_TotalRent.Text = Format(Val(Txt_TotalRent.Text), "#0.00")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_Period_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Period.TextChanged
        Try



            Dim M_CommonBL As New CommonBL
            Dim TempDt As DataTable
            TempDt = M_CommonBL.Get_LockerRent(CmbBranch.SelectedValue, Cmb_LockerType.SelectedValue, 417, Txt_Period.Text, Ce_EffectiveDt.Text)
            Txt_RentAmount.Text = TempDt.Rows(0).Item("Lrr_Rent")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ce_EffectiveDt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ce_EffectiveDt.TextChanged
        Try

  
            If Val(Ce_EffectiveDt.Text) >= 0 Then
                Ce_DueDate.Text = DateSerial(Year(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")) + Val(Ce_EffectiveDt.Text), Month(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")), Day(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_RentPaidYears_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_RentPaidYears.TextChanged
        Try
            Txt_TotalRent.Text = Val(Txt_RentAmount.Text) * Val(Txt_RentPaidYears.Text)

            If Val(Txt_RentPaidYears.Text) >= 0 Then
                'Ce_DueDate.Text = DateSerial(Year(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")) + Val(Ce_EffectiveDt.Text), Month(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")), Day(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")))
                Ce_DueDate.Text = DateSerial(Year(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")) + Val(Txt_RentPaidYears.Text), Month(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")), Day(Format(Convert.ToDateTime(Ce_OpeningDate.Text), "dd/MM/yyyy")))
            End If
            Set_TransactionGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/main.aspx")
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

    Protected Sub Ddl_SEC_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_SEC.SelectedIndexChanged
        If Ddl_SEC.SelectedIndex = 1 Then
            Security.Visible = True
        Else
            Security.Visible = False
        End If
    End Sub
    Private Sub SecurityDetails()
        Try

            Dim Dt_AcDtlsN As DataTable
            Dim M_CommonBL As New CommonBL
            If TxtSecAccNo.Text.Trim.Length <= 7 Then
                TxtSecAccNo.Text = Right("000" + CmbSecBranch.SelectedValue, 3) + Right("000" + CmbSceScheme.SelectedValue, 3) + Right("0000000" + TxtSecAccNo.Text, 7)
            End If
            Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbSecBranch.SelectedValue, CmbSceScheme.SelectedValue, TxtSecAccNo.Text, Session("TransDate"))
            If Dt_AcDtlsN.Rows.Count > 0 Then
                If (Dt_AcDtlsN.Rows(0)("Acc_Status").ToString() = "C") Then
                    ShowAlertMessage("Account is Closed!")
                Else
                    CmbSceScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Sch_Code").ToString()
                    CmbSecBranch.SelectedValue = Dt_AcDtlsN.Rows(0)("Br_Code").ToString()
                    SecAccid.Value = Dt_AcDtlsN.Rows(0)("Acc_id").ToString()
                    TxtAccHolName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
                    TxtAccHolAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TxtSecAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSecAccNo.TextChanged
        SecurityDetails()
    End Sub
End Class