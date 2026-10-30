Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Partial Public Class NextNumber
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1107
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
    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        M_CommonBL.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, CmbBranch.SelectedValue)
        M_CommonBL = Nothing

    End Sub
    Protected Sub CmbAgent_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbAgent.SelectedIndexChanged
        Call GetNextNo()
    End Sub

    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtSchCode.Text
        TxtSchCode.Text = CmbScheme.SelectedValue
        Call GetNextNo()

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            Call GetNextNo()
            TxtSchCode.Text = CmbScheme.SelectedValue
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, CmbBranch.SelectedValue)
        M_CommonBL = Nothing
    End Sub

    Protected Sub ChkAgent_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAgent.CheckedChanged
        If ChkAgent.Checked Then
            CmbAgent.Enabled = True
        Else
            CmbAgent.Enabled = False
        End If
    End Sub

    Private Sub GetNextNo()
        Try

            Dim M_CommonBL As New CommonBL
            TxtOldNo.Text = M_CommonBL.GetNextAccountNo(CmbBranch.SelectedValue, CmbScheme.SelectedValue, IIf(ChkAgent.Checked, CmbAgent.SelectedValue, "0"))
            M_CommonBL = Nothing

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Call UpdateNextNo()
        ShowAlertMessage("Updated Successfully")
        TxtNewNo.Text = ""
    End Sub


    Private Sub UpdateNextNo()
        Try
            Dim M_CommonBL As New CommonBL
            TxtOldNo.Text = M_CommonBL.UpdateNextAccountNo(CmbBranch.SelectedValue, CmbScheme.SelectedValue, IIf(ChkAgent.Checked, CmbAgent.SelectedValue, "0"), TxtNewNo.Text)
            M_CommonBL = Nothing
        Catch ex As Exception
        End Try
    End Sub


End Class