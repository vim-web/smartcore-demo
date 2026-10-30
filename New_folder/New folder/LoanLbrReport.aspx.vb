Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class LoanLbrReport
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Private Rpt_Qry As String
    Private Rpt_Path As String
    Private Rpt_Heading As String
    Private Str_Flf As String
    Dim FormID As Integer = 1090
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            M_DBConn = New Connection
            Dim M_CommonBL As New CommonBL
            TxtDtFrom.Text = Session("TransDate")
            TxtDtTo.Text = Session("TransDate")
            Cmb_Wise.Items.Add("UrbanWise")
            Cmb_Wise.Items.Add("RuralWise")
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim ClsComm As New CommonClass
            Dim Heading As String
            Dim StrSlf As String
            If Cmb_Wise.SelectedIndex = 0 Then
                Rpt_Path = "LBRU2.rpt"
                Heading = "LBR AREA WISE REPORT BETWEEN " & TxtDtFrom.Text & " AND " & TxtDtTo.Text & " (MONTHLY)"
                Rpt_Qry = "Exec Loan_LBR_Sp 'URBAN'," & ClsComm.DbDate(TxtDtFrom.Text) & "," & ClsComm.DbDate(TxtDtTo.Text) & ""

                Dim BrName As String
                Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

                Session.Add("RptQry", Rpt_Qry)
                Session.Add("RptFormula", Str_Flf)
                StrSlf = "Company|" & Company_Name & "|Heading|" & Heading
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/" & Rpt_Path))
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            Else
                Rpt_Path = "LBRArea.rpt"
                Heading = "LBR RURAL WISE REPORT  BETWEEN " & TxtDtFrom.Text & " AND " & TxtDtTo.Text & " (MONTHLY)"
                Rpt_Qry = "Exec Loan_LBR_Sp 'RURAL'," & ClsComm.DbDate(TxtDtFrom.Text) & "," & ClsComm.DbDate(TxtDtTo.Text) & ""
                Dim BrName As String
                Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

                Session.Add("RptQry", Rpt_Qry)
                StrSlf = "Company|" & Company_Name & "|Heading|" & Heading
                Session.Add("RptFormula", Str_Flf)

                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/" & Rpt_Path))
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

            End If
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

End Class