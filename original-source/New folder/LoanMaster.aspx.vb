Imports SMARTCORE_BL
Imports SMARTCORE_DAL

Partial Public Class LoanMaster
    Inherits System.Web.UI.Page

    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Shared M_AccHead As String
    Shared Obj_UserRights As Object
    Dim FormID As Integer = 1091
    Public AccHolderTbl As New DataTable("Table")
    Public AdditionalMember As New DataTable("Table")
    Shared ParameterDt As DataTable
    Public CropTbl As New DataTable("Table")
    Public OrnamentTbl As New DataTable("Table")
    Public InsuranceTbl As New DataTable("Table")
    Public BondTbl As New DataTable("Table")
    Public VehicleTbl As New DataTable("Table")
    Public DepositTbl As New DataTable("Table")
    Public SalaryTbl As New DataTable("Table")
    Shared Emi_IRRParameter As DataTable
    Shared EmiParameter As DataTable

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            HdnDate.Value = Session("TransDate")
            Load_DefaultValues()

            Txt_LoanDate.Attributes.Add("readonly", "readonly")
            Txt_LoanDate.Enabled = False
            Txt_Interest.Attributes.Add("readonly", "readonly")
            Txt_DueDate.Attributes.Add("readonly", "readonly")
            Txt_InstNo.Attributes.Add("readonly", "readonly")
            TxtAdvInst.Attributes.Add("readonly", "readonly")
            TxtIRR.Attributes.Add("readonly", "readonly")
            Txt_PenalInterest.Attributes.Add("readonly", "readonly")
            Txt_DueDate.Enabled = False
            'Txt_Month .Attributes.Add("readonly", "readonly")
            'Txt_PenalInterest.Attributes.Add("readonly", "readonly")
            Txt_InterestUpto.Attributes.Add("readonly", "readonly")
            Txt_InterestUpto.Enabled = False
            Txt_BalanceAmount.Attributes.Add("readonly", "readonly")
            Txt_BalanceAmount.Enabled = False
            '-------Modified By Mohammed Anshad on 16/01/2016
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Session("SearchType") = "ACCOUNTNUMBER"
            '--------------------------------------END
            Set_Grid()
            If Len(Request.QueryString("TRANID")) > 0 Then
                GetAuthoriseDtls("MLOAN", Request.QueryString("TRANID"))
                Hid_ODPStatus.Value = "Y"

                If Len(Request.QueryString("DEL")) > 0 Then

                    CmdSave.Visible = False
                    CmdClear.Visible = False
                    cmdDelete.Visible = True


                End If
            Else
                Assign_Values()
            End If

        End If
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
    Private Sub Set_Grid()
        Try
            DgvLand.DataSource = Nothing
            Dim LandTbl As New DataTable("Table")
            LandTbl.Columns.Add("Tax", GetType(String))
            LandTbl.Columns.Add("BuildingNo", GetType(String))
            LandTbl.Columns.Add("DeedNo", GetType(String))
            LandTbl.Columns.Add("DeedDate", GetType(String))
            LandTbl.Columns.Add("WardNo", GetType(String))
            LandTbl.Columns.Add("Extend", GetType(String))
            LandTbl.Columns.Add("District", GetType(String))
            LandTbl.Columns.Add("Tehsil", GetType(String))
            LandTbl.Columns.Add("Panchayath", GetType(String))
            LandTbl.Columns.Add("Village", GetType(String))
            LandTbl.Columns.Add("SubRegOffice", GetType(String))
            LandTbl.Columns.Add("MarketValue", GetType(String))
            LandTbl.Columns.Add("TaxNo", GetType(String))
            LandTbl.Columns.Add("TaxDate", GetType(String))
            LandTbl.Columns.Add("SurveyNo", GetType(String))
            LandTbl.Columns.Add("PossessionCrtNo", GetType(String))
            LandTbl.Columns.Add("PossessionCrtDate", GetType(String))
            LandTbl.Columns.Add("EasyNo", GetType(String))
            LandTbl.Columns.Add("EasyDate", GetType(String))
            LandTbl.Columns.Add("LoactionMapNo", GetType(String))
            LandTbl.Columns.Add("LocationMapdate", GetType(String))
            LandTbl.Columns.Add("RecepitNo", GetType(String))
            LandTbl.Columns.Add("RecepitDate", GetType(String))
            LandTbl.Columns.Add("GahanNo", GetType(String))
            LandTbl.Columns.Add("GahanDate", GetType(String))
            LandTbl.Columns.Add("PofAtNo", GetType(String))
            LandTbl.Columns.Add("PofAtNoDate", GetType(String))


            LandTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("LandTbl") = LandTbl
            DgvLand.DataSource = LandTbl
            DgvLand.DataBind()

            DgvCrop.DataSource = Nothing
            CropTbl.Columns.Add("CropName", GetType(String))
            CropTbl.Columns.Add("SurveyNo", GetType(String))
            CropTbl.Columns.Add("Extend", GetType(String))
            CropTbl.Columns.Add("TaxNo", GetType(String))
            CropTbl.Columns.Add("TaxDate", GetType(String))
            CropTbl.Columns.Add("Village", GetType(String))
            CropTbl.Columns.Add("PcNo", GetType(String))
            CropTbl.Columns.Add("PcDate", GetType(String))
            CropTbl.Rows.Add("", 0, 0)
            Session("CropTbl") = CropTbl
            DgvCrop.DataSource = CropTbl
            DgvCrop.DataBind()

            DgvOrnament.DataSource = Nothing
            OrnamentTbl.Columns.Add("OrnamentType", GetType(String))
            OrnamentTbl.Columns.Add("OrnamentDtls", GetType(String))
            OrnamentTbl.Columns.Add("GoldCount", GetType(Integer))
            OrnamentTbl.Columns.Add("Remarks", GetType(String))
            OrnamentTbl.Columns.Add("NetWeight", GetType(String))
            OrnamentTbl.Columns.Add("GrossWeight", GetType(String))
            OrnamentTbl.Rows.Add("", "", 0, "", "", "")
            Session("OrnamentTbl") = OrnamentTbl
            DgvOrnament.DataSource = OrnamentTbl
            DgvOrnament.DataBind()

            DgvInsurance.DataSource = Nothing
            InsuranceTbl.Columns.Add("InsuranceCompany", GetType(String))
            InsuranceTbl.Columns.Add("PolicyName", GetType(String))
            InsuranceTbl.Columns.Add("PolicyNo", GetType(String))
            InsuranceTbl.Columns.Add("SumAssured", GetType(Double))
            InsuranceTbl.Columns.Add("MaturityDate", GetType(String))
            InsuranceTbl.Columns.Add("SurrenderValue", GetType(String))
            InsuranceTbl.Rows.Add("", "", "", 0, "", "")
            Session("InsuranceTbl") = InsuranceTbl
            DgvInsurance.DataSource = InsuranceTbl
            DgvInsurance.DataBind()

            DgvBond.DataSource = Nothing
            BondTbl.Columns.Add("IssuedPo", GetType(String))
            BondTbl.Columns.Add("SerialNo", GetType(Double))
            BondTbl.Columns.Add("PhaseValue", GetType(String))
            BondTbl.Columns.Add("DueDate", GetType(String))
            BondTbl.Columns.Add("TotPhaseVal", GetType(String))
            BondTbl.Rows.Add("", 0, "", "", "")
            Session("BondTbl") = BondTbl
            DgvBond.DataSource = BondTbl
            DgvBond.DataBind()

            DgvVehicle.DataSource = Nothing
            VehicleTbl.Columns.Add("VType", GetType(String))
            VehicleTbl.Columns.Add("RegionNo", GetType(String))
            VehicleTbl.Columns.Add("ChasisNo", GetType(String))
            VehicleTbl.Columns.Add("EngineNo", GetType(String))
            VehicleTbl.Columns.Add("OldNew", GetType(String))
            VehicleTbl.Columns.Add("Brand", GetType(String))
            VehicleTbl.Columns.Add("Model", GetType(String))
            VehicleTbl.Columns.Add("KeySub", GetType(String))
            VehicleTbl.Columns.Add("KeyNo", GetType(String))
            VehicleTbl.Columns.Add("PermitScope", GetType(String))
            VehicleTbl.Columns.Add("RtOffice", GetType(String))
            VehicleTbl.Columns.Add("LienMarked", GetType(String))
            VehicleTbl.Columns.Add("LienNo", GetType(String))
            VehicleTbl.Columns.Add("LienDate", GetType(String))
            VehicleTbl.Columns.Add("LienReleased", GetType(String))
            VehicleTbl.Columns.Add("TextDetails", GetType(String))
            VehicleTbl.Columns.Add("TaxDate", GetType(String))
            VehicleTbl.Columns.Add("InsuranceDetails", GetType(String))
            VehicleTbl.Columns.Add("InsuranceDate", GetType(String))
            VehicleTbl.Columns.Add("InsuranceValue", GetType(String))
            VehicleTbl.Columns.Add("OtherDetails", GetType(String))
            VehicleTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("VehicleTbl") = VehicleTbl
            DgvVehicle.DataSource = VehicleTbl
            DgvVehicle.DataBind()

            DgvDeposit.DataSource = Nothing
            DepositTbl.Columns.Add("OtherBankAC", GetType(String))
            DepositTbl.Columns.Add("Branch1", GetType(String))
            DepositTbl.Columns.Add("BranchDtls1", GetType(String))
            DepositTbl.Columns.Add("Scheme", GetType(String))
            DepositTbl.Columns.Add("schemeDtls", GetType(String))
            DepositTbl.Columns.Add("AcNo", GetType(String))
            DepositTbl.Columns.Add("CertificateNo", GetType(String))
            DepositTbl.Columns.Add("DepositAmt", GetType(String))
            DepositTbl.Columns.Add("AmtOutstanding", GetType(String))
            DepositTbl.Columns.Add("DueDate", GetType(String))
            DepositTbl.Columns.Add("BankName", GetType(String))
            DepositTbl.Columns.Add("Branch", GetType(String))
            DepositTbl.Columns.Add("AcType", GetType(String))
            DepositTbl.Columns.Add("CustomerNo", GetType(String))
            DepositTbl.Columns.Add("Name", GetType(String))
            DepositTbl.Columns.Add("Addr", GetType(String))
            DepositTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("DepositTbl") = DepositTbl
            DgvDeposit.DataSource = DepositTbl
            DgvDeposit.DataBind()

            DgvAdditionalMem.DataSource = Nothing
            AdditionalMember.Columns.Add("MemDtls", GetType(String))
            AdditionalMember.Columns.Add("MemBrCode", GetType(String))
            AdditionalMember.Columns.Add("MemSchCode", GetType(String))
            AdditionalMember.Columns.Add("MemSch", GetType(String))
            AdditionalMember.Columns.Add("MemNo", GetType(String))
            AdditionalMember.Columns.Add("CustID", GetType(String))
            AdditionalMember.Columns.Add("CustName", GetType(String))
            AdditionalMember.Columns.Add("Relation", GetType(String))
            AdditionalMember.Columns.Add("CustAdd", GetType(String))
            AdditionalMember.Columns.Add("CustType", GetType(String))
            AdditionalMember.Columns.Add("CustCatg", GetType(String))
            AdditionalMember.Columns.Add("HouseNm", GetType(String))
            AdditionalMember.Columns.Add("Place", GetType(String))
            AdditionalMember.Columns.Add("Dist", GetType(String))
            AdditionalMember.Columns.Add("State", GetType(String))
            AdditionalMember.Columns.Add("Country", GetType(String))
            AdditionalMember.Columns.Add("Pincode", GetType(String))
            AdditionalMember.Columns.Add("MblNo", GetType(String))
            AdditionalMember.Columns.Add("LandPh", GetType(String))
            AdditionalMember.Columns.Add("RelType", GetType(String))
            AdditionalMember.Columns.Add("Dob", GetType(String))
            AdditionalMember.Columns.Add("Age", GetType(String))

            AdditionalMember.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("AdditionalMember") = AdditionalMember
            DgvAdditionalMem.DataSource = AdditionalMember
            DgvAdditionalMem.DataBind()

            Grid_Salary.DataSource = Nothing
            SalaryTbl.Columns.Add("CertiNo", GetType(String))
            SalaryTbl.Columns.Add("FirmName", GetType(String))
            SalaryTbl.Columns.Add("Branch", GetType(String))
            SalaryTbl.Columns.Add("EmployeeName", GetType(String))
            SalaryTbl.Columns.Add("Designation", GetType(String))
            SalaryTbl.Columns.Add("JoinDate", GetType(String))
            SalaryTbl.Columns.Add("BasicSal", GetType(String))
            SalaryTbl.Columns.Add("GrossPay", GetType(String))
            SalaryTbl.Columns.Add("NetPay", GetType(String))
            SalaryTbl.Columns.Add("ServicePeriod", GetType(String))
            SalaryTbl.Columns.Add("RetirementDate", GetType(String))
            SalaryTbl.Columns.Add("IssuingAuthority", GetType(String))
            SalaryTbl.Columns.Add("IssuingAuthorityAddress", GetType(String))
            SalaryTbl.Columns.Add("IssueDate", GetType(String))
            SalaryTbl.Columns.Add("Remarks", GetType(String))
            SalaryTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Grid_Salary.DataSource = SalaryTbl
            Grid_Salary.DataBind()
            Session("SalaryTbl") = SalaryTbl
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("MLOAN", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        Dim M_MemTbl As DataTable

        M_TranTbl = M_Dataset.Tables(0)
        M_MemTbl = M_Dataset.Tables(1)
        DgvTransaction.DataSource = M_Dataset.Tables(2)
        '  Session("TransTbl") = M_Dataset.Tables(2) ****
        DgvTransaction.DataBind()
        M_CommonBL = Nothing

        GetAccountDtls(M_TranTbl, M_MemTbl)
        PnlAuthorise.Enabled = False
        '  Set_TransactionGrid()
    End Sub

    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable, ByVal M_MemTbl As DataTable)

        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        CmbOpMode.SelectedValue = M_AccTbl.Rows(0).Item("Lm_OperationMode")
        CmbScheme.SelectedValue = M_AccTbl.Rows(0).Item("Sch_Code")
        TxtNoOfOperator.Text = M_AccTbl.Rows(0).Item("NoOfOp")
        Txt_LoanDate.Text = M_AccTbl.Rows(0).Item("Lm_LoanDate")
        TxtAdvInst.Text = M_AccTbl.Rows(0).Item("LM_AdvEMI")
        TxtIRR.Text = M_AccTbl.Rows(0).Item("LM_IRR_Rate")
        If M_AccTbl.Rows(0).Item("Lm_PeriodType") = "M" Then
            Txt_Month.Text = M_AccTbl.Rows(0).Item("Lm_Period")
        Else
            Txt_Days.Text = M_AccTbl.Rows(0).Item("Lm_Period")
        End If

        If Val(M_AccTbl.Rows(0).Item("PlEligibleAmnt")) > 0 Then
            LblPl.Visible = True
            LblPLEligibleAmnt.Visible = True
            LblPLEligibleAmnt.Text = Format(Val(M_AccTbl.Rows(0).Item("PlEligibleAmnt")), "#0.00")
        Else
            LblPl.Visible = False
            LblPLEligibleAmnt.Visible = False
        End If
        Txt_DueDate.Text = M_AccTbl.Rows(0).Item("Lm_DueDate")
        Txt_LoanAmount.Text = M_AccTbl.Rows(0).Item("Lm_LoanAmount")
        Txt_InterestUpto.Text = M_AccTbl.Rows(0).Item("Lm_LastIntDate")
        Txt_BalanceAmount.Text = M_AccTbl.Rows(0).Item("Lm_Balance")
        Txt_InstNo.Text = M_AccTbl.Rows(0).Item("Lm_InstallmentNo")
        Txt_InstAmt.Text = M_AccTbl.Rows(0).Item("Lm_InstallmentAmount")
        Ddl_Purpose.SelectedValue = M_AccTbl.Rows(0).Item("Lm_Mainpurpose")
        Ddl_SubPurpose.SelectedValue = M_AccTbl.Rows(0).Item("Lm_Subpurpose")
        Txt_IssuedAmt.Text = M_AccTbl.Rows(0).Item("Lm_LoanAmount")
        Ddl_SanctionedBy.SelectedValue = M_AccTbl.Rows(0).Item("Lm_SanctionBy")
        Ddl_Priority.SelectedIndex = M_AccTbl.Rows(0).Item("Lm_Priority")
        Txt_GrahanNo.Text = M_AccTbl.Rows(0).Item("Lm_RegNo")
        Txt_Interest.Text = M_AccTbl.Rows(0).Item("Lm_IntRate")
        Txt_PenalInterest.Text = M_AccTbl.Rows(0).Item("Lm_PenalRate")
        Ddl_CashTransfer.SelectedIndex = M_AccTbl.Rows(0).Item("Cash")
        Txt_ReslDt.Text = M_AccTbl.Rows(0).Item("Lm_ResDate")
        Txt_wrdNo.Text = M_AccTbl.Rows(0).Item("LM_WardCode")
        Txt_ReslNo.Text = M_AccTbl.Rows(0).Item("Lm_ResNo")
        Txt_Cent.Text = M_AccTbl.Rows(0).Item("LM_Cent")
        CmbCanvassedBy.SelectedValue = M_AccTbl.Rows(0).Item("Lm_Introducedby")

        DgvCustomer.DataSource = M_MemTbl
        DgvCustomer.DataBind()

    End Sub
    Private Sub DeleteCust(ByVal CustCode As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("AccHolderTblSur"), DataTable)
            For i = 0 To DgvCustSur.Rows.Count - 1
                If dt.Rows.Item(i).Item(5).ToString = CustCode Then
                    dt.Rows.Item(i).Delete()
                    GoTo otherow
                End If
            Next i
otherow:
            DgvCustSur.DataSource = dt
            DgvCustSur.DataBind()
            Session("AccHolderTblSur") = dt


            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub DgvCustSur_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles DgvCustSur.RowDeleting
        Try
            Dim CustCode As String = CInt(DgvCustSur.DataKeys(e.RowIndex).Value)
            DeleteCust(CustCode)
        Catch ex As Exception

        End Try
    End Sub
 
    Private Sub Load_CustomerData(ByVal SType As String)
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_CustMasterBL As New CustMasterBL

            ''''''''''''''''''''''''''''''''''gowtham
            Dim M_PhotoID As New SMARTCORE_BO.CustIdentityBO
            Dim M_ADDID As New SMARTCORE_BO.CustIdentityBO

            M_CustMaster = M_CustMasterBL.Locate_Data(Val(HdnSurCustID.Value), M_PresAddress, M_PermAddress, M_PhotoID, M_ADDID)
            Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress, SType)
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO, ByVal SType As String)
        Try
            Dim Address As String
            Address = M_PresAddress.AddAddress1 + "," + M_PresAddress.AddAddress2 + "," + M_PresAddress.AddAddress3 + "," + M_PresAddress.AddPinCode + M_PresAddress.AddDist
            Dim dt As DataTable
            If SType.ToUpper = "S" Then
                dt = DirectCast(Session("AccHolderTblSur"), DataTable)
            End If

            If SType.ToUpper = "A" Then
                dt = DirectCast(Session("AdditionalMember"), DataTable)
            End If

            Dim Row As DataRow
            Row = dt.NewRow
            If Len(HdnSurMemNo.Value.Trim) > 0 Then
                Row("MemDtls") = CmbSurMemType.SelectedItem.Text.Trim + " " + HdnSurMemNo.Value.Trim + " (" + CmbSurBranch.SelectedValue + " )"
            End If

            If SType.ToUpper = "A" Then
                Row("MemDtls") = CmbSurMem.SelectedItem.Text.Trim + " " + HdnSurMemNo.Value.Trim + " (" + CmbAdMemBranch.SelectedValue + " )"

            End If
            Row("MemBrCode") = CmbSurBranch.SelectedValue
            Row("MemSchCode") = CmbSurMemType.SelectedValue
            Row("MemSch") = CmbSurMemType.SelectedValue
            Row("MemNo") = HdnSurMemNo.Value
            Row("CustID") = HdnSurCustID.Value
            Row("CustName") = M_CustMaster.CustName
            Row("Relation") = ""
            Row("CustAdd") = Address
            Row("CustType") = M_CustMaster.CustTypeCode
            Row("CustCatg") = M_CustMaster.CustCatgCode
            Row("HouseNm") = M_PresAddress.AddAddress1
            Row("Place") = M_PresAddress.AddAddress2
            Row("Dist") = M_PresAddress.AddDist
            Row("State") = M_PresAddress.AddAddress3
            Row("Country") = M_PresAddress.AddCountry
            Row("Pincode") = M_PresAddress.AddPinCode
            Row("MblNo") = M_CustMaster.CustMobile
            Row("LandPh") = M_CustMaster.CustPhone1
            Row("RelType") = M_CustMaster.CustRelTypCode
            If IsDate(M_CustMaster.CustDobInc) = False Then
                Row("Dob") = "01/01/1900"
            Else
                Row("Dob") = M_CustMaster.CustDobInc
            End If
            Row("Age") = Val(M_CustMaster.CustAge)
            Try
                dt.Rows.Add(Row)
                'ClearCustomer()
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try

            Try
                Dim Expression As String = "CustID=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try
            'If RadioSecurity.SelectedValue.ToString() = "Surety" Then
            If SType.ToUpper = "S" Then
                DgvCustSur.DataSource = dt
                DgvCustSur.DataBind()
                Session("AccHolderTblSur") = dt
            End If
            If SType.ToUpper = "A" Then
                DgvAdditionalMem.DataSource = dt
                DgvAdditionalMem.DataBind()
                Session("AdditionalMember") = dt
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Private Sub Assign_Values()
        Dim ParameterDt As DataTable
        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
        CmbOpMode.SelectedValue = Session("AcOpenOpMode")
        TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
        Dim StrScheme As String = Session("AcOpenScheme")
        If StrScheme.ToLower = "cc" Or StrScheme.ToLower = "od" Then
            lbl_BondDate.Visible = True
            lbl_BondNo.Visible = True
            Txt_BondNo.Visible = True
            Txt_BondDate.Visible = True
        Else
            lbl_BondDate.Visible = False
            lbl_BondNo.Visible = False
            Txt_BondNo.Visible = False
            Txt_BondDate.Visible = False
        End If
        Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
        Txt_MobileNo.Text = dt.Rows(0)("MblNo").ToString()
        DgvCustomer.DataSource = dt
        DgvCustomer.DataBind()

        HdnMemBrCode.Value = dt.Rows.Item(0)(1).ToString()
        HdnMemType.Value = dt.Rows.Item(0)(2).ToString()
        HdnMemNo.Value = dt.Rows.Item(0)(4).ToString()
        HdnCustID.Value = dt.Rows.Item(0)(5).ToString()

        Dim M_BankRate As Double
        Dim M_CommonBL As New CommonBL

        M_CommonBL.Fill_SchemePurpose(Ddl_Purpose, CmbScheme.SelectedValue)
        M_CommonBL.Fill_Pickup(CmbOrnamentType, 30, 0)
        M_CommonBL.Fill_Pickup(Ddl_SubPurpose, 32, Val(Ddl_Purpose.SelectedValue))
        M_CommonBL.Fill_Directors(CmbDirector)
        M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)

        Txt_LoanDate.Text = Session("TransDate")
        ParameterDt = M_CommonBL.GetLoanParameter(CmbScheme.SelectedValue)
        If (ParameterDt.Rows.Count > 0) Then
            HdnPrdType.Value = ParameterDt.Rows(0).Item("Lnp_PeriodType")
            HdnEMIType.Value = ParameterDt.Rows(0).Item("Lnp_EMIType")
            TxtAdvInst.Text = Val(ParameterDt.Rows(0).Item("Lnp_Adv_EmiNos"))
            If ParameterDt.Rows(0).Item("Lnp_EMIType") = "Y" Then
                btnEMI.Enabled = True
            Else
                btnEMI.Enabled = False
            End If
            Txt_LoanDate.Text = Session("TransDate")
            HdnGLSecScheme.Value = ParameterDt.Rows(0).Item("GoldSecurityReq")
            If ParameterDt.Rows(0).Item("GoldSecurityReq") = "Y" Then
                LblPl.Visible = True
                LblPLEligibleAmnt.Visible = True
                Dim M_PLdt As New DataTable
                M_PLdt = M_CommonBL.PlEligibleAmt(HdnMemNo.Value, Session("TransDate"))
                If M_PLdt.Rows.Count > 0 Then
                    HdnPLEligible.Value = Val(M_PLdt.Rows(0).Item("PlEligible"))
                    LblPLEligibleAmnt.Text = Format(Val(HdnPLEligible.Value), "#0.00")
                Else
                    HdnPLEligible.Value = 0
                    LblPLEligibleAmnt.Text = "0.00"
                End If
            Else
                LblPl.Visible = False
                LblPLEligibleAmnt.Visible = False
            End If

            If ParameterDt.Rows(0).Item("Lnp_PeriodType") = "D" Then
                Dim Due_Dt As New DataTable
                Txt_Days.Text = ParameterDt.Rows(0).Item("Lnp_MinPeriod")
                Due_Dt = M_CommonBL.Get_DueDate(CmbScheme.SelectedValue, Txt_Days.Text, Txt_LoanDate.Text)
                Txt_DueDate.Text = Due_Dt.Rows(0).Item("Due_Date")
            Else
                Txt_Month.Text = ParameterDt.Rows(0).Item("Lnp_MinPeriod")
                Txt_DueDate.Text = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, Val(Txt_Month.Text), CDate(Txt_LoanDate.Text)))
            End If

            Txt_InstNo.Text = ParameterDt.Rows(0).Item("Lnp_NoOfInstallments")



            M_CommonBL = Nothing
            Txt_LoanAmount_TextChanged(Me.Txt_LoanAmount, System.EventArgs.Empty)
            Txt_InstNo_TextChanged(Me.Txt_InstNo, System.EventArgs.Empty)
        End If

    End Sub
   
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Branch(CmbSurBranch)
        M_CommonBL.Fill_Branch(CmbAdMemBranch)
        M_CommonBL.GetSchemes(CmbSurMemType, Session("TransDate"), ("SHARE"))
        M_CommonBL.GetSchemes(CmbSurMem, Session("TransDate"), ("SHARE"))
        M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
        M_CommonBL.Fill_Pickup(Ddl_SanctionedBy, 33, 0)
        M_CommonBL.Fill_Pickup(Ddl_Purpose, 31, 0)
        M_CommonBL.Fill_Pickup(Ddl_SubPurpose, 32, Ddl_Purpose.SelectedValue)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN", "GEN.LOAN','OD")
        M_CommonBL.Fill_BatchNo(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo)
        M_CommonBL.Fill_CanvassedBy(CmbCanvassedBy)
        M_CommonBL.Fill_Branch(CmbTrfrBr)
        M_CommonBL.GetSchemes(CmbTrfrSch, Session("TransDate"), "DEPOSIT", "SB','CA")
        CmbTrfrBr.SelectedValue = Session("DefaultBrCode")

        CmbBatchNo.SelectedIndex = 0
        DgvTransaction.DataSource = Nothing
        Dim Dt As DataTable
        Dt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
        DgvTransaction.DataSource = Dt
        DgvTransaction.DataBind()
        PickupDt = DirectCast(Cache("PickupTbl"), DataTable)
        M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)

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

        M_CommonBL = Nothing
        Ddl_Priority.Items.Add("NO")
        Ddl_Priority.Items.Add("YES")
        Ddl_Priority.SelectedIndex = 0
        Ddl_CashTransfer.Items.Add("CASH")
        Ddl_CashTransfer.Items.Add("TRANSFER")
        Ddl_CashTransfer.SelectedIndex = 0
        CmbNomReq.Items.Add("No")
        CmbNomReq.Items.Add("Yes")
        CmbNomReq.SelectedIndex = 0
        Txt_LoanDate.Text = Session("TransDate")
        Txt_InterestUpto.Text = Session("TransDate")



        If Session("Lm_LoanAmount") = Nothing Then
            Txt_LoanAmount.Text = "0"
        Else
            
            Txt_LoanAmount.Text = Val(Session("Lm_LoanAmount"))
            Txt_IssuedAmt.Text = Val(Session("Lm_LoanAmount"))

        End If
        CheckBox1.Visible = False
        cmdDelete.Visible = False

    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub Ddl_Purpose_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Purpose.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Pickup(Ddl_SubPurpose, 32, Ddl_Purpose.SelectedValue)
        M_CommonBL = Nothing
    End Sub
    Public Sub Calc_InterestRate()
        Dim M_CommonBL As New CommonBL
        Txt_Interest.Text = Format(M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", IIf(Txt_Month.Text.Trim.Length, Val(Txt_Month.Text), Val(Txt_Days.Text)), IIf(Txt_Month.Text.Trim.Length, "M", "D"), Val(Txt_LoanAmount.Text), CommonBL.Intr_RateTypes.INTR_RATE_NORMAL, "", "", "Y"), "#0.00")
        Txt_PenalInterest.Text = Format(M_CommonBL.Get_Intr_Rate(Session("TransDate"), CmbScheme.SelectedValue, "", IIf(Txt_Month.Text.Trim.Length, Val(Txt_Month.Text), Val(Txt_Days.Text)), IIf(Txt_Month.Text.Trim.Length, "M", "D"), Val(Txt_LoanAmount.Text), CommonBL.Intr_RateTypes.INTR_RATE_PENAL, "", "", "Y"), "#0.00")
        M_CommonBL = Nothing
    End Sub

    Protected Sub Txt_SanctionedAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_IssuedAmt.TextChanged
        If Val(Txt_IssuedAmt.Text) > 100000 And Ddl_CashTransfer.SelectedValue = "CASH" Then
            ShowAlertMessage("Change CASH mode to TRANSFER mode.")
            Ddl_CashTransfer.Focus()
            Txt_IssuedAmt.Text = ""
            Txt_BalanceAmount.Text = ""
            Exit Sub
            'CmbCashTransfer.SelectedIndex = 1
            'CmbCashTransfer_SelectedIndexChanged(Me.CmbCashTransfer, EventArgs.Empty)
        End If
        Calc_InterestRate()
        Set_TransactionGrid()
    End Sub
    Private Sub Set_TransactionGrid()
        Dim M_CommonBL As New CommonBL
        Dim Ds As DataSet
        Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Txt_LoanDate.Text, IIf(Ddl_CashTransfer.SelectedIndex = 0, "C", "T"), "D", "M", Val(Txt_IssuedAmt.Text), HdnCustID.Value, Val(Txt_LoanAmount.Text), Txt_DueDate.Text)
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
    End Sub

    Protected Sub Ddl_CashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_CashTransfer.SelectedIndexChanged
        If Ddl_CashTransfer.SelectedValue = "CASH" And Val(Txt_LoanAmount.Text) > 100000 Then
            Txt_SanctionedAmt_TextChanged(Me.Txt_IssuedAmt, System.EventArgs.Empty)
        Else
            Txt_LoanAmount_TextChanged(Me.Txt_LoanAmount, System.EventArgs.Empty)
        End If
        Set_TransactionGrid()

        If Ddl_CashTransfer.SelectedIndex = 0 Then ' Cash
            CmbBatchNo.Enabled = False
            ImgBatch.Enabled = False
            ' TrfrDiv.Visible = False
            ChkBank.Checked = False
            ChkBank.Enabled = False
            'DivBankDetails.Visible = False
        Else
            ' TrfrDiv.Visible = True
            CmbBatchNo.Enabled = True
            ImgBatch.Enabled = True
            ' DivBankDetails.Visible = True
            ChkBank.Enabled = True
        End If
        If Ddl_CashTransfer.SelectedValue = "TRANSFER" Then CheckBox1.Visible = True
    End Sub

    Protected Sub Txt_Month_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Month.TextChanged
        If Val(Txt_Month.Text) >= 0 Then
            Txt_Days.Text = ""
            Txt_DueDate.Text = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, Val(Txt_Month.Text), CDate(Txt_LoanDate.Text)))
            Calc_InterestRate()
            Txt_InstNo.Text = Txt_Month.Text
            Call Txt_InstNo_TextChanged(Me.Txt_InstNo, System.EventArgs.Empty)
        End If
    End Sub

    Protected Sub Txt_Days_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Days.TextChanged
        If Val(Txt_Days.Text) >= 0 Then
            Txt_Month.Text = ""
            Dim M_commonBl As New CommonBL
            Dim parameterDt As DataTable
            parameterDt = M_commonBl.Get_DueDate(CmbScheme.SelectedValue, Txt_Days.Text, Txt_LoanDate.Text)
            ''Txt_DueDate.Text = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Day, Val(Txt_Days.Text), CDate(Txt_LoanDate.Text)))
            Txt_DueDate.Text = parameterDt.Rows(0).Item("Due_Date")
            Calc_InterestRate()
            Call Txt_InstNo_TextChanged(Me.Txt_InstNo, System.EventArgs.Empty)
        End If
    End Sub
    Private Function CashControl() As Boolean
        Try
            If Ddl_CashTransfer.SelectedItem.Text <> "CASH" Then
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
            M_Cash = M_con.GetTanLimit(Session("DefaultBrCode").ToString(), CmbScheme.SelectedValue, custId, CashRctTotal + CashPayTotal, Ddl_CashTransfer.SelectedItem.Text, Session("TransDate"))

            If M_Cash.Rows(0).Item("Status") = "N" Then
                ShowAlertMessage("Customer Cash Limit Exceeded, Change Cash to Transfer")
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function
    Protected Sub Btn_Save_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Dim M_BatchNo As String = ""
        'If Not ((Val(Session("MaxTranLimit").ToString) >= (Val(LblRctCashTotal.Text) + Val(LblRctTransferTotal.Text) + Val(LblPayCashTotal.Text) + Val(LblPayTransferTotal.Text)))) Then
        '    ShowAlertMessage("Maximum Transaction Limit is :" + Session("MaxTranLimit").ToString())
        '    Exit Sub
        'End If
        If CashControl() = False Then
            Exit Sub
        End If
        If Ddl_CashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text

        If Len(Request.QueryString("TRANID")) > 0 Then
            Dim M_CommonBL As New CommonBL       
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
        'Dim ParameterDt As DataTable
        Dim M_CommnBL As New CommonBL
        'ParameterDt = M_CommnBL.GetLoanParameter(CmbScheme.SelectedValue)
        ' Emi_IRRParameter = M_CommnBL.Get_IRR(CmbScheme.SelectedValue)

        If CmbBranch.SelectedValue = "" Then
            Response.Redirect("~/SmartLogin.aspx")
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

        With M_LoanMaster
            .AccID = M_AccID
            .CustId = DgvCustomer.Rows(0).Cells(4).Text
            .LmHolidayPeriod = 0
            .LmInstallmentNo = Val(Txt_InstNo.Text)
            .LmApprID = 0
            .LmMainpurpose = Ddl_Purpose.SelectedValue
            .LmAgentCode = 0
            .LmJewName = 0
            .LmSubpurpose = Ddl_SubPurpose.SelectedValue
            .LmPriority = Ddl_Priority.SelectedIndex
            .LmSanctionBy = Ddl_SanctionedBy.SelectedValue
            .LmNomNos = IIf(NomDiv.Visible = False, 0, 1)
            .LmCumType = 0
            .LogID = 0
            .LmLoanDate = Txt_LoanDate.Text
            .LmDueDate = Txt_DueDate.Text
            .LmLastIntDate = Txt_LoanDate.Text
            .LmResDate = Txt_LoanDate.Text
            .LmLastUpdate = Txt_LoanDate.Text
            .LmOpIntdate = Txt_LoanDate.Text
            .LmOrnamentsReleasedDate = "01/01/1900"
            .Cashiertime = Date.Now
            .LmjewBillDate = Date.Now
            .LmLegalDate = Date.Now
            .LmNpaDate = "01/01/1900"
            .LmFirstNpaDate = "01/01/1900"
            .LmClosedDate = "01/01/1900"
            .MakingTime = Date.Now
            .CheckingTime = "01/01/1900"
            .LmLoanAmount = Val(Txt_LoanAmount.Text)
            .LmBalance = Val(Txt_IssuedAmt.Text)
            .LmPeriod = IIf(Val(Txt_Month.Text) > 0, Val(Txt_Month.Text), Val(Txt_Days.Text))
            .LmMarketRate = 0
            .LmBankRate = 0
            .LmIntRate = Val(Txt_Interest.Text)
            .LmBillAmt = 0
            .LmPenalRate = Val(Txt_PenalInterest.Text)
            .LmInstallmentAmount = Val(Txt_InstAmt.Text)
            .LmApprValue = Val(Txt_IssuedAmt.Text)
            .LmOpBal = 0
            .LmOpPayAmt = 0
            .LmOpIntrate = Val(Txt_Interest.Text)
            .AccNo = M_AccNo
            .SchCode = CmbScheme.SelectedValue
            .BrCode = CmbBranch.SelectedValue
            .LmApplSlno = 0
            .AccCode = M_AccHead
            .CheckerID = ""
            .CashierID = ""
            .BankAccNo = ""
            .LmJewBillno = ""
            .LmRegNo = Txt_GrahanNo.Text
            .LmLbrCode = CmbBranch.SelectedValue
            .LmLbrActCode = ""
            .LmIntroducedby = CmbCanvassedBy.SelectedValue
            .TranNo = ""
            .MakerID = Session("UserID")
            .LmShrBrCode = HdnMemBrCode.Value
            .LmShrSchCode = HdnMemType.Value
            .LmMemNo = HdnMemNo.Value
            .LmResNo = Txt_ReslNo.Text
            .LmSanctionNo = ""
            .LmSecurityDetails = ""
            .LmRemarks = Txt_Remarks.Text
            .LmBalType = "D"
            .LmPeriodType = IIf(Val(Txt_Month.Text) > 0, "M", "D")
            .LmHolidayPeriodType = IIf(Val(Txt_Month.Text) > 0, "M", "D")
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
            .LMDirector = "0" 'CmbDirector.SelectedValue.ToString()
            .LMReligion = "0"
            .LMCaste = "0"
            .LMGender = "0"
            .LMRegOffice = "0"
            .LMExtend = "0"
            .LMSurveyNo = "0"
            .LMDocumentNo = "0"
            .LMResurveyNo = "0"
            .LMWardCode = IIf(Txt_wrdNo.Text = "", 0, Txt_wrdNo.Text)
            .LmResDate = IIf(Txt_ReslDt.Text = "", "01/01/1990", Txt_ReslDt.Text)
            .LM_Cent = IIf(Txt_Cent.Text = "", "0", Txt_Cent.Text)
            .LM_SroNo = Txt_SroNo.Text.Trim()
            .LM_BondNo = Txt_BondNo.Text.Trim()
            .LM_BondDate = IIf(Txt_BondDate.Text = "", "01/01/1990", Txt_BondDate.Text)
            .LM_AdvEMI = Val(TxtAdvInst.Text)
            .LM_EMI_IRR = Val(TxtIRR.Text)
            .PLAmnt = Val(HdnPLEligible.Value)
        End With

        Try
            Dim AccHolderdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_AccHolderSw As New IO.StringWriter
            AccHolderdt.WriteXml(M_AccHolderSw)

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
            If Len(Request.QueryString("TRANID")) = 0 Then
                If RadioSecurity.SelectedValue.ToString() = "Surety" Then
                    If Len(DgvCustSur.Rows(0).Cells(5).Text) = 0 Or DgvCustSur.Rows(0).Cells(5).Text = "&nbsp;" Then
                        ShowAlertMessage("Enter Sureties")
                        Exit Sub
                    End If
                End If

            End If
            'If Ddl_CashTransfer.SelectedIndex = 1 And ChkBank.Checked = False Then
            '    ChkBank.Focus()
            '    ChkBank.ForeColor = Drawing.Color.Red
            '    ShowAlertMessage("Please Enter the bank Details")
            '    Exit Sub
            'End If
            Dim M_CommonBl As New CommonBL
            Dim M_Dt2 As New DataTable()
            If Ddl_CashTransfer.SelectedIndex = 0 Then

                M_Dt2 = M_CommonBl.Get_CashBalance(Session("DefaultBrCode"), Session("TransDate"))
                If M_Dt2.Rows.Count > 0 Then
                    ''- IIf(TxtTransfer.Text = "", 0, Val(TxtTransfer.Text)) 
                    If Ddl_CashTransfer.SelectedIndex = 0 And Val(Txt_LoanAmount.Text) > Val(M_Dt2.Rows(0).Item("CashBalance")) Then
                        Txt_LoanAmount.Text = 0
                        Call Txt_LoanAmount_TextChanged(Me.Txt_LoanAmount, System.EventArgs.Empty)
                        ShowAlertMessage("Please Check Your Cash Balance, Balance : " + M_Dt2.Rows(0).Item("CashBalance").ToString())
                        Exit Sub
                    End If
                End If
            End If

            If ChkBank.Checked = True And Ddl_CashTransfer.SelectedIndex = 1 Then
                Dim M_Dt1 As New DataTable
                Dim McommnBl As New CommonBL
                M_Dt1 = McommnBl.Get_BankBalance(drpbankAccno.SelectedItem.Text, Session("TransDate"))
                If Val(TxtBankAmnt.Text) > Val(M_Dt1.Rows(0).Item("BankBalance").ToString()) Then
                    ShowAlertMessage("Please Check The Selected Bank Balance, Available Balance = '" & M_Dt1.Rows(0).Item("BankBalance").ToString() & "'")
                    Exit Sub
                End If
            End If

            Dim M_LoanMasterBL As New LoanMasterBL
            Dim M_Dt As DataTable
            Session("M_EntryMode") = "NEW"
            M_Dt = M_LoanMasterBL.Save_Data(M_LoanMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), "", M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), "", IIf(Session("LoanAppNo") = Nothing, "", Session("LoanAppNo")), M_BatchNo, HdnTrfrAccID.Value, M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"))
            Dim M_AcID As String = M_Dt.Rows(0)("AccID")
            SaveOtherSuerties(M_AcID)
            ShowAlertMessage(" LOAN NO : " & M_Dt.Rows(0)("Acc_No").ToString)
            Set_Grid()

            'transfer mode saving-------------------------------------------
            If Ddl_CashTransfer.SelectedIndex = 1 And ChkBank.Checked = True Then
                Dim M_LoanMasterBL1 As New LoanMasterBL
                Dim dtTransferDetails As New DataTable
                dtTransferDetails = M_LoanMasterBL1.SaveTransferDetails_Data(M_Dt.Rows(0)("Tran_No").ToString, drpBank.SelectedItem.ToString(), drpbankAccno.SelectedItem.Text.ToString(), TxtCheque.Text.ToString(), txtChequeDate.Text.ToString(), drpTranferType.SelectedValue.ToString(), txtIFSC.Text.ToString(), txtaccno.Text.ToString(), txtCommission.Text.ToString(), Session("TransDate"), Val(TxtBankAmnt.Text), IIf(DdlTranType.SelectedIndex = 1, "C", "D"))
            End If
            'transfer mode saving------------------------------------------

            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=GEN.LOAN" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=LOAN" & "&DRCR=" & "D")

            M_LoanMasterBL = Nothing
            'Response.Redirect("~/AccOpen.aspx")

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try


    End Sub

    Protected Sub Txt_LoanAmount_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_LoanAmount.TextChanged
        Calc_InterestRate()
        If Val(Txt_LoanAmount.Text) > 100000 And Ddl_CashTransfer.SelectedItem.Text = "CASH" Then
            ShowAlertMessage("Change CASH mode to TRANSFER mode.")
            Ddl_CashTransfer.Focus()
            Exit Sub
            'CmbCashTransfer.SelectedIndex = 1
            'CmbCashTransfer_SelectedIndexChanged(Me.CmbCashTransfer, EventArgs.Empty)
        End If

        If HdnGLSecScheme.Value = "Y" Then
            If Val(Txt_LoanAmount.Text) > Val(HdnPLEligible.Value) Then
                ShowAlertMessage("Loan Amount Grater Than Eligible Amount")
                Txt_LoanAmount.Text = 0
                Set_TransactionGrid()
                Exit Sub
            End If
        End If
        Dim M_comBl As New CommonBL
        Dim M_dt As New DataTable
        M_dt = M_comBl.Loan_IssuedAmt(CmbScheme.SelectedValue, Session("DefaultBrCode"))
        If (M_dt.Rows.Count > 0) Then
            Txt_IssuedAmt.Enabled = False
            Txt_IssuedAmt.Text = (Txt_LoanAmount.Text * M_dt.Rows.Item(0)("Lp_BankRatePerc") / 100)
            Txt_BalanceAmount.Text = Val(Txt_IssuedAmt.Text)
            Set_TransactionGrid()
            Txt_InstNo_TextChanged(Me.Txt_InstNo, System.EventArgs.Empty)
        Else
            Txt_BalanceAmount.Text = Val(Txt_LoanAmount.Text)
            Txt_IssuedAmt.Text = Val(Txt_LoanAmount.Text)
            Set_TransactionGrid()
            Txt_InstNo_TextChanged(Me.Txt_InstNo, System.EventArgs.Empty)
        End If
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

    Protected Sub Txt_InstNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_InstNo.TextChanged
        Try
            Call Get_InstalmentDetails()
            Call Calc_IRR()
        Catch ex As Exception

        End Try
       
    End Sub

    Protected Sub ImageButton5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        If Len(Request.QueryString("TRANID")) > 0 Then
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
        Else
            Response.Redirect("~/Main.aspx")
        End If
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

    Protected Sub TxtTrfrAcNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTrfrAcNo.TextChanged
        'Try
        '    Dim Dt_AcDtlsN As DataTable
        '    Dim Dt_AcDtls As DataTable
        '    Dim M_CommonBL As New CommonBL
        '    TxtTrfrAcNo.Text = Right("000" + CmbTrfrBr.SelectedValue, 3) + Right("000" + CmbTrfrSch.SelectedValue, 3) + Right("0000000" + TxtTrfrAcNo.Text, 7)
        '    Dim M_AccNo As String = TxtTrfrAcNo.Text

        '    Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbTrfrBr.SelectedValue, CmbTrfrSch.SelectedValue, M_AccNo, Session("TransDate"))
        '    If Dt_AcDtls.Rows.Count = 0 Then
        '        HdnTrfrAccID.Value = "0"
        '        ShowAlertMessage("Invalid Account Number")
        '        TxtTrfrAcNo.Text = ""
        '        TxtTrfrAcNo.Focus()
        '        M_CommonBL = Nothing
        '        Exit Sub
        '    End If

        '    HdnTrfrAccID.Value = "0"
        '    Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbTrfrBr.SelectedValue, CmbTrfrSch.SelectedValue, M_AccNo, Session("TransDate"))
        '    If Dt_AcDtlsN.Rows.Count > 0 Then
        '        TxtTrfrAcHoldenName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
        '        TxtTrfrAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
        '        Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
        '        TxtTrfrBalAmt.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")
        '        HdnTrfrAccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
        '        CmbTrfrSch.SelectedValue = Dt_AcDtls.Rows(0)("Br_Code").ToString()
        '        CmbTrfrBr.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
        '    End If

        '    M_CommonBL = Nothing

        'Catch ex As Exception
        '    ShowAlertMessage(ex.Message)
        'End Try
    End Sub

    Protected Sub ImgBatch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBatch.Click
        Try

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Ddl_SanctionedBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Ddl_SanctionedBy.SelectedIndexChanged

    End Sub

    Protected Sub ImgBtnAddLand_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAddLand.Click
        Dim dt As DataTable = DirectCast(Session("LandTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("Tax") = IIf(ChkTax.Checked, "Y", "N")
        Row("BuildingNo") = TxtBuildingNo.Text.Trim
        Row("DeedNo") = TxtDeedNo.Text
        Row("DeedDate") = Format(CDate(IIf(TxtDeedDate.Text.Length = 0, "01/01/1900", TxtDeedDate.Text)), "MM/dd/yyyy")
        Row("WardNo") = TxtWardNo.Text.Trim()
        Row("Extend") = TxtExtend.Text
        Row("District") = TxtDistrict.Text
        Row("Tehsil") = TxtTehsil.Text
        Row("Panchayath") = TxtPanchMunci.Text
        Row("Village") = TxtVillage.Text
        Row("SubRegOffice") = TxtSupRegOff.Text
        Row("MarketValue") = TxtMarketValue.Text
        Row("TaxNo") = TxtTaxNo.Text
        Row("TaxDate") = Format(CDate(IIf(TxtTaxDate.Text.Length = 0, "01/01/1900", TxtTaxDate.Text)), "MM/dd/yyyy")
        Row("SurveyNo") = TxtSurveyNo.Text.Trim()
        Row("PossessionCrtNo") = TxtPossessionCrtNo.Text.Trim()
        Row("PossessionCrtDate") = Format(CDate(IIf(txtPossessionCrtDate.Text.Length = 0, "01/01/1900", txtPossessionCrtDate.Text)), "MM/dd/yyyy")
        Row("EasyNo") = txtEasyNo.Text.Trim()
        Row("EasyDate") = Format(CDate(IIf(TxtEasyDate.Text.Length = 0, "01/01/1900", TxtEasyDate.Text)), "MM/dd/yyyy")
        Row("LoactionMapNo") = TxtLoacationMapNo.Text.Trim()
        Row("LocationMapdate") = Format(CDate(IIf(txtLoacationMapDate.Text.Length = 0, "01/01/1900", txtLoacationMapDate.Text)), "MM/dd/yyyy")
        Row("RecepitNo") = TxtRecepitNo.Text.Trim()
        Row("RecepitDate") = Format(CDate(IIf(TxtRecepitDate.Text.Length = 0, "01/01/1900", TxtRecepitDate.Text)), "MM/dd/yyyy")
        Row("GahanNo") = TxtGahanNo.Text.Trim()
        Row("GahanDate") = Format(CDate(IIf(TxtGahanDate.Text.Length = 0, "01/01/1900", TxtGahanDate.Text)), "MM/dd/yyyy")
        Row("PofAtNo") = TxtPofAtNo.Text.Trim()
        Row("PofAtNoDate") = Format(CDate(IIf(TxtPofatNoDate.Text.Length = 0, "01/01/1900", TxtPofatNoDate.Text)), "MM/dd/yyyy")
        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "DeedDate=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try
        Session("LandTbl") = dt
        DgvLand.DataSource = dt
        DgvLand.DataBind()
        TxtDeedNo.Text = ""
        TxtDeedDate.Text = ""
        TxtBuildingNo.Text = ""
        TxtExtend.Text = ""
        TxtDistrict.Text = ""
        TxtTehsil.Text = ""
        TxtWardNo.Text = ""
        TxtVillage.Text = ""
        TxtPossessionCrtNo.Text = ""
        txtPossessionCrtDate.Text = ""
        TxtMarketValue.Text = ""
        TxtTaxNo.Text = ""
        TxtTaxDate.Text = ""
        Txt_GrahanNo.Text = ""
        TxtGahanDate.Text = ""
        TxtEasyDate.Text = ""
        txtEasyNo.Text = ""
        TxtSurveyNo.Text = ""
        txtLoacationMapDate.Text = ""
        TxtLoacationMapNo.Text = ""
        TxtRecepitDate.Text = ""
        TxtRecepitNo.Text = ""
        TxtPofAtNo.Text = ""
        TxtPofatNoDate.Text = ""
    End Sub
    Private Sub drpBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpBank.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
    End Sub
    Protected Sub ImgBtnCropAdd_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCropAdd.Click
        Dim dt As DataTable = DirectCast(Session("CropTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("CropName") = TxtCropName.Text
        Row("SurveyNo") = txtCropSurveyNo.Text.Trim()
        Row("Extend") = TxtCropExtend.Text.Trim()
        Row("TaxNo") = TxtCropTaxNo.Text.Trim()
        Row("TaxDate") = Format(CDate(txtCropTaxDate.Text.Trim()), "MM/dd/yyyy")
        Row("Village") = txtCropVillage.Text.Trim()
        Row("PcNo") = TxtCropPcNo.Text.Trim.Trim()
        Row("PcDate") = Format(CDate(TxtCropPcDate.Text.Trim()), "MM/dd/yyyy")
        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "CropName=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        DgvCrop.DataSource = dt
        DgvCrop.DataBind()
        Session("CropTbl") = dt
        TxtCropName.Text = ""
        TxtCropExtend.Text = ""
        txtCropSurveyNo.Text = ""
        TxtCropPcDate.Text = ""
        TxtCropPcNo.Text = ""
        txtCropTaxDate.Text = ""
        TxtCropTaxNo.Text = ""
        txtCropVillage.Text = ""
    End Sub

    Protected Sub imgAddSalary_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddSalary.Click
        Dim dt As DataTable = DirectCast(Session("SalaryTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("CertiNo") = txtCertiNo.Text.Trim()
        Row("EmployeeName") = txtEmployeeName.Text.Trim()
        Row("FirmName") = txtFirmName.Text.Trim()
        Row("Branch") = txtBranch.Text.Trim()
        Row("Designation") = txtDesignation.Text.Trim()
        Row("JoinDate") = Format(CDate(txtJoinDate.Text.Trim()), "MM/dd/yyyy")
        Row("BasicSal") = txt_basicSalary.Text.Trim()
        Row("GrossPay") = txtGrossPay.Text.Trim()
        Row("NetPay") = txtNetPay.Text.Trim()
        Row("ServicePeriod") = txtSerPeriod.Text.Trim()
        Row("RetirementDate") = Format(CDate(txtRetirement.Text.Trim()), "MM/dd/yyyy")
        Row("IssuingAuthority") = txtissuingAuthority.Text.Trim()
        Row("IssuingAuthorityAddress") = txtissauthAddress.Text.Trim()
        Row("IssueDate") = Format(CDate(txtissueDate.Text.Trim()), "MM/dd/yyyy")
        Row("Remarks") = txtsalaryRemarks.Text.Trim()

        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "CertiNo=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        Grid_Salary.DataSource = dt
        Grid_Salary.DataBind()
        Session("SalaryTbl") = dt
        txtCertiNo.Text = ""
        txtEmployeeName.Text = ""
        txtFirmName.Text = ""
        txtBranch.Text = ""
        txtDesignation.Text = ""
        txtJoinDate.Text = ""
        txt_basicSalary.Text = ""
        txtNetPay.Text = ""
        txtGrossPay.Text = ""
        txtRetirement.Text = ""
        txtSerPeriod.Text = ""
        txtissauthAddress.Text = ""
        txtissueDate.Text = ""
        txtissuingAuthority.Text = ""
        txtsalaryRemarks.Text = ""
    End Sub

    Protected Sub ImgBtnOrnament_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnOrnament.Click
        Dim dt As DataTable = DirectCast(Session("OrnamentTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("OrnamentType") = CmbOrnamentType.SelectedValue
        Row("OrnamentDtls") = CmbOrnamentType.SelectedItem.Text
        Row("GoldCount") = Val(TxtCount.Text)
        Row("Remarks") = TxtRemarks.Text
        Row("NetWeight") = Val(TxtNetWeight.Text)
        Row("GrossWeight") = Val(TxtGrossWeight.Text)
        Try
            dt.Rows.Add(Row)

        Catch ex As Exception
            ShowAlertMessage("Ornament Already Exist")
        End Try
        Try
            Dim Expression As String = "OrnamentType=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        DgvOrnament.DataSource = dt
        DgvOrnament.DataBind()
        Session("OrnamentTbl") = dt
        CmbOrnamentType.SelectedIndex = 0
        TxtCount.Text = ""
        TxtRemarks.Text = ""
        TxtNetWeight.Text = ""
        TxtGrossWeight.Text = ""

    End Sub

    Protected Sub TxtInsuranceDate0_TextChanged(sender As Object, e As EventArgs) Handles TxtInsuranceDate0.TextChanged

    End Sub

    Protected Sub TxtInsuranceValue_TextChanged(sender As Object, e As EventArgs) Handles TxtInsuranceValue.TextChanged

    End Sub

    Protected Sub ImgBtnInsurance_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnInsurance.Click
        Dim dt As DataTable = DirectCast(Session("InsuranceTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("InsuranceCompany") = TxtInsuranceCompany.Text
        Row("PolicyName") = TxtPolicyName.Text
        Row("PolicyNo") = TxtPolicyNo.Text
        Row("SumAssured") = TxtSumAssured.Text
        Row("MaturityDate") = Format(CDate(TxtDateOfMaturity.Text), "MM/dd/yyyy") '@@@
        Row("SurrenderValue") = Val(TxtSurrenderValue.Text)
        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "InsuranceCompany=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        DgvInsurance.DataSource = dt
        DgvInsurance.DataBind()
        Session("InsuranceTbl") = dt

        TxtInsuranceCompany.Text = ""
        TxtPolicyName.Text = ""
        TxtPolicyNo.Text = ""
        TxtSumAssured.Text = ""
        TxtDateOfMaturity.Text = ""
        TxtSurrenderValue.Text = ""
    End Sub

    Protected Sub ImgBtnBond_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnBond.Click
        Dim dt As DataTable = DirectCast(Session("BondTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("IssuedPo") = TxtIssuedPostOffice.Text
        Row("SerialNo") = TxtSerialNo.Text
        Row("PhaseValue") = Val(TxtPhaseValue.Text)
        Row("DueDate") = Format(CDate(TxtDueDate0.Text), "MM/dd/yyyy")
        Row("TotPhaseVal") = Val(TxtTotalPhaseValue.Text)
        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "IssuedPo=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        DgvBond.DataSource = dt
        DgvBond.DataBind()
        Session("BondTbl") = dt
        

        TxtIssuedPostOffice.Text = ""
        TxtSerialNo.Text = ""
        TxtPhaseValue.Text = ""
        TxtDueDate0.Text = ""
        TxtTotalPhaseValue.Text = ""


    End Sub

    Protected Sub ImgBtnVehicle_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnVehicle.Click
        Dim dt As DataTable = DirectCast(Session("VehicleTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("VType") = TxtType.Text
        Row("RegionNo") = Val(TxtRegionNo.Text)
        Row("ChasisNo") = TxtChassisNo.Text
        Row("EngineNo") = TxtEngineNo.Text
        If RadNew.Checked = True Then
            Row("OldNew") = "N"
        ElseIf RadOld.Checked = True Then
            Row("OldNew") = "O"
        End If

        Row("Brand") = TxtBrand.Text
        Row("Model") = TxtModel.Text
        If ChkKeySubmited.Checked = True Then
            Row("KeySub") = "Y"
        Else
            Row("KeySub") = "N"
        End If

        Row("KeyNo") = TxtKeyNo.Text
        Row("PermitScope") = TxtPermitScope.Text
        Row("RtOffice") = TxtRTOffice.Text
        If ChkLienMarked.Checked = True Then
            Row("LienMarked") = "Y"
        Else
            Row("LienMarked") = "N"
        End If

        Row("LienNo") = TxtLienNo.Text
        Row("LienDate") = Format(CDate(txtLienDate.Text), "MM/dd/yyyy")
        Row("LienReleased") = Format(CDate(TxtLienReleased.Text), "MM/dd/yyyy")
        Row("TextDetails") = TxtTextDetails.Text
        Row("TaxDate") = Format(CDate(TxtTextDate.Text), "MM/dd/yyyy")
        Row("InsuranceDate") = Format(CDate(TxtInsuranceDate0.Text), "MM/dd/yyyy")
        Row("InsuranceDetails") = TxtInsuranceDetails.Text
        Row("InsuranceValue") = Val(TxtInsuranceValue.Text)
        Row("OtherDetails") = TxtOtherDetails0.Text
        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "VType=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        DgvVehicle.DataSource = dt
        DgvVehicle.DataBind()
        Session("VehicleTbl") = dt

        TxtType.Text = ""
        TxtRegionNo.Text = ""
        TxtChassisNo.Text = ""
        TxtEngineNo.Text = ""
        RadNew.Checked = False
        RadOld.Checked = False
        TxtBrand.Text = ""
        TxtModel.Text = ""
        ChkKeySubmited.Checked = False
        TxtKeyNo.Text = ""
        TxtPermitScope.Text = ""
        TxtRTOffice.Text = ""
        ChkLienMarked.Checked = False
        TxtLienNo.Text = ""
        txtLienDate.Text = ""
        TxtLienReleased.Text = ""
        TxtTextDetails.Text = ""
        TxtInsuranceDate0.Text = ""
        TxtInsuranceValue.Text = ""
        TxtOtherDetails0.Text = ""
    End Sub

    Protected Sub ImgBtnTermDeposit_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnTermDeposit.Click
        Dim dt As DataTable = DirectCast(Session("DepositTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("OtherBankAC") = ChkOtherBankAc.Text
        If ChkOtherBankAc.Checked = True Then
            Row("Branch1") = TxtSecTDBrName.Text
            Row("BranchDtls1") = ""
            Row("Scheme") = TxtAcType.Text
            Row("schemeDtls") = ""
        Else
            Row("Branch1") = CmbBranch1.SelectedValue
            Row("BranchDtls1") = CmbBranch1.SelectedItem.Text
            Row("Scheme") = DdlScheme.SelectedValue
            Row("schemeDtls") = ""
        End If
        Row("AcNo") = TxtAcNo.Text
        Row("CertificateNo") = TxtCertificateNo.Text
        Row("DepositAmt") = TxtDepositeAmt.Text
        Row("AmtOutstanding") = TxtAmtOutstanding.Text
        Row("DueDate") = Format(CDate(TxtDueDate5.Text), "MM/dd/yyyy")
        Row("BankName") = TxtSecBankName.Text
        Row("Branch") = CmbBranch.SelectedValue
        Row("AcType") = TxtAcType.Text
        Row("CustomerNo") = TxtCustomerNo.Text
        Row("Name") = ""
        Row("Addr") = TxtTDAddress.Text
        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "AcNo=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        DgvDeposit.DataSource = dt
        DgvDeposit.DataBind()
        Session("DepositTbl") = dt

        ChkOtherBankAc.Checked = False
        CmbScheme.SelectedIndex = 0
        TxtAcNo.Text = ""
        TxtCertificateNo.Text = ""
        TxtDepositeAmt.Text = ""
        TxtAmtOutstanding.Text = ""
        TxtDueDate5.Text = ""
        TxtSecBankName.Text = ""
        TxtSecTDBrName.Text = ""
        TxtAcType.Text = ""
        TxtCustomerNo.Text = ""
        TxtSecTD_CustName.Text = ""
        TxtTDAddress.Text = ""
    End Sub

    Protected Sub RadioSecurity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioSecurity.SelectedIndexChanged
        Dim Secur As String = RadioSecurity.SelectedValue.ToString()
        If Secur = "Surety" Then
            DivSur.Visible = True
            DivOtherSuerties.Visible = False
            AddMem.Visible = False
            Exit Sub
        End If
        If Secur = "Security" Then
            DivSur.Visible = False
            DivOtherSuerties.Visible = True
            AddMem.Visible = False
            Exit Sub
        End If
        If Secur = "ADL Member" Then
            DivSur.Visible = False
            DivOtherSuerties.Visible = False
            AddMem.Visible = True
            Exit Sub
        End If
    End Sub

    Protected Sub RadiOtherSuerties_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadiOtherSuerties.SelectedIndexChanged
        Dim Othersecurity As String = RadiOtherSuerties.SelectedValue.ToString()
        If Othersecurity = "Land And Building" Then
            DivBond.Visible = False
            DivCrops.Visible = False
            DivGold.Visible = False
            DivInsurance.Visible = False
            DivLandBuilding.Visible = True
            DivSalary.Visible = False
            DivTermDeposit.Visible = False
            DivVehicle.Visible = False
            Exit Sub
        End If
        If Othersecurity = "Crops" Then
            DivBond.Visible = False
            DivCrops.Visible = True
            DivGold.Visible = False
            DivInsurance.Visible = False
            DivLandBuilding.Visible = False
            DivSalary.Visible = False
            DivTermDeposit.Visible = False
            DivVehicle.Visible = False
            Exit Sub
        End If
        If Othersecurity = "Gold Ornaments" Then
            DivBond.Visible = False
            DivCrops.Visible = False
            DivGold.Visible = True
            DivInsurance.Visible = False
            DivLandBuilding.Visible = False
            DivSalary.Visible = False
            DivTermDeposit.Visible = False
            DivVehicle.Visible = False
            Exit Sub
        End If
        If Othersecurity = "Insurance" Then
            DivBond.Visible = False
            DivCrops.Visible = False
            DivGold.Visible = False
            DivInsurance.Visible = False
            DivLandBuilding.Visible = True
            DivSalary.Visible = False
            DivTermDeposit.Visible = False
            DivVehicle.Visible = False
            Exit Sub
        End If
        If Othersecurity = "Other Bonds" Then
            DivBond.Visible = True
            DivCrops.Visible = False
            DivGold.Visible = False
            DivInsurance.Visible = False
            DivLandBuilding.Visible = False
            DivSalary.Visible = False
            DivTermDeposit.Visible = False
            DivVehicle.Visible = False
            Exit Sub
        End If
        If Othersecurity = "Vehicle" Then
            DivBond.Visible = False
            DivCrops.Visible = False
            DivGold.Visible = False
            DivInsurance.Visible = False
            DivLandBuilding.Visible = False
            DivSalary.Visible = False
            DivTermDeposit.Visible = False
            DivVehicle.Visible = True
            Exit Sub
        End If
        If Othersecurity = "Term Deposit" Then
            DivBond.Visible = False
            DivCrops.Visible = False
            DivGold.Visible = False
            DivInsurance.Visible = False
            DivLandBuilding.Visible = False
            DivSalary.Visible = False
            DivTermDeposit.Visible = True
            DivVehicle.Visible = False
            Exit Sub
        End If
        If Othersecurity = "Salary Slip" Then
            DivBond.Visible = False
            DivCrops.Visible = False
            DivGold.Visible = False
            DivInsurance.Visible = False
            DivLandBuilding.Visible = False
            DivSalary.Visible = True
            DivTermDeposit.Visible = False
            DivVehicle.Visible = False
            Exit Sub
        End If
       
    End Sub

    Protected Sub ChkTax_CheckedChanged(sender As Object, e As EventArgs) Handles ChkTax.CheckedChanged
        If ChkTax.Checked = True Then
            TxtTaxNo.Enabled = True
            TxtTaxDate.Enabled = True
        Else
            TxtTaxNo.Enabled = False
            TxtTaxDate.Enabled = False
        End If
    End Sub

    Protected Sub ChkBuilding_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBuilding.CheckedChanged
        If ChkBuilding.Checked = True Then
            TxtBuildingNo.Enabled = True
        Else
            TxtBuildingNo.Enabled = False
        End If
    End Sub

    Protected Sub TxtWardNo_TextChanged(sender As Object, e As EventArgs) Handles TxtWardNo.TextChanged

    End Sub


    Private Sub SaveOtherSuerties(ByVal Acid As String)

        Try

            Dim M_LoanSecurityBO As New SMARTCORE_BO.LoanAppFormBO
            With M_LoanSecurityBO
                .BrCode = CmbBranch.SelectedValue
                .AccId = Acid
                .SchCode = CmbScheme.SelectedValue
                .LmApplNo = IIf(Session("LoanAppNo") = Nothing, "", Session("LoanAppNo"))
            End With
            Dim LandTbldt As DataTable = DirectCast(Session("LandTbl"), DataTable)
            Dim M_Landsw As New IO.StringWriter
            LandTbldt.WriteXml(M_Landsw)
            Session("LandTbl") = Nothing
            Dim CropTbldt As DataTable = DirectCast(Session("CropTbl"), DataTable)
            Dim M_Cropsw As New IO.StringWriter
            CropTbldt.WriteXml(M_Cropsw)
            Session("CropTbl") = Nothing
            Dim OrnamentTbldt As DataTable = DirectCast(Session("OrnamentTbl"), DataTable)
            Dim M_Ornamentsw As New IO.StringWriter
            OrnamentTbldt.WriteXml(M_Ornamentsw)
            Session("OrnamentTbl") = Nothing
            Dim InsuranceTbldt As DataTable = DirectCast(Session("InsuranceTbl"), DataTable)
            Dim M_Insurancesw As New IO.StringWriter
            InsuranceTbldt.WriteXml(M_Insurancesw)
            Session("InsuranceTbl") = Nothing
            Dim BondTbldt As DataTable = DirectCast(Session("BondTbl"), DataTable)
            Dim M_Bondsw As New IO.StringWriter
            BondTbldt.WriteXml(M_Bondsw)
            Session("BondTbl") = Nothing
            Dim DepositTbldt As DataTable = DirectCast(Session("DepositTbl"), DataTable)
            Dim M_Depositsw As New IO.StringWriter
            DepositTbldt.WriteXml(M_Depositsw)
            Session("DepositTbl") = Nothing
            Dim VehicleTbldt As DataTable = DirectCast(Session("VehicleTbl"), DataTable)
            Dim M_Vehiclesw As New IO.StringWriter
            VehicleTbldt.WriteXml(M_Vehiclesw)
            Session("VehicleTbl") = Nothing
            Dim SalaryTblDt As DataTable = DirectCast(Session("SalaryTbl"), DataTable)
            Dim M_SalrSW As New IO.StringWriter
            SalaryTblDt.WriteXml(M_SalrSW)
            Session("SalaryTbl") = Nothing
            Dim Suertiesdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_Suerties As New IO.StringWriter
            Suertiesdt.WriteXml(M_Suerties)
            Session("AccHolderTbl") = Nothing
            Dim dt As DataTable = DirectCast(Session("AdditionalMember"), DataTable)
            Dim M_AdditionalMember As New IO.StringWriter
            dt.WriteXml(M_AdditionalMember)
            Session("AdditionalMember") = Nothing
            Dim M_LoanAppBL As New LoanApplicationBL

            'M_LoanAppBL.Save_LADetails(M_LoanSecurityBO, M_Landsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Cropsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Ornamentsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Insurancesw.ToString.Replace("DocumentElement", "NewDataSet"), M_Bondsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Vehiclesw.ToString.Replace("DocumentElement", "NewDataSet"), M_Depositsw.ToString.Replace("DocumentElement", "NewDataSet"))

            M_LoanAppBL.Save_LADetails(M_LoanSecurityBO, IIf(RadiOtherSuerties.SelectedValue = "Land And Building", M_Landsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadiOtherSuerties.SelectedValue = "Crops", M_Cropsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadiOtherSuerties.SelectedValue = "Gold Ornaments", M_Ornamentsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadiOtherSuerties.SelectedValue = "Insurance", M_Insurancesw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadiOtherSuerties.SelectedValue = "Other Bonds", M_Bondsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadiOtherSuerties.SelectedValue = "Vehicle", M_Vehiclesw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadiOtherSuerties.SelectedValue = "Term Deposit", M_Depositsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadiOtherSuerties.SelectedValue = "Salary Slip", M_SalrSW.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadioSecurity.SelectedValue = "Surety", M_Suerties.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(RadioSecurity.SelectedValue = "ADL Member", M_AdditionalMember.ToString.Replace("DocumentElement", "NewDataSet"), ""))


        Catch ex As Exception
            ShowAlertMessage(ex.ToString)
        End Try


    End Sub

    Protected Sub CmbNomReq_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNomReq.SelectedIndexChanged
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

    Protected Sub Btnverfiy_Click(sender As Object, e As EventArgs) Handles Btnverfiy.Click
        Try
            Session("MobOTP") = ""
            Dim M_CustMasterBL As New CustMasterBL
            Dim M_Datatable As New DataTable
            Dim MobileNo As String = Txt_MobileNo.Text
            M_Datatable = M_CustMasterBL.OTPGenrate(MobileNo)
            Session("ODPTBL") = M_Datatable
            Session("MobOTP") = M_Datatable.Rows(0)("OTP")
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');", True)

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub But_verify_Click(sender As Object, e As EventArgs) Handles But_verify.Click
    '    Try
    '        Dim M_CustMasterBL As New CustMasterBL
    '        Dim OTPNO As String = txt_verifycode.Text
    '        Dim M_Datatable As New DataTable
    '        'M_Datatable = Session("ODPTBL")
    '        Dim MobileNo As String = Txt_MobileNo.Text
    '        M_Datatable = M_CustMasterBL.VerifyOTP(MobileNo)
    '        Dim OTPCODE As String = M_Datatable.Rows(1)("OTP")
    '        Dim OtpDate As Date = Convert.ToDateTime(M_Datatable.Rows(2)("GenerateTime"))
    '        OtpDate = OtpDate.AddMinutes(20)
    '        Dim Nowdate As Date = Now.ToString()

    '        If OtpDate >= Nowdate Then
    '            If OTPNO = OTPCODE Then
    '                Hid_ODPStatus.Value = "Y"
    '                Session("ODPTBL") = Nothing
    '                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('cover');hide('VerifyMob');hide('div_Verify');hide('imgverifyError');show('imgVerify');hide('lbl_sessionexp');hide('lbl_errMsg');", True)
    '            Else
    '                ' ErrorMessage("Enter Correct OTP Code")
    '                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');hide('lbl_sessionexp');show('lbl_errMsg');", True)
    '            End If
    '        Else
    '            Session("ODPTBL") = Nothing
    '            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');hide('div_Verify');show('imgverifyError');hide('imgVerify');show('lbl_sessionexp');hide('lbl_errMsg');", True)
    '        End If

    '        'Hid_ODPStatus.Value = "Y"
    '        'Session("ODPTBL") = Nothing
    '        'ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('cover');hide('VerifyMob');hide('div_Verify');hide('imgverifyError');show('imgVerify');hide('lbl_sessionexp');hide('lbl_errMsg');", True)


    '    Catch ex As Exception

    '    End Try
    'End Sub


    Protected Sub But_verify_Click(sender As Object, e As EventArgs) Handles But_verify.Click
        Try
            Dim M_CustMasterBL As CustMasterBL
            Dim OTPNO As String = txt_verifycode.Text
            Dim M_Datatable As New DataTable
            'M_Datatable = Session("ODPTBL")
            Dim MobileNo As String = Txt_MobileNo.Text
            M_Datatable = M_CustMasterBL.VerifyOTP(MobileNo)
            Dim OTPCODE As String = M_Datatable.Rows(1)("OTP")
            Dim OtpDate As Date = Convert.ToDateTime(M_Datatable.Rows(2)("GenerateTime"))
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





    Protected Sub Txt_Interest_TextChanged(sender As Object, e As EventArgs) Handles Txt_Interest.TextChanged

    End Sub


    Protected Sub btnEMI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEMI.Click
        Dim CommBL As New CommonBL
        Dim Prd As Integer

        If Val(Txt_Month.Text) > 0 Then
            Prd = Txt_Month.Text
        ElseIf Val(Txt_Days.Text) > 0 Then
            Prd = Txt_Days.Text
        End If

        Dim InstDt As Date = Convert.ToDateTime(Session("TransDate"))
        Dim LoanAmnt As Double = Val(Txt_LoanAmount.Text)
        Dim LoanDate As Date = Convert.ToDateTime(Txt_LoanDate.Text)
        Dim SchCode As String = CmbScheme.SelectedValue

        Dim Status As String
        Dim NextDate As Date
        Dim Dt As DataTable
        Dim M_Sql As String = ""
        Dim M_Dt As DataTable = New DataTable()
        Dim M_DBConn As New Connection()

        M_Sql = "select Isnull(Emi_Slab,'N') Status from Loan_Parameter_P_Tbl where Lnp_EMIType='Y' and Sch_Code='" & SchCode & "'"
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
        Status = M_Dt.Rows(0)("Status").ToString

        If Status = "Y" Then

            M_Sql = "select dbo.Get_NextEmiDate_Fn('" & SchCode & "','" & Format(LoanDate, "MM/dd/yyyy") & "') NextDate"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            NextDate = M_Dt.Rows(0)("NextDate")

            Dt = CommBL.Get_EMITable(LoanAmnt - (Val(TxtAdvInst.Text) * Val(Txt_InstAmt.Text)), Val(TxtIRR.Text), (Prd - Val(TxtAdvInst.Text)), NextDate, CmbScheme.SelectedValue, 0, HdnPrdType.Value, Val(TxtAdvInst.Text), Session("TransDate"))
            CommBL = Nothing
        Else

            Dt = CommBL.Get_EMITable(LoanAmnt - (Val(TxtAdvInst.Text) * Val(Txt_InstAmt.Text)), Val(TxtIRR.Text), (Prd - Val(TxtAdvInst.Text)), InstDt, CmbScheme.SelectedValue, 0, HdnPrdType.Value, Val(TxtAdvInst.Text), Session("TransDate"))
        End If
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
                Ds = M_CommonBL.Set_TransactionProcGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Txt_LoanDate.Text, IIf(Ddl_CashTransfer.SelectedIndex = 0, "C", "T"), "D", "M", Val(Txt_IssuedAmt.Text), HdnCustID.Value, Val(Txt_LoanAmount.Text), Txt_DueDate.Text)
                Dim Dt As DataTable = Ds.Tables(0)
                DgvTransaction.DataSource = Dt
                DgvTransaction.DataBind()
                Session("TransTbl") = Dt
            Else
                Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Txt_LoanDate.Text, IIf(Ddl_CashTransfer.SelectedIndex = 0, "C", "T"), "D", "M", Val(Txt_IssuedAmt.Text), HdnCustID.Value, Val(Txt_LoanAmount.Text), Txt_DueDate.Text)
                Dim Dt As DataTable = Ds.Tables(0)
                DgvTransaction.DataSource = Dt
                DgvTransaction.DataBind()
                Session("TransTbl") = Dt
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function Validate_Control() As Boolean

        If Session("MobOTP") <> txt_verifycode.Text Then
            txt_verifycode.Focus()
            ShowAlertMessage("INVALID OTP")
            Return False
        End If

        If Val(Ddl_SubPurpose.SelectedItem.Text.Length) <= 0 Then
            Ddl_SubPurpose.Focus()
            ShowAlertMessage("INVALID SUB PURPOSE. CONTACT ADMIN")
            Return False
        End If

        If Val(Txt_IssuedAmt.Text) <= 0 Then
            Txt_IssuedAmt.Focus()
            ShowAlertMessage("INVALID ISSUED AMT ENTERED")
            Return False
        End If

        If Val(Txt_LoanAmount.Text) <= 0 Then
            Txt_LoanAmount.Focus()
            ShowAlertMessage("INVALID LOAN AMT ENTERED")
            Return False
        End If

        If CmbCanvassedBy.SelectedItem.Text = "<-- Select -->" Then
            CmbCanvassedBy.Focus()
            ShowAlertMessage("KINDLY PLEASE SELECT CANVASSER")
            Return False
        End If

        Return True
    End Function
    Protected Sub DgvOrnament_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles DgvOrnament.RowDeleting
        Try
            Dim selectedRow As String = e.RowIndex.ToString()
            DeleteOrnaments(selectedRow)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub DeleteOrnaments(ByVal Rowindex As String)
        Try
            Dim dt As DataTable = DirectCast(Session("OrnamentTbl"), DataTable)
            dt.Rows(Rowindex).Delete()
            DgvOrnament.DataSource = dt
            DgvOrnament.DataBind()
            Session("OrnamentTbl") = dt
            If dt.Rows.Count <= 0 Then
                DgvOrnament.DataSource = Nothing
                OrnamentTbl.Columns.Add("OrnamentType", GetType(String))
                OrnamentTbl.Columns.Add("OrnamentDtls", GetType(String))
                OrnamentTbl.Columns.Add("GoldCount", GetType(Integer))
                OrnamentTbl.Columns.Add("Remarks", GetType(String))
                OrnamentTbl.Columns.Add("NetWeight", GetType(String))
                OrnamentTbl.Columns.Add("GrossWeight", GetType(String))
                OrnamentTbl.Rows.Add("", "", 0, "", "", "")
                Session("OrnamentTbl") = OrnamentTbl
                DgvOrnament.DataSource = OrnamentTbl
                DgvOrnament.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ChkBank_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkBank.CheckedChanged
        If ChkBank.Checked = True Then
            DivBankDetails.Visible = True
            DivChq.Visible = True
        Else
            DivBankDetails.Visible = False
            DivChq.Visible = False
        End If
    End Sub

    Protected Sub drpTranferType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpTranferType.SelectedIndexChanged
        If drpTranferType.SelectedItem.Text = "Cheque" Then
            dvRTGS.Visible = False
        Else
            dvRTGS.Visible = True
        End If
    End Sub


    Protected Sub DgvVehicle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvVehicle.RowDeleting
        Dim dt As DataTable = DirectCast(Session("VehicleTbl"), DataTable)
        dt.Rows(e.RowIndex).Delete()

        DgvVehicle.DataSource = dt
        DgvVehicle.DataBind()
        Session("VehicleTbl") = dt

        If dt.Rows.Count <= 0 Then
            DgvVehicle.DataSource = Nothing
            VehicleTbl.Columns.Add("VType", GetType(String))
            VehicleTbl.Columns.Add("RegionNo", GetType(String))
            VehicleTbl.Columns.Add("ChasisNo", GetType(String))
            VehicleTbl.Columns.Add("EngineNo", GetType(String))
            VehicleTbl.Columns.Add("OldNew", GetType(String))
            VehicleTbl.Columns.Add("Brand", GetType(String))
            VehicleTbl.Columns.Add("Model", GetType(String))
            VehicleTbl.Columns.Add("KeySub", GetType(String))
            VehicleTbl.Columns.Add("KeyNo", GetType(String))
            VehicleTbl.Columns.Add("PermitScope", GetType(String))
            VehicleTbl.Columns.Add("RtOffice", GetType(String))
            VehicleTbl.Columns.Add("LienMarked", GetType(String))
            VehicleTbl.Columns.Add("LienNo", GetType(String))
            VehicleTbl.Columns.Add("LienDate", GetType(String))
            VehicleTbl.Columns.Add("LienReleased", GetType(String))
            VehicleTbl.Columns.Add("TextDetails", GetType(String))
            VehicleTbl.Columns.Add("TaxDate", GetType(String))
            VehicleTbl.Columns.Add("InsuranceDetails", GetType(String))
            VehicleTbl.Columns.Add("InsuranceDate", GetType(String))
            VehicleTbl.Columns.Add("InsuranceValue", GetType(String))
            VehicleTbl.Columns.Add("OtherDetails", GetType(String))
            VehicleTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("VehicleTbl") = VehicleTbl
            DgvVehicle.DataSource = VehicleTbl
            DgvVehicle.DataBind()
        End If
        'Call Set_Grid()

        ' Call Calc_OrnTot()
    End Sub
    Protected Sub Calc_IRR()
        Try
            Dim Prd As Integer
            If Val(Txt_Month.Text) > 0 Then
                Prd = Txt_Month.Text
            ElseIf Val(Txt_Days.Text) > 0 Then
                Prd = Txt_Days.Text
            End If

            If (Val(Txt_LoanAmount.Text)) > 0 And Val(Prd) > 0 And Val(Txt_Interest.Text) > 0 And HdnEMIType.Value = "Y" And HdnPrdType.Value = "M" Then
                Dim M_CommBL As New CommonBL
                Dim Dt As New DataTable
                Dim LoanAmnt As Double = Val(Txt_LoanAmount.Text)
                Dt = M_CommBL.Get_Loan_IRR(LoanAmnt, Val(Txt_Interest.Text), Prd, HdnPrdType.Value, Prd, Val(TxtAdvInst.Text))
                TxtIRR.Text = Dt.Rows(0).Item("IRR_Rate")
            Else
                Dim Emi_IRR As New DataTable
                Dim M_CommnBL As New CommonBL
                Emi_IRR = M_CommnBL.Get_IRR(CmbScheme.SelectedValue)
                If Val(Emi_IRR.Rows(0).Item("Int_IRR")) > 0 Then
                    TxtIRR.Text = Val(Emi_IRR.Rows(0).Item("Int_IRR"))
                Else
                    TxtIRR.Text = 0
                End If

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
            Dim LoanAmnt As Double = Val(Txt_LoanAmount.Text)
            Dim PrdType As String
            PrdType = HdnPrdType.Value
            Dim Prd As Integer
            If Val(Txt_Month.Text) > 0 Then
                Prd = Txt_Month.Text
            ElseIf Val(Txt_Days.Text) > 0 Then
                Prd = Txt_Days.Text
            End If
            If HdnEMIType.Value = "Y" Then
                Dim A As Double
                Dim B As Double
                Dim C As Double
                A = (LoanAmnt * Val(Txt_Interest.Text) * Prd)
                If PrdType = "M" Then
                    B = 1200
                ElseIf PrdType = "D" Then
                    B = 36500
                ElseIf PrdType = "W" Then
                    B = 36500
                End If
                C = Math.Round(((A / B) + (LoanAmnt)) / Prd, 0)
                If Val(Txt_Interest.Text) > 0 And LoanAmnt > 0 And Val(Prd) > 0 Then
                    Txt_InstAmt.Text = C.ToString()
                End If
            Else
                Txt_InstAmt.Text = Math.Round((LoanAmnt) / Prd, 0)
            End If
        Catch ex As Exception
            Return Nothing

        End Try
    End Function
End Class