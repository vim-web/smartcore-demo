Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports System.Windows
Partial Public Class LoanRate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txt_date.Text = Session("TransDate")
            LoadDefaultValues()
        End If

    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_save.Click
        Dim ds As DataSet
        Try
            Dim EntryMode As String
            Dim M_dt As New DataTable
            Dim M_CommBL As New CommonBL
            ds = M_CommBL.Get_LoanPercentage(CmbBranch.SelectedValue, txt_date.Text, Cmbscheme.SelectedValue)
            M_dt = ds.Tables(0)
            If M_dt.Rows.Count > 0 Then
                Session("EntryMode") = "UPDATE"
            Else
                Session("EntryMode") = "NEW"
            End If
            M_dt = M_CommBL.Save_LoanPecentage(Cmbscheme.SelectedValue, CmbBranch.SelectedValue, txt_date.Text, txt_rate.Text, Txt_BankRate.Text, Session("UserID"), Session("EntryMode"))

            Set_Grid()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadDefaultValues()
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable

            M_CommonBl.Fill_Branch(CmbBranch)
            CmbBranch.Items.Add("ALL")
            M_Dt = M_CommonBl.GenLoanScheme()
            Cmbscheme.DataTextField = "Sch_Name"
            Cmbscheme.DataValueField = "Sch_Code"
            Cmbscheme.DataSource = M_Dt
            Cmbscheme.DataBind()

            Set_Grid()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Set_Grid()
        Dim ds As DataSet
        Try

            Dim M_dt As New DataTable
            Dim M_CommBL As New CommonBL
            ds = M_CommBL.Get_LoanPercentage(CmbBranch.SelectedValue, txt_date.Text, Cmbscheme.SelectedValue)
            M_dt = ds.Tables(0)
            If M_dt.Rows.Count > 0 Then
                txt_rate.Text = M_dt.Rows(0).Item("MAXIMUM_ELIGIBLE")
                Txt_BankRate.Text = M_dt.Rows(0).Item("PERCENTAGE")
                ''CmbBranch.SelectedValue = "ALL"


                GvRpt.DataSource = M_dt
                GvRpt.DataBind()
            End If
            Return
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_date_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_date.TextChanged
        Try
            Set_Grid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Cmbscheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cmbscheme.SelectedIndexChanged
        Set_Grid()
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Set_Grid()
    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_clear.Click
        Try
            Txt_BankRate.Text = ""
            txt_rate.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_close.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class