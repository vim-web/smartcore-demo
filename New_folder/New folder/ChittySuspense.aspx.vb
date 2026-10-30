Imports SMARTCORE_BL

Public Class ChittySuspense
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1035
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
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(Now.AddSeconds(-1))
            Response.Cache.SetNoStore()
            Response.AppendHeader("Pragma", "no-cache")
        End If
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        TxtBranch.Text = CmbBranch.SelectedValue
        Dim M_ChittyGroupBL As New ChittyGroupBL
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("GROUP DEPOSIT"))
        TxtScheme.Text = CmbScheme.SelectedValue
        M_ChittyGroupBL.Fill_ChittyGrp(CmbGroup, Session("DefaultBrCode"), TxtScheme.Text)
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

    Protected Sub CmbGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbGroup.SelectedIndexChanged
        Dim M_ChittyGroupBL As New ChittyGroupBL
        Dim M_Datatable As DataTable
        M_Datatable = M_ChittyGroupBL.Get_ChittyGrpDtls(TxtBranch.Text, TxtScheme.Text, CmbGroup.SelectedValue)
        If M_Datatable.Rows.Count > 0 Then
            HdnSuspAccid.Value = M_Datatable.Rows(0).Item("SuspAccid")
            HdnAdmnFee.Value = M_Datatable.Rows(0).Item("AdmnFeeHead")

        End If
    End Sub

    Protected Sub CmdGenerate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdGenerate.Click
        Dim M_ChittyGroupBL As New ChittyGroupBL
        Dim M_Datatable As DataTable
        M_Datatable = M_ChittyGroupBL.Get_GenerateAcc(TxtBranch.Text, TxtScheme.Text, CmbGroup.SelectedValue, Val(TxtAdmnFee.Text), Session("TransDate"), Session("UserID"), DateTime.Now, "N")
        GvAcc.DataSource = M_Datatable
        GvAcc.DataBind()
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub CmdTransfer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdTransfer.Click
        Dim M_ChittyGroupBL As New ChittyGroupBL
        Dim M_Dt As New DataTable
        Dim Tran_No As String
        M_Dt = M_ChittyGroupBL.Get_GenerateAcc(TxtBranch.Text, TxtScheme.Text, CmbGroup.SelectedValue, Val(TxtAdmnFee.Text), Session("TransDate"), Session("UserID"), DateTime.Now, "Y")
        If M_Dt.Rows.Count > 0 Then
            Tran_No = M_Dt.Rows(0)("Tran_No").ToString()
            Response.Redirect("~/NewAccount.aspx?AccNo=" & CmbScheme.SelectedItem.Text & "" & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & "" & "&DRCR=" & "C" & "&TRANCATG=" & "TRAN")
        Else
            ShowAlertMessage("POSTED")
        End If
    End Sub
End Class