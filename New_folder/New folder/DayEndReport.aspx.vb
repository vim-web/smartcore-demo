Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class DayEndReport

    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then


                ''USer Rights
                'Dim M_Comm As New CommonBL
                'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                'M_Comm = Nothing
                'If Obj_UserRights.View_Right = False Then
                '    Response.Redirect("~/UnauthorisedAcces.ASPX")
                'End If



                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<--ALL-->")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                DtFrom.Text = Session("TransDate")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()


        Dim HeaderArr(4, 4) As String
        Dim HeaderWidth(4, 1) As String

        HeaderArr(0, 0) = "SLNO"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "5"
        HeaderArr(0, 3) = "5"

        HeaderArr(1, 0) = "NARRATION"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "50"
        HeaderArr(1, 3) = "50"

        HeaderArr(2, 0) = "VALUE"
        HeaderArr(2, 1) = "R"
        HeaderArr(2, 2) = "15"
        HeaderArr(2, 3) = "15"



        Session("RptHeader") = HeaderArr

        M_Sql = "Exec DayEndRpt_1_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & DdlBranch.SelectedValue & "'  "

        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

End Class