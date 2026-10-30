Imports SMARTCORE_BL
Imports System.Drawing
Imports System.Data
Partial Public Class LoanEMIChart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnAccid.Value = Request.QueryString("AccID")
            Load_EMI()
        End If
    End Sub

    Private Sub Load_EMI()
        Dim Dt As New DataTable
        Dim M_CommonBL As New CommonBL
        Dim Dt1 As New DataTable

        If Val(hdnAccid.Value) = 999999999 Then
            Dt = DirectCast(Session("EMITbl"), DataTable)
            DgvEMI.DataSource = Dt
            Session("PaidInstNo") = "0"
        Else
            DgvEMI.DataSource = M_CommonBL.Get_EMIChart(Val(hdnAccid.Value))
            Dt1 = M_CommonBL.Get_EmiDetails(Val(hdnAccid.Value), Session("TransDate"))
            'If Val(Dt1.Rows(0).Item("InstNo")) > 0 Then
            session("PaidInstNo") = Dt1.Rows(0).Item("InstNo").ToString()
        End If



        DgvEMI.DataBind()
        M_CommonBL = Nothing
    End Sub
    Protected Sub DgvEMI_OnRowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvEMI.RowDataBound

        If val(e.Row.Cells(0).Text) <= val(session("PaidInstNo")) Then
            e.Row.BackColor = System.Drawing.Color.Aquamarine
        End If

    End Sub
End Class