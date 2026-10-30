Imports SMARTCORE_BL

Public Class AccountPrevilage
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1003
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbOldPrevilage, 42, 0)
        M_CommonBL.Fill_Pickup(CmbNewPrevilage, 42, 0)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "DEPOSIT")
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtScheme.Text = CmbScheme.SelectedValue
            Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
        Set_SchemeCtrl()
        TxtShortCode.Text = ""
        TxtShortCode.Focus()
    End Sub

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim M_AccNo As String = TxtAccNo.Text

        Dim Dt_AcDtls As DataTable
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            HdnAccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            CmbOldPrevilage.SelectedValue = Dt_AcDtls.Rows(0)("Previlage").ToString()
            TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
        Else
            ShowAlertMessage("INVALID ACCOUNT NO")
        End If

        M_CommonBL = Nothing
        If Len(HdnAccID.Value) > 0 Then TxtAccNo.Focus()
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_DepBL As New DepositMasterBL
        Dim M_AccNo As String = TxtAccNo.Text
        Dim Dt_AcDtls As DataTable
        Try
            Dt_AcDtls = M_DepBL.Update_Previlage(HdnAccID.Value, Session("TransDate"), CmbOldPrevilage.SelectedValue, CmbNewPrevilage.SelectedValue, Txt_IntRate.Text, Session("UserID"))
            M_DepBL = Nothing
            ShowAlertMessage("New Previlege Updated.Successfully Saved")
            Call ClearData()
        Catch ex As Exception
            ShowAlertMessage(" SAVING FAILED")
        End Try
    End Sub


    Private Sub ClearData()
        TxtName.Text = ""
        TxtAddress.Text = ""
        TxtAccNo.Text = ""
        TxtShortCode.Text = ""
        TxtShortCode.Focus()
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Call ClearData()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class