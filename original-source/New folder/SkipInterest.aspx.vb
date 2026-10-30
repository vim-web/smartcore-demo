Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports System.Data

Public Class SkipInterest
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1131
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtSchCode.Text, 7)
            TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
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
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        TxtRcptDate.Attributes.Add("readonly", "readonly")
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
    End Sub
    Private Sub Clear_Controls()
        TxtAccNo.Text = ""
        TxtRcptDate.Text = ""
        TxtRcptno.Text = ""
        TxtReason.Text = ""
        TxtAcHoldrAddr.Text = ""
        TxtAcHoldrName.Text = ""
        TxtAccNo.Focus()
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue

        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtSchCode.Text, 7)

    End Sub

    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        On Error Resume Next


        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Set_SchemeCtrl()

    End Sub


    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
        Set_SchemeCtrl()
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As DataTable
        Dim CustID As String = ""

        Dim M_AccNo As String = TxtAccNo.Text
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            CustID = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            TxtAcHoldrName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtAcHoldrAddr.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
        Else

            ShowAlertMessage("INVALID ACCOUNT NO")
            Clear_Controls()
        End If

        M_CommonBL = Nothing
        If Len(CustID) > 0 Then TxtAccNo.Focus()
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_SkipInterest As New SMARTCORE_BO.SkipInterestBO
        Dim M_StID As Integer
        Try



            With M_SkipInterest
                .RegID = 0
                .RegDate = Session("TransDate")
                .Witheffdate = Session("TransDate")
                .RevokeDate = "1/1/1900"
                .RevokeTime = "1/1/1900"
                .AccNo = TxtAccNo.Text
                .SchCode = Val(TxtScheme.Text)
                .BrCode = Val(TxtBranch.Text)
                .MakerID = Session("Login")
                .CheckerID = Session("Login")
                .Reason = TxtReason.Text
                .ReceiptDate = TxtRcptDate.Text
                .ReceiptNo = TxtRcptno.Text
                .ModeofReceipt = CmbModOfRecpt.SelectedValue
                .Status = "N"
                .PostingStatus = "N"
                .AccruedStatus = "P"
                .Authorised = "N"
                .RevokeAuthorised = "N"
                .RevokeUser = "N"
            End With


            Dim M_SkipInterestBL As New SkipInterestBL

            Session("M_EntryMode") = "NEW"
            M_StID = M_SkipInterestBL.Save_Data(M_SkipInterest, Session("M_EntryMode"))
            If (M_StID > 0) Then
                ShowAlertMessage("Successfully Saved")
                Clear_Controls()
            Else
                ShowAlertMessage(" SAVING FAILED")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try




    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()

    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class