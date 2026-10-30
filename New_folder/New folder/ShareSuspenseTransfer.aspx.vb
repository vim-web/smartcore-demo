Imports SMARTCORE_BL

Partial Public Class ShareSuspenseTransfer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(Now.AddSeconds(-1))
            Response.Cache.SetNoStore()
            Response.AppendHeader("Pragma", "no-cache")
        End If
    End Sub

    Private Sub Load_DefaultValues()
        TxtFromDate.Text = Session("TransDate")
        TxtToDate.Text = Session("TransDate")

        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        TxtBranch.Text = CmbBranch.SelectedValue
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("SHARE"), ("SHARE.SUSP"))
        TxtTransferScheme.Text = CmbTransferScheme.SelectedValue
        M_CommonBL.GetSchemes(CmbTransferScheme, Session("TransDate"), ("SHARE"))
        TxtScheme.Text = CmbScheme.SelectedValue

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
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmbTransferScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbTransferScheme.SelectedIndexChanged
        TxtTransferScheme.Text = CmbTransferScheme.SelectedValue
    End Sub

    Protected Sub TxtTransferScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtTransferScheme.TextChanged
        On Error Resume Next
        CmbTransferScheme.SelectedValue = TxtTransferScheme.Text
        TxtTransferScheme.Text = CmbTransferScheme.SelectedValue
    End Sub

    Protected Sub CmdGenerate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdGenerate.Click
        Dim M_ShareSusMasterBL As New ShareSusMasterBL
        Dim M_Datatable As DataTable
        M_Datatable = M_ShareSusMasterBL.Get_ShareSuspAcc(TxtBranch.Text, TxtScheme.Text, TxtFromDate.Text, TxtToDate.Text)
        GvAcc.DataSource = M_Datatable
        GvAcc.DataBind()
    End Sub

    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = DirectCast(GvAcc.HeaderRow.FindControl("chkSelectAll"), CheckBox)
        If chkAll.Checked = True Then
            For Each gvRow As GridViewRow In GvAcc.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = True

            Next
        Else
            For Each gvRow As GridViewRow In GvAcc.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = False
            Next
        End If
    End Sub
End Class