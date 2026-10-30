Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL



Public Class GuarantorDetails_Rpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
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

            Dim User_Id As String = Session("UserID")
            Dim Sch_code As String = DdlScheme.SelectedValue
            Dim Section As String = Ddlsection.SelectedValue


            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            Dim HeaderArr(13, 4) As String
            Dim HeaderWidth(13, 1) As String

            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "R"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "Branch"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "22"
            HeaderArr(1, 3) = "22"

            HeaderArr(2, 0) = "Scheme Type"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "22"
            HeaderArr(2, 3) = "22"

            HeaderArr(3, 0) = "Scheme"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "13"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "Cust Id"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "Cust Name"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "22"
            HeaderArr(5, 3) = "22"

            HeaderArr(6, 0) = "Account Number"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"

            HeaderArr(7, 0) = "Loan Date"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "5"

            HeaderArr(8, 0) = "Due Date"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "12"
            HeaderArr(8, 3) = "12"

            HeaderArr(9, 0) = "Loan Amount"
            HeaderArr(9, 1) = "R"
            HeaderArr(9, 2) = "12"
            HeaderArr(9, 3) = "12"

            HeaderArr(10, 0) = "Outstanding"
            HeaderArr(10, 1) = "R"
            HeaderArr(10, 2) = "12"
            HeaderArr(10, 3) = "12"

            HeaderArr(11, 0) = "Gr Cust Id"
            HeaderArr(11, 1) = "L"
            HeaderArr(11, 2) = "12"
            HeaderArr(11, 3) = "12"

            HeaderArr(12, 0) = "Guarantor Name"
            HeaderArr(12, 1) = "L"
            HeaderArr(12, 2) = "12"
            HeaderArr(12, 3) = "12"

            HeaderArr(13, 0) = "Address"
            HeaderArr(13, 1) = "L"
            HeaderArr(13, 2) = "12"
            HeaderArr(13, 3) = "12"



            M_Sql = "EXEC Get_GuarantorDetails_Sp '" + Br_code + "','" + Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(DtTo.Text).ToString("MM/dd/yyyy") + "','" & Sch_code & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & DrpType.SelectedValue & "'"
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

    '        'Dim User_Id As String = Session("UserID")
    '        Dim Sch_code As String = DdlScheme.SelectedValue
    '        Dim Section As String = Ddlsection.SelectedValue
    '        Dim SchType As String = DrpType.SelectedValue


    '        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

    '        M_Sql = "Exec Get_DpositDemandList_Sp '" + Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") + "' ,'" + Br_code + "','" & Sch_code & "','" & Session("HostID") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"



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