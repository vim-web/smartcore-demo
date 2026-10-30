Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class Loan_NPA_Rpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
                M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
                DdlBranch.Items.Add("ALL")
                Ddlsection.Items.Add("LOAN")
                DtFrom.Text = Session("TransDate")

                Ddlsection_SelectedIndexChanged(Me.Ddlsection, System.EventArgs.Empty)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_dt As New DataTable
            Dim M_Sql As String = ""
            Dim Br_code As String = DdlBranch.SelectedValue
            Dim From_Date As String = DtFrom.Text
            Dim Sch_code As String = DdlScheme.SelectedValue
            Dim SchType As String = DrpType.SelectedValue
            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

            M_Sql = " Exec Process_NPA_Report_Sp '" & Br_code & "','" & SchType & "','" & Sch_code & "','" & Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") & "','" & Session("HostID") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            M_DBConn.ExecuteDataTable(M_dt, M_Sql, False)
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

    Protected Sub Ddlsection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddlsection.SelectedIndexChanged
        Try

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
    Private Sub DrpType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrpType.SelectedIndexChanged
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim clscomm As New CommonClass
            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            Dim Br_code As String = DdlBranch.SelectedValue
            Dim From_Date As String = DtFrom.Text
            Dim Sch_code As String = DdlScheme.SelectedValue
            Dim SchType As String = DrpType.SelectedValue

            Dim HeaderArr(19, 4) As String
            Dim HeaderWidth(19, 1) As String
            HeaderArr(0, 0) = "CustId"
            HeaderArr(0, 1) = "R"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "4"

            HeaderArr(1, 0) = "Cust Name"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "12"
            HeaderArr(1, 3) = "20"

            HeaderArr(2, 0) = "Acc No"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "24"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "Loan Date"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "5"
        

            HeaderArr(4, 0) = "PrdType"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "4"
            HeaderArr(4, 3) = "4"

            HeaderArr(5, 0) = "Prd"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "4"
            HeaderArr(5, 3) = "4"

            HeaderArr(6, 0) = "Due Date"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "5"
            HeaderArr(6, 3) = "5"

            HeaderArr(7, 0) = "Def.Inst"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "5"

            HeaderArr(8, 0) = "Def.Amount"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "8"
            HeaderArr(8, 3) = "8"

            HeaderArr(9, 0) = "Last InstDt"
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "5"
            HeaderArr(9, 3) = "5"

            HeaderArr(10, 0) = "Last.PaidDt"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "5"
            HeaderArr(10, 3) = "5"

            HeaderArr(11, 0) = "Outstand"
            HeaderArr(11, 1) = "R"
            HeaderArr(11, 2) = "8"
            HeaderArr(11, 3) = "8"

            HeaderArr(12, 0) = "Prn.Pending"
            HeaderArr(12, 1) = "R"
            HeaderArr(12, 2) = "8"
            HeaderArr(12, 3) = "8"

            HeaderArr(13, 0) = "Prov.Std"
            HeaderArr(13, 1) = "R"
            HeaderArr(13, 2) = "8"
            HeaderArr(13, 3) = "8"

            HeaderArr(14, 0) = "Prov.SubStd"
            HeaderArr(14, 1) = "R"
            HeaderArr(14, 2) = "8"
            HeaderArr(14, 3) = "8"

            HeaderArr(15, 0) = "Prov.DB1"
            HeaderArr(15, 1) = "R"
            HeaderArr(15, 2) = "8"
            HeaderArr(15, 3) = "8"

            HeaderArr(16, 0) = "Prov.DB2"
            HeaderArr(16, 1) = "R"
            HeaderArr(16, 2) = "8"
            HeaderArr(16, 3) = "8"

            HeaderArr(17, 0) = "Prov.DB3"
            HeaderArr(17, 1) = "R"
            HeaderArr(17, 2) = "8"
            HeaderArr(17, 3) = "8"

            HeaderArr(18, 0) = "Loss"
            HeaderArr(18, 1) = "R"
            HeaderArr(18, 2) = "8"
            HeaderArr(18, 3) = "8"

            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC [Get_Npa_List_Sp] '" & Br_code & "','" & SchType & "','" & Sch_code & "','" & Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") & "','" & Session("HostID") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)
        Catch ex As Exception

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