Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data
Public Class GeneralRemainder
    Inherits System.Web.UI.Page
    Shared GmTable As DataTable
    Dim FormID As Integer = 1061
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TxtRemDate.Attributes.Add("readonly", "readonly")
        If Not IsPostBack Then

            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            Set_Grid()
        End If

    End Sub
    Private Function Validate_Control() As Boolean

    End Function
    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        'If validate_Control = False Then Exit Sub
        Dim M_Remainder As New SMARTCORE_BO.RemainderBO
        With M_Remainder
            .ReminderId = 0
            .ReminderToID = 0
            .RegisterDate = Session("TransDate")
            .RemainderDate = TxtRemDate.Text
            .AcknowledgedDatetime = "1/1/1900"
            .ReminderTo = ""
            .RemainderTitle = ""
            .RemainderContent = TxtRemainder.Text
            .AssignedBy = TxtRemPostedBy.Text
            .Priority = "H"
            .AcknoledgementRequired = "Y"
            .Acknowledged = "N"
            .Status = "O"
        End With


        Dim M_RemainderBL As New RemainderBL
        Dim M_StID As Integer
        Session("M_EntryMode") = "NEW"
        M_StID = M_RemainderBL.Save_Data(M_Remainder, Session("M_EntryMode"))
        If (M_StID > 0) Then
            ShowAlertMessage("Remainder Successfully Saved")
            Clear_Controls()
        Else
            ShowAlertMessage(" SAVING FAILED")
        End If

    End Sub


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Private Sub DgvRemainder_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvRemainder.PageIndexChanging
        Dim M_Dt As DataTable = DirectCast(Session("RemainderTbl"), DataTable)
        DgvRemainder.PageIndex = e.NewPageIndex
        DgvRemainder.DataSource = M_Dt
        DgvRemainder.DataBind()
    End Sub

    Private Sub DgvRemainder_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvRemainder.RowDeleting
        Try            
            Dim M_RemainderBl As New RemainderBL
            M_RemainderBl.Update_RemainderStatus(CInt(DgvRemainder.DataKeys(e.RowIndex).Value))
            Set_Grid()
        Catch ex As Exception

        End Try
    End Sub
    
    Private Sub Set_Grid()

        Dim M_RemainderBl As New RemainderBL
        Session("RemainderTbl") = M_RemainderBl.Set_GenRemainderGrid
        Dim M_Dt As DataTable = DirectCast(Session("RemainderTbl"), DataTable)
        DgvRemainder.DataSource = M_Dt
        DgvRemainder.DataBind()

    End Sub
    Private Sub Clear_Controls()
        Set_Grid()
        TxtRemainder.Text = ""
        TxtRemDate.Text = ""
        TxtRemPostedBy.Text = ""
    End Sub


    Protected Sub TxtRemainder_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtRemainder.TextChanged
        
    End Sub

    Protected Sub DgvRemainder_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvRemainder.SelectedIndexChanged

    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

End Class