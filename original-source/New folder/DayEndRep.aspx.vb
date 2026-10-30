
Imports SMARTCORE_DAL
Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing
Partial Public Class DayEndRep

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GC.Collect()
            Load_Report()
            Session("PreUrl") = Request.UrlReferrer.ToString()
        End If
    End Sub
    Private Sub Load_Report()
        Dim M_DaySummary As New DataTable
        Dim M_PerformanceApp As New DataTable
        Dim M_DepSchValDt As New DataTable
        Dim M_LoanSchValDt As New DataTable
        Dim M_DepScheduleDt As New DataTable
        Dim M_LoanScheduleDt As New DataTable
        Dim M_ShareScheduleDt As New DataTable

        Dim M_Qry As String
        Dim M_Heading As String = ""

        M_Heading = Session("RptHeading")
        M_Qry = Session("RptQry")

        LblBankName.Text = Session("RptHeading1")
        LblHeading.Text = M_Heading

        Dim M_Ds As New DataSet
        Dim M_ReportsBL As New SMARTCORE_DAL.Connection
        M_ReportsBL.ExecuteDataSet(M_Ds, M_Qry, False)
        M_DaySummary = M_Ds.Tables(0)
        M_PerformanceApp = M_Ds.Tables(1)
        M_DepSchValDt = M_Ds.Tables(2)
        M_LoanSchValDt = M_Ds.Tables(3)
        M_DepScheduleDt = M_Ds.Tables(4)
        M_LoanScheduleDt = M_Ds.Tables(5)
        M_ShareScheduleDt = M_Ds.Tables(6)

        DgvSummary.DataSource = M_DaySummary
        DgvSummary.DataBind()

        DgvPerformance.DataSource = M_PerformanceApp
        DgvPerformance.DataBind()

        DgvDeposit.DataSource = M_DepScheduleDt
        DgvDeposit.DataBind()

        DgvLoan.DataSource = M_LoanScheduleDt
        DgvLoan.DataBind()

        DgvShare.DataSource = M_ShareScheduleDt
        DgvShare.DataBind()

        'DepChart.Series.Add("M_DepSchValDt")
        'DepChart.Series("M_DepSchValDt").XValueMember = "DepositScheme"
        'DepChart.Series("M_DepSchValDt").YValueMembers = "DepositBalance"
        'DepChart.Series("M_DepSchValDt").ChartType = SeriesChartType.SplineArea

        '' Enable data points labels
        ''DepChart.Series("M_DepSchValDt").IsValueShownAsLabel = True
        '' Show the Point marker lines
        '' DepChart.Series("M_DepSchValDt").CustomProperties = "ShowMarkerLines=true"
        'DepChart.DataSource = M_DepSchValDt
        'Dim t1 As New Title("Deposit Balance", Docking.Top, New System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105))
        'DepChart.Titles.Add(t1)

        'LoanChart.Series.Add("M_LoanSchValDt")
        'LoanChart.Series("M_LoanSchValDt").XValueMember = "LoanScheme"
        'LoanChart.Series("M_LoanSchValDt").YValueMembers = "LoanBalance"
        'LoanChart.Series("M_LoanSchValDt").ChartType = SeriesChartType.SplineArea



        '' Enable data points labels
        '' LoanChart.Series("M_LoanSchValDt").IsValueShownAsLabel = True
        '' Show the Point marker lines
        ''LoanChart.Series("M_LoanSchValDt").CustomProperties = "ShowMarkerLines=true"
        'LoanChart.DataSource = M_LoanSchValDt
        'Dim t2 As New Title("Loan Balance", Docking.Top, New System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105))
        'LoanChart.Titles.Add(t2)

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdClose.Click
        Response.Redirect(Session("PreUrl"))
    End Sub

End Class