Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO.MemoryStream
Imports System.IO
Imports System.Web.Script.Services

Partial Public Class CustomerRegister
    Inherits System.Web.UI.Page
    Private EntryMode As String
    Private M_CustId As Integer
    Private BranchDt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If

            If Not IsPostBack Then
                Session("M_EntryMode") = "NEW"
                hid_CustID.Value = "NEW"
                Load_DefaultValues()
                TxtDoa.Text = Session("TransDate")
                DtpGrading.Text = Session("TransDate")
                TxtDoa.Attributes.Add("readonly", "readonly")

                CmbAdmBranch.Attributes.Add("readonly", "readonly")

                Session("SearchType") = "CUSTOMER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                ImgImage.Attributes.Add("OnClick", "window.open('CustImg.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                ImgSrch1.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

            End If
            If Session("SearchResult") <> "" Then
                If (TxtName.Text <> "" And TxtCustID.Text = "NEW") Then
                    If Session("SearchResult") <> "NEW" Then
                        TxtIntroID.Text = Session("SearchResult").ToString()
                        Call TxtIntroID_TextChanged(TxtIntroID, System.EventArgs.Empty)
                    End If
                    Session("SearchResult") = "NEW"
                ElseIf (TxtName.Text <> "" And TxtCustID.Text <> "NEW" And TxtIntroID.Text <> "NEW") Then
                    If TxtIntroID.Text.Length = 0 Then
                        TxtIntroID.Text = Session("SearchResult").ToString()
                        Call TxtIntroID_TextChanged(TxtIntroID, System.EventArgs.Empty)
                        Session("SearchResult") = "NEW"
                    End If
                Else
                    TxtCustID.Text = Session("SearchResult")
                    TxtCustID_TextChanged(Me.TxtCustID, System.EventArgs.Empty)
                    TxtCustID.Focus()
                    Session("SearchResult") = ""
                End If
            End If
            If Not Me.IsPostBack Then
                Session("Hexstring") = ""
                If Request.InputStream.Length > 0 Then
                    Using reader As New StreamReader(Request.InputStream)
                        Dim hexString As String = Server.UrlEncode(reader.ReadToEnd())
                        Session("Hexstring") = hexString
                        Dim imageName As String = Session("HostID").ToString() + DateTime.Now.ToString("dd-MM-yy hh-mm-ss")
                        Dim imagePath As String = String.Format("~/Captures/{0}.JPG", imageName)
                        File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString))
                        Session("CapturedImage") = ResolveUrl(imagePath)
                        Session("CapturedImageType") = "Captures/" + imageName + ".JPG"

                    End Using
                End If
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Shared Function ConvertHexToBytes(ByVal hex As String) As Byte()
        Dim bytes As Byte() = New Byte(hex.Length / 2 - 1) {}
        For i As Integer = 0 To hex.Length - 1 Step 2
            bytes(i / 2) = Convert.ToByte(hex.Substring(i, 2), 16)
        Next
        Return bytes
    End Function
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Pickup(cmbCustomerType, 6, 0)
            M_CommonBL.Fill_Pickup(CmbTitle, 5, 0)
            M_CommonBL.Fill_Pickup(CmbRelType, 9, 0)
            M_CommonBL.Fill_Pickup(CmbOccupation, 4, 0)
            M_CommonBL.Fill_Pickup(CmbEducation, 13, 0)
            M_CommonBL.Fill_Pickup(CmbMaritialStatus, 10, 0)
            M_CommonBL.Fill_Pickup(CmbGender, 8, 0)
            M_CommonBL.Fill_Pickup(CmbReligion, 1, 0)
            M_CommonBL.Fill_Pickup(CmbMainCaste, 2, 0)
            'M_CommonBL.Fill_Pickup(CmbSubCaste, 2, 0)
            M_CommonBL.Fill_Pickup(CmbBloodGrp, 11, 0)
            M_CommonBL.Fill_Pickup(CmbGrading, 26, 0)
            M_CommonBL.Fill_Pickup(CmbNationality, 12, 0)
            M_CommonBL.Fill_Pickup(CmbIncomeRange, 3, 0)
            M_CommonBL.Fill_Pickup(CmbNRI, 1, 0)
            M_CommonBL.Fill_Pickup(CmbITDoc, 15, 0)
            M_CommonBL.Fill_Pickup(CmbCurrency, 23, 0)
            M_CommonBL.Fill_Pickup(CmbDisability, 24, 0)
            M_CommonBL.Fill_Pickup(CmbNRI, 29, 0)
            M_CommonBL.Fill_Pickup(CmbPhotoIDProof, 18, 0)
            M_CommonBL.Fill_Pickup(CmbAddProof, 18, 0)
            M_CommonBL.Fill_Pickup(CmbGuardianRel, 9, 0)
            M_CommonBL.Fill_Branch(CmbAdmBranch)
            M_CommonBL.Fill_Pickup(CmbCustomerCatg, 7, cmbCustomerType.SelectedValue)
            M_CommonBL.Fill_Pickup(CmbState, 48, 0)
            M_CommonBL.Fill_Pickup(CmbDist, 103, CmbState.SelectedValue)
            M_CommonBL.Fill_Loacation(CmbLocation, 14, CmbDist.SelectedValue)
            TxtCustID.Text = "NEW"
            TxtPermDist.Text = Session("DefDist")
            TxtPermState.Text = Session("DefState")
            TxtPermCountry.Text = Session("DefCountry")

            TxtPresDist.Text = Session("DefDist")
            TxtPresState.Text = Session("DefState")
            TxtPresCountry.Text = Session("DefCountry")
            CmbAdmBranch.SelectedValue = Session("DefaultBrCode")
            M_CommonBL.Fill_SangalDetails(CmbGroup, CmbAdmBranch.SelectedValue)
            BranchDt = M_CommonBL.Fill_BranchDtls(CmbAdmBranch.SelectedValue)
            CmbSubCaste.Items.Add("APL")
            CmbSubCaste.Items.Add("BPL")

            ''Dim M_Dt As New DataTable
            ''M_Dt = M_CommonBL.Get_District()
            ''CmbDist.DataTextField = "pkc_Description"
            ''CmbDist.DataValueField = "pkc_ParentId"
            ''CmbDist.DataSource = M_Dt
            ''CmbDist.DataBind()
            ' ''M_Dt = Nothing

            ''Dim M_Datatable As New DataTable
            ''M_Datatable = M_CommonBL.LocationDtls(CmbDist.SelectedValue)
            ''CmbLocation.DataTextField = "PostOf"
            ''CmbLocation.DataValueField = "PostOff_Id"
            ''CmbLocation.DataSource = M_Datatable
            ''CmbLocation.DataBind()
            M_CommonBL = Nothing
            Session("SearchBranch") = ""
            SetGroupDiv()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Fill_Pickup(ByRef Cmbbx As Object, ByVal M_PkcType As Integer, ByVal M_PkcParentID As Integer)
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Pickup(Cmbbx, M_PkcType, M_PkcParentID)
        M_CommonBL = Nothing
    End Sub

    Protected Sub cmbCustomerType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCustomerType.SelectedIndexChanged
        Fill_Pickup(CmbCustomerCatg, 7, cmbCustomerType.SelectedValue)
    End Sub

    Protected Sub CmbReligion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbReligion.SelectedIndexChanged
        Fill_Pickup(CmbMainCaste, 2, CmbReligion.SelectedValue)
    End Sub




    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Private Sub Clear_Controls()
        DivModification.Visible = False
        TxtCustID.Text = ""
        TxtAge.Text = ""
        TxtIntroNm.Text = ""
        cmbCustomerType.SelectedIndex = 0
        CmbCustomerCatg.SelectedIndex = 0
        CmbTitle.SelectedIndex = 0
        CmbGender.SelectedIndex = 0
        CmbRelType.SelectedIndex = 0
        CmbGrading.SelectedIndex = 0
        CmbDisability.SelectedIndex = 0
        CmbCurrency.SelectedIndex = 0
        CmbNationality.SelectedIndex = 0
        CmbMaritialStatus.SelectedIndex = 0
        CmbIncomeRange.SelectedIndex = 0
        CmbITDoc.SelectedIndex = 0
        CmbLocation.SelectedIndex = 0
        CmbGuardianRel.SelectedIndex = 0
        CmbReligion.SelectedIndex = 0
        CmbReligion_SelectedIndexChanged(Me.CmbReligion, System.EventArgs.Empty)
        CmbMainCaste.SelectedIndex = 0
        CmbOccupation.SelectedIndex = 0
        CmbEducation.SelectedIndex = 0
        TxtDob.Text = ""
        TxtDoa.Text = Session("TransDate")
        DtpGrading.Text = Session("TransDate")
        CmbAdmBranch.SelectedValue = Session("DefaultBrCode")
        TxtName.Text = ""
        TxtMobile.Text = ""
        TxtPhotoIDNoProof.Text = ""
        TxtAdhaarNo.Text = ""
        TxtIdentityMark.Text = ""
        CmbBloodGrp.SelectedIndex = 0
        TxtWebAdd.Text = ""
        TxtFax.Text = ""
        TxtEmail.Text = ""
        TxtLandPhone.Text = ""
        TxtRelName.Text = ""
        TxtGuardianID.Text = ""
        TxtIntroID.Text = ""
        TxtRemarks.Text = ""
        TxtLetterNO.Text = ""
        TxtLetterDate.Text = ""
        TxtChangeReason.Text = ""
        TxtPresHouseName.Text = ""
        TxtPresPlace.Text = ""
        TxtPresState.Text = ""
        TxtPresLand.Text = ""
        TxtPresPin.Text = ""
        TxtPresHouseNo.Text = ""
        TxtPresDist.Text = ""
        TxtPresCountry.Text = ""
        TxtPrestaluk.Text = ""
        txtPresvillage.Text = ""
        TxtPermHouseName.Text = ""
        TxtPermPlace.Text = ""
        TxtPermState.Text = ""
        TxtPermLand.Text = ""
        TxtPermPin.Text = ""
        TxtPermHouseNo.Text = ""
        TxtPermDist.Text = ""
        TxtPermCountry.Text = ""
        Hid_ODPStatus.Value = "N"
        TxtPermtaluk.Text = ""
        TxtPermvillage.Text = ""
        TxtAddIDProof.Text = ""
        TxtPhotoIDProof.Text = ""
        TxtPrempancynt.Text = ""
        TxtPremward.Text = ""

        TxtPrespancynt.Text = ""
        TxtPresward.Text = ""
        TxtMotherName.Text = ""
        SetGroupDiv()
        TxtMobile.Text = ""
        TxtMobile.Enabled = False
        ImgEditNo.Attributes.Add("style", "Visibility:False")
        TxtLastModiDate.Text = ""
    End Sub



    Protected Sub TxtCustID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustID.TextChanged
        Dim CustId As String = TxtCustID.Text
        Call Clear_Controls()


        TxtCustID.Text = CustId
        If TxtCustID.Text.Length = 0 Then
            TxtCustID.Text = "NEW"
            Session("M_EntryMode") = "NEW"
            hid_CustID.Value = "NEW"
        Else
            Try


                Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
                Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
                Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
                Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
                Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
                Dim M_CustMasterBL As New CustMasterBL




                M_CustMaster = M_CustMasterBL.Locate_Data(TxtCustID.Text, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
                If M_CustMaster.CustId > 0 Then

                    Dim M_Dt As New DataTable
                    Dim M_CommonBL As New CommonBL
                    M_Dt = M_CommonBL.LocationDtls(CustId)
                    If (M_Dt.Rows.Count > 0) Then
                        CmbState.SelectedValue = M_Dt.Rows(0).Item("State").ToString()
                        Call CmbState_SelectedIndexChanged(sender, e)
                        CmbDist.SelectedValue = M_Dt.Rows(0).Item("District").ToString()
                        Call CmbDist_SelectedIndexChanged(sender, e)
                    End If
                    Dim dtLogDet As New DataTable
                    dtLogDet = M_CustMasterBL.CustModifiedLogDet(CustId)
                    If (dtLogDet.Rows.Count > 0) Then
                        TxtLastModiDate.Text = dtLogDet.Rows(0).Item("ModDet").ToString()
                    End If
                    Session("ImgSignature") = ""
                    Session("ImgDocument") = ""
                    Session("Imgphoto") = ""

                    Locate_Data(M_CustMaster, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
                    TxtIntroNm.Text = M_CustMasterBL.Get_CustomerName(IIf(TxtIntroID.Text = "", 0, TxtIntroID.Text))
                    If M_CustMaster.IsCompleted = "Y" Then
                        DivModification.Visible = True
                    End If
                End If
                M_CustMasterBL = Nothing
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        End If
    End Sub
    Private Sub SetGroupDiv()
        If BranchDt.Rows.Count > 0 Then
            If BranchDt.Rows(0)("Br_CustGrpNeeded").ToString = "N" Then
                DivGroup.Visible = False
            Else
                DivGroup.Visible = True
            End If
        End If

    End Sub




    Private Sub Locate_Data(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO, Optional ByVal M_PhotoID As SMARTCORE_BO.CustIdentityBO = Nothing, Optional ByVal M_ADDID As SMARTCORE_BO.CustIdentityBO = Nothing)
        On Error Resume Next

        If M_CustMaster.CustId = 0 Then
            Session("M_EntryMode") = "NEW"
            TxtCustID.Text = "NEW"
            hid_CustID.Value = "NEW"
            Exit Sub
        End If

        Dim M_Dt As New DataTable
        Dim M_CommonBL As New CommonBL
        M_Dt = M_CommonBL.LocationDtls(TxtCustID.Text)

        With M_CustMaster
            Session("CustomerId") = M_CustMaster.CustId
            TxtAge.Text = .CustAge
            cmbCustomerType.SelectedValue = .CustTypeCode
            Fill_Pickup(CmbCustomerCatg, 7, cmbCustomerType.SelectedValue)
            CmbTitle.SelectedValue = .CustTitleCode
            CmbGender.SelectedValue = .CustGender
            CmbRelType.SelectedValue = .CustRelTypCode
            CmbGrading.SelectedValue = .CustGradeCode
            CmbDisability.SelectedValue = .CustDisabilityCode
            CmbCurrency.SelectedValue = .CustCurrencyCode
            '=.InstitutionCode
            CmbNationality.SelectedValue = .CustNationalityCode
            CmbMaritialStatus.SelectedValue = .CustMaritalStatusCode
            CmbIncomeRange.SelectedValue = .CustIncomeRangeCode
            CmbITDoc.SelectedValue = .CustItDocumentCode
            CmbDist.SelectedValue = .CmbDistrict
            CmbLocation.SelectedValue = .CustLocationCode

            'CmbLocation.SelectedValue = M_Dt.Rows(0).Item("Post_Office")

            '=.CustGuardianRelationCode
            CmbReligion.SelectedValue = .CustReligionCode
            CmbReligion_SelectedIndexChanged(Me.CmbReligion, System.EventArgs.Empty)
            CmbMainCaste.SelectedValue = .CustCasteCode
            CmbOccupation.SelectedValue = .CustOccupationCode
            CmbEducation.SelectedValue = .CustEducationCode
            TxtDob.Text = .CustDobInc
            TxtDoa.Text = .CustRegDate
            DtpGrading.Text = .CustRatingDate
            '=.CustMinor
            '=.CustAuthorised
            CmbAdmBranch.SelectedValue = .BrCode
            TxtName.Text = .CustName
            '=.CustFirstName
            '=.CustMiddleName
            '=.CustLastName
            '=.CustShortName
            '=.CustPhone2
            TxtMobile.Text = .CustMobile
            TxtITDocNo.Text = .CustITDocRemarks
            '=.MakerID
            '=.CheckerID

            TxtAdhaarNo.Text = .CustAdhaarNo
            TxtIdentityMark.Text = .CustIdentificationMark
            CmbBloodGrp.SelectedValue = .CustBloodGroup
            TxtWebAdd.Text = .CustWebsite
            TxtFax.Text = .CustFax
            TxtEmail.Text = .CustEmail
            TxtLandPhone.Text = .CustPhone1
            TxtRelName.Text = .CustRelNm
            '=.CustGuardianId
            TxtIntroID.Text = .CustIntroCustId
            TxtRemarks.Text = .CustRemarks
            CmbCustomerCatg.SelectedValue = .CustCatgCode
            TxtMotherName.Text = .Cust_MotherName
            '=.CustIslanderCardNo
            '=.CustLocalCertNo
            '=.CustStaff
            '=.CustStatus
            '=.IsCompleted
            Dim Status As String
            If .CustStatus = "A" Or .CustStatus = "Y" Then
                CmbStatus.SelectedIndex = 0
            ElseIf .CustStatus = "I" Then
                CmbStatus.SelectedIndex = 1
            Else
                CmbStatus.SelectedIndex = 2
            End If
            CmbGroup.SelectedValue = .CusGrpID

        End With
        TxtPresHouseName.Text = M_PresAddress.AddAddress1
        TxtPresPlace.Text = M_PresAddress.AddPostoffice
        TxtPresState.Text = M_PresAddress.AddAddress3
        TxtPresLand.Text = M_PresAddress.AddAddress2
        TxtPresPin.Text = M_PresAddress.AddPinCode
        TxtPresHouseNo.Text = M_PresAddress.AddHouseNo
        TxtPresDist.Text = M_PresAddress.AddDist
        TxtPresCountry.Text = M_PresAddress.AddCountry
        txtPresvillage.Text = M_PresAddress.Village
        TxtPrestaluk.Text = M_PresAddress.Taluk
        TxtPrespancynt.Text = M_PresAddress.Add_Panchayat
        TxtPresward.Text = M_PresAddress.Add_Ward


        TxtPermHouseName.Text = M_PermAddress.AddAddress1
        TxtPermPlace.Text = M_PermAddress.AddPostoffice
        TxtPermState.Text = M_PermAddress.AddAddress3
        TxtPermLand.Text = M_PermAddress.AddAddress2
        TxtPermPin.Text = M_PermAddress.AddPinCode
        TxtPermHouseNo.Text = M_PermAddress.AddHouseNo
        TxtPermDist.Text = M_PermAddress.AddDist
        TxtPermCountry.Text = M_PermAddress.AddCountry
        TxtPermtaluk.Text = M_PermAddress.Taluk
        TxtPermvillage.Text = M_PermAddress.Village
        TxtPrempancynt.Text = M_PermAddress.Add_Panchayat
        TxtPremward.Text = M_PermAddress.Add_Ward


        CmbPhotoIDProof.SelectedValue = M_PhotoID.CidntCode
        TxtPhotoIdentityDtls.Text = M_PhotoID.Cidntdetails
        radio_PhotoProof.SelectedValue = M_PhotoID.IDproopType
        If CommonClass.ChkDate(M_PhotoID.CidntIssueDate) = False Then
            TxtPhotoIssDate.Text = "01/01/1900"
        Else
            TxtPhotoIssDate.Text = M_PhotoID.CidntIssueDate
        End If
        If CommonClass.ChkDate(M_PhotoID.CidntExpiryDate) = False Then
            TxtPhotoExpiryDate.Text = "01/01/1900"
        Else
            TxtPhotoExpiryDate.Text = M_PhotoID.CidntExpiryDate
        End If
        TxtPhotoIssAuth.Text = M_PhotoID.CidntIssueAuthority
        TxtPhotoIDNoProof.Text = M_PhotoID.CidntNo
        TxtPhotoIDProof.Text = M_PhotoID.CidntNo

        CmbAddProof.SelectedValue = M_ADDID.CidntCode
        TxtAddIdentittyDtls.Text = M_ADDID.Cidntdetails
        RadioIDproofType.SelectedValue = M_ADDID.IDproopType
        If CommonClass.ChkDate(M_ADDID.CidntIssueDate) = False Then
            TxtAddIssDate.Text = "01/01/1900"
        Else
            TxtAddIssDate.Text = M_ADDID.CidntIssueDate
        End If
        If CommonClass.ChkDate(M_ADDID.CidntExpiryDate) = False Then
            TxtAddExpiryDate.Text = "01/01/1900"
        Else
            TxtAddExpiryDate.Text = M_ADDID.CidntExpiryDate
        End If
        TxtAddIssAuth.Text = M_ADDID.CidntIssueAuthority
        TxtAddProofNo.Text = M_ADDID.CidntNo
        TxtAddIDProof.Text = M_ADDID.CidntNo
        Session("M_EntryMode") = "VIEW"
        hid_CustID.Value = "VIEW"

        Exit Sub
ErrHndlr:


    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Public Shared Sub ErrorMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alertify.error('" & [error] & "');", True)
        End If
    End Sub

    Private Function Validate_Control() As Boolean
        If Len(Trim(TxtName.Text)) = 0 Then
            TxtName.Focus()
            ErrorMessage("INVALID CUSTOMER NAME ENTERED")
            Return False
        End If
        If Len(Trim(TxtDob.Text)) = 0 Then
            TxtDob.Focus()
            ErrorMessage("INVALID CUSTOMER DATE OF BIRTH")
            Return False
        End If
        If CmbReligion.SelectedValue = "137" Then
            TxtPermPlace.Focus()
            ErrorMessage("INVALID CUSTOMER RELIGION")
            Return False
        End If
        If CmbMainCaste.SelectedValue = "" Or CmbReligion.SelectedValue = "19" Then
            TxtPermDist.Focus()
            ErrorMessage("INVALID CUSTOMER CASTE")
            Return False
        End If
        If Len(Trim(TxtIntroNm.Text)) = 0 Then
            TxtIntroNm.Focus()
            ErrorMessage("INVALID CUSTOMER INTRODUCED ")
            Return False
        End If
        If Len(Trim(TxtPresHouseName.Text)) = 0 Then
            TxtPresHouseName.Focus()
            ErrorMessage("INVALID CUSTOMER PRESENT HOUSE NAME")
            Return False
        End If
        If Len(Trim(TxtPresPlace.Text)) = 0 Then
            TxtPresPlace.Focus()
            ErrorMessage("INVALID CUSTOMER PRESENT POST OFFICE")
            Return False
        End If
        If Len(Trim(txtPresvillage.Text)) = 0 Then
            txtPresvillage.Focus()
            ErrorMessage("INVALID CUSTOMER PRESENT VILLAGE")
            Return False
        End If
        If Len(Trim(TxtPrestaluk.Text)) = 0 Then
            TxtPrestaluk.Focus()
            ErrorMessage("INVALID CUSTOMER PRESENT TALUK")
            Return False
        End If
        If Len(Trim(TxtPresDist.Text)) = 0 Then
            TxtPresDist.Focus()
            ErrorMessage("INVALID CUSTOMER PRESENT DISTRICT")
            Return False
        End If
        If Len(Trim(TxtPermHouseName.Text)) = 0 Then
            TxtPermHouseName.Focus()
            ErrorMessage("INVALID CUSTOMER PERMANENT HOUSE NAME")
            Return False
        End If

        If Len(Trim(TxtPermPlace.Text)) = 0 Then
            TxtPermPlace.Focus()
            ErrorMessage("INVALID CUSTOMER PERMANENT POST OFFICE")
            Return False
        End If
        If Len(Trim(TxtPermvillage.Text)) = 0 Then
            TxtPermvillage.Focus()
            ErrorMessage("INVALID CUSTOMER PERMANENT VILLAGE")
            Return False
        End If
        If Len(Trim(TxtPermtaluk.Text)) = 0 Then
            TxtPermtaluk.Focus()
            ErrorMessage("INVALID CUSTOMER PERMANENT TALUK")
            Return False
        End If
        If Len(Trim(TxtPermDist.Text)) = 0 Then
            TxtPermDist.Focus()
            ErrorMessage("INVALID CUSTOMER PERMANENT DISTRICT")
            Return False
        End If
        If hid_CustID.Value = "NEW" Then
            Dim CustBl As New CustMasterBL
            Dim dtMobNoExist As New DataTable
            dtMobNoExist = CustBl.ValidateMobNoExist(TxtMobile.Text)

            If dtMobNoExist.Rows(0).Item("result") = "Y" Then
                TxtMobile.Focus()
                ErrorMessage("Mobile No Already Exist")
                Return False
            End If
            If TxtAdhaarNo.Text <> "" Then
                Dim dtAhrExist As New DataTable
                dtAhrExist = CustBl.ValidateAdharNoExist(TxtAdhaarNo.Text)

                If dtAhrExist.Rows(0).Item("result") = "Y" Then
                    TxtAdhaarNo.Focus()
                    ErrorMessage("Adhar No Already Exist")
                    Return False
                End If
            End If
        End If

        If Hid_ODPStatus.Value = "N" Then
            TxtMobile.Focus()
            ErrorMessage("VERIFY CUSTOMER MOBILE NO")
            Return False
        End If
        If Len(Trim(TxtPhotoIDProof.Text)) = 0 Then
            TxtPhotoIDProof.Focus()
            ErrorMessage("VERIFY CUSTOMER PHOTO ID PROOF")
            Return False
        End If
        If Len(Trim(TxtPresPin.Text)) = 0 Then
            TxtPresPin.Focus()
            ErrorMessage("VERIFY CUSTOMER PIN CODE")
            Return False
        End If
        If Len(Trim(TxtPermPin.Text)) = 0 Then
            TxtPermPin.Focus()
            ErrorMessage("VERIFY CUSTOMER PIN CODE")
            Return False
        End If

        If DivModification.Visible = True Then
            If TxtLetterNO.Text.Trim.Length <= 0 Then
                ErrorMessage("Invalid Letted No")
                TxtLetterNO.Focus()
                Return False
            End If
            If TxtLetterDate.Text.Trim.Length <= 0 Then
                ErrorMessage("Invalid Letted Date")
                TxtLetterDate.Focus()
                Return False
            End If
            If TxtChangeReason.Text.Trim.Length <= 0 Then
                ErrorMessage("Invalid Modification Reason")
                TxtChangeReason.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim imageSize As New Byte()

        If Validate_Control() = False Then
            Exit Sub
        End If
        Dim Status As String = "A"
        If CmbStatus.SelectedItem.Text = "ACTIVE" Then
            Status = "A"
        ElseIf CmbStatus.SelectedItem.Text = "INACTIVE" Then
            Status = "I"
        ElseIf CmbStatus.SelectedItem.Text = "DEAD" Then
            Status = "D"
        End If

        Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
        With M_CustMaster

            .CustAge = Val(TxtAge.Text)
            .CustNoOfDependents = 0
            .CustId = Val(TxtCustID.Text)
            .CustTypeCode = cmbCustomerType.SelectedValue
            .CustCatgCode = CmbCustomerCatg.SelectedValue
            .CustTitleCode = CmbTitle.SelectedValue
            .CustGender = CmbGender.SelectedValue
            .CustRelTypCode = CmbRelType.SelectedValue
            .CustGradeCode = CmbGrading.SelectedValue
            .CustDisabilityCode = CmbDisability.SelectedValue
            .CustCurrencyCode = CmbCurrency.SelectedValue
            .LogID = 0
            .CustNationalityCode = CmbNationality.SelectedValue
            .CustMaritalStatusCode = CmbMaritialStatus.SelectedValue
            .CustDefAddTypeCode = 95
            .CustIncomeRangeCode = CmbIncomeRange.SelectedValue
            .CustItDocumentCode = CmbITDoc.SelectedValue
            .CustLocationCode = CmbLocation.SelectedValue
            .CustGuardianRelationCode = CmbGuardianRel.SelectedValue
            .CustReligionCode = CmbReligion.SelectedValue
            .CustCasteCode = CmbMainCaste.SelectedValue
            .CustOccupationCode = CmbOccupation.SelectedValue
            .CustEducationCode = CmbEducation.SelectedValue
            .CustStaffId = 0
            .CustDobInc = IIf(IsDate(TxtDob.Text), TxtDob.Text, "01/01/1900")
            .CustRegDate = TxtDoa.Text
            .CustRatingDate = DtpGrading.Text
            .MakingTime = Date.Now.ToString("dd/MM/yyyy")
            .CheckingTime = Date.Now.ToString("dd/MM/yyyy")
            .CustMinor = IIf(Val(TxtAge.Text) < 18, 1, 0)
            .CustDesignationCode = 1
            .CustPhone2TypeCode = 40
            .InstitutionCode = 0
            .CustHouseTypeCode = 467
            .CustResidentStatusCode = 226
            .CustInsuranceCode = 465
            .CustVehicleOwnedCode = 466
            .CustInvestmentCode = 464
            .CustPhone1TypeCode = 39
            .CustAuthorised = 1
            .BrCode = CmbAdmBranch.SelectedValue
            .CustName = TxtName.Text
            .CustFirstName = ""
            .CustMiddleName = ""
            .CustLastName = ""
            .CustShortName = ""
            .CustPhone2 = ""
            .CustMobile = TxtMobile.Text
            .CustITDocRemarks = TxtPhotoIDNoProof.Text
            .MakerID = Session("UserID")
            .CheckerID = ""
            .CustAdhaarNo = TxtAdhaarNo.Text
            .CustIdentificationMark = TxtIdentityMark.Text
            .CustBloodGroup = CmbBloodGrp.SelectedValue
            .CustWebsite = TxtWebAdd.Text
            .CustFax = TxtFax.Text
            .CustEmail = TxtEmail.Text
            .CustPhone1 = TxtLandPhone.Text
            .CustRelNm = TxtRelName.Text
            .CustGuardianId = TxtGuardianID.Text
            .CustIntroCustId = TxtIntroID.Text
            .CustRemarks = TxtRemarks.Text
            .CustIslanderCardNo = ""
            .CustLocalCertNo = ""
            .CustStaff = "N"
            .CustStatus = Status
            .IsCompleted = "Y"
            .LetterNo = TxtLetterNO.Text
            .LetterDate = IIf(TxtLetterDate.Text.Trim.Length <= 0, "01/01/1900", TxtLetterDate.Text.Trim)
            .ModifyReason = TxtChangeReason.Text
            .CusGrpID = IIf(CmbGroup.SelectedValue = "", 0, CmbGroup.SelectedValue)
            .Cust_MotherName = TxtMotherName.Text

        End With

        Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
        M_PresAddress.AddTypeId = 96
        M_PresAddress.AddAddress1 = TxtPresHouseName.Text
        M_PresAddress.AddAddress2 = TxtPresLand.Text
        M_PresAddress.AddAddress3 = TxtPresState.Text
        M_PresAddress.AddPostoffice = TxtPresPlace.Text
        M_PresAddress.AddPinCode = TxtPresPin.Text
        M_PresAddress.AddHouseNo = TxtPresHouseNo.Text
        M_PresAddress.AddDist = TxtPresDist.Text
        M_PresAddress.AddCountry = TxtPresCountry.Text
        M_PresAddress.BrCode = CmbAdmBranch.SelectedValue
        M_PresAddress.Taluk = TxtPrestaluk.Text.Trim()
        M_PresAddress.Village = txtPresvillage.Text.Trim()
        M_PresAddress.CustId = Val(TxtCustID.Text)
        M_PresAddress.Add_Panchayat = TxtPrespancynt.Text
        M_PresAddress.Add_Ward = TxtPresward.Text

        Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
        M_PermAddress.AddTypeId = 95
        M_PermAddress.AddAddress1 = TxtPermHouseName.Text
        M_PermAddress.AddAddress2 = TxtPermLand.Text
        M_PermAddress.AddAddress3 = TxtPermState.Text
        M_PermAddress.AddPostoffice = TxtPermPlace.Text
        M_PermAddress.AddPinCode = TxtPermPin.Text
        M_PermAddress.AddHouseNo = TxtPermHouseNo.Text
        M_PermAddress.AddDist = TxtPermDist.Text
        M_PermAddress.AddCountry = TxtPermCountry.Text
        M_PermAddress.BrCode = CmbAdmBranch.SelectedValue
        M_PermAddress.Village = TxtPermvillage.Text.Trim()
        M_PermAddress.Taluk = TxtPermtaluk.Text.Trim()
        M_PermAddress.CustId = Val(TxtCustID.Text)
        M_PermAddress.Add_Panchayat = TxtPrempancynt.Text
        M_PermAddress.Add_Ward = TxtPremward.Text

        Dim M_PhotoIdentity As New SMARTCORE_BO.CustIdentityBO
        M_PhotoIdentity.BrCode = CmbAdmBranch.SelectedValue
        M_PhotoIdentity.CidntCode = CmbPhotoIDProof.SelectedValue
        M_PhotoIdentity.Cidntdetails = TxtPhotoIdentityDtls.Text
        M_PhotoIdentity.IDproopType = radio_PhotoProof.SelectedValue
        If CommonClass.ChkDate(TxtPhotoIssDate.Text) = False Then
            M_PhotoIdentity.CidntIssueDate = "01/01/1900"
        Else
            M_PhotoIdentity.CidntIssueDate = TxtPhotoIssDate.Text
        End If

        If CommonClass.ChkDate(TxtPhotoExpiryDate.Text) = False Then
            M_PhotoIdentity.CidntExpiryDate = "01/01/1900"
        Else
            M_PhotoIdentity.CidntExpiryDate = TxtPhotoExpiryDate.Text
        End If
        M_PhotoIdentity.CidntIssueAuthority = TxtPhotoIssAuth.Text
        M_PhotoIdentity.CidntNo = TxtPhotoIDNoProof.Text
        M_PhotoIdentity.CustId = 0

        Dim M_AddIdentity As New SMARTCORE_BO.CustIdentityBO
        M_AddIdentity.BrCode = CmbAdmBranch.SelectedValue
        M_AddIdentity.CidntCode = Val(CmbAddProof.SelectedValue)
        M_AddIdentity.Cidntdetails = TxtAddIdentittyDtls.Text
        M_AddIdentity.IDproopType = RadioIDproofType.SelectedValue
        If CommonClass.ChkDate(TxtAddIssDate.Text) = False Then
            M_AddIdentity.CidntIssueDate = "01/01/1900"
        Else
            M_AddIdentity.CidntIssueDate = TxtAddIssDate.Text
        End If
        If CommonClass.ChkDate(TxtAddExpiryDate.Text) = False Then
            M_AddIdentity.CidntExpiryDate = "01/01/1900"
        Else
            M_AddIdentity.CidntExpiryDate = TxtAddExpiryDate.Text
        End If
        M_AddIdentity.CidntIssueAuthority = TxtAddIssAuth.Text
        ''M_AddIdentity.CidntNo = TxtAddIdentittyDtls.Text
        M_AddIdentity.CidntNo = TxtAddProofNo.Text
        M_AddIdentity.CustId = 0

        Try
            Dim M_CustMasterBL As New CustMasterBL
            M_CustId = M_CustMasterBL.Save_Data(M_CustMaster, M_PresAddress, M_PermAddress, M_PhotoIdentity, M_AddIdentity, hid_CustID.Value)
            UpLoadImageFile(M_CustId)
            ShowAlertMessage("Customer ID : " & M_CustId)
            Call Clear_Controls()
            TxtMobile.Enabled = True
            ImgEditNo.Visible = False
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgImage.Click
    '    'Session.Add("CustomerId", TxtCustID.Text)
    '    'Session.Add("CustomerUrl", "~/CustomerRegister.aspx")

    '    'Dim sb As New StringBuilder

    '    'sb.Append("<script>")
    '    'sb.Append("window.open('~/CommonImage.aspx', '', '');")
    '    'sb.Append("</scri")
    '    'sb.Append("pt>")
    '    'ClientScript.RegisterStartupScript(UpdatePanel1.GetType(), "test", sb.ToString())
    '    ''        Response.Redirect("~/CommonImage.aspx")

    'End Sub

    Protected Sub TxtIntroID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtIntroID.TextChanged
        Dim M_CommonBL As New CommonBL
        TxtIntroNm.Text = (M_CommonBL.GetCustomerName(Val(TxtIntroID.Text)))
        M_CommonBL = Nothing
    End Sub
    Private Sub Locate_LocationDtls()
        Dim M_Datatable As DataTable
        Dim M_CommonBL As New CommonBL
        M_Datatable = M_CommonBL.GetLocationDtls(CmbLocation.SelectedValue)
        M_CommonBL = Nothing

    End Sub

    Protected Sub TxtAddIDProof_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAddIDProof.TextChanged
        Try
            Dim M_Dt1 As New DataTable
            Dim M_Dt2 As New DataTable
            Dim ds As New DataSet
            Dim M_CommonBl As New CommonBL
            ds = M_CommonBl.Get_IdDetails(TxtAddProofNo.Text, CmbAddProof.SelectedValue)
            M_Dt1 = ds.Tables(0)
            M_Dt2 = ds.Tables(1)
            If M_Dt1.Rows(0).Item("Status") = "Y" Then
                ShowAlertMessage("Already Existing Customer ," + " Name:" & M_Dt2.Rows(0).Item("Cust_Name") & " ," + " Cust_Id:" & M_Dt2.Rows(0).Item("Cust_Id") & "")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub ImgSrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSrch.Click

    End Sub

    Protected Sub TxtDob_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtDob.TextChanged
        Try
            Dim M_Dt As New DataTable
            Dim M_CommonBl As New CommonBL
            M_Dt = M_CommonBl.Get_Age(TxtDob.Text)
            TxtAge.Text = M_Dt.Rows(0)("Age").ToString

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImgImage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgImage.Click

    End Sub

    Protected Sub CmbMainCaste_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbMainCaste.SelectedIndexChanged

    End Sub

    Protected Sub CmbSubCaste_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSubCaste.SelectedIndexChanged

    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles butClear.Click
        Clear_Controls()

    End Sub

    <WebMethod(EnableSession:=True)> _
    Public Shared Function GetCapturedImage() As String
        Dim url As String = HttpContext.Current.Session("CapturedImage").ToString()
        HttpContext.Current.Session("CapturedImage") = Nothing
        Return url
    End Function

    Private Sub UpLoadImageFile(ByVal CustID As String)

        Try


            'For i As Integer = 0 To GvIDProof.Rows.Count - 1
            '    Dim bol As Boolean = False

            '    If GvIDProof.Rows(i).Cells(2).Text = "" Then
            '        bol = True

            '    End If
            '    If bol = True Then
            '        ShowAlertMessage("Please Attach Signature...")
            '        Return
            '    End If
            'Next
            'If CmbPhotoType.SelectedIndex = 0 Then
            '    If TxtBrowse.Value = "" Then
            '        ShowAlertMessage("Please Select Photo")
            '        Return
            '    End If
            'Dim CopyPath As String
            'Dim HomeDir As String
            'Dim FileName As String = ""
            'FileName = DateTime.Now.ToString("MMddyyyyHHmmssms")


            ' CopyPath = "~\\Files\\" + DateTime.Now.Second.ToString() + TxtBrowse.Name
            'HomeDir = ConfigurationManager.AppSettings("HomeFolder").ToString()
            'Server.MapPath("CustTempImages//" + FileName + ".jpg")
            'Dim imageSize As New Byte(TxtBrowse.PostedFile.ContentLength)



            'ViewState("PhotoName") = Server.MapPath("CustTempImages//" + FileName + ".jpg")
            'Dim imageSize As Byte() = New Byte(TxtBrowse.PostedFile.ContentLength - 1) {}

            'Dim uploadedImage As HttpPostedFile = TxtBrowse.PostedFile

            ' uploadedImage.InputStream.Read(imageSize, 0, CInt(TxtBrowse.PostedFile.ContentLength))
            ' Dim ms As New MemoryStream(imageSize)

            'Dim Img As System.Drawing.Image = Bitmap.FromStream(ms, True)
            ' Img.Save(Server.MapPath("CustTempImages/" + FileName + ".jpg"))

            'ImgPhoto.ImageUrl = Server.MapPath("~/CustTempImages/" + FileName + ".jpg")

            'Session("CapturedImageType") = "CustTempImages/" + FileName + ".jpg"
            ' info = Session("CapturedImageType")


            ' Else
            'If Session("CapturedImageType") = "" Then
            'ShowAlertMessage("Please select Photo...")
            'Return
            'End If

            'End If


            Dim objConn As SqlConnection
            Dim objCom As SqlCommand
            Try
                Dim converter As New ImageConverter()

                ' Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(info))
                objConn = New SqlConnection(ConfigurationManager.AppSettings("SQL_CONNSTR").ToString())
                Dim ImageDB As String = ConfigurationManager.AppSettings("ImageDB").ToString()
                Dim imgPath As String
                If Session("Imgphoto") <> Nothing Then
                    imgPath = Session("Imgphoto").ToString()
                    Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))
                    Dim imgBytes As Byte() = New Byte(-1) {}
                    imgBytes = DirectCast(converter.ConvertTo(Imge, imgBytes.[GetType]()), Byte())

                    objCom = New SqlCommand("delete from " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl where Cust_ID='" + CustID + "' and Img_ID='1'", objConn)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    Dim InsQry As String
                    InsQry = "insert into " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl(Br_Code,Cust_ID,Cust_Type,Cust_Oper_Id,Img_ID,Img_Date,MakerID,CheckerID,Making_Time,Checking_Time,Img_Image) values ('" + CmbAdmBranch.SelectedValue + "','" + CustID + "', "
                    InsQry = InsQry + "'I','0','1', "
                    InsQry = InsQry + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "','" + Session("LoginUserID").ToString() + "','" + Session("LoginUserID").ToString() + "','" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "', "
                    InsQry = InsQry + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "',@Picture)"

                    objCom = New SqlCommand(InsQry, objConn)
                    Dim pictureParameter As SqlParameter = New SqlParameter("@Picture", SqlDbType.Image)
                    pictureParameter.Value = imgBytes
                    objCom.Parameters.Add(pictureParameter)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    Session("Imgphoto") = Nothing
                End If

                If Session("ImgSignature") <> Nothing Then
                    'Dim bitmabimgsig As Bitmap = Session("ImgSignature")
                    imgPath = Session("ImgSignature").ToString()
                    objCom = New SqlCommand("delete from " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl where Cust_ID='" + CustID + "' and Img_ID='2'", objConn)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    ' Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(info))
                    Dim Imgesig As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))
                    Dim imgBytessig As Byte() = New Byte(-1) {}
                    imgBytessig = DirectCast(converter.ConvertTo(Imgesig, imgBytessig.[GetType]()), Byte())
                    Dim InsQrysig As String
                    InsQrysig = "insert into " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl(Br_Code,Cust_ID,Cust_Type,Cust_Oper_Id,Img_ID,Img_Date,MakerID,CheckerID,Making_Time,Checking_Time,Img_Image) values ('" + CmbAdmBranch.SelectedValue + "','" + CustID + "', "
                    InsQrysig = InsQrysig + "'I','0','2', "
                    InsQrysig = InsQrysig + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "','" + Session("LoginUserID").ToString() + "','" + Session("LoginUserID").ToString() + "','" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "', "
                    InsQrysig = InsQrysig + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "',@Picture)"

                    objCom = New SqlCommand(InsQrysig, objConn)
                    Dim pictureParametersig As SqlParameter = New SqlParameter("@Picture", SqlDbType.Image)
                    pictureParametersig.Value = imgBytessig
                    objCom.Parameters.Add(pictureParametersig)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    Session("ImgSignature") = Nothing
                End If

                If Session("ImgDocument") <> Nothing Then
                    imgPath = Session("ImgDocument").ToString()
                    Dim DocName As String = CustID + "Document.Png"
                    Dim ImgDocument As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))
                    ImgDocument.Save(Server.MapPath("~/DocumentFile/" + DocName))
                    objCom = New SqlCommand("delete from " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl where Cust_ID='" + CustID + "' and Img_ID='3'", objConn)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    ' Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(info))
                    Dim Imgesig As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))
                    Dim imgBytessig As Byte() = New Byte(-1) {}
                    imgBytessig = DirectCast(converter.ConvertTo(Imgesig, imgBytessig.[GetType]()), Byte())
                    Dim InsQrysig As String
                    InsQrysig = "insert into " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl(Br_Code,Cust_ID,Cust_Type,Cust_Oper_Id,Img_ID,Img_Date,MakerID,CheckerID,Making_Time,Checking_Time,Img_Image) values ('" + CmbAdmBranch.SelectedValue + "','" + CustID + "', "
                    InsQrysig = InsQrysig + "'I','0','3', "
                    InsQrysig = InsQrysig + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "','" + Session("LoginUserID").ToString() + "','" + Session("LoginUserID").ToString() + "','" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "', "
                    InsQrysig = InsQrysig + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "',@Picture)"

                    objCom = New SqlCommand(InsQrysig, objConn)
                    Dim pictureParametersig As SqlParameter = New SqlParameter("@Picture", SqlDbType.Image)
                    pictureParametersig.Value = imgBytessig
                    objCom.Parameters.Add(pictureParametersig)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    Session("ImgDocument") = Nothing
                End If

                If Session("ImgAadhaar") <> Nothing Then
                    'Dim bitmabimgsig As Bitmap = Session("ImgSignature")
                    imgPath = Session("ImgAadhaar").ToString()
                    objCom = New SqlCommand("delete from " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl where Cust_ID='" + CustID + "' and Img_ID='4'", objConn)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    ' Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(info))
                    Dim Imgesig As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))
                    Dim imgBytessig As Byte() = New Byte(-1) {}
                    imgBytessig = DirectCast(converter.ConvertTo(Imgesig, imgBytessig.[GetType]()), Byte())
                    Dim InsQrysig As String
                    InsQrysig = "insert into " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl(Br_Code,Cust_ID,Cust_Type,Cust_Oper_Id,Img_ID,Img_Date,MakerID,CheckerID,Making_Time,Checking_Time,Img_Image) values ('" + CmbAdmBranch.SelectedValue + "','" + CustID + "', "
                    InsQrysig = InsQrysig + "'I','0','4', "
                    InsQrysig = InsQrysig + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "','" + Session("LoginUserID").ToString() + "','" + Session("LoginUserID").ToString() + "','" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "', "
                    InsQrysig = InsQrysig + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "',@Picture)"


                    objCom = New SqlCommand(InsQrysig, objConn)
                    Dim pictureParametersig As SqlParameter = New SqlParameter("@Picture", SqlDbType.Image)
                    pictureParametersig.Value = imgBytessig
                    objCom.Parameters.Add(pictureParametersig)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    Session("ImgAadhaar") = Nothing
                End If
                If Session("ImgPancard") <> Nothing Then
                    'Dim bitmabimgsig As Bitmap = Session("ImgSignature")
                    imgPath = Session("ImgPancard").ToString()
                    objCom = New SqlCommand("delete from " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl where Cust_ID='" + CustID + "' and Img_ID='5'", objConn)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    ' Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(info))
                    Dim Imgesig As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(imgPath))
                    Dim imgBytessig As Byte() = New Byte(-1) {}
                    imgBytessig = DirectCast(converter.ConvertTo(Imgesig, imgBytessig.[GetType]()), Byte())
                    Dim InsQrysig As String
                    InsQrysig = "insert into " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl(Br_Code,Cust_ID,Cust_Type,Cust_Oper_Id,Img_ID,Img_Date,MakerID,CheckerID,Making_Time,Checking_Time,Img_Image) values ('" + CmbAdmBranch.SelectedValue + "','" + CustID + "', "
                    InsQrysig = InsQrysig + "'I','0','5', "
                    InsQrysig = InsQrysig + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "','" + Session("LoginUserID").ToString() + "','" + Session("LoginUserID").ToString() + "','" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "', "
                    InsQrysig = InsQrysig + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "',@Picture)"


                    objCom = New SqlCommand(InsQrysig, objConn)
                    Dim pictureParametersig As SqlParameter = New SqlParameter("@Picture", SqlDbType.Image)
                    pictureParametersig.Value = imgBytessig
                    objCom.Parameters.Add(pictureParametersig)
                    objConn.Open()
                    objCom.ExecuteNonQuery()
                    objConn.Close()
                    Session("ImgPancard") = Nothing
                End If

                'ShowAlertMessage("Saved Successfully...")
                ' Clear()
                ' Session("CapturedImageType") = ""

            Catch ex As Exception
                Throw New Exception(ex.Message)
                objConn.Close()
            End Try
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ImgSrch1_Click(sender As Object, e As ImageClickEventArgs) Handles ImgSrch1.Click

    End Sub

    'Protected Sub But_Crop_Click(sender As Object, e As EventArgs) Handles But_Crop.Click
    '    Try

    '        Dim croppedFileName As String = String.Empty
    '        Dim croppedFilePath As String = String.Empty
    '        Dim str As String = hid_imgvalue.Value
    '        Dim img As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(hid_imgvalue.Value)
    '        'Dim i As Integer = Im_Upload.

    '        Dim striem As System.IO.MemoryStream = New System.IO.MemoryStream(img, 0, img.Length)
    '        striem.Read(img, 0, img.Length)
    '        Dim orgImg1 As System.Drawing.Image = New Bitmap(striem)
    '        Dim fileName As String = "MSMsss"
    '        ' Dim cropCard As New Rectangle(Convert.ToInt32(x1.Value), Convert.ToInt32(y1.Value), Convert.ToInt32(w.Value), Convert.ToInt32(h.Value))
    '        ' Dim Bitmap As Bitmap = New Bitmap(cropCard.Width, cropCard.Height)
    '        'Dim fileName As String = Path.GetFileName(Img_Upload.PostedFile.FileName)
    '        'Dim FilePath As String = Path.Combine(Server.MapPath("~/Images"), fileName)
    '        Dim orgImg As Image = System.Drawing.Image.FromStream(striem)

    '        'Dim msmimg As System.Drawing.Image=System.Drawing.Image.
    '        Dim newWidth As Integer = Convert.ToInt32(imgw.Value)
    '        Dim newHeight As Integer = Val(imgH.Value)

    '        Using thumbnailBitmap = New Bitmap(newWidth, newHeight)
    '            Using thumbnailGraph = Graphics.FromImage(thumbnailBitmap)
    '                thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality
    '                thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality
    '                thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic

    '                Dim imageRectangle = New Rectangle(0, 0, newWidth, newHeight)
    '                thumbnailGraph.DrawImage(orgImg, imageRectangle)
    '                thumbnailBitmap.Save(Server.MapPath("~/Images/mmmmmmmmmmm.Png"))
    '                ' orgImg1 = thumbnailBitmap.UnlockBits
    '            End Using
    '        End Using
    '        ' orgImg.Width = Convert.ToInt32(imgw.Value)

    '        Dim areaToCrop As New Rectangle(Convert.ToInt32(x1.Value), Convert.ToInt32(y1.Value), Convert.ToInt32(w.Value), Convert.ToInt32(h.Value))

    '        Try
    '            Dim filName As String = "mmmmmmmmmmm.Png"
    '            Dim FilePat As String = Path.Combine(Server.MapPath("~/Images"), filName)
    '            orgImg1 = System.Drawing.Image.FromFile(FilePat)
    '            ' Dim orgImg1 As System.Drawing.Image = System.Drawing.Image.FromFile()
    '            Dim bitMap As New Bitmap(areaToCrop.Width, areaToCrop.Height)
    '            'Create graphics object for alteration
    '            Using g As Graphics = Graphics.FromImage(bitMap)
    '                'Draw image to screen
    '                g.DrawImage(orgImg1, New Rectangle(0, 0, bitMap.Width, bitMap.Height), areaToCrop, GraphicsUnit.Pixel)
    '            End Using

    '            'name the cropped image
    '            croppedFileName = "crop_" & fileName
    '            'Create path to store the cropped image
    '            croppedFilePath = Path.Combine(Server.MapPath("~/Images"), croppedFileName)
    '            'save cropped image in folder
    '            bitMap.Save(croppedFilePath)
    '            orgImg.Dispose()
    '            bitMap = Nothing
    '            'Now you can delete the original uploaded image from folder               
    '            'File.Delete(FilePath)
    '            'Hide the panel
    '            ' pnlCrop.Visible = False
    '            'Show success message in label
    '            'lblMsg.ForeColor = Color.Green
    '            'lblMsg.Text = "Image cropped and saved successfully"

    '            'Show cropped image
    '            imgCapture.Src = "~/Images/" & croppedFileName

    '            'Show Reset button
    '            ' btnReset.Visible = True
    '        Catch ex As Exception
    '            ' lblMsg.Text = "Oops!! error occured : " & ex.Message.ToString()
    '        Finally
    '            fileName = String.Empty
    '            ' FilePath = String.Empty
    '            croppedFileName = String.Empty
    '            croppedFilePath = String.Empty
    '        End Try
    '        'Dim imgcr As Image = Bitmap.GetThumbnailImage(w,h,cropImg.
    '        ' Dim g As Graphics = New Graphics(FileUpload1.FileContent)
    '    Catch ex As Exception

    '    End Try
    'End Sub




    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles But_verfiy.Click
        Try
            Dim M_CustMasterBL As New CustMasterBL
            Dim M_Datatable As New DataTable
            Dim MobileNo As String = TxtMobile.Text
            M_Datatable = M_CustMasterBL.OTPGenrate(MobileNo)
            Session("ODPTBL") = M_Datatable
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('img_process');show('cover');show('VerifyMob');", True)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub But_verify_Click(sender As Object, e As EventArgs) Handles But_verify.Click
        Dim M_CustMasterBL As CustMasterBL
        Dim OTPNO As String = txt_verifycode.Text
        Dim M_Datatable As New DataTable
        M_Datatable = Session("ODPTBL")

        Dim OTPCODE As String = M_Datatable.Rows(0)("OTP")
        Dim OtpDate As Date = Convert.ToDateTime(M_Datatable.Rows(0)("GenTime"))
        Session("OTPMOB") = M_Datatable.Rows(0)("MobileNo")
        OtpDate = OtpDate.AddMinutes(20)
        Dim Nowdate As Date = Now.ToString()

        If OtpDate >= Nowdate And TxtMobile.Text = Session("OTPMOB") Then
            If OTPNO = OTPCODE Then
                Hid_ODPStatus.Value = "Y"
                Session("ODPTBL") = Nothing
                txt_verifycode.Text = ""
                TxtMobile.Enabled = False
                ImgEditNo.Attributes.Add("style", "Visibility:True")

                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('cover');hide('VerifyMob');hide('div_Verify');hide('imgverifyError');show('imgVerify');hide('lbl_sessionexp');hide('lbl_errMsg');  ", True)
            Else
                'ImgEditNo.Attributes.Add("style", "Visibility:false")

                '  ErrorMessage("Enter Correct OTP Code")
                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');hide('lbl_sessionexp');show('lbl_errMsg');", True)
            End If
        Else
            Session("ODPTBL") = Nothing
            ' ImgEditNo.Attributes.Add("style", "Visibility:False")
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "show('cover');show('VerifyMob');hide('div_Verify');show('imgverifyError');hide('imgVerify');show('lbl_sessionexp');hide('lbl_errMsg');", True)
        End If
    End Sub

    Protected Sub CmbDist_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbDist.SelectedIndexChanged
        Try
            Dim M_Datatable As New DataTable
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Pickup(CmbLocation, 14, CmbDist.SelectedValue)
            M_Datatable = M_CommonBL.Get_PostOffice(CmbLocation.SelectedValue)
            TxtPresDist.Text = CmbDist.SelectedItem.Text
            TxtPresPlace.Text = CmbLocation.SelectedItem.Text
            TxtPermDist.Text = CmbDist.SelectedItem.Text
            TxtPermPlace.Text = CmbLocation.SelectedItem.Text
            TxtPermState.Text = CmbState.SelectedItem.Text
            TxtPresState.Text = CmbState.SelectedItem.Text
            'TxtPresDist.Text = M_Datatable.Rows(0).Item("District")
            ' ''TxtPresPin.Text = M_Datatable.Rows(0).Item("Pincode")
            'TxtPresPlace.Text = M_Datatable.Rows(0).Item("Post_Office")
            'TxtPermDist.Text = M_Datatable.Rows(0).Item("District")
            ' ''TxtPermPin.Text = M_Datatable.Rows(0).Item("Pincode")
            'TxtPermPlace.Text = M_Datatable.Rows(0).Item("Post_Office")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLocation.SelectedIndexChanged
        Try
            Dim M_Datatable As New DataTable
            Dim M_CommonBL As New CommonBL
            M_Datatable = M_CommonBL.Get_PostOffice(CmbLocation.SelectedValue)
            TxtPresDist.Text = M_Datatable.Rows(0).Item("District")
            TxtPresPlace.Text = M_Datatable.Rows(0).Item("Post_Office")

            TxtPermDist.Text = M_Datatable.Rows(0).Item("District")
            TxtPermPlace.Text = M_Datatable.Rows(0).Item("Post_Office")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TxtPhotoIDProof_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtPhotoIDProof.TextChanged

        Dim M_Dt1 As New DataTable
        Dim M_Dt2 As New DataTable
        Dim ds As New DataSet
        Dim M_CommonBl As New CommonBL
        ds = M_CommonBl.Get_IdDetails(TxtAddProofNo.Text, CmbAddProof.SelectedValue)
        M_Dt1 = ds.Tables(0)
        M_Dt2 = ds.Tables(1)
        If (M_Dt1.Rows.Count > 0) Then
            If M_Dt1.Rows(0).Item("Status") = "Y" Then
                ShowAlertMessage("Already Existing Customer ," + " Name:" & M_Dt2.Rows(0).Item("Cust_Name") & " ," + " Cust_Id:" & M_Dt2.Rows(0).Item("Cust_Id") & "")
            End If
        End If


    End Sub

    'Protected Sub CmbPhotoIDProof_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbPhotoIDProof.SelectedIndexChanged
    '    Try
    '        TxtPhotoIDNoProof.Text = ""
    '        TxtPhotoIdentityDtls.Text = ""
    '        TxtPhotoIssDate.Text = ""
    '        TxtPhotoExpiryDate.Text = ""
    '        TxtPhotoRemarks.Text = ""

    '        TxtPhotoIssAuth.Text = ""

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub CmbState_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbState.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Pickup(CmbDist, 103, CmbState.SelectedValue)
            Call CmbDist_SelectedIndexChanged(Me.CmbDist, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ImgEditNo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgEditNo.Click
        Try
            TxtMobile.Enabled = True
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "hide('cover');Show('VerifyMob');hide('div_Verify');hide('imgverifyError');hide('imgVerify');hide('lbl_sessionexp');hide('lbl_errMsg');", True)
        Catch ex As Exception

        End Try
    End Sub

    'Public Shared Function SaveImage1(imageBase64 As String) As String
    '    Try
    '        imageBase64 = imageBase64.Replace("data:image/png;base64,", "") _
    '                                 .Replace("data:image/jpeg;base64,", "")

    '        Dim bytes As Byte() = Convert.FromBase64String(imageBase64)

    '        Dim path As String = HttpContext.Current.Server.MapPath("~/CustTempImages")

    '        If Not Directory.Exists(path) Then
    '            Directory.CreateDirectory(path)
    '        End If

    '        Dim fileName As String = "WebCam_" & DateTime.Now.Ticks & ".png"
    '        File.WriteAllBytes(path & fileName, bytes)

    '        Return "success"
    '    Catch ex As Exception
    '        Return "error: " & ex.Message
    '    End Try
    'End Function
    Public Shared Function SaveImage(imageBase64 As String) As String

        Try
            imageBase64 = imageBase64.Split(","c)(1)
            Dim bytes = Convert.FromBase64String(imageBase64)

            Dim path = HttpContext.Current.Server.MapPath("~/CustTempImages/")
            If Not Directory.Exists(path) Then Directory.CreateDirectory(path)

            Dim filename As String = "IMG_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".png"
            File.WriteAllBytes(path & filename, bytes)

            Return "Saved successfully"
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    'Public Shared Sub SaveImage(imageBase64 As String)
    '    imageBase64 = imageBase64.Replace("data:image/png;base64,", "") _
    '                             .Replace("data:image/jpeg;base64,", "")

    '    Dim bytes As Byte() = Convert.FromBase64String(imageBase64)

    '    Dim path As String = HttpContext.Current.Server.MapPath("~/CustTempImages")

    '    If Not Directory.Exists(path) Then
    '        Directory.CreateDirectory(path)
    '    End If

    '    Dim fileName As String = "WebCam_" & DateTime.Now.Ticks & ".png"
    '    File.WriteAllBytes(path & fileName, bytes)
    'End Sub
    'Public Shared Function SaveImage(imageBase64 As String) As String

    '    ' Remove data:image/...;base64, if present
    '    If imageBase64.Contains(",") Then
    '        imageBase64 = imageBase64.Split(","c)(1)
    '    End If

    '    Dim imageBytes As Byte() = Convert.FromBase64String(imageBase64)

    '    ' Example: save to database
    '    Using con As New SqlConnection("your_connection_string")
    '        Using cmd As New SqlCommand("INSERT INTO CustomerImages(ImageData) VALUES(@img)", con)
    '            cmd.Parameters.Add("@img", SqlDbType.VarBinary).Value = imageBytes
    '            con.Open()
    '            cmd.ExecuteNonQuery()
    '        End Using
    '    End Using

    '    Return "OK"
    'End Function
    Public Shared Function Ping() As String
        Return "OK"
    End Function

    Private Sub BtnWebCamUpload_Click(sender As Object, e As EventArgs) Handles BtnWebCamUpload.Click

        Dim imgData As String = Request.Form("imgData")

        If String.IsNullOrEmpty(imgData) Then Exit Sub

        imgData = imgData.Split(","c)(1)
        Dim bytes = Convert.FromBase64String(imgData)

        Dim path = Server.MapPath("~/CustTempImages/")
        If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
        Dim filename As String = "IMG_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".png"
        File.WriteAllBytes(path & filename, bytes)
        Session("Imgphoto") = "~/CustTempImages/" & filename
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "closecamera", "closecamera();", True)
    End Sub
End Class