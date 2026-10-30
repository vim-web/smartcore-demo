Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class SuspenseCash
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(DdlBranch, False)
                ''DdlBranch.Text.

                DtFrom.Text = Session("TransDate")
                Dim M_Dt As New DataTable
                DdlBranch.Items.Add("ALL")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
            End If
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click

        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim clscomm As New CommonClass
            Dim dt As String = DtFrom.Text

            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

            M_Sql = "exec Get_SuspenseSchedule_Rpt'" & DdlBranch.SelectedValue & "','" & Convert.ToDateTime(dt).ToString("MM/dd/yyyy") & "'"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|Suspense Balance Schedule As On " & DtFrom.Text & " |User_ID|" & Session("UserID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
            Session.Add("RptQry", M_Sql)
            Session.Add("RptFormula", StrSlf)
            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/SuspenseCashRpt.rpt"))
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class