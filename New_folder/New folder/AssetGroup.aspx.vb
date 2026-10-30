Imports SMARTCORE_BO
Imports SMARTCORE_BL
Partial Public Class WebForm8
    Inherits System.Web.UI.Page
    Private M_MasterType As String
    Dim FormID As Integer = 1016
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Clear_Values()
            TxtGrpCode.Focus()
            Session("SearchType") = "AssetGrp"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If
        If Session("SearchResult") <> "" Then
            If Session("SearchType") = "AssetGrp" Then
                TxtGrpCode.Text = Session("SearchResult")
                TxtGrpCode_TextChanged(Me.TxtGrpCode, System.EventArgs.Empty)
            End If
            Session("SearchResult") = ""
        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        SaveGroup()
        TxtGrpCode.Text = ""
        TxtGrpCode.Focus()
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Values()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
    Private Sub SaveGroup()
        Dim M_Group As New SMARTCORE_BO.FAGroupBO
        Dim M_GrpCode As String
        With M_Group
            .GrpDepPercentage = Val(TxtGrpDepPerc.Text)
            .GrpCode = TxtGrpCode.Text
            .GrpName = TxtGrpName.Text
            .GrpShortName = TxtGrpShorName.Text
        End With
        Try
            Dim M_FAGroupBL As New FAGroupBL
            M_GrpCode = M_FAGroupBL.Save_Data(Session("M_EntryMode"), M_Group)
            'MsgBox("Group Code : " & M_GrpCode)
            ShowAlertMessage("Saved Successfully")
            M_FAGroupBL = Nothing
            Clear_Values()
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
    Private Sub Clear_Values()
        Session("M_EntryMode") = "NEW"
        TxtGrpName.Text = ""
        TxtGrpDepPerc.Text = ""
        TxtGrpShorName.Text = ""

    End Sub

    Protected Sub TxtGrpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtGrpCode.TextChanged
        If TxtGrpCode.Text.Length = 0 Then
            Call Clear_Values()
        Else
            Try
                Dim M_Group As New SMARTCORE_BO.FAGroupBO
                Dim M_FAGroupBL As New FAGroupBL
                Dim M_GrpCod As String
                M_GrpCod = TxtGrpCode.Text
                Call Clear_Values()
                M_Group = M_FAGroupBL.Locate_Data(M_GrpCod)
                Locate_GroupData(M_Group)
                M_FAGroupBL = Nothing
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        End If
    End Sub
    Private Sub Locate_GroupData(ByVal M_Group As SMARTCORE_BO.FAGroupBO)
        Try
            If M_Group.GrpCode = 0 Then

                Session("M_EntryMode") = "NEW"
                Exit Sub
            End If
            With M_Group
                TxtGrpDepPerc.Text = .GrpDepPercentage
                TxtGrpName.Text = .GrpName
                TxtGrpShorName.Text = .GrpShortName
            End With
            Session("M_EntryMode") = "VIEW"


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class


