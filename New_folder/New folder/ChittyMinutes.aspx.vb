Imports SMARTCORE_BL
Public Class Minutes
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1029
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
        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_ChittyMinute As New SMARTCORE_BO.ChittyMinutesBO
        With M_ChittyMinute
            .ChittyNo = 0

            .Amount = TxtAmount.Text
            .ChittyGroup = CmbChittyGrp.SelectedValue
            .AuctionNo = TxtAucnNo.Text
            .AccNo = TxtChittyAcNo.Text
            .Name = TxtName.Text
            .Address = TxtAddress.Text
            .MakerId = Session("LoginID")
        End With
        Dim M_ChittyMinuteBL As New SMARTCORE_BL.ChittyMinutesBL
        Session("M_EntryMode") = "NEW"
        M_ChittyMinuteBL.Save_Data(M_ChittyMinute, Session("M_EntryMode"))
    End Sub
End Class