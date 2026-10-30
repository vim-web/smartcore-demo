Imports SMARTCORE_BL
Partial Public Class AgentCommission
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1007
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If


            Dim M_CommonBL As New CommonBL
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "DEPOSIT", "DD")
            M_CommonBL.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, Session("DefaultBrCode"))
            Txt_FromDate.Attributes.Add("readonly", "readonly")
            Txt_ToDate.Attributes.Add("readonly", "readonly")
            Txt_ToDate.Text = Session("TransDate")
            Txt_FromDate.Text = "01/" + Right(Session("TransDate"), 7)
        End If
    End Sub


    Protected Sub CmdShowDtls_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdShowDtls.Click
        Dim dt As New DataTable
        Dim M_DepositMasterBL As New DepositMasterBL
        Dim M_ColAmt As Double
        dt = M_DepositMasterBL.Calc_CommAmt(Session("DefaultBrCode"), CmbScheme.SelectedValue, CmbAgent.SelectedValue, Txt_FromDate.Text, Txt_ToDate.Text, Val(Txt_CommissionPer.Text), M_ColAmt)
        Session("CommDt") = dt
        DgvTransaction.DataSource = dt
        DgvTransaction.DataBind()
        Get_Total()
        Txt_Collection.Text = Format(M_ColAmt, "#0.00")
        Dim M_ComAmt As Double
        M_ComAmt = M_ColAmt * Val(Txt_CommissionPer.Text) / 100
        M_ComAmt = Int(M_ComAmt + 0.501)
        Txt_CommissionAmt.Text = Format(M_ComAmt, "#0.00")
    End Sub



    Private Function Get_Total() As DataTable
        On Error Resume Next
        Dim M_CommonBL As New CommonBL
        Dim Trandt As DataTable = DirectCast(Session("CommDt"), DataTable)
        Dim objSum As Object
        If Trandt.Rows.Count > 0 Then
            objSum = Trandt.Compute("Sum(RctCash)", "")
            LblRctCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = Trandt.Compute("Sum(RctTransfer)", "")
            LblRctTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = Trandt.Compute("Sum(PayCash)", "")
            LblPayCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = Trandt.Compute("Sum(PayTransfer)", "")
            LblPayTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
        End If

        Return Trandt
    End Function

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_CommonBL As New CommonBL
        Dim M_Dt As New DataTable
        Dim Tran_No As String
        Dim Trandt As DataTable = DirectCast(Session("CommDt"), DataTable)
        Dim M_TranSw As New IO.StringWriter
        Trandt.WriteXml(M_TranSw)
        M_Dt = M_CommonBL.Update_AccNoTran(Session("DefaultBrCode"), "", Session("TransDate"), Session("DefaultBrCode"), "Commision from " & CmbAgent.SelectedItem.Text & "" & Txt_FromDate.Text & " to " & Txt_ToDate.Text, Session("LoginID"), 0, "", M_TranSw.ToString, , , "AGCOM", , , CmbAgent.SelectedValue, , , , , "AGCOM", "AGCOM", "Transfer")

        If M_Dt.Rows.Count > 0 Then
            Tran_No = M_Dt.Rows(0)("Tran_No").ToString()
            ShowAlertMessage("UPDATED")
            'Response.Redirect("~/NewAccount.aspx?AccNo=" & "AGENT COMMISSION" & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & "VOU" & "&SEC=" & "GENERAL" & "&DRCR=B" & "&TRANCATG=" & "TRAN")
        Else
            ShowAlertMessage("FAILED")
        End If

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmbAgent_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbAgent.SelectedIndexChanged

    End Sub
End Class