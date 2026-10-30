Imports SMARTCORE_BL
Partial Public Class other
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadDefaultValues()
            TxtCash.Text = 0.0
            TxtTrans.Text = 0.0

        End If
    End Sub

    Private Sub Grd_fill()
        Try
            Dim M_dt As New DataTable
            M_dt.Columns.Add("Particular")
            M_dt.Columns.Add("Rct_Cash")
            M_dt.Columns.Add("Rct_tran")
            M_dt.Columns.Add("pay_cash")
            M_dt.Columns.Add("Pay_tran")
            Session("Set_Grid") = M_dt
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadDefaultValues()
        Try
            Dim M_CmmnBl As New CommonBL
            Dim M_dt As New DataTable
            M_dt = M_CmmnBl.CmbService()
            CmbCompany.DataTextField = "Name"
            CmbCompany.DataValueField = "Service_id"
            CmbCompany.DataSource = M_dt
            CmbCompany.DataBind()
            CmbService.DataTextField = "Type_Name"
            CmbService.DataValueField = "Acc_code"
            M_dt = M_CmmnBl.CmbServiceType(CmbCompany.SelectedValue)
            CmbService.DataSource = M_dt
            CmbService.DataBind()
            CmbTranTp.Items.Add("Credit")
            CmbTranTp.Items.Add("Debit")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Try
            Dim M_Datatable As New DataTable
            Dim ParameterDt As New DataTable
            Dim M_CommnBL As New CommonBL
            Dim RctCash, RctTran, payCash, payTran As Double
            Grd_fill()
            ParameterDt = M_CommnBL.OtherTranGrd(CmbService.SelectedValue)

            If CmbTranTp.SelectedValue = "Credit" Then
                If TxtCash.Text > 0 Then

                    payCash = 0
                    payTran = 0
                    RctTran = 0
                    RctCash = TxtCash.Text

                Else
                    payCash = 0
                    payTran = 0
                    RctCash = 0
                    RctTran = TxtTrans.Text
                End If
            Else

                If TxtTrans.Text > 0 Then

                    RctCash = 0
                    RctTran = 0
                    payCash = 0
                    payTran = TxtTrans.Text
                Else
                    RctTran = 0
                    RctCash = 0
                    payTran = 0
                    payCash = TxtCash.Text

                End If
            End If

            M_Datatable = Session("Set_Grid")
            M_Datatable.Rows.Add(ParameterDt.Rows(0).Item("Acc_Name"), RctCash, RctTran, payCash, payTran)
            DgvTransaction.DataSource = M_Datatable
            DgvTransaction.DataBind()
            Session("Set_Grid") = M_Datatable
            TxtCash.Text = "0.00"
            TxtCash.Text = "0.00"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCompany.SelectedIndexChanged
        Try
            Dim M_CmmnBl As New CommonBL
            Dim M_dt As New DataTable
            M_dt = M_CmmnBl.CmbServiceType(CmbCompany.SelectedValue)
            CmbService.DataSource = M_dt
            CmbService.DataBind()
        Catch ex As Exception

        End Try
    End Sub

End Class