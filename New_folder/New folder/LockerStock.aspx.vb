Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Public Class LockerStock
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1100
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            TxtInstalatnDate.Attributes.Add("readonly", "readonly")
            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_LockerDetails As New SMARTCORE_BO.LockerDetailsBO

        Try
            With M_LockerDetails
                .LrdCode = Val(TxtLockerCode.Text)
                .LrdTotalNo = Val(TxtNoOfLockr.Text)
                .LogID = 0
                .LrdInstalldate = TxtInstalatnDate.Text
                .LrdWidth = Val(TxtWidth.Text)
                .LrdHeight = Val(TxtHeight.Text)
                .LrdDepth = Val(TxtDepth.Text)
                .BrCode = Val(TxtBranch.Text)
                .LrdManufacturer = TxtManufacturer.Text
                .LrdType = TxtLockerType.Text
                .LrdFromNo = Val(TxtFrom.Text)
                .LrdToNo = Val(TxtTo.Text)
                .MakerID = Session("LoginId")
                .CheckerID = Session("LoginId")
            End With
            Dim M_LockerDetailsBL As New LockerDetailsBL
            Dim M_StID As Integer

            Session("M_EntryMode") = "NEW"
            M_StID = M_LockerDetailsBL.Save_Data(M_LockerDetails, Session("M_EntryMode"))
            If (M_StID > 0) Then
                ShowAlertMessage("LOCKER STOCK SAVING COMPLETED, LOCKER CODE : " + M_StID.ToString)
                Clear_Controls()
            Else
                ShowAlertMessage("LOCKER STOCK SAVING FAILED")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Clear_Controls()
        TxtDepth.Text = ""
        TxtFrom.Text = ""
        TxtHeight.Text = ""
        TxtInstalatnDate.Text = ""
        TxtLockerCode.Text = ""
        TxtLockerType.Text = ""
        TxtManufacturer.Text = ""
        TxtNoOfLockr.Text = ""
        TxtTo.Text = ""
        TxtWidth.Text = ""
    End Sub

    Private Sub TxtLockerCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtLockerCode.TextChanged
        Try
            Dim LockerBo As New SMARTCORE_BO.LockerDetailsBO
            Dim LockerBL As New LockerDetailsBL
            Dim LkrCode As Integer

            LkrCode = Val(TxtLockerCode.Text)
            Call Clear_Controls()
            TxtLockerCode.Text = LkrCode

            If TxtLockerCode.Text.Trim.Length > 0 Then

                LockerBo = LockerBL.Locate_Data(TxtLockerCode.Text)

                With LockerBo
                    If .LrdCode > 0 Then
                        CmbBranch.SelectedValue = .BrCode
                        TxtNoOfLockr.Text = .LrdTotalNo
                        TxtInstalatnDate.Text = .LrdInstalldate
                        TxtWidth.Text = .LrdWidth
                        TxtHeight.Text = .LrdHeight
                        TxtDepth.Text = .LrdDepth
                        TxtBranch.Text = .BrCode
                        TxtManufacturer.Text = .LrdManufacturer
                        TxtLockerType.Text = .LrdType
                        TxtFrom.Text = .LrdFromNo
                        TxtTo.Text = .LrdToNo
                    Else
                        ShowAlertMessage("Invalid Locker Code, Please Check")
                        TxtLockerCode.Text = ""
                        TxtLockerCode.Focus()
                    End If
                End With
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
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