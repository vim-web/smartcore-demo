
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class LoanCreditApproval
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

            'TxtPifDate.Attributes.Add("readonly", "readonly")
            Load_DefaultValues()
            ' Set_Grid()
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        'M_CommonBL.Fill_Directors(CmbDirector)
        'M_CommonBL.Fill_Pickup(CmbDocument, 99, 0)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"), "GEN.LOAN','OD")
        ' M_CommonBL.GetSchemes(CmbLoanAppType, Session("TransDate"), ("LOAN','LOAN"), "LF','LS")
        '   TxtPifDate.Attributes.Add("readonly", "readonly")

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
        TxtLoanDate.Text = ""
        TxtPropertyValue.Text = ""
        TxtPifDate.Text = ""



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
        'Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
        M_CustMasterBL = Nothing

    End Sub


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

    Protected Sub Set_Grid()

        'DgvDoc.DataSource = Nothing
        'DocRptTbl.Columns.Add("ACCID", GetType(String))
        'DocRptTbl.Columns.Add("LANO", GetType(String))
        'DocRptTbl.Columns.Add("DocID", GetType(String))
        'DocRptTbl.Columns.Add("DocDesc", GetType(String))


        'DocRptTbl.Rows.Add("", "", "", "")
        'Session("DocRptTbl") = DocRptTbl
        'DgvDoc.DataSource = DocRptTbl
        'DgvDoc.DataBind()

    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        ' Dim Dt As Date = IIf(TxtPifDate.Text = "", "01/01/1900", TxtPifDate.Text)
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
            .LmLoanAmount = "0" ' Val(TxtLoanAmount.Text)
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
            .SiteValuate = "0" 'Val(TxtPropertyValue.Text)
            .SiteDate = "01/01/1900" 'Format(Dt, "MM/dd/yyyy")
            .BoardApproval = "0"
            .BoradDate = "01/01/1900"
            .SanctionLetter = "0"
            .SLetterDate = "01/01/1900"
            .Disbursement = "0"
            .DisburseDate = "01/01/1900"
            .LogID = "0"
            .MakerID = Session("UserID")
            .MakingTime = Date.Now
            .CrApprovedStatus = CmbCreditStatus.SelectedValue
            .CrApprovedUser = Session("UserID")




        End With

        'Dim Docdt As DataTable = DirectCast(Session("DocRptTbl"), DataTable)
        'Dim M_DocSr As New IO.StringWriter
        'Docdt.WriteXml(M_DocSr)


        Dim M_LoanApplicationBL As New LoanApplicationBL
        Dim M_Dt As DataTable
        Session("M_EntryMode") = "CREDIT APPROVAL"
        M_Dt = M_LoanApplicationBL.Save_Data(M_LoanApplicationBO, Session("M_EntryMode"), "", "", "", "")
        ShowAlertMessage("Credit Status Updated Successfully")
        GetsecLaNo()
        Call ClearMemberDetails()

    End Sub

  


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
            ' TxtPifDate.Focus()
        Else
            ShowAlertMessage("Invalid Application Number")

        End If

        M_CommonBL = Nothing

    End Sub

    Private Sub GetsecLaNo()
        Dim M_LoanApplicationBL As New LoanApplicationBL
        M_LoanApplicationBL.GetCreditLaNo("S", CmbApplicationNo, CmbBranch.SelectedValue)
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
        'Response.Redirect("~/RptPreview.aspx", False)

    End Sub

    Protected Sub ImgDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgDoc.Click
        If CmbApplicationNo.SelectedItem.Text <> "SELECT" Then
            PrintDocument(CmbApplicationNo.SelectedValue)
        End If
    End Sub
End Class
