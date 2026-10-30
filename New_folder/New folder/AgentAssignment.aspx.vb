Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Public Class AgentAssignment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
            Set_SchemeCtrl()
            Session("SearchType") = "ACCOUNTNO"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

        End If
        If Session("SearchResult") <> "" Then
            TxtAccNo.Text = Session("SearchResult")
            TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
            Session("SearchResult") = ""
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBrCode)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        M_CommonBL.Fill_Agent(CmbAgent, 1)
        CmbBrCode.SelectedValue = Session("DefaultBrCode")
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
    End Sub
     Private Sub Set_SchemeCtrl()
        Try
            TxtAccNo.Text = Right("000" + CmbBrCode.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)

        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub TxtShortCode_TextChanged(sender As Object, e As EventArgs) Handles TxtShortCode.TextChanged
        Try
            TxtAccNo.Text = Right("000" + CmbBrCode.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
            Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNo.Text
            Dim Dt_AcDtls As New DataTable
            Dim M_Dt As New DataTable
            Dim M_Sql As String
            Dim M_DbConn As New Connection
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBrCode.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                HdnAccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString
                M_Sql = "Select Acc_AgentId from Accmain_M_Tbl where acc_id='" & HdnAccId.Value & "'"
                M_DbConn.ExecuteDataTable(M_Dt, M_Sql, False)
                If M_Dt.Rows.Count > 0 Then
                    CmbAgent.SelectedValue = M_Dt.Rows(0)("Acc_AgentId").ToString
                End If

            Else
                ShowAlertMessage("Invalid Account No")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TxtScheme_TextChanged(sender As Object, e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Set_SchemeCtrl()
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
        CmbScheme.SelectedValue = TxtScheme.Text
        Section_Name()
        Set_SchemeCtrl()
    End Sub
    Private Function Section_Name() As String
        Dim M_Sql As String
        Dim M_Dbconn As New Connection
        M_Sql = "Select Sch_Section_Name from Gen_SchemeMaster_P_Tbl where sch_code='" & CmbScheme.SelectedValue & "'"
        Session("SchSection") = M_Dbconn.ExecuteScalar(M_Sql, 0, False)
        Return Session("SchSection")
    End Function

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_Dbconn As New Connection
            Dim M_Sql As String
            Dim M_Sql2 As String
            If Session("SchSection") = "DEPOSIT" Then
                M_Sql = "update accmain_m_tbl set Acc_AgentId='" & CmbAgent.SelectedValue & "' where acc_id='" & HdnAccId.Value & "'"
                M_Dbconn.ExecuteScalar(M_Sql, 0, False)
                M_Sql2 = "update dep_master_m_tbl set dm_agent_code='" & CmbAgent.SelectedValue & "' where acc_id='" & HdnAccId.Value & "'"
                M_Dbconn.ExecuteScalar(M_Sql2, 0, False)
            ElseIf Session("SchSection") = "LOAN" Then
                M_Sql = "update accmain_m_tbl set Acc_AgentId='" & CmbAgent.SelectedValue & "' where acc_id='" & HdnAccId.Value & "'"
                M_Dbconn.ExecuteScalar(M_Sql, 0, False)
                M_Sql2 = "update Loan_Master_M_Tbl set Lm_Agent_Code='" & CmbAgent.SelectedValue & "' where acc_id='" & HdnAccId.Value & "'"
                M_Dbconn.ExecuteScalar(M_Sql2, 0, False)
            Else
                M_Sql = "update accmain_m_tbl set Acc_AgentId='" & CmbAgent.SelectedValue & "' where acc_id='" & HdnAccId.Value & "'"
                M_Dbconn.ExecuteScalar(M_Sql, 0, False)
            End If
            ShowAlertMessage("Agent Code added")
        Catch ex As Exception
            ShowAlertMessage("Please select Agent Code")
        End Try
        


    End Sub
    Private Sub clear()
        TxtAddress.Text = ""
        TxtName.Text = ""
        TxtShortCode.Text = ""
        CmbAgent.SelectedIndex = 0
        CmbScheme.SelectedIndex = 0


    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        clear()
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class