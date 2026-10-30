Imports SMARTCORE_BL
Public Class DeactivateCustomer
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1047
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
            CmbDeactiveType.Attributes.Add("onChange", "return DeactiveType_Validate();")
            Cmbbranch.Attributes.Add("onChange", "return Branch_Validate();")
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(Cmbbranch)
        TxtDeactivateDate.Attributes.Add("readonly", "readonly")
        TxtInformationDate.Attributes.Add("readonly", "readonly")

        Call CmbBranch_SelectedIndexChanged(Me.Cmbbranch, System.EventArgs.Empty)
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = Cmbbranch.SelectedValue
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        Cmbbranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = Cmbbranch.SelectedValue
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_DeactivateCustomer As New SMARTCORE_BO.DeactivateCustomerBO
            With M_DeactivateCustomer
                .DCusID = 0
                .CustId = TxtCid.Text
                .DCusLogId = 0
                .DCusDeactiveDate = TxtDeactivateDate.Text
                .DCusRemarkDate = "1/1/1900"
                .DCusRevokeDate = "1/1/1900"
                .DCusType = CmbDeactiveType.SelectedValue
                .BrCode = Val(TxtBranch.Text)
                .DCusReason = TxtDeactivatereason.Text
                .DCusRemarks = TxtRemarks.Text
                .DCusRevokeRemarks = ""
                .DCusTypeOthers = ""
                .MakerID = Session("Login")
                .CheckerID = Session("Login")
                .Authorised = "Y"
                .DCusStatus = CmbDeactiveType.SelectedItem.Text(0)
            End With

            Dim M_DeactivateCustomerBL As New SMARTCORE_BL.DeactivateCustomerBL

            Session("M_EntryMode") = "NEW"

            If M_DeactivateCustomerBL.Save_Data(M_DeactivateCustomer, Session("M_EntryMode")) > 0 Then
                ShowAlertMessage("CUSTOMER DEACTIVATE SAVING COMPLETED")
                Call Clear_Controls()
            Else
                ShowAlertMessage("CUSTOMER DEACTIVATE SAVING FAILED")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Clear_Controls()
        TxtAddress.Text = ""
        TxtCid.Text = ""
        TxtDeactivateDate.Text = ""
        TxtDeactivatereason.Text = ""
        TxtInformationDate.Text = ""
        TxtName.Text = ""
        TxtOther.Text = ""
        TxtRemarks.Text = ""
        CmbDeactiveType.SelectedIndex = 0
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Private Function Load_CustomerData() As Boolean
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_CustMasterBL As New CustMasterBL

            M_CustMaster = M_CustMasterBL.Locate_Data(Val(TxtCID.Text), M_PresAddress, M_PermAddress)

            HdnAlert.Value = ""
            If M_CustMaster.CustId = 0 Then
                ShowAlertMessage("Invalid Customer, Please Enter Customer ID")
                HdnAlert.Value = "Invalid Customer, Please Enter Customer ID"
                TxtCID.Focus()
                Return False
            End If

            TxtName.Text = M_CustMaster.CustName
            With M_PermAddress
                TxtAddress.Text = .AddAddress1 + vbCrLf + .AddAddress2 + vbCrLf + .AddAddress3 + vbCrLf + .AddPostoffice + vbCrLf + .AddDist + vbCrLf + .AddPinCode
            End With

            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
        Return True
    End Function

    Protected Sub TxtCid_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCid.TextChanged
        TxtCid.ToolTip = TxtCid.Text
        Clear_Controls()
        TxtCid.Text = TxtCid.ToolTip
        TxtCid.ToolTip = ""

        Load_CustomerData()

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class