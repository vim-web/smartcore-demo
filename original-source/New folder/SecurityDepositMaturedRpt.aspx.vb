Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Partial Public Class SecurityDepositMaturedRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1142
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If


                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        TxtFromDate.Text = Session("TransDate")
        TxtToDate.Text = Session("TransDate")
        Dim M_CommonBL As New CommonBL

        M_CommonBL.Fill_Branch(DdlBranch)

        DdlBranch.SelectedValue = Session("DefaultBrCode")
        M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "DEPOSIT", "", "", "TERM1")

        DdlBranch.Items.Add("<-- ALL -->")
        DdlScheme.Items.Add("<-- ALL -->")


    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click

        Try
            If (Date.Parse(TxtFromDate.Text) > Date.Parse(TxtToDate.Text)) Then
                ShowAlertMessage("Please Enter TO Date Greater Than From Date....")
                Exit Sub
            End If

            Dim M_Sql As String
            Dim M_Dt As New DataTable()

            Dim HeaderArr(8, 4) As String
            Dim HeaderWidth(8, 1) As String

            HeaderArr(0, 0) = "SLNO NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "LOAN NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "20"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "LOAN DATE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "LOAN DUE DATE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "DEPOSIT NO"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "15"

            HeaderArr(5, 0) = "DEPOSIT DUE DATE"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "DEPOSIT AMOUNT"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "15"
            HeaderArr(6, 3) = "15"

            HeaderArr(7, 0) = "LOAN BALANCE"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "15"
            HeaderArr(7, 3) = "15"

            Session("RptHeader") = HeaderArr


            M_Sql = "EXEC SecurityDepositMatured_Sp'" & IIf(DdlBranch.SelectedItem.Text = "<-- ALL -->", "<-- ALL -->", DdlBranch.SelectedValue) & "','" & IIf(DdlScheme.SelectedItem.Text = "<-- ALL -->", "<-- ALL -->", DdlScheme.SelectedValue) & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class
