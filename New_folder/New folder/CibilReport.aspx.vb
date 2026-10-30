Imports SMARTCORE_BL
Imports SMARTCORE_Common
Public Class CibilReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/LoginPage.aspx")
                Exit Sub
            End If

            'Dim M_Comm As New CommonBL
            'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            'M_Comm = Nothing
            'If Obj_UserRights.View_Right = False Then
            '    Response.Redirect("~/UnauthorisedAcces.ASPX")
            'End If


            If Not IsPostBack Then
                Load_DefaultValues()

            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try

            TxtAsOnDate.Text = Session("TransDate")

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonBL As New CommonBL





        M_Sql = "EXEC [Get_Monthly_Rpt_Sp] " & CommonClass.DbDate(TxtAsOnDate.Text) & ",'" & DDL_ReportType.SelectedValue & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
        ' M_Dt = M_CommonBL.Get_CibilReport(CommonClass.DbDate(TxtAsOnDate.Text), DDL_ReportType.SelectedValue)
        'GridView1.DataSource = M_Dt
        'GridView1.DataBind()
        'Dim attachment As String = "attachment; filename=Report.xlsx"
        'Response.ClearContent()
        'Response.AddHeader("content-disposition", attachment)
        'Response.ContentType = "application/vnd.ms-excel"
        'Dim sWriter As New System.IO.StringWriter()
        'Dim htwWriter As New HtmlTextWriter(sWriter)
        'GridView1.RenderControl(htwWriter)
        'Response.Write(sWriter.ToString())
        'Response.[End]()

        'ExportDiv.Visible = True
        'M_Sql = "EXEC GramRateRPT_Sp '%" & str & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "'"
        'Session("RptQry") = M_Sql
        'Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub
End Class