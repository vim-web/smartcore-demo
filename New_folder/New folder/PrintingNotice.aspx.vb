Imports SMARTCORE_BL
Imports SMARTCORE_Common

Public Class PrintingNotice
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1116
    Shared Obj_UserRights As Object




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
            TxtAsOnDate.Text = Session("TransDate")
            TxtBranch.Attributes.Add("readonly", "readonly")
        End If

    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbLoanTyype, Session("TransDate"), "LOAN")
        M_CommonBL.Fill_Pickup(CmbPrintType, 75, 0)
        M_CommonBL.Fill_Pickup(CmbNoticeType, 64, 0)

        M_CommonBL.Fill_Pickup(Lst_ActExcept, 64, 0)
        M_CommonBL.Fill_Pickup(Lst_ActInclude, 64, 0)

        CmbBranch.SelectedValue = Session("DefaultBrCode")

        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call CmbLoanTyype_SelectedIndexChanged(Me.CmbLoanTyype, System.EventArgs.Empty)
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub ChkActionType_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkActionType.CheckedChanged
        If ChkActionType.Checked = True Then
            DivActExcept.Visible = True
            DivActInclude.Visible = True
        Else
            DivActExcept.Visible = False
            DivActInclude.Visible = False
        End If
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmbNoticeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbNoticeType.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As DataTable

            M_Dt = M_CommonBL.Get_NoticeCodes(CmbLoanTyype.SelectedValue, CmbNoticeType.SelectedValue, Session("TransDate"))

            If M_Dt.Rows.Count > 0 Then
                TxtNoticeAmt.Text = Format(Val(M_Dt.Rows(0)("Amount").ToString), "#0.00")
                TxtMiscAmt.Text = Format(Val(M_Dt.Rows(0)("Misc_Amount").ToString), "#0.00")
                HdnNoticeHead.Value = M_Dt.Rows(0)("Notice_AccCode").ToString
                HdnMiscHead.Value = M_Dt.Rows(0)("Misc_AccCode").ToString
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

    Protected Sub CmbLoanTyype_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanTyype.SelectedIndexChanged
        Call CmbNoticeType_SelectedIndexChanged(Me.CmbNoticeType, System.EventArgs.Empty)
    End Sub

    Protected Sub CmdProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdProcess.Click
        Dim M_CommonBL As New CommonBL
        Dim M_Dt As DataTable
        'M_Dt = M_CommonBL.Process_Notice(CmbBranch.SelectedValue, CmbLoanTyype.SelectedValue, TxtAsOnDate.Text)
        DgvNotice.DataSource = M_Dt
        DgvNotice.DataBind()
    End Sub

    Protected Sub DgvNotice_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvNotice.PageIndexChanging
        DgvNotice.PageIndex = e.NewPageIndex
        DgvNotice.DataBind()
    End Sub
End Class