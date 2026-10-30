Imports SMARTCORE_BL
Partial Public Class ChiityDtls
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1023

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnAccid.Value = Request.QueryString("AccID")
            Load_ChittyDtls()
        End If
    End Sub

    Private Sub Load_ChittyDtls()
        Dim Ds As New DataSet
        Dim Dt As New DataTable
        Dim M_DepositMasterBL As New DepositMasterBL
        Ds = M_DepositMasterBL.Set_ChittyDtls(Val(hdnAccid.Value), Session("TransDate"), 99, , "Y")
        DgvChitty.DataSource = Ds.Tables(0)
        DgvChitty.DataBind()
    End Sub

    Protected Sub DgvChitty_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvChitty.SelectedIndexChanged

    End Sub
End Class