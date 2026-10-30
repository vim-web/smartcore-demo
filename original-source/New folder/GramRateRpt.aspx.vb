Imports SMARTCORE_BL
Imports SMARTCORE_Common

Partial Public Class GramRateRpt
    Inherits System.Web.UI.Page
    Shared Obj_UserRights As Object
    Dim FormID As Integer = 1001

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/LoginPage.aspx")
                Exit Sub
            End If

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If


            If Not IsPostBack Then
                Load_DefaultValues()

            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL

            M_CommonBL.GetSchemes(Cmbscheme, Session("TransDate"), "LOAN", "GOLDLOAN")
            'Call Cmbscheme_SelectedIndexChanged(Me.Cmbscheme, System.EventArgs.Empty)
            Cmbscheme.Items.Add("ALL")
            TxtFromDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim M_Dt As New DataTable()

        Dim HeaderArr(4, 4) As String
        Dim HeaderWidth(4, 1) As String
        HeaderArr(0, 0) = "SCHEME NAME"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "25"
        HeaderArr(0, 3) = "20"

        HeaderArr(1, 0) = "DATE"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "25"
        HeaderArr(1, 3) = "20"

        HeaderArr(2, 0) = "MARKET RATE"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "25"
        HeaderArr(2, 3) = "20"

        HeaderArr(3, 0) = "BANK RATE"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "25"
        HeaderArr(3, 3) = "20"


        Session("RptHeader") = HeaderArr
        Dim str As String
        If (Cmbscheme.SelectedItem.Text = "ALL") Then
            str = ""
        Else
            str = Cmbscheme.SelectedValue
        End If

        M_Sql = "EXEC GramRateRPT_Sp '%" & str & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
End Class