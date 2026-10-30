Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL


Partial Public Class InvestmentReport
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Private Rpt_Qry As String
    Private Rpt_Path As String
    Private Rpt_Heading As String
    Private Rpt_Formula As String
    Private Rpt_ProcessQry As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<--ALL-->")
                'If Session("option") = "LoanRate" Then
                '    M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")
                'Else
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "INVESTMENT")

                'End If
                DdlScheme.Items.Add("<--ALL-->")

                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1

                DdlReportOptions.Items.Add("---SELECT---")
                DdlReportOptions.Items.Add("Matured List")
                DdlReportOptions.Items.Add("Due Date Diary")
                DdlReportOptions.Items.Add("Amount Wise Investments")
                DdlReportOptions.Items.Add("Interest Rate Wise")
                DdlReportOptions.Items.Add("Period Wise")
                DdlReportOptions.Items.Add("Opening Register")
                DdlReportOptions.Items.Add("Closing Register")

                ' ControlVisibility()
                M_CommonBL = Nothing
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Public Sub FillRptControls(ByVal RptID As Integer)
        Try
            Dim M_DataTable As DataTable = New DataTable()
            Dim M_Dt As DataTable = New DataTable()
            Dim I As Integer = 1
            Dim M_Sql As String

            'M_Sql = "Select * from  Report_Sub_Tbl "
            'M_Sql = M_Sql & " Where Rpt_ID=" & DdlRptType.SelectedValue
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            DdlBranch.Visible = False
            DdlScheme.Visible = False
            DdlBranch.Items.Clear()
            DdlScheme.Items.Clear()

            LblDdl1.Text = ""
            LblDdl2.Text = ""
            LblDt1.Text = ""
            LblDt2.Text = ""
            LblTxt3.Text = ""
            LblTxt4.Text = ""
            LblTxt1.Text = ""
            LblTxt2.Text = ""

            While M_Dt.Rows.Count >= I
                If M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "DDL" Then ' DropdownList
                    If M_Dt.Rows(I - 1)("Ctrl_Order").ToString() = 1 Then
                        DdlBranch.DataTextField = M_Dt.Rows(I - 1)("Ctrl_Display").ToString()
                        DdlBranch.DataValueField = M_Dt.Rows(I - 1)("Ctrl_Value").ToString()
                        M_Sql = M_Dt.Rows(I - 1)("Ctr_Qry").ToString()
                        M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)
                        DdlBranch.DataSource = M_DataTable
                        DdlBranch.DataBind()
                        If M_Sql = M_Dt.Rows(I - 1)("Ctr_AllOption").ToString() Then
                            DdlBranch.Items.Add(" <--- SELECT --->")
                        Else
                            If DdlBranch.Items.Count > 0 Then DdlBranch.SelectedIndex = 0
                        End If
                        DdlBranch.Visible = True

                        LblDdl1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()

                    ElseIf M_Dt.Rows(I - 1)("Ctrl_Order").ToString() = 2 Then
                        DdlScheme.DataTextField = M_Dt.Rows(I - 1)("Ctrl_Display").ToString()
                        DdlScheme.DataValueField = M_Dt.Rows(I - 1)("Ctrl_Value").ToString()
                        M_Sql = M_Dt.Rows(I - 1)("Ctr_Qry").ToString()
                        M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)
                        DdlScheme.DataSource = M_DataTable
                        DdlScheme.DataBind()
                        If M_Sql = M_Dt.Rows(I - 1)("Ctr_AllOption").ToString() Then
                            DdlScheme.Items.Add(" <--- SELECT --->")
                        Else
                            If DdlScheme.Items.Count > 0 Then DdlBranch.SelectedIndex = 0
                        End If
                        DdlScheme.Visible = True

                        LblDdl2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End If
                ElseIf M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "DT" Then ' DropdownList
                    Select Case M_Dt.Rows(I - 1)("Ctrl_Order").ToString()
                        Case 1
                            LblDt1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 2
                            LblDt2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 3
                            LblTxt3.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 4
                            LblTxt4.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End Select
                ElseIf M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "TXT" Then ' DropdownList
                    Select Case M_Dt.Rows(I - 1)("Ctrl_Order").ToString()
                        Case 1
                            LblTxt1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 2
                            LblTxt2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End Select
                End If
                I = I + 1
            End While
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    'Protected Sub DdlSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlSection.SelectedIndexChanged
    '    Call FillRptType(DdlSection.SelectedValue)
    'End Sub


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Sub ControlVisibility()
        Try
            LblTxt1.Visible = False
            LblTxt2.Visible = False
            LblTxt3.Visible = False
            LblTxt4.Visible = False
            LblTxt1.Visible = False
            LblTxt2.Visible = False
            Dt3.Visible = False
            Dt4.Visible = False
            Txt1.Visible = False
            Txt2.Visible = False
            DdlPeriodFrom.Visible = False
            DdlPeriodTo.Visible = False

            If DdlReportOptions.Text = "Matured List" Then
                LblTxt3.Visible = True
                'LblDt3.Text = "As On"
                LblDt1.Text = "As On"
                DtTo.Visible = False
                LblDt2.Visible = False
                LblTxt4.Visible = False
                LblTxt1.Visible = False
                LblTxt2.Visible = False
                LblTxt3.Visible = False
                DtFrom.Text = Session("TransDate")
            ElseIf DdlReportOptions.Text = "Due Date Diary" Then
                LblDt1.Text = "From Date"

                LblTxt1.Visible = False
                LblTxt2.Visible = False
                LblTxt3.Visible = False
                LblTxt4.Visible = False
                LblDt2.Visible = True

                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
                LblDt2.Text = "To Date"
                DtTo.Visible = True
                'LblTxt1.Visible = True
                'LblTxt2.Visible = True
            ElseIf DdlReportOptions.Text = "Amount Wise Investments" Then
                LblDt1.Text = "From Date"
                'LblDt3.Visible = True
                'LblDt3.Text = "As On"
                LblDt2.Visible = True
                DtTo.Visible = True

                LblTxt1.Visible = True
                LblTxt1.Text = "From Amount"
                Txt1.Visible = True
                LblTxt2.Text = "To Amount"
                LblTxt2.Visible = True
                Txt2.Visible = True

                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
            ElseIf DdlReportOptions.Text = "Interest Rate Wise" Then
                LblDt1.Text = "From Date"
                'LblDt3.Visible = True
                'LblDt3.Text = "As On"
                LblDt2.Visible = True
                DtTo.Visible = True

                LblTxt1.Visible = True
                LblTxt1.Text = "Rate From"
                Txt1.Visible = True
                LblTxt2.Text = "Rate To"
                LblTxt2.Visible = True
                Txt2.Visible = True
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")


            ElseIf DdlReportOptions.Text = "Period Wise" Then
                LblDt1.Text = "From Date"

                LblDt2.Visible = True
                DtTo.Visible = True

                LblTxt1.Visible = True
                LblTxt1.Text = "Period From"
                Txt1.Visible = True
                LblTxt2.Text = "Period To"
                LblTxt2.Visible = True
                Txt2.Visible = True
                'LblTxt3.Visible = True
                'LblTxt4.Visible = True
                DdlPeriodFrom.Visible = True
                DdlPeriodTo.Visible = True
                DdlPeriodFrom.Items.Add("M")
                DdlPeriodFrom.Items.Add("D")
                DdlPeriodTo.Items.Add("M")
                DdlPeriodTo.Items.Add("D")

                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")

            ElseIf DdlReportOptions.Text = "Opening Register" Then
                LblDt1.Text = "From Date"

                LblTxt1.Visible = False
                LblTxt2.Visible = False
                LblTxt3.Visible = False
                LblTxt4.Visible = False
                LblDt2.Visible = True

                LblDt2.Text = "To Date"
                DtTo.Visible = True
                'LblTxt1.Visible = True
                'LblTxt2.Visible = True

                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")

            ElseIf DdlReportOptions.Text = "Closing Register" Then
                LblDt1.Text = "From Date"

                LblTxt1.Visible = False
                LblTxt2.Visible = False
                LblTxt3.Visible = False
                LblTxt4.Visible = False
                LblDt2.Visible = True

                LblDt2.Text = "To Date"
                DtTo.Visible = True
                'LblTxt1.Visible = True
                'LblTxt2.Visible = True

                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable
            Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
            Dim Brcode As String
            Dim SchemeCode As String
            If DdlBranch.SelectedValue <> "<--ALL-->" Then
                Brcode = DdlBranch.SelectedValue
            Else
                Brcode = "0"
            End If
            If DdlScheme.SelectedValue <> "<--ALL-->" Then
                SchemeCode = DdlScheme.SelectedValue
            Else
                SchemeCode = "0"
            End If

            M_Sql = Rpt_Qry '.ToUpper()


            ' Formula From Sql Query Output 

            Dim M_Qry As String = ""
            Dim M_QryFormula As String = ""
            Dim QryVar As String = ""
            Dim QryNo1 As Integer = "0"

            If DdlReportOptions.Text = "Due Date Diary" Then

                Dim HeaderArr(5, 4) As String
                Dim HeaderWidth(5, 1) As String
                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "Dep Date"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "10"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "Due Date"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "10"

                HeaderArr(3, 0) = "Dep No"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"


                HeaderArr(4, 0) = "Cust Name"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "22"
                HeaderArr(4, 3) = "22"

                HeaderArr(5, 0) = "Balance"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "12"
                HeaderArr(5, 3) = "12"



                Session("RptHeader") = HeaderArr

                M_Sql = "EXEC [InvestmentMatruedDueDateRpt_Sp] '" & Brcode & "','" & SchemeCode & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "','Y','Y','Y','DEPOSIT'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub

                'Rpt_Path = "Dep_MaturityList.rpt"
                'M_Qry = "EXEC [Proc_Deposit_MaturedListRPT] '" & Brcode & "','" & SchemeCode & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "','Y','Y','Y','DEPOSIT'"

            ElseIf DdlReportOptions.Text = "Matured List" Then

                Dim HeaderArr(5, 4) As String
                Dim HeaderWidth(5, 1) As String
                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "Dep Date"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "12"
                HeaderArr(1, 3) = "12"

                HeaderArr(2, 0) = "Due Date"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "12"
                HeaderArr(2, 3) = "12"

                HeaderArr(3, 0) = "Dep No"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"


                HeaderArr(4, 0) = "Cust Name"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "20"
                HeaderArr(4, 3) = "20"

                HeaderArr(5, 0) = "Balance"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "11"
                HeaderArr(5, 3) = "11"



                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC [InvestmentMatruedDueDateRpt_Sp] '" & Brcode & "','" & SchemeCode & "',NULL,'" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "','Y','Y','Y','DEPOSIT'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub


                'Rpt_Path = "Dep_MaturityList.rpt"
                'M_Qry = "EXEC [Proc_Deposit_MaturedListRPT] '" & Brcode & "','" & SchemeCode & "',NULL,'" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Session("LoginID").ToString & "','" & Session("HostID") & "','Y','Y','Y','DEPOSIT'"
            ElseIf DdlReportOptions.Text = "Interest Rate Wise" Then

                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String
                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "Account No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "NAME"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "20"
                HeaderArr(2, 3) = "20"


                HeaderArr(3, 0) = "Dep Date"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "Due Date"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Rate"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "5"
                HeaderArr(4, 3) = "5"

                HeaderArr(6, 0) = "Balance"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "12"
                HeaderArr(6, 3) = "12"



                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC [InvestmentIntRateWiseRpt_Sp] '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Txt1.Text & "','" & Txt2.Text & "', '" & SchemeCode & "','" & Brcode & "'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub

            ElseIf DdlReportOptions.Text = "Period Wise" Then

                Dim HeaderArr(7, 7) As String
                Dim HeaderWidth(4, 1) As String

                HeaderArr(0, 0) = "SL.NO"
                HeaderArr(0, 1) = "R"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "ACCOUNT NO"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "DEP DATE"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "10"

                HeaderArr(3, 0) = "PERIOD"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "6"
                HeaderArr(3, 3) = "6"

                HeaderArr(4, 0) = "NAME"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "20"
                HeaderArr(4, 3) = "20"

                HeaderArr(5, 0) = "AMOUNT"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "11"
                HeaderArr(5, 3) = "11"

                HeaderArr(6, 0) = "INT RATE"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "8"
                HeaderArr(6, 3) = "8"

                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC Proc_PeriodwiseInvestment '" & Brcode & "','" & SchemeCode & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "'," & Val(Txt1.Text) & "," & Val(Txt2.Text) & ",'" & (DdlPeriodFrom.Text) & "','" & (DdlPeriodTo.Text) & "','" & Session("LoginID") & "','" & Session("HostID") & "','Y'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=130", False)
                Exit Sub

            ElseIf DdlReportOptions.Text = "Amount Wise Investments" Then
                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String

                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "Account No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"


                HeaderArr(2, 0) = "Cust Name"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "18"
                HeaderArr(2, 3) = "18"

                HeaderArr(3, 0) = "Date"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "D/C"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "3"
                HeaderArr(4, 3) = "3"

                HeaderArr(5, 0) = "Cash"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "11"
                HeaderArr(5, 3) = "11"


                HeaderArr(6, 0) = "Transfer"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "11"
                HeaderArr(6, 3) = "11"


                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC [InvestmentAmountWiseRpt_Sp] '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Txt1.Text & "','" & Txt2.Text & "', '" & SchemeCode & "','" & Brcode & "'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub


            ElseIf DdlReportOptions.Text = "Opening Register" Then
                Dim HeaderArr(8, 4) As String
                Dim HeaderWidth(8, 1) As String

                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "Acc No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"


                HeaderArr(2, 0) = "Name"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "15"

                HeaderArr(3, 0) = "DepDate"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "DepAmt"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "12"
                HeaderArr(4, 3) = "12"

                HeaderArr(5, 0) = "Months"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "6"
                HeaderArr(5, 3) = "6"


                HeaderArr(6, 0) = "Days"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "4"
                HeaderArr(6, 3) = "4"


                HeaderArr(7, 0) = "Rate"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "4"
                HeaderArr(7, 3) = "4"

                HeaderArr(8, 0) = "Balance"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "12"
                HeaderArr(8, 3) = "12"

                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC [InvestmentOpeningClosingRpt_Sp] '" & Brcode & "', '" & SchemeCode & "', '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "','O'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub


            ElseIf DdlReportOptions.Text = "Closing Register" Then
                Dim HeaderArr(8, 4) As String
                Dim HeaderWidth(8, 1) As String

                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "Acc No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"


                HeaderArr(2, 0) = "Name"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "15"

                HeaderArr(3, 0) = "DepDate"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "DepAmt"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "12"
                HeaderArr(4, 3) = "12"

                HeaderArr(5, 0) = "Months"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "6"
                HeaderArr(5, 3) = "6"


                HeaderArr(6, 0) = "Days"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "4"
                HeaderArr(6, 3) = "4"


                HeaderArr(7, 0) = "Rate"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "4"
                HeaderArr(7, 3) = "4"

                HeaderArr(8, 0) = "CloseDate"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "10"
                HeaderArr(8, 3) = "10"

                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC [InvestmentOpeningClosingRpt_Sp] '" & Brcode & "', '" & SchemeCode & "', '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "','C'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub

            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub DdlReportOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlReportOptions.SelectedIndexChanged
        ControlVisibility()

    End Sub
End Class