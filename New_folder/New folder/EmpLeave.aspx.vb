Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class EmpLeave
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1054
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Session("M_EntryMode") = "NEW"
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Pickup(Cmb_LeaveType, 65, 0)
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Session("SearchType") = "EMPLOYEE"
        End If
        If Session("SearchResult") <> "" Then
            TxtEmp_Id.Text = Session("SearchResult")
            TxtEmp_Id_TextChanged(Me.TxtEmp_Id, System.EventArgs.Empty)
            TxtEmp_Id.Focus()
            Session("SearchResult") = ""
        End If
        Session("SearchResult") = ""

    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_EmpLeave As New SMARTCORE_BO.EmpLeaveBO
        With M_EmpLeave

            .Fromdate = TxtFromDate.Text
            .EmpID = TxtEmp_Id.Text
            .LeaveType = Cmb_LeaveType.SelectedValue
            .EmpName = TxtEmp_Name.Text
            .Description = Txt_Description.Text
            .Todate = TxtToDate.Text

        End With

        Try
            Dim M_EmpLeaveBL As New EmpLeaveBL
            Dim M_EmpID As Integer
            M_EmpID = M_EmpLeaveBL.Save_Data(M_EmpLeave, Session("M_EntryMode"))
            ShowAlertMessage("Saved Sucessfully")
        Catch ex As Exception
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Private Sub Locate_Data(ByVal M_Leavesetting As SMARTCORE_BO.EmpLeaveBO)
        'If M_Leavesetting.EmpID = 0 Then
        '    Session("M_EntryMode") = "NEW"
        '    Exit Sub
        'End If
        With M_Leavesetting
            'TxtFromDate.Text = .Fromdate
            'TxtEmp_Id.Text = .EmpID
            'Cmb_LeaveType.SelectedValue = .LeaveType
            TxtEmp_Name.Text = .EmpName
            'Txt_Description.Text = .Description
            'TxtToDate.Text = .Todate
        End With
        Session("M_EntryMode") = "NEW"
    End Sub
    Private Sub Clear_Controls()
        TxtEmp_Id.Text = ""
        TxtEmp_Name.Text = ""
        TxtFromDate.Text = ""
        TxtToDate.Text = ""
        Txt_Description.Text = ""

    End Sub

    Protected Sub TxtEmp_Id_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtEmp_Id.TextChanged
        If TxtEmp_Id.Text.Length = 0 Then
            Call Clear_Controls()
        Else
            Try
                Dim M_EmpLeave As New SMARTCORE_BO.EmpLeaveBO
                Dim M_EmpLeaveBL As New EmpLeaveBL
                M_EmpLeave = M_EmpLeaveBL.Locate_Data(TxtEmp_Id.Text)
                Locate_Data(M_EmpLeave)
                M_EmpLeaveBL = Nothing
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub ImgSrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSrch.Click

    End Sub
End Class