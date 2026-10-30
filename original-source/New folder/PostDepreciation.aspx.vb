Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class PostDepreciation
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Shared Obj_UserRights As Object
    Dim FormID As Integer = 2223
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL
                Dim M_Dt As New DataTable
                Dim FromDt As Date
                M_CommonBL.Fill_Branch(DdlBranch, False)
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                DdlBranch.Items.Add("ALL")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                M_Dt = M_CommonBL.Fill_GenSettings(Session("DefaultBrCode"))
                FromDt = M_Dt.Rows(0).Item("AssetPostDt")
                DtFrom.Text = DateAdd(DateInterval.Day, 1, FromDt)
                'DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
                DtFrom.Enabled = False

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click

        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable

            M_Dt = M_CommonBl.Post_Depreciation(DdlBranch.SelectedValue, DtFrom.Text, DtTo.Text, Session("UserID"))
            If M_Dt.Rows.Count > 0 Then
                ShowAlertMessage(M_Dt.Rows(0).Item("Status").ToString())
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try

            Dim FromDate As Date
            Dim ToDate As Date
            FromDate = DtFrom.Text
            ToDate = DtTo.Text

            If FromDate > ToDate Then
                ShowAlertMessage("Please enter From date less than To date")
                Exit Sub
            End If


            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()

            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String


            HeaderArr(0, 0) = "SL NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "10"

            HeaderArr(1, 0) = "BRANCH"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "35"
            HeaderArr(1, 3) = "35"

            HeaderArr(2, 0) = "ASSET OPEN DATE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "35"
            HeaderArr(2, 3) = "35"

            HeaderArr(3, 0) = "DAYS"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "ASSET CODE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "15"
            HeaderArr(4, 3) = "15"

            HeaderArr(5, 0) = "ASSET NAME"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "35"
            HeaderArr(5, 3) = "35"

            HeaderArr(6, 0) = "PERCENTAGE"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "15"
            HeaderArr(6, 3) = "15"

            HeaderArr(7, 0) = "ASSET VALUE"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "15"
            HeaderArr(7, 3) = "15"

            HeaderArr(8, 0) = "DEPRECIATION AMOUNT"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "15"
            HeaderArr(8, 3) = "15"


            Dim DFr As Date
            DFr = DtFrom.Text

            Dim DTo As Date
            DTo = DtTo.Text

            Session("RptHeader") = HeaderArr




            M_Sql = "Exec AssetDep_Calc_sp '" & DdlBranch.SelectedValue & "','" & Format(DFr, "MM/dd/yyyy") & "','" & Format(DTo, "MM/dd/yyyy") & "'"

            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ChkPost_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkPost.CheckedChanged
        If ChkPost.Checked = True Then
            BtnPro.Visible = True
            CmdShow.Visible = False
        Else
            BtnPro.Visible = False
            CmdShow.Visible = True
        End If
    End Sub
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class