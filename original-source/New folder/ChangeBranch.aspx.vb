Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Partial Public Class ChangeBranch
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim FormID As Integer = 1016
    Shared Obj_UserRights As Object

    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        Dim M_Dt As New DataTable
        Session("SearchType") = "USER"
        M_CommonBL.Fill_Branch(DrpOrgBranch, False)

        BindBranch()
        M_CommonBL = Nothing
    End Sub


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
                Load_DefaultValues()
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                CmbAutBranch.Enabled = False
            End If
            If Session("SearchResult") <> "" Then
                TxtUserEmpId.Text = Session("SearchResult")
                TxtUserEmpId_TextChanged(Me.TxtUserEmpId, System.EventArgs.Empty)
                Session("SearchResult") = ""

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


    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub



    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        ClearData()
    End Sub

    Private Sub ClearData()
        'TxtConPassword.Text = ""
        'TxtUsrName.Text = ""
        'TxtPassword.Text = ""

        TxtRemarks.Text = ""

    End Sub

    Protected Sub CmdSave_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click




        Dim M_UsrRegBO As New UserBO
        Dim M_UsrRegBL As New UserBL


        Dim Dt_BranchArea As New DataTable
        Dt_BranchArea.TableName = "Table"
        Dt_BranchArea.Columns.Add("BranchCode")
        Dim DtRow As DataRow
        For i As Integer = 0 To GvBranchDetails.Rows.Count - 1

            Dim ChkView As CheckBox = DirectCast(GvBranchDetails.Rows(i).Cells(0).FindControl("ChkSelect"), CheckBox)
            If ChkView.Checked Then
                DtRow = Dt_BranchArea.NewRow()
                DtRow(0) = GvBranchDetails.Rows(i).Cells(1).Text
                Dt_BranchArea.Rows.Add(DtRow)
            End If
        Next
        Dim AreaMapping As New System.IO.StringWriter()
        Dt_BranchArea.WriteXml(AreaMapping)


        Try
            With M_UsrRegBO
                .UserName = TxtUserName.Text
                .UserType = "0"
                .UserID = "1"
                .UserEmpID = TxtUserEmpId.Text
                .UserPwd = TxtRemarks.Text
                .UserPwdValidUpto = "01/01/1900"
                .UserStatus = IIf(CmbUserStatus.SelectedIndex = 0, "Y", "N")
                .UserAllotedUser = Session("LoginID")
                .UserAllotedTime = "01/01/1900"
                .UserActive = IIf(CmbUserStatus.SelectedIndex = 0, "Y", "N")
                .UserStatusChgUser = Session("LoginID")
                .UserStatusChgTime = "01/01/1900"
                .UserTbar = ""
                .AssignedBranch = IIf(ChkAll.Checked = False, DrpOrgBranch.SelectedValue, "%")
                .UserOtherBranch = AreaMapping.ToString
            End With
            If RdoOption.SelectedValue = "C" Then

                M_UsrRegBL.Save_Data(M_UsrRegBO, "CHANGEBRANCH")
                ShowAlertMessage("User Transfered Successfully")

            ElseIf RdoOption.SelectedValue = "D" Then

                M_UsrRegBL.Save_Data(M_UsrRegBO, "DEACTIVE")
                ShowAlertMessage("Updated Successfully")
            End If
            
            ClearData()

        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub
    Protected Sub BindBranch()
        Dim M_CommonBL As New CommonBL
        Dim M_dt As DataTable
        M_dt = M_CommonBL.Get_BranchAreaRegion(TxtUserEmpId.Text)
        If M_dt.Rows.Count > 0 And TxtUserEmpId.Text <> "" Then
            GvBranchDetails.DataSource = M_dt
            GvBranchDetails.DataBind()
        End If
    End Sub
    'Protected Sub CmbUserName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbUserName.SelectedIndexChanged
    '    Try
    '        Dim M_CommBl As New CommonBL
    '        Dim Dt As DataTable
    '        Dt = M_CommBl.GetUserDetails(CmbUserName.SelectedValue)
    '        M_CommBl = Nothing

    '        If Dt.Rows.Count > 0 Then
    '            'CmbBranch.SelectedValue = Dt.Rows(0).Item("Assigned_Branch")
    '            CmbBranchNew.SelectedValue = IIf(Dt.Rows(0).Item("Assigned_Branch") = "%", "<-- ALL -->", Dt.Rows(0).Item("Assigned_Branch"))
    '            TxtEmpName.Text = Dt.Rows(0).Item("Emp_Name")
    '            CmbUserStatus.SelectedIndex = IIf(Dt.Rows(0).Item("User_Active") = "Y", 0, 1)
    '            CmbAutBranch.SelectedIndex = IIf(Dt.Rows(0).Item("Assigned_Branch") <> "%", 0, 1)
    '        End If
    '        BindBranch()
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Protected Sub RdoOption_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdoOption.SelectedIndexChanged
        Try
            If RdoOption.SelectedValue = "C" Then
                'DivAssign.Visible = True
                LblReason.Text = "REASON FOR BRANCH CHANGE"
            Else
                DivAssign.Visible = False
                LblReason.Text = "REASON FOR IN-ACTIVE"
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
    '    Try
    '        Dim M_CommonBL As New CommonBL
    '        Dim M_Dt As New DataTable
    '        M_Dt = M_CommonBL.Fill_UserChangeBranch(IIf(CmbBranch.SelectedValue = "<-- ALL -->", "%", CmbBranch.SelectedValue))
    '        If M_Dt.Rows.Count > 0 Then
    '            CmbUserName.DataTextField = "User_Name"
    '            CmbUserName.DataValueField = "Emp_ID"
    '            CmbUserName.DataSource = M_Dt
    '            CmbUserName.DataBind()
    '        End If
    '        CmbUserName_SelectedIndexChanged(Me.CmbUserName, System.EventArgs.Empty)
    '        M_CommonBL = Nothing
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub GvBranchDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBranchDetails.RowDataBound
        Try
            If e.Row.Cells(6).Text = "Y" Then
                Dim Check As CheckBox = DirectCast(e.Row.Cells(0).FindControl("ChkSelect"), CheckBox)
                Check.Checked = True
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ChkAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAll.CheckedChanged
        Try
            ChkExisting.Checked = False
            If ChkAll.Checked = True Then

                Dim i As Integer
                For i = 0 To GvBranchDetails.Rows.Count - 1
                    Dim row As GridViewRow = GvBranchDetails.Rows(i)
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = True
                Next
            ElseIf ChkAll.Checked = False Then
                For i = 0 To GvBranchDetails.Rows.Count - 1
                    Dim row As GridViewRow = GvBranchDetails.Rows(i)
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = False
                Next

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChkExisting_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkExisting.CheckedChanged
        ChkAll.Checked = False
        BindBranch()
    End Sub

    Protected Sub TxtUserEmpId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtUserEmpId.TextChanged
        Try
            Dim DtTab As New DataTable()
            Dim DtTab1 As New DataTable()
            Dim M_UserBL As New UserBL
            DtTab = M_UserBL.Get_UserDetails(TxtUserEmpId.Text)
            If DtTab.Rows.Count > 0 Then
                TxtEmpName.Text = DtTab.Rows(0).Item("Emp_Name").ToString()
                TxtUserName.Text = DtTab.Rows(0).Item("User_Name").ToString()
                BindBranch()
                If DtTab.Rows(0).Item("assigned_branch") = "%" Then
                    ChkAll.Checked = True
                    ChkAll_CheckedChanged(Me.ChkAll, System.EventArgs.Empty)
                    DrpOrgBranch.SelectedValue = "999"
                Else
                    ChkAll.Checked = False
                    DrpOrgBranch.SelectedValue = DtTab.Rows(0).Item("assigned_branch")
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class