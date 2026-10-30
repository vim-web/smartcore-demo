Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class Outstand
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
                M_dt = M_CommonBL.Get_SchemeSection
                Ddlsection.DataTextField = "Sch_Section_Name"
                Ddlsection.DataValueField = "Sch_Section_Name"
                Ddlsection.DataSource = M_dt
                Ddlsection.DataBind()
                'M_dt = M_CommonBL.Get_Scheme(Ddlsection.SelectedValue)
                'DdlScheme.DataTextField = "Sch_Name"
                'DdlScheme.DataValueField = "Sch_Code"
                'DdlScheme.DataSource = M_dt
                'DdlScheme.DataBind()
                DdlScheme.Items.Add("ALL")
                M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
                DdlBranch.Items.Add("ALL")
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")

                Ddlsection_SelectedIndexChanged(Me.Ddlsection, System.EventArgs.Empty)

            End If
         
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Ddlsection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddlsection.SelectedIndexChanged
        Try
            'Dim M_CommonBL As New CommonBL
            'Dim M_dt As New DataTable
            'M_dt = M_CommonBL.Get_Scheme(Ddlsection.SelectedValue)
            'DdlScheme.DataTextField = "Sch_Name"
            'DdlScheme.DataValueField = "Sch_Code"
            'DdlScheme.DataSource = M_dt
            'DdlScheme.DataBind()
            'DdlScheme.Items.Add("ALL")


            Dim M_CommonBL As New CommonBL
            Dim M_dt As New DataTable
            Dim LoanType As String = ""

            If Ddlsection.SelectedItem.Text = "LOAN" Then
                M_CommonBL.Fill_Pickup(DrpType, 102, 0)
                DrpType.Items.Add("ALL")
                LoanType = DrpType.SelectedValue.ToString()
            ElseIf Ddlsection.SelectedItem.Text = "DEPOSIT" Then
                M_CommonBL.Fill_Pickup(DrpType, 109, 0)
                DrpType.Items.Add("ALL")
                LoanType = DrpType.SelectedValue.ToString()
            ElseIf Ddlsection.SelectedItem.Text = "SHARE" Then
                M_CommonBL.Fill_Pickup(DrpType, 110, 0)
                DrpType.Items.Add("ALL")
                LoanType = DrpType.SelectedValue.ToString()
            End If


            M_dt = M_CommonBL.Get_Scheme(Ddlsection.SelectedValue, LoanType)
            DdlScheme.DataTextField = "Sch_Name"
            DdlScheme.DataValueField = "Sch_Code"
            DdlScheme.DataSource = M_dt
            DdlScheme.DataBind()
            DdlScheme.Items.Add("ALL")

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

            Dim Br_code As String = DdlBranch.SelectedValue
            Dim From_Date As String = DtFrom.Text
            Dim To_Date As String = DtTo.Text
            Dim User_Id As String = Session("UserID")
            Dim Sch_code As String = DdlScheme.SelectedValue
            Dim Section As String = Ddlsection.SelectedValue


            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            If ChkDep.Checked = True Then
                If ChkToExcel.Checked = True Then

                    Dim HeaderArr(12, 4) As String
                    Dim HeaderWidth(12, 1) As String
                    HeaderArr(0, 0) = "SLN0"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "5"
                    HeaderArr(0, 3) = "5"

                    HeaderArr(1, 0) = "Cust Name"
                    HeaderArr(1, 1) = "L"
                    HeaderArr(1, 2) = "22"
                    HeaderArr(1, 3) = "22"

                    HeaderArr(3, 0) = "Scheme"
                    HeaderArr(3, 1) = "L"
                    HeaderArr(3, 2) = "22"
                    HeaderArr(3, 3) = "22"

                    HeaderArr(2, 0) = "Acc No"
                    HeaderArr(2, 1) = "L"
                    HeaderArr(2, 2) = "13"
                    HeaderArr(2, 3) = "13"

                    HeaderArr(4, 0) = "Dep Date"
                    HeaderArr(4, 1) = "L"
                    HeaderArr(4, 2) = "10"
                    HeaderArr(4, 3) = "10"

                    HeaderArr(5, 0) = "Pay Mode"
                    HeaderArr(5, 1) = "L"
                    HeaderArr(5, 2) = "22"
                    HeaderArr(5, 3) = "22"

                    HeaderArr(6, 0) = "Int Due Date"
                    HeaderArr(6, 1) = "L"
                    HeaderArr(6, 2) = "10"
                    HeaderArr(6, 3) = "10"

                    HeaderArr(7, 0) = "Int Rate"
                    HeaderArr(7, 1) = "L"
                    HeaderArr(7, 2) = "5"
                    HeaderArr(7, 3) = "5"

                    HeaderArr(8, 0) = "Dep Amount"
                    HeaderArr(8, 1) = "R"
                    HeaderArr(8, 2) = "12"
                    HeaderArr(8, 3) = "12"

                    HeaderArr(9, 0) = "Interest"
                    HeaderArr(9, 1) = "R"
                    HeaderArr(9, 2) = "12"
                    HeaderArr(9, 3) = "12"

                    HeaderArr(10, 0) = "TDS"
                    HeaderArr(10, 1) = "R"
                    HeaderArr(10, 2) = "12"
                    HeaderArr(10, 3) = "12"

                    HeaderArr(11, 0) = "Net Amnt"
                    HeaderArr(11, 1) = "R"
                    HeaderArr(11, 2) = "12"
                    HeaderArr(11, 3) = "12"


                    Session("RptHeader") = HeaderArr
                    If ChckDay.Checked = True Then
                        M_Sql = "EXEC [Get_depositMonthly_OrderByDay_Sp] '" & DdlBranch.SelectedValue & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("HostID") & "','" & DdlScheme.SelectedValue & "'"
                        Session("RptQry") = M_Sql
                        Response.Redirect("~/PrnView.aspx?PageType=80", False)
                    Else
                        M_Sql = "EXEC [Get_depositMonthly_Sp] '" & DdlBranch.SelectedValue & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("HostID") & "','" & DdlScheme.SelectedValue & "'"
                        Session("RptQry") = M_Sql
                        Response.Redirect("~/PrnView.aspx?PageType=80", False)
                    End If
                   

                    Exit Sub
                End If
                M_Sql = " Select * from Temp_Dep_DemandTbl where Terminal='" & Session("HostID") & "'"
                If ChckDay.Checked = True And ChkDep.Checked = True Then
                    M_Sql = M_Sql + " ORDER BY CAST(DAY(IntDueDate)AS INT) ASC  "
                End If
            ElseIf ChkDep.Checked = True And ChkToExcel.Checked = False Then

                M_Sql = "SELECT * FROM Outstand_R_Tbl where Terminal='" & Session("HostID") & "' "
            Else
                'M_Sql = "SELECT * FROM Outstand_R_Tbl where Terminal='" & Session("HostID") & "' "
                If Ddlsection.SelectedItem.Text = "DEPOSIT" Then


                    Dim HeaderArr(12, 4) As String
                    Dim HeaderWidth(12, 1) As String
                    HeaderArr(0, 0) = "SLN0"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "5"
                    HeaderArr(0, 3) = "5"

                    HeaderArr(1, 0) = "Cust Name"
                    HeaderArr(1, 1) = "L"
                    HeaderArr(1, 2) = "22"
                    HeaderArr(1, 3) = "22"

                    HeaderArr(3, 0) = "Acc No"
                    HeaderArr(3, 1) = "L"
                    HeaderArr(3, 2) = "13"
                    HeaderArr(3, 3) = "13"

                    HeaderArr(2, 0) = "Scheme"
                    HeaderArr(2, 1) = "L"
                    HeaderArr(2, 2) = "22"
                    HeaderArr(2, 3) = "22"

                    HeaderArr(4, 0) = "Dep Date"
                    HeaderArr(4, 1) = "L"
                    HeaderArr(4, 2) = "10"
                    HeaderArr(4, 3) = "10"

                    HeaderArr(5, 0) = "Due Date"
                    HeaderArr(5, 1) = "L"
                    HeaderArr(5, 2) = "10"
                    HeaderArr(5, 3) = "10"

                    HeaderArr(6, 0) = "Int Rate"
                    HeaderArr(6, 1) = "L"
                    HeaderArr(6, 2) = "5"
                    HeaderArr(6, 3) = "5"

                    HeaderArr(7, 0) = "Deposit Amount"
                    HeaderArr(7, 1) = "R"
                    HeaderArr(7, 2) = "12"
                    HeaderArr(7, 3) = "12"

                    HeaderArr(8, 0) = "Outstand"
                    HeaderArr(8, 1) = "R"
                    HeaderArr(8, 2) = "12"
                    HeaderArr(8, 3) = "12"


                    HeaderArr(9, 0) = "Interest Paid"
                    HeaderArr(9, 1) = "R"
                    HeaderArr(9, 2) = "12"
                    HeaderArr(9, 3) = "12"

                    HeaderArr(10, 0) = "Payable"
                    HeaderArr(10, 1) = "R"
                    HeaderArr(10, 2) = "12"
                    HeaderArr(10, 3) = "12"

                    Session("RptHeader") = HeaderArr
                Else
                    Dim HeaderArr(12, 4) As String
                    Dim HeaderWidth(12, 1) As String
                    HeaderArr(0, 0) = "SLN0"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "5"
                    HeaderArr(0, 3) = "5"

                    HeaderArr(1, 0) = "Cust Name"
                    HeaderArr(1, 1) = "L"
                    HeaderArr(1, 2) = "22"
                    HeaderArr(1, 3) = "22"

                    HeaderArr(2, 0) = "Scheme"
                    HeaderArr(2, 1) = "L"
                    HeaderArr(2, 2) = "22"
                    HeaderArr(2, 3) = "22"

                    HeaderArr(3, 0) = "Acc No"
                    HeaderArr(3, 1) = "L"
                    HeaderArr(3, 2) = "13"
                    HeaderArr(3, 3) = "13"

                    HeaderArr(4, 0) = "Loan Date"
                    HeaderArr(4, 1) = "L"
                    HeaderArr(4, 2) = "10"
                    HeaderArr(4, 3) = "10"

                    HeaderArr(5, 0) = "Due Date"
                    HeaderArr(5, 1) = "L"
                    HeaderArr(5, 2) = "10"
                    HeaderArr(5, 3) = "10"

                    HeaderArr(6, 0) = "Int Rate"
                    HeaderArr(6, 1) = "L"
                    HeaderArr(6, 2) = "5"
                    HeaderArr(6, 3) = "5"

                    HeaderArr(7, 0) = "Loan Amount"
                    HeaderArr(7, 1) = "R"
                    HeaderArr(7, 2) = "12"
                    HeaderArr(7, 3) = "12"

                    HeaderArr(8, 0) = "Outstand"
                    HeaderArr(8, 1) = "R"
                    HeaderArr(8, 2) = "12"
                    HeaderArr(8, 3) = "12"


                    HeaderArr(9, 0) = "Interest Received"
                    HeaderArr(9, 1) = "R"
                    HeaderArr(9, 2) = "12"
                    HeaderArr(9, 3) = "12"

                    HeaderArr(10, 0) = "Receivable"
                    HeaderArr(10, 1) = "R"
                    HeaderArr(10, 2) = "12"
                    HeaderArr(10, 3) = "12"

                    HeaderArr(11, 0) = "EMI"
                    HeaderArr(11, 1) = "R"
                    HeaderArr(11, 2) = "13"
                    HeaderArr(11, 3) = "13"

                    HeaderArr(12, 0) = "Last Paid Date"
                    HeaderArr(12, 1) = "R"
                    HeaderArr(12, 2) = "13"
                    HeaderArr(12, 3) = "12"


                    Session("RptHeader") = HeaderArr
                End If
                M_Sql = "EXEC [Get_DepsitOutstandRpt2New_sp] '" & Session("HostID") & "','" & DdlScheme.SelectedValue & "','" & DdlBranch.SelectedValue & "','" & Ddlsection.SelectedItem.Text & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
                Exit Sub
            End If



            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|" & Section & " Schedule Statement From " & DtFrom.Text & " To " & DtTo.Text & " |User_ID|" & User_Id
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
            Session.Add("RptQry", M_Sql)
            Session.Add("RptFormula", StrSlf)
            If Section = "LOAN" And ChkDep.Checked = False Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/OutStandRptType2.rpt"))
            ElseIf ChkDep.Checked = False Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/OutStandDep_Rpt.rpt"))
            ElseIf ChkDep.Checked = True Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/OutStandDepDemand_Rpt.rpt"))
            End If


            Session.Add("RptFormula", StrSlf)
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            'Response.Redirect("~/RptPreview.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Try
            Dim M_Dt As New DataTable
            Dim M_Sql As String = ""
            Dim Br_code As String = DdlBranch.SelectedValue
            Dim From_Date As String = DtFrom.Text
            Dim To_Date As String = DtTo.Text
            'Dim User_Id As String = Session("UserID")
            Dim Sch_code As String = DdlScheme.SelectedValue
            Dim Section As String = Ddlsection.SelectedValue
            Dim SchType As String = DrpType.SelectedValue


            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            If ChkDep.Checked = True Then
                M_Sql = "Exec Get_DpositDemandList_Sp '" + Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(To_Date).ToString("MM/dd/yyyy") + "','" + Br_code + "','" & Sch_code & "','" & Session("HostID") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"

            Else
                M_Sql = "Exec OUTSTAND_TYPE2_SP '" + Br_code + "','" + Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(To_Date).ToString("MM/dd/yyyy") + "','" & Sch_code & "','" & Section & "','" & SchType & "', '" & Session("HostID") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            End If

            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub ChkDep_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkDep.CheckedChanged

    End Sub

    Private Sub DrpType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DrpType.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_dt As New DataTable
            M_dt = M_CommonBL.Get_Scheme(Ddlsection.SelectedValue, DrpType.SelectedValue)
            DdlScheme.DataTextField = "Sch_Name"
            DdlScheme.DataValueField = "Sch_Code"
            DdlScheme.DataSource = M_dt
            DdlScheme.DataBind()
            DdlScheme.Items.Add("ALL")
        Catch ex As Exception

        End Try
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