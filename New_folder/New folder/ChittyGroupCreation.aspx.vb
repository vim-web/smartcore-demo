Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass

Public Class ChittyGroupCreation
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Dim FormID As Integer = 1028
    Shared Obj_UserRights As Object



    Private Sub Page_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("GROUP DEPOSIT"))
        M_CommonBL.Fill_AccHead(CmbRctHead)
        M_CommonBL.Fill_AccHead(CmbPayHead)
        M_CommonBL.Fill_AccHead(CmbDisc)
        M_CommonBL.Fill_AccHead(CmbCommission)
        M_CommonBL.Fill_AccHead(CmbDiscFor)
        M_CommonBL.Fill_AccHead(CmbIntHead)
        M_CommonBL.Fill_AccHead(CmbForeman)
        M_CommonBL.Fill_AccHead(CmbFormanDisc)
        M_CommonBL.Fill_AccHead(CmbAdmnFee)

        'CmbAdmnFee.DataSource = CmbRctHead.Items
        'CmbAdmnFee.DataBind()

        CmbRctHead.SelectedValue = "37"
        CmbPayHead.SelectedValue = "37"
        CmbDisc.SelectedValue = "51"
        CmbCommission.SelectedValue = "155"
        CmbDiscFor.SelectedValue = "156"
        CmbIntHead.SelectedValue = "179"
        CmbForeman.SelectedValue = "46"
        CmbFormanDisc.SelectedValue = "154"
        CmbAdmnFee.SelectedValue = "145"

        TxtStartDate.Attributes.Add("readonly", "readonly")

        M_CommonBL = Nothing
        SchemeDt = DirectCast(Session("SchemeTbl"), DataTable)
        AcHeadDt = DirectCast(Session("AcHeadTbl"), DataTable)

        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)

    End Sub

    Private Sub TxtBankForemanAc_TextChanged(ByVal textBox As TextBox, ByVal Empty As EventArgs)

    End Sub


    Protected Sub TxtSuspenseLiabAc_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSuspenseLiabAc.TextChanged
        Dim M_CommonBL As New CommonBL
        HdnSuspAccid.Value = M_CommonBL.GetAccountAccid(TxtSuspenseLiabAc.Text)
        M_CommonBL = Nothing
        If HdnSuspAccid.Value = 0 Then
            ShowAlertMessage("INVALID SUSPENSE ACCOUNT")
        End If
    End Sub
    Private Function Validate_Control() As Boolean

        If HdnSuspAccid.Value = 0 Then
            ShowAlertMessage("INVALID SUSPENSE ACCOUNT")
            Return False
        End If
        If TxtGroupCode.Text.Length = 0 Then
            ShowAlertMessage("INVALID GROUP CODE")
            Return False
        End If
        If TxtGroupName.Text.Length = 0 Then
            ShowAlertMessage("INVALID GROUP NAME")
            Return False
        End If
        If TxtForemanCustID.Text.Length = 0 Then
            ShowAlertMessage("INVALID FOREMAN CUST ID")
            Return False
        End If
        If Val(TxtSala.Text) = 0 Then
            ShowAlertMessage("INVALID CHITTY SALA")
            Return False
        End If
        If Val(TxtNoOfMember.Text) = 0 Then
            ShowAlertMessage("INVALID NO OF MEMBERS")
            Return False
        End If
        If Val(TxtNoOfMonths.Text) = 0 Then
            ShowAlertMessage("INVALID PERIOD")
            Return False
        End If

        If Val(TxtNoOfMonths.Text) <> Val(TxtNoOfMember.Text) Then
            ShowAlertMessage("NO OF MEMBERS AND PERIOD MISMATCH")
            Return False
        End If

        If Val(TxtFinePerNonPrized.Text) = 0 Then
            ShowAlertMessage("INVALID FINE PERCENTAGE")
            Return False
        End If

        If Val(TxtFinePerPrized.Text) = 0 Then
            ShowAlertMessage("INVALID FINE PERCENTAGE")
            Return False
        End If

        If Val(TxtForefeitMth.Text) = 0 Then
            ShowAlertMessage("INVALID FORFEIT PERIOD")
            Return False
        End If

        Return True
    End Function
    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        If Validate_Control = False Then
            Exit Sub
        End If

        Dim M_ChittyGroup As New SMARTCORE_BO.ChittyGroupBO
        With M_ChittyGroup
            .GrpCode = TxtGroupCode.Text
            .NoOfMembers = Val(TxtNoOfMember.Text)
            .NoOfMonths = Val(TxtNoOfMonths.Text)
            .SuspAccid = HdnSuspAccid.Value
            .BankFormanCustID = TxtForemanCustID.Text
            .GrpStartDate = TxtStartDate.Text
            .GrpID = 0
            .SalaAmount = Val(TxtSala.Text)
            .InstalmentAmt = Val(TxtSala.Text) / Val(TxtNoOfMember.Text)
            .BankCharge = Val(TxtCommPer.Text)
            .FinePercentNonPrized = Val(TxtFinePerNonPrized.Text)
            .FinePercentPrized = Val(TxtFinePerPrized.Text)
            .ForfeitMth = Val(TxtForefeitMth.Text)
            .BrCode = CmbBranch.SelectedValue
            .SchCode = CmbScheme.SelectedValue
            .GrpName = TxtGroupName.Text
            .AdmnFeeHead = CmbAdmnFee.SelectedValue
            .RctHead = CmbRctHead.SelectedValue
            .FormanDiscHead = CmbFormanDisc.SelectedValue
            .PayHead = CmbPayHead.SelectedValue
            .DiscHead = CmbDisc.SelectedValue
            .DiscForfeitHead = CmbDiscFor.SelectedValue
            .CommHead = CmbCommission.SelectedValue
            .FineHead = CmbIntHead.SelectedValue
            .FormanHead = CmbForeman.SelectedValue
        End With

        Try
            Session("M_EntryMode") = "NEW"
            Dim M_ChittyGroupBL As New ChittyGroupBL
            If M_ChittyGroupBL.Save_Data(M_ChittyGroup, Session("M_EntryMode")) = True Then
                Clear_Controls()
                ShowAlertMessage("CHITTY GROUP SAVING COMPLETED")
            Else
                ShowAlertMessage("CHITTY GROUP SAVING FAILED")
            End If
            M_ChittyGroupBL = Nothing
        Catch ex As Exception

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
        TxtForemanCustID.Text = ""
        TxtGroupName.Text = ""
        TxtGroupCode.Text = ""
        TxtNoOfMember.Text = ""
        TxtNoOfMonths.Text = ""
        TxtSala.Text = ""
        TxtStartDate.Text = ""
        TxtSuspenseLiabAc.Text = ""
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
        Get_NewID()
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub


    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        Get_NewID()
    End Sub


    Private Sub Get_NewID()
        CmbScheme.SelectedValue = TxtScheme.Text
        Dim GroupDepositMasterBL As New GroupDepositMasterBL
        TxtGroupCode.Text = GroupDepositMasterBL.Get_NewChittyCode(TxtBranch.Text, TxtScheme.Text)
        TxtGroupName.Text = CmbScheme.SelectedItem.Text + " " + TxtGroupCode.Text
    End Sub

End Class