Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BL
Partial Public Class Scheme_Details
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CmmonBl As New CommonBL
                'M_CmmonBl.GetSchemes(Ddl_Scheme, Session("TransDate"), "LOAN")
                M_CmmonBl.GetSchemes(Ddl_Scheme, Session("TransDate"), "LOAN", "GOLDLOAN")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim Sch_code As String = Ddl_Scheme.SelectedValue
        'Dim T_date As String = Txt_Date.Text
        Dim M_CommanBL As New CommonBL
        Dim M_Datatable As New DataTable()
        M_Datatable = M_CommanBL.Get_SchemeDetails(Sch_code)
        PrintDocument(Sch_code)

    End Sub
    Public Sub PrintDocument(ByVal Sch_code As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""


        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        'M_Sql = "Exec getLoanApplicationDetailsRpt_Sp '" & LaNo & "'"
        'M_DBConn.ExecuteScalar(M_Sql, , False)

        M_Sql = "SELECT        dbo.Gen_SchemeMaster_P_Tbl.Sch_Code, dbo.Gen_SchemeMaster_P_Tbl.Sch_Name,dbo.Gen_IntRateMaster_P_Tbl.Im_SlabId, "
        M_Sql = M_Sql + " dbo.Gen_IntRateMaster_P_Tbl.Im_Periodupto, dbo.Gen_IntRateMaster_P_Tbl.Im_IntRate, "
        M_Sql = M_Sql + " dbo.Loan_Parameter_P_Tbl.Lnp_MinLimit, dbo.Loan_Parameter_P_Tbl.Lnp_MaxLimit, dbo.Gold_GramRate_P_Tbl.Ggr_BankRatePerc"

        M_Sql = M_Sql + " FROM dbo.Gen_SchemeMaster_P_Tbl"
        M_Sql = M_Sql + " INNER Join"
        M_Sql = M_Sql + " dbo.Gold_GramRate_P_Tbl ON dbo.Gen_SchemeMaster_P_Tbl.Sch_Code = dbo.Gold_GramRate_P_Tbl.Sch_Code "
        M_Sql = M_Sql + " INNER Join"
        M_Sql = M_Sql + " dbo.Loan_Parameter_P_Tbl ON dbo.Gen_SchemeMaster_P_Tbl.Sch_Code = dbo.Loan_Parameter_P_Tbl.Sch_Code "
        M_Sql = M_Sql + " INNER Join"
        M_Sql = M_Sql + "  dbo.Gen_IntRateMaster_P_Tbl ON dbo.Gen_SchemeMaster_P_Tbl.Sch_Code = dbo.Gen_IntRateMaster_P_Tbl.Sch_Code"
        M_Sql = M_Sql + "  where Gold_GramRate_P_Tbl.Making_Time=(select max(Making_Time) from Gold_GramRate_P_Tbl where Sch_Code='" + Sch_code + "') and Gen_SchemeMaster_P_Tbl.Sch_Code='" + Sch_code + "'"

        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)


        'StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & Session("BranchName").ToString & "|Heading|  " & Session("BranchName").ToString & "|User_ID|" & Session("UserID")
        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : 999|Heading|  999|User_ID|" & Session("UserID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/SchemeDetails.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        'Response.Redirect("~/RptPreview.aspx", False)

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class