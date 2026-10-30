Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Public Class NotSubmitPanCard
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
            
                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("ALL")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                DrpSubmit.Items.Add("YES")
                DrpSubmit.Items.Add("NO")
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "DEPOSIT")
                DdlScheme.Items.Add("ALL")
                TxtFrom.Text = Session("TransDate")
                TxtTo.Text = Session("TransDate")
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

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click

        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim Heading As String = ""
        Dim Status As String
        Dim ClsComm As New CommonClass()
        If ChkQuarterly.Checked = True Then
            Dim HeaderArr(10, 4) As String
            Dim HeaderWidth(10, 1) As String
            HeaderArr(0, 0) = "Sl No"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "6"
            HeaderArr(0, 3) = "6"

            HeaderArr(1, 0) = "CUST ID"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "6"

            HeaderArr(2, 0) = "NAME"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "30"

            HeaderArr(3, 0) = "ACCOUNT NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "SCHEME"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "30"

            HeaderArr(5, 0) = "DEPOSIT AMOUNT"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "PAY MODE"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "15"

            HeaderArr(7, 0) = "PAN NUMBER"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "12"
            HeaderArr(7, 3) = "20"


            HeaderArr(8, 0) = "INT PAID"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "10"

            Session("RptHeader") = HeaderArr



            M_Sql = "EXEC Get_15GIntPaid_Rpt_Sp '" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "'," & ClsComm.DbDate(TxtFrom.Text) & "," & ClsComm.DbDate(TxtTo.Text) & ",'" & IIf(DrpSubmit.SelectedValue = "YES", "Y", "N") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)

            Exit Sub
        End If

        If ChkQuarterly.Checked = False Then

            
            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String

            HeaderArr(0, 0) = "Sl No"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "6"
            HeaderArr(0, 3) = "6"

            HeaderArr(1, 0) = "CUST ID"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "6"

            HeaderArr(2, 0) = "NAME"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "30"

            HeaderArr(3, 0) = "ACCOUNT NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "SCHEME"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "30"

            HeaderArr(5, 0) = "ADDRESS"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "30"

            HeaderArr(6, 0) = "MOBILE"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "15"

            HeaderArr(7, 0) = "DEPOSIT AMOUNT"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "16"
            HeaderArr(7, 3) = "13"


            HeaderArr(8, 0) = "PAN NUM"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "20"

            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC PanCardUnSubmit_Sp '" & DdlBranch.SelectedValue & "','" & IIf(DrpSubmit.SelectedValue = "YES", "Y", "N") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)
            Exit Sub
        End If

        Dim M_Dt As DataTable = New DataTable()
        M_Sql = "Exec [PanCardUnSubmit_Sp] '" & DdlBranch.SelectedValue & "','" & Status & "'"
        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|" & Heading & " |User_ID|:" & Session("UserID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/WithOutPanCard.rpt"))
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub ChkQuarterly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkQuarterly.CheckedChanged
        If ChkQuarterly.Checked = True Then
            LblFromDt.Visible = True
            LblToDt.Visible = True
            TxtFrom.Visible = True
            TxtTo.Visible = True
            LblScheme.Visible = True
            DdlScheme.Visible = True
        Else
            LblFromDt.Visible = False
            LblToDt.Visible = False
            TxtFrom.Visible = False
            TxtTo.Visible = False
            LblScheme.Visible = False
            DdlScheme.Visible = False
        End If
    End Sub
End Class