Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL


Public Class DailyCollectionReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                'Dim M_dt As New DataTable
                'M_dt = M_CommonBL.Get_SchemeSectionLoan()
                'Ddlsection.DataTextField = "Sch_Section_Name"
                'Ddlsection.DataValueField = "Sch_Section_Name"
                'Ddlsection.DataSource = M_dt
                'Ddlsection.DataBind()
                Ddlsection.Items.Add("LOAN")
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
            Dim HeaderArr(21, 4) As String
            Dim HeaderWidth(21, 1) As String

            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "R"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "Branch"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "Date"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "Account No"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "13"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "Cust Name"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "Product"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "15"

            HeaderArr(6, 0) = "Scheme"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"

            HeaderArr(7, 0) = "Loan Date"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "5"

            HeaderArr(8, 0) = "Loan Amount"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "12"
            HeaderArr(8, 3) = "12"

            HeaderArr(9, 0) = "EMI"
            HeaderArr(9, 1) = "R"
            HeaderArr(9, 2) = "12"
            HeaderArr(9, 3) = "12"

            HeaderArr(10, 0) = "Installment Number"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "12"
            HeaderArr(10, 3) = "12"

            HeaderArr(11, 0) = "EMI Count"
            HeaderArr(11, 1) = "R"
            HeaderArr(11, 2) = "12"
            HeaderArr(11, 3) = "12"

            HeaderArr(12, 0) = "Arrear status"
            HeaderArr(12, 1) = "R"
            HeaderArr(12, 2) = "12"
            HeaderArr(12, 3) = "12"

            HeaderArr(13, 0) = "Principle"
            HeaderArr(13, 1) = "R"
            HeaderArr(13, 2) = "12"
            HeaderArr(13, 3) = "12"

            HeaderArr(14, 0) = "Interest"
            HeaderArr(14, 1) = "R"
            HeaderArr(14, 2) = "12"
            HeaderArr(14, 3) = "12"

            HeaderArr(15, 0) = "OD"
            HeaderArr(15, 1) = "R"
            HeaderArr(15, 2) = "12"
            HeaderArr(15, 3) = "12"

            HeaderArr(16, 0) = "Other"
            HeaderArr(16, 1) = "R"
            HeaderArr(16, 2) = "12"
            HeaderArr(16, 3) = "12"

            HeaderArr(17, 0) = "Suspense"
            HeaderArr(17, 1) = "R"
            HeaderArr(17, 2) = "12"
            HeaderArr(17, 3) = "12"


            HeaderArr(18, 0) = "Total"
            HeaderArr(18, 1) = "R"
            HeaderArr(18, 2) = "12"
            HeaderArr(18, 3) = "12"

            HeaderArr(19, 0) = "Cash"
            HeaderArr(19, 1) = "R"
            HeaderArr(19, 2) = "12"
            HeaderArr(19, 3) = "12"


            HeaderArr(20, 0) = "Transfer"
            HeaderArr(20, 1) = "R"
            HeaderArr(20, 2) = "12"
            HeaderArr(20, 3) = "12"

            HeaderArr(21, 0) = "CanvasserName"
            HeaderArr(21, 1) = "L"
            HeaderArr(21, 2) = "12"
            HeaderArr(21, 3) = "12"

                    Session("RptHeader") = HeaderArr

            M_Sql = "EXEC Get_LoanCollection_Rpt '" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & DdlBranch.SelectedValue & "','" & DrpType.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("UserID") & "','" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    'Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
    '    Try
    '        Dim M_Dt As New DataTable
    '        Dim M_Sql As String = ""
    '        Dim Br_code As String = DdlBranch.SelectedValue
    '        Dim From_Date As String = DtFrom.Text
    '        Dim To_Date As String = DtTo.Text
    '        'Dim User_Id As String = Session("UserID")
    '        Dim Sch_code As String = DdlScheme.SelectedValue
    '        Dim Section As String = Ddlsection.SelectedValue
    '        Dim SchType As String = DrpType.SelectedValue


    '        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

    '        M_Sql = "Exec  '" + Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(To_Date).ToString("MM/dd/yyyy") + "','" + Br_code + "','" & Sch_code & "','" & Session("HostID") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"



    '        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
    '        ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
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