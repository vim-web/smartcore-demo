Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class OutstandConsolidate
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
                M_dt = M_CommonBL.Get_Scheme(Ddlsection.SelectedValue)
                DdlScheme.DataTextField = "Sch_Name"
                DdlScheme.DataValueField = "Sch_Code"
                DdlScheme.DataSource = M_dt
                DdlScheme.DataBind()
                DdlScheme.Items.Add("ALL")
                ''Region Wise
                M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DtTo.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                Ddlsection_SelectedIndexChanged(Me.Ddlsection, System.EventArgs.Empty)
            End If

        Catch ex As Exception

        End Try
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
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_dt As New DataTable
            Dim M_CommonBl As New CommonBL
            Dim M_Sql As String = ""
            If Ddlsection.SelectedItem.Text = "DEPOSIT" Then
                Dim HeaderArr(11, 4) As String
                Dim HeaderWidth(11, 1) As String
                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "Acc No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Cust Name"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "22"
                HeaderArr(2, 3) = "22"

                HeaderArr(3, 0) = "Scheme"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "22"
                HeaderArr(3, 3) = "22"

                HeaderArr(4, 0) = "Dep Date"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Dep Amount"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "12"
                HeaderArr(5, 3) = "12"

                HeaderArr(6, 0) = "Due Date"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "10"

                HeaderArr(7, 0) = "Int Rate"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "5"
                HeaderArr(7, 3) = "5"

                HeaderArr(8, 0) = "Period"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "7"
                HeaderArr(8, 3) = "7"

                HeaderArr(9, 0) = "Pay Mode"
                HeaderArr(9, 1) = "L"
                HeaderArr(9, 2) = "22"
                HeaderArr(9, 3) = "22"

                HeaderArr(10, 0) = "Outstand"
                HeaderArr(10, 1) = "L"
                HeaderArr(10, 2) = "10"
                HeaderArr(10, 3) = "10"

                Session("RptHeader") = HeaderArr
               

                M_Sql = "Exec [dbo].[Get_Consolidate_Outstand_Sp] '" & DdlBranch.SelectedValue & "','" & Ddlsection.SelectedItem.Text & "','" & DrpType.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Convert.ToDateTime(DtTo.Text).ToString("MM/dd/yyyy") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
                Exit Sub


            ElseIf Ddlsection.SelectedItem.Text = "LOAN" Then

                Dim HeaderArr(10, 4) As String
                Dim HeaderWidth(10, 1) As String
                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "Acc No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Cust Name"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "22"
                HeaderArr(2, 3) = "22"

                HeaderArr(3, 0) = "Scheme"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "22"
                HeaderArr(3, 3) = "22"

                HeaderArr(4, 0) = "Open Date"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Loan Amount"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "12"
                HeaderArr(5, 3) = "12"

                HeaderArr(6, 0) = "Due Date"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "10"

                HeaderArr(7, 0) = "Int Rate"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "5"
                HeaderArr(7, 3) = "5"

                HeaderArr(8, 0) = "Period"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "7"
                HeaderArr(8, 3) = "7"

       

                HeaderArr(9, 0) = "Outstand"
                HeaderArr(9, 1) = "L"
                HeaderArr(9, 2) = "10"
                HeaderArr(9, 3) = "10"

                Session("RptHeader") = HeaderArr


                M_Sql = "Exec [dbo].[Get_Consolidate_Outstand_Sp] '" & DdlBranch.SelectedValue & "','" & Ddlsection.SelectedItem.Text & "','" & DrpType.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Convert.ToDateTime(DtTo.Text).ToString("MM/dd/yyyy") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
                Exit Sub
            ElseIf Ddlsection.SelectedItem.Text = "SHARE" Then


                Dim HeaderArr(8, 4) As String
                Dim HeaderWidth(8, 1) As String
                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "Acc No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Cust Name"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "22"
                HeaderArr(2, 3) = "22"

                HeaderArr(3, 0) = "Scheme"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "22"
                HeaderArr(3, 3) = "22"

                HeaderArr(4, 0) = "Open Date"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Outstand"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "12"
                HeaderArr(5, 3) = "12"

                HeaderArr(6, 0) = "ID Details"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "15"
                HeaderArr(6, 3) = "15"

                HeaderArr(7, 0) = "Address"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "22"
                HeaderArr(7, 3) = "22"
               

                Session("RptHeader") = HeaderArr

                M_Sql = "Exec [dbo].[Get_Consolidate_Outstand_Sp] '" & DdlBranch.SelectedValue & "','" & Ddlsection.SelectedItem.Text & "','" & DrpType.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Convert.ToDateTime(DtTo.Text).ToString("MM/dd/yyyy") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.Process_Consolidate_Outstand(DdlBranch.SelectedValue, Ddlsection.SelectedItem.Text, DrpType.SelectedValue, DdlScheme.SelectedValue, DtTo.Text, Session("HostID"), Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue)

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

    Protected Sub DrpType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpType.SelectedIndexChanged
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