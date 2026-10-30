Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class LOANARCEPRPT
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1087

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<--ALL-->")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()

        If DdlRptType.SelectedItem.Text = "ARBITRATION AND EXECUTION STATEMENT" Then


            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String
            HeaderArr(0, 0) = "SL.No"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "ARBITRATION"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "50"
            HeaderArr(1, 3) = "35"

            HeaderArr(2, 0) = "Nos"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "Amount"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "15"

            Session("RptHeader") = HeaderArr

            M_Sql = "Exec LoanARC_EX_Sp " & ClsComm.DbDate(DtFrom.Text) & "," & ClsComm.DbDate(DtTo.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & IIf(DdlBranch.SelectedItem.Text = "<--ALL-->", "0", DdlBranch.SelectedValue) & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)
            Exit Sub
        End If
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class