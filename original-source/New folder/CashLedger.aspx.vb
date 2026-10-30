Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Imports System.Data
Partial Public Class CashLedger
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_Branch(DdlBranch)
            DdlBranch.Items.Add("ALL")
            DtFrom.Text = Session("TransDate")
            DtTo.Text = Session("TransDate")
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA

            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            M_DBConn = New Connection()

            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String

            HeaderArr(0, 0) = "BR_CODE"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "6"
            HeaderArr(0, 3) = "6"

            HeaderArr(1, 0) = "BRANCH"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "TRDATE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "VR_NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "5"

            HeaderArr(4, 0) = "PARTICULARS"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "25"
            HeaderArr(4, 3) = "25"

            HeaderArr(5, 0) = "RECEIPT"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "6"
            HeaderArr(5, 3) = "6"

            HeaderArr(6, 0) = "PAYMENT"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "6"
            HeaderArr(6, 3) = "6"

            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC Get_CashLedger_Sp '" & DdlBranch.SelectedValue & "'," & ClsComm.DbDate(DtFrom.Text) & "," & ClsComm.DbDate(DtTo.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
End Class