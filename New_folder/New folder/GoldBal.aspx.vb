Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class GoldBal
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1063


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.SelectedValue = Session("DefaultBrCode")


                DtFrom.Text = Session("TransDate")

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
        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()

            Dim HeaderArr(4, 4) As String
            Dim HeaderWidth(4, 1) As String

            HeaderArr(0, 0) = "ACCOUNT NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "79"
            HeaderArr(0, 3) = "79"
            Dim Dt As Date
            Dt = DtFrom.Text
            Session("RptHeader") = HeaderArr
            M_Sql = "Exec GoldBal_Sp '" & DdlBranch.SelectedValue & "','" & Format(Dt, "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

End Class