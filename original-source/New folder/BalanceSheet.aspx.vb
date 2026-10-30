Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class BalanceSheet

    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1010
    Shared Obj_UserRights As Object





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL
                ''**********************************************************************************
                 M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
                DdlBranch.Items.Add("ALL")


                DtFrom.Text = Session("TransDate")
                'RdoOption.Items(0).Selected = True

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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            If ChckExcel.Checked = True Then
                Dim M_Sql As String
                Dim StrSlf As String = ""
                Dim ClsComm As New CommonClass()
                Dim M_Dt As DataTable = New DataTable()

                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String




                HeaderArr(0, 0) = "HEAD CODE"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "15"
                HeaderArr(0, 3) = "15"

                HeaderArr(1, 0) = "LIABILITY"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "35"
                HeaderArr(1, 3) = "35"

                HeaderArr(2, 0) = "AMOUNT"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "15"

                HeaderArr(3, 0) = "HEAD CODE"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "15"
                HeaderArr(3, 3) = "15"

                HeaderArr(4, 0) = "ASSET"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "35"
                HeaderArr(4, 3) = "35"

                HeaderArr(5, 0) = "AMOUNT"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "15"
                HeaderArr(5, 3) = "15"

                Dim Dt As Date
                Dt = DtFrom.Text
                Session("RptHeader") = HeaderArr
                'If DdlBranch.Text = "<-- ALL -->" Then
                '    M_Sql = "Exec BalanceSheet_Sp_Type2_EX 'ALL','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "'"
                'Else
                M_Sql = "Exec BalanceSheet_Sp_Type2_EX '" & DdlBranch.SelectedValue & "','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                'End If
                Session("RptQry") = M_Sql

                Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Else




                Dim Dt As Date
                Dt = DtFrom.Text

                Dim M_Sql As String
                Dim StrSlf As String = ""
                Dim ClsComm As New CommonClass()
                Dim M_Dt As DataTable = New DataTable()
                'If RdoOption.Items(0).Selected Then
                '    If DdlBranch.Text = "<-- ALL -->" Then
                '        M_Sql = "Exec BalanceSheet_Sp 'ALL','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "'"
                '    Else
                '        M_Sql = "Exec BalanceSheet_Sp '" & DdlBranch.SelectedValue & "','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "'"
                '    End If

                'Else
                If ChkSchedule.Checked = False Then


                    'If DdlBranch.Text = "<-- ALL -->" Then
                    '    M_Sql = "Exec BalanceSheet_Sp_Type2 'ALL','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "'"
                    'Else
                    M_Sql = "Exec BalanceSheet_Sp_Type2 '" & DdlBranch.SelectedValue & "','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                    'End If
                    StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Region|Region : " & DrpRegn.SelectedItem.ToString & "|Area|Area : " & DrpArea.SelectedItem.ToString & "|Heading|Balance Sheet As on  " & DtFrom.Text & " |User_ID|" & Session("UserID")
                    StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                    Session.Add("RptQry", M_Sql)
                    Session.Add("RptFormula", StrSlf)
                    Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/BalSheetStmt.rpt"))
                    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
                Else
                    Dim StrSub_sql As String = ""
                    Dim rssub(2) As String
                    Dim SubRptName As String
                    Dim ReportType As String = "Balance Sheet Schedule 3"

                    If DdlBranch.Text = "<-- ALL -->" Then
                        M_Sql = "Exec BalanceSheetSchedule3_Sp 'ALL','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "'"
                    Else
                        M_Sql = "Exec BalanceSheetSchedule3_Sp '" & DdlBranch.SelectedValue & "','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "'"
                    End If

                    M_Sql = " SELECT * FROM Temp_BS_Schedule_R_Tbl where ACC_ALIE='L' ORDER BY Slno ASC  "

                    StrSub_sql = " SELECT * FROM Temp_BS_Schedule_R_Tbl where ACC_ALIE='A' ORDER BY Slno ASC "

                    rssub(0) = StrSub_sql

                    Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
                    M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

                    StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|Balance Sheet Schedule 3 As on  " & DtFrom.Text & " |User_ID|" & Session("UserID")
                    StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                    ' SubFld = "Asset_Expenditure"
                    Session.Add("RptQry", M_Sql)
                    SubRptName = "Schedule3Asset"
                    Session.Add("SubRptNames", SubRptName)
                    Session.Add("SubRptQry", rssub)
                    Session.Add("RptPath", Server.MapPath("~/Reports/Loans/Schedule3.rpt"))
                    Session.Add("RptFormula", StrSlf)
                    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
                End If
                'End If

            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Protected Sub DrpRegn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpRegn.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DrpArea_SelectedIndexChanged(Me.DrpArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
            DdlBranch.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub
End Class