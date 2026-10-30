Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Public Class LoanOdrpt
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 1037
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<-- ALL -->")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                Txt_Fromdate.Text = Session("TransDate")
                Txt_Todate.Text = Session("TransDate")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click
        ValidateControls()
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        ValidateControls()
        Dim M_Qry As String = ""

        Dim HeaderArr(9, 4) As String
        Dim HeaderWidth(9, 1) As String

        HeaderArr(0, 0) = "SLNO"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "4"
        HeaderArr(0, 3) = "5"

        HeaderArr(1, 0) = "Particulars"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "15"
        HeaderArr(1, 3) = "15"


        HeaderArr(2, 0) = "OD Begining_No"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "10"
        HeaderArr(2, 3) = "5"

        HeaderArr(3, 0) = "OD Begining_Amount"
        HeaderArr(3, 1) = "R"
        HeaderArr(3, 2) = "15"
        HeaderArr(3, 3) = "10"

        HeaderArr(4, 0) = "During Month_No"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "15"
        HeaderArr(4, 3) = "5"

        HeaderArr(5, 0) = "During Month_Amount"
        HeaderArr(5, 1) = "R"
        HeaderArr(5, 2) = "15"
        HeaderArr(5, 3) = "10"

        HeaderArr(6, 0) = "Repaid During Month_No"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "15"
        HeaderArr(6, 3) = "5"

        HeaderArr(7, 0) = "Repaid During Month_Amount"
        HeaderArr(7, 1) = "R"
        HeaderArr(7, 2) = "10"
        HeaderArr(7, 3) = "10"

        HeaderArr(8, 0) = "Od At End_No"
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "10"
        HeaderArr(8, 3) = "10"

        HeaderArr(9, 0) = "Od At End_Amount"
        HeaderArr(9, 1) = "R"
        HeaderArr(9, 2) = "10"
        HeaderArr(9, 3) = "10"

        Session("RptHeader") = HeaderArr

        M_Qry = "EXEC LoanOd_RPT  '" & DdlBranch.SelectedValue & "'," & ClsComm.DbDate(Txt_Fromdate.Text) & "," & ClsComm.DbDate(Txt_Todate.Text) & ""
        Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub
    Private Sub ValidateControls()
        If Not IsDate(Txt_Fromdate.Text) Then
            ShowAlertMessage("Enter Valid From Date")
            Exit Sub
        End If

        If Not IsDate(Txt_Todate.Text) Then
            ShowAlertMessage("Enter Valid To Date")
            Exit Sub
        End If
    End Sub
End Class