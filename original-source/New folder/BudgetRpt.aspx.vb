Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass


Public Class BudgetRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(Ddl_Branch)
                M_CommonBL.Fill_Pickup(CmbYear, 101, 0)
                Ddl_Branch.Items.Add("<--ALL-->")
                Ddl_Branch.SelectedValue = Session("DefaultBrCode")
                M_CommonBL = Nothing
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CmbShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmbShow.Click
        Dim Br_Code As String = Ddl_Branch.SelectedValue
        Dim M_commonbl As New CommonBL
        Dim M_Datatable As New DataTable()
        M_Datatable = M_commonbl.Get_Budget(Br_Code)
        PrintReport(IIf(Br_Code = "<--ALL-->", "0", Br_Code), CmbYear.SelectedItem.Text)

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged
        Try
            Session("SearchBranch") = Ddl_Branch.SelectedValue.ToString()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub PrintReport(ByVal Br_Code As String, ByVal FinYrear As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""


        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        M_Sql = "Exec Budget_Rpt '" & FinYrear & "','" & Br_Code & "'"
        M_DBConn.ExecuteScalar(M_Sql, , False)

        M_Sql = " SELECT   isnull(b.Br_Name,'') Br_Name,t.* from Temp_BudgetRptTbl t left join Gen_BranchDetails_P_Tbl b on b.Br_Code=t.Br_Code "

        'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & Ddl_Branch.SelectedItem.Text & "|Heading|Budget Report On : " & FinYrear & "" & "" & "|User_ID|" & Session("UserID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/budget_rpt.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        'Response.Redirect("~/RptPreview.aspx", False)

    End Sub

    Protected Sub CmbClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmbClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class