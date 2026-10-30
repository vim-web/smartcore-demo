Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class CompanyPosition
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Dim M_commonBL As New CommonBL
                M_commonBL.Fill_Branch(Ddl_Branch)
                Ddl_Branch.SelectedValue = Session("DefaultBrCode")
                Ddl_Branch.Items.Add("ALL")
                TxtAson.Text = Session("TransDate")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_ds As DataSet
            Dim M_dt1 As DataTable
            Dim M_Dt2 As DataTable
            Dim M_Dt3 As DataTable
            Dim M_Dt4 As DataTable
            Dim M_Dt5 As DataTable
            'Dim M_Dt6 As DataTable
            M_ds = M_CommonBl.Get_CompanyPosition(Ddl_Branch.SelectedValue, Session("HostID"))

            M_dt1 = M_ds.Tables(0)
            M_Dt2 = M_ds.Tables(1)
            M_Dt3 = M_ds.Tables(2)
            M_Dt4 = M_ds.Tables(3)
            M_Dt5 = M_ds.Tables(4)
            'M_Dt6 = M_ds.Tables(5)

            If M_dt1.Rows.Count > 0 Then
                GvLoan.DataSource = M_dt1
                GvLoan.DataBind()
            End If
            If M_Dt2.Rows.Count > 0 Then
                GvDeposit.DataSource = M_Dt2
                GvDeposit.DataBind()
            End If
            If M_Dt3.Rows.Count > 0 Then
                GvShare.DataSource = M_Dt3
                GvShare.DataBind()
            End If
            If M_Dt4.Rows.Count > 0 Then
                GvSuspense.DataSource = M_Dt4
                GvSuspense.DataBind()
            End If
            If M_Dt5.Rows.Count > 0 Then
                GV_Other.DataSource = M_Dt5
                GV_Other.DataBind()
            End If
            'If M_Dt6.Rows.Count > 0 Then
            '    GvSuspense.DataSource = M_Dt6
            '    GvSuspense.DataBind()
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GvLoan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvLoan.RowDataBound
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Try
            Dim M_CommonBl As New CommonBL
            Dim Mdt As New DataTable

            Mdt = M_CommonBl.Process_CompanyPosition(Ddl_Branch.SelectedValue, TxtAson.Text, Session("HostID"))
            ShowAlertMessage("PROCEES COMPLETED")
        Catch ex As Exception

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
End Class