Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Public Class LoanCorrectionUtility
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN")
        M_CommonBL.Fill_Pickup(CmbPurpose, 31, 0)
        M_CommonBL.Fill_Pickup(CmbSubpurpose, 32, CmbPurpose.SelectedValue)
        M_CommonBL.Fill_Pickup(CmbGender, 8, 0)
        M_CommonBL.Fill_Pickup(CmbReligion, 1, 0)
        M_CommonBL.Fill_Pickup(CmbCaste, 2, 0)
        M_CommonBL.Fill_Directors(CmbDirectors)
        CmbDirectors.Items.Add("OLD DIRECTORS")
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_LoanUtilityBO As New SMARTCORE_BO.LoanCorrectionUtilityBO
            Dim M_StID As Integer
            With M_LoanUtilityBO
                .ReslDate = TxtReslDate.Text
                .PurposeCode = CmbPurpose.SelectedValue
                .SubPurposeCode = CmbSubpurpose.SelectedValue
                .Religion = CmbReligion.SelectedValue
                .Caste = CmbCaste.SelectedValue
                .Gender = CmbGender.SelectedValue
                .Director = CmbDirectors.SelectedValue
                .MakerTime = "1/1/1900"
                .AccID = HdnAccId.Value
                .DocNo = IIf(TxtDocNo.Text = "", "0", TxtDocNo.Text)
                .WardCode = TxtWardCode.Text
                .GrahanNo = TxtGrahanNo.Text
                .RegOffice = CmbRegoff.SelectedItem.Text
                .ReslNo = TxtReslNo.Text
                .MakerID = Session("UserId")
                .Extend = TxtExtend.Text
                .SurveyNo = TxtSurveyNo.Text
                .Resurvey = TxtResurveyNo.Text
            End With

            Session("M_EntryMode") = "NEW"

            Dim M_LoanBL As New LoanCorrectionUtilityBL
            M_StID = M_LoanBL.Save_Data(M_LoanUtilityBO, Session("M_EntryMode"))

            ShowAlertMessage("Successfully Saved")
            Clear()
        Catch ex As Exception
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub

    Protected Sub TxtAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim M_LoanUtilityBo As New SMARTCORE_BO.LoanCorrectionUtilityBO
        Dim M_LoanUtilityBL As New LoanCorrectionUtilityBL
        Dim Dt_AcDtls As New DataTable
        Dim M_AccNo As String = TxtAccNo.Text

        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            HdnAccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtAccDetails.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            M_LoanUtilityBo = M_LoanUtilityBL.Locate_Data(HdnAccId.Value)
            If M_LoanUtilityBo.AccID <> 0 Then
                With M_LoanUtilityBo
                    TxtGrahanNo.Text = .GrahanNo
                    CmbRegoff.SelectedItem.Text = .RegOffice
                    TxtReslDate.Text = .ReslDate
                    TxtReslNo.Text = .ReslNo
                    TxtWardCode.Text = .WardCode
                    CmbCaste.SelectedValue = .Caste
                    CmbDirectors.SelectedValue = .Director
                    CmbGender.SelectedValue = .Gender
                    CmbPurpose.SelectedValue = .PurposeCode
                    CmbReligion.SelectedValue = .Religion
                    CmbSubpurpose.SelectedValue = .SubPurposeCode
                    TxtDocNo.Text = .DocNo
                    TxtExtend.Text = .Extend
                    TxtSurveyNo.Text = .SurveyNo
                    TxtResurveyNo.Text = .Resurvey
                End With
            Else
                Exit Sub
            End If
        Else
            ShowAlertMessage("Invalid A/c No")
            TxtAccNo.Focus()
        End If
       
        M_CommonBL = Nothing

    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Clear()
        TxtAccNo.Text = ""
        TxtGrahanNo.Text = ""
        TxtReslDate.Text = ""
        TxtReslNo.Text = ""
        TxtWardCode.Text = ""
        CmbDirectors.SelectedIndex = 0
        CmbCaste.SelectedIndex = 0
        CmbReligion.SelectedIndex = 0
        CmbSubpurpose.SelectedIndex = 0
        CmbPurpose.SelectedIndex = 0
        TxtDocNo.Text = ""
        TxtSurveyNo.Text = ""
        TxtResurveyNo.Text = ""
        TxtExtend.Text = ""
        TxtName.Text = ""
        TxtAccDetails.Text = ""
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear()
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
    Private Sub Fill_Pickup(ByRef Cmbbx As Object, ByVal M_PkcType As Integer, ByVal M_PkcParentID As Integer)
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Pickup(Cmbbx, M_PkcType, M_PkcParentID)
        M_CommonBL = Nothing
    End Sub

    Protected Sub CmbReligion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbReligion.SelectedIndexChanged
        Fill_Pickup(CmbCaste, 2, CmbReligion.SelectedValue)
    End Sub

    Protected Sub CmbPurpose_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPurpose.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Pickup(CmbSubpurpose, 32, CmbPurpose.SelectedValue)
        M_CommonBL = Nothing
    End Sub
End Class