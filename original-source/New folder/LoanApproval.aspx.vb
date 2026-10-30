Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class LoanApproval
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Public AccHolderTbl As New DataTable("Table")
    Shared ParameterDt As DataTable
    Dim FormID As Integer = 1084

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            TxtPifDate.Attributes.Add("readonly", "readonly")
            Load_DefaultValues()
            ' Set_Grid()
        End If
    End Sub
    Private Sub Load_DefaultValues()


        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Directors(CmbDirector)
        M_CommonBL.Fill_Pickup(CmbAdvocate, 86, 0)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"), "GEN.LOAN','OD")
        M_CommonBL.GetSchemes(CmbLoanAppType, Session("TransDate"), ("LOAN','LOAN"), "LF','LS")
        TxtPifDate.Attributes.Add("readonly", "readonly")

        M_CommonBL = Nothing
        SchemeDt = DirectCast(Session("SchemeTbl"), DataTable)
        AcHeadDt = DirectCast(Cache("Achead"), DataTable)

        Set_SchemeCtrl()
        GetLoanAppNo()
      
        Call CmbLoanStatus_SelectedIndexChanged(Me.CmbLoanStatus, System.EventArgs.Empty)
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        'CmbBranch.SelectedValue = Session("DefaultBrCode")
    End Sub

    Private Sub ClearMemberDetails()
        HdnCustID.Value = Nothing
        TxtName.Text = ""
        TxtAddress.Text = ""
        TxtAmsam.Text = ""
        TxtDesam.Text = ""
        TxtMemberno.Text = ""
        HdnAccid.Value = Nothing
        TxtLoanAmount.Text = ""
        TxtPropertyValue.Text = ""
        TxtReason.Text = ""
        TxtResDate.Text = ""
        TxtResNo.Text = ""
        TxtLoanDate.Text = ""
        TxtPifDate.Text = ""
        TxtRiDate.Text = ""
        TxtLoanSanAmount.Text = ""

    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            'TxtSchCode.Text = CmbScheme.SelectedValue
            Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    'Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
    '    On Error Resume Next
    '    CmbBranch.SelectedValue = TxtBranch.Text
    '    TxtBranch.Text = CmbBranch.SelectedValue
    'End Sub

    'Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
    '    On Error Resume Next
    '    CmbScheme.SelectedValue = TxtSchCode.Text
    '    TxtSchCode.Text = CmbScheme.SelectedValue
    'End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    'Protected Sub CmbMemType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbMemType.SelectedIndexChanged
    '    TxtMemNo.Text = Right("000" + CmbMemBranch.SelectedValue, 3) + Right("000" + CmbMemType.SelectedValue, 3)
    'End Sub

    'Protected Sub TxtMemNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtMemNo.TextChanged
    '    'Dim Dt_AcDtlsN As DataTable
    '    'Dim M_CommonBL As New CommonBL
    '    'Dim M_AccNo As String = TxtMemNo.Text

    '    'Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbMemBranch.SelectedValue, CmbMemType.SelectedValue, M_AccNo, Session("TransDate"))
    '    'If Dt_AcDtlsN.Rows.Count > 0 Then
    '    '    HdnCustID.Value = Dt_AcDtlsN.Rows(0)("Cust_ID").ToString()
    '    '    ' Session("CustomerId") = TxtCustID.Text
    '    '    TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
    '    '    TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
    '    '    TxtAmsam.Text = Dt_AcDtlsN.Rows(0)("Cust_Amsam").ToString()
    '    '    TxtDesam.Text = Dt_AcDtlsN.Rows(0)("Cust_Desam").ToString()

    '    '    HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
    '    'End If

    '    'M_CommonBL = Nothing

    'End Sub


    Private Sub Load_CustomerData()
        Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
        Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_CustMasterBL As New CustMasterBL

        ' M_CustMaster = M_CustMasterBL.Locate_Data(Val(HdnSurCustID.Value), M_PresAddress, M_PermAddress)
        Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
        M_CustMasterBL = Nothing

    End Sub


    'Private Sub Set_Grid()
    '    DgvCustomer.DataSource = Nothing
    '    AccHolderTbl.Columns.Add("MemDtls", GetType(String))
    '    AccHolderTbl.Columns.Add("MemBrCode", GetType(String))
    '    AccHolderTbl.Columns.Add("MemSchCode", GetType(String))
    '    AccHolderTbl.Columns.Add("MemSch", GetType(String))
    '    AccHolderTbl.Columns.Add("MemNo", GetType(String))
    '    AccHolderTbl.Columns.Add("CustID", GetType(String))
    '    AccHolderTbl.Columns.Add("CustName", GetType(String))
    '    AccHolderTbl.Columns.Add("Relation", GetType(String))
    '    AccHolderTbl.Columns.Add("CustAdd", GetType(String))
    '    AccHolderTbl.Columns.Add("CustType", GetType(String))
    '    AccHolderTbl.Columns.Add("CustCatg", GetType(String))
    '    AccHolderTbl.Columns.Add("HouseNm", GetType(String))
    '    AccHolderTbl.Columns.Add("Place", GetType(String))
    '    AccHolderTbl.Columns.Add("Dist", GetType(String))
    '    AccHolderTbl.Columns.Add("State", GetType(String))
    '    AccHolderTbl.Columns.Add("Country", GetType(String))
    '    AccHolderTbl.Columns.Add("Pincode", GetType(String))
    '    AccHolderTbl.Columns.Add("MblNo", GetType(String))
    '    AccHolderTbl.Columns.Add("LandPh", GetType(String))
    '    AccHolderTbl.Columns.Add("RelType", GetType(String))
    '    AccHolderTbl.Columns.Add("Dob", GetType(String))
    '    AccHolderTbl.Columns.Add("Age", GetType(String))

    '    AccHolderTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
    '    Session("AccHolderTbl") = AccHolderTbl
    '    DgvCustomer.DataSource = AccHolderTbl
    '    DgvCustomer.DataBind()
    'End Sub
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


    Private Sub Set_TransactionGrid()
        'Dim M_CommonBL As New CommonBL
        'Dim Dt As DataTable

        'Dt = M_CommonBL.Set_TransactionLoanChargeGrid(CmbScheme.SelectedValue, "01/01/1900", Val(TxtLoanAmt.Text))

        'DgvTransaction.DataSource = Dt
        'DgvTransaction.DataBind()
        'Session("TransTbl") = Dt
        'M_CommonBL = Nothing

    End Sub


    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)


        'Dim Address As String
        'Address = M_PresAddress.AddAddress1 + "," + M_PresAddress.AddAddress2 + "," + M_PresAddress.AddAddress3 + "," + M_PresAddress.AddPinCode + M_PresAddress.AddDist



        'Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)

        'Dim Row As DataRow
        'Row = dt.NewRow
        'If Len(HdnSurMemNo.Value.Trim) > 0 Then
        '    Row("MemDtls") = CmbSurMemType.SelectedItem.Text.Trim + " " + HdnSurMemNo.Value.Trim + " (" + CmbSurBranch.SelectedValue + " )"
        'End If
        'Row("MemBrCode") = CmbSurBranch.SelectedValue
        'Row("MemSchCode") = CmbSurMemType.SelectedValue
        'Row("MemSch") = CmbSurMemType.SelectedValue
        'Row("MemNo") = HdnSurMemNo.Value
        'Row("CustID") = HdnSurCustID.Value
        'Row("CustName") = M_CustMaster.CustName
        'Row("Relation") = ""
        'Row("CustAdd") = Address
        'Row("CustType") = M_CustMaster.CustTypeCode
        'Row("CustCatg") = M_CustMaster.CustCatgCode
        'Row("HouseNm") = M_PresAddress.AddAddress1
        'Row("Place") = M_PresAddress.AddAddress2
        'Row("Dist") = M_PresAddress.AddDist
        'Row("State") = M_PresAddress.AddAddress3
        'Row("Country") = M_PresAddress.AddCountry
        'Row("Pincode") = M_PresAddress.AddPinCode
        'Row("MblNo") = M_CustMaster.CustMobile
        'Row("LandPh") = M_CustMaster.CustPhone1
        'Row("RelType") = M_CustMaster.CustRelTypCode
        'If IsDate(M_CustMaster.CustDobInc) = False Then
        '    Row("Dob") = "01/01/1900"
        'Else
        '    Row("Dob") = M_CustMaster.CustDobInc
        'End If
        'Row("Age") = Val(M_CustMaster.CustAge)
        'Try
        '    dt.Rows.Add(Row)
        '    'ClearCustomer()
        'Catch ex As Exception

        'End Try

        'Try
        '    Dim Expression As String = "CustID=''"
        '    dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        'Catch ex As Exception

        'End Try
        ''   DgvCustomer.DataSource = dt
        '' DgvCustomer.DataBind()
        'Session("AccHolderTbl") = dt




    End Sub
    'Protected Sub TxtSurMemNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSurMemNo.TextChanged
    '    'Dim Dt_AcDtlsN As DataTable
    '    'Dim M_CommonBL As New CommonBL
    '    ''  Dim M_AccNo As String = TxtSurMemNo.Text

    '    'Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, M_AccNo, Session("TransDate"))
    '    'If Dt_AcDtlsN.Rows.Count = 0 Then
    '    '    ShowAlertMessage("Invalid Member No Entered")
    '    '    M_CommonBL = Nothing
    '    '    Exit Sub
    '    'End If





    '    'Dim M_MemNo As String = TxtSurMemNo.Text
    '    'HdnSurCustID.Value = M_CommonBL.GetAccountCustID(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, TxtSurMemNo.Text, M_MemNo)
    '    'If Len(M_MemNo.Trim) > 0 Then
    '    '    HdnSurMemNo.Value = M_MemNo
    '    'End If
    '    'M_CommonBL = Nothing
    '    Load_CustomerData()



    'End Sub





    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        Dim Dt As Date = IIf(TxtResDate.Text.Length = 0, "01/01/1900", TxtResDate.Text)
        Dim M_LoanApplicationBO As New SMARTCORE_BO.LoanApplicationBO
        With M_LoanApplicationBO

            .LmID = HdnAccid.Value
            .LoanType = IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O")
            .BrCode = CmbBranch.SelectedValue
            .SchCode = CmbScheme.SelectedValue
            .LmApplSlno = "0"
            .LmLoanSchCode = CmbScheme.SelectedValue
            .CustId = HdnCustID.Value
            .ShrAccid = HdnAccid.Value
            .LmLoanAmount = Val(TxtLoanSanAmount.Text)
            .LmDirector = CmbDirector.SelectedValue
            .LmLoanDate = Session("TransDate")
            .LmPeriod = "0"
            .LmPeriodType = "N"
            .LmHolidayPeriod = "0"
            .LmHolidayPeriodType = "N"
            .LmMainpurpose = "0"
            .LmSubpurpose = "0"
            .LmResNo = IIf(TxtResNo.Visible = False, "", TxtResNo.Text)
            .LmResDate = IIf(TxtResDate.Visible = False, "01/01/1900", Dt)
            .LmSanctionBy = "0"
            .LmSanctionNo = "0"
            .LmSecurityDetails = "0"
            .LmOperationMode = "0"
            .LmRemarks = IIf(TxtReason.Text = "", "", TxtReason.Text)
            .LmInsurance = "0"
            .LmRegType = "0"
            .LmRegNo = "0"
            .LmIntroducedby = "0"
            .LmStatus = IIf(CmbLoanStatus.SelectedIndex = 0, "A", "R")
            .LmClosedDate = "01/01/1900"
            .RecordInspect = Val(txtRiAmt.Text)
            .RecordDate = Dt
            .SiteValuate = Val(TxtPropertyValue.Text)
            .SiteDate = Dt 'Format(Dt, "MM/dd/yyyy")
            .BoardApproval = "0"
            .BoradDate = "01/01/1900"
            .SanctionLetter = "0"
            .SLetterDate = "01/01/1900"
            .Disbursement = "0"
            .DisburseDate = "01/01/1900"
            .LogID = "0"
            .MakerID = Session("UserID")
            .MakingTime = Date.Now



        End With

        Dim M_LoanApplicationBL As New LoanApplicationBL
        Dim M_Dt As New DataTable
        Session("M_EntryMode") = "APPROVAL"

        M_Dt = M_LoanApplicationBL.Save_Data(M_LoanApplicationBO, Session("M_EntryMode"), "", "")

        ShowAlertMessage(" Updated Successfully")
        Call ClearMemberDetails()
    End Sub

    Protected Sub CmbLoanAppType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanAppType.SelectedIndexChanged
        GetLoanAppNo()
        ClearMemberDetails()
    End Sub

    Protected Sub CmbLoanStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanStatus.SelectedIndexChanged

        If CmbLoanStatus.SelectedIndex = 0 Then
            LblReason.Visible = False
            LblResDate.Visible = True
            TxtResDate.Visible = True
            TxtResNo.Visible = True
            LblResno.Visible = True
            TxtReason.Visible = False
            TxtResNo.Focus()
            LblSanAmt.Visible = True
            TxtLoanSanAmount.Visible = True



        Else
            LblReason.Visible = True
            LblResDate.Visible = False
            LblResno.Visible = False
            TxtResNo.Visible = False
            TxtResDate.Visible = False
            TxtReason.Visible = True
            LblSanAmt.Visible = False
            TxtLoanSanAmount.Visible = False
            TxtReason.Focus()

        End If

    End Sub

    Protected Sub CmbAdvocate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbAdvocate.SelectedIndexChanged

    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Call ClearMemberDetails()

    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click

    End Sub

    Protected Sub CmbLoanNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanNo.SelectedIndexChanged
        Try


            Dim Dt_AcDtlsN As DataTable
            Dim Dt_AccStatus As DataTable
            Dim Dt As New DataTable
            Dim M_CommonBL As New CommonBL
            Dim M_loanAppBL As New LoanApplicationBL
            Dt = M_loanAppBL.BranchDetailsDtls(CmbLoanNo.SelectedValue)
            If CmbBranch.SelectedValue <> Dt.Rows(0)("Br_Code").ToString() Then
                CmbBranch.SelectedValue = Dt.Rows(0)("Br_Code").ToString()
            Else
                CmbBranch.SelectedValue = Dt.Rows(0)("Br_Code").ToString()
            End If
            Dt_AcDtlsN = M_CommonBL.GetLoanApplicationDtls(CmbBranch.SelectedValue, CmbLoanNo.SelectedValue, IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O"), Session("TransDate"))
            If Dt_AcDtlsN.Rows.Count > 0 Then

                HdnCustID.Value = Dt_AcDtlsN.Rows(0)("Cust_ID").ToString()
                ' Session("CustomerId") = TxtCustID.Text
                TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
                TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
                TxtAmsam.Text = Dt_AcDtlsN.Rows(0)("Cust_Amsam").ToString()
                TxtDesam.Text = Dt_AcDtlsN.Rows(0)("Cust_Desam").ToString()

                HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Shr_Accid").ToString()
                TxtMemberno.Text = Dt_AcDtlsN.Rows(0)("ACC_NO").ToString()
                CmbDirector.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_Director").ToString()
                CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
                TxtLoanAmount.Text = Dt_AcDtlsN.Rows(0)("Lm_LoanAmount").ToString()
                TxtLoanDate.Text = Dt_AcDtlsN.Rows(0)("Lm_LoanDate").ToString()
                '  CmbLoanAppType.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
                TxtPifDate.Text = Dt_AcDtlsN.Rows(0)("Site_Date").ToString()
                TxtPropertyValue.Text = Dt_AcDtlsN.Rows(0)("Site_Valuate").ToString()
                txtRiAmt.Text = Dt_AcDtlsN.Rows(0)("Record_Inspect").ToString()
                TxtRiDate.Text = Dt_AcDtlsN.Rows(0)("Record_Date").ToString()
                CmbBranch.SelectedValue = Dt_AcDtlsN.Rows(0)("Br_Code").ToString()
                ' CmbDirector.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_SanctionBy").ToString()

            Else
                ShowAlertMessage("Invalid Loan Application No")
                Call ClearMemberDetails()

            End If


            M_CommonBL = Nothing
            CmbLoanStatus.Focus()

        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Private Sub GetLoanAppNo()
        Dim M_LoanApplicationBL As New LoanApplicationBL
        M_LoanApplicationBL.GetLoanAppNo(IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O"), CmbLoanNo)
        CmbLoanNo.Items.Insert("0", "SELECT")
        CmbLoanNo.SelectedIndex = "0"
    End Sub
End Class
