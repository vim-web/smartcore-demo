Imports SMARTCORE_BL
Public Class DeactivateRevoke
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1048
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
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_DeactivateCustomer As New SMARTCORE_BO.DeactivateCustomerBO
        With M_DeactivateCustomer
            .DCusID = 0
            .CustId = TxtCid.Text
            .DCusLogId = 0
            .DCusDeactiveDate = "1/1/1900"
            .DCusRemarkDate = "1/1/1900"
            .DCusRevokeDate = TxtRevokeDate.Text

            .DCusType = CmbDeactiveType.SelectedValue
            .BrCode = Val(TxtBranch.Text)
            .DCusReason = TxtRevokeReason.Text
            .DCusRemarks = ""
            .DCusRevokeRemarks = ""
            .DCusTypeOthers = ""
            .MakerID = Session("Login")
            .CheckerID = Session("Login")
            .Authorised = "Y"
            .DCusStatus = CmbDeactiveType.SelectedItem.Text(0)
        End With

        Dim M_DeactivateCustomerBL As New SMARTCORE_BL.DeactivateCustomerBL

        Session("M_EntryMode") = "NEW"
        M_DeactivateCustomerBL.Save_Data(M_DeactivateCustomer, Session("M_EntryMode"))
    End Sub

    Protected Sub TxtRevokeReason_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtRevokeReason.TextChanged

    End Sub
End Class