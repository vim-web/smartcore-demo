Imports SMARTCORE_BL
Partial Public Class GoldItems
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1066

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnAccid.Value = Request.QueryString("AccID")
            Load_AccHldrs()
        End If
    End Sub

    Private Sub Load_AccHldrs()
        Dim Dt As New DataTable
        Dim M_CommonBL As New CommonBL
        DgvJointAc.DataSource = M_CommonBL.Get_GoldItems(Val(hdnAccid.Value))
        DgvJointAc.DataBind()
        M_CommonBL = Nothing
    End Sub

End Class