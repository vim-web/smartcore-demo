Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class EmpList_Rpt
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Dim FormID As Integer = 1152
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            If Not IsPostBack Then

                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Cmb_Branch)
            
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub Cmd_Show_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Show.Click
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String
        M_DBConn = New Connection()
        Company_Name = HttpContext.Current.Cache("CompanyName")

        Dim HeaderArr(6, 4) As String
        Dim HeaderWidth(6, 1) As String
        HeaderArr(0, 0) = "EMP ID"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "10"
        HeaderArr(0, 3) = "10"

        HeaderArr(1, 0) = "NAME"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "40"
        HeaderArr(1, 3) = "40"

        HeaderArr(2, 0) = "DOJ"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "10"
        HeaderArr(2, 3) = "15"

        HeaderArr(3, 0) = "DESIGNATION"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "20"
        HeaderArr(3, 3) = "15"

       
        Session("RptHeader") = HeaderArr
        
        M_Sql = "EXEC EmpList_Sp'" & Cmb_Branch.SelectedValue & "','" & Session("HostID") & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)

    End Sub

    Protected Sub Cmd_Close_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Close.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class