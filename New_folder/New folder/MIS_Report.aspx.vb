Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports System.IO
Imports System.Net

Partial Public Class MIS_Report
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Shared Rpt_Document As New ReportDocument
    Dim Comm_cl As New CommonClass
    Public M_Type1 As Integer
    Public M_Type2 As Integer
    Dim StrSql As String
    Dim Company_Name As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            LoadDefaultValue()
            dt = New DataTable
            dt.Columns.Add("Date")
            Dim dr As DataRow
            dr = dt.NewRow()
            dr(0) = ""
            dt.Rows.Add(dt)
            Gridreportdata.DataSource = dt
            Gridreportdata.DataBind()
        End If
    End Sub
    Private Sub LoadDefaultValue()
        Dim M_commanBL As New CommonBL
        Dim M_DataTable As New DataTable
        M_commanBL.Fill_MIS_Setting(ddl_acc_report_1)
        M_commanBL.Fill_MIS_Setting(ddl_acc_report_2)
        M_DataTable = M_commanBL.Fill_Branch()
        grd_branchdetails.DataSource = M_DataTable
        grd_branchdetails.DataBind()
    End Sub


    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim Str_Flf As String
            Str_Flf = ""

            If Not Request.QueryString("Type") = "" Then

                Company_Name = HttpContext.Current.Cache("CompanyName")
                Dim Report_Type = Request.QueryString("Type")
                Dim Acc_Type_Txt1 As String = ddl_acc_report_1.SelectedItem.Text.Trim()
                Dim Acc_Type_Txt2 As String = ddl_acc_report_2.SelectedItem.Text.Trim()
                Dim Acc_Type_1 As String = ddl_acc_report_1.SelectedValue.Trim()
                Dim Acc_Type_2 As String = ddl_acc_report_2.SelectedValue.Trim()
                Dim Str_Date As String
                Dim BranchList As String = ""

                For index As Integer = 0 To Gridreportdata.Rows.Count - 1
                    Str_Date = Str_Date + Comm_cl.DbDate(Gridreportdata.Rows(index).Cells(0).Text.Trim()) + ","
                Next


                For index As Integer = 0 To grd_branchdetails.Rows.Count - 1
                    Dim Chk As CheckBox = DirectCast(grd_branchdetails.Rows(index).Cells(0).FindControl("chkSelectAll1"), CheckBox)
                    If Chk.Checked = True Then
                        BranchList = BranchList + grd_branchdetails.Rows(index).Cells(1).Text.Trim() + ","
                    End If

                Next

                BranchList = Mid(BranchList, 1, BranchList.Length - 1)
                Str_Date = Mid(Str_Date, 1, Str_Date.Length - 1)
                Dim M_valid As Boolean = False

                If Report_Type = "MisReport" Then
                    M_valid = Mis_Report(Acc_Type_1, Acc_Type_2, Str_Date, BranchList)
                End If
                If Report_Type = "Increase" Then
                    M_valid = Report_Increase(Acc_Type_1, Str_Date, BranchList)
                End If

                If Report_Type = "Balance" Then
                    M_valid = Report_Balance(Acc_Type_1, Acc_Type_2, Str_Date, BranchList)
                End If

                If Report_Type = "Employee" Then
                    M_valid = Report_Employe(Acc_Type_1, Str_Date, BranchList)
                End If



                If M_valid = True Then
                    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
                Else
                    ShowAlertMessage("Sorry !. Server Error. Try Again")
                End If
            End If

        Catch ex As Exception

            ShowAlertMessage("Sorry !. Server Error. Try Again")
        End Try


    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub but_add_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles but_add.Click
        dt = New DataTable()
        dt.Columns.Add("Date")

        Dim dr As DataRow
        If hid_gird_row.Value = "OLD" Then
            For index As Integer = 0 To Gridreportdata.Rows.Count - 1
                dr = dt.NewRow()
                Dim str As String = Gridreportdata.Rows(index).Cells(0).Text
                dr(0) = Gridreportdata.Rows(index).Cells(0).Text

                dt.Rows.Add(dr)
            Next
        End If
        hid_gird_row.Value = "OLD"
        dr = dt.NewRow()
        dr(0) = txt_date.Text.Trim()
        dt.Rows.Add(dr)
        'Dim sortRow As DataRow()
        'sortRow = dt.[Select]("", "Date asc")
        'Dim dv As New DataView(dt)

        Gridreportdata.DataSource = dt
        Gridreportdata.DataBind()
        txt_date.Text = ""
    End Sub

   
    Protected Sub Gridreportdata_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Gridreportdata.RowCommand

        If e.CommandName = "Delete" Then
            Dim intexrow As Integer = CInt(e.CommandArgument)
            Dim dr_ As DataRow
            dt = New DataTable
            dt.Columns.Add("Date")
            For index As Integer = 0 To Gridreportdata.Rows.Count - 1
                If index <> intexrow Then
                    dr_ = dt.NewRow()
                    Dim str As String = Gridreportdata.Rows(index).Cells(0).Text
                    dr_(0) = Gridreportdata.Rows(index).Cells(0).Text.ToString()
                    dt.Rows.Add(dr_)
                End If
            Next

            Gridreportdata.DataSource = dt
            Gridreportdata.DataBind()
        End If

    End Sub

    Protected Sub Gridreportdata_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Gridreportdata.RowDeleting

    End Sub

    Protected Sub grd_branchdetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd_branchdetails.RowDataBound
If (e.Row.RowType = DataControlRowType.Header) Then
            DirectCast(e.Row.FindControl("chkSelectAll"), CheckBox).Attributes.Add("onclick", "javascript:SelectAll('" & DirectCast(e.Row.FindControl("chkSelectAll"), CheckBox).ClientID & "')")
             
            End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onclick", "GetRows(this," & e.Row.RowIndex & ")")
            e.Row.Cells(1).Attributes.Add("onclick", "GetRows(this," & e.Row.RowIndex & ")")
            e.Row.Cells(2).Attributes.Add("onclick", "GetRows(this," & e.Row.RowIndex & ")")
        End If


    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "`")
                Dim M_CommonCls As New CommonClass

                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Function Mis_Report(ByVal Acc_Type_1 As String, ByVal Acc_Type_2 As String, ByVal StrDate As String, ByVal BranchList As String) As Boolean
        Try


            Dim Str_Flf As String
            Str_Flf = ""
            Dim M_MisReportBL As New MisReportBL
            Str_Flf = "Company|" & Company_Name & "|ChartHeading|" & "% of " & ddl_acc_report_1.SelectedItem.Text.Trim & " to " & ddl_acc_report_2.SelectedItem.Text.Trim
            Str_Flf = Str_Flf & "|Head1| " & ddl_acc_report_1.SelectedItem.Text.Trim
            Str_Flf = Str_Flf & "|Head2| " & ddl_acc_report_2.SelectedItem.Text.Trim
            Str_Flf = Str_Flf & "|Head3| " & "PERCENTAGE"
            Dim System_Name As String = Session("HostID")
            Dim M_vali As Boolean = M_MisReportBL.FillAllDetails(Acc_Type_1, Acc_Type_2, StrDate, BranchList, System_Name)

            If M_vali = True Then
                Dim M_path As String = Server.MapPath("Reports\MIS REPORTS\Consolidation\MIS\Fund Management\FUNDMGMT2.rpt")
                StrSql = "SELECT  Acc_LedgerBalance_R_Tbl.Tr_Date, "
                StrSql = StrSql + " SUM(case when Mis_RptSetting_M_Tbl.Rpt_Code ='" & Acc_Type_1 & "' then Acc_LedgerBalance_R_Tbl.Clos_Balance else 0 end) AS Balance1,"
                StrSql = StrSql + " SUM(case when Mis_RptSetting_M_Tbl.Rpt_Code ='" & Acc_Type_2 & "' then Acc_LedgerBalance_R_Tbl.Clos_Balance else 0 end) AS Balance2"
                StrSql = StrSql + " FROM Acc_LedgerBalance_R_Tbl INNER JOIN"
                StrSql = StrSql + " Mis_Accounts_M_Tbl ON Acc_LedgerBalance_R_Tbl.Acc_Code = Mis_Accounts_M_Tbl.Acc_Code"
                StrSql = StrSql + " INNER Join Mis_RptSetting_M_Tbl ON Mis_Accounts_M_Tbl.Rpt_Code = Mis_RptSetting_M_Tbl.Rpt_Code AND "
                StrSql = StrSql + " Acc_LedgerBalance_R_Tbl.Tr_Date IN (" & StrDate & ")"
                StrSql = StrSql + " AND Acc_LedgerBalance_R_Tbl.Terminal  ='" & System_Name & "'"
                StrSql = StrSql + " GROUP BY Acc_LedgerBalance_R_Tbl.Tr_Date "
                StrSql = StrSql + " ORDER BY Acc_LedgerBalance_R_Tbl.Tr_Date "
                Session.Add("RptQry", StrSql)
                Session.Add("RptPath", M_path)
                Session.Add("RptFormula", Str_Flf)
                ' Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
                Return M_vali
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function Report_Increase(ByVal Acc_Type As String, ByVal StrDate As String, ByVal BranchList As String) As Boolean
        Try

            Dim M_MisReort As New MisReportBL
            Dim StrSql As String
            Dim Str_Flf As String
            Dim Condition As String
            Dim i As Int16
            Dim System_Name As String = Session("HostID")
            Condition = "'"
            Str_Flf = ""
            Str_Flf = "Company|" & Company_Name & "|ChartHeading|" & "Increase of " & ddl_acc_report_1.SelectedItem.Text.Trim
            Dim M_vali As Boolean = M_MisReort.Mis_BalanceReport(Acc_Type, StrDate, BranchList, System_Name)

            If M_vali = True Then


                Dim M_path As String = Server.MapPath("Reports\MIS REPORTS\Consolidation\MIS\Fund Management\FUNDMGMT5.rpt")

                StrSql = " SELECT MAX(Mis_RptSetting_M_Tbl.Rpt_Name) AS RptName, Acc_LedgerBalance_R_Tbl.Tr_Date,"
                StrSql = StrSql + " SUM(CASE WHEN Acc_LedgerBalance_R_Tbl.Clos_BalType = 'C' THEN Acc_LedgerBalance_R_Tbl.Clos_Balance ELSE Acc_LedgerBalance_R_Tbl.Clos_Balance * - 1 END) AS Balance"
                StrSql = StrSql + "  FROM  Acc_LedgerBalance_R_Tbl INNER JOIN"
                StrSql = StrSql + " Mis_Accounts_M_Tbl ON Acc_LedgerBalance_R_Tbl.Acc_Code = Mis_Accounts_M_Tbl.Acc_Code INNER JOIN"
                StrSql = StrSql + " Mis_RptSetting_M_Tbl ON Mis_Accounts_M_Tbl.Rpt_Code = Mis_RptSetting_M_Tbl.Rpt_Code AND "
                StrSql = StrSql + " Acc_LedgerBalance_R_Tbl.Tr_Date IN (" & StrDate & ") and Mis_RptSetting_M_Tbl.Rpt_Code ='" & Acc_Type & "' "
                StrSql = StrSql + " GROUP BY Acc_LedgerBalance_R_Tbl.Tr_Date"
                Session.Add("RptQry", StrSql)
                Session.Add("RptPath", M_path)
                Session.Add("RptFormula", Str_Flf)
                ' Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

            End If
            Return M_vali
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function Report_Balance(ByVal Acc_Type_1 As String, ByVal Acc_Type_2 As String, ByVal StrDate As String, ByVal BranchList As String) As Boolean
        Try

            Dim StrSql As String
            Dim Str_Flf As String
            Dim Condition As String
            Dim HeadStr(2) As String
            Dim i As Int16
            Dim System_Name As String = Session("HostID")
            Dim M_MisReort As New MisReportBL
            HeadStr(0) = Acc_Type_1
            'HeadStr(1) =' Cmb_Type2.Text.Substring(1, 99).Trim
            HeadStr(2) = "% of " & ddl_acc_report_1.SelectedItem.Text.Trim & " to " '& Cmb_Type2.Text.Substring(1, 99).Trim
            'Heading to Passed (@Head1,@Head2,@Head3)
            '@ChartHeading = "% of " & Cmb_Type1.Text.Substring(1, 99).Trim & " to " & Cmb_Type2.Text.Substring(1, 99).Trim
            Dim M_path As String = Server.MapPath("Reports\MIS REPORTS\Consolidation\MIS\Fund Management\FUNDMGMT1.rpt")
            Str_Flf = "Company|" & Company_Name & "|ChartHeading|" & "Balance of " & ddl_acc_report_1.SelectedItem.Text.Trim

            Dim M_vali As Boolean = M_MisReort.Mis_BalanceReport(Acc_Type_1, StrDate, BranchList, System_Name)

            If M_vali = True Then
                StrSql = " SELECT     MAX(Mis_RptSetting_M_Tbl.Rpt_Name) AS RptName, Acc_LedgerBalance_R_Tbl.Tr_Date, ABS(SUM(Acc_LedgerBalance_R_Tbl.Clos_Balance)) "
                StrSql = StrSql + "               AS Balance"
                StrSql = StrSql + " FROM         Acc_LedgerBalance_R_Tbl INNER JOIN"
                StrSql = StrSql + "                       Mis_Accounts_M_Tbl ON Acc_LedgerBalance_R_Tbl.Acc_Code = Mis_Accounts_M_Tbl.Acc_Code INNER JOIN"
                StrSql = StrSql + "                       Mis_RptSetting_M_Tbl ON Mis_Accounts_M_Tbl.Rpt_Code = Mis_RptSetting_M_Tbl.Rpt_Code AND "
                StrSql = StrSql + "                       Acc_LedgerBalance_R_Tbl.Tr_Date IN (" & StrDate & ") and Mis_RptSetting_M_Tbl.Rpt_Code ='" & Acc_Type_1 & "' "
                StrSql = StrSql + " GROUP BY Acc_LedgerBalance_R_Tbl.Tr_Date, Mis_RptSetting_M_Tbl.Rpt_Code"
                Session.Add("RptQry", StrSql)
                Session.Add("RptPath", M_path)
                Session.Add("RptFormula", Str_Flf)
            End If
            Return M_vali
            'Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Report_Employe(ByVal Acc_Type As String, ByVal StrDate As String, ByVal BranchList As String) As Boolean
        Try

            Dim Str_Flf As String = ""
            Dim HeadStr(2) As String
            Dim M_MisReport As New MisReportBL
            Dim M_commanBL As New CommonBL


            Dim EmployeeCount As String = M_commanBL.EmployeeCount(BranchList)
            Str_Flf = "Company|" & Company_Name & "|ChartHeading|" & "Per Employee " & ddl_acc_report_1.SelectedItem.Text.Trim() & ""
            Str_Flf = Str_Flf & "|NoOfEmp|" & EmployeeCount & " |Head1|" & ddl_acc_report_1.SelectedItem.Text.Trim() & ""
            Dim System_Name As String = Session("HostID")
            HeadStr(0) = ddl_acc_report_1.SelectedItem.Text.Trim()
            'HeadStr(1) =' Cmb_Type2.Text.Substring(1, 99).Trim
            HeadStr(2) = "% of " & ddl_acc_report_1.SelectedItem.Text.Trim() & " to " '& Cmb_Type2.Text.Substring(1, 99).Trim
            'Heading to Passed (@Head1,@Head2,@Head3)

            '@ChartHeading = "% of " & Cmb_Type1.Text.Substring(1, 99).Trim & " to " & Cmb_Type2.Text.Substring(1, 99).Trim

            ' Str_Flf = "Company|" & Company_Name & "|ChartHeading|" & "Balance of " & Cmb_Type1.Text.Substring(0, 99).Trim
            Dim M_vali As Boolean = M_MisReport.Mis_BalanceReport(Acc_Type, StrDate, BranchList, System_Name)

            If M_vali = True Then

                StrSql = " SELECT     MAX(Mis_RptSetting_M_Tbl.Rpt_Name) AS RptName, Acc_LedgerBalance_R_Tbl.Tr_Date, SUM(Acc_LedgerBalance_R_Tbl.Clos_Balance) "
                StrSql = StrSql + "               AS Balance"
                StrSql = StrSql + " FROM         Acc_LedgerBalance_R_Tbl INNER JOIN"
                StrSql = StrSql + "                       Mis_Accounts_M_Tbl ON Acc_LedgerBalance_R_Tbl.Acc_Code = Mis_Accounts_M_Tbl.Acc_Code INNER JOIN"
                StrSql = StrSql + "                       Mis_RptSetting_M_Tbl ON Mis_Accounts_M_Tbl.Rpt_Code = Mis_RptSetting_M_Tbl.Rpt_Code AND "
                StrSql = StrSql + "                       Acc_LedgerBalance_R_Tbl.Tr_Date IN (" & StrDate & ") and Mis_RptSetting_M_Tbl.Rpt_Code ='" & ddl_acc_report_1.SelectedValue.Trim & "' "
                StrSql = StrSql + " GROUP BY Acc_LedgerBalance_R_Tbl.Tr_Date, Mis_RptSetting_M_Tbl.Rpt_Code"


                StrSql = " SELECT  Acc_LedgerBalance_R_Tbl.Tr_Date, "
                StrSql = StrSql + " SUM(case when Mis_RptSetting_M_Tbl.Rpt_Code ='" & ddl_acc_report_1.SelectedValue.Trim & "' then Acc_LedgerBalance_R_Tbl.Clos_Balance else 0 end) AS Balance1"
                StrSql = StrSql + " FROM Acc_LedgerBalance_R_Tbl INNER JOIN"
                StrSql = StrSql + " Mis_Accounts_M_Tbl ON Acc_LedgerBalance_R_Tbl.Acc_Code = Mis_Accounts_M_Tbl.Acc_Code"
                StrSql = StrSql + " INNER Join"
                StrSql = StrSql + " Mis_RptSetting_M_Tbl ON Mis_Accounts_M_Tbl.Rpt_Code = Mis_RptSetting_M_Tbl.Rpt_Code AND "
                StrSql = StrSql + " Acc_LedgerBalance_R_Tbl.Tr_Date IN (" & StrDate & ") "
                StrSql = StrSql + " GROUP BY Acc_LedgerBalance_R_Tbl.Tr_Date"
                Dim M_path As String = Server.MapPath("Reports\MIS REPORTS\Consolidation\MIS\Fund Management\FUNDMGMT3.rpt")
                Session.Add("RptQry", StrSql)
                Session.Add("RptPath", M_path)
                Session.Add("RptFormula", Str_Flf)
            End If
            Return M_vali
        Catch ex As Exception

        End Try
    End Function




End Class