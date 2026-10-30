Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Public Class LoanApprovalSecurity
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Public AccHolderTbl As New DataTable("Table")
    Public AdditionalMember As New DataTable("Table")
    Shared ParameterDt As DataTable
    Public CropTbl As New DataTable("Table")
    Public OrnamentTbl As New DataTable("Table")
    Public InsuranceTbl As New DataTable("Table")
    Public BondTbl As New DataTable("Table")
    Public VehicleTbl As New DataTable("Table")
    Public DepositTbl As New DataTable("Table")
    Dim FormID As Integer = 1085
    Shared Obj_UserRights As Object


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



            TxtTaxDate.Enabled = False
            TxtTaxDetails.Enabled = False
            TxtTaxNo.Enabled = False
            TxtInsuranceDate.Enabled = False
            TxtInsuranceNo.Enabled = False
            TxtDueDate.Enabled = False
            TxtPifDate.Attributes.Add("readonly", "readonly")

            checkotherbank_Acc()
            Keychecked()
            Load_DefaultValues()
            Set_Grid()
            TxtDateOfMaturity.Attributes.Add("readonly", "readonly")
            TxtDeedDate.Attributes.Add("readonly", "readonly")
            'TxtDepositeAmt.Attributes.Add("readonly", "readonly")
            TxtDueDate.Attributes.Add("readonly", "readonly")
            TxtDueDate0.Attributes.Add("readonly", "readonly")
            TxtDueDate5.Attributes.Add("readonly", "readonly")
            txtLienDate.Attributes.Add("readonly", "readonly")
            TxtLienReleased.Attributes.Add("readonly", "readonly")
            TxtTaxDate.Attributes.Add("readonly", "readonly")
            TxtTextDate.Attributes.Add("readonly", "readonly")
            TxtInsuranceDate.Attributes.Add("readonly", "readonly")
            TxtInsuranceDate0.Attributes.Add("readonly", "readonly")
        End If
    End Sub
    
    Private Sub Set_Grid()
        Try
            DgvLand.DataSource = Nothing
            Dim LandTbl As New DataTable("Table")
            LandTbl.Columns.Add("Tax", GetType(String))
            LandTbl.Columns.Add("Insurance", GetType(String))
            LandTbl.Columns.Add("DeedNo", GetType(String))
            LandTbl.Columns.Add("DeedDate", GetType(String))
            LandTbl.Columns.Add("PriorNo", GetType(String))
            LandTbl.Columns.Add("Extend", GetType(String))
            LandTbl.Columns.Add("District", GetType(String))
            LandTbl.Columns.Add("Tehsil", GetType(String))
            LandTbl.Columns.Add("Panchayath", GetType(String))
            LandTbl.Columns.Add("Village", GetType(String))
            LandTbl.Columns.Add("SubRegOffice", GetType(String))
            LandTbl.Columns.Add("MarketValue", GetType(String))
            LandTbl.Columns.Add("TaxNo", GetType(String))
            LandTbl.Columns.Add("TaxDate", GetType(String))
            LandTbl.Columns.Add("TaxDetails", GetType(String))
            LandTbl.Columns.Add("ModeOfOwnership", GetType(String))
            LandTbl.Columns.Add("RecoveryNo", GetType(String))
            LandTbl.Columns.Add("OtherDetails", GetType(String))
            LandTbl.Columns.Add("InsuranceNo", GetType(String))
            LandTbl.Columns.Add("InsuranceDate", GetType(String))
            LandTbl.Columns.Add("DueDate", GetType(String))
            LandTbl.Columns.Add("SurveyNo", GetType(String))
            LandTbl.Columns.Add("ResurveyNo", GetType(String))
            LandTbl.Columns.Add("DocNo", GetType(String))

            LandTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("LandTbl") = LandTbl
            DgvLand.DataSource = LandTbl
            DgvLand.DataBind()

            DgvCrop.DataSource = Nothing
            CropTbl.Columns.Add("CropName", GetType(String))
            CropTbl.Columns.Add("NoOfTrees", GetType(Integer))
            CropTbl.Columns.Add("Value", GetType(Integer))
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



            DgvCustomer.DataSource = Nothing
            AccHolderTbl.Columns.Add("MemDtls", GetType(String))
            AccHolderTbl.Columns.Add("MemBrCode", GetType(String))
            AccHolderTbl.Columns.Add("MemSchCode", GetType(String))
            AccHolderTbl.Columns.Add("MemSch", GetType(String))
            AccHolderTbl.Columns.Add("MemNo", GetType(String))
            AccHolderTbl.Columns.Add("CustID", GetType(String))
            AccHolderTbl.Columns.Add("CustName", GetType(String))
            AccHolderTbl.Columns.Add("Relation", GetType(String))
            AccHolderTbl.Columns.Add("CustAdd", GetType(String))
            AccHolderTbl.Columns.Add("CustType", GetType(String))
            AccHolderTbl.Columns.Add("CustCatg", GetType(String))
            AccHolderTbl.Columns.Add("HouseNm", GetType(String))
            AccHolderTbl.Columns.Add("Place", GetType(String))
            AccHolderTbl.Columns.Add("Dist", GetType(String))
            AccHolderTbl.Columns.Add("State", GetType(String))
            AccHolderTbl.Columns.Add("Country", GetType(String))
            AccHolderTbl.Columns.Add("Pincode", GetType(String))
            AccHolderTbl.Columns.Add("MblNo", GetType(String))
            AccHolderTbl.Columns.Add("LandPh", GetType(String))
            AccHolderTbl.Columns.Add("RelType", GetType(String))
            AccHolderTbl.Columns.Add("Dob", GetType(String))
            AccHolderTbl.Columns.Add("Age", GetType(String))

            AccHolderTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("AccHolderTbl") = AccHolderTbl
            DgvCustomer.DataSource = AccHolderTbl
            DgvCustomer.DataBind()

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
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()


        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Branch(CmbBranch1)
        M_CommonBL.Fill_Branch(CmbSurBranch)
        M_CommonBL.Fill_Branch(CmbAdMemBranch)
        M_CommonBL.Fill_Directors(CmbDirector)
        M_CommonBL.Fill_Pickup(CmbAdvocate, 83, 0)
        M_CommonBL.Fill_Pickup(CmbOrnamentType, 30, 0)
        M_CommonBL.GetSchemes(DdlScheme0, Session("TransDate"), "DEPOSIT", "FD','RIS','RD")
        M_CommonBL.GetSchemes(CmbSurMemType, Session("TransDate"), ("SHARE"))
        M_CommonBL.GetSchemes(CmbSurMem, Session("TransDate"), ("SHARE"))
        M_CommonBL.GetSchemes(CmbLoanAppType, Session("TransDate"), ("LOAN','LOAN"), "LF','LS")
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"), , , , , , , , , , CmbLoanAppType.SelectedValue)
        'M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"), "GEN.LOAN','OD")

        TxtPifDate.Attributes.Add("readonly", "readonly")
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        CmbBranch.Enabled = False
        M_CommonBL = Nothing
        SchemeDt = DirectCast(Session("SchemeTbl"), DataTable)
        AcHeadDt = DirectCast(Cache("Achead"), DataTable)
        CmbSurBranch.SelectedValue = Session("DefShareBranch")
        CmbAdMemBranch.SelectedValue = Session("DefShareBranch")
        Set_SchemeCtrl()
        GetLoanNo()
        
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call CmbLoanAppType_SelectedIndexChanged(Me.CmbLoanAppType, System.EventArgs.Empty)


    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged

    End Sub


    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            'TxtSchCode.Text = CmbScheme.SelectedValue
            Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Load_CustomerData()
        Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
        Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_CustMasterBL As New CustMasterBL

        M_CustMaster = M_CustMasterBL.Locate_Data(Val(HdnSurCustID.Value), M_PresAddress, M_PermAddress)
        Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
        M_CustMasterBL = Nothing

    End Sub
    Private Sub Load_CustomerData1()
        Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
        Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_CustMasterBL As New CustMasterBL

        M_CustMaster = M_CustMasterBL.Locate_Data(Val(HiddenSurCustid.Value), M_PresAddress, M_PermAddress)
        Locate1_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
        M_CustMasterBL = Nothing

    End Sub
    Private Sub Set_SchemeCtrl()
        Try

            Dim locAcHeadTbl As DataTable
            Dim M_CommonBL As New CommonBL
            locAcHeadTbl = M_CommonBL.Fill_SchemeHeads(CmbScheme.SelectedValue, "CHARGE")
            ' DgvTransaction.DataSource = locAcHeadTbl
            Session("locAcHeadTbl") = locAcHeadTbl
            ' DgvTransaction.DataBind()
            Session("SearchScheme") = CmbScheme.SelectedValue
            Dim TempDt As DataTable
            TempDt = M_CommonBL.Get_SchemeDtls(CmbScheme.SelectedValue)
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try

    End Sub
    Private Sub Locate1_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        Try
            Dim Address As String
            Address = M_PresAddress.AddAddress1 + "," + M_PresAddress.AddAddress2 + "," + M_PresAddress.AddAddress3 + "," + M_PresAddress.AddPinCode + M_PresAddress.AddDist

            Dim dt As DataTable = DirectCast(Session("AdditionalMember"), DataTable)

            Dim Row As DataRow
            Row = dt.NewRow
            If Len(HiddenSurMemNo.Value.Trim) > 0 Then
                Row("MemDtls") = CmbSurMem.SelectedItem.Text.Trim + " " + HiddenSurMemNo.Value.Trim + " (" + CmbAdMemBranch.SelectedValue + " )"
            End If
            Row("MemBrCode") = CmbAdMemBranch.SelectedValue
            Row("MemSchCode") = CmbSurMem.SelectedValue
            Row("MemSch") = CmbSurMemType.SelectedValue
            Row("MemNo") = HiddenSurMemNo.Value
            Row("CustID") = HiddenSurCustid.Value
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
            DgvAdditionalMem.DataSource = dt
            DgvAdditionalMem.DataBind()
            Session("AdditionalMember") = dt

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        Try
            Dim Address As String
            Address = M_PresAddress.AddAddress1 + "," + M_PresAddress.AddAddress2 + "," + M_PresAddress.AddAddress3 + "," + M_PresAddress.AddPinCode + M_PresAddress.AddDist

            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)

            Dim Row As DataRow
            Row = dt.NewRow
            If Len(HdnSurMemNo.Value.Trim) > 0 Then
                Row("MemDtls") = CmbSurMemType.SelectedItem.Text.Trim + " " + HdnSurMemNo.Value.Trim + " (" + CmbSurBranch.SelectedValue + " )"
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
            DgvCustomer.DataSource = dt
            DgvCustomer.DataBind()
            Session("AccHolderTbl") = dt

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub CheckSureties()
        'checks()
        Dim M_LoanAppBL As New LoanApplicationBL
        Dim M_CommonBL As New CommonBL
        Dim Msg As String = ""
        Dim DS_Security As DataSet
        DS_Security = M_LoanAppBL.Check_Surety(HdnAccid.Value)
        'If DS_Security.Tables(0).Rows.Count > 0 Then

        '    For I = 0 To DS_Security.Tables(0).Rows.Count - 1
        '        Msg = Msg + " " + DS_Security.Tables(0).Rows(0)(0).ToString
        '    Next
        '    ShowAlertMessage("The  Following  :" + Msg + " !!! ")
        'End If



        If DS_Security.Tables(1).Rows.Count > 0 Then
            For I = 0 To DS_Security.Tables(1).Rows.Count - 1
                CmbSurBranch.SelectedValue = DS_Security.Tables(1).Rows(I)("Ao_Shr_BrCode").ToString()
                CmbSurMemType.SelectedValue = DS_Security.Tables(1).Rows(I)("Ao_Shr_SchCode").ToString()
                'Call TxtSurMemNo_TextChanged(Me.TxtSurMemNo, System.EventArgs.Empty, DS_Security.Tables(1).Rows(I)("Ao_Shr_MemNo").ToString())
                TxtSurMemNo.Text = DS_Security.Tables(1).Rows(I)("Ao_Shr_MemNo").ToString()
                'TxtSurMem.Text=DS_Security.Tables(1).Rows(I)("Ao_Shr_MemNo").ToString()
                Dim M_MemNo As String = TxtSurMemNo.Text
                HdnSurCustID.Value = M_CommonBL.GetAccountCustID(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, TxtSurMemNo.Text, M_MemNo)
                If Len(M_MemNo.Trim) > 0 Then
                    HdnSurMemNo.Value = M_MemNo
                End If
                Load_CustomerData()
                TxtSurMemNo.Text = ""
            Next
            ChkSur.Checked = True
            Call ChkSur_CheckedChanged(Me.ChkSur, System.EventArgs.Empty)

        End If

                If DS_Security.Tables(2).Rows.Count > 0 Then
            For I = 0 To DS_Security.Tables(2).Rows.Count - 1
                Call GetGold(DS_Security.Tables(2))
            Next
            ChkGoldOrnaments.Checked = True
            Call ChkGoldOrnaments_CheckedChanged(Me.ChkGoldOrnaments, System.EventArgs.Empty)
        End If

        If DS_Security.Tables(3).Rows.Count > 0 Then
            Call GetInsurance(DS_Security.Tables(3))
            ChkInsur.Checked = True
            Call ChkInsur_CheckedChanged(Me.ChkInsur, System.EventArgs.Empty)
        End If

        If DS_Security.Tables(4).Rows.Count > 0 Then
            Call GetLandAndBuilding(DS_Security.Tables(4))
            ChkLandBuilding.Checked = True
            Call ChkLandBuilding_CheckedChanged(Me.ChkLandBuilding, System.EventArgs.Empty)
        End If


                If DS_Security.Tables(5).Rows.Count > 0 Then
                    Call GetTermDeposit(DS_Security.Tables(5))
                    ChkTermDeposit.Checked = True
                    Call ChkTermDeposit_CheckedChanged(Me.ChkTermDeposit, System.EventArgs.Empty)
                End If

                If DS_Security.Tables(6).Rows.Count > 0 Then

                    Call GetOtherBond(DS_Security.Tables(6))
                    ChkBond.Checked = True
                    Call ChkBond_CheckedChanged(Me.ChkBond, System.EventArgs.Empty)
                End If

                If DS_Security.Tables(7).Rows.Count > 0 Then

                    Call Getvehicledetails(DS_Security.Tables(7))
                    ChkVehicle.Checked = True
                    Call ChkVehicle_CheckedChanged(Me.ChkBond, System.EventArgs.Empty)
                End If

                If DS_Security.Tables(8).Rows.Count > 0 Then

                    Call GetCropDetails(DS_Security.Tables(8))
                    ChkCrops.Checked = True
                    Call ChkCrops_CheckedChanged(Me.ChkBond, System.EventArgs.Empty)
                End If
                DS_Security = Nothing
    End Sub
    Protected Sub GetCropDetails(ByVal Dtcrop As DataTable)
        Try
            Dim dt As DataTable = DirectCast(Session("CropTbl"), DataTable)
            Dim Row As DataRow
            If Dtcrop.Rows.Count > 0 Then
                For I = 0 To Dtcrop.Rows.Count - 1
                    Row = dt.NewRow
                    Row("CropName") = Dtcrop.Rows(I)("Ls_CropName").ToString()
                    Row("NoOfTrees") = Dtcrop.Rows(I)("Ls_NoOfTrees").ToString()
                    Row("Value") = Val(Dtcrop.Rows(I)("Ls_Value").ToString())
                    Try
                        dt.Rows.Add(Row)
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try
                Next
            End If




            Try
                Dim Expression As String = "CropName=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try

            DgvCrop.DataSource = dt
            DgvCrop.DataBind()
            Session("CropTbl") = dt
            TxtCropName.Text = ""
            TxtNoOfTrees.Text = ""
            TxtValue.Text = ""




        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub GetOtherBond(ByVal DtBond As DataTable)
        Try
            Dim dt As DataTable = DirectCast(Session("BondTbl"), DataTable)
            Dim Row As DataRow
            If DtBond.Rows.Count > 0 Then
                For I = 0 To DtBond.Rows.Count - 1
                    Row = dt.NewRow
                    Row("IssuedPo") = DtBond.Rows(I)("Ls_IssuedPostOffice").ToString()
                    Row("SerialNo") = DtBond.Rows(I)("Ls_SerialNo").ToString()
                    Row("PhaseValue") = Val(DtBond.Rows(I)("Ls_PhaseValue").ToString())
                    Row("DueDate") = Format(CDate(DtBond.Rows(I)("Ls_DueDate").ToString()), "MM/dd/yyyy")
                    Row("TotPhaseVal") = Val(DtBond.Rows(I)("LS_TotalPhaseValue").ToString())
                    Try
                        dt.Rows.Add(Row)
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try
                Next
            End If




            Try
                Dim Expression As String = "IssuedPo=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try

            DgvBond.DataSource = dt
            DgvBond.DataBind()
            Session("BondTbl") = dt

            TxtIssuedPostOffice.Text = ""
            TxtSerialNo.Text = ""
            TxtPhaseValue.Text = ""
            TxtDueDate.Text = ""
            TxtTotalPhaseValue.Text = ""



        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub GetGold(ByVal DtGold As DataTable)
        Try
            Dim dt As DataTable = DirectCast(Session("OrnamentTbl"), DataTable)
            Dim Row As DataRow
            If DtGold.Rows.Count > 0 Then
                For I = 0 To DtGold.Rows.Count - 1
                    Row = dt.NewRow
                    Row("OrnamentType") = DtGold.Rows(I)("LS_OrnamentType").ToString()
                    Row("OrnamentDtls") = DtGold.Rows(I)("Pkc_Description").ToString()
                    Row("GoldCount") = DtGold.Rows(I)("LS_Count").ToString()
                    Row("Remarks") = DtGold.Rows(I)("Remarks").ToString()
                    Row("NetWeight") = DtGold.Rows(I)("Ls_NetWt").ToString()
                    Row("GrossWeight") = DtGold.Rows(I)("Ls_GrossWt").ToString()
                    Try
                        dt.Rows.Add(Row)
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try
                Next
            End If


            Try
                Dim Expression As String = "OrnamentType=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try


            DgvOrnament.DataSource = dt
            DgvOrnament.DataBind()
            Session("OrnamentTbl") = dt

            CmbOrnamentType.SelectedIndex = 0
            TxtCount.Text = ""
            TxtRemarks.Text = ""
            TxtNetWeight.Text = ""
            TxtGrossWeight.Text = ""
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub Getvehicledetails(ByVal Dtvehicles As DataTable)
        Try
            Dim dt As DataTable = DirectCast(Session("VehicleTbl"), DataTable)
            Dim Row As DataRow
            If Dtvehicles.Rows.Count > 0 Then
                For i = 0 To Dtvehicles.Rows.Count - 1
                    Row = dt.NewRow
                    Row("VType") = Dtvehicles.Rows(i)("Ls_Type").ToString()
                    Row("RegionNo") = Dtvehicles.Rows(i)("Ls_RegNo").ToString()
                    Row("ChasisNo") = Dtvehicles.Rows(i)("Ls_ChassisNo").ToString()
                    Row("EngineNo") = Dtvehicles.Rows(i)("Ls_EngineNo").ToString()
                    Row("OldNew") = Dtvehicles.Rows(i)("Ls_OldOrNew").ToString() 'Format(CDate(TxtDateOfMaturity.Text), "MM/dd/yyyy") '@@@
                    Row("Brand") = Dtvehicles.Rows(i)("Ls_Brand").ToString()
                    Row("Model") = Dtvehicles.Rows(i)("Ls_VehModel").ToString()
                    Row("KeySub") = Dtvehicles.Rows(i)("Ls_KeySubmitted").ToString()
                    Row("KeyNo") = Dtvehicles.Rows(i)("Ls_KeyNo").ToString()
                    Row("PermitScope") = Dtvehicles.Rows(i)("Ls_PermitScope").ToString()
                    Row("RtOffice") = Dtvehicles.Rows(i)("Ls_RtOffice").ToString()
                    Row("LienMarked") = Dtvehicles.Rows(i)("Ls_LienMarkedInRC").ToString()
                    Row("LienNo") = Dtvehicles.Rows(i)("Ls_LienNo").ToString()
                    Row("LienDate") = Dtvehicles.Rows(i)("Ls_LienDate").ToString()
                    Row("LienReleased") = Dtvehicles.Rows(i)("Ls_LienReleasedLetterDate").ToString()
                    Row("TextDetails") = Dtvehicles.Rows(i)("Ls_TaxDetails").ToString()
                    Row("TaxDate") = Dtvehicles.Rows(i)("Ls_TaxDate").ToString()
                    Row("InsuranceDetails") = Dtvehicles.Rows(i)("Ls_InsuranceDetails").ToString()
                    Row("InsuranceDate") = Dtvehicles.Rows(i)("Ls_InsuranceDate").ToString()
                    Row("InsuranceValue") = Dtvehicles.Rows(i)("Ls_InsuranceValue").ToString()
                    Row("OtherDetails") = Dtvehicles.Rows(i)("Ls_OtherDetails").ToString()

                    Try
                        dt.Rows.Add(Row)
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try
                    Try
                        Dim Expression As String = "VType=''"
                        If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try
                Next
            End If

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
            TxtTextDate.Text = ""
            TxtInsuranceDate0.Text = ""
            TxtInsuranceDetails.Text = ""
            TxtInsuranceValue.Text = ""
            TxtOtherDetails0.Text = ""

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub GetInsurance(ByVal DtInsurance As DataTable)
        Try
            Dim dt As DataTable = DirectCast(Session("InsuranceTbl"), DataTable)
            Dim Row As DataRow
            If DtInsurance.Rows.Count > 0 Then
                For I = 0 To DtInsurance.Rows.Count - 1
                    Row = dt.NewRow
                    Row("InsuranceCompany") = DtInsurance.Rows(I)("Ls_InsuranceCompany").ToString()
                    Row("PolicyName") = DtInsurance.Rows(I)("Ls_PolicyName").ToString()
                    Row("PolicyNo") = DtInsurance.Rows(I)("Ls_PolicyNo").ToString()
                    Row("SumAssured") = DtInsurance.Rows(I)("Ls_SumAssured").ToString()
                    Row("MaturityDate") = DtInsurance.Rows(I)("Ls_DateOfMaturity").ToString() 'Format(CDate(TxtDateOfMaturity.Text), "MM/dd/yyyy") '@@@
                    Row("SurrenderValue") = DtInsurance.Rows(I)("Ls_SurrenderValue").ToString()
                    Try
                        dt.Rows.Add(Row)
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try
                    Try
                        Dim Expression As String = "InsuranceCompany=''"
                        If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try
                Next
            End If

            DgvInsurance.DataSource = dt
            DgvInsurance.DataBind()
            Session("InsuranceTbl") = dt

            TxtInsuranceCompany.Text = ""
            TxtPolicyName.Text = ""
            TxtPolicyNo.Text = ""
            TxtSumAssured.Text = ""
            TxtDateOfMaturity.Text = ""
            TxtSurrenderValue.Text = ""
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub GetLandAndBuilding(ByVal DtLand As DataTable)
        Try
            Dim dt As DataTable = DirectCast(Session("LandTbl"), DataTable)
            Dim Row As DataRow

            Row = dt.NewRow
            If DtLand.Rows.Count > 0 Then
                For I = 0 To DtLand.Rows.Count - 1


                    Row("Tax") = IIf(DtLand.Rows(I)("Ls_Insurance").ToString() = "Y", "Y", "N")
                    Row("Insurance") = IIf(DtLand.Rows(I)("Ls_TaxPaid").ToString() = "Y", "Y", "N")
                    Row("DeedNo") = DtLand.Rows(I)("Ls_DeedNo").ToString()
                    Row("DeedDate") = Format(CDate(IIf(DtLand.Rows(I)("Ls_DeedDate").ToString().Length = 0, "01/01/1900", DtLand.Rows(I)("Ls_DeedDate").ToString())), "MM/dd/yyyy")
                    Row("PriorNo") = DtLand.Rows(I)("Ls_PriorDeedNo").ToString()
                    Row("Extend") = DtLand.Rows(I)("Ls_Extend").ToString()
                    Row("District") = DtLand.Rows(I)("Ls_District").ToString()
                    Row("Tehsil") = DtLand.Rows(I)("Ls_Taluk").ToString()
                    Row("Panchayath") = DtLand.Rows(I)("Ls_Panchayath").ToString()
                    Row("Village") = DtLand.Rows(I)("Ls_Village").ToString()
                    Row("SubRegOffice") = DtLand.Rows(I)("Ls_SubRegOffice").ToString()
                    Row("MarketValue") = DtLand.Rows(I)("Ls_MarketValue").ToString()
                    Row("TaxNo") = DtLand.Rows(I)("Ls_TaxNo").ToString()
                    Row("TaxDate") = DtLand.Rows(I)("Ls_TaxDate").ToString()
                    Row("TaxDetails") = DtLand.Rows(I)("Ls_TaxDetails").ToString()
                    Row("ModeOfOwnership") = 0
                    Row("RecoveryNo") = DtLand.Rows(I)("Ls_ResurveyNo").ToString()
                    Row("OtherDetails") = DtLand.Rows(I)("Ls_OtherDetails").ToString()
                    Row("InsuranceNo") = DtLand.Rows(I)("Ls_InsuranceNo").ToString()
                    Row("InsuranceDate") = Format(CDate(IIf(DtLand.Rows(I)("Ls_InsuranceDate").ToString().Length = 0, "01/01/1900", DtLand.Rows(I)("Ls_InsuranceDate").ToString())), "MM/dd/yyyy") 'Format(CDate IIf(TxtInsuranceDate.Text.Length = 0, "01/01/1900", TxtInsuranceDate.Text)), "MM/dd/yyyy")
                    Row("DueDate") = "01/01/1900" ' Format(CDate(IIf(DtLand.Rows(I)("Ls_InsuranceDueDate").ToString() = 0, "01/01/1900", DtLand.Rows(I)("Ls_InsuranceDueDate").ToString())), "MM/dd/yyyy")
                    Row("SurveyNo") = DtLand.Rows(I)("Ls_SurveyNo").ToString()
                    Row("ResurveyNo") = DtLand.Rows(I)("Ls_ReSurNo").ToString()
                    Row("DocNo") = DtLand.Rows(I)("Ls_DocNo").ToString()
                    Try
                        dt.Rows.Add(Row)
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try
                    Try
                        Dim Expression As String = "DeedDate=''"
                        dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
                    Catch ex As Exception
                        ShowAlertMessage(ex.Message)
                    End Try

                Next
            End If


            DgvLand.DataSource = dt
            DgvLand.DataBind()
            Session("LandTbl") = dt
            TxtResurveyNo.Text = ""
            TxtDocNo.Text = ""
            TxtSurveyno.Text = ""


            TxtDeedNo.Text = ""
            TxtDeedDate.Text = ""
            TxtPriorNo.Text = ""
            TxtExtend.Text = ""
            TxtDistrict.Text = ""
            TxtTehsil.Text = ""
            TxtPunchMun.Text = ""
            TxtVillage.Text = ""
            TxtSubRegOffice.Text = ""
            TxtMarketValue.Text = ""
            TxtTaxNo.Text = ""
            TxtTaxDate.Text = ""
            TxtTaxDetails.Text = ""
            CmbModeOfOwnerdhip.SelectedIndex = 0
            TxtRecoveryNo.Text = ""
            TxtOtherDetails.Text = ""
            TxtInsuranceNo.Text = ""
            TxtInsuranceDate.Text = ""
            TxtDueDate.Text = ""


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub GetTermDeposit(ByVal DtBond As DataTable)
        Try
            Dim dt As DataTable = DirectCast(Session("DepositTbl"), DataTable)
            Dim Row As DataRow

            If DtBond.Rows.Count > 0 Then
                For I = 0 To DtBond.Rows.Count - 1
                    Row = dt.NewRow
                    Row("OtherBankAC") = IIf(ChkOtherBankAc.Checked = True, "Y", "N")
                    Row("BranchDtls1") = DtBond.Rows(I)("Br_Name").ToString()
                    Row("SchemeDtls") = DtBond.Rows(I)("Ls_AcType").ToString()
                    Row("AcNo") = DtBond.Rows(I)("Ls_AccNo").ToString()
                    Row("CertificateNo") = DtBond.Rows(I)("Ls_CertificateNo").ToString()
                    Row("DepositAmt") = DtBond.Rows(I)("Ls_DepAmt").ToString()
                    Row("AmtOutstanding") = DtBond.Rows(I)("Ls_AmtOutstanding").ToString()
                    Row("DueDate") = Format(CDate(DtBond.Rows(I)("Ls_DueDate").ToString()), "MM/dd/yyyy")
                    Row("BankName") = DtBond.Rows(I)("Ls_BankName").ToString()
                    Row("Branch") = DtBond.Rows(I)("Ls_Branch").ToString()
                    Row("AcType") = DtBond.Rows(I)("Ls_AcType").ToString()
                    Row("CustomerNo") = "" 'DtBond.Rows(I)("Ls_BankTyp").ToString()
                    Row("Name") = "" 'DtBond.Rows(I)("Ls_BankTyp").ToString()
                    Row("Addr") = "" 'DtBond.Rows(I)("Ls_BankTyp").ToString()
                    Try
                        dt.Rows.Add(Row)

                    Catch ex As Exception

                    End Try
                    Try
                        Dim Expression As String = "Scheme=''"
                        If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
                    Catch ex As Exception

                    End Try
                Next
            End If



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
            TextBox1.Text = ""
            'TxtBranch.Text = ""
            TxtAcType.Text = ""
            TxtCustomerNo.Text = ""
            'TxtName.Text = ""
            'TxtAddress.Text = ""
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Set_TransactionGrid()

    End Sub
   



    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        Try
            If ChkSur.Checked = True Then
                If DgvCustomer.Rows.Count < 2 Then
                    ShowAlertMessage("Plese Enter Two Sureties Compulsory")
                    Exit Sub
                Else
                    Call SaveOtherSuerties()
                    ShowAlertMessage("Saved Successfully")
                    Call ClearMe()
                End If
            Else
                Call SaveOtherSuerties()
                ShowAlertMessage("Saved Successfully")
                Call ClearMe()
            End If
            '  Response.Redirect("~/LoanApprovalSecurity.aspx")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try


    End Sub



    Protected Sub CmbLoanAppType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanAppType.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"), , , , , , , , , , CmbLoanAppType.SelectedValue)
            M_CommonBL = Nothing
            GetLoanNo()
            If CmbLoanAppType.SelectedValue = "LS" Then
                DivOtherSuerties.Visible = False
            Else
                DivOtherSuerties.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChkTax_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkTax.CheckedChanged
        If ChkTax.Checked Then
            TxtTaxDate.Enabled = True
            TxtTaxDetails.Enabled = True
            TxtTaxNo.Enabled = True
        Else
            TxtTaxDate.Enabled = False
            TxtTaxDetails.Enabled = False
            TxtTaxNo.Enabled = False
        End If
    End Sub

    Protected Sub ChkInsurance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkInsurance.CheckedChanged
        If ChkInsurance.Checked Then
            TxtInsuranceDate.Enabled = True
            TxtInsuranceNo.Enabled = True
            TxtDueDate.Enabled = True

        Else
            TxtInsuranceDate.Enabled = False
            TxtInsuranceNo.Enabled = False
            TxtDueDate.Enabled = False

        End If
    End Sub

   


    Protected Sub ImgBtnAddLand_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAddLand.Click
        Dim dt As DataTable = DirectCast(Session("LandTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("Tax") = IIf(ChkTax.Checked, "Y", "N")
        Row("Insurance") = IIf(ChkInsurance.Checked, "Y", "N")
        Row("DeedNo") = TxtDeedNo.Text
        Row("DeedDate") = Format(CDate(IIf(TxtDeedDate.Text.Length = 0, "01/01/1900", TxtDeedDate.Text)), "MM/dd/yyyy")
        Row("PriorNo") = TxtPriorNo.Text
        Row("Extend") = TxtExtend.Text
        Row("District") = TxtDistrict.Text
        Row("Tehsil") = TxtTehsil.Text
        Row("Panchayath") = TxtPunchMun.Text
        Row("Village") = TxtVillage.Text
        Row("SubRegOffice") = TxtSubRegOffice.Text
        Row("MarketValue") = TxtMarketValue.Text
        Row("TaxNo") = TxtTaxNo.Text
        Row("TaxDate") = Format(CDate(IIf(TxtTaxDate.Text.Length = 0, "01/01/1900", TxtTaxDate.Text)), "MM/dd/yyyy")
        Row("TaxDetails") = TxtTaxDetails.Text
        Row("ModeOfOwnership") = IIf(CmbModeOfOwnerdhip.SelectedIndex = 0, "S", "M")
        Row("RecoveryNo") = TxtRecoveryNo.Text
        Row("OtherDetails") = TxtOtherDetails.Text
        Row("InsuranceNo") = TxtInsuranceNo.Text
        Row("InsuranceDate") = Format(CDate(IIf(TxtInsuranceDate.Text.Length = 0, "01/01/1900", TxtInsuranceDate.Text)), "MM/dd/yyyy") 'Format(CDate IIf(TxtInsuranceDate.Text.Length = 0, "01/01/1900", TxtInsuranceDate.Text)), "MM/dd/yyyy")
        Row("DueDate") = Format(CDate(IIf(TxtDueDate.Text.Length = 0, "01/01/1900", TxtDueDate.Text)), "MM/dd/yyyy")
        Row("SurveyNo") = TxtSurveyno.Text
        Row("ResurveyNo") = TxtSurveyno.Text
        Row("DocNo") = TxtSurveyno.Text

        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "DeedDate=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        DgvLand.DataSource = dt
        DgvLand.DataBind()
        Session("LandTbl") = dt

        TxtDeedNo.Text = ""
        TxtDeedDate.Text = ""
        TxtPriorNo.Text = ""
        TxtExtend.Text = ""
        TxtDistrict.Text = ""
        TxtTehsil.Text = ""
        TxtPunchMun.Text = ""
        TxtVillage.Text = ""
        TxtSubRegOffice.Text = ""
        TxtMarketValue.Text = ""
        TxtTaxNo.Text = ""
        TxtTaxDate.Text = ""
        TxtTaxDetails.Text = ""
        CmbModeOfOwnerdhip.SelectedIndex = 0
        TxtRecoveryNo.Text = ""
        TxtOtherDetails.Text = ""
        TxtInsuranceNo.Text = ""
        TxtInsuranceDate.Text = ""
        TxtDueDate.Text = ""
        TxtResurveyNo.Text = ""
        TxtDocNo.Text = ""
        TxtSurveyno.Text = ""
    End Sub

    Protected Sub ImgBtnCropAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnCropAdd.Click
        Dim dt As DataTable = DirectCast(Session("CropTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("CropName") = TxtCropName.Text
        Row("NoOfTrees") = Val(TxtNoOfTrees.Text)
        Row("Value") = Val(TxtValue.Text)
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
        TxtNoOfTrees.Text = ""
        TxtValue.Text = ""
    End Sub

    Protected Sub ImgBtnOrnament_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnOrnament.Click
        Try

            If Val(TxtNetWeight.Text) > Val(TxtGrossWeight.Text) Then
                ShowAlertMessage("Net Weight cannot be greater than Gross Weight")
                Exit Sub
            End If

            If Val(TxtCount.Text) <= 0 Then
                ShowAlertMessage("Invalid Ornament Count")
                Exit Sub
            End If

            If Val(TxtGrossWeight.Text) <= 0 Then
                ShowAlertMessage("Invalid Gross Weight")
                Exit Sub
            End If

            If Val(TxtNetWeight.Text) <= 0 Then
                ShowAlertMessage("Invalid Net Weight")
                Exit Sub
            End If
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

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImgBtnInsurance_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnInsurance.Click
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

    Protected Sub ImgBtnBond_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnBond.Click
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
        TxtDueDate.Text = ""
        TxtTotalPhaseValue.Text = ""
    End Sub

    Protected Sub ImgBtnVehicle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnVehicle.Click
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

    Protected Sub ImgBtnTermDeposit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnTermDeposit.Click
        Dim dt As DataTable = DirectCast(Session("DepositTbl"), DataTable)
        Dim Row As DataRow

        Row = dt.NewRow
        Row("OtherBankAC") = ChkOtherBankAc.Text
        If ChkOtherBankAc.Checked = True Then
            Row("Branch1") = TextBox1.Text
            Row("BranchDtls1") = TextBox1.Text
            Row("Scheme") = ""
            Row("schemeDtls") = TxtAcType.Text
        Else
            Row("Branch1") = CmbBranch1.SelectedValue
            Row("BranchDtls1") = CmbBranch1.SelectedItem.Text
            Row("Scheme") = DdlScheme0.SelectedValue
            Row("schemeDtls") = DdlScheme0.SelectedItem.Text
        End If
        Row("AcNo") = TxtAcNo.Text
        Row("CertificateNo") = TxtCertificateNo.Text
        Row("DepositAmt") = TxtDepositeAmt.Text
        Row("AmtOutstanding") = TxtAmtOutstanding.Text
        Row("DueDate") = Format(CDate(TxtDueDate5.Text), "MM/dd/yyyy")
        Row("BankName") = TxtBankName.Text
        Row("Branch") = CmbBranch.SelectedValue
        Row("AcType") = TxtAcType.Text
        Row("CustomerNo") = TxtCustomerNo.Text
        Row("Name") = TextBox1.Text
        Row("Addr") = TxtAddress.Text
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
        TxtBankName.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TxtAcType.Text = ""
        TxtCustomerNo.Text = ""
        TextBox1.Text = ""
        TxtAddress1.Text = ""
    End Sub

    Private Sub DgvLand_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvLand.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("LandTbl"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvLand.DataSource = dt
            DgvLand.DataBind()
            Session("LandTbl") = dt
            If dt.Rows.Count <= 0 Then
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                Session("LandTbl") = dt
                DgvLand.DataSource = dt
                DgvLand.DataBind()

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub





    Private Sub SaveOtherSuerties()

        Try

            Dim M_LoanSecurityBO As New SMARTCORE_BO.LoanAppFormBO
            With M_LoanSecurityBO
                .BrCode = CmbBranch.SelectedValue
                .AccId = HdnAccid.Value
                .SchCode = CmbScheme.SelectedValue
                .LmApplNo = CmbLoanNo.SelectedValue
            End With
            Dim LandTbldt As DataTable = DirectCast(Session("LandTbl"), DataTable)
            Dim M_Landsw As New IO.StringWriter
            LandTbldt.WriteXml(M_Landsw)

            Dim CropTbldt As DataTable = DirectCast(Session("CropTbl"), DataTable)
            Dim M_Cropsw As New IO.StringWriter
            CropTbldt.WriteXml(M_Cropsw)

            Dim OrnamentTbldt As DataTable = DirectCast(Session("OrnamentTbl"), DataTable)
            Dim M_Ornamentsw As New IO.StringWriter
            OrnamentTbldt.WriteXml(M_Ornamentsw)

            Dim InsuranceTbldt As DataTable = DirectCast(Session("InsuranceTbl"), DataTable)
            Dim M_Insurancesw As New IO.StringWriter
            InsuranceTbldt.WriteXml(M_Insurancesw)

            Dim BondTbldt As DataTable = DirectCast(Session("BondTbl"), DataTable)
            Dim M_Bondsw As New IO.StringWriter
            BondTbldt.WriteXml(M_Bondsw)

            Dim DepositTbldt As DataTable = DirectCast(Session("DepositTbl"), DataTable)
            Dim M_Depositsw As New IO.StringWriter
            DepositTbldt.WriteXml(M_Depositsw)

            Dim VehicleTbldt As DataTable = DirectCast(Session("VehicleTbl"), DataTable)
            Dim M_Vehiclesw As New IO.StringWriter
            VehicleTbldt.WriteXml(M_Vehiclesw)

            Dim Suertiesdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_Suerties As New IO.StringWriter
            Suertiesdt.WriteXml(M_Suerties)

            Dim dt As DataTable = DirectCast(Session("AdditionalMember"), DataTable)
            Dim M_AdditionalMember As New IO.StringWriter
            dt.WriteXml(M_AdditionalMember)

            Dim M_LoanAppBL As New LoanApplicationBL

            'M_LoanAppBL.Save_LADetails(M_LoanSecurityBO, M_Landsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Cropsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Ornamentsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Insurancesw.ToString.Replace("DocumentElement", "NewDataSet"), M_Bondsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Vehiclesw.ToString.Replace("DocumentElement", "NewDataSet"), M_Depositsw.ToString.Replace("DocumentElement", "NewDataSet"))

            M_LoanAppBL.Save_LADetails(M_LoanSecurityBO, IIf(ChkLandBuilding.Checked, M_Landsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(ChkCrops.Checked, M_Cropsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(ChkGoldOrnaments.Checked, M_Ornamentsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(ChkInsur.Checked, M_Insurancesw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(ChkBond.Checked, M_Bondsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(ChkVehicle.Checked, M_Vehiclesw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(ChkTermDeposit.Checked, M_Depositsw.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(ChkSur.Checked, M_Suerties.ToString.Replace("DocumentElement", "NewDataSet"), ""), IIf(Chkaddmem.Checked, M_AdditionalMember.ToString.Replace("DocumentElement", "NewDataSet"), ""))


        Catch ex As Exception
            ShowAlertMessage(ex.ToString)
        End Try


    End Sub

    Protected Sub ChkLandBuilding_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkLandBuilding.CheckedChanged

        If ChkLandBuilding.Checked Then

            DivLandBuilding.Visible = True

        Else
            DivLandBuilding.Visible = False
        End If

    End Sub

    Protected Sub ChkCrops_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkCrops.CheckedChanged

        If ChkCrops.Checked Then

            DivCrops.Visible = True

        Else
            DivCrops.Visible = False
        End If
    End Sub

    Protected Sub ChkGoldOrnaments_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkGoldOrnaments.CheckedChanged
        If ChkGoldOrnaments.Checked Then

            DivGold.Visible = True

        Else
            DivGold.Visible = False
        End If
    End Sub


    Protected Sub ChkBond_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkBond.CheckedChanged
        If ChkBond.Checked Then

            DivBond.Visible = True

        Else
            DivBond.Visible = False
        End If
    End Sub

    Protected Sub ChkVehicle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkVehicle.CheckedChanged
        If ChkVehicle.Checked Then

            DivVehicle.Visible = True

        Else
            DivVehicle.Visible = False
        End If
    End Sub

    Protected Sub ChkTermDeposit_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkTermDeposit.CheckedChanged
        If ChkTermDeposit.Checked Then

            DivTermDeposit.Visible = True

        Else
            DivTermDeposit.Visible = False
        End If
    End Sub



    Protected Sub ChkInsur_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkInsur.CheckedChanged
        If ChkInsur.Checked Then

            DivInsurance.Visible = True

        Else
            DivInsurance.Visible = False
        End If
    End Sub

    Protected Sub DgvCrop_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles DgvCrop.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("CropTbl"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvCrop.DataSource = dt
            DgvCrop.DataBind()
            Session("CropTbl") = dt
            If dt.Rows.Count <= 0 Then
                dt.Rows.Add("", 0, 0)
                Session("CropTbl") = dt
                DgvCrop.DataSource = dt
                DgvCrop.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DgvOrnament_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles DgvOrnament.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("OrnamentTbl"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvOrnament.DataSource = dt
            DgvOrnament.DataBind()
            Session("OrnamentTbl") = dt
            If dt.Rows.Count <= 0 Then
                dt.Rows.Add("", "", 0, "", "", "")
                Session("OrnamentTbl") = dt
                DgvOrnament.DataSource = dt
                DgvOrnament.DataBind()

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DgvInsurance_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles DgvInsurance.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("InsuranceTbl"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvInsurance.DataSource = dt
            DgvInsurance.DataBind()
            Session("InsuranceTbl") = dt
            If dt.Rows.Count <= 0 Then
                dt.Rows.Add("", "", "", 0, "", "")
                Session("InsuranceTbl") = dt
                DgvInsurance.DataSource = dt
                DgvInsurance.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DgvBond_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles DgvBond.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("BondTbl"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvBond.DataSource = dt
            DgvBond.DataBind()
            Session("BondTbl") = dt
            If dt.Rows.Count <= 0 Then
                dt.Rows.Add("", 0, "", "", "")
                Session("BondTbl") = dt
                DgvBond.DataSource = dt
                DgvBond.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DgvVehicle_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles DgvVehicle.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("VehicleTbl"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvVehicle.DataSource = dt
            DgvVehicle.DataBind()
            Session("VehicleTbl") = dt
            If dt.Rows.Count <= 0 Then
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                Session("VehicleTbl") = dt
                DgvVehicle.DataSource = dt
                DgvVehicle.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAcNo_TextChanged(sender As Object, e As EventArgs) Handles TxtAcNo.TextChanged
        GetTransDetails()
    End Sub
    Private Sub GetTransDetails()
        Try

            Dim Dt_AcDtlsN As DataTable
            Dim M_CommonBL As New CommonBL
            Dim M_DepositBL As New DepositMasterBL
            Dim M_DepositBO As New SMARTCORE_BO.DepositMasterBO
            Dim Acc_ID As Integer
            Dim M_AccNo As String = TxtAcNo.Text
            TxtCustomerNo.Text = ""
            TextBox2.Text = ""
            TxtAddress1.Text = ""

            'Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
            'TxtBankName.Text = Company_Name
            'TextBox1.Text = CmbBranch.SelectedItem.Text

            If ChkOtherBankAc.Checked = False Then
                Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
                TxtBankName.Text = Company_Name
                If TxtAcNo.Text.Trim.Length <= 7 Then
                    TxtAcNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + DdlScheme0.SelectedValue, 3) + Right("0000000" + TxtAcNo.Text, 7)
                End If


                ' Dt_AcDtlsN = M_CommonBL.GetLoanApplicationDtls(CmbBranch.SelectedValue, TxtAccNo.Text, IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O"), Session("TransDate"))
                Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, DdlScheme0.SelectedValue, TxtAcNo.Text, Session("TransDate"))

                If Dt_AcDtlsN.Rows.Count > 0 Then
                    If (Dt_AcDtlsN.Rows(0)("Acc_Status").ToString() = "C") Then
                        ShowAlertMessage("Account is Closed!")
                    Else
                        TxtCustomerNo.Text = Dt_AcDtlsN.Rows(0)("Cust_ID").ToString()
                        TextBox2.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
                        TxtAddress1.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
                        TxtDueDate5.Text = Dt_AcDtlsN.Rows(0)("DueDate").ToString()
                        TextBox1.Text = Dt_AcDtlsN.Rows(0)("Br_Name").ToString()
                        TxtAmtOutstanding.Text = Format(Val(Dt_AcDtlsN.Rows(0)("Cur_Bal").ToString()), "#0.00")
                        Acc_ID = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
                        M_DepositBO = M_DepositBL.Locate_Data(Acc_ID)
                        TxtDepositeAmt.Text = Format(M_DepositBO.DmDepAmount, "#0.00")

                    End If


                    If DdlScheme0.SelectedValue <> Dt_AcDtlsN.Rows(0)("Sch_Code").ToString() Then
                        DdlScheme0.SelectedValue = Dt_AcDtlsN.Rows(0)("Sch_Code").ToString()

                        If M_AccNo.Length >= 8 Then
                            DdlScheme0_SelectedIndexChanged(Me.DdlScheme0, System.EventArgs.Empty)
                        End If
                    End If

                Else
                    ShowAlertMessage("Invalid Account No")
                    TxtAcNo.Focus()

                End If
            End If


            M_CommonBL = Nothing
            M_DepositBO = Nothing
            M_DepositBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub
    Private Sub checkotherbank_Acc()
        If ChkOtherBankAc.Checked = True Then

            DdlScheme0.Enabled = False

            TxtAcType.Enabled = True
            CmbBranch1.Enabled=False
            TxtCertificateNo.Enabled = True
        Else

            DdlScheme0.Enabled = True

            TxtAcType.Enabled = False
            CmbBranch1.Enabled=True
            TxtCertificateNo.Enabled = False
        End If
    End Sub

    Protected Sub ChkOtherBankAc_CheckedChanged(sender As Object, e As EventArgs) Handles ChkOtherBankAc.CheckedChanged
        checkotherbank_Acc()
    End Sub

    Protected Sub ChkKeySubmited_CheckedChanged(sender As Object, e As EventArgs) Handles ChkKeySubmited.CheckedChanged
        Keychecked()
    End Sub
    Private Sub Keychecked()
        If ChkKeySubmited.Checked = True Then
            TxtKeyNo.Enabled = True
            TxtPermitScope.Enabled = True
        Else
            TxtKeyNo.Enabled = False
            TxtPermitScope.Enabled = False
        End If
    End Sub
  
   
    Protected Sub ChkLienMarked_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLienMarked.CheckedChanged

    End Sub

    Protected Sub DdlScheme0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlScheme0.SelectedIndexChanged

    End Sub

    
    Protected Sub Clear_Click(sender As Object, e As ImageClickEventArgs) Handles Clear.Click
        ClearMe()
    End Sub
    Private Sub ClearMe()
        Set_Grid()

        TxtMemberno.Text = ""
        TxtAmsam.Text = ""
        TxtAddress.Text = ""
        TxtLoanAmount.Text = ""
        TxtLoanDate.Text = ""
        TxtPifDate.Text = ""
        TxtValue.Text = ""
        TxtVillage.Text = ""
        TxtPropertyValue.Text = ""
        TxtRiDate.Text = ""
        txtRiAmt.Text = ""
        TxtDesam.Text = ""
        TxtAcNo.Text = ""
        TxtAcType.Text = ""
        TxtAddress.Text = ""
        TxtDueDate.Text=""
        TxtAmtOutstanding.Text = ""
        TxtBrand.Text = ""
        TxtCertificateNo.Text = ""
        TxtChassisNo.Text = ""
        TxtCount.Text = ""
        TxtCropName.Text = ""
        TxtCropName.Text = ""
        TxtCustomerNo.Text = ""
        TxtDateOfMaturity.Text = ""
        TxtDeedDate.Text = ""
        TxtDeedNo.Text = ""
        TxtDepositeAmt.Text = ""
        TxtDistrict.Text = ""
        TxtDueDate.Text = ""
        TxtDueDate0.Text = ""
        TxtDueDate5.Text = ""
        TxtEngineNo.Text = ""
        TxtExtend.Text = ""
        TxtGrossWeight.Text = ""
        TxtInsuranceCompany.Text = ""
        TxtInsuranceDate.Text = ""
        TxtInsuranceDate0.Text = ""
        TxtInsuranceDetails.Text = ""
        TxtInsuranceNo.Text = ""
        TxtInsuranceValue.Text = ""
        TxtIssuedPostOffice.Text = ""
        TxtKeyNo.Text = ""
        txtLienDate.Text = ""
        TxtLienNo.Text = ""
        TxtLienReleased.Text = ""
        TxtMarketValue.Text = ""
        TxtMemberno.Text = ""
        TxtModel.Text = ""
        TxtName.Text = ""
        TxtNetWeight.Text = ""
        TxtNoOfTrees.Text = ""
        TxtOtherDetails.Text = ""
        TxtOtherDetails0.Text = ""
        TxtPermitScope.Text = ""
        TxtPhaseValue.Text = ""
        TxtPolicyName.Text = ""
        TxtPolicyNo.Text = ""
        TxtPriorNo.Text = ""
        TxtPunchMun.Text = ""
        TxtRecoveryNo.Text = ""
        TxtRegionNo.Text = ""
        TxtRemarks.Text = ""
        TxtRTOffice.Text = ""
        TxtBankName.Text = ""
        TextBox2.Text = ""
        TextBox1.Text = ""
        TxtSerialNo.Text = ""
        TxtSubRegOffice.Text = ""
        TxtSumAssured.Text = ""
        TxtSurrenderValue.Text = ""
        TxtTaxDate.Text = ""
        TxtTaxDetails.Text = ""
        TxtTaxNo.Text = ""
        TxtAddress1.Text = ""
        TxtTehsil.Text = ""
        TxtTextDate.Text = ""
        TxtTextDetails.Text = ""
        TxtTotalPhaseValue.Text = ""
        TxtType.Text = ""
        TextBox1.Text = ""
        TxtAddress1.Text = ""
        TxtSurMem.Text = ""
        TxtSurveyno.Text = ""
        TxtResurveyNo.Text = ""
        TxtDocNo.Text = ""

    End Sub

    Protected Sub close_Click(sender As Object, e As ImageClickEventArgs) Handles close.Click
        Response.Redirect("Main.aspx")
    End Sub

    Protected Sub DgvDeposit_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles DgvDeposit.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("DepositTbl"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvDeposit.DataSource = dt
            DgvDeposit.DataBind()
            Session("DepositTbl") = dt
            If dt.Rows.Count <= 0 Then
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "")
                Session("DepositTbl") = dt
                DgvDeposit.DataSource = dt
                DgvDeposit.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    
  

    Protected Sub ChkSur_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkSur.CheckedChanged
        If ChkSur.Checked = True Then
            DivSur.Visible = True
        Else
            DivSur.Visible = False
        End If
    End Sub

    Protected Sub ChkSec_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkSec.CheckedChanged
        If ChkSec.Checked = True Then
            DivOtherSuerties.Visible = True
        Else
            DivOtherSuerties.Visible = False
        End If
    End Sub
    Protected Sub DgvCustomer_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvCustomer.RowDeleting
        Try
            Dim CustCode As String = CInt(DgvCustomer.DataKeys(e.RowIndex).Value)
            DeleteCust(CustCode)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteCust(ByVal CustCode As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            For i = 0 To DgvCustomer.Rows.Count - 1
                If dt.Rows.Item(i).Item(5).ToString = CustCode Then
                    dt.Rows.Item(i).Delete()
                    GoTo otherow
                End If
            Next i
otherow:
            DgvCustomer.DataSource = dt
            DgvCustomer.DataBind()
            Session("AccHolderTbl") = dt


            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetLoanNo()
        Dim M_LoanApplicationBL As New LoanApplicationBL
        M_LoanApplicationBL.GetLaNo(CmbBranch.SelectedValue, IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O"), CmbLoanNo)
        CmbLoanNo.Items.Insert("0", "SELECT")
        CmbLoanNo.SelectedIndex = "0"
    End Sub

    Protected Sub CmbLoanNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanNo.SelectedIndexChanged

        Try
            Set_Grid()
            Dim Dt_AcDtlsN As DataTable
            Dim Dt_AccStatus As DataTable
            Dim M_CommonBL As New CommonBL
            Dim M_loanAppBL As New LoanApplicationBL

            Dt_AcDtlsN = M_CommonBL.GetLoanApplicationDtls(CmbBranch.SelectedValue, CmbLoanNo.SelectedValue, IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O"), Session("TransDate"))
            If Dt_AcDtlsN.Rows.Count > 0 Then
                HdnCustID.Value = Dt_AcDtlsN.Rows(0)("Cust_ID").ToString()
                TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
                TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
                TxtAmsam.Text = Dt_AcDtlsN.Rows(0)("Cust_Amsam").ToString()
                TxtDesam.Text = Dt_AcDtlsN.Rows(0)("Cust_Desam").ToString()
                HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Shr_Accid").ToString()
                TxtMemberno.Text = Dt_AcDtlsN.Rows(0)("CustMemNo").ToString()
                CmbDirector.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_Director").ToString()
                CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
                TxtLoanAmount.Text = Dt_AcDtlsN.Rows(0)("Lm_LoanAmount").ToString()
                TxtLoanDate.Text = Dt_AcDtlsN.Rows(0)("Lm_LoanDate").ToString()
                CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
                TxtPifDate.Text = Dt_AcDtlsN.Rows(0)("Site_Date").ToString()
                TxtPropertyValue.Text = Dt_AcDtlsN.Rows(0)("Site_Valuate").ToString()
                txtRiAmt.Text = Dt_AcDtlsN.Rows(0)("Record_Inspect").ToString()
                TxtRiDate.Text = Dt_AcDtlsN.Rows(0)("Record_Date").ToString()
                CheckSureties()
            Else
                ShowAlertMessage("  Invalid Loan Application No")
            End If
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

  
    Protected Sub HdnSurCustID_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles HdnSurCustID.ValueChanged

    End Sub

    Protected Sub Chkaddmem_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Chkaddmem.CheckedChanged
        If Chkaddmem.Checked = True Then
            AddMem.Visible = True
        Else
            AddMem.Visible = False
        End If
    End Sub

    Protected Sub TxtSurMem_TextChanged(ByVal sender As Object, ByVal e As EventArgs, Optional ByVal AccNo As String = "") Handles TxtSurMem.TextChanged
        Try
            Dim Dt_AcDtlsN As DataTable
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String
            Dim dt As New DataTable
            Dim M_Common As New LoanApplicationBL
            TxtSurMem.Text = Right("000" + CmbSurBranch.SelectedValue, 3) + Right("000" + CmbSurMemType.SelectedValue, 3) + Right("0000000" + TxtSurMem.Text, 7)
            If CmbLoanNo.SelectedValue = TxtSurMemNo.Text Then
                ShowAlertMessage("Application Member and Suerty Member are Same !")
                TxtSurMemNo.Text = ""
                Exit Sub
            End If
            If AccNo.Length > 0 Then
                M_AccNo = AccNo
                TxtSurMem.Text = AccNo
            Else
                M_AccNo = TxtSurMem.Text
            End If
            Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtlsN.Rows.Count = 0 Then
                ShowAlertMessage("Invalid Member No Entered")
                M_CommonBL = Nothing
                Exit Sub
            ElseIf Dt_AcDtlsN.Rows.Count > 0 Then
                If (Dt_AcDtlsN.Rows(0)("Acc_Status").ToString() = "C") Then
                    ShowAlertMessage("Account is Closed!")
                    Exit Sub
                Else
                    SurMemAccid0.Value = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
                End If
            End If
            Dim M_MemNo As String = TxtSurMem.Text
            HiddenSurCustid.Value = M_CommonBL.GetAccountCustID(CmbAdMemBranch.SelectedValue, CmbSurMem.SelectedValue, TxtSurMem.Text, M_MemNo)
            If Len(M_MemNo.Trim) > 0 Then
                HiddenSurMemNo.Value = M_MemNo
            End If
            M_CommonBL = Nothing           
                    Load_CustomerData1()
                    TxtSurMem.Text = ""               
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    
    

    Protected Sub DgvAdditionalMem_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvAdditionalMem.RowDeleting
        Try
            Dim CustCode As String = CInt(DgvAdditionalMem.DataKeys(e.RowIndex).Value)
            DeleteAdlMem(CustCode)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteAdlMem(ByVal CustCode As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("AdditionalMember"), DataTable)
            For i = 0 To DgvAdditionalMem.Rows.Count - 1
                If dt.Rows.Item(i).Item(5).ToString = CustCode Then
                    dt.Rows.Item(i).Delete()
                    GoTo otherow
                End If
            Next i
otherow:
            DgvAdditionalMem.DataSource = dt
            DgvAdditionalMem.DataBind()
            Session("AdditionalMember") = dt
            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtSurMemNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs, Optional ByVal AccNo As String = "") Handles TxtSurMemNo.TextChanged
        Try
            Dim Dt_AcDtlsN As DataTable
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String
            Dim dt As New DataTable
            Dim M_Common As New LoanApplicationBL
            TxtSurMemNo.Text = Right("000" + CmbSurBranch.SelectedValue, 3) + Right("000" + CmbSurMemType.SelectedValue, 3) + Right("0000000" + TxtSurMemNo.Text, 7)
            If CmbLoanNo.SelectedValue = TxtSurMemNo.Text Then
                ShowAlertMessage("Application Member and Suerty Member are Same !")
                TxtSurMemNo.Text = ""
                Exit Sub
            End If
            If AccNo.Length > 0 Then
                M_AccNo = AccNo
                TxtSurMemNo.Text = AccNo
            Else
                M_AccNo = TxtSurMemNo.Text
            End If
            dt = M_Common.CheckSur(TxtSurMemNo.Text, CmbScheme.SelectedValue)
            If dt.Rows.Count = 2 Then
                ShowAlertMessage("Already Surety Give Two Times.Exceed Limit!")
                Exit Sub
            Else
                Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, M_AccNo, Session("TransDate"))
                If Dt_AcDtlsN.Rows.Count = 0 Then
                    ShowAlertMessage("Invalid Member No Entered")
                    M_CommonBL = Nothing
                    Exit Sub
                ElseIf Dt_AcDtlsN.Rows.Count > 0 Then
                    If (Dt_AcDtlsN.Rows(0)("Acc_Status").ToString() = "C") Then
                        ShowAlertMessage("Account is Closed!")
                        Exit Sub
                    Else
                        SurMemAccid.Value = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
                    End If
                End If
            End If
            Dim M_MemNo As String = TxtSurMemNo.Text
            HdnSurCustID.Value = M_CommonBL.GetAccountCustID(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, TxtSurMemNo.Text, M_MemNo)
            If Len(M_MemNo.Trim) > 0 Then
                HdnSurMemNo.Value = M_MemNo
            End If
            M_CommonBL = Nothing
            If ChkSur.Checked = True Then
                If DgvCustomer.Rows.Count = 2 Then
                    ShowAlertMessage("Maximum Two Sureties will Accept!")
                    Exit Sub
                Else
                    Load_CustomerData()
                    TxtSurMemNo.Text = ""
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class