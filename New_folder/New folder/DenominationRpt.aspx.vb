Imports SMARTCORE_BL
Imports SMARTCORE_Common
Public Class DenominationRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then

                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            'M_CommonBL.Fill_UserEmployee(Cmb_teller)

            TxtFromDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
            CmbBranch.Items.Add("ALL")
            CmbBranch.SelectedValue = Session("DefaultBrCode")

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

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

        Dim M_Sql As String
        Dim M_Dt As New DataTable()

            Dim HeaderArr(20, 4) As String
            Dim HeaderWidth(20, 1) As String

            HeaderArr(0, 0) = "SLNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "BRANCH NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "DATE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "5"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "RS 2000"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "RS 1000"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "15"

            HeaderArr(5, 0) = "RS 500"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "15"

            HeaderArr(6, 0) = "RS 200"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "15"

            HeaderArr(7, 0) = "RS 100"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "15"

            HeaderArr(8, 0) = "RS 50"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "10"

            HeaderArr(9, 0) = "RS 20"
            HeaderArr(9, 1) = "R"
            HeaderArr(9, 2) = "10"
            HeaderArr(9, 3) = "10"

            HeaderArr(10, 0) = "RS 10"
            HeaderArr(10, 1) = "R"
            HeaderArr(10, 2) = "10"
            HeaderArr(10, 3) = "10"

            HeaderArr(11, 0) = "RS 5"
            HeaderArr(11, 1) = "R"
            HeaderArr(11, 2) = "10"
            HeaderArr(11, 3) = "10"

            HeaderArr(12, 0) = "COIN 5"
            HeaderArr(12, 1) = "R"
            HeaderArr(12, 2) = "10"
            HeaderArr(12, 3) = "10"

            HeaderArr(13, 0) = "COIN 2"
            HeaderArr(13, 1) = "R"
            HeaderArr(13, 2) = "10"
            HeaderArr(13, 3) = "10"

            HeaderArr(14, 0) = "COIN 1"
            HeaderArr(14, 1) = "R"
            HeaderArr(14, 2) = "10"
            HeaderArr(14, 3) = "10"

            HeaderArr(15, 0) = "OTHERS"
            HeaderArr(15, 1) = "R"
            HeaderArr(15, 2) = "10"
            HeaderArr(15, 3) = "10"

            HeaderArr(16, 0) = "CLOSE BALANCE"
            HeaderArr(16, 1) = "R"
            HeaderArr(16, 2) = "10"
            HeaderArr(16, 3) = "10"

            HeaderArr(17, 0) = "DENOMINATION"
            HeaderArr(17, 1) = "R"
            HeaderArr(17, 2) = "10"
            HeaderArr(17, 3) = "10"

            HeaderArr(18, 0) = "MAKING TIME"
            HeaderArr(18, 1) = "L"
            HeaderArr(18, 2) = "10"
            HeaderArr(18, 3) = "10"

            HeaderArr(19, 0) = "USER ID"
            HeaderArr(19, 1) = "L"
            HeaderArr(19, 2) = "10"
            HeaderArr(19, 3) = "10"

        Session("RptHeader") = HeaderArr

        M_Sql = "Get_Denomination_Details_Sp '" & CmbBranch.SelectedValue & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ""
        Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class