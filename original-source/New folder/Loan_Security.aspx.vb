Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class WebForm7
    Inherits System.Web.UI.Page
    Dim M_DBConn As New Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()

        End If
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Cmb_Branch)
            M_CommonBL.GetSchemes(Cmb_Scheme, Session("TransDate"))
            Txt_FrDate.Text = Session("TransDate")
            Txt_ToDate.Text = Session("TransDate")
            Cmb_Branch.SelectedValue = Session("DefaultBrCode")
            Cmb_Scheme.Items.Add("All")
            Cmb_Branch.Items.Add("All")
            CmbLoansecurity.Items.Add("BOND")
            CmbLoansecurity.Items.Add("GOLD")
            CmbLoansecurity.Items.Add("INSURANCE")
            CmbLoansecurity.Items.Add("LAND AND BUILDINGS")
            CmbLoansecurity.Items.Add("SALARY")
            CmbLoansecurity.Items.Add("TERM DEPOSIT")


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

    Protected Sub Cmd_Close_Click(sender As Object, e As ImageClickEventArgs) Handles Cmd_Close.Click
        Response.Redirect("~/Main.aspx")

    End Sub

    Protected Sub Cmd_Show_Click(sender As Object, e As ImageClickEventArgs) Handles Cmd_Show.Click
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim StrSlf As String
        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String

        Dim Brcode As String
        Dim SchemeCode As String
        If Cmb_Branch.SelectedValue <> "<--ALL-->" Then
            Brcode = Cmb_Branch.SelectedValue
        Else
            Brcode = "0"
        End If
        If Cmb_Scheme.SelectedValue <> "<--ALL-->" Then
            SchemeCode = Cmb_Scheme.SelectedValue
        Else
            SchemeCode = "0"
        End If


        M_DBConn = New Connection()

        Company_Name = HttpContext.Current.Cache("CompanyName")
        If CmbLoansecurity.SelectedItem.Text = "GOLD" Then
            Dim HeaderArr(10, 4) As String
            Dim HeaderWidth(10, 1) As String
            HeaderArr(0, 0) = "BRANCH NAME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "15"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "SCHEME NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "ACCOUNT NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "13"



            HeaderArr(3, 0) = "ORNAMENT "
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "COUNT"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "REMARKS"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "NETWT"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "GROSSWT"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "13"

           


            Session("RptHeader") = HeaderArr

        ElseIf CmbLoansecurity.SelectedItem.Text = "BOND" Then
            Dim HeaderArr(7, 4) As String
            Dim HeaderWidth(7, 1) As String
            HeaderArr(0, 0) = "BRANCH NAME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "15"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "SCHEME NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "40"

            HeaderArr(2, 0) = "ACCOUNT NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "13"



            HeaderArr(3, 0) = "POSTOFFICE "
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "NO"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "PHASE VALUE"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "DUE DATE"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "TOTAL VALUE"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "13"




            Session("RptHeader") = HeaderArr






        ElseIf CmbLoansecurity.SelectedItem.Text = "INSURANCE" Then
            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String
            HeaderArr(0, 0) = "BRANCH NAME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "15"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "SCHEME NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "ACCOUNT NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "13"



            HeaderArr(3, 0) = "INSURANCE COMPANY "
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "POLICY NAME"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "POLICY NO"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "AMOUNT"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "DATE OF MATURITY"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "13"

            HeaderArr(8, 0) = "SURRENDER VALUE"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "13"


            Session("RptHeader") = HeaderArr

        ElseIf CmbLoansecurity.SelectedItem.Text = "LAND AND BUILDINGS" Then
            Dim HeaderArr(21, 4) As String
            Dim HeaderWidth(21, 1) As String
            HeaderArr(0, 0) = "BRANCH NAME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "SCHEME NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "5"
            HeaderArr(1, 3) = "40"

            HeaderArr(2, 0) = "ACCOUNT NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "5"
            HeaderArr(2, 3) = "13"



            HeaderArr(3, 0) = "DEED NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "DEED DATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "5"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "PRIOR DEEDNO"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "5"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "DISTRICT"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "5"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "TALUK"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "13"

            HeaderArr(8, 0) = "PANCHAYATH"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "5"
            HeaderArr(8, 3) = "13"

            HeaderArr(9, 0) = "VILLAGE"
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "5"
            HeaderArr(9, 3) = "13"

            HeaderArr(10, 0) = "REG OFFICE"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "5"
            HeaderArr(10, 3) = "13"

            HeaderArr(11, 0) = "EXTEND"
            HeaderArr(11, 1) = "L"
            HeaderArr(11, 2) = "5"
            HeaderArr(11, 3) = "13"

            HeaderArr(12, 0) = "MARKET VALUE"
            HeaderArr(12, 1) = "L"
            HeaderArr(12, 2) = "5"
            HeaderArr(12, 3) = "13"

            HeaderArr(13, 0) = "TAXNO"
            HeaderArr(13, 1) = "L"
            HeaderArr(13, 2) = "5"
            HeaderArr(13, 3) = "13"

            HeaderArr(14, 0) = "TAX DATE"
            HeaderArr(14, 1) = "L"
            HeaderArr(14, 2) = "5"
            HeaderArr(14, 3) = "13"

            HeaderArr(15, 0) = "TAX DETAILS"
            HeaderArr(15, 1) = "L"
            HeaderArr(15, 2) = "5"
            HeaderArr(15, 3) = "13"

            HeaderArr(16, 0) = "INSURANCE NO"
            HeaderArr(16, 1) = "L"
            HeaderArr(16, 2) = "5"
            HeaderArr(16, 3) = "13"

            HeaderArr(17, 0) = "INSURANCE DATE"
            HeaderArr(17, 1) = "L"
            HeaderArr(17, 2) = "5"
            HeaderArr(17, 3) = "13"

            HeaderArr(18, 0) = "INSURANCE DUEDATE"
            HeaderArr(18, 1) = "L"
            HeaderArr(18, 2) = "4"
            HeaderArr(18, 3) = "13"

            HeaderArr(19, 0) = "OTHER DETAILS"
            HeaderArr(19, 1) = "L"
            HeaderArr(19, 2) = "4"
            HeaderArr(19, 3) = "13"

            HeaderArr(20, 0) = "RESURVEY NO"
            HeaderArr(20, 1) = "L"
            HeaderArr(20, 2) = "2"
            HeaderArr(20, 3) = "13"


            Session("RptHeader") = HeaderArr

        ElseIf CmbLoansecurity.SelectedItem.Text = "SALARY" Then
            Dim HeaderArr(15, 4) As String
            Dim HeaderWidth(15, 1) As String
            HeaderArr(0, 0) = "BRANCH NAME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "7"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "SCHEME NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "7"
            HeaderArr(1, 3) = "40"

            HeaderArr(2, 0) = "ACCOUNT NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "7"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "NAME "
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "7"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "BRANCH"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "7"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "EMPLOYEE NAME"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "7"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "DESIGNATION"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "7"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "JOIN DATE"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "7"
            HeaderArr(7, 3) = "13"

            HeaderArr(8, 0) = "BASIC SALARY"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "7"
            HeaderArr(8, 3) = "13"

            HeaderArr(9, 0) = "ALLOWANCE"
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "7"
            HeaderArr(9, 3) = "13"

            HeaderArr(10, 0) = "DEDUCTIONS"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "7"
            HeaderArr(10, 3) = "13"

            HeaderArr(11, 0) = "RETIREMENT DATE"
            HeaderArr(11, 1) = "L"
            HeaderArr(11, 2) = "7"
            HeaderArr(11, 3) = "13"

            HeaderArr(12, 0) = "ISSUING AUTHORITY"
            HeaderArr(12, 1) = "L"
            HeaderArr(12, 2) = "7"
            HeaderArr(12, 3) = "13"

            HeaderArr(13, 0) = "AUTHORITY ADDRESS"
            HeaderArr(13, 1) = "L"
            HeaderArr(13, 2) = "7"
            HeaderArr(13, 3) = "13"

            HeaderArr(14, 0) = "ISSUE DATE"
            HeaderArr(14, 1) = "L"
            HeaderArr(14, 2) = "5"
            HeaderArr(14, 3) = "13"

            HeaderArr(15, 0) = "REMARKS"
            HeaderArr(15, 1) = "L"
            HeaderArr(15, 2) = "4"
            HeaderArr(15, 3) = "13"




            Session("RptHeader") = HeaderArr


        ElseIf CmbLoansecurity.SelectedItem.Text = "TERM DEPOSIT" Then
            Dim HeaderArr(7, 4) As String
            Dim HeaderWidth(7, 1) As String
            HeaderArr(0, 0) = "BRANCH NAME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "15"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "SCHEME NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "40"

            HeaderArr(2, 0) = "SCHEME TYPE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "13"



            HeaderArr(3, 0) = "ACC NO "
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "SCHEME NO"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "DEP AMT"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "DUE DATE"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "AMOUNT OUTSTSNDING"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "13"




            Session("RptHeader") = HeaderArr



        End If


        M_Sql = "EXEC Loan_Security_rpt_sp'" & Cmb_Branch.SelectedValue & "','" & Cmb_Scheme.SelectedValue & "','" & Session("TransDate") & "','" & Session("TransDate") & "','" & CmbLoansecurity.SelectedItem.Text & "'"
        Session("RptQry") = M_Sql
        Session.Add("RptFormula", M_Sql)
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub
End Class