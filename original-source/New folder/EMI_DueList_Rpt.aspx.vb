Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class EMI_DueList_Rpt
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
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")
                DdlScheme.Items.Add("ALL")
                DtFrom.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
            End If

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

            Dim Br_code As String = IIf(DdlBranch.SelectedValue = "ALL", "0", DdlBranch.SelectedValue)
            Dim From_Date As String = DtFrom.Text
            Dim Sch_code As String = IIf(DdlScheme.SelectedValue = "ALL", "0", DdlScheme.SelectedValue)



            Dim HeaderArr(11, 4) As String
            Dim HeaderWidth(11, 1) As String
            HeaderArr(0, 0) = "Cust Name"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "22"
            HeaderArr(0, 3) = "22"

            HeaderArr(1, 0) = "Acc_No"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "Loan Date"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "Scheme"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "22"
            HeaderArr(3, 3) = "22"

            HeaderArr(4, 0) = "Period"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "8"
            HeaderArr(4, 3) = "3"

            HeaderArr(5, 0) = "Due Date"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "15"

            HeaderArr(6, 0) = "EMI DefNo"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "3"

            HeaderArr(7, 0) = "Def Amount"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "8"

            HeaderArr(8, 0) = "Suspense"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "8"

            HeaderArr(9, 0) = "Last InstDt"
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "12"
            HeaderArr(9, 3) = "15"

            HeaderArr(10, 0) = "Last CollectDt"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "15"
            HeaderArr(10, 3) = "15"

            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC [Get_EmiDueList_Sp] '" & Br_code & "','" & Sch_code & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & DrpPeriodType.SelectedValue & "'"
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