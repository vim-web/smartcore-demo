Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class LoanSecLandAndBuilding
    Inherits System.Web.UI.Page
    Private EntryMode As String
    Private M_CustId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("M_EntryMode") = "NEW"
            Session("SearchType") = "ACCOUNTNO"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            TxtTaxDate.Enabled = False
            TxtTaxDetails.Enabled = False
            TxtTaxNo.Enabled = False
            TxtInsuranceDate.Enabled = False
            TxtInsuranceNo.Enabled = False
            TxtDueDate.Enabled = False
        Else
            If Session("SearchResult") <> "" Then
                TxtAccountNo.Text = Session("SearchResult")
                Dim Dt As New DataTable

                Dim GDr As GridViewRow = DirectCast(Session("SearchDataRow"), GridViewRow)
                lblName.Text = GDr.Cells(2).Text
                lblSchCode.Text = GDr.Cells(7).Text
                lblSchName.Text = GDr.Cells(4).Text
                lblAccId.Text = GDr.Cells(6).Text



                'Dt.Rows.Add(DirectCast(Session("SearchDataRow"), DataRow))


                ' GridView1.DataSource = Dt
                'GridView1.DataBind()

                'TxtDeedNo.Text = Dt.Rows(2)(2).ToString()
                'Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
                'Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
                'Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO

                'Dim M_CustMasterBL As New CustMasterBL
                'M_CustMaster = M_CustMasterBL.Locate_Data(TxtCustID.Text, M_PresAddress, M_PermAddress)
                'Locate_Data(M_CustMaster, M_PresAddress, M_PermAddress)
                'M_CustMasterBL = Nothing
                'TxtCustID.Focus()
                'Session("SearchResult") = ""
            End If
        End If

    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        If Validate_Control() = False Then
            Exit Sub
        End If
        Dim M_LandAndBuildingBO As New SMARTCORE_BO.LoanSecurityLandBuildingBO
        With M_LandAndBuildingBO
            .AccID = lblAccId.Text
            .SchCode = lblSchCode.Text
            .AccNo = TxtAccountNo.Text
            .BrCode = Session("DefaultBrCode").ToString
            .LsId = 0
            .LsDeedNo = TxtDeedNo.Text
            .LsDeedDate = TxtDeedDate.Text
            .LsPriorDeedNo = TxtPriorNo.Text
            .LsDistrict = TxtDistrict.Text
            .LsTaluk = TxtTehsil.Text
            .LsPanchayath = TxtPunchMun.Text
            .LsVillage = TxtVillage.Text
            .LsSubRegOffice = TxtSubRegOffice.Text
            .LsExtend = TxtExtend.Text
            .LsMarketValue = Val(TxtMarketValue.Text)
            .LsTaxPaid = IIf(ChkTax.Checked = True, "Y", "N")
            .LsTaxNo = TxtTaxNo.Text
            .LsTaxDate = TxtTaxDate.Text
            .LsTaxDetails = TxtTaxDetails.Text
            .LsInsurance = IIf(ChkInsurance.Checked = True, "Y", "N")
            .LsInsuranceNo = TxtInsuranceNo.Text
            .LsInsuranceDate = TxtInsuranceDate.Text
            .LsInsuranceDueDate = TxtInsuranceDate.Text
            .LsModeofownership = IIf(DdlModeOfOwnerdhip.SelectedIndex = 0, "S", "M")
            .LsOtherDetails = TxtOtherDetails.Text
            .SubAccNo = "0"
            .LsResurveyNo = TxtRecoveryNo.Text
        End With


        Try
            Dim M_LandAndBuildingBL As New SMARTCORE_BL.LoanSecurityLandBuildingBL
            M_CustId = M_LandAndBuildingBL.Save_Data(M_LandAndBuildingBO, Session("M_EntryMode")) ', M_PresAddress, M_PermAddress, M_PhotoIdentity, M_AddIdentity, Session("M_EntryMode"))
            MsgBox("Saved Succesfully : " & M_CustId)
            M_LandAndBuildingBL = Nothing
            'M_LandAndBuildingBO = Nothing
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub ImgSrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSrch.Click

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



    Private Function Validate_Control() As Boolean
        If lblAccId.Text.Length = 0 Then
            ShowAlertMessage("SELECT ACCOUNT NN.")
            Return False
        End If
        If lblSchCode.Text.Length = 0 Then
            ShowAlertMessage("SELECT ACCOUNT NN.")
            Return False
        End If
        If TxtDeedNo.Text.Length = 0 Then
            TxtDeedNo.Text = ""
        End If
        If TxtDeedDate.Text.Length = 0 Then
            TxtDeedDate.Text = "01/01/1900"
        End If
        If TxtPriorNo.Text.Length = 0 Then
            TxtPriorNo.Text = ""
        End If
        If TxtDistrict.Text.Length = 0 Then
            TxtDistrict.Text = ""
        End If
        If TxtTehsil.Text.Length = 0 Then
            TxtTehsil.Text = ""
        End If
        If TxtPunchMun.Text.Length = 0 Then
            TxtPunchMun.Text = ""
        End If
        If TxtVillage.Text.Length = 0 Then
            TxtVillage.Text = ""
        End If
        If TxtSubRegOffice.Text.Length = 0 Then
            TxtSubRegOffice.Text = ""
        End If
        If TxtExtend.Text.Length = 0 Then
            TxtExtend.Text = ""
        End If
        If TxtTaxNo.Text.Length = 0 Then
            TxtTaxNo.Text = ""
        End If
        If TxtTaxDate.Text.Length = 0 Then
            TxtTaxDate.Text = "01/01/1900"
        End If
        If TxtTaxDetails.Text.Length = 0 Then
            TxtTaxDetails.Text = ""
        End If
        If TxtInsuranceDate.Text.Length = 0 Then
            TxtInsuranceDate.Text = "01/01/1900"
        End If
        If TxtOtherDetails.Text.Length = 0 Then
            TxtOtherDetails.Text = ""
        End If
        If TxtInsuranceNo.Text.Length = 0 Then
            TxtInsuranceNo.Text = ""
        End If
        If TxtRecoveryNo.Text.Length = 0 Then
            TxtRecoveryNo.Text = ""
        End If

        Return True
    End Function
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub TxtAccountNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccountNo.TextChanged
        Dim M_Query As String
        M_Query = "Select TOP(200) Acc_No,Cust_Name,Cust_Id,SCH_NAME,ADDRESS,ACC_ID,Sch_Code from Account_Detail_Vw Where Acc_No = '" & TxtAccountNo.Text & "'"
        Dim M_SearchBL As New SearchBL
        Dim M_Dt As DataTable
        M_Dt = M_SearchBL.ExecuteQuery(M_Query)
        M_SearchBL = Nothing
        If M_Dt.Rows.Count >= 1 Then
            lblName.Text = M_Dt.Rows(0)(1).ToString
            lblSchCode.Text = M_Dt.Rows(0)(6).ToString 'GDr.Cells(7).Text
            lblSchName.Text = M_Dt.Rows(0)(3).ToString 'GDr.Cells(4).Text
            lblAccId.Text = M_Dt.Rows(0)(5).ToString 'GDr.Cells(6).Text '
        End If


    End Sub
End Class