Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports System.Drawing
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Public Class GoldOrnamentsAppraiserDtlsEntry
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

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImageButton2_Click1(sender As Object, e As ImageClickEventArgs) Handles BtnShow.Click
        LoadGrid()
    End Sub

    Protected Sub BtnClose_Click(sender As Object, e As ImageClickEventArgs) Handles BtnClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Private Sub GvDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvDetails.RowCommand
        If e.CommandName = "Save" Then
            Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
            Dim AccID As Integer = GvDetails.DataKeys(RowIndex)("Acc_ID").ToString()
            Dim Slno As Integer = GvDetails.DataKeys(RowIndex)("slno").ToString()
            Dim AppGw As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("TxtAppGw"), TextBox).Text)
            Dim AppNw As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("TxtAppNw"), TextBox).Text)
            Dim Stone As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("TxtStone"), TextBox).Text)
            Dim Excess As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("TxtExcess"), TextBox).Text)
            Dim N916H As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("Txt916H"), TextBox).Text)
            Dim N916 As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("Txt916"), TextBox).Text)
            Dim N22ct As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("Txt22ct"), TextBox).Text)
            Dim N20ct As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("Txt20ct"), TextBox).Text)
            Dim N18ct As Double = Val(DirectCast(GvDetails.Rows(RowIndex).FindControl("Txt18ct"), TextBox).Text)
            Dim Remarks As String = DirectCast(GvDetails.Rows(RowIndex).FindControl("TxtRemarks"), TextBox).Text
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.InsertGoldOrnamentsAppraiserDet(AccID, AppGw, AppNw, Stone, Excess, N916H, N916, N22ct, N20ct, N18ct, Remarks, Session("LoginID"), Session("TransDate"), Slno)
            If M_Dt.Rows.Count > 0 Then
                Dim Msg As String = M_Dt.Rows(0)("Msg").ToString()
                ShowAlertMessage(Msg)
            Else
                ShowAlertMessage("FAILED")
            End If
            LoadGrid()
        End If
    End Sub
    Private Sub LoadGrid()
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.GetGoldOrnamentAppriserDet(Ddl_Branch.SelectedValue, Txt_FrDate.Text, Txt_ToDate.Text)
            If M_Dt.Rows.Count > 0 Then
                GvDetails.DataSource = M_Dt
                GvDetails.DataBind()
            End If
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

    Private Sub Ddl_Branch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged
        GvDetails.DataSource = Nothing
        GvDetails.DataBind()
    End Sub

    Private Sub Txt_FrDate_TextChanged(sender As Object, e As EventArgs) Handles Txt_FrDate.TextChanged
        GvDetails.DataSource = Nothing
        GvDetails.DataBind()
    End Sub

    Private Sub Txt_ToDate_TextChanged(sender As Object, e As EventArgs) Handles Txt_ToDate.TextChanged
        GvDetails.DataSource = Nothing
        GvDetails.DataBind()
    End Sub
End Class