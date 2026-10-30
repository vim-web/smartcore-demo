Imports SMARTCORE_BL

Partial Public Class ShareDividentPosting
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1127
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
        End If
    End Sub

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "SHARE")

            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)

            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)

            Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Set_Grid()
        Try
            DgvDivPost.DataSource = Nothing
            Dim DivPostTbl As New DataTable("Table")
            DivPostTbl.Columns.Add("Mem_No", GetType(String))
            DivPostTbl.Columns.Add("Cust_Name", GetType(String))
            DivPostTbl.Columns.Add("DivAmt", GetType(Double))
            DivPostTbl.Columns.Add("Share_Out", GetType(Double))
            DivPostTbl.Columns.Add("Cust_ID", GetType(Double))
            DivPostTbl.Columns.Add("Acc_ID", GetType(Double))
            DivPostTbl.Rows.Add("", "", 0, 0, 0, 0)
            Session("DivPostTbl") = DivPostTbl
            DgvDivPost.DataSource = DivPostTbl
            DgvDivPost.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBrCode.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtSchCode.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtSchCode.Text
        TxtSchCode.Text = CmbScheme.SelectedValue
        'Set_SchemeCtrl()
    End Sub

    Protected Sub TxtBrCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBrCode.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBrCode.Text
        TxtBrCode.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtFromDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtFromDate.TextChanged
        TxtFromDate.Text = "01/04/" & CDate(TxtFromDate.Text).Year.ToString
        HdnToDate.Value = "31/03/" & (CDate(TxtFromDate.Text).Year + 1).ToString
        LblDateDtls.Text = HdnToDate.Value
    End Sub

    Protected Sub CmdProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdProcess.Click
        Try
            Dim M_ShareBL As New ShareMasterBL
            Dim M_Dt As DataTable
            M_Dt = M_ShareBL.Process_Divident(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtFromDate.Text, HdnToDate.Value, Session("UserID"), Val(TxtPercent.Text))
            Session("DivPostTbl") = M_Dt
            DgvDivPost.DataSource = M_Dt
            DgvDivPost.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_ShareBL As New ShareMasterBL
            Dim M_Dt As DataTable

            M_Dt = M_ShareBL.Process_Divident(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtFromDate.Text, HdnToDate.Value, Session("UserID"), Val(TxtPercent.Text), False, True, Session("TransDate"))

            If M_Dt.Rows.Count > 0 Then
                Response.Redirect("~/NewAccount.aspx?AccNo=DIVIDENT POSING&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=SHARE&SCHEME=" & CmbScheme.SelectedValue & "&SEC=SHARE&DRCR=" & "C")
            Else
                ShowAlertMessage("DIVIDENT POSTING FAILD")
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

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub DgvDivPost_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvDivPost.PageIndexChanging
        DgvDivPost.PageIndex = e.NewPageIndex
    End Sub
End Class