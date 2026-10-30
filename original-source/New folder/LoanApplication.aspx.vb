Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass

Public Class LoanApplication
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Public AccHolderTbl As New DataTable("Table")
    Public AdditionalMember As New DataTable("Table")
    Public FieldRptTbl As New DataTable("Table")
    Public DocRptTbl As New DataTable("Table")
    Shared ParameterDt As DataTable
    Dim FormID As Integer = 1082
    Shared Obj_UserRights As Object
    Private Shared Common As New CommonBL



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


            TxtIssueDate.Attributes.Add("readonly", "readonly")
            Load_DefaultValues()
            Set_Grid()
            If Len(Request.QueryString("TRANID")) > 0 Then
                GetAuthoriseDtls("LA", Request.QueryString("TRANID"))
            End If
            Session("SearchType") = "CUSTOMER"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If

        If Session("SearchResult") <> "" Then

            TxtCustId.Text = Session("SearchResult")
            TxtCustId_TextChanged(Me.TxtCustId, System.EventArgs.Empty)
            TxtCustId.Focus()
            Session("SearchResult") = ""

        End If





    End Sub

    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("LFLS", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        M_TranTbl = M_Dataset.Tables(0)
        Session("SuretyTbl") = M_Dataset.Tables(1)

        Session("Adlmem") = M_Dataset.Tables(2)

        Session("TransTbl") = M_Dataset.Tables(3)

        M_CommonBL = Nothing
        GetAccountDtls(M_TranTbl)
        DgvCustomer.DataSource = M_Dataset.Tables(1)
        DgvCustomer.DataBind()

        DgvAdditionalMem.DataSource = M_Dataset.Tables(2)
        DgvAdditionalMem.DataBind()
       
        DgvTransaction.DataSource = M_Dataset.Tables(3)
        DgvTransaction.DataBind()

        'Calc_TranTotal()
        'DgvCustomer.DataSource = M_Dataset.Tables(2)
        'DgvCustomer.DataBind()

    End Sub

    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable)
        'If M_AccTbl.Rows(0).Item("LM_Type") = "S" Then
        '    CmbLoanAppType.SelectedIndex = 1
        '    DivSur.Visible = True
        'Else
        '    CmbLoanAppType.SelectedIndex = 0
        '    AddMem.Visible = True
        'End If
        AddMem.Visible = True
        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        'CmbMemBranch.SelectedValue = M_AccTbl.Rows(0).Item("MemBrCode")
        ' CmbMemType.SelectedValue = M_AccTbl.Rows(0).Item("MemSchCode")
        '        CmbDirector.SelectedValue = M_AccTbl.Rows(0).Item("LM_Director")
        TxtApplNo.Text = M_AccTbl.Rows(0).Item("LM_Appl_SLNo")
        ' TxtMemNo.Text = M_AccTbl.Rows(0).Item("Memberno")
        Panel1.Visible = True
        Cmb_LoanAppNo.Visible = False
        ' CmbLoanAppType.Enabled = False
        TxtLoanAmt.Enabled = False
        TxtApplNo.Enabled = False
        CmbBranch.Enabled = False
        TxtBranch.Enabled = False
        CmbScheme.Enabled = False
        TxtSchCode.Enabled = False
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        '  Call TxtMemNo_TextChanged(Me.TxtMemNo, System.EventArgs.Empty)
        Call TxtApplNo_TextChanged(Me.TxtApplNo, System.EventArgs.Empty)
        Call TxtLoanAmt_TextChanged(Me.TxtLoanAmt, System.EventArgs.Empty)
        TxtLoanAmt.Text = Decimal.Round(M_AccTbl.Rows(0).Item("Lm_LoanAmount")).ToString()
        CmbScheme.SelectedValue = M_AccTbl.Rows(0).Item("Sch_Code")
        TxtSchCode.Text = M_AccTbl.Rows(0).Item("Sch_Code")
    End Sub


    Private Sub Load_DefaultValues()
        '   SetGrid()

        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        ' M_CommonBL.Fill_Branch(CmbSurBranch)
        M_CommonBL.Fill_Branch(CmbAdMemBranch)
        M_CommonBL.Fill_Pickup(cmbOpinion, 97, 0)
        M_CommonBL.Fill_Pickup(CmbScore, 96, 0)
        M_CommonBL.Fill_Pickup(CmbDocument, 98, 0)
        ' M_CommonBL.Fill_Branch(CmbMemBranch)
        '  M_CommonBL.Fill_Directors(CmbDirector)
        '  M_CommonBL.GetSchemes(CmbLoanAppType, Session("TransDate"), ("LOAN','LOAN"), "LF','LS")
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"))
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"), "GEN.LOAN','OD")
        'M_CommonBL.GetSchemes(CmbMemType, Session("TransDate"), ("SHARE"))
        M_CommonBL.GetSchemes(CmbSurMem, Session("TransDate"), ("SHARE"))
        '  M_CommonBL.GetSchemes(CmbSurMemType, Session("TransDate"), ("SHARE"))
        AddMem.Visible = False
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        ' M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"))
        TxtIssueDate.Attributes.Add("readonly", "readonly")
        M_CommonBL = Nothing
        SchemeDt = DirectCast(Session("SchemeTbl"), DataTable)
        '  AcHeadDt = DirectCast(Session("AcHeadTbl"), DataTable)
        AcHeadDt = DirectCast(Cache("Achead"), DataTable)
        TxtIssueDate.Text = Session("TransDate")
        GetLoanApplicationNo()
        Set_SchemeCtrl()
        Cmb_LoanAppNo.Items.Insert("0", "SELECT")
        Cmb_LoanAppNo.SelectedIndex = "0"
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        'Call CmbLoanAppType_SelectedIndexChanged(Me.CmbLoanAppType, System.EventArgs.Empty)
        LblRctCashTotal.Text = "0.00"
        LblRctTransferTotal.Text = "0.00"
        LblPayCashTotal.Text = "0.00"
        LblPayTransferTotal.Text = "0.00"
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        Panel1.Visible = False
    End Sub
    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
        GetLoanApplicationNo()
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtSchCode.Text = CmbScheme.SelectedValue
            'Set_SchemeCtrl()
            DivTran.Visible = False
            TxtLoanAmt.Text = " "
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtSchCode.Text
        TxtSchCode.Text = CmbScheme.SelectedValue


    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    'Protected Sub CmbMemType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbMemType.SelectedIndexChanged
    '    ' TxtMemNo.Text = Right("000" + CmbMemBranch.SelectedValue, 3) + Right("000" + CmbMemType.SelectedValue, 3)
    'End Sub

    'Protected Sub TxtMemNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtMemNo.TextChanged
    '    Try
    '        Dim Dt_AcDtlsN As DataTable
    '        Dim M_CommonBL As New CommonBL

    '        'If TxtMemNo.Text.Length <> 0 And HdnCustID.Value.Length = 0 Then
    '        '    TxtMemNo.Text = Right("000" + CmbMemBranch.SelectedValue, 3) + Right("000" + CmbMemType.SelectedValue, 3) + Right("0000000" + TxtMemNo.Text, 7)
    '        'End If

    '        ' Dim M_AccNo As String = TxtMemNo.Text
    '        'Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbMemBranch.SelectedValue, CmbMemType.SelectedValue, M_AccNo, Session("TransDate"))
    '        ' If Dt_AcDtlsN.Rows.Count > 0 Then
    '        'HdnCustID.Value = Dt_AcDtlsN.Rows(0)("Cust_ID").ToString()
    '        '' Session("CustomerId") = TxtCustID.Text
    '        'TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
    '        'TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
    '        'TxtAmsam.Text = Dt_AcDtlsN.Rows(0)("Cust_Amsam").ToString()
    '        'TxtDesam.Text = Dt_AcDtlsN.Rows(0)("Cust_Desam").ToString()
    '        'TxtMemNo.Text = Dt_AcDtlsN.Rows(0)("Acc_NO").ToString()
    '        'HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
    '        'TxtLoanAmt.Focus()
    '        'Else
    '        'Call ClearMemberDetails()
    '        'End If

    '        M_CommonBL = Nothing
    '    Catch ex As Exception
    '        ShowAlertMessage(ex.Message)
    '    End Try
    'End Sub

  


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
            Row("MemSch") = "0" 'CmbSurMemType.SelectedValue
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

    Private Sub Set_Grid()
        Try
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

            DgvField.DataSource = Nothing
            FieldRptTbl.Columns.Add("ACCID", GetType(String))
            FieldRptTbl.Columns.Add("LANO", GetType(String))
            FieldRptTbl.Columns.Add("EnqID", GetType(String))
            FieldRptTbl.Columns.Add("EnqIDDesc", GetType(String))
            FieldRptTbl.Columns.Add("EnqRateID", GetType(String))
            FieldRptTbl.Columns.Add("EnqRateDesc", GetType(String))
            FieldRptTbl.Columns.Add("EnqValue", GetType(String))


            FieldRptTbl.Rows.Add("", "", "", "", "", "", "")
            Session("FieldRptTbl") = FieldRptTbl
            DgvField.DataSource = FieldRptTbl
            DgvField.DataBind()

            '*****
            DgvDoc.DataSource = Nothing
            DocRptTbl.Columns.Add("ACCID", GetType(String))
            DocRptTbl.Columns.Add("LANO", GetType(String))
            DocRptTbl.Columns.Add("DocID", GetType(String))
            DocRptTbl.Columns.Add("DocDesc", GetType(String))


            DocRptTbl.Rows.Add("", "", "", "")
            Session("DocRptTbl") = DocRptTbl
            DgvDoc.DataSource = DocRptTbl
            DgvDoc.DataBind()



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
    Private Sub Set_SchemeCtrl()
        Try

            Dim locAcHeadTbl As DataTable
            Dim M_CommonBL As New CommonBL
            locAcHeadTbl = M_CommonBL.Fill_SchemeHeads(CmbScheme.SelectedValue, "CHARGE")
            DgvTransaction.DataSource = locAcHeadTbl
            Session("locAcHeadTbl") = locAcHeadTbl
            DgvTransaction.DataBind()
            Session("SearchScheme") = CmbScheme.SelectedValue
            Dim TempDt As DataTable
            TempDt = M_CommonBL.Get_SchemeDtls(CmbScheme.SelectedValue)
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try

    End Sub


    'Private Sub Set_TransactionGrid()
    '    Dim M_CommonBL As New CommonBL
    '    Dim Dt As DataTable
    '    Dt = M_CommonBL.Set_TransactionLoanChargeGrid(CmbScheme.SelectedValue, Session("TransDate"), Val(TxtLoanAmt.Text), IIf(ChkPropertyOnHold.Checked = True, "Y", "N"))
    '    DgvTransaction.DataSource = Dt
    '    DgvTransaction.DataBind()
    '    Session("TransTbl") = Dt
    '    'Session("AccHolderTbl") = Dt
    '    Session("locAcHeadTbl") = Dt


    '    M_CommonBL = Nothing
    '    Calc_TranTotal()
    'End Sub


    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        Try
            Dim Address As String
            Address = M_PresAddress.AddAddress1 + "," + M_PresAddress.AddAddress2 + "," + M_PresAddress.AddAddress3 + "," + M_PresAddress.AddPinCode + M_PresAddress.AddDist



            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)

            Dim Row As DataRow
            Row = dt.NewRow
            If Len(HdnSurMemNo.Value.Trim) > 0 Then
                Row("MemDtls") = "0" 'CmbSurMemType.SelectedItem.Text.Trim + " " + HdnSurMemNo.Value.Trim + " (" + CmbSurBranch.SelectedValue + " )"
            End If
            Row("MemBrCode") = "0" 'CmbSurBranch.SelectedValue
            Row("MemSchCode") = "0" 'CmbSurMemType.SelectedValue
            Row("MemSch") = "0" 'CmbSurMemType.SelectedValue
            Row("MemNo") = "0" 'HdnSurMemNo.Value
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

    Protected Sub TxtLoanAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtLoanAmt.TextChanged
        '
        'Set_TransactionGrid()
        '  Call Get_ServiceTax()
        Calc_TranTotal()
        ' DivTran.Visible = True
    End Sub
    Private Sub Calc_TranTotal()
        On Error Resume Next
        Dim Trandt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)

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

    End Sub

    Private Function Get_ServiceTax() As DataTable
        On Error Resume Next
        Dim M_CommonBL As New CommonBL
        Dim Trandt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
        Dim M_TranSw As New IO.StringWriter

        Trandt.TableName = "Table"

        Trandt.WriteXml(M_TranSw)
        Trandt = M_CommonBL.Get_ServiceTax(Session("DefaultBrCode"), "C", M_TranSw.ToString())

        DgvTransaction.DataSource = Trandt
        DgvTransaction.DataBind()
        Session("locAcHeadTbl") = Trandt


        Return Trandt
    End Function



    Private Sub DgvCustomer_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvCustomer.RowDeleting
        Try
            Dim CustCode As String = CInt(DgvCustomer.DataKeys(e.RowIndex).Value)
            DeleteCust(CustCode)
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        Try
            If Len(Request.QueryString("TRANID")) > 0 Then
                Dim M_CommonBL1 As New CommonBL
                If M_CommonBL1.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                    ShowAlertMessage("Authotisation Failed")
                    Response.Redirect("~/ManagerPassing.aspx.vb")
                    Exit Sub
                End If
                M_CommonBL1 = Nothing
                Response.Redirect("~/ManagerPassing.aspx?Passing=y")
                Exit Sub
            End If
            CheckSurety()


        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub CmbLoanAppType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanAppType.SelectedIndexChanged

    '    Dim M_CommonBL As New CommonBL
    '    M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"))
    '    M_CommonBL = Nothing
    '    DivSur.Visible = True
    '    'If CmbLoanAppType.SelectedValue = "LS" Then

    '    '    DivTran.Visible = True
    '    'Else
    '    '    DivSur.Visible = False
    '    '    DivTran.Visible = True
    '    'End If

    '    Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
    'End Sub

   

    Private Sub ClearMemberDetails()
        HdnCustID.Value = Nothing
        TxtName.Text = ""
        TxtAddress.Text = ""

        ' HdnAccid.Value = Nothing
        TxtLoanAmt.Text = ""

        LblRctCashTotal.Text = "0.00"
        LblRctTransferTotal.Text = "0.00"
        LblPayCashTotal.Text = "0.00"
        LblPayTransferTotal.Text = "0.00"
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Set_Grid()
        Call ClearMemberDetails()
    End Sub



    Protected Sub HdnCustID_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles HdnCustID.ValueChanged

    End Sub

    Protected Sub Cmb_LoanAppNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cmb_LoanAppNo.SelectedIndexChanged
        On Error Resume Next
        ' TxtBranch.Text = CmbBranch.SelectedValue

        Dim Dt_AcDtlsN As DataTable
        Dim Dt_AccStatus As DataTable
        Dim Dt_MemDtls As DataTable
        Dim M_loanAppBL As New LoanApplicationBL
        Dim M_CommonBL As New CommonBL
        Dt_AccStatus = M_loanAppBL.CheckAppFormStatus(Cmb_LoanAppNo.SelectedValue)


        If Dt_AccStatus.Rows.Count > 0 And Len(Request.QueryString("TRANID")) = 0 Then
            Call ClearMemberDetails()
            ShowAlertMessage("This Form No is  Registered with " & Dt_AccStatus.Rows(0)("LM_AccNo").ToString() & " Application No.")
            Exit Sub
        End If



        '  Dim M_AccNo As String = TxtMemNo.Text

        Dt_AcDtlsN = M_loanAppBL.GetLoanApplicationFormDtls(Cmb_LoanAppNo.SelectedValue)
        If Dt_AcDtlsN.Rows.Count > 0 Then
            HdnCustID.Value = Dt_AcDtlsN.Rows(0)("CustID").ToString()

            Locate_Customer(HdnCustID.Value)

            ' Session("CustomerId") = TxtCustID.Text
            ' TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
            ' TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
            ' TxtAmsam.Text = Dt_AcDtlsN.Rows(0)("Cust_Amsam").ToString()
            'TxtDesam.Text = Dt_AcDtlsN.Rows(0)("Cust_Desam").ToString()
            '
            HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Shr_Accid").ToString()
            '  TxtMemNo.Text = Dt_AcDtlsN.Rows(0)("Acc_NO").ToString()
            'CmbDirector.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_ID").ToString()
            CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
            Dt_MemDtls = M_CommonBL.Get_MemberDetails(Dt_AcDtlsN.Rows(0)("Lm_ID").ToString())
            ' CmbMemBranch.SelectedValue = Dt_MemDtls.Rows(0)("Br_Code").ToString()
            'CmbMemType.SelectedValue = Dt_MemDtls.Rows(0)("scheme").ToString()

        Else
            Call ClearMemberDetails()
            ShowAlertMessage("Invalid Form Number ")
            TxtLoanAmt.Focus()
        End If

        M_loanAppBL = Nothing
        M_CommonBL = Nothing
        CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        'TxtMemNo_TextChanged(Me.TxtMemNo, System.EventArgs.Empty)
        TxtLoanAmt.Focus()
    End Sub
    Protected Sub DgvAdditionalMem_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvAdditionalMem.RowDeleting
        Try
            Dim CustCode As String = CInt(DgvAdditionalMem.DataKeys(e.RowIndex).Value)
            DeleteAdlMem(CustCode)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Locatecustomer(ByVal Cust_ID As String)
        Try
            Dim Common As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = Common.GetCustDetails(Cust_ID)

            If M_Dt.Rows.Count > 0 Then
                TxtName.Text = M_Dt.Rows(0)("Cust_Name").ToString()
                TxtAddress.Text = M_Dt.Rows(0)("relname").ToString() + " ,  " + M_Dt.Rows(0)("Full_Address").ToString()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
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
    
    Private Sub GetLoanApplicationNo()
        Dim M_LoanApplicationBL As New LoanApplicationBL
        M_LoanApplicationBL.GetLoanApplicationNo(CmbBranch.SelectedValue, Cmb_LoanAppNo)
        Cmb_LoanAppNo.Items.Insert("0", "SELECT")
        Cmb_LoanAppNo.SelectedIndex = "0"
    End Sub
  
    Protected Sub TxtApplNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtApplNo.TextChanged
        On Error Resume Next
        ' TxtBranch.Text = CmbBranch.SelectedValue

        Dim Dt_AcDtlsN As DataTable
        Dim Dt_AccStatus As DataTable
        Dim Dt_MemDtls As DataTable
        Dim M_loanAppBL As New LoanApplicationBL
        Dim M_CommonBL As New CommonBL
        Dt_AccStatus = M_loanAppBL.CheckAppFormStatus(TxtApplNo.Text)



        If Dt_AccStatus.Rows.Count > 0 And Len(Request.QueryString("TRANID")) = 0 Then
            Call ClearMemberDetails()
            ShowAlertMessage("This Form No is  Registered with " & Dt_AccStatus.Rows(0)("LM_AccNo").ToString() & " Application No.")
            Exit Sub
        End If



        '  Dim M_AccNo As String = TxtMemNo.Text

        Dt_AcDtlsN = M_loanAppBL.GetLoanApplicationFormDtls(TxtApplNo.Text)
        If Dt_AcDtlsN.Rows.Count > 0 Then
            HdnCustID.Value = Dt_AcDtlsN.Rows(0)("CustID").ToString()
            HdnCustID.Value = Dt_AcDtlsN.Rows(0)("Cust_ID").ToString()
            ' Session("CustomerId") = TxtCustID.Text
            TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
            TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
   
            ' HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Shr_Accid").ToString()
            ' TxtMemNo.Text = Dt_AcDtlsN.Rows(0)("Acc_NO").ToString()
            'CmbDirector.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_ID").ToString()
            CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
            Dt_MemDtls = M_CommonBL.Get_MemberDetails(Dt_AcDtlsN.Rows(0)("Lm_ID").ToString())
            ' CmbMemBranch.SelectedValue = Dt_MemDtls.Rows(0)("Br_Code").ToString()
            ' CmbMemType.SelectedValue = Dt_MemDtls.Rows(0)("scheme").ToString()

        Else
            Call ClearMemberDetails()
            ShowAlertMessage("Invalid Form Number ")
            TxtLoanAmt.Focus()
        End If

        M_loanAppBL = Nothing
        M_CommonBL = Nothing
        CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        ' TxtMemNo_TextChanged(Me.TxtMemNo, System.EventArgs.Empty)
        TxtLoanAmt.Focus()
    End Sub
    Private Sub CheckSurety()
        'If CmbLoanAppType.SelectedItem.Text = "LA OTHER LOAN" Then
        '    Save()
        'ElseIf CmbLoanAppType.SelectedItem.Text = "LA SUERTY" Then
        '    If DgvCustomer.Rows.Count < 2 Then
        '        ShowAlertMessage("Please Enter Two Sureties Compulsory")
        '        Exit Sub
        '    Else
        '        Save()
        '    End If

        'End If
        Save()
    End Sub
    Private Sub Save()
        Try

            If (Cmb_LoanAppNo.SelectedItem.Text = "SELECT") Then
                ShowAlertMessage("Please Select LA Form  No!")
                Exit Sub
            Else
                Dim M_LoanApplicationBO As New SMARTCORE_BO.LoanApplicationBO

                With M_LoanApplicationBO

                    .LmID = 0
                    .LoanType = "S" 'IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O")
                    .BrCode = Session("DefaultBrCode")
                    .SchCode = CmbScheme.SelectedValue
                    .LmApplSlno = Cmb_LoanAppNo.SelectedValue
                    .LmLoanSchCode = CmbScheme.SelectedValue
                    .CustId = HdnCustID.Value
                    .ShrAccid = "0" 'HdnAccid.Value
                    .LmLoanAmount = Val(TxtLoanAmt.Text)
                    .LmDirector = "0" 'CmbDirector.SelectedValue
                    .LmLoanDate = Session("TransDate")
                    .LmPeriod = "0"
                    .LmPeriodType = "N"
                    .LmHolidayPeriod = "0"
                    .LmHolidayPeriodType = "N"
                    .LmMainpurpose = "0"
                    .LmSubpurpose = "0"
                    .LmResNo = "0"
                    .LmResDate = "01/01/1900"
                    .LmSanctionBy = "0"
                    .LmSanctionNo = "0"
                    .LmSecurityDetails = "0"
                    .LmOperationMode = "0"
                    .LmRemarks = "Nil"
                    .LmInsurance = "0"
                    .LmRegType = "0"
                    .LmRegNo = "0"
                    .LmIntroducedby = "0"
                    .LmStatus = "Y"
                    .LmClosedDate = "01/01/1900"
                    .RecordInspect = "0"
                    .RecordDate = "01/01/1900"
                    .SiteValuate = "0"
                    .SiteDate = "01/01/1900"
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



                Dim Suertiesdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
                Dim M_Suerties As New IO.StringWriter
                Suertiesdt.WriteXml(M_Suerties)

                Dim EnQdt As DataTable = DirectCast(Session("FieldRptTbl"), DataTable)
                Dim M_EnQSr As New IO.StringWriter
                EnQdt.WriteXml(M_EnQSr)

                Dim Docdt As DataTable = DirectCast(Session("DocRptTbl"), DataTable)
                Dim M_DocSr As New IO.StringWriter
                Docdt.WriteXml(M_DocSr)


                'Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
                'Dim M_TranSw As New IO.StringWriter
                'Trandt.WriteXml(M_TranSw)

            

                Dim M_LoanApplicationBL As New LoanApplicationBL

                Dim DtDocCheck As New DataTable
                Dim I As Integer
                Dim StrMsg As String = "\n"
                DtDocCheck = M_LoanApplicationBL.CheckDocument(CmbScheme.SelectedValue, M_DocSr.ToString.Replace("DocumentElement", "NewDataSet"))
                If DtDocCheck.Rows.Count > 0 Then
                    For I = 0 To DtDocCheck.Rows.Count - 1
                        StrMsg = StrMsg + DtDocCheck.Rows(I)(0).ToString() & "\n"
                    Next
                    ShowAlertMessage("Please Add The Following Mandatory Documents. " & StrMsg)
                    Exit Sub
                End If
                Dim M_LoanAppBL As New LoanApplicationBL
                Dim M_Dt As New DataTable
                Session("M_EntryMode") = "NEW"
                M_Dt = M_LoanApplicationBL.Save_Data(M_LoanApplicationBO, Session("M_EntryMode"), M_Suerties.ToString.Replace("DocumentElement", "NewDataSet"), "", M_EnQSr.ToString.Replace("DocumentElement", "NewDataSet"), M_DocSr.ToString.Replace("DocumentElement", "NewDataSet"))

                If Chkaddmem.Checked = True Then
                    Dim M_LoanSecurityBO As New SMARTCORE_BO.LoanAppFormBO
                    With M_LoanSecurityBO
                        .BrCode = CmbBranch.SelectedValue
                        .AccId = HdnAccid.Value
                        .SchCode = CmbScheme.SelectedValue
                        .LmApplNo = Cmb_LoanAppNo.SelectedValue
                    End With
                    Dim dt As DataTable = DirectCast(Session("AdditionalMember"), DataTable)
                    Dim M_AdditionalMember As New IO.StringWriter
                    dt.WriteXml(M_AdditionalMember)
                    M_LoanAppBL.Save_LADetails(M_LoanSecurityBO, "", "", "", "", "", "", "", "", IIf(Chkaddmem.Checked, M_AdditionalMember.ToString.Replace("DocumentElement", "NewDataSet"), ""))
                End If
                ShowAlertMessage(" LOAN APPLICATION NO : " & M_Dt.Rows(0)("Acc_No").ToString)
                Response.Redirect("~/NewAccount.aspx?AccNo=" & CmbScheme.SelectedItem.Text & " - " & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=GENERAL" & "&ModuleCatg=GEN.LOAN" & "&SCHEME=" & "LS" & "&SEC=" & "GENERAL" & "&DRCR=" & "C" & "&TRANCATG=" & "TRAN")


            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub TxtSurMem_TextChanged(ByVal sender As Object, ByVal e As EventArgs, Optional ByVal AccNo As String = "") Handles TxtSurMem.TextChanged
        Try
            Dim Dt_AcDtlsN As DataTable
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String
            Dim dt As New DataTable
            Dim M_Common As New LoanApplicationBL
            ' TxtSurMem.Text = Right("000" + CmbAdMemBranch.SelectedValue, 3) + Right("000" + CmbSurMem.SelectedValue, 3) + Right("0000000" + TxtSurMem.Text, 7)
            
            If AccNo.Length > 0 Then
                M_AccNo = AccNo
                TxtSurMem.Text = AccNo
            Else
                M_AccNo = TxtSurMem.Text
            End If
            Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbAdMemBranch.SelectedValue, CmbSurMem.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtlsN.Rows.Count = 0 Then
                ShowAlertMessage("Invalid Member No Entered")
                M_CommonBL = Nothing
                Exit Sub
            ElseIf Dt_AcDtlsN.Rows.Count > 0 Then
                If (Dt_AcDtlsN.Rows(0)("Acc_Status").ToString() = "C") Then
                    ShowAlertMessage("Account is Closed!")
                    Exit Sub
                Else
                    ' SurMemAccid0.Value = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
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

    Protected Sub Chkaddmem_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Chkaddmem.CheckedChanged

        If Chkaddmem.Checked = True Then
            AddMem.Visible = True
        Else
            AddMem.Visible = False
        End If

    End Sub

  
    Protected Sub ChkSur_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkSur.CheckedChanged
        If ChkSur.Checked = True Then
            DivSur.Visible = True
        Else
            DivSur.Visible = False
        End If
    End Sub
    Private Sub Locate_customer(ByVal Cust_ID As String)
        Try
            Dim M_Dt As New DataTable
            M_Dt = Common.GetCustDetails(Cust_ID)

            If M_Dt.Rows.Count > 0 Then
                TxtName.Text = M_Dt.Rows(0)("Cust_Name").ToString()
                TxtAddress.Text = M_Dt.Rows(0)("relname").ToString() + " ,  " + M_Dt.Rows(0)("Full_Address").ToString()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub TxtCustId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustId.TextChanged


        Try
            Dim Dt_AcDtlsN As DataTable
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String
            Dim M_SurCustId As String
            Dim dt As New DataTable
            Dim M_Common As New LoanApplicationBL
            ' TxtSurMemNo.Text = Right("000" + CmbSurBranch.SelectedValue, 3) + Right("000" + CmbSurMemType.SelectedValue, 3) + Right("0000000" + TxtSurMemNo.Text, 7)
            'If TxtMemNo.Text = TxtSurMemNo.Text Then
            '    ShowAlertMessage("Application Member and Suerty Member are Same !")
            '    TxtSurMemNo.Text = ""
            '    Exit Sub
            'End If
            If Val(TxtCustId.Text) > 0 Then
                M_SurCustId = TxtCustId.Text
                HdnSurCustID.Value = M_SurCustId
            Else
                TxtCustId.Text = ""
                M_SurCustId = ""
            End If
            'If ChkRenewal.Checked = False Then
            '    dt = M_Common.CheckSur(TxtSurMemNo.Text, CmbScheme.SelectedValue)
            '    If dt.Rows.Count = 2 Then
            '        ShowAlertMessage("Already Surety Give Two Times.Exceed Limit!")
            '        Set_Grid()
            '        Exit Sub
            '    End If
            'Else

 

            'Dim M_MemNo As String = TxtSurMemNo.Text
            'HdnSurCustID.Value = M_CommonBL.GetAccountCustID(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, TxtSurMemNo.Text, M_MemNo)
            'If Len(M_MemNo.Trim) > 0 Then
            '    HdnSurMemNo.Value = M_MemNo
            'End If
            'M_CommonBL = Nothing
            If Val(TxtCustId.Text) > 0 Then
                Load_CustomerData()
            End If


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try


       
    End Sub


    Protected Sub BtnEnqAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnqAdd.Click
        Try


            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("FieldRptTbl"), DataTable)
            For i = 0 To DgvField.Rows.Count - 1
                If dt.Rows.Item(i).Item(2).ToString = CmbScore.SelectedValue Then
                    ShowAlertMessage("Select Next Score Point.")
                    Exit Sub

                End If
            Next i



            'Dim dt As DataTable = DirectCast(Session("FieldRptTbl"), DataTable)

            Dim Row As DataRow
            Row = dt.NewRow

            Row("ACCID") = "0"
            Row("LANO") = "0"
            Row("EnqID") = CmbScore.SelectedValue
            Row("EnqIDDesc") = CmbScore.SelectedItem.Text
            Row("EnqRateID") = CmbOpinion.SelectedValue
            Row("EnqRateDesc") = CmbOpinion.SelectedItem.Text
            Row("EnqValue") = ""
           
            Try
                dt.Rows.Add(Row)
                'ClearCustomer()
            Catch ex As Exception

            End Try

            Try
                Dim Expression As String = "EnqID=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try
            DgvField.DataSource = dt
            DgvField.DataBind()
            Session("FieldRptTbl") = dt
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DgvField_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvField.RowDeleting
        Try
            Dim EnqCode As String = CInt(DgvField.DataKeys(e.RowIndex).Value)
            DeleteEnQID(EnqCode)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteEnQID(ByVal EnqCode As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("FieldRptTbl"), DataTable)
            For i = 0 To DgvField.Rows.Count - 1
                If dt.Rows.Item(i).Item(2).ToString = EnqCode Then
                    dt.Rows.Item(i).Delete()
                    GoTo otherow
                End If
            Next i
otherow:
            DgvField.DataSource = dt
            DgvField.DataBind()
            Session("FieldRptTbl") = dt


            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub BtnDocAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnDocAdd.Click
        Try


            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("DocRptTbl"), DataTable)
            For i = 0 To DgvDoc.Rows.Count - 1
                If dt.Rows.Item(i).Item(2).ToString = CmbDocument.SelectedValue Then
                    ShowAlertMessage("Select Next Document.")
                    Exit Sub

                End If
            Next i




            'Dim dt As DataTable = DirectCast(Session("FieldRptTbl"), DataTable)

            Dim Row As DataRow
            Row = dt.NewRow

            Row("ACCID") = "0"
            Row("LANO") = "0"
            Row("DocID") = CmbDocument.SelectedValue
            Row("DocDesc") = CmbDocument.SelectedItem.Text
  

            Try
                dt.Rows.Add(Row)
                'ClearCustomer()
            Catch ex As Exception

            End Try

            Try
                Dim Expression As String = "DocID=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try
            DgvDoc.DataSource = dt
            DgvDoc.DataBind()
            Session("DocRptTbl") = dt
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub DeleteDocID(ByVal DocCode As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("DocRptTbl"), DataTable)
            For i = 0 To DgvDoc.Rows.Count - 1
                If dt.Rows.Item(i).Item(2).ToString = DocCode Then
                    dt.Rows.Item(i).Delete()
                    GoTo otherow
                End If
            Next i
otherow:
            DgvDoc.DataSource = dt
            DgvDoc.DataBind()
            Session("DocRptTbl") = dt


            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DgvDoc_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvDoc.RowDeleting
        Try
            Dim DocCode As String = CInt(DgvDoc.DataKeys(e.RowIndex).Value)
            DeleteDocID(DocCode)
        Catch ex As Exception

        End Try
    End Sub
End Class