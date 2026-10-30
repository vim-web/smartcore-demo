Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class EMI_BktReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
                Dim LoanType As String = ""
                DdlScheme.Items.Add("ALL")
                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("ALL")
                DtTo.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                M_CommonBL.Fill_Pickup(DrpType, 102, 0)
                DrpType.Items.Add("ALL")
                LoanType = DrpType.SelectedValue.ToString()

                M_dt = M_CommonBL.Get_Scheme("LOAN", LoanType)
                DdlScheme.DataTextField = "Sch_Name"
                DdlScheme.DataValueField = "Sch_Code"
                DdlScheme.DataSource = M_dt
                DdlScheme.DataBind()
                DdlScheme.Items.Add("ALL")

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Try
            Dim M_Dt As New DataTable
            Dim M_Sql As String = ""
            Dim Br_code As String = IIf(DdlBranch.SelectedValue = "ALL", "0", DdlBranch.SelectedValue)
            'Dim To_Date As String = DtTo.Text
            Dim User_Id As String = Session("UserID")
            Dim Sch_code As String = IIf(DdlScheme.SelectedValue = "ALL", "0", DdlScheme.SelectedValue)
            Dim LoanType As String = IIf(DrpType.SelectedValue = "ALL", "0", DrpType.SelectedValue)


            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

            M_Sql = "Exec Process_EmiBucketList_Sp '" + Br_code + "','" + LoanType + "','" + Sch_code + "','" & Convert.ToDateTime(DtTo.Text).ToString("MM/dd/yyyy") & "','" & Session("HostID") & "'"


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
    Protected Sub DrpType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpType.SelectedIndexChanged
        Try
            Dim M_dt As New DataTable
            Dim M_CommonBL As New CommonBL
            Dim LoanType As String = ""
            LoanType = DrpType.SelectedValue.ToString()
            M_dt = M_CommonBL.Get_Scheme("LOAN", LoanType)
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
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim clscomm As New CommonClass

            Dim Br_code As String = IIf(DdlBranch.SelectedValue = "ALL", "0", DdlBranch.SelectedValue)
            Dim To_Date As String = DtTo.Text
            Dim User_Id As String = Session("UserID")
            Dim Sch_code As String = IIf(DdlScheme.SelectedValue = "ALL", "0", DdlScheme.SelectedValue)
            Dim LoanType As String = IIf(DrpType.SelectedValue = "ALL", "0", DrpType.SelectedValue)


            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()



            Dim HeaderArr(17, 4) As String
            Dim HeaderWidth(17, 1) As String
            HeaderArr(0, 0) = "Branch"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "10"

            HeaderArr(1, 0) = "CustID"
            HeaderArr(1, 1) = "R"
            HeaderArr(1, 2) = "5"
            HeaderArr(1, 3) = "5"

            HeaderArr(2, 0) = "Cust Name"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "22"
            HeaderArr(2, 3) = "22"

            HeaderArr(3, 0) = "Scheme"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "22"
            HeaderArr(3, 3) = "22"

            HeaderArr(4, 0) = "Acc No"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "13"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "Period"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "5"
            HeaderArr(5, 3) = "5"

            HeaderArr(6, 0) = "Last TrDate"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"

            HeaderArr(7, 0) = "OD Date"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "10"

            HeaderArr(8, 0) = "EMI"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "10"

            HeaderArr(9, 0) = "1st Bkt"
            HeaderArr(9, 1) = "R"
            HeaderArr(9, 2) = "10"
            HeaderArr(9, 3) = "10"

            HeaderArr(10, 0) = "2nd Bkt"
            HeaderArr(10, 1) = "R"
            HeaderArr(10, 2) = "10"
            HeaderArr(10, 3) = "10"

            HeaderArr(11, 0) = "3rd Bkt"
            HeaderArr(11, 1) = "R"
            HeaderArr(11, 2) = "10"
            HeaderArr(11, 3) = "10"

            HeaderArr(12, 0) = "4th Bkt"
            HeaderArr(12, 1) = "R"
            HeaderArr(12, 2) = "10"
            HeaderArr(12, 3) = "10"

            HeaderArr(13, 0) = "5th Bkt"
            HeaderArr(13, 1) = "R"
            HeaderArr(13, 2) = "10"
            HeaderArr(13, 3) = "10"

            HeaderArr(14, 0) = "6&Abv Bkt"
            HeaderArr(14, 1) = "R"
            HeaderArr(14, 2) = "10"
            HeaderArr(14, 3) = "10"

            HeaderArr(15, 0) = "OD Int"
            HeaderArr(15, 1) = "R"
            HeaderArr(15, 2) = "10"
            HeaderArr(15, 3) = "10"

            HeaderArr(16, 0) = "Total"
            HeaderArr(16, 1) = "R"
            HeaderArr(16, 2) = "10"
            HeaderArr(16, 3) = "10"


            Session("RptHeader") = HeaderArr
            
            M_Sql = "EXEC [Get_EmiBucketList_Sp] '" & DdlBranch.SelectedValue & "','" & LoanType & "','" & Sch_code & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("HostID") & "' "
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
          
        Catch ex As Exception

        End Try
    End Sub
End Class