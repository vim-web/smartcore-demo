Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class TicketMaster_Report
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1093
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
                DtFrom.Text = DateTime.Now.Date
                DtTo.Text = DateTime.Now.Date

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click

        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()
            Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

            If Not IsDate(DtFrom.Text) Then
                ShowAlertMessage("Enter Valid From Date")
                Exit Sub
            End If

            If Not IsDate(DtTo.Text) Then
                ShowAlertMessage("Enter Valid To Date")
                Exit Sub
            End If

            'Dim Br_Code As String


            'Br_Code = DdlBranch.SelectedValue

            M_Sql = Rpt_Qry '.ToUpper()
            ' Formula From Sql Query Output 

            Dim M_Qry As String = ""

            Dim HeaderArr(5, 6) As String
            Dim HeaderWidth(3, 1) As String
            'HeaderArr(0, 0) = "SLN0"
            'HeaderArr(0, 1) = "L"
            'HeaderArr(0, 2) = "4"
            'HeaderArr(0, 3) = "4"


            'HeaderArr(1, 0) = "CUST_ID"
            'HeaderArr(1, 1) = "L"
            'HeaderArr(1, 2) = "4"
            'HeaderArr(1, 3) = "4"

            'HeaderArr(2, 0) = "DATE"
            'HeaderArr(2, 1) = "L"
            'HeaderArr(2, 2) = "13"
            'HeaderArr(2, 3) = "13"



            'HeaderArr(3, 0) = "NAME"
            'HeaderArr(3, 1) = "L"
            'HeaderArr(3, 2) = "15"
            'HeaderArr(3, 3) = "15"


            'HeaderArr(4, 0) = "ADDRESS"
            'HeaderArr(4, 1) = "L"
            'HeaderArr(4, 2) = "15"
            'HeaderArr(4, 3) = "15"

            'HeaderArr(5, 0) = "MOBILE NUMBER"
            'HeaderArr(5, 1) = "L"
            'HeaderArr(5, 2) = "15"
            'HeaderArr(5, 3) = "15"





            Session("RptHeader") = HeaderArr

            M_Qry = "EXEC [TicketMaster_rpt]   '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Qry
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

            Exit Sub

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Img_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Img_Close.Click
        Response.Redirect("~/main.aspx")
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

End Class