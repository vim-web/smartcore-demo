Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports System.Drawing
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL

Public Class GoldOrnamentsAppraiserDtlsReport
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1006
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                Dim M_CommonBl As New CommonBL
                Obj_UserRights = M_CommonBl.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                M_CommonBl = New CommonBL
                Dim M_Dt As New DataTable
                M_CommonBl.Fill_Branch(Ddl_Branch)
                Ddl_Branch.SelectedValue = Session("DefaultBrCode")
                Txt_FrDate.Text = Session("TransDate")
                Txt_ToDate.Text = Session("TransDate")
                M_CommonBl.Fill_UserWiseRegion(DdlRegion, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBl.Fill_UserWiseArea(DdlArea, Session("UserID"), DdlRegion.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBl.Fill_UserWiseBranch(Ddl_Branch, Session("UserID"), DdlRegion.SelectedValue, DdlArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                Ddl_Branch.Items.Remove(Ddl_Branch.Items.FindByText("ALL"))
                Ddl_Branch.Items.Add("ALL")
                Ddl_Branch.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImageButton2_Click1(sender As Object, e As ImageClickEventArgs) Handles BtnShow.Click
        'Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        If Not IsDate(Txt_FrDate.Text) Then
            ShowAlertMessage("Enter Valid From Date")
            Exit Sub
        End If

        If Not IsDate(Txt_ToDate.Text) Then
            ShowAlertMessage("Enter Valid To Date")
            Exit Sub
        End If

        Dim M_Qry As String = ""
        Dim HeaderArr(25, 4) As String
        Dim HeaderWidth(25, 1) As String

        HeaderArr(0, 0) = "Slno"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "4"
        HeaderArr(0, 3) = "4"

        HeaderArr(1, 0) = "Branch"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "13"
        HeaderArr(1, 3) = "13"

        HeaderArr(2, 0) = "Account No"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "13"
        HeaderArr(2, 3) = "13"

        HeaderArr(3, 0) = "OpenDate"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "10"
        HeaderArr(3, 3) = "10"

        HeaderArr(4, 0) = "EndDate"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "10"
        HeaderArr(4, 3) = "10"

        HeaderArr(5, 0) = "System Gweight"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "10"

        HeaderArr(6, 0) = "Stone"
        HeaderArr(6, 1) = "R"
        HeaderArr(6, 2) = "8"
        HeaderArr(6, 3) = "8"

        HeaderArr(7, 0) = "System Netweight"
        HeaderArr(7, 1) = "L"
        HeaderArr(7, 2) = "10"
        HeaderArr(7, 3) = "10"


        HeaderArr(8, 0) = "Appraiser Actual Wt."
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "8"
        HeaderArr(8, 3) = "8"

        HeaderArr(9, 0) = "Net Wt."
        HeaderArr(9, 1) = "L"
        HeaderArr(9, 2) = "8"
        HeaderArr(9, 3) = "8"

        HeaderArr(10, 0) = "Excess"
        HeaderArr(10, 1) = "L"
        HeaderArr(10, 2) = "8"
        HeaderArr(10, 3) = "8"


        HeaderArr(11, 0) = "916H"
        HeaderArr(11, 1) = "R"
        HeaderArr(11, 2) = "8"
        HeaderArr(11, 3) = "8"

        HeaderArr(12, 0) = "916"
        HeaderArr(12, 1) = "R"
        HeaderArr(12, 2) = "12"
        HeaderArr(12, 3) = "12"

        HeaderArr(13, 0) = "22CT"
        HeaderArr(13, 1) = "R"
        HeaderArr(13, 2) = "12"
        HeaderArr(13, 3) = "12"

        HeaderArr(14, 0) = "20CT"
        HeaderArr(14, 1) = "R"
        HeaderArr(14, 2) = "12"
        HeaderArr(14, 3) = "12"

        HeaderArr(15, 0) = "18CT"
        HeaderArr(15, 1) = "R"
        HeaderArr(15, 2) = "12"
        HeaderArr(15, 3) = "12"

        HeaderArr(16, 0) = "Packet No"
        HeaderArr(16, 1) = "R"
        HeaderArr(16, 2) = "12"
        HeaderArr(16, 3) = "12"

        HeaderArr(17, 0) = "Loan Liability"
        HeaderArr(17, 1) = "R"
        HeaderArr(17, 2) = "12"
        HeaderArr(17, 3) = "12"

        HeaderArr(18, 0) = "Market Status"
        HeaderArr(18, 1) = "R"
        HeaderArr(18, 2) = "12"
        HeaderArr(18, 3) = "12"

        HeaderArr(19, 0) = "Profit"
        HeaderArr(19, 1) = "R"
        HeaderArr(19, 2) = "12"
        HeaderArr(19, 3) = "12"

        HeaderArr(20, 0) = "Loss"
        HeaderArr(20, 1) = "R"
        HeaderArr(20, 2) = "12"
        HeaderArr(20, 3) = "12"

        HeaderArr(21, 0) = "Remarks"
        HeaderArr(21, 1) = "R"
        HeaderArr(21, 2) = "12"
        HeaderArr(21, 3) = "12"

        HeaderArr(22, 0) = "AppName"
        HeaderArr(22, 1) = "R"
        HeaderArr(22, 2) = "12"
        HeaderArr(22, 3) = "12"

        Session("RptHeader") = HeaderArr
        M_Qry = "Exec Gold_Ornament_AppriseReport_Grid '" & DdlRegion.SelectedValue & "','" & DdlArea.SelectedValue & "','" & Ddl_Branch.SelectedValue & "','" & Format(CDate(Txt_FrDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(Txt_ToDate.Text), "MM/dd/yyyy") & "'," & Val(TxtTdayGrate.Text) & ",'" & Session("UserID") & "'"
        Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub BtnClose_Click(sender As Object, e As ImageClickEventArgs) Handles BtnClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub DdlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlRegion.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DdlArea, Session("UserID"), DdlRegion.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DdlArea_SelectedIndexChanged(Me.DdlArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DdlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(Ddl_Branch, Session("UserID"), DdlRegion.SelectedValue, DdlArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Ddl_Branch.Items.Remove(Ddl_Branch.Items.FindByText("ALL"))
            Ddl_Branch.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        'Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        If Not IsDate(Txt_FrDate.Text) Then
            ShowAlertMessage("Enter Valid From Date")
            Exit Sub
        End If

        If Not IsDate(Txt_ToDate.Text) Then
            ShowAlertMessage("Enter Valid To Date")
            Exit Sub
        End If

        Dim M_Qry As String = ""
        Dim M_Sql As String
        Dim BrCode As String = ""
        Dim Cust_ID As String = ""
        Dim M_DBConn As New Connection
        Dim M_Ds As New DataSet()

        M_Sql = "Exec Gold_Ornament_AppriseReport_Summery '" & DdlRegion.SelectedValue & "','" & DdlArea.SelectedValue & "','" & Ddl_Branch.SelectedValue & "','" & Format(CDate(Txt_FrDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(Txt_ToDate.Text), "MM/dd/yyyy") & "'," & Val(TxtTdayGrate.Text) & ",'" & Session("UserID") & "'"
        M_DBConn.ExecuteDataSet(M_Ds, M_Sql, False)
        Dim CustName As String
        Dim CentreName As String
        Dim MailingAddress As String
        Dim MblNO As String
        Dim BranchName As String
        Dim Product As String
        Dim LoanAmt As String
        Dim LoanDate As String
        Dim Tenure As String
        Dim DueDate As String
        Dim RepayMode As String
        Dim Status As String
        'Dim M_Dt As DataTable = M_Ds.Tables(1)
        'Dim M_DtChart As DataTable = M_Ds.Tables(0)


        'StrSlf = "CustName|" & CustName & "|CentreName|" & CentreName
        'StrSlf = StrSlf & "|MailingAddress|" & MailingAddress & "|MblNO|" & MblNO
        'StrSlf = StrSlf & "|BranchName|" & BranchName & "|Product|" & Product
        'StrSlf = StrSlf & "|LoanAmt|" & LoanAmt & "|LoanDate|" & LoanDate
        'StrSlf = StrSlf & "|Tenure|" & Tenure & "|DueDate|" & DueDate
        'StrSlf = StrSlf & "|RepayMode|" & RepayMode & "|Status|" & Status
        'StrSlf = StrSlf & "|CustID|" & Cust_ID & "|UserID|" & Session("UserID")
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptQry", M_Sql)
        Session.Add("RptPath", Server.MapPath("~/REPORTS/Loans/GlAppraiser.rpt"))

        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        If Not IsDate(Txt_FrDate.Text) Then
            ShowAlertMessage("Enter Valid From Date")
            Exit Sub
        End If

        If Not IsDate(Txt_ToDate.Text) Then
            ShowAlertMessage("Enter Valid To Date")
            Exit Sub
        End If

        Dim M_Qry As String = ""
        Dim HeaderArr(23, 4) As String
        Dim HeaderWidth(20, 1) As String

                HeaderArr(0, 0) = "Slno"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "4"
        HeaderArr(0, 3) = "4"

        HeaderArr(1, 0) = "Branch"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "13"
        HeaderArr(1, 3) = "13"

        HeaderArr(2, 0) = "Account No"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "13"
        HeaderArr(2, 3) = "13"

        HeaderArr(3, 0) = "OpenDate"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "10"
        HeaderArr(3, 3) = "10"

        HeaderArr(4, 0) = "EndDate"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "10"
        HeaderArr(4, 3) = "10"

        HeaderArr(5, 0) = "System Gweight"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "10"

        HeaderArr(6, 0) = "Stone"
        HeaderArr(6, 1) = "R"
        HeaderArr(6, 2) = "8"
        HeaderArr(6, 3) = "8"

        HeaderArr(7, 0) = "System Netweight"
        HeaderArr(7, 1) = "L"
        HeaderArr(7, 2) = "10"
        HeaderArr(7, 3) = "10"


        HeaderArr(8, 0) = "Appraiser Actual Wt."
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "8"
        HeaderArr(8, 3) = "8"

        HeaderArr(9, 0) = "Net Wt."
        HeaderArr(9, 1) = "L"
        HeaderArr(9, 2) = "8"
        HeaderArr(9, 3) = "8"

        HeaderArr(10, 0) = "Excess"
        HeaderArr(10, 1) = "L"
        HeaderArr(10, 2) = "8"
        HeaderArr(10, 3) = "8"


        HeaderArr(11, 0) = "916H"
        HeaderArr(11, 1) = "R"
        HeaderArr(11, 2) = "8"
        HeaderArr(11, 3) = "8"

        HeaderArr(12, 0) = "916"
        HeaderArr(12, 1) = "R"
        HeaderArr(12, 2) = "12"
        HeaderArr(12, 3) = "12"

        HeaderArr(13, 0) = "22CT"
        HeaderArr(13, 1) = "R"
        HeaderArr(13, 2) = "12"
        HeaderArr(13, 3) = "12"

        HeaderArr(14, 0) = "20CT"
        HeaderArr(14, 1) = "R"
        HeaderArr(14, 2) = "12"
        HeaderArr(14, 3) = "12"

        HeaderArr(15, 0) = "18CT"
        HeaderArr(15, 1) = "R"
        HeaderArr(15, 2) = "12"
        HeaderArr(15, 3) = "12"

        HeaderArr(16, 0) = "Packet No"
        HeaderArr(16, 1) = "R"
        HeaderArr(16, 2) = "12"
        HeaderArr(16, 3) = "12"

        HeaderArr(17, 0) = "Loan Liability"
        HeaderArr(17, 1) = "R"
        HeaderArr(17, 2) = "12"
        HeaderArr(17, 3) = "12"

        HeaderArr(18, 0) = "Market Status"
        HeaderArr(18, 1) = "R"
        HeaderArr(18, 2) = "12"
        HeaderArr(18, 3) = "12"

        HeaderArr(19, 0) = "Profit"
        HeaderArr(19, 1) = "R"
        HeaderArr(19, 2) = "12"
        HeaderArr(19, 3) = "12"

        HeaderArr(20, 0) = "Loss"
        HeaderArr(20, 1) = "R"
        HeaderArr(20, 2) = "12"
        HeaderArr(20, 3) = "12"

        HeaderArr(21, 0) = "Remarks"
        HeaderArr(21, 1) = "R"
        HeaderArr(21, 2) = "12"
        HeaderArr(21, 3) = "12"

        HeaderArr(22, 0) = "AppName"
        HeaderArr(22, 1) = "R"
        HeaderArr(22, 2) = "12"
        HeaderArr(22, 3) = "12"

        Session("RptHeader") = HeaderArr
        M_Qry = "Exec Gold_Ornament_AppriseReport_Risk '" & DdlRegion.SelectedValue & "','" & DdlArea.SelectedValue & "','" & Ddl_Branch.SelectedValue & "','" & Format(CDate(Txt_FrDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(Txt_ToDate.Text), "MM/dd/yyyy") & "'," & Val(TxtTdayGrate.Text) & ",'" & Session("UserID") & "'"
        Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub
End Class