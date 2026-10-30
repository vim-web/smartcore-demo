Imports SMARTCORE_BL
Partial Public Class NomDtls
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1108

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnAccid.Value = Request.QueryString("AccID")
            Load_AccHldrs()
        End If
    End Sub

    Private Sub Load_AccHldrs()
        Dim Dt As New DataTable
        Dim M_CommonBL As New CommonBL
        DgvNominee.DataSource = M_CommonBL.Get_NomineeDtls(Val(hdnAccid.Value))
        DgvNominee.DataBind()
    End Sub

End Class