Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Public Class NPA_Prdwise_Report
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 1037
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If IsPostBack = False Then

                'USer Rights
                'Dim M_Comm As New CommonBL
                'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                'M_Comm = Nothing
                'If Obj_UserRights.View_Right = False Then
                '    Response.Redirect("~/UnauthorisedAcces.ASPX")
                'End If
                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<-- ALL -->")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                Txt_AsOnDate.Text = Session("TransDate")
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN", , )
                DdlScheme.Items.Add("<-- ALL -->")
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

    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click

        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

        Dim M_Qry As String = ""

        Dim HeaderArr(20, 4) As String
        Dim HeaderWidth(20, 1) As String

        HeaderArr(0, 0) = "SLNO"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "5"
        HeaderArr(0, 3) = "5"

        HeaderArr(1, 0) = "Br_Name"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "15"
        HeaderArr(1, 3) = "15"


        HeaderArr(2, 0) = "Sch_Name"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "10"
        HeaderArr(2, 3) = "5"

        HeaderArr(3, 0) = "Acc_No"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "15"
        HeaderArr(3, 3) = "10"

        HeaderArr(4, 0) = "Cust_id"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "15"
        HeaderArr(4, 3) = "5"


        HeaderArr(5, 0) = "Cust_Name"
        HeaderArr(5, 1) = "R"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "10"


        HeaderArr(6, 0) = "Loan Date"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "10"
        HeaderArr(6, 3) = "10"

        HeaderArr(7, 0) = "Period"
        HeaderArr(7, 1) = "R"
        HeaderArr(7, 2) = "15"
        HeaderArr(7, 3) = "5"

        HeaderArr(8, 0) = "Due Date"
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "10"
        HeaderArr(8, 3) = "10"

        HeaderArr(9, 0) = "Loan Amount"
        HeaderArr(9, 1) = "R"
        HeaderArr(9, 2) = "10"
        HeaderArr(9, 3) = "10"

        HeaderArr(10, 0) = "EMI"
        HeaderArr(10, 1) = "R"
        HeaderArr(10, 2) = "5"
        HeaderArr(10, 3) = "5"

        HeaderArr(11, 0) = "Last_PaidInstDate"
        HeaderArr(11, 1) = "L"
        HeaderArr(11, 2) = "10"
        HeaderArr(11, 3) = "10"

        HeaderArr(12, 0) = "NPA_Date"
        HeaderArr(12, 1) = "L"
        HeaderArr(12, 2) = "10"
        HeaderArr(12, 3) = "10"

        HeaderArr(13, 0) = "Outstand"
        HeaderArr(13, 1) = "R"
        HeaderArr(13, 2) = "10"
        HeaderArr(13, 3) = "10"

        HeaderArr(14, 0) = "Receivable Priciple"
        HeaderArr(14, 1) = "R"
        HeaderArr(14, 2) = "10"
        HeaderArr(14, 3) = "10"

        HeaderArr(15, 0) = "Receivable Interest"
        HeaderArr(15, 1) = "R"
        HeaderArr(15, 2) = "10"
        HeaderArr(15, 3) = "10"

        HeaderArr(16, 0) = "Total Receivable"
        HeaderArr(16, 1) = "R"
        HeaderArr(16, 2) = "10"
        HeaderArr(16, 3) = "10"

        HeaderArr(17, 0) = "Period Type"
        HeaderArr(17, 1) = "L"
        HeaderArr(17, 2) = "10"
        HeaderArr(17, 3) = "10"

        HeaderArr(18, 0) = "Last Paid InstNo"
        HeaderArr(18, 1) = "R"
        HeaderArr(18, 2) = "8"
        HeaderArr(18, 3) = "8"

        HeaderArr(19, 0) = "ArearInstNo"
        HeaderArr(19, 1) = "R"
        HeaderArr(19, 2) = "10"
        HeaderArr(19, 3) = "8"

        

        Session("RptHeader") = HeaderArr

        M_Qry = "EXEC [dbo].[Get_NPA_AccountDetails_Sp]'" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "'," & ClsComm.DbDate(Txt_AsOnDate.Text) & "," & Txt_NpaPeriod.Text & ""
        Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

End Class