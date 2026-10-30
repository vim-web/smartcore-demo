Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BL
Partial Public Class DayEndStatus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Datatable As New DataTable()
        Dim M_commonBL As New CommonBL()
        M_Datatable = M_commonBL.Get_BranchDayEndSts()
        Grid_DayEndrpt.DataSource = M_Datatable
        Grid_DayEndrpt.DataBind()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class