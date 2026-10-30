Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data
Imports SMARTCORE_BO

Public Class ExternalLien
    Inherits System.Web.UI.Page
    Shared Dt_AcDtls As DataTable
    Dim FormID As Integer = 1055
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

            Load_DefaultValues()
        End If
    End Sub
    Private Sub Set_SchemeCtrl()
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
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
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "DEPOSIT")
            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Clear_Controls()
        TxtAddress.Text = ""
        TxtBalance.Text = ""
        TxtLienWith.Text = ""
        TxtMobNo.Text = ""
        TxtName.Text = ""
        TxtRemark.Text = ""
        TxtAccNo.Text = ""
        TxtShortCode.Text = ""
        HdnAlert.Value = ""
        Call Set_SchemeCtrl()
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
        Set_SchemeCtrl()
        TxtShortCode.Text = ""
        TxtShortCode.Focus()
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtScheme.Text = CmbScheme.SelectedValue
            Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_LienBL As New ExternalLienBL
            Dim M_LienBO As New ExternalLienBO
            Dim M_AccNo As String = TxtAccNo.Text

            HdnAlert.Value = ""

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                TxtMobNo.Text = Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                AccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                Dim Bal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
                TxtBalance.Text = Format(Val(CStr(Math.Abs(Bal)).ToString), "#0.00") & " " & IIf(Bal < 0, "Dr", "Cr")

                M_LienBO = M_LienBL.Locate_Data(AccID.Value)

                With M_LienBO
                    If .LienID > 0 Then  '' Liened Data Exists
                        If .RevokeAuthorised = "N" Then
                            HdnAlert.Value = "This A/c Aleardy Liend with :" & .LienWith & vbCrLf & "Liened On: " & .TrDate
                            ShowAlertMessage(HdnAlert.Value)
                        Else
                            ShowAlertMessage("This A/c Previously Liened With: " & .LienWith & vbCrLf & "Liened On: " & .TrDate & _
                                                vbCrLf & " And Revoked On: " & .RevokeDate)
                        End If
                    End If
                End With
            Else
                Clear_Controls()
                ShowAlertMessage("INVALID ACCOUNT NO")
            End If

            M_CommonBL = Nothing
            M_LienBL = Nothing
            M_LienBO = Nothing

            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
            TxtAccNo.Text = M_AccNo

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_ExternalLien As New SMARTCORE_BO.ExternalLienBO
            With M_ExternalLien
                .LienID = 0
                .AccID = AccID.Value
                .TrDate = Session("TransDate")
                '.MakingTime = "1/1/1900"
                '.CheckingTime = M_Dt.Rows(0).Item("Checking_Time")
                .RevokeDate = "1/1/1900"
                '.RevokeMakingTime = "1/1/1900"
                '.RevokeCheckingTime = "1/1/1900"
                .AccNo = Val(TxtAccNo.Text)
                .SchCode = Val(TxtScheme.Text)
                .BrCode = Val(TxtBranch.Text)
                .LienWith = TxtLienWith.Text
                .LienRemarks = TxtRemark.Text
                .MakerID = Session("LoginID")
                .CheckerID = Session("LoginID")
                .RevokeRemarks = ""
                .RevokeMakerID = ""
                .RevokeCheckerID = ""
                .Authorised = "Y"
                .RevokeAuthorised = "N"

            End With
            Dim M_ExternalLienBL As New ExternalLienBL
            Session("M_EntryMode") = "NEW"
            If M_ExternalLienBL.Save_Data(M_ExternalLien, Session("M_EntryMode")) > 0 Then
                ShowAlertMessage("EXTERNAL LIEN SAVING COMPLETED")
                Call Clear_Controls()
            Else
                ShowAlertMessage("EXTERNAL LIEN SAVING FAILED")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class