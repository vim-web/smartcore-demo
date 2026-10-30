Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BL
Partial Public Class Loan_appDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(Ddl_Branch)
                Ddl_Branch.Items.Add("<--ALL-->")
                Ddl_Branch.SelectedValue = Session("DefaultBrCode")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim Approved As String = Drp_Approved.SelectedValue
            Dim From_date As String = Txt_Fromdate.Text
            Dim To_date As String = Txt_Todate.Text
            Dim Branch_name As String = Ddl_Branch.SelectedValue
            Dim M_CommanBL As New CommonBL
            Dim M_Datatable As New DataTable()
            M_Datatable = M_CommanBL.get_LoanAppGrid(From_date, To_date, Approved, Branch_name)
            Grid_LoanApp.DataSource = M_Datatable
            Grid_LoanApp.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception

        End Try

    End Sub
    Public Sub PrintDocument(ByVal LaNo As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""


        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        M_Sql = "Exec getLoanApplicationDetailsRpt_Sp '" & LaNo & "'"
        M_DBConn.ExecuteScalar(M_Sql, , False)

        M_Sql = "Select * FROM result_app /*Where Tran_No='" & LaNo & "'*/"
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)


        'StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & Session("BranchName").ToString & "|Heading|  " & Session("BranchName").ToString & "|User_ID|" & Session("UserID")
        'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/LoanDocuments.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        'Response.Redirect("~/RptPreview.aspx", False)

    End Sub

    Protected Sub Grid_LoanApp_RowCommand1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid_LoanApp.RowCommand
            Try
            Dim Rowindex As Integer = e.CommandArgument

            If e.CommandName.ToString() = "Select" Then

                Dim accno As String = Grid_LoanApp.Rows(Rowindex).Cells(0).Text()
                PrintDocument(accno)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged
        Try
            Session("SearchBranch") = Ddl_Branch.SelectedValue.ToString()
        Catch ex As Exception

        End Try
    End Sub
End Class