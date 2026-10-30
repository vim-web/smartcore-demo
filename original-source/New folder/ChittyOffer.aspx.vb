Imports SMARTCORE_BL
Imports SMARTCORE_BO

Public Class Offer
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1031
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
            TxtOfrDate.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_ChittyOffer As New SMARTCORE_BO.ChittyOfferBO
        With M_ChittyOffer
            .OfferId = 0
            .OfferDate = TxtOfrDate.Text
            .AccNo = TxtChittyAcNo.Text
            .Name = TxtName.Text
            .Address = TxtAddress.Text
            .ModeOfReceipt = CmbModeOfRecpt.SelectedValue
            .Details = TxtDetails.Text
            .MakerId = Session("LoginID")
        End With
        Dim M_ChittyOfferBL As New SMARTCORE_BL.ChittyOfferBL

        Session("M_EntryMode") = "NEW"
        M_ChittyOfferBL.Save_Data(M_ChittyOffer, Session("M_EntryMode"))

    End Sub
    Private Sub Clear_Controls()
        TxtAddress.Text = ""
        TxtChittyAcNo.Text = ""
        TxtDetails.Text = ""
        TxtName.Text = ""
        TxtOfrDate.Text = ""
        TxtOfrNo.Text = ""
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()

    End Sub

    Protected Sub TxtOfrNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtOfrNo.TextChanged
        Dim ChittyOfferBO As New ChittyOfferBO
        Dim ChittyOfferBL As New SMARTCORE_BL.ChittyOfferBL

        If TxtOfrNo.Text.Trim.Length > 0 Then
            ChittyOfferBO = ChittyOfferBL.Locate_Data(TxtOfrNo.Text)
            With ChittyOfferBO
                If .OfferId > 0 Then
                    TxtOfrDate.Text = .OfferDate
                    TxtChittyAcNo.Text = .AccNo
                    TxtName.Text = .Name
                    TxtAddress.Text = .Address
                    CmbModeOfRecpt.SelectedValue = .ModeOfReceipt
                    TxtDetails.Text = .Details

                End If
            End With
        End If
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class