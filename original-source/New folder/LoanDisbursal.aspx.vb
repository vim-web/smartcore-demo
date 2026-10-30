

Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_Common.CommonClass
Partial Public Class LoanDisbursal
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Public AccHolderTbl As New DataTable("Table")
    Shared ParameterDt As DataTable
    Dim FormID As Integer = 1096
    Shared Obj_UserRights As Object
    Private Shared Common As New CommonBL
    Public DocRptTbl As New DataTable("Table")

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

            TxtPifDate.Attributes.Add("readonly", "readonly")
            Load_DefaultValues()
            ' Set_Grid()
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        'M_CommonBL.Fill_Directors(CmbDirector)
        M_CommonBL.Fill_Pickup(CmbDocument, 100, 0)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"), "GEN.LOAN','OD")
        ' M_CommonBL.GetSchemes(CmbLoanAppType, Session("TransDate"), ("LOAN','LOAN"), "LF','LS")
        TxtPifDate.Attributes.Add("readonly", "readonly")

        CmbBranch.SelectedValue = Session("DefaultBrCode")
        CmbBranch.Enabled = False
        M_CommonBL = Nothing
        SchemeDt = DirectCast(Session("SchemeTbl"), DataTable)
        AcHeadDt = DirectCast(Cache("Achead"), DataTable)
        Set_SchemeCtrl()
        GetsecLaNo()
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Set_Grid()
    End Sub


    Private Sub ClearMemberDetails()
        HdnCustID.Value = Nothing
        TxtName.Text = ""
        TxtAddress.Text = ""
        ' TxtAmsam.Text = ""
        ' TxtDesam.Text = ""
        'TxtMemberno.Text = ""
        HdnAccid.Value = Nothing
        TxtLoanAmount.Text = ""
        TxtPropertyValue.Text = ""
        TxtDisbursement.Text = ""
        Session("DocRptTbl") = Nothing
        Dim dt As DataTable = DirectCast(Session("DocRptTbl"), DataTable)
        DgvDoc.DataSource = dt
        DgvDoc.DataBind()
        GetsecLaNo()

    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            'TxtSchCode.Text = CmbScheme.SelectedValue
            Call ClearMemberDetails()
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
            'DgvTransaction.DataSource = locAcHeadTbl
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



    Protected Sub Set_Grid()

        DgvDoc.DataSource = Nothing
        DocRptTbl.Columns.Add("ACCID", GetType(String))
        DocRptTbl.Columns.Add("LANO", GetType(String))
        DocRptTbl.Columns.Add("DocID", GetType(String))
        DocRptTbl.Columns.Add("DocDesc", GetType(String))


        DocRptTbl.Rows.Add("", "", "", "")
        Session("DocRptTbl") = DocRptTbl
        DgvDoc.DataSource = DocRptTbl
        DgvDoc.DataBind()

    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        Dim Dt As Date = IIf(TxtDisDate.Text = "", "01/01/1900", TxtDisDate.Text)
        Dim M_LoanApplicationBO As New SMARTCORE_BO.LoanApplicationBO
        With M_LoanApplicationBO

            .LmID = Val(HdnAccid.Value)
            .LoanType = "O" 'IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O")
            .BrCode = CmbBranch.SelectedValue
            .SchCode = CmbScheme.SelectedValue
            .LmApplSlno = "0"
            .LmLoanSchCode = CmbScheme.SelectedValue
            .CustId = HdnCustID.Value
            .ShrAccid = Val(HdnAccid.Value)
            .LmLoanAmount = Val(TxtLoanAmount.Text)
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
            .SiteValuate = Val(TxtPropertyValue.Text)
            .SiteDate = Dt 'Format(Dt, "MM/dd/yyyy")
            .BoardApproval = "0"
            .BoradDate = "01/01/1900"
            .SanctionLetter = "0"
            .SLetterDate = "01/01/1900"
            .Disbursement = TxtDisbursement.Text
            .DisburseDate = Dt 'Format(Dt, "MM/dd/yyyy")
            .LogID = "0"
            .MakerID = Session("UserID")
            .MakingTime = Date.Now



        End With

        Dim Docdt As DataTable = DirectCast(Session("DocRptTbl"), DataTable)
        Dim M_DocSr As New IO.StringWriter
        Docdt.WriteXml(M_DocSr)


        Dim M_LoanApplicationBL As New LoanApplicationBL
        Dim M_Dt As DataTable
        Session("M_EntryMode") = "DISBURSAL"
        M_Dt = M_LoanApplicationBL.Save_Data(M_LoanApplicationBO, Session("M_EntryMode"), "", "", "", M_DocSr.ToString.Replace("DocumentElement", "NewDataSet"))
        ShowAlertMessage(" Updated Successfully")
        Call ClearMemberDetails()

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

    'Protected Sub CmbLoanAppType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanAppType.SelectedIndexChanged
    '    GetsecLaNo()

    'End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Call ClearMemberDetails()
        Set_SchemeCtrl()
    End Sub

    Protected Sub CmbApplicationNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbApplicationNo.SelectedIndexChanged
        Dim Dt_AcDtlsN As DataTable
        Dim M_CommonBL As New CommonBL
        Dim dt As New DataTable
        Dim Dt_AccStatus As DataTable
        Dim M_loanAppBL As New LoanApplicationBL
        'Dt_AcDtlsN = M_CommonBL.GetLoanApplicationDtls(CmbBranch.SelectedValue, CmbApplicationNo.SelectedValue, IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O"), Session("TransDate"))
        Dt_AcDtlsN = M_CommonBL.GetLoanApplicationDtls(CmbBranch.SelectedValue, CmbApplicationNo.SelectedValue, "S", Session("TransDate"))
        If Dt_AcDtlsN.Rows.Count > 0 Then
            'Dt_AccStatus = M_loanAppBL.CheckPIFStatus(CmbApplicationNo.SelectedValue)
            'If Dt_AccStatus.Rows.Count = 0 Then
            '    Call ClearMemberDetails()
            '    ShowAlertMessage("Property Inspection Fee is already Entered ")
            '    M_loanAppBL = Nothing
            '    Exit Sub
            'End If


            HdnCustID.Value = Dt_AcDtlsN.Rows(0)("CustID").ToString()
            LocateCustomer(HdnCustID.Value)


            '' '' Session("CustomerId") = TxtCustID.Text
            ' ''TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
            ' ''TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
            ' ''TxtAmsam.Text = Dt_AcDtlsN.Rows(0)("Cust_Amsam").ToString()
            ' ''TxtDesam.Text = Dt_AcDtlsN.Rows(0)("Cust_Desam").ToString()
            ' ''CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
            HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Lm_ID").ToString()
            ' ''TxtMemberno.Text = Dt_AcDtlsN.Rows(0)("Acc_No").ToString() 'Dt_AcDtlsN.Rows(0)("MemberNo").ToString()
            ' ''CmbDirector.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_Director").ToString()
            CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
            TxtLoanAmount.Text = Dt_AcDtlsN.Rows(0)("Lm_LoanAmount").ToString()
            TxtLoanDate.Text = Dt_AcDtlsN.Rows(0)("Lm_LoanDate").ToString()
            TxtPropertyValue.Text = Dt_AcDtlsN.Rows(0)("Site_Valuate").ToString()
            TxtPifDate.Text = Dt_AcDtlsN.Rows(0)("Site_Date").ToString()
            '' '' CmbLoanAppType.SelectedValue = IIF(Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()=
            '' ''Lm_LoanSchCode()
            TxtPifDate.Focus()
        Else
            ShowAlertMessage("Invalid Loan Application Number")

        End If

        M_CommonBL = Nothing

    End Sub

    Private Sub GetsecLaNo()
        Dim M_LoanApplicationBL As New LoanApplicationBL
        M_LoanApplicationBL.GetDisbursalLaNo("S", CmbApplicationNo, CmbBranch.SelectedValue)
        CmbApplicationNo.Items.Insert("0", "SELECT")
        CmbApplicationNo.SelectedIndex = "0"
    End Sub
    Private Sub LocateCustomer(ByVal Cust_ID As String)
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

    Protected Sub DgvDoc_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvDoc.RowDeleting
        Try
            Dim DocCode As String = CInt(DgvDoc.DataKeys(e.RowIndex).Value)
            DeleteDocID(DocCode)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub PrintDocument(ByVal LaNo As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""


        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        M_Sql = "Exec getLoanApplicationDetailsRpt_Sp '" & LaNo & "'"
        M_DBConn.ExecuteScalar(M_Sql, , False)

        M_Sql = "Select * FROM result_app /*Where Tran_No='" & LaNo & "'*/"
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)


        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & CmbBranch.SelectedItem.ToString & "|Heading|  " & CmbBranch.SelectedItem.ToString & "|User_ID|" & Session("UserID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/LoanDocuments.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        ' Response.Redirect("~/RptPreview.aspx", False)

    End Sub

    Protected Sub ImgDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgDoc.Click
        If CmbApplicationNo.SelectedItem.Text <> "SELECT" Then
            PrintDocument(CmbApplicationNo.SelectedValue)
        End If
    End Sub
End Class
