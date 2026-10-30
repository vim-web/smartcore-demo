Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class EmpLeavesettings
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
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Pickup(CmbLeaveType, 65, 0)
            Session("M_EntryMode") = "NEW"
            TxtMaxCarry.Text = "0"
            TxtMaxCarry.Enabled = False
        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_ID As Integer        Dim M_Emp_LeaveSetting As New SMARTCORE_BO.Emp_LeaveSettingBO        With M_Emp_LeaveSetting

            .EffectiveDate = Session("TransDate")
            .ID = 0
            .LeaveTypeID = CmbLeaveType.SelectedValue
            .LeaveMaxNoPerYear = TxtMaxLeave.Text
            .LeaveMaxCarryOver = TxtMaxCarry.Text
            .LeaveCount = TxtLeaveCount.Text
            .LeaveEnCashable = IIf(ChkEncash.Checked = True, "Y", "N")
            .LeaveCarryOver = IIf(Chk_CarryOverToNxtYr.Checked = True, "Y", "N")

        End With        Try            Dim M_Emp_LeaveSettingBL As New Emp_LeaveSettingBL            M_ID = M_Emp_LeaveSettingBL.Save_Data(M_Emp_LeaveSetting, Session("M_EntryMode"))            ShowAlertMessage("Saved Sucessfully")            clear()            M_Emp_LeaveSettingBL = Nothing        Catch ex As Exception            ShowAlertMessage("Saving Failed")        End Try    End Sub    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Locate_Data(ByVal M_Emp_LeaveSetting As SMARTCORE_BO.Emp_LeaveSettingBO)        With M_Emp_LeaveSetting


            CmbLeaveType.SelectedValue = .LeaveTypeID
            TxtMaxLeave.Text = .LeaveMaxNoPerYear
            TxtMaxCarry.Text = .LeaveMaxCarryOver
            TxtLeaveCount.Text = .LeaveCount
            ChkEncash.Checked = IIf(.LeaveEnCashable = "Y", ChkEncash.Checked = True, ChkEncash.Checked = False)
            Chk_CarryOverToNxtYr.Checked = IIf(.LeaveCarryOver = "Y", Chk_CarryOverToNxtYr.Checked = True, Chk_CarryOverToNxtYr.Checked = False)

        End With    End Sub
    'Private Sub Txt_ID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_ID.Validating    '    If Txt_ID.Text.Length = 0 Then    '        Call Clear_Controls()    '    Else    '        Try    '            Dim M_Emp_LeaveSetting As New SMARTCORE_BO.Emp_LeaveSettingBO    '            Dim M_Emp_LeaveSettingBL As New Emp_LeaveSettingBL    '            M_Emp_LeaveSetting = M_Emp_LeaveSettingBL.Locate_Data(Txt_ID.Text)    '            Locate_Data(M_Emp_LeaveSetting)    '            M_Emp_LeaveSettingBL = Nothing    '        Catch ex As Exception    '        End Try    '    End If    'End Sub
    Private Sub clear()
        TxtLeaveCount.Text = ""
        TxtMaxCarry.Text = ""
        TxtMaxLeave.Text = ""
        Chk_CarryOverToNxtYr.Checked = False
        ChkEncash.Checked = False
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        clear()
    End Sub

    Protected Sub Chk_CarryOverToNxtYr_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Chk_CarryOverToNxtYr.CheckedChanged
        TxtMaxCarry.Enabled = True
    End Sub
End Class