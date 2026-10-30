Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Partial Public Class AccountGroupMaster
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1134
    Shared Obj_UserRights As Object
    Shared AcHeadDt As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Session("M_EntryMode") = "NEW"
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            TxtAccGroupCode.Text = ""
            TxtAccGrpName.Text = ""
            TxtRemarks.Text = ""
            TxtSeqNo.Text = ""
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

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_AccountGroupMaster As New SMARTCORE_BO.AccountGroupMasterBO

            With M_AccountGroupMaster


                .GrpCode = TxtAccGroupCode.Text
                .GrpSeqNo = TxtSeqNo.Text
                .GrpParentId = TxtAccGroupCode.Text
                .GrpName = TxtAccGrpName.Text
                .MakerID = Session("LoginUserID").ToString()
                .CheckerID = Session("LoginUserID").ToString()

                .Remarks = TxtRemarks.Text

            End With

            Try
                Dim M_GrpCode As String
                Dim M_AccountGroupMasterBL As New AccountGroupMasterBL
                M_GrpCode = M_AccountGroupMasterBL.Save_Data(M_AccountGroupMaster, Session("M_EntryMode"))
                If M_GrpCode > 0 Then
                    If Session("M_EntryMode") = "NEW" Then
                        ShowAlertMessage("Saved Successfully")
                    Else
                        ShowAlertMessage("Updated Successfully")
                    End If

                    Clear()
                Else
                    ShowAlertMessage("Not Saved Successfully")

                End If
                M_AccountGroupMasterBL = Nothing
            Catch ex As Exception

            End Try

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAccGroupCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccGroupCode.TextChanged
        Try
            Dim M_AccountGroupMaster As New SMARTCORE_BO.AccountGroupMasterBO
            Dim M_AccountGroupMasterBL As New AccountGroupMasterBL
            M_AccountGroupMaster = M_AccountGroupMasterBL.Locate_Data(TxtAccGroupCode.Text)
            If M_AccountGroupMaster.GrpCode = 0 Then
                Dim str As String
                str = TxtAccGroupCode.Text
                Clear()
                TxtAccGroupCode.Text = str
                TxtAccGroupCode.Focus()
                Session("M_EntryMode") = "NEW"
                Exit Sub
            End If
            Session("M_EntryMode") = "UPDATE"
            With M_AccountGroupMaster
                TxtAccGroupCode.Text = .GrpCode
                TxtSeqNo.Text = .GrpSeqNo
                TxtAccGrpName.Text = .GrpName
                TxtRemarks.Text = .Remarks

            End With
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try
            Clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class