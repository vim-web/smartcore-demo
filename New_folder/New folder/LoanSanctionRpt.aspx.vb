Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class LoanSanctionRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
            TxtToDate.Text = Session("TransDate")
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrcode")
            CmbBranch.Items.Add("ALL")
            CmbRptOpt.Items.Insert("0", "ISSUE LA FORM ONLY")
            CmbRptOpt.Items.Insert("1", "LA APPLICATION")
            CmbRptOpt.Items.Insert("2", "LA SECURITY ENTERED")
            CmbRptOpt.Items.Insert("3", "LA PROPERTY INSPECTED")
            CmbRptOpt.Items.Insert("4", "LA RECORD INSPECTED")
            CmbRptOpt.Items.Insert("5", "SANCTIONED LOANS")
            CmbRptOpt.Items.Insert("6", "REJECTED LOANS")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        If CmbRptOpt.SelectedIndex = "0" Then
            Dim HeaderArr(11, 4) As String
            Dim HeaderWidth(11, 1) As String
            HeaderArr(0, 0) = "APPLICATIONFORM NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "ISSUE DATE"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "20"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "REMARKS"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "10"
            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC Loan_ApplicationsRpt_Sp '" & CmbBranch.SelectedValue & "','" & CmbRptOpt.SelectedIndex & "'," & CommonClass.DbDate(TxtToDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        ElseIf CmbRptOpt.SelectedIndex = "1" Or CmbRptOpt.SelectedIndex = "2" Then
            Dim HeaderArr(7, 4) As String
            Dim HeaderWidth(7, 1) As String
            HeaderArr(0, 0) = "SLNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "APPLICATION NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "13"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "LOAN TYPE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "30"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "LA FORM NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "10"


            HeaderArr(4, 0) = "LOAN AMOUNT"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"


            HeaderArr(5, 0) = "LOAN DATE"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"
            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC Loan_ApplicationsRpt_Sp '" & CmbBranch.SelectedValue & "','" & CmbRptOpt.SelectedIndex & "'," & CommonClass.DbDate(TxtToDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        ElseIf CmbRptOpt.SelectedIndex = "3" Then
            Dim HeaderArr(11, 4) As String
            Dim HeaderWidth(11, 1) As String

            HeaderArr(0, 0) = "SLNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "APPLICATION NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "13"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "LOAN TYPE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "30"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "LA FORM NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "SITE VALUATION"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "SITE INSPECTED DATE"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "LOAN DATE"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"
            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC Loan_ApplicationsRpt_Sp '" & CmbBranch.SelectedValue & "','" & CmbRptOpt.SelectedIndex & "'," & CommonClass.DbDate(TxtToDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)


        ElseIf CmbRptOpt.SelectedIndex = "4" Then
            Dim HeaderArr(11, 4) As String
            Dim HeaderWidth(11, 1) As String

            HeaderArr(0, 0) = "SLNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "APPLICATION NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "13"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "LOAN TYPE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "30"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "LA FORM NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "ADVOCATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "RECORD INSPECTED DATE"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "LOAN DATE"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"
            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC Loan_ApplicationsRpt_Sp '" & CmbBranch.SelectedValue & "','" & CmbRptOpt.SelectedIndex & "'," & CommonClass.DbDate(TxtToDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        ElseIf CmbRptOpt.SelectedIndex = "5" Or CmbRptOpt.SelectedIndex = "6" Then
            Dim HeaderArr(11, 4) As String
            Dim HeaderWidth(11, 1) As String

            HeaderArr(0, 0) = "SLNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "APPLICATION NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "13"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "LOAN TYPE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "30"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "LA FORM NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "LOAN DATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"


            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC Loan_ApplicationsRpt_Sp '" & CmbBranch.SelectedValue & "','" & CmbRptOpt.SelectedIndex & "'," & CommonClass.DbDate(TxtToDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        End If
    End Sub
End Class