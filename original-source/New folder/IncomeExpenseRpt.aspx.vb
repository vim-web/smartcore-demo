Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Partial Public Class IncomeExpenseRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Txt_FromDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")

            Dim M_dt As New DataTable
            Dim M_Common_Bl As New CommonBL
            M_Common_Bl.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            M_Common_Bl.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            M_Common_Bl.Fill_UserWiseBranch(DrpBranch2, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))

            M_Common_Bl.Fill_Branch(DrpBranch, True)
            DrpRptType.Items.Add("Port Folio Distribution")
            DrpRptType.Items.Add("Monthly Business")
            DrpRptType.Items.Add("Income & Expense Comparison")
            DrpRptType.Items.Add("Profit & Loss Comparison")
            DrpRptType.Items.Add("Monthly Business Comparison")
            DrpRptType.Items.Add("Interest Received Report")
            DrpRptType.Items.Add("Inter Branch Report")
            ''Dim M_dt As New DataTable
            ''Dim M_Bl As New CommonBL

            M_dt = M_Common_Bl.Get_Pkc_Outstand()
            DrpSection.DataTextField = "Pkc_Description"
            DrpSection.DataValueField = "Pkc_Code"
            DrpSection.DataSource = M_dt
            DrpSection.DataBind()
            DrpSection.Items.Add("ALL")
           
            DrpRptType_SelectedIndexChanged(Me.DrpRptType, System.EventArgs.Empty)
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim Section As String = DrpSection.SelectedValue
            Dim RptHead As String = DrpRptType.SelectedValue


            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

            If DrpRptType.SelectedValue = "Income & Expense Comparison" Then

                M_Sql = "Exec IncomeExpense_Rpt_sp  '" & DrpBranch.SelectedValue & "' ," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(TxtToDate.Text) & ""

                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/IncomeExpenseRpt.rpt"))
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading|Income Expense Report From : " & Txt_FromDate.Text & " To : " & TxtToDate.Text & " |User_ID|" & Session("UserID")

            ElseIf DrpRptType.SelectedValue = "Port Folio Distribution" Then

                M_Sql = "Exec PortFolio_Sp   " & ClsComm.DbDate(Txt_FromDate.Text) & ",'" & Section & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & Session("UserId") & "','" & DrpBranch2.SelectedValue & "'"
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading|Port Folio Distribution As On : " & Txt_FromDate.Text & " |User_ID|" & Session("UserID")
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/PortFliorpt.rpt"))
            ElseIf DrpRptType.SelectedValue = "Monthly Business" Then

                M_Sql = "Exec MonthlyBusinessRpt   " & ClsComm.DbDate(Txt_FromDate.Text) & ""
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
               StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading| Monthly Business Report As On : " & Txt_FromDate.Text & " |User_ID|" & Session("UserID")
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/MonthlyBusinessRpt.rpt"))
            ElseIf DrpRptType.SelectedValue = "Profit & Loss Comparison" Then

                M_Sql = "Exec Get_ProfitLoss_Comp_Sp  " & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(TxtToDate.Text) & ""
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading| Profit & Loss Comparison Report Between  : " & Txt_FromDate.Text & " and "& TxtToDate.Text &" |User_ID|" & Session("UserID")
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/ProfiLoss_Comparison_Rpt.rpt"))
            ElseIf DrpRptType.SelectedValue = "Monthly Business Comparison" Then


                M_Sql = "Exec Business_Comparison_Sp  '" & Section & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(TxtToDate.Text) & ""
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading| Monthly Business Comparison Report Between  : " & Txt_FromDate.Text & " and " & TxtToDate.Text & " |User_ID|" & Session("UserID")
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/Business_Comparison_Rpt.rpt"))

            ElseIf DrpRptType.SelectedValue = "Interest Received Report" Then

                M_Sql = "Exec PortFolio_IntRecvd_Sp  'ALL','" & Section & "'," & ClsComm.DbDate(Txt_FromDate.Text) & ", " & ClsComm.DbDate(TxtToDate.Text) & ""
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading|Interest Received Report Between : " & Txt_FromDate.Text & " And  " & TxtToDate.Text & "|User_ID|" & Session("UserID")
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/PortFolioInterestRcvd_Rpt.rpt"))
            ElseIf DrpRptType.SelectedValue = "Inter Branch Report" Then

                M_Sql = "Exec Get_IBTransDetails_Rpt  '" & DrpBranch.SelectedValue & "'," & ClsComm.DbDate(Txt_FromDate.Text) & ""
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading|Inter Branch Transaction Report As On : " & Txt_FromDate.Text & " |User_ID|" & Session("UserID")
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/IBTrans_Rpt.rpt"))
            End If





            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
            Session.Add("RptQry", M_Sql)
            Session.Add("RptFormula", StrSlf)
            ''Session.Add("RptPath", Server.MapPath("~/Reports/Loans/IncomeExpenseRpt.rpt"))
            Session("SubRptNames") = Nothing
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub DrpRptType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpRptType.SelectedIndexChanged

        If DrpRptType.SelectedValue = "Income & Expense Comparison" Then
            LblBranch.Visible = True
            DrpBranch.Visible = True
            LblSection.Visible = False
            DrpSection.Visible = False
            LblFromdt.Visible = True
            Txt_FromDate.Visible = True
            LblTodt.Visible = True
            TxtToDate.Visible = True
            LblFromdt.Text = "From Date"
            LblTodt.Text = "To Date"
            LblRegion.Visible = False
            DrpRegn.Visible = False
            LblArea.Visible = False
            DrpArea.Visible = False
            LblBranch2.Visible = False
            DrpBranch2.Visible = False
        ElseIf DrpRptType.SelectedValue = "Port Folio Distribution" Then
            LblBranch.Visible = False
            DrpBranch.Visible = False
            DrpSection.Visible = True
            LblSection.Visible = True
            LblFromdt.Text = "As On Date"
            Txt_FromDate.Visible = True
            LblTodt.Visible = False
            TxtToDate.Visible = False
            LblRegion.Visible = True
            DrpRegn.Visible = True
            LblArea.Visible = True
            DrpArea.Visible = True
            LblBranch2.Visible = True
            DrpBranch2.Visible = True
        ElseIf DrpRptType.SelectedValue = "Inter Branch Report" Then
            LblBranch.Visible = True
            DrpBranch.Visible = True
            DrpSection.Visible = False
            LblSection.Visible = False
            LblFromdt.Text = "As On Date"
            Txt_FromDate.Visible = True
            LblTodt.Visible = False
            TxtToDate.Visible = False
            LblRegion.Visible = False
            DrpRegn.Visible = False
            LblArea.Visible = False
            DrpArea.Visible = False
            LblBranch2.Visible = False
            DrpBranch2.Visible = False
        ElseIf DrpRptType.SelectedValue = "Interest Received Report" Then
            LblBranch.Visible = False
            DrpBranch.Visible = False
            LblSection.Visible = True
            DrpSection.Visible = True
            LblTodt.Visible = True
            TxtToDate.Visible = True
            Txt_FromDate.Visible = True
            LblFromdt.Text = "From Date"
            LblRegion.Visible = False
            DrpRegn.Visible = False
            LblArea.Visible = False
            DrpArea.Visible = False
            LblBranch2.Visible = False
            DrpBranch2.Visible = False
        ElseIf DrpRptType.SelectedValue = "Monthly Business" Then
            LblBranch.Visible = False
            DrpBranch.Visible = False
            LblSection.Visible = False
            DrpSection.Visible = False
            LblTodt.Visible = False
            TxtToDate.Visible = False
            Txt_FromDate.Visible = True
            LblFromdt.Text = "As On Date"
            LblRegion.Visible = False
            DrpRegn.Visible = False
            LblArea.Visible = False
            DrpArea.Visible = False
            LblBranch2.Visible = False
            DrpBranch2.Visible = False
        ElseIf DrpRptType.SelectedValue = "Profit & Loss Comparison" Then
            LblBranch.Visible = False
            DrpBranch.Visible = False
            LblSection.Visible = False
            DrpSection.Visible = False
            LblTodt.Visible = True
            TxtToDate.Visible = True
            Txt_FromDate.Visible = True
            LblFromdt.Text = "From Date"
            LblRegion.Visible = False
            DrpRegn.Visible = False
            LblArea.Visible = False
            DrpArea.Visible = False
            LblBranch2.Visible = False
            DrpBranch2.Visible = False
       Else If DrpRptType.SelectedValue ="Monthly Business Comparison" then
            LblBranch.Visible = False
            DrpBranch.Visible = False
            DrpSection.Visible = True
            LblSection.Visible = True
            LblFromdt.Text = "From Date"
            Txt_FromDate.Visible = True
            LblTodt.Visible = true
            TxtToDate.Visible = True
            LblRegion.Visible = False
            DrpRegn.Visible = False
            LblArea.Visible = False
            DrpArea.Visible = False
            LblBranch2.Visible = False
            DrpBranch2.Visible = False
        End If


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
            M_CommonBl.Fill_UserWiseBranch(DrpBranch2, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            DrpBranch2.Items.Remove(DrpBranch2.Items.FindByText("ALL"))
            DrpBranch2.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub
End Class