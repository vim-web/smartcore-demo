Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Partial Public Class PickUpMaster
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


                Load_DefaultValues()
                Session("SearchType") = "PICKUP"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            End If
            If Session("SearchResult") <> "" Then
                TxtPickUpCode.Text = Session("SearchResult")
                TxtPickUpCode_TextChanged(Me.TxtPickUpCode, System.EventArgs.Empty)
                Session("SearchResult") = ""

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_Common As New CommonBL
            Dim M_Dt As New DataTable
            M_Common.GetPickuptype(CmbPkcType, Session("TransDate"))
            CmbActive.Items.Add("Yes")
            CmbActive.Items.Add("No")
            M_Dt = M_Common.Get_MaxPickupCode()

            If M_Dt.Rows.Count > 0 Then
                TxtPickUpCode.Text = M_Dt.Rows(0).Item("Pkc_Code")
            End If
            LoadParentId()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Private Sub LoadParentId()
        Try
            Dim M_Common As New CommonBL
            M_Common.GetParentType(CmbParentId, Session("TransDate"), CmbPkcType.SelectedValue)
            If (CmbParentId.Items.Count > 0) Then
                TrParentID.Visible = True
            Else
                TrParentID.Visible = False
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Private Sub clear()
        Try

            TxtPickUpCode.Text = ""
            TxtPickupDescription.Text = ""
            CmbActive.SelectedIndex = 0
            ' CmbParentId.SelectedIndex = 0
            CmbPkcType.SelectedIndex = 0

            Dim M_Common As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_Common.Get_MaxPickupCode()
            If M_Dt.Rows.Count > 0 Then
                TxtPickUpCode.Text = M_Dt.Rows(0).Item("Pkc_Code")
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

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Try
            Dim M_PickUpMaster As New SMARTCORE_BO.PickupMasterBO

            With M_PickUpMaster
                .PkcCode = TxtPickUpCode.Text
                .PkcDescription = TxtPickupDescription.Text
                .PkcLocked = CmbActive.SelectedIndex.ToString()
                .PkcType = CmbPkcType.SelectedValue
                .PkcParentId = IIf(TrParentID.Visible = True, CmbParentId.SelectedValue, 0)
            End With
            Try
                Dim M_AccCode As String
                Dim M_PickUpMasterBL As New PickupMasterBL
                M_AccCode = M_PickUpMasterBL.Save_Data(M_PickUpMaster, Session("M_EntryMode"))
                If M_AccCode > 0 Then
                    If Session("M_EntryMode") = "NEW" Then
                        ShowAlertMessage("Saved Successfully")
                    Else
                        ShowAlertMessage("Updated Successfully")
                    End If

                    clear()
                Else
                    ShowAlertMessage("Not Saved Successfully")

                End If
                M_PickUpMasterBL = Nothing
            Catch ex As Exception

            End Try




        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try

            clear()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub CmbPkcType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbPkcType.SelectedIndexChanged
        Try


            LoadParentId()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub TxtPickUpCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtPickUpCode.TextChanged
        Try
            Dim M_PickUpMaster As New SMARTCORE_BO.PickupMasterBO
            Dim M_PickUpMasterBL As New PickupMasterBL
            M_PickUpMaster = M_PickUpMasterBL.Locate_Data(TxtPickUpCode.Text)
            If M_PickUpMaster.PkcCode = 0 Then
                Dim str As String
                str = TxtPickUpCode.Text
                clear()
                TxtPickUpCode.Text = str
                TxtPickUpCode.Focus()
                Session("M_EntryMode") = "NEW"
                Exit Sub
            End If
            Session("M_EntryMode") = "UPDATE"

            With M_PickUpMaster
                TxtPickUpCode.Text = .PkcCode
                TxtPickupDescription.Text = .PkcDescription
                CmbPkcType.SelectedValue = .PkcType
                If .PkcLocked.ToString() = 1 Then
                    CmbActive.SelectedIndex = 0
                Else
                    CmbActive.SelectedIndex = 1
                End If
                LoadParentId()
                If TrParentID.Visible = True Then
                    CmbParentId.SelectedValue = .PkcParentId
                End If
                CmbActive.SelectedIndex = .PkcLocked.ToString()
            End With
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
End Class