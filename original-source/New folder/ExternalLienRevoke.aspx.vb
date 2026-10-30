Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Imports System.Data
Public Class ExternalLienRevoke
    Inherits System.Web.UI.Page
    Shared Dt_AcDtls As DataTable
    Public RevokTbl As New DataTable("Table")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
            Set_Grid()
            CmbBranch.SelectedValue = Session("DefaultBrCode")
        End If
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        'TxtLienDate.Attributes.Add("readonly", "readonly")
        'TxtLienWith.Attributes.Add("readonly", "readonly")
        'TxtRemark.Attributes.Add("readonly", "readonly")
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
    End Sub
    Private Sub Set_Grid()
        DgvLienRevoke.DataSource = Nothing
        RevokTbl.Columns.Add("Tr_Date")
        RevokTbl.Columns.Add("LienWith")
        RevokTbl.Columns.Add("LienRemarks")
        RevokTbl.Rows.Add("", "", "")
        DgvLienRevoke.DataSource = RevokTbl
        DgvLienRevoke.DataBind()
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtScheme.Text = CmbScheme.SelectedValue
            Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    'Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
    'Dim M_ExternalLien As New SMARTCORE_BO.ExternalLienBO
    'With M_ExternalLien
    '    .LienID = 0
    '    .AccID = 0
    '    .TrDate = Session("TransDate")
    '    '.MakingTime = "1/1/1900"
    '    '.CheckingTime = M_Dt.Rows(0).Item("Checking_Time")
    '    .RevokeDate = TxtRevokeDate.Text
    '    '.RevokeMakingTime = "1/1/1900"
    '    '.RevokeCheckingTime = "1/1/1900"
    '    .AccNo = Val(TxtAccNo.Text)
    '    .SchCode = Val(TxtScheme.Text)
    '    .BrCode = Val(TxtBranch.Text)
    '    .LienWith =TxtLienWith.Text
    '    .LienRemarks = TxtRemark.Text
    '    .MakerID = Session("Login")
    '    .CheckerID = Session("Login")
    '    .RevokeRemarks = TxtRevkRemark.Text
    '    .RevokeMakerID = ""
    '    .RevokeCheckerID = ""
    '    .Authorised = "Y"
    '    .RevokeAuthorised = "N"

    'End With
    'Dim M_ExternalLienBL As New ExternalLienBL
    'Session("M_EntryMode") = "NEW"
    'If M_ExternalLienBL.Save_Data(M_ExternalLien, Session("M_EntryMode")) = True Then
    '    ShowAlertMessage("EXTERNAL LIEN SAVING COMPLETED")
    'Else
    '    ShowAlertMessage("EXTERNAL LIEN SAVING FAILED")
    'End If


    'End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub
    Private Sub Set_SchemeCtrl()
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As New DataTable
        Dim M_RevokeTbl As New DataTable
        Dim M_ExternalLienBL As New ExternalLienBL

        Dim M_AccNo As String = TxtAccNo.Text

        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            HdnAccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()

            M_RevokeTbl = M_ExternalLienBL.Get_LienRevoke(HdnAccID.Value)
            If M_RevokeTbl.Rows.Count > 0 Then
                DgvLienRevoke.DataSource = M_RevokeTbl
                DgvLienRevoke.DataBind()
            End If
        End If
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
        Set_SchemeCtrl()
        TxtShortCode.Text = ""
        TxtShortCode.Focus()
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    'Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
    '    Response.Redirect("~/Main.aspx")
    'End Sub

    Private Sub DgvLienRevoke_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvLienRevoke.RowCommand
        Dim M_Sql As String
        Dim M_DBConn As New Connection

        'Dim index As Integer = DgvReminder.SelectedIndex
        'HdnRemId.Value = Convert.ToString(Me.DgvReminder.SelectedRow)
        M_Sql = "update Accmain_m_tbl set Acc_LienStatus ='N' where ACC_ID ='" & HdnAccID.Value & "'"
        M_DBConn.ExecuteScalar(M_Sql, 0, False)
        ShowAlertMessage("Lien for Acc No  " + TxtAccNo.Text + "  Has been Revoked")
    End Sub
End Class