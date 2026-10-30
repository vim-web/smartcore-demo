Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL

Public Class BondPrintDelete
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1015
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("LoginUserID") = "" Then
                Response.Redirect("~/smartLogin.aspx")
            End If
            'USer Rights
            Dim M_Common As New CommonBL
            Obj_UserRights = M_Common.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Common = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            If Not Session("PrintFromDt") = Nothing Then
                TxtTranDt.Text = Session("PrintFromDt")
            Else
                TxtTranDt.Text = Session("TransDate")
            End If
            LoadGrid()
        End If
    End Sub

    Private Sub Dgv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Dgv.PageIndexChanging
        Dgv.PageIndex = e.NewPageIndex
        Call LoadGrid()
    End Sub

    Private Sub Dgv_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Dgv.RowCommand
        Dim Tran_No As String
        Dim TranDate As String
        Dim M_DBConn As New Connection()
        Dim SqlStr As String = ""
        If e.CommandName.ToUpper = "SELECT" Then
            Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
            If Dgv.DataKeys(RowIndex) IsNot Nothing Then
                Tran_No = Dgv.DataKeys(RowIndex)("TranNo").ToString()
                TranDate = Dgv.DataKeys(RowIndex)("Tr_date").ToString()
                Dim SqlString As String = ""
                Dim Dt As DataTable = New DataTable()
                SqlString = "Exec BondPrint_log_Sp " & CommonClass.DbDate(CDate(TranDate)) & ",'" & Tran_No & "','" & Session("UserID") & "'"
                M_DBConn.ExecuteDataTable(Dt, SqlString, False)
                ShowAlertMessage("Deleted")
            End If
            Call LoadGrid()
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Call LoadGrid()
    End Sub
    Private Sub LoadGrid()
        If CommonClass.ChkDate(TxtTranDt.Text) = False Then
            ShowAlertMessage("Invalid Date Entered")
            Exit Sub
        End If
        Dim SqlString As String = ""
        Dim Dt As DataTable = New DataTable()
        Dim M_DBConn As New Connection()
        Dim PrintType As String
        If CmbPrnType.SelectedValue = 1 Then
            PrintType = "KFS"
        ElseIf CmbPrnType.SelectedValue = 2 Then
            PrintType = "GLBOND"
        End If
        SqlString = "Exec BondPrint_view_Sp " & CommonClass.DbDate(CDate(TxtTranDt.Text)) & ", 0,'" & PrintType & "'"
        M_DBConn.ExecuteDataTable(Dt, SqlString, False)
        Dgv.DataSource = Dt
        Dgv.DataBind()
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Sub Dgv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Dgv.SelectedIndexChanged

    End Sub

    Protected Sub Close_Click(sender As Object, e As EventArgs) Handles Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class