Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Partial Public Class LoanAnalysisRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights       


            Txt_DemandDate.Text = Session("TransDate")
            Dim M_dt As New DataTable
            Dim M_Bl As New CommonBL
            M_Bl.Fill_Branch(DrpBranch)

            DrpBranch.Items.Add("ALL")




        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""



        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        'M_Sql = "Exec getLoanApplicationDetailsRpt_Sp '" & LaNo & "'"
        'M_DBConn.ExecuteScalar(M_Sql, , False)

        M_Sql = "Exec LoanAnalysis_Rpt   '" & DrpBranch.SelectedValue & "'," & ClsComm.DbDate(Txt_DemandDate.Text) & ""

        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        'If section = "GEN.LOAN" Then Heading = "GENERAL LOAN"
        'If section = "GOLDLOAN" Then Heading = "GOLD LOAN"

        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch :  " & IIf(DrpBranch.SelectedItem.Text <> "ALL", DrpBranch.SelectedItem.Text, "Consolidation") & " |Heading|Loan Analysis Report As On : " & Txt_DemandDate.Text & " |User_ID|" & Session("UserID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/LoanAnalysisRpt.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        'Response.Redirect("~/RptPreview.aspx", False)
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class